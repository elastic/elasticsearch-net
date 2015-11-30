using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	internal class FeaturesJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var marker = value as Features;
			if (marker == null)
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();
			var s = ((IUrlParameter)marker).GetString(settings);
			writer.WriteValue(s);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				string indices = reader.Value.ToString();
				return (Features)indices;
			}
			return null;
		}

	}
}
