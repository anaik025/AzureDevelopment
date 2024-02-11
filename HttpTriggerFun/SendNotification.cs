using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HttpTriggerFun
{
    public static class SendNotification
    {
        [FunctionName("SendNotification")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string email = req.Query["emailId"];
            string message = req.Query["message"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            var envVar = System.Environment.GetEnvironmentVariable("Environment");

            string responseMessage = string.IsNullOrEmpty(email)
                ? "This HTTP triggered function executed successfully. Pass a email and message in the query string"
                : $"To, {email}. Message {message}. Environment - {envVar}";
           
            return new OkObjectResult(responseMessage);
        }
    }
}
