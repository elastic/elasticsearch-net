using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class ForceStringReader : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return false; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Date)
			{
				var dateTime = (reader.Value as DateTime?);
				if (!dateTime.HasValue)
					return null;
				return dateTime.Value.ToString("yyyy-MM-dd'T'HH:mm:ss.fff", CultureInfo.InvariantCulture);
			}
			return reader.Value as string;
		}
	}

	public class ReadAsTypeConverter<T> : JsonConverter
		where T : class, new()
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var t = new T();
			serializer.Populate(reader, t);
			return t;
		}
	}
	
}
