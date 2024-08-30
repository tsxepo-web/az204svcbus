using Azure.Messaging.ServiceBus;

string connectionString  = "connectionString";

string queueName = "az204-queue";

ServiceBusClient client;
ServiceBusSender sender;

client = new ServiceBusClient(connectionString);
sender = client.CreateSender(queueName);

using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

for (int i =1; i <= 3; i++)
{
    if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
    {
        throw new Exception($"Exception {i} has occurred.");
    }
}

try
{
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of three messages has been published to the queue.");
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}

Console.WriteLine("Follow the directions in the exercise to review the results in the Azure portal.");
Console.WriteLine("Press any key to continue");
Console.ReadKey();