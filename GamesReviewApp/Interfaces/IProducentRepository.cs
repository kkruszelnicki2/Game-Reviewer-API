using GamesReviewApp.Models;

namespace GamesReviewApp.Interfaces
{
    public interface IProducentRepository
    {
        ICollection<Producent> GetProducers();
        Producent GetProducent(int prodId);
        ICollection<Producent> GetProducentOfAGame(int gameId);
        ICollection<Game> GetGameByProducent(int producentId);
        bool ProducentExists(int producentId);
        bool CreateProducent(Producent producent);
        bool Save();
    }
}
