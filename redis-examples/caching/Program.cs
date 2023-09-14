
using StackExchange.Redis;

// Connect to the Redis server
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis database instance
var db = redis.GetDatabase();

// Key for caching
string cacheKey = "myCacheKey";

// Check if the data is already cached
var cachedData = await db.StringGetAsync(cacheKey);

if (!cachedData.IsNullOrEmpty)
{
    Console.WriteLine("Cache hit!");
    Console.WriteLine($"Cached Data: {cachedData}");
}
else
{
    Console.WriteLine("Cache miss!");

    // If data is not cached, fetch it and store it in the cache
    string newData = "This is the data to cache.";
    Console.WriteLine($"Fetching and caching data: {newData}");

    // Store the data in the cache with a time-to-live (TTL)
    await db.StringSetAsync(cacheKey, newData, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
}

// Close the Redis connection
redis.Close();

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();