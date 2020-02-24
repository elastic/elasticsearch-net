using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class LazyDocumentJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var d = (LazyDocument)value;
			if (d?.Token == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteToken(d.Token.CreateReader());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var requestResponseSerializer = serializer.GetConnectionSettings().RequestResponseSerializer;
			var token = reader.ReadTokenWithDateParseHandlingNone();
			return new LazyDocument(token, sourceSerializer, requestResponseSerializer);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
