using AutoMapper;
using GamesReviewApp.Data;
using GamesReviewApp.Interfaces;
using GamesReviewApp.Models;


namespace GamesReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.OrderBy(c => c.Name).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByProducent(int producentId)
        {
            return _context.Producers.Where(p => p.Id == producentId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Producent> GetProducersFromCountry(int countryId)
        {
            return _context.Producers.Where(c => c.Country.Id == countryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return (saved > 0);
        }
    }
}
