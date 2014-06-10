using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace Nest.Resolvers.Converters
{
	public class IndexNameMarkerConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(IndexNameMarker) == objectType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as IndexNameMarker;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract != null && contract.ConnectionSettings != null)
			{
				var indexName = contract.Infer.IndexName(marker);
				writer.WriteValue(indexName);
			}
			else throw new Exception("If you use a custom contract resolver be sure to subclass from ElasticResolver");
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string typeName = reader.Value.ToString();
				return (IndexNameMarker)typeName;
			}
			return null;
		}

	}
}
