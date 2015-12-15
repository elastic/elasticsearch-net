using System;
using Newtonsoft.Json;

namespace Nest
{
	//TODO Only used in IP mapping, these smells like a wrong mapping
	//If not rename to IpMappingIndexJsonConverter ?
	internal class YesNoBoolJsonConverter : JsonConverter
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
			var v = reader.Value.ToString();
			return reader.Value != null && (v == "yes" || v == "True");
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(bool) || objectType == typeof(bool?);
		}
	}
}
