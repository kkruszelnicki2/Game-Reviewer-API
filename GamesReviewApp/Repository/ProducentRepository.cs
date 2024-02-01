using AutoMapper;
using GamesReviewApp.Data;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;

namespace GamesReviewApp.Repository
{
    public class ProducentRepository : IProducentRepository
    {
        private readonly DataContext _context;

        public ProducentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateProducent(Producent producent)
        {
            _context.Add(producent);
            return Save();
        }

        public ICollection<Game> GetGameByProducent(int producentId)
        {
            return _context.GameProducers.Where(p => p.Producent.Id == producentId).Select(g => g.Game).ToList();
        }

        public Producent GetProducent(int prodId)
        {
            return _context.Producers.Where(p => p.Id == prodId).FirstOrDefault();
        }

        public ICollection<Producent> GetProducentOfAGame(int gameId)
        {
            return _context.GameProducers.Where(g => g.Game.Id == gameId).Select(p => p.Producent).ToList();
        }

        public ICollection<Producent> GetProducers()
        {
            return _context.Producers.OrderBy(p => p.Name).ToList();
        }

        public bool ProducentExists(int producentId)
        {
            return _context.Producers.Any(p => p.Id == producentId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
