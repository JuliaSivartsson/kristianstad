
using Kristianstad.CompareDomain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
// using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.DAL
{
    public class CacheManager : ICacheManager
    {
        private static readonly MemoryCache Cache = MemoryCache.Default;

        /// <summary>
        /// Get cached data from memory
        /// </summary>
        /// <param name="key">Key name of cache location</param>
        /// <returns>Cached data as an Object</returns>
        public object GetCache(string key)
        {
            return Cache[key];
        }

        /// <summary>
        /// Saves an object to cache memory
        ///
        /// </summary>
        /// <param name="key">Associated Key name</param>
        /// <param name="value">Value to save in cache</param>
        /// <param name="cacheItemPolicy">Lifetime of cache in seconds</param>
        public void SetCache(string key, object value, int cacheItemPolicy)
        {
            if (key != null && value != null)
            {
                CacheItem cacheItem = new CacheItem(key, value);
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(cacheItemPolicy))
                };

                Cache.Add(cacheItem.Key, cacheItem.Value, policy);
            }
        }

        /// <summary>
        /// Saves an object to cache memory
        /// Cache will expire after 1 day 
        /// </summary>
        /// <param name="key">Associated Key name</param>
        /// <param name="value">Value to save in cache</param>
        public void SetCache(string key, object value)
        {
            if (key != null && value != null)
            {
                CacheItem cacheItem = new CacheItem(key, value);
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddDays(1))
                };

                Cache.Add(cacheItem.Key, cacheItem.Value, policy);
            }
        }


        /// <summary>
        /// Checks if cache has value
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>bool</returns>
        public bool HasValue(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// Deletes key/value from Cache
        /// </summary>
        /// <param name="key">Key name</param>
        public void RemoveFromCache(string key)
        {
            if (this.HasValue(key))
            {
                Cache.Remove(key);
            }
        }
    }
}