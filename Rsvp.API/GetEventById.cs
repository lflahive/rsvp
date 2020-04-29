using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Rsvp.API
{
    public class GetEventById : BaseFunction
    {
        [FunctionName("GetEventById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            if(String.IsNullOrWhiteSpace(req.Query["eventId"]))
                return new BadRequestResult();

            if(!Guid.TryParse(req.Query["eventId"], out var eventId))
                return new BadRequestResult();

            var invitation = await Repository.GetEventAsync(eventId);

            if(invitation == null)
                return new NotFoundResult();

            return new JsonResult(invitation);
        }
    }
}
