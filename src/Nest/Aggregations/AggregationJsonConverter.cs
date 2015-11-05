using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class AggregationJsonConverter : JsonConverter
	{
		private static Regex _numeric = new Regex(@"^[\d.]+(\.[\d.]+)?$");

		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => this.ReadAggregation(reader, serializer);

		public override bool CanConvert(Type objectType) => objectType == typeof(IAggregation);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private IAggregation ReadAggregation(JsonReader reader, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject)
				return null;
			reader.Read();

			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			var property = reader.Value as string;
			if (_numeric.IsMatch(property))
				return GetPercentilesMetricAggregation(reader, serializer, oldFormat: true);

			switch (property)
			{
				case "values":
					reader.Read();
					reader.Read();
					return GetPercentilesMetricAggregation(reader, serializer);
				case "value":
					return GetValueMetricOrAggregation(reader, serializer);
				case "buckets":
				case "doc_count_error_upper_bound":
					return GetBucketAggregation(reader, serializer);
				case "key":
					return GetKeyedBucketItem(reader, serializer);
				case "from":
				case "to":
					return GetRangeAggregation(reader, serializer);
				case "key_as_string":
					return GetDateHistogramAggregation(reader, serializer);
				case "count":
					return GetStatsAggregation(reader, serializer);
				case "doc_count":
					return GetSingleBucketAggregation(reader, serializer);
				case "bounds":
					return GetGeoBoundsMetricAggregation(reader, serializer);
				case "hits":
					return GetHitsAggregation(reader, serializer);
				default:
					return null;

			}
		}

		private IAggregation GetHitsAggregation(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var o = JObject.Load(reader);
			if (o == null)
				return null;

			var total = o["total"].ToObject<long>();
			var maxScore = o["max_score"].ToObject<double?>();
			var hits = o["hits"].Children().OfType<JObject>().Select(s => s);
			reader.Read();
			return new TopHitsMetric(hits, serializer) { Total = total, MaxScore = maxScore };
		}

		private IAggregation GetGeoBoundsMetricAggregation(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var o = JObject.Load(reader);
			if (o == null)
				return null;
			var geoBoundsMetric = new GeoBoundsMetric();
			JToken topLeftToken;
			o.TryGetValue("top_left", out topLeftToken);
			if (topLeftToken != null)
			{
				var topLeft = topLeftToken.ToObject<LatLon>();
				if (topLeft != null)
					geoBoundsMetric.Bounds.TopLeft = topLeft;
			}
			JToken bottomRightToken;
			o.TryGetValue("bottom_right", out bottomRightToken);
			if (bottomRightToken != null)
			{
				var bottomRight = bottomRightToken.ToObject<LatLon>();
				if (bottomRight != null)
					geoBoundsMetric.Bounds.BottomRight = bottomRight;
			}
			reader.Read();
			return geoBoundsMetric;
		}

		private IAggregation GetPercentilesMetricAggregation(JsonReader reader, JsonSerializer serializer, bool oldFormat = false)
		{
			var metric = new PercentilesMetric();
			var percentileItems = new List<PercentileItem>();
			if (reader.TokenType == JsonToken.StartObject)
				reader.Read();
			while (reader.TokenType != JsonToken.EndObject)
			{
				if ((reader.Value as string).Contains("_as_string"))
				{
					reader.Read();
					reader.Read();
				}
				if (reader.TokenType != JsonToken.EndObject)
				{
					var percentile = double.Parse(reader.Value as string, CultureInfo.InvariantCulture);
					reader.Read();
					var value = reader.Value as double?;
					percentileItems.Add(new PercentileItem()
					{
						Percentile = percentile,
						Value = value.GetValueOrDefault(0)
					});
					reader.Read();
				}
			}
			metric.Items = percentileItems;
			if (!oldFormat) reader.Read();
			return metric;
		}

		private IAggregation GetSingleBucketAggregation(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var docCount = (reader.Value as long?).GetValueOrDefault(0);
			var bucket = new SingleBucket() { DocCount = docCount };
			reader.Read();
			if (reader.TokenType == JsonToken.PropertyName
				&& ((string)reader.Value) == "buckets"
				)
			{
				var b = this.GetBucketAggregation(reader, serializer) as Bucket;
				return new DocCountBucket()
				{
					DocCount = docCount,
					Items = b.Items

				};
			}

			bucket.Aggregations = this.GetNestedAggregations(reader, serializer);

			return bucket;
		}

		private IAggregation GetStatsAggregation(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var count = (reader.Value as long?).GetValueOrDefault(0);
			reader.Read(); reader.Read();
			var min = (reader.Value as double?);
			reader.Read(); reader.Read();
			var max = (reader.Value as double?);
			reader.Read(); reader.Read();
			var average = (reader.Value as double?);
			reader.Read(); reader.Read();
			var sum = (reader.Value as double?);

			var statsMetric = new StatsMetric()
			{
				Average = average,
				Count = count,
				Max = max,
				Min = min,
				Sum = sum
			};

			reader.Read();

			if (reader.TokenType == JsonToken.EndObject)
				return statsMetric;

			while (reader.TokenType != JsonToken.EndObject && (reader.Value as string).Contains("_as_string"))
			{
				reader.Read();
				reader.Read();
			}

			if (reader.TokenType == JsonToken.EndObject)
				return statsMetric;

			return GetExtendedStatsAggregation(statsMetric, reader);
		}

		private IAggregation GetExtendedStatsAggregation(StatsMetric statsMetric, JsonReader reader)
		{
			var extendedStatsMetric = new ExtendedStatsMetric()
			{
				Average = statsMetric.Average,
				Count = statsMetric.Count,
				Max = statsMetric.Max,
				Min = statsMetric.Min,
				Sum = statsMetric.Sum
			};

			reader.Read();
			extendedStatsMetric.SumOfSquares = (reader.Value as double?);
			reader.Read(); 
			reader.Read();
			extendedStatsMetric.Variance = (reader.Value as double?);
			reader.Read(); reader.Read();
			extendedStatsMetric.StdDeviation = (reader.Value as double?);
			reader.Read();

			if (reader.TokenType != JsonToken.EndObject)
			{
				var bounds = new StandardDeviationBounds();
				reader.Read();
				reader.Read();
				if ((reader.Value as string) == "upper")
				{
					reader.Read();
					bounds.Upper = reader.Value as double?;
				}
				reader.Read();
				if ((reader.Value as string) == "lower")
				{
					reader.Read();
					bounds.Lower = reader.Value as double?;
				}
				extendedStatsMetric.StdDeviationBounds = bounds;
				reader.Read();
				reader.Read();
			}
			while (reader.TokenType != JsonToken.EndObject && (reader.Value as string).Contains("_as_string"))
			{
				// std_deviation_bounds is an object, so we need to skip its properties
				if ((reader.Value as string).Equals("std_deviation_bounds_as_string"))
				{
					reader.Read();
					reader.Read();
					reader.Read();
					reader.Read();
				}
				reader.Read();
				reader.Read();
			}
			return extendedStatsMetric;
		}

		private IAggregation GetDateHistogramAggregation(JsonReader reader, JsonSerializer serializer)
		{
			var keyAsString = reader.ReadAsString();
			reader.Read(); reader.Read();
			var key = (reader.Value as long?).GetValueOrDefault(0);
			reader.Read(); reader.Read();
			var docCount = (reader.Value as long?).GetValueOrDefault(0);
			reader.Read();

			var dateHistogram = new HistogramItem() { Key = key, KeyAsString = keyAsString, DocCount = docCount };
			dateHistogram.Aggregations = this.GetNestedAggregations(reader, serializer);
			return dateHistogram;

		}

		private IAggregation GetKeyedBucketItem(JsonReader reader, JsonSerializer serializer)
		{
			var key = reader.ReadAsString();
			reader.Read();
			var property = reader.Value as string;
			if (property == "from" || property == "to")
				return GetRangeAggregation(reader, serializer, key);

			var keyItem = new KeyedBucket();
			keyItem.Key = key;

			if (property == "key_as_string")
			{
				keyItem.KeyAsString = reader.ReadAsString();
				reader.Read();
			}

			reader.Read(); //doc_count;
			var docCount = reader.Value as long?;
			keyItem.DocCount = docCount.GetValueOrDefault(0);
			reader.Read();

			var nextProperty = reader.Value as string;
			if (nextProperty == "score")
			{
				return GetSignificantTermItem(reader, serializer, keyItem);
			}


			keyItem.Aggregations = this.GetNestedAggregations(reader, serializer);
			return keyItem;

		}

		private IAggregation GetSignificantTermItem(JsonReader reader, JsonSerializer serializer, KeyedBucket keyItem)
		{
			reader.Read();
			var score = reader.Value as double?;
			reader.Read();
			reader.Read();
			var bgCount = reader.Value as long?;
			var significantTermItem = new SignificantTermItem()
			{
				Key = keyItem.Key,
				DocCount = keyItem.DocCount,
				BgCount = bgCount.GetValueOrDefault(0),
				Score = score.GetValueOrDefault(0)
			};
			reader.Read();
			significantTermItem.Aggregations = this.GetNestedAggregations(reader, serializer);
			return significantTermItem;
		}

		private IDictionary<string, IAggregation> GetNestedAggregations(JsonReader reader, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			var nestedAggs = new Dictionary<string, IAggregation>();
			var currentDepth = reader.Depth;
			do
			{
				var fieldName = reader.Value as string;
				reader.Read();
				var agg = this.ReadAggregation(reader, serializer);
				nestedAggs.Add(fieldName, agg);
				reader.Read();
				if (reader.Depth == currentDepth && reader.TokenType == JsonToken.EndObject || reader.Depth < currentDepth)
					break;
			} while (true);
			return nestedAggs;
		}

		private IAggregation GetBucketAggregation(JsonReader reader, JsonSerializer serializer)
		{
			var bucket = new Bucket();
			var property = reader.Value as string;
			if (property == "doc_count_error_upper_bound")
			{
				reader.Read();
				bucket.DocCountErrorUpperBound = reader.Value as long?;
				reader.Read();
			}
			property = reader.Value as string;
			if (property == "sum_other_doc_count")
			{
				reader.Read();
				bucket.SumOtherDocCount = reader.Value as long?;
				reader.Read();
			}
			var aggregations = new List<IAggregation>();
			reader.Read();

			if (reader.TokenType == JsonToken.StartObject)
			{
				reader.Read();
				var temp = new Dictionary<string, IAggregation>();
				do
				{
					var name = reader.Value.ToString();
					reader.Read();
					var innerAgg = this.ReadAggregation(reader, serializer);
					temp.Add(name, innerAgg);
					reader.Read();
				} while (reader.TokenType != JsonToken.EndObject);

				var agg = new AggregationsHelper(temp);
				reader.Read();
				return new FiltersBucket(agg);
			}

			if (reader.TokenType != JsonToken.StartArray)
				return null;
			reader.Read(); //move from start array to start object
			if (reader.TokenType == JsonToken.EndArray)
			{
				reader.Read();
				bucket.Items = Enumerable.Empty<IAggregation>();
				return bucket;
			}
			do
			{
				var agg = this.ReadAggregation(reader, serializer);
				aggregations.Add(agg);
				reader.Read();
			} while (reader.TokenType != JsonToken.EndArray);
			bucket.Items = aggregations;
			reader.Read();
			return bucket;
		}

		private IAggregation GetValueMetricOrAggregation(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var valueMetric = new ValueMetric()
			{
				Value = (reader.Value as double?)
			};
			if (valueMetric.Value == null && reader.ValueType == typeof(long))
				valueMetric.Value = reader.Value as long?;

			if (valueMetric.Value != null)
			{
				reader.Read();
				if (reader.TokenType != JsonToken.EndObject)
				{
					if (reader.TokenType == JsonToken.PropertyName && (reader.Value as string) == "keys")
					{
						var keyedValueMetric = new KeyedValueMetric
						{
							Value = valueMetric.Value
						};
						var keys = new List<string>();
						reader.Read();
						reader.Read();
						while (reader.TokenType != JsonToken.EndArray)
						{
							keys.Add(reader.Value.ToString());
							reader.Read();
						}
						reader.Read();
						keyedValueMetric.Keys = keys;
						return keyedValueMetric;
					}
					else
					{
						reader.Read();
						reader.Read();
					}
				}
				return valueMetric;
			}

			var scriptedMetric = serializer.Deserialize(reader);

			if (scriptedMetric != null)
				return new ScriptedValueMetric { _Value = scriptedMetric };

			reader.Read();
			return valueMetric;
		}

		public IAggregation GetRangeAggregation(JsonReader reader, JsonSerializer serializer, string key = null)
		{
			string fromAsString = null, toAsString = null;
			long? docCount = null;
			double? toDouble = null, fromDouble = null;

			var readExpectedProperty = true;
			while (readExpectedProperty)
			{
				switch (reader.Value as string)
				{
					case "from":
						reader.Read();
						if (reader.ValueType == typeof(double))
							fromDouble = (double)reader.Value;
						reader.Read();
						break;
					case "to":
						reader.Read();
						if (reader.ValueType == typeof(double))
							toDouble = (double)reader.Value;
						reader.Read();
						break;
					case "key":
						key = reader.ReadAsString();
						reader.Read();
						break;
					case "from_as_string":
						fromAsString = reader.ReadAsString();
						reader.Read();
						break;
					case "to_as_string":
						toAsString = reader.ReadAsString();
						reader.Read();
						break;
					case "doc_count":
						reader.Read();
						docCount = (reader.Value as long?).GetValueOrDefault(0);
						reader.Read();
						break;
					default:
						readExpectedProperty = false;
						break;
				}
			}
			var bucket = new RangeItem
			{
				Key = key,
				From = fromDouble,
				To = toDouble,
				DocCount = docCount.GetValueOrDefault(),
				FromAsString = fromAsString,
				ToAsString = toAsString
			};

			bucket.Aggregations = this.GetNestedAggregations(reader, serializer);
			return bucket;

		}
	}
}