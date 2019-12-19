using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarWarsCharacters.Entities;
using StarWarsCharacters.Models;

namespace StarWarsCharacters.Profiles
{
    public class CharacterRatingProfile : Profile
    {
        public CharacterRatingProfile()
        {
            CreateMap<CharacterRating, CharacterRatingCreationDTO>().ReverseMap();
            CreateMap<CharacterRating, CharacterRatingDTO>().ReverseMap();
        }
    }
}
