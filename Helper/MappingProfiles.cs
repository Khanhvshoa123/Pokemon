using PokemonReviewApp.Models;
using PokemonReviewApp.NewFolder;
using AutoMapper;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();
        }
    }
}
