using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PercentilesAggregationJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(IPercentilesAggregation);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var properties = o.Properties().ToDictionary(p => p.Name, p => p.Value);
			var percentiles = new PercentilesAggregation();
			ReadMetricProperties(percentiles, properties);
			percentiles.Method = ReadMethodProperty(properties);
			if (properties.TryGetValue("percents", out var percentsToken))
				percentiles.Percents = percentsToken.ToObject<List<double>>();
			return percentiles;
		}

		protected IPercentilesMethod ReadMethodProperty(Dictionary<string, JToken> properties)
		{
			IPercentilesMethod method = null;
			if (properties.TryGetValue("hdr", out var hdrToken))
				method = hdrToken.ToObject<HDRHistogramMethod>();
			else if (properties.TryGetValue("tdigest", out var tdigestToken))
				method = tdigestToken.ToObject<TDigestMethod>();
			return method;
		}

		protected void ReadMetricProperties(IMetricAggregation metric, Dictionary<string, JToken> properties)
		{
			if (properties.TryGetValue("field", out var fieldToken))
				metric.Field = fieldToken.ToString();

			if (properties.TryGetValue("script", out var scriptToken))
			{
				var scriptProps = JObject.FromObject(scriptToken).Properties().ToDictionary(p => p.Name, p => p.Value);
				if (scriptProps.ContainsKey("source") || scriptProps.ContainsKey("inline"))
					metric.Script = scriptToken.ToObject<InlineScript>();
				else if (scriptProps.TryGetValue("id", out var idToken))
					metric.Script = idToken.ToObject<IndexedScript>();
			}

			if (properties.TryGetValue("missing", out var missingToken))
				metric.Missing = double.Parse(missingToken.ToString());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var percentiles = value as IPercentilesAggregation;
			if (percentiles == null) return;

			writer.WriteStartObject();
			WriteMetricProperties(percentiles, writer, serializer);
			if (percentiles.Percents != null)
			{
				writer.WritePropertyName("percents");
				serializer.Serialize(writer, percentiles.Percents);
			}
			WriteMethodProperty(percentiles.Method, writer, serializer);
			writer.WriteEndObject();
		}

		protected void WriteMethodProperty(IPercentilesMethod method, JsonWriter writer, JsonSerializer serializer)
		{
			var tdigest = method as ITDigestMethod;
			if (tdigest != null)
			{
				writer.WritePropertyName("tdigest");
				writer.WriteStartObject();
				if (tdigest.Compression.HasValue)
				{
					writer.WritePropertyName("compression");
					writer.WriteValue(tdigest.Compression);
				}
				writer.WriteEndObject();
				return;
			}

			var hdr = method as IHDRHistogramMethod;
			if (hdr != null)
			{
				writer.WritePropertyName("hdr");
				writer.WriteStartObject();
				if (hdr.NumberOfSignificantValueDigits.HasValue)
				{
					writer.WritePropertyName("number_of_significant_value_digits");
					writer.WriteValue(hdr.NumberOfSignificantValueDigits);
				}
				writer.WriteEndObject();
				return;
			}
		}

		protected void WriteMetricProperties(IMetricAggregation metric, JsonWriter writer, JsonSerializer serializer)
		{
			if (metric.Field != null)
			{
				var settings = serializer.GetConnectionSettings();
				writer.WritePropertyName("field");
				writer.WriteValue(settings.Inferrer.Field(metric.Field));
			}

			if (metric.Script != null)
			{
				writer.WritePropertyName("script");
				serializer.Serialize(writer, metric.Script);
			}

			if (metric.Missing.HasValue)
			{
				writer.WritePropertyName("missing");
				writer.WriteValue(metric.Missing);
			}
		}
	}
}
