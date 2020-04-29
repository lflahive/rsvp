using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace Rsvp.Logic
{
    public class RsvpRepository
    {
        private CloudTable _table { get; set; }
        private IMapper _mapper { get; set; }
        public RsvpRepository(string connectionString, string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            _table = tableClient.GetTableReference(tableName);
            _mapper = RsvpMapper.GetMapper();
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            var eventEntity = _mapper.Map<Event, EventEntity>(newEvent);
            var insertOperation = TableOperation.Insert(eventEntity);
            await _table.ExecuteAsync(insertOperation);
            return newEvent;
        }

        public async Task<Event> GetEventAsync(Guid id)
        {
            var operation = TableOperation.Retrieve<EventEntity>("Event", id.ToString());
            var currentEvent = await _table.ExecuteAsync(operation);
            return _mapper.Map<EventEntity, Event>((EventEntity)currentEvent.Result);
        }

        public async Task<Invitation> CreateInvitationAsync(Invitation invitation)
        {
            var invitationEntity = _mapper.Map<Invitation, InvitationEntity>(invitation);
            var insertOperation = TableOperation.Insert(invitationEntity);
            await _table.ExecuteAsync(insertOperation);
            return invitation;
        }

        public async Task<Invitation> UpdateInvitationAsync(Invitation invitation)
        {
            var invitationEntity = _mapper.Map<Invitation, InvitationEntity>(invitation);
            var mergeOperation = TableOperation.Merge(invitationEntity);
            await _table.ExecuteAsync(mergeOperation);
            return invitation;
        }

        public async Task<Invitation> GetInvitationByIdAsync(Guid eventId, Guid invitationId)
        {
            var retrieveOperation = TableOperation.Retrieve<InvitationEntity>($"Invitation_{eventId}", invitationId.ToString());
            var result = await _table.ExecuteAsync(retrieveOperation);
            return _mapper.Map<InvitationEntity, Invitation>((InvitationEntity)result.Result);
        }

        public List<Invitation> GetInvitationsByEventId(Guid id)
        {
            var query = new TableQuery<InvitationEntity>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, $"Invitation_{id}")
            );

            var invitationEntities = _table.ExecuteQuery(query).ToList();
            
            return _mapper.Map<List<InvitationEntity>, List<Invitation>>(invitationEntities);
        }
    }
}