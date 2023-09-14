using StackExchange.Redis;

// Connect to the Redis server
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis subscriber instance
var subscriber = redis.GetSubscriber();

string channelName = "my-channel";
await subscriber.SubscribeAsync(channelName, (channel, message) =>
{
    Console.WriteLine($"Received message on channel {channel}: {message}");
});

//// Close the Redis connection
//redis.Close();

//Console.WriteLine("Press Enter to exit.");
Console.ReadLine();