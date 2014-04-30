using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class ReadAsTypeConverter<T> : JsonConverter
		where T : class, new()
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
			var t = new T();
			var customReader = t as ICustomJsonReader<T>;
			if (customReader != null)
				return customReader.FromJson(reader, objectType, existingValue, serializer);

			serializer.Populate(reader, t);
			return t;
		}
	}
	
}
