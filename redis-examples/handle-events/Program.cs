
using StackExchange.Redis;
using System.Text.Json;

// Connect to Redis
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis database instance
var db = redis.GetDatabase();

// Retrieve and process the stored click events
while (true)
{
    // Pop and process click events from the Redis list
    var clickEventData = await db.ListRightPopAsync("click-events1");
    if (clickEventData != RedisValue.Null)
    {
        var clickEvent = JsonSerializer.Deserialize<CustomEvent>(clickEventData);

        // Perform real-time analytics on the click event data
        Console.WriteLine($"Catched {clickEvent.Title} code {clickEvent.EventCode}");
    }

    // Add a delay to simulate real-time processing
    //await Task.Delay(1000);
}

class CustomEvent
{
    public string Title { get; set; }
    public int EventCode { get; set; }
}