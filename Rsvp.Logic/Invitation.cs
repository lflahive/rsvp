using System;
using System.Collections.Generic;

namespace Rsvp.Logic
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public List<Guest> Guests { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}