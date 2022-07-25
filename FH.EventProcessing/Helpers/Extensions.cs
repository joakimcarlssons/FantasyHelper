using FH.EventProcessing.Dtos;

namespace FH.EventProcessing.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Converts an <see cref="EventType"/> to a event string
        /// </summary>
        public static string ConvertEventTypeToEventString(this EventType eventType) => string.Concat(eventType.ToString().Select(c => char.IsUpper(c) ? "_" + c.ToString() : c.ToString())).TrimStart('_');

        public static DataPublishedDto<TData> ToPublishDataDto<TData>(this TData data, EventType eventType, EventSource eventSource)
            => new DataPublishedDto<TData>
            {
                Event = eventType.ConvertEventTypeToEventString(),
                Source = eventSource,
                Data = data
            };
    }
}
