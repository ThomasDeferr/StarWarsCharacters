using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsCharacters.Helpers
{
    public static class DistributedCacheExtension
    {
        public static async Task SaveToCache<T>(this IDistributedCache cache, string key, T item, double expirationInHours)
        {
            string json = JsonConvert.SerializeObject(item);

            if (!String.IsNullOrWhiteSpace(json))
            {
                await cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationInHours)
                });
            }
        }

        public static async Task<T> RetrieveFromCache<T>(this IDistributedCache cache, string key)
        {
            string json = await cache.GetStringAsync(key);

            if (!String.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }
    }
}
