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
			var settings = serializer.ContractResolver as ElasticResolver;
			if (settings != null && settings.ConnectionSettings != null)
			{
				var typeName = marker.Resolve(settings.ConnectionSettings);
				writer.WriteValue(typeName);
			}
			else throw new Exception("Could not find connection settings on the json contract resolver");
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
