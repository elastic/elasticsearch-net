using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class PercentileRanksAggregationJsonConverter : PercentilesAggregationJsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(IPercentileRanksAggregation);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var properties = o.Properties().ToDictionary(p => p.Name, p => p.Value);
			var percentileRanks = new PercentileRanksAggregation();
			ReadMetricProperties(percentileRanks, properties);
			percentileRanks.Method = ReadMethodProperty(properties);
			if (properties.ContainsKey("values"))
				percentileRanks.Values = properties["values"].ToObject<List<double>>();
			return percentileRanks;;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var percentileRanks = value as IPercentileRanksAggregation;
			if (percentileRanks == null) return;
			writer.WriteStartObject();
			WriteMetricProperties(percentileRanks, writer, serializer);
			if (percentileRanks.Values != null)
			{
				writer.WritePropertyName("values");
				serializer.Serialize(writer, percentileRanks.Values);
			}
			WriteMethodProperty(percentileRanks.Method, writer, serializer);
			writer.WriteEndObject();
		}
	}
}
