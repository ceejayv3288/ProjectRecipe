using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectRecipe.Services.Interfaces
{
    public interface IMemoryCacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, DateTimeOffset absoluteExpiry);
    }
}
