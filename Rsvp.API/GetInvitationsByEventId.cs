using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;

namespace Rsvp.API
{
    public class GetInvitationsByEventId : BaseFunction
    {
        [FunctionName("GetInvitationsByEventId")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            if(String.IsNullOrWhiteSpace(req.Query["eventId"]))
                return new BadRequestResult();

            if(!Guid.TryParse(req.Query["eventId"], out var eventId))
                return new BadRequestResult();

            var invitations = Repository.GetInvitationsByEventId(eventId);

            if(!invitations.Any())
                return new NotFoundResult();

            return new JsonResult(invitations);
        }
    }
}
