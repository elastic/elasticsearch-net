using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Nest.Resolvers.Converters
{
	public class TypeNameMarkerConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(TypeNameMarker) == objectType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as TypeNameMarker;
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
				return (TypeNameMarker) typeName;
			}
			return null;
		}

	}
}
