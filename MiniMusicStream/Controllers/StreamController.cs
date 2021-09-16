using System;
using Microsoft.AspNetCore.Mvc;
using MiniMusicStream.Repositories;

namespace MiniMusicStream.Controllers
{
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
            foreach(var artist in tags.AlbumArtists) {
                Console.WriteLine(artist);
            }
            Console.WriteLine($"Album: {tags.Album}");
            Console.WriteLine($"Performer: {tags.FirstPerformer}");
            Console.WriteLine($"Comment: {tags.Comment}");
            Console.WriteLine($"Title: {tags.Title}");
            Console.WriteLine($"Year: {tags.Year}");
            Console.WriteLine($"Track: {tags.Track}");
            Console.WriteLine($"TrackCount: {tags.TrackCount}");

            foreach (var frame in tags.GetFrames())
            {
                Console.WriteLine($"{frame.FrameId}: {frame.ToString()}");
            }
            
            return Ok(tags.GetFrames());
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