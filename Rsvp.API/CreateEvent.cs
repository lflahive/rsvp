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
            var newEvent = JsonConvert.DeserializeObject<Event>(await req.ReadAsStringAsync());

            var validator = new EventValidator();
            var validatorResults = validator.Validate(newEvent);
            if(!validatorResults.IsValid)
                return new BadRequestObjectResult(validatorResults.Errors);

            await newEvent.SaveAsync(Repository);
            return new JsonResult(newEvent);
        }
    }
}
