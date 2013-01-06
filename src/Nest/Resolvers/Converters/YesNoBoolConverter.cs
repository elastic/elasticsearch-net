using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class YesNoBoolConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteValue("no");
			else 
				writer.WriteValue(((bool)value) ? "yes" : "no");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.Value != null && reader.Value.ToString() == "yes";
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(bool) || objectType == typeof(bool?);
		}
	}
}
