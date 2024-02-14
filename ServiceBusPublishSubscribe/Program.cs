using Azure.Messaging.ServiceBus;

namespace ServiceBusPublishSubscribe
{
    internal class Program
    {
        string connectionString = "";
        string queueName = "";

        string topicName = "";
        string topicScriptionName = "";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }


        public void PublishMessageToQueue(string message)
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(queueName);

            sender.SendMessageAsync(new ServiceBusMessage(message));
            client.DisposeAsync();

        }

        public async void SubscribeToQueue()
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);

            ServiceBusProcessor processor = client.CreateProcessor(queueName);

            try
            {
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Thread.Sleep(1000);

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        public void PublishMessageToTopic(string message)
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);

            ServiceBusSender sender = client.CreateSender(topicName);

            sender.SendMessageAsync(new ServiceBusMessage(message));
            client.DisposeAsync();

        }

        public async void SubscribeToTopic()
        {
            ServiceBusClient client = new ServiceBusClient(connectionString);

            ServiceBusProcessor processor = client.CreateProcessor(topicName,topicScriptionName);

            try
            {
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Thread.Sleep(1000);

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"ID: {body}");

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);

        }

        // handle any errors when receiving messages
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

    }
}
