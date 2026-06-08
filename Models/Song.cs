namespace MusicArchive.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Artist { get; set; } = string.Empty;
        public string Album { get; set; } = string.Empty;
        public int Year { get; set; }
        public int DurationSeconds { get; set; }
        public string Genre { get; set; } = string.Empty;

        public override string ToString()
        {
            int minutes = DurationSeconds / 60;
            int seconds = DurationSeconds % 60;
            return $"[{Id}] {Title} - {Artist} | {Album} ({Year}) | {Genre} | {minutes}:{seconds:D2}";
        }
    }
}