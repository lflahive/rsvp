using System;
using Microsoft.Azure.Cosmos.Table;

namespace Rsvp.Logic
{
    public class EventEntity : TableEntity
    {
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}