using StackExchange.Redis;

// Connect to the Redis server
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis subscriber instance
var subscriber = redis.GetSubscriber();

string channelName = "my-channel";
int stopCode = 0;

do
{
    Console.Write("push your event number(0 for exit): ");
    int.TryParse(Console.ReadLine(), out stopCode);

    await subscriber.PublishAsync(channelName, $"i published {stopCode} here");
    Console.WriteLine($"Published: {stopCode}");

} while (stopCode != 0);

// Close the Redis connection
redis.Close();

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();