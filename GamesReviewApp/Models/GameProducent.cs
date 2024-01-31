namespace GamesReviewApp.Models
{
    public class GameProducent
    {
        public int GameId { get; set; }
        public int ProducentId { get; set; }
        public Game Game { get; set; }
        public Producent Producent { get; set; }
    }
}
