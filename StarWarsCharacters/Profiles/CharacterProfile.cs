using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWarsCharacters.Entities;
using StarWarsCharacters.Models;

namespace StarWarsCharacters.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            CreateMap<People, Character>();
            CreateMap<Character, CharacterDTO>()
                .ForMember(dest => dest.Homeworld,
                           opt => opt.MapFrom(src => src.PlanetHomeworld))
                .ForMember(dest => dest.Species,
                           opt => opt.MapFrom(src => src.KindSpecies.DefaultIfEmpty().Select(x => x.Name)))
                .ForMember(dest => dest.AverageRating,
                           opt => opt.MapFrom(src => src.Ratings.DefaultIfEmpty().Average(r => r.Rating)))
                .ForMember(dest => dest.MaxRating,
                           opt => opt.MapFrom(src => src.Ratings.DefaultIfEmpty().Max(r => r.Rating)));
        }
    }
}
