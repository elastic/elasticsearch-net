using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest.Resolvers.Converters
{
    public class UnixDateTimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime EpochUtc = new DateTime(1970, 1, 1);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            long val;
            if (value is DateTime)
            {
                val = (long) ((DateTime) value - EpochUtc).TotalMilliseconds;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }

            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                                        JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception("Wrong Token Type");
            }

            var time = (long) reader.Value;
            return EpochUtc.AddMilliseconds(time);
        }
    }
}