using StackExchange.Redis;
using System.Text.Json;

namespace ContactManagementV2.Services.Cache;

public class CacheService : ICacheService
{
    private IDatabase _db;

    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _db = redis.GetDatabase();
    }

    public object DeleteData(string key)
    {
        var result = _db.KeyExists(key);
        if (result)
        {
            return _db.KeyDelete(key);
        }
        return false;
    }

    public T? GetData<T>(string key)
    {
        var value = _db.StringGet(key);

        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset ExpireTime)
    {
        TimeSpan ExpireDate = ExpireTime.DateTime.Subtract(DateTime.Now);
        return _db.StringSet(key, JsonSerializer.Serialize(value), ExpireDate);
    }

    public bool UpdateData<T>(string key, T value)
    {
        var result = _db.KeyExists(key);
        if (result)
        {
            return _db.StringSet(key, JsonSerializer.Serialize(value));
        }
        return false;
    }
}