using GamesReviewApp.Models;

namespace GamesReviewApp.Interfaces
{
    public interface IGameRepository
    {
        ICollection<Game> GetGames();
        Game GetGame(int id);
        Game GetGame(string name);
        decimal GetGameRating(int gameId);
        bool GameExists(int gameId);
        bool CreateGame(int producentId, int tagId, Game game);
        bool Save();
    }
}
