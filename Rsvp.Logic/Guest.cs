using System;

namespace Rsvp.Logic
{
    public class Guest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Accepted { get; set; }
    }
}