using System;
using System.IO;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class SourceConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public virtual SerializationFormatting Formatting { get; } = SerializationFormatting.Indented;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var v = sourceSerializer.SerializeToString(value, Formatting);
			writer.WriteRawValue(v);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			using (var ms = new MemoryStream(reader.ReadAsBytes()))
				return serializer.GetConnectionSettings().SourceSerializer.Deserialize(objectType, ms);
		}
	}

	internal class CollapsedSourceConverter : SourceConverter
	{
		public override SerializationFormatting Formatting { get; } = SerializationFormatting.None;
	}

}
