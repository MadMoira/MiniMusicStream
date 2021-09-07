using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMusicStream.Models;
using MiniMusicStream.Repositories;

namespace MiniMusicStream.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryContext _libraryContext;

        public LibraryController(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }
        
        [HttpGet]
        public IActionResult ReadLibrary()
        {
            _libraryContext.Database.ExecuteSqlRaw("DELETE FROM Songs");
            _libraryContext.Database.ExecuteSqlRaw("DELETE FROM Albums");
            _libraryContext.Database.ExecuteSqlRaw("DELETE FROM Artists");
            
            var artists = Directory.GetDirectories("F:\\Music");
            foreach (var artist in artists)
            {
                var artistName = artist.Split(Path.DirectorySeparatorChar).Last(); 
                var newArtist = new Artist {Name = artistName, Path = artist};
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