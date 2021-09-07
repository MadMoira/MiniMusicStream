using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MiniMusicStream.Models;
using MiniMusicStream.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace MiniMusicStream.Controllers
{
    public class PaginatedResponse<T>
    {
        public int page { get; set; }
        public List<T> items { get; set; }
        public string url { get; set; }
    }

    [Route("api/v1/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;
        private readonly IConfiguration _config;
        private readonly LinkGenerator _linkGenerator;
        private readonly int pageSize = 2;

        public LibraryController(LibraryContext libraryContext, IConfiguration config, LinkGenerator linkGenerator)
        {
            _libraryContext = libraryContext;
            _config = config;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("/library/artists")]
        public IActionResult RetrieveArtists([FromQuery(Name = "page")] int? page)
        {
            var currentPage = page ?? 1;

            var artists = _libraryContext.Artists
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var url = _linkGenerator.GetUriByAction(HttpContext, null, null, new { page = ++currentPage });

            var paginatedResponse = new PaginatedResponse<Artist> { page = currentPage, items = artists, url = url };
            return Ok(paginatedResponse);
        }

        [HttpGet("/library/{artist_id}/albums")]
        public IActionResult RetrieveAlbumsFromArtist(int artist_id, [FromQuery(Name = "page")] int? page)
        {
            var artist = _libraryContext.Artists.Find(artist_id);
            if (artist == null) return NotFound();

            var currentPage = page ?? 1;
            var albums = _libraryContext.Albums
                .Where(s => s.Artist == artist)
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(albums);
        }

        [HttpGet("/library/{album_id}/songs")]
        public IActionResult RetrieveSongsFromAlbum(int album_id, [FromQuery(Name = "page")] int? page)
        {
            var album = _libraryContext.Albums.Find(album_id);
            if (album == null) return NotFound();

            var currentPage = page ?? 1;
            var songs = _libraryContext.Songs
                .Where(s => s.Album == album)
                .OrderBy(s => s.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return Ok(songs);
        }

        [HttpGet("/sync")]
        public IActionResult ReadLibrary()
        {
            _libraryContext.Database.ExecuteSqlRaw("DELETE FROM Songs");
            _libraryContext.Database.ExecuteSqlRaw("DELETE FROM Albums");
            _libraryContext.Database.ExecuteSqlRaw("DELETE FROM Artists");

            var artists = Directory.GetDirectories(_config["StartingPath"]);
            foreach (var artist in artists)
            {
                var artistName = artist.Split(Path.DirectorySeparatorChar).Last();
                var newArtist = new Artist { Name = artistName, Path = artist };
                _libraryContext.Artists.Add(newArtist);

                var albums = Directory.GetDirectories(artist);
                foreach (var album in albums)
                {
                    var albumName = album.Split(Path.DirectorySeparatorChar).Last();
                    var newAlbum = new Album
                    {
                        Name = albumName,
                        Path = album,
                        Artist = newArtist
                    };
                    _libraryContext.Albums.Add(newAlbum);

                    var songs = Directory.GetFiles(album);
                    foreach (var song in songs)
                    {
                        var songName = song.Split(Path.DirectorySeparatorChar).Last();
                        if (!Song.CheckIfItsSong(songName)) continue;

                        var newSong = new Song
                        {
                            Name = songName,
                            Path = song,
                            Album = newAlbum,
                        };
                        _libraryContext.Songs.Add(newSong);
                    }
                }
            }
            _libraryContext.SaveChanges();
            return Ok(artists);
        }
    }
}