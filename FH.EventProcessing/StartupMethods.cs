using FH.EventProcessing.Config;
using FH.EventProcessing.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FH.EventProcessing
{
    public class InitialDataLoader : IConfigureOptions<RabbitMQOptions>
    {
        /// <summary>
        /// Send events to a given exchange asking for a given event to be triggered
        /// </summary>
        /// <param name="app"></param>
        /// <param name="events"></param>
        //public static void ForceDataLoads(this IApplicationBuilder app, List<DataPublishedDto<EventType>> events)
        //{
        //    var publishService = app.ApplicationServices.GetRequiredService<IMessageBusPublisher>();
        //    foreach (var e in events)
        //    {
        //        publishService.PublishData(e);
        //    }
        //}

        private readonly IServiceScopeFactory _scopeFactory;
        public InitialDataLoader(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        public void Configure(RabbitMQOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
