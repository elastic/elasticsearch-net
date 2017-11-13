using System;
using System.IO;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
			var v = sourceSerializer.SerializeToBytes(value, Formatting);
			using(var ms = new MemoryStream(v))
			using(var streamReader = new StreamReader(ms))
			using(var reader = new JsonTextReader(streamReader))
				writer.WriteToken(reader);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var token = JToken.ReadFrom(reader);
			using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(token.ToString())))
				return serializer.GetConnectionSettings().SourceSerializer.Deserialize(objectType, ms);
		}
	}

	internal class CollapsedSourceConverter : SourceConverter
	{
		public override SerializationFormatting Formatting { get; } = SerializationFormatting.None;
	}

}
