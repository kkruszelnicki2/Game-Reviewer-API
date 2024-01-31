
namespace GamesReviewApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<GameProducent> GameProducers { get; set; }
        public ICollection<GameTag> GameTags { get; set; }
    }
}
