using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Rsvp.Logic;

namespace Rsvp.API
{
    public class CreateEvent : BaseFunction
    {
        [FunctionName("CreateEvent")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            var body = await req.ReadAsStringAsync();
            var newEvent = JsonConvert.DeserializeObject<Event>(body);
            newEvent = await Repository.CreateEventAsync(newEvent);
            return new JsonResult(newEvent);
        }
    }
}
