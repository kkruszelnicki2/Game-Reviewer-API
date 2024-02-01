using GamesReviewApp.Models;

namespace GamesReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountryByProducent(int producentId);
        ICollection<Producent> GetProducersFromCountry(int countryId);
        bool CountryExists(int id);
        bool CreateCountry(Country country);
        bool Save();
    }
}
