using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StarWarsCharacters.Entities;
using StarWarsCharacters.Helpers;

namespace StarWarsCharacters.Data
{
    public class PlanetRepository : ApiRepository<Planet>, IEntityRepositoryAsync<Planet>
    {
        private readonly IDistributedCache _cache;
        private readonly string _apiURL;

        public PlanetRepository(IDistributedCache cache, IConfiguration configuration)
            :base(configuration)
        {
            this._cache = cache;
            this._apiURL = configuration.GetValue<string>("ApiStarWars:Planet");
        }

        public async Task<Planet> GetById(int Id)
        {
            try
            {
                Planet planet;

                planet = await GetByIdInCache(Id);
                if (planet == null)
                {
                    planet = await GetByIdInApi(Id);
                }

                return planet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Planet> GetByIdInCache(int Id)
        {
            string apiURLCompleta = _apiURL + Id;
            Planet planet = await _cache.RetrieveFromCache<Planet>(apiURLCompleta);
            return planet;
        }

        private async Task<Planet> GetByIdInApi(int Id)
        {
            string apiURLCompleta = _apiURL + Id;
            Planet planet = await GetByUrl(apiURLCompleta);

            await _cache.SaveToCache<Planet>(apiURLCompleta, planet, _cacheDurationInHours);

            return planet;
        }
    }
}
