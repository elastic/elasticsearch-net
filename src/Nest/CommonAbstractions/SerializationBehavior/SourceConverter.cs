using System;
using Elasticsearch.Net;
using Newtonsoft.Json;
using static Elasticsearch.Net.SerializationFormatting;

namespace Nest
{
	internal class SourceConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public virtual SerializationFormatting? ForceFormatting { get; } = null;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var token = reader.ReadTokenWithDateParseHandlingNone();
			using (var ms = token.ToStream(serializer.GetConnectionSettings().MemoryStreamFactory))
				return serializer.GetConnectionSettings().SourceSerializer.Deserialize(objectType, ms);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var f = ForceFormatting ?? (writer.Formatting == Formatting.Indented ? Indented : None);
			var v = sourceSerializer.SerializeToString(value, f);
			writer.WriteRawValue(v);
		}
	}

	internal class CollapsedSourceConverter : SourceConverter
	{
		public override SerializationFormatting? ForceFormatting { get; } = None;
	}
}
