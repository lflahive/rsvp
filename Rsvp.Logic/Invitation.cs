using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Rsvp.Logic
{
    public class Invitation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EventId { get; set; }
        public List<Guest> Guests { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public async Task SaveAsync(RsvpRepository repository)
        {
            await repository.CreateInvitationAsync(this);
        }

        public async Task UpdateAsync(RsvpRepository repository)
        {
            var existingInvitation = await repository.GetInvitationByIdAsync(EventId, Id);
            
            // Iterate over the existing guests so that we only change the acceptance. 
            // Don't allow new guests to be added or old guests to be removed.
            existingInvitation.Guests.ForEach(guest => {
                var matchingGuest = Guests.FirstOrDefault(x => x.Id == guest.Id);
                guest.Accepted = matchingGuest.Accepted;
            });

            await repository.UpdateInvitationAsync(existingInvitation);
        }
    }

    public class InvitationValidator : AbstractValidator<Invitation> 
    {
        private RsvpRepository _repository;
        public InvitationValidator(RsvpRepository repository) {
            _repository = repository;

            RuleFor(x => x.EventId).NotEmpty().WithMessage("Event ID is required.").DependentRules(() => {
                RuleFor(x => x.EventId).MustAsync(async (id, cancellation) => {
                    return await _repository.GetEventAsync(id) != null;
                }).WithMessage("Event doesn't exist.");
            });
            RuleFor(x => x.Guests).NotEmpty().WithMessage("Guests are required.");
            RuleFor(x => x.Email).NotEmpty().When(x => x.Mobile == null).WithMessage("Email or Mobile is required.");
            RuleFor(x => x.Mobile).NotEmpty().When(x => x.Email == null).WithMessage("Email or Mobile is required.");      
        }
    }
}