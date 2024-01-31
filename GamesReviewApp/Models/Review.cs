namespace GamesReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Text { get; set; } 
        public Reviewer Reviewer { get; set; }
        public Game Game { get; set; }
    }
}
