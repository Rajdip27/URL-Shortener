using System.Text.Json;
using StackExchange.Redis;

namespace UrlShortener.Infrastructure.Caching;

public interface IRedisCacheService
{
    /// <summary>
    /// Sets the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="expiry">The expiry.</param>
    /// <returns></returns>
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    Task<T?> GetAsync<T>(string key);
    /// <summary>
    /// Increments the asynchronous.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    Task<long> IncrementAsync(string key);
}
public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _db;
    public RedisCacheService(IConnectionMultiplexer mux) { _db = mux.GetDatabase(); }
    /// <summary>
    /// Sets the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <param name="expiry">The expiry.</param>
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, json, expiry);
    }

    /// <summary>
    /// Gets the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public async Task<T?> GetAsync<T>(string key)
    {
        var val = await _db.StringGetAsync(key);
        if (val.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(val!);
    }
    /// <summary>
    /// Increments the asynchronous.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public async Task<long> IncrementAsync(string key)
    {
        return await _db.StringIncrementAsync(key);
    }
}

