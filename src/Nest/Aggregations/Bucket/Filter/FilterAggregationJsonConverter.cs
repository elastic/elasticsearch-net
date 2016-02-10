using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FilterAggregationJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var f = value as IFilterAggregation;
			if (f == null || f.Filter == null)
			{
				writer.WriteStartObject();
				writer.WriteEndObject();
				return;
			};

			serializer.Serialize(writer, f.Filter);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			var container = new QueryContainer();
			serializer.Populate(reader, container);
			var agg = new FilterAggregation();
			agg.Filter = container;
			return agg;
		}
	}

}
	