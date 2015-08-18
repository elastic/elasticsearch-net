using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	internal class TypeNameJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(TypeName) == objectType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as TypeName;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract != null && contract.ConnectionSettings != null)
			{
				var typeName = contract.Infer.TypeName(marker);
				writer.WriteValue(typeName);
			}
			else throw new Exception("If you use a custom contract resolver be sure to subclass from ElasticResolver");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string typeName = reader.Value.ToString();
				return (TypeName) typeName;
			}
			return null;
		}

	}
}
