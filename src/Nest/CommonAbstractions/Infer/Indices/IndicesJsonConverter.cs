using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	internal class IndicesJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Types) == objectType;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as Indices;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null || contract.ConnectionSettings == null)
				throw new Exception("If you use a custom contract resolver be sure to subclass from ElasticResolver");
			marker.Match(
				all=> writer.WriteNull(),
				many =>
				{
					writer.WriteStartArray();
					foreach(var m in many.Indices.Cast<IUrlParameter>())
						writer.WriteValue(m.GetString(contract.ConnectionSettings));
					writer.WriteEndArray();
				}
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{

			if (reader.TokenType != JsonToken.StartArray) return null;
			var indices = new List<IndexName> { };
			while (reader.TokenType != JsonToken.EndArray)
			{
				var index = reader.ReadAsString();
				if (reader.TokenType == JsonToken.String)
					indices.Add(index);
			}
			return new Indices(indices);
		}

	}
}
