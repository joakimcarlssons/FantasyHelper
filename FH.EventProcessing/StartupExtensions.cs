using FH.EventProcessing.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing
{
    public static class StartupExtensions
    {
        // TODO: Setup ServiceCollection


        public static void SetupQueue(this IApplicationBuilder app, string queueName)
        {
            // Get service
            var publishService = app.ApplicationServices.GetRequiredService<IMessageBusPublisher>();

            // Setup queue in order to receive data
            publishService.SetupQueue(queueName, true, false, false, null, "trigger", "");
        }

        /// <summary>
        /// Sets up a queue for a service and sends out a request for certain events. 
        /// This will be sent out to all existing queues within the exchange in the set up publishig service.
        /// </summary>
        public static void SetupQueueAndTriggerDataLoad(this IApplicationBuilder app, List<DataPublishedDto<DataLoadingRequestDto>> dataloadingRequests)
        {
            // Get service
            var publishService = app.ApplicationServices.GetRequiredService<IMessageBusPublisher>();

            // Go through requests
            foreach (var e in dataloadingRequests)
            {
                // Trigger event
                publishService.PublishData(data: e);

                // Setup queue in order to receive data
                publishService.SetupQueue(e.Data.QueueName, true, false, false, null, "trigger", "");
            }
        }
    }
}
