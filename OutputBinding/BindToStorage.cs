using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.Storage;

namespace OutputBinding
{
    public static class BindToStorage
    {
        [FunctionName("BindToStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Queue("localQueueName"), StorageAccount("AzureWebJobsStorage")] ICollector<string> queueMessage,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string message = req.Query["message"];
            queueMessage.Add(message);

            string responseMessage = string.IsNullOrEmpty(message)
                ? "Pass a message in the query string."
                : $"Your message, {message} is queued. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
