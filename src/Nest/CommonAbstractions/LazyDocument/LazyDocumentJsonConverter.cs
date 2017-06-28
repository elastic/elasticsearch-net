using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class LazyDocumentJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var d = (LazyDocument)value;
			if (d?._Value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteToken(d._Value.CreateReader());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var document = serializer.Deserialize(reader) as JToken;
			return new LazyDocument
			{
				_Value = document,
				_Serializer = serializer
			};
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
