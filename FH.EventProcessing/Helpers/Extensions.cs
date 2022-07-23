using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FH.EventProcessing.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Converts an <see cref="EventType"/> to a event string
        /// </summary>
        public static string ConvertEventTypeToEventString(this EventType eventType)
        {
            var convertedString = string.Concat(eventType.ToString().Select(c => char.IsUpper(c) ? "_" + c.ToString() : c.ToString())).TrimStart('_');
            //if (convertedString[0] == '_') convertedString = convertedString.Remove(0, 1);
            return convertedString;
        }
    }
}
