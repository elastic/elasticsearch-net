using System;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Elasticsearch.Net.SerializationFormatting;

namespace Nest
{
	internal class LazyDocumentJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var d = (LazyDocument)value;
			if (d?.Token == null) return;

			writer.WriteToken(d.Token.CreateReader());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var token = JToken.ReadFrom(reader);
			return new LazyDocument
			{
				Token = token,
				SourceSerializer = sourceSerializer
			};
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
