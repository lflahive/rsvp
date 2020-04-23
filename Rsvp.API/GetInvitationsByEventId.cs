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
    public class GetInvitationsByEventId : BaseFunction
    {
        [FunctionName("GetInvitationsByEventId")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
           return new JsonResult(Repository.GetInvitationsByEventId(Guid.Parse(req.Query["eventId"])));
        }
    }
}