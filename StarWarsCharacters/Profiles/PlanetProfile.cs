using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWarsCharacters.Entities;
using StarWarsCharacters.Models;

namespace StarWarsCharacters.Profiles
{
    public class PlanetProfile : Profile
    {
        public PlanetProfile()
        {
            CreateMap<Planet, PlanetDTO>()
                .ForMember(dest => dest.KnowResidentsCount,
                           opt => opt.MapFrom(src => src.Residents.Count()));
        }
    }
}
