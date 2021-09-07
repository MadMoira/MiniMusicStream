using System.Linq;

namespace MiniMusicStream.Models
{
    public class Song
    {
        private static readonly string[] FORMATS = { ".mp3" };
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Album Album { get; set; }

        public static bool CheckIfItsSong(string filename)
        {
            var ext = filename.Split(".").Last();
            return FORMATS.Contains(ext);
        }
        
    }
}