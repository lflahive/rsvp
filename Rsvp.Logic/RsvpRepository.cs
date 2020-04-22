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
    }
}