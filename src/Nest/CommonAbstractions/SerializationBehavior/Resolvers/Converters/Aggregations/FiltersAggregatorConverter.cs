using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters.Aggregations
{
	public class FiltersAggregatorConverter : JsonConverter
	{
		public override bool CanRead { get { return true; } }
		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IFiltersAggregator;
			if (f == null || f.Filters == null) return;

			writer.WriteStartObject();
			writer.WritePropertyName("filters");

			if (!f.Filters.Any(filter => filter == null))
			{
				writer.WriteStartObject();
				foreach (var filter in f.Filters)
				{
					// TODO
					// writer.WritePropertyName(filter.FilterName);
					serializer.Serialize(writer, filter);
				}
				writer.WriteEndObject();
			}
			else
			{
				serializer.Serialize(writer, f.Filters);
			}

			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			//todo implement
			if (reader.TokenType != JsonToken.StartObject) return null;
			reader.Read();
			if (reader.TokenType != JsonToken.PropertyName) return null;
			var prop = reader.Value;
			if ((string)reader.Value != "filter") return null;
			reader.Read();
			var agg = new FilterAggregator(null);
			serializer.Populate(reader, agg);
			return agg;
		}
	}

}
