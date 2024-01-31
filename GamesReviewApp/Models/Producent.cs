namespace GamesReviewApp.Models
{
    public class Producent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public ICollection<GameProducent> GameProducers { get; set; }
    }
}
