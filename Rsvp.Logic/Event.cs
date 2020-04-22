using System;

namespace Rsvp.Logic
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
