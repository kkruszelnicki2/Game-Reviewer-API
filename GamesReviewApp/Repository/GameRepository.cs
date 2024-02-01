using GamesReviewApp.Data;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;

namespace GamesReviewApp.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext _context;
        public GameRepository(DataContext context) {
            _context = context;
        }

        public bool CreateGame(int producentId, int tagId, Game game)
        {
            var gameProducentEntity = _context.Producers.Where(p => p.Id == producentId).FirstOrDefault();
            var tag = _context.Tags.Where(t => t.Id == tagId).FirstOrDefault();

            var gameProducent = new GameProducent()
            {
                Producent = gameProducentEntity,
                Game = game,
            };

            _context.Add(gameProducent);

            var gameTag = new GameTag()
            {
                Tag = tag,
                Game = game,
            };

            _context.Add(gameTag);

            _context.Add(game);

            return Save();
        }

        public bool GameExists(int gameId)
        {
            return _context.Games.Any(g => g.Id == gameId);
        }

        public Game GetGame(int id)
        {
            return _context.Games.Where(g => g.Id == id).FirstOrDefault();
        }

        public Game GetGame(string name)
        {
            return _context.Games.Where(g => g.Name == name).FirstOrDefault();
        }

        public decimal GetGameRating(int gameId)
        {
            var review = _context.Reviews.Where(g => g.Game.Id == gameId);

            if(review.Count() <= 0)
            {
                return 0;
            }

            return ((decimal)review.Sum(r => r.Rate) / review.Count());
        }

        public ICollection<Game> GetGames()
        {
            return _context.Games.OrderBy(g => g.Name).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool UpdateGame(int producentId, int tagId, Game game)
        {
            _context.Update(game);
            return Save();
        }
    }
}
