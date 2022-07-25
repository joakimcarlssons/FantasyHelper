using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing.Dtos
{
    public class DataPublishedDto<T>
    {
        public string Event { get; set; }
        public EventSource Source { get; set; }
        public T Data { get; set; }
    }
}
