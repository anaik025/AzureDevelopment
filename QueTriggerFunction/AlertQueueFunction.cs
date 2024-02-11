using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueTriggerFunction
{
    public class AlertQueueFunction
    {
        [FunctionName("AlertQueueFunction")]
        public void Run([QueueTrigger("AlertQueue", Connection = "AlertQueueConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
