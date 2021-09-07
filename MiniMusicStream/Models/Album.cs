namespace MiniMusicStream.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public Artist Artist { get; set; }
    }
}