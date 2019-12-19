using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StarWarsCharacters.Contexts;
using StarWarsCharacters.Entities;

namespace StarWarsCharacters.Data
{
    public class CharacterRepository : IEntityRepositoryAsync<Character>
    {
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly ICharacterRatingRepository characterRatingRepository;

        public CharacterRepository(IMapper mapper, IDistributedCache cache, IConfiguration configuration, ICharacterRatingRepository characterRatingRepository)
        {
            this._mapper = mapper;
            this._cache = cache;
            this._configuration = configuration;
            this.characterRatingRepository = characterRatingRepository;
        }

        public async Task<Character> GetById(int Id)
        {
            try
            {
                Character character = null;
                
                People people = await (new PeopleRepository(_cache, _configuration).GetById(Id));

                if (people != null)
                {
                    character = _mapper.Map<Character>(people);

                    character.Ratings = await characterRatingRepository.GetByCharacter(Id);
                }

                return character;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
