using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class AggregationResultJsonConverter<TReadAs> : ReadAsTypeJsonConverter<TReadAs>
		where TReadAs : class
	{ }

	internal class AggregationResultJsonConverter : JsonConverter
	{
		private static Regex _numeric = new Regex(@"^[\d.]+(\.[\d.]+)?$");

		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => this.ReadAggregation(reader, serializer);

		public override bool CanConvert(Type objectType) => objectType == typeof(IAggregationResult);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		private IAggregationResult ReadAggregation(JsonReader reader, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject)
				return null;
			reader.Read();

			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			IAggregationResult result = null;

			var property = reader.Value as string;
			if (_numeric.IsMatch(property))
				result = GetPercentilesMetric(reader, serializer, oldFormat: true);

			var meta = (property == "meta") ? GetMetadata(reader) : null;

			property = reader.Value as string;

			switch (property)
			{
				case "values":
					reader.Read();
					reader.Read();
					result = GetPercentilesMetric(reader, serializer);
					break;
				case "value":
					result = GetValueMetric(reader, serializer);
					break;
				case "buckets":
				case "doc_count_error_upper_bound":
					result = GetBucket(reader, serializer);
					break;;
				case "count":
					result = GetStatsMetric(reader, serializer);
					break;
				case "doc_count":
					result = GetDocCountBucket(reader, serializer);
					break;
				case "bounds":
					result = GetGeoBoundsMetric(reader, serializer);
					break;
				case "hits":
					result = GetTopHitsMetric(reader, serializer);
					break;
				default:
					return null;
			}
			result.Meta = meta;
			return result;
		}

		private IBucketItem ReadBucketItem(JsonReader reader, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject)
				return null;
			reader.Read();

			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			IBucketItem item = null;

			var property = reader.Value as string;

			switch (property)
			{
				case "key":
					item = GetKeyedBucketItem(reader, serializer);
					break;
				case "from":
				case "to":
					item = GetRangeItem(reader, serializer);
					break;
				case "key_as_string":
					item = GetDateHistrogramItem(reader, serializer);
					break;
				case "doc_count":
					item = GetFiltersBucketItem(reader, serializer);
					break;
				default:
					return null;
			}
			return item;
		}

		private Dictionary<string, object> GetMetadata(JsonReader reader)
		{
			var meta = new Dictionary<string, object>();
			reader.Read();
			reader.Read();
			while (reader.TokenType != JsonToken.EndObject)
			{
				var key = reader.Value as string;
				reader.Read();
				var value = reader.Value;
				meta.Add(key, value);
				reader.Read();
			}
			reader.Read();
			return meta;
		}

		private IAggregationResult GetTopHitsMetric(JsonReader reader, JsonSerializer serializer)
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

		private IAggregationResult GetGeoBoundsMetric(JsonReader reader, JsonSerializer serializer)
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

		private IAggregationResult GetPercentilesMetric(JsonReader reader, JsonSerializer serializer, bool oldFormat = false)
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

		private IAggregationResult GetDocCountBucket(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var docCount = (reader.Value as long?).GetValueOrDefault(0);
			var bucket = new DocCountBucket { DocCount = docCount };
			reader.Read();
			if (reader.TokenType == JsonToken.PropertyName
				&& ((string)reader.Value) == "buckets"
				)
			{
				var b = this.GetBucket(reader, serializer) as BucketDto;
				return new BucketDto
				{
					DocCount = docCount,
					Items = b.Items
				};
			}

			bucket.Aggregations = this.GetNestedAggregations(reader, serializer);

			return bucket;
		}

		private IAggregationResult GetStatsMetric(JsonReader reader, JsonSerializer serializer)
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

		private IAggregationResult GetExtendedStatsAggregation(StatsMetric statsMetric, JsonReader reader)
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

		private IDictionary<string, IAggregationResult> GetNestedAggregations(JsonReader reader, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.PropertyName)
				return null;

			var nestedAggs = new Dictionary<string, IAggregationResult>();
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

		private IAggregationResult GetBucket(JsonReader reader, JsonSerializer serializer)
		{
			var bucket = new BucketDto();
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
			var items = new List<IBucketItem>();
			reader.Read();

			if (reader.TokenType == JsonToken.StartObject)
			{
				reader.Read();
				var aggs = new Dictionary<string, IAggregationResult>();
				do
				{
					var name = reader.Value.ToString();
					reader.Read();
					var innerAgg = this.ReadAggregation(reader, serializer);
					aggs.Add(name, innerAgg);
					reader.Read();
				} while (reader.TokenType != JsonToken.EndObject);

				reader.Read();
				return new FiltersBucket(aggs);
			}

			if (reader.TokenType != JsonToken.StartArray)
				return null;
			reader.Read(); //move from start array to start object
			if (reader.TokenType == JsonToken.EndArray)
			{
				reader.Read();
				bucket.Items = Enumerable.Empty<IBucketItem>();
				return bucket;
			}
			do
			{
				var item = this.ReadBucketItem(reader, serializer);
				items.Add(item);
				reader.Read();
			} while (reader.TokenType != JsonToken.EndArray);
			bucket.Items = items;
			reader.Read();
			return bucket;
		}

		private IAggregationResult GetValueMetric(JsonReader reader, JsonSerializer serializer)
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

		public IBucketItem GetRangeItem(JsonReader reader, JsonSerializer serializer, string key = null)
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

		private IBucketItem GetDateHistrogramItem(JsonReader reader, JsonSerializer serializer)
		{
			var keyAsString = reader.ReadAsString();
			reader.Read(); reader.Read();
			var key = (reader.Value as long?).GetValueOrDefault(0);
			reader.Read(); reader.Read();
			var docCount = (reader.Value as long?).GetValueOrDefault(0);
			reader.Read();

			var dateHistogram = new DateHistogramItem() { Key = key, KeyAsString = keyAsString, DocCount = docCount };
			dateHistogram.Aggregations = this.GetNestedAggregations(reader, serializer);
			return dateHistogram;

		}

		private IBucketItem GetKeyedBucketItem(JsonReader reader, JsonSerializer serializer)
		{
			var key = reader.ReadAsString();
			reader.Read();
			var property = reader.Value as string;
			if (property == "from" || property == "to")
				return GetRangeItem(reader, serializer, key);

			var keyItem = new KeyedBucketItem();
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

		private IBucketItem GetSignificantTermItem(JsonReader reader, JsonSerializer serializer, KeyedBucketItem keyItem)
		{
			reader.Read();
			var score = reader.Value as double?;
			reader.Read();
			reader.Read();
			var bgCount = reader.Value as long?;
			var significantTermItem = new SignificantTermItem()
			{
				Key = keyItem.Key,
				DocCount = keyItem.DocCount.GetValueOrDefault(0),
				BgCount = bgCount.GetValueOrDefault(0),
				Score = score.GetValueOrDefault(0)
			};
			reader.Read();
			significantTermItem.Aggregations = this.GetNestedAggregations(reader, serializer);
			return significantTermItem;
		}

		private IBucketItem GetFiltersBucketItem(JsonReader reader, JsonSerializer serializer)
		{
			reader.Read();
			var filtersBucketItem = new FiltersBucketItem();
			filtersBucketItem.DocCount = (reader.Value as long?).GetValueOrDefault(0);
			reader.Read();
			filtersBucketItem.Aggregations = this.GetNestedAggregations(reader, serializer);
			return filtersBucketItem;
		}
	}
}