using StackExchange.Redis;

// Connect to the Redis server
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis database instance
var db = redis.GetDatabase();

int stopCode = 0; 
do
{
    Console.Write("Input job code");
    int.TryParse(Console.ReadLine(), out stopCode);

    await AddJobToQueue(db, "job code: " + stopCode);

}while (stopCode != 0);

redis.Close();

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();

static async Task AddJobToQueue(IDatabase db, string jobData)
{
    // Generate a unique job ID (e.g., using GUID)
    string jobId = Guid.NewGuid().ToString();

    // Add the job data to a Redis list (the job queue)
    await db.ListLeftPushAsync("job-queue", jobData);

    // Store the job ID for later tracking or removal if needed
    await db.HashSetAsync("job-details", jobId, jobData);
}