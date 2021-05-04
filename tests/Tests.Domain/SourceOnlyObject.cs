// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Tests.Domain
{
	public class SourceOnlyObject
	{
		[Ignore] [IgnoreDataMember]
		public string NotReadByDefaultSerializer { get; set; }

		[Ignore] [IgnoreDataMember]
		public string NotWrittenByDefaultSerializer { get; set; }
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
				reader.Read();
			while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
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
