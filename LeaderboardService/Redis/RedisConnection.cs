using StackExchange.Redis;

namespace LeaderboardService.Redis;

public class RedisConnection
{
    private readonly ConnectionMultiplexer _connection;


    public RedisConnection()
    {
        var connectionString = Environment.GetEnvironmentVariable("Redis__Connection");
        _connection = ConnectionMultiplexer.Connect(connectionString);
    }

    public IDatabase GetDatabase()
    {
        return _connection.GetDatabase();
    }
}