using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class SourceValueWriteConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Write(writer, value, serializer);
		}

		public static void Write(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var nativeType = value.GetType().Assembly() == typeof(SourceValueWriteConverter).Assembly();

			var settings = serializer.GetConnectionSettings();
			var s = nativeType ? settings.RequestResponseSerializer : settings.SourceSerializer;
			var f = writer.Formatting == Formatting.Indented ? SerializationFormatting.Indented : SerializationFormatting.None;
			var v = s.SerializeToString(value, f);
			writer.WriteRawValue(v);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
