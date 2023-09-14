using StackExchange.Redis;
using System.Text.Json;

// Connect to Redis
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis database instance
var db = redis.GetDatabase();

// looping make events
int stopCode = 0;
do
{
    Console.Write("push your event number(0 for exit): ");
    int.TryParse(Console.ReadLine(), out stopCode );

    if(stopCode != 0)
    {
        await db.ListLeftPushAsync("click-events1", JsonSerializer.Serialize(new
        {
            Title = "hey, i make new event",
            EventCode = stopCode,
        }));
        Console.WriteLine($"you make event {stopCode} successfully!");
    }

}while (stopCode != 0);