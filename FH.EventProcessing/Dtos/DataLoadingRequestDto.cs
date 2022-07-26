using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing.Dtos
{
    /// <summary>
    /// Used on startup for services sending requests for data
    /// </summary>
    public record DataLoadingRequestDto(EventType EventType, string QueueName);
}
