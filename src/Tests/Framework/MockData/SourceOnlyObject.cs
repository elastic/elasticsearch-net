using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tests.Framework.MockData
{
	public class SourceOnlyObject
	{
		[JsonIgnore]
		public string NotWrittenByDefaultSerializer { get; set; }
		[JsonIgnore]
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
