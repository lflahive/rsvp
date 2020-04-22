using System;
using Microsoft.Extensions.Configuration;
using Rsvp.Logic;

namespace Rsvp.API
{
    public abstract class BaseFunction
    {
        protected RsvpRepository Repository { get; set; }
        public BaseFunction()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            Repository = new RsvpRepository(
                config.GetConnectionString("RsvpTableStorage"),
                config.GetValue<string>("RsvpTable"));
        }
    }
}