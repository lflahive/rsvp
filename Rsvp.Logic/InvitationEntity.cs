using Microsoft.Azure.Cosmos.Table;

namespace Rsvp.Logic
{
    public class InvitationEntity : TableEntity
    {
        public string Guests { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}