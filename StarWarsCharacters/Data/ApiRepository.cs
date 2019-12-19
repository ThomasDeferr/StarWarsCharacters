using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarWarsCharacters.Data
{
    public class ApiRepository<T>
    {
        protected readonly double _cacheDurationInHours;

        public ApiRepository(IConfiguration configuration)
        {
            this._cacheDurationInHours = configuration.GetValue<double>("ApiStarWars:CacheDurationInHours");
        }

        public async Task<T> GetByUrl(string URL)
        {
            T entity = default(T);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    DefaultContractResolver contractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };

                    entity = JsonConvert.DeserializeObject<T>(apiResponse, new JsonSerializerSettings
                    {
                        ContractResolver = contractResolver,
                        Formatting = Formatting.Indented
                    });
                }
            }
            return entity;
        }
    }
}
