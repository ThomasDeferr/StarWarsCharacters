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
    public class KindRepository : ApiRepository<Kind>, IEntityRepositoryAsync<Kind>
    {
        private readonly IDistributedCache _cache;
        private readonly string _apiURL;

        public KindRepository(IDistributedCache cache, IConfiguration configuration)
            :base(configuration)
        {
            this._cache = cache;
            this._apiURL = configuration.GetValue<string>("ApiStarWars:Species");
        }

        public async Task<Kind> GetById(int Id)
        {
            try
            {
                Kind Kind;

                Kind = await GetByIdInCache(Id);
                if (Kind == null)
                {
                    Kind = await GetByIdInApi(Id);
                }

                return Kind;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<Kind> GetByIdInCache(int Id)
        {
            string apiURLCompleta = _apiURL + Id;
            Kind Kind = await _cache.RetrieveFromCache<Kind>(apiURLCompleta);
            return Kind;
        }

        private async Task<Kind> GetByIdInApi(int Id)
        {
            string apiURLCompleta = _apiURL + Id;
            Kind Kind = await GetByUrl(apiURLCompleta);

            await _cache.SaveToCache<Kind>(apiURLCompleta, Kind, _cacheDurationInHours);

            return Kind;
        }
    }
}
