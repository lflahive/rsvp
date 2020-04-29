using System;
using System.Collections.Generic;
using AutoMapper;
using Newtonsoft.Json;

namespace Rsvp.Logic
{
    public static class RsvpMapper
    {
        public static IMapper GetMapper()
        {
            var mapperConfig = new MapperConfiguration(config => {
                config.CreateMap<List<Guest>, string>()
                    .ConvertUsing(x => JsonConvert.SerializeObject(x));
                config.CreateMap<string, List<Guest>>()
                    .ConvertUsing(x => JsonConvert.DeserializeObject<List<Guest>>(x));
                config.CreateMap<Event, EventEntity>()
                    .BeforeMap((s, d) => d.PartitionKey = "Event")
                    .ForMember(d => d.RowKey, o => o.MapFrom(s => s.Id));
                config.CreateMap<EventEntity, Event>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.RowKey));
                config.CreateMap<Invitation, InvitationEntity>()
                    .BeforeMap((s, d) => d.PartitionKey = $"Invitation_{s.EventId}")
                    .ForMember(d => d.RowKey, o => o.MapFrom(s => s.Id));
                config.CreateMap<InvitationEntity, Invitation>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.RowKey));
            });

            return mapperConfig.CreateMapper();
        }
    }
}