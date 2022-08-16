using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing
{
    public record Queue(EventType eventType, string queueName);

    public static class Queues
    {
        public static List<Queue> ActiveQueues { get; set; } = new List<Queue>();

        public static void AddToQueue(Queue queue) => ActiveQueues.Add(queue);
    }
}
