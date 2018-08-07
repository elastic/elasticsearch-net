using System;
using Nest;
using Newtonsoft.Json;

namespace Tests.Domain
{
	public class SourceOnlyObject
	{
		[Ignore, JsonIgnore]
		public string NotWrittenByDefaultSerializer { get; set; }
		[Ignore, JsonIgnore]
		public string NotReadByDefaultSerializer { get; set; }
	}

	public class SourceOnlyUsingBuiltInConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("notWrittenByDefaultSerializer");
			writer.WriteValue("written");
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			//ignore json as provided
			var depth = reader.Depth;
			do
			{
				reader.Read();
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
			var p = new SourceOnlyObject
			{
				NotWrittenByDefaultSerializer = "written",
				NotReadByDefaultSerializer = "read"
			};
			return p;
		}
		public override bool CanConvert(Type objectType) => objectType == typeof(SourceOnlyObject);
	}
}
