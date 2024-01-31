namespace GamesReviewApp.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Producent> Producents { get; set;}
    }
}
