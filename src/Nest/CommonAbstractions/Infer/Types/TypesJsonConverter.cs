using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	internal class TypesJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => typeof(Types) == objectType;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as Types;
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
				many => writer.WriteValue(((IUrlParameter)marker).GetString(contract.ConnectionSettings))
			);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string types = reader.Value.ToString();
				return (Types)types;
			}
			return null;
		}

	}
}
