using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace ExpenseManager.Converters
{
    public class LongDateTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
            writer.WriteRawValue(UnixTimestampFromDateTime((DateTime) value).ToString());

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) =>
            reader.Value == null
                ? Epoch
                : TimeFromUnixTimestamp((long) reader.Value);

        private static DateTime TimeFromUnixTimestamp(long unixTimestamp) => 
            Epoch.AddMilliseconds(unixTimestamp);

        private static long UnixTimestampFromDateTime(DateTime date) => 
            (long) (date - Epoch).TotalMilliseconds;
    }
}