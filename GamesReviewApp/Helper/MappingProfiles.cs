using AutoMapper;
using GamesReviewApp.Dto;
using GamesReviewApp.Models;

namespace GamesReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Tag, TagDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Producent, ProducentDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
