using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kristianstad.CompareDomain.Helpers
{
    public interface ICacheManager
    {
        Object GetCache(string key);
        void SetCache(string key, Object data, int cacheItemPolicy);
        void SetCache(string key, object data);
        bool HasValue(string key);
        void RemoveFromCache(string key);
    }
}
