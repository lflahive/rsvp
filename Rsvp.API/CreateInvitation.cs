using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rsvp.Logic;

namespace Rsvp.API
{
    public class CreateInvitation : BaseFunction
    {
        [FunctionName("CreateInvitation")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger log)
        {
            var invitation = JsonConvert.DeserializeObject<Invitation>(await req.ReadAsStringAsync());

            var validator = new InvitationValidator(Repository);
            var validatorResults = validator.Validate(invitation);
            if(!validatorResults.IsValid)
                return new BadRequestObjectResult(validatorResults.Errors);

            await invitation.SaveAsync(Repository);
            return new JsonResult(invitation);
        }
    }
}
