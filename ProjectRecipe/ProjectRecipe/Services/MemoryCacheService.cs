using Microsoft.Extensions.Caching.Memory;
using ProjectRecipe.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions() { });
        }

        public T Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out T value))
                return value;
            else
                return default(T);
        }

        public void Set<T>(string key, T value, DateTimeOffset absoluteExpiry)
        {
            _cache.Set(key, value, absoluteExpiry);
        }
    }
}
