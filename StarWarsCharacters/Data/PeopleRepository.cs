using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using StarWarsCharacters.Entities;
using StarWarsCharacters.Helpers;

namespace StarWarsCharacters.Data
{
    public class PeopleRepository : ApiRepository<People>, IEntityRepositoryAsync<People>
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;
        private readonly string _apiURL;

        public PeopleRepository(IDistributedCache cache, IConfiguration configuration)
            :base(configuration)
        {
            this._cache = cache;
            this._configuration = configuration;
            this._apiURL = configuration.GetValue<string>("ApiStarWars:People");
        }

        public async Task<People> GetById(int Id)
        {
            try
            {
                People people;

                people = await GetByIdInCache(Id);
                if (people == null)
                {
                    people = await GetByIdInApi(Id);
                }

                if (people != null)
                {
                    if (!String.IsNullOrWhiteSpace(people.HomeworldURL))
                    {
                        people.PlanetHomeworld = await (new PlanetRepository(_cache, _configuration)).GetByUrl(people.HomeworldURL);
                    }

                    if (people.SpeciesURLs != null && people.SpeciesURLs.Any())
                    {
                        var tasksGetSpecies = people.SpeciesURLs.Select(x => (new KindRepository(_cache, _configuration)).GetByUrl(x));
                        var resultGetSpecies = await Task.WhenAll(tasksGetSpecies);
                        people.KindSpecies = resultGetSpecies.ToList();
                    }
                }

                return people;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<People> GetByIdInCache(int Id)
        {
            string apiURLCompleta = _apiURL + Id;
            People people = await _cache.RetrieveFromCache<People>(apiURLCompleta);
            return people;
        }

        private async Task<People> GetByIdInApi(int Id)
        {
            string apiURLCompleta = _apiURL + Id;
            People people = await GetByUrl(apiURLCompleta);

            await _cache.SaveToCache<People>(apiURLCompleta, people, _cacheDurationInHours);

            return people;
        }
    }
}
