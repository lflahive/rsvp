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
    public class GetInvitationById : BaseFunction
    {
        [FunctionName("GetInvitationById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            if(String.IsNullOrWhiteSpace(req.Query["eventId"]) || String.IsNullOrWhiteSpace(req.Query["invitationId"]))
                return new BadRequestResult();

            if(!Guid.TryParse(req.Query["eventId"], out var eventId) || !Guid.TryParse(req.Query["invitationId"], out var invitationId))
                return new BadRequestResult();

            var invitation = await Repository.GetInvitationByIdAsync(eventId, invitationId);

            if(invitation == null)
                return new NotFoundResult();

            return new JsonResult(invitation);
        }
    }
}
