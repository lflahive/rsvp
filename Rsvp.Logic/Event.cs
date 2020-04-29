using System;
using System.Threading.Tasks;
using FluentValidation;

namespace Rsvp.Logic
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }

        public async Task SaveAsync(RsvpRepository repository)
        {
            await repository.CreateEventAsync(this);
        }
    }

    public class EventValidator : AbstractValidator<Event> {
        public EventValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Event Name is required.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Event Date is required.");        
        }
    }
}
