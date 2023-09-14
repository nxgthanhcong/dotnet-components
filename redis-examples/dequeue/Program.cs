using StackExchange.Redis;

// Connect to the Redis server
var redis = ConnectionMultiplexer.Connect("localhost"); // Replace with your Redis server connection string

// Create a Redis database instance
var db = redis.GetDatabase();

await ProcessJobsFromQueue(db);

redis.Close();

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();

static async Task ProcessJobsFromQueue(IDatabase db)
{
    while (true)
    {
        // Pop a job from the Redis list (the job queue)
        var jobData = await db.ListRightPopAsync("job-queue");

        if (!jobData.IsNullOrEmpty)
        {
            Console.WriteLine($"Processing job: {jobData}");

            // You can perform your job processing logic here
            // For this example, we'll just simulate a delay
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
        else
        {
            Console.WriteLine("No more jobs in the queue.");
            break;
        }
    }
}