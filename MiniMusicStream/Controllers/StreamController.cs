using System;
using Microsoft.AspNetCore.Mvc;
using MiniMusicStream.Repositories;

namespace MiniMusicStream.Controllers
{
    public class SongResponse
    {
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Performer { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int Track { get; set; }
        public double Duration {get;set;}
    }

    [ApiController]
    [Route("api/[controller]")]
    public class StreamController : ControllerBase
    {
        private readonly LibraryContext libraryContext;

        public StreamController(LibraryContext libraryContext)
        {
            this.libraryContext = libraryContext;
        }

        [HttpGet("song/metadata/{id}")]
        public IActionResult RetrieveSongMetadata(int id)
        {
            var song = libraryContext.Songs.Find(id);
            if (song == null) return NotFound();

            var tfile = TagLib.File.Create(song.Path);
            TagLib.Id3v2.Tag tags = (TagLib.Id3v2.Tag)tfile.GetTag(TagLib.TagTypes.Id3v2);

            foreach (var frame in tags.GetFrames())
            {
                Console.WriteLine($"{frame.FrameId}: {frame.ToString()}");
            }

            var songMetadata = new SongResponse
            {
                Album = tags.Album,
                Artist = tags.FirstAlbumArtist,
                Performer = tags.FirstPerformer,
                Comment = tags.Comment,
                Title = tags.Title,
                Year = (int)tags.Year,
                Track = (int)tags.Track,
                Duration = tfile.Properties.Duration.TotalSeconds,
            };

            return Ok(songMetadata);
        }

        [HttpGet("song/{id}")]
        public IActionResult StreamSong(int id)
        {
            var song = libraryContext.Songs.Find(id);
            if (song == null) return NotFound();

            return PhysicalFile(song.Path, "audio/mpeg", enableRangeProcessing: true);
        }
    }
}