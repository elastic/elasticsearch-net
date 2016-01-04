using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class LazyDocumentJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var d = value as LazyDocument;
			if (d == null || d._Value == null)
				return;
			writer.WriteToken(d._Value.CreateReader());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var document = serializer.Deserialize(reader) as JToken;
			return new LazyDocument { _Value = document };
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}