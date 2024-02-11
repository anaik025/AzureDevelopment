using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusTrigger
{
    public class SendAlerts
    {
        [FunctionName("SendAlerts")]
        public void Run([ServiceBusTrigger("alertqueue", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
