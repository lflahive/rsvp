using AutoMapper;

namespace Rsvp.Logic
{
    public static class RsvpMapper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<Event, EventEntity>()
                    .BeforeMap((s, d) => d.PartitionKey = "Event")
                    .ForMember(d => d.RowKey, o => o.MapFrom(s => s.Id));
                config.CreateMap<EventEntity, Event>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.RowKey));
            });

            return mapperConfig.CreateMapper();
        }
    }
}