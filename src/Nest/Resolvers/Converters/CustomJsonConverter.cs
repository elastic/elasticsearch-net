using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace Nest.Resolvers.Converters
{
	public class CustomJsonConverter : JsonConverter
	{
		public override bool CanRead { get { return false; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var custom = value as ICustomJson;
			if (custom == null)
				return;

			var json = custom.GetCustomJson();
			var rawJson = json as RawJson;
			if (rawJson != null)
				writer.WriteRawValue(rawJson.Data);
			else 
				serializer.Serialize(writer, json);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}
}

