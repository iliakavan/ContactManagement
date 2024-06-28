namespace ContactManagementV2.Services.Cache;

public interface ICacheService
{
    T? GetData<T>(string key);
    bool SetData<T>(string key, T value,DateTimeOffset ExpireTime);
    object DeleteData(string key);
    bool UpdateData<T> (string key, T value);

}