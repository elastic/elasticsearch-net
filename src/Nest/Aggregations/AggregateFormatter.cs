using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class AggregateFormatter : IJsonFormatter<IAggregate>
	{
		private static readonly Regex _numeric = new Regex(@"^[\d.]+(\.[\d.]+)?$");

		static AggregateFormatter()
		{
			AllReservedAggregationNames = typeof(Parser)
				.GetFields(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
				.Where(f => f.IsLiteral && !f.IsInitOnly)
				.Select(f => (string)f.GetValue(null))
				.ToArray();

			var allKeys = string.Join(", ", AllReservedAggregationNames);
			UsingReservedAggNameFormat =
				"'{0}' is one of the reserved aggregation keywords"
				+ " we use a heuristics based response parser and using these reserved keywords"
				+ " could throw its heuristics off course. We are working on a solution in elasticsearch itself to make"
				+ " the response parseable. For now these are all the reserved keywords: "
				+ allKeys;
		}

		public static string[] AllReservedAggregationNames { get; }

		public static string UsingReservedAggNameFormat { get; }

		public IAggregate Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			ReadAggregate(ref reader, formatterResolver);

		public void Serialize(ref JsonWriter writer, IAggregate value, IJsonFormatterResolver formatterResolver) => throw new NotSupportedException();

		private IAggregate ReadAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadNext();

			if (reader.GetCurrentJsonToken() != JsonToken.String)
				return null;

			IAggregate aggregate = null;

			var propertyName = reader.ReadPropertyName();

			if (_numeric.IsMatch(propertyName))
				aggregate = GetPercentilesAggregate(ref reader, true);

			Dictionary<string, object> meta = null;

			if (propertyName == Parser.Meta)
			{
				meta = GetMetadata(ref reader, formatterResolver);

				// meta should never be the only property
				reader.ReadIsValueSeparatorWithVerify();
				propertyName = reader.ReadPropertyName();
			}

			if (aggregate != null)
			{
				// TODO: Close aggregate object here?

				aggregate.Meta = meta;
				return aggregate;
			}

			switch (propertyName)
			{
				case Parser.Values:
					reader.ReadNext();
					reader.ReadNext();
					aggregate = GetPercentilesAggregate(ref reader);
					break;
				case Parser.Value:
					aggregate = GetValueAggregate(ref reader, formatterResolver);
					break;
				case Parser.AfterKey:
					//reader.ReadNext();
					var dictionaryFormatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
					var afterKeys = dictionaryFormatter.Deserialize(ref reader, formatterResolver);
					reader.ReadNext(); // ,
					var bucketsPropertyName = reader.ReadPropertyName();
					var bucketAggregate = bucketsPropertyName == Parser.Buckets
						? GetMultiBucketAggregate(ref reader, formatterResolver, bucketsPropertyName) as BucketAggregate ?? new BucketAggregate()
						: new BucketAggregate();
					bucketAggregate.AfterKey = afterKeys;
					aggregate = bucketAggregate;
					break;
				case Parser.Buckets:
				case Parser.DocCountErrorUpperBound:
					aggregate = GetMultiBucketAggregate(ref reader, formatterResolver, propertyName);
					break;
				case Parser.Count:
					aggregate = GetStatsAggregate(ref reader);
					break;
				case Parser.DocCount:
					aggregate = GetSingleBucketAggregate(ref reader, formatterResolver);
					break;
				case Parser.Bounds:
					aggregate = GetGeoBoundsAggregate(ref reader, formatterResolver);
					break;
				case Parser.Hits:
					aggregate = GetTopHitsAggregate(ref reader, formatterResolver);
					break;
				case Parser.Location:
					aggregate = GetGeoCentroidAggregate(ref reader, formatterResolver);
					break;
				case Parser.Fields:
					aggregate = GetMatrixStatsAggregate(ref reader, formatterResolver);
					break;
				default:
					return null;
			}
			aggregate.Meta = meta;
			return aggregate;
		}

		private IBucket ReadBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadNext();

			if (reader.GetCurrentJsonToken() != JsonToken.String)
				return null;

			IBucket item;
			var property = reader.ReadPropertyName();
			switch (property)
			{
				case Parser.Key:
					item = GetKeyedBucket(ref reader, formatterResolver);
					break;
				case Parser.From:
				case Parser.To:
					item = GetRangeBucket(ref reader, formatterResolver, null, property);
					break;
				case Parser.KeyAsString:
					item = GetDateHistogramBucket(ref reader, formatterResolver);
					break;
				case Parser.DocCount:
					item = GetFiltersBucket(ref reader, formatterResolver);
					break;
				default:
					return null;
			}
			return item;
		}

		private Dictionary<string, object> GetMetadata(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var meta = formatterResolver.GetFormatter<Dictionary<string, object>>().Deserialize(ref reader, formatterResolver);
			return meta;
		}

		private IAggregate GetMatrixStatsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, long? docCount = null)
		{
			var matrixStats = new MatrixStatsAggregate { DocCount = docCount };
			var matrixStatsListFormatter = formatterResolver.GetFormatter<List<MatrixStatsField>>();
			matrixStats.Fields = matrixStatsListFormatter.Deserialize(ref reader, formatterResolver);
			return matrixStats;
		}

		private IAggregate GetTopHitsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var count = 0;
			long total = 0;
			double? maxScore = null;
			List<LazyDocument> topHits = null;

			while (reader.ReadIsInObject(ref count))
			{
				// TODO: use AutomataDictionary and don't allocate strings
				var propertyName = reader.ReadPropertyName();

				switch (propertyName)
				{
					case Parser.Total:
						total = reader.ReadInt64();
						break;
					case Parser.MaxScore:
						maxScore = reader.ReadNullableDouble();
						break;
					case Parser.Hits:
						var lazyDocumentsFormatter = formatterResolver.GetFormatter<List<LazyDocument>>();
						topHits = lazyDocumentsFormatter.Deserialize(ref reader, formatterResolver);
						break;
				}
			}

			return new TopHitsAggregate(topHits)
			{
				Total = total,
				MaxScore = maxScore
			};
		}

		private IAggregate GetGeoCentroidAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			reader.ReadNext();

			var geoLocationFormatter = formatterResolver.GetFormatter<GeoLocation>();
			var geoCentroid = new GeoCentroidAggregate
			{
				Location = geoLocationFormatter.Deserialize(ref reader, formatterResolver)
			};

			reader.ReadNext();

			if (reader.GetCurrentJsonToken() == JsonToken.String && reader.ReadPropertyName() == Parser.Count)
			{
				reader.ReadNext();
				geoCentroid.Count = reader.ReadInt64();
				reader.ReadNext();
			}
			return geoCentroid;
		}

		private IAggregate GetGeoBoundsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			reader.ReadNext();

			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var latLonDictionaryFormatter = formatterResolver.GetFormatter<Dictionary<string, LatLon>>();
			var latLons = latLonDictionaryFormatter.Deserialize(ref reader, formatterResolver);

			var geoBoundsMetric = new GeoBoundsAggregate();
			if (latLons.TryGetValue(Parser.TopLeft, out var topLeft) && topLeft != null)
				geoBoundsMetric.Bounds.TopLeft = topLeft;

			if (latLons.TryGetValue(Parser.BottomRight, out var bottomRight) && bottomRight != null)
				geoBoundsMetric.Bounds.BottomRight = bottomRight;

			reader.ReadNext();
			return geoBoundsMetric;
		}

		private IAggregate GetPercentilesAggregate(ref JsonReader reader, bool oldFormat = false)
		{
			var metric = new PercentilesAggregate();
			var percentileItems = new List<PercentileItem>();
			metric.Items = percentileItems;

			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return metric;

			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyName();
				if (propertyName.Contains(Parser.AsStringSuffix))
				{
					reader.ReadNext();
					reader.ReadNext();
				}

				if (reader.GetCurrentJsonToken() != JsonToken.EndObject)
				{
					var percentileValue = reader.ReadString();
					var percentile = double.Parse(percentileValue, CultureInfo.InvariantCulture);
					reader.ReadNext();

					var value = reader.ReadNullableDouble();
					percentileItems.Add(new PercentileItem
					{
						Percentile = percentile,
						Value = value.GetValueOrDefault(0)
					});
					reader.ReadNext();
				}
			}

			if (!oldFormat)
				reader.ReadNext();

			return metric;
		}

		private IAggregate GetSingleBucketAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var docCount = reader.ReadInt64();
			var token = reader.GetCurrentJsonToken();
			Dictionary<string, IAggregate> subAggregates = null;
			if (token == JsonToken.ValueSeparator)
			{
				reader.ReadNext(); // ,

				long bgCount = 0;
				var propertyName = reader.ReadPropertyName();

				if (propertyName == Parser.BgCount)
				{
					bgCount = reader.ReadInt64();
					reader.ReadIsValueSeparatorWithVerify();
					propertyName = reader.ReadPropertyName();
				}

				if (propertyName == Parser.Fields)
					return GetMatrixStatsAggregate(ref reader, formatterResolver, docCount);

				if (propertyName == Parser.Buckets)
				{
					var b = GetMultiBucketAggregate(ref reader, formatterResolver, propertyName) as BucketAggregate;
					return new BucketAggregate
					{
						BgCount = bgCount,
						DocCount = docCount,
						Items = b?.Items ?? EmptyReadOnly<IBucket>.Collection
					};
				}

				subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			}
			else
				reader.ReadIsEndObjectWithVerify();

			var bucket = new SingleBucketAggregate(subAggregates)
			{
				DocCount = docCount
			};

			return bucket;
		}

		private IAggregate GetStatsAggregate(ref JsonReader reader)
		{
			var count = reader.ReadNullableLong().GetValueOrDefault(0);

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
			{
				reader.ReadNext();
				return new GeoCentroidAggregate { Count = count };
			}

			reader.ReadNext(); // ,
			reader.ReadNext(); // "min"
			reader.ReadNext(); // :
			var min = reader.ReadNullableDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "max"
			reader.ReadNext(); // :
			var max = reader.ReadNullableDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // avg
			reader.ReadNext(); // :
			var average = reader.ReadNullableDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // sum
			reader.ReadNext(); // :
			var sum = reader.ReadNullableDouble();

			var statsMetric = new StatsAggregate()
			{
				Average = average,
				Count = count,
				Max = max,
				Min = min,
				Sum = sum
			};

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
			{
				reader.ReadNext();
				return statsMetric;
			}

			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			while (reader.GetCurrentJsonToken() != JsonToken.EndObject && propertyName.Contains(Parser.AsStringSuffix))
			{
				reader.ReadNext();
				reader.ReadNext();
			}

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				return statsMetric;

			return GetExtendedStatsAggregate(ref reader, statsMetric);
		}

		private IAggregate GetExtendedStatsAggregate(ref JsonReader reader, StatsAggregate statsMetric)
		{
			var extendedStatsMetric = new ExtendedStatsAggregate
			{
				Average = statsMetric.Average,
				Count = statsMetric.Count,
				Max = statsMetric.Max,
				Min = statsMetric.Min,
				Sum = statsMetric.Sum
			};

			reader.ReadNext();
			extendedStatsMetric.SumOfSquares = reader.ReadNullableDouble();
			reader.ReadNext();
			reader.ReadNext();
			extendedStatsMetric.Variance = reader.ReadNullableDouble();
			reader.ReadNext();
			reader.ReadNext();
			extendedStatsMetric.StdDeviation = reader.ReadNullableDouble();
			reader.ReadNext();

			string propertyName;

			if (reader.GetCurrentJsonToken() != JsonToken.EndObject)
			{
				var bounds = new StandardDeviationBounds();
				reader.ReadNext();
				reader.ReadNext();

				propertyName = reader.ReadPropertyName();
				if (propertyName == Parser.Upper)
				{
					reader.ReadNext();
					bounds.Upper = reader.ReadNullableDouble();
				}
				reader.ReadNext();

				propertyName = reader.ReadPropertyName();
				if (propertyName == Parser.Lower)
				{
					reader.ReadNext();
					bounds.Lower = reader.ReadNullableDouble();
				}
				extendedStatsMetric.StdDeviationBounds = bounds;
				reader.ReadNext();
				reader.ReadNext();
			}

			propertyName = reader.ReadPropertyName();
			while (reader.GetCurrentJsonToken() != JsonToken.EndObject && propertyName.Contains(Parser.AsStringSuffix))
			{
				// std_deviation_bounds is an object, so we need to skip its properties
				if (propertyName.Equals(Parser.StdDeviationBoundsAsString))
				{
					reader.ReadNext();
					reader.ReadNext();
					reader.ReadNext();
					reader.ReadNext();
				}
				reader.ReadNext();
				reader.ReadNext();
			}
			return extendedStatsMetric;
		}

		private Dictionary<string, IAggregate> GetSubAggregates(ref JsonReader reader, string name, IJsonFormatterResolver formatterResolver)
		{
			var nestedAggs = new Dictionary<string, IAggregate>();

			// deserialize the first aggregate
			var aggregate = Deserialize(ref reader, formatterResolver);
			nestedAggs.Add(name, aggregate);

			// start at 1 to skip the BeginObject check
			var count = 1;
			while (reader.ReadIsInObject(ref count))
			{
				name = reader.ReadPropertyName();
				aggregate = Deserialize(ref reader, formatterResolver);
				nestedAggs.Add(name, aggregate);
			}

//			var currentDepth = reader.Depth;
//			do
//			{
//				var fieldName = (string)reader.Value;
//				reader.Read();
//				var agg = ReadAggregate(ref reader, formatterResolver);
//				nestedAggs.Add(fieldName, agg);
//				reader.Read();
//				if (reader.Depth == currentDepth && reader.TokenType == JsonToken.EndObject || reader.Depth < currentDepth)
//					break;
//			} while (true);
			return nestedAggs;
		}

		private IAggregate GetMultiBucketAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, string propertyName)
		{
			var bucket = new BucketAggregate();
			if (propertyName == Parser.DocCountErrorUpperBound)
			{
				bucket.DocCountErrorUpperBound = reader.ReadNullableLong();
				reader.ReadIsValueSeparatorWithVerify();
				propertyName = reader.ReadPropertyName();
			}

			if (propertyName == Parser.SumOtherDocCount)
			{
				bucket.SumOtherDocCount = reader.ReadNullableLong();
				reader.ReadIsValueSeparatorWithVerify();
				reader.ReadPropertyName(); // "buckets"
			}

			var items = new List<IBucket>();
			var count = 0;
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.BeginObject)
			{
				var filterAggregates = new Dictionary<string, IAggregate>();
				while (reader.ReadIsInObject(ref count))
				{
					var name = reader.ReadPropertyName();
					var innerAgg = ReadAggregate(ref reader, formatterResolver);
					filterAggregates[name] = innerAgg;
				}
				return new FiltersAggregate(filterAggregates);
			}

			if (token != JsonToken.BeginArray)
			{
				reader.ReadNextBlock();
				return null;
			}

			while (reader.ReadIsInArray(ref count))
			{
				var item = ReadBucket(ref reader, formatterResolver);
				items.Add(item);
			}

			bucket.Items = items;
			reader.ReadNext(); // close outer }
			return bucket;
		}

		private IAggregate GetValueAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var valueMetric = new ValueAggregate
			{
				Value = reader.ReadNullableDouble()
			};

			if (valueMetric.Value == null && reader.GetCurrentJsonToken() == JsonToken.Number)
				valueMetric.Value = reader.ReadNullableLong();

			// https://github.com/elastic/elasticsearch-net/issues/3311
			// above code just checks for long through reader.ValueType, this is not always the case
			if (valueMetric.Value != null || reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				//reader.ReadNext();
				var token = reader.GetCurrentJsonToken();
				if (token != JsonToken.EndObject)
				{
					if (token == JsonToken.String)
					{
						var propertyName = reader.ReadPropertyName();

						if (propertyName == Parser.ValueAsString)
						{
							valueMetric.ValueAsString = reader.ReadString();
							reader.ReadNext();
						}

						if (reader.GetCurrentJsonToken() == JsonToken.String)
						{
							propertyName = reader.ReadPropertyName();
							if (propertyName == Parser.Keys)
							{
								var keyedValueMetric = new KeyedValueAggregate
								{
									Value = valueMetric.Value
								};
								var keys = new List<string>();
								reader.ReadNext();
								reader.ReadNext();

								var count = 0;
								while (reader.ReadIsInArray(ref count)) keys.Add(reader.ReadString());
								reader.ReadNext();
								keyedValueMetric.Keys = keys;
								return keyedValueMetric;
							}
						}
					}
					else
					{
						reader.ReadNext();
						reader.ReadNext();
					}
				}
				else
					reader.ReadNext(); // }

				return valueMetric;
			}

			var scriptedMetric = reader.ReadNextBlockSegment();
			reader.ReadNext();
			if (scriptedMetric != default)
				return new ScriptedMetricAggregate(new LazyDocument(BinaryUtil.ToArray(ref scriptedMetric), formatterResolver));

			return valueMetric;
		}

		public IBucket GetRangeBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver, string key, string propertyName)
		{
			string fromAsString = null;
			string toAsString = null;
			long? docCount = null;
			double? toDouble = null;
			double? fromDouble = null;
			var isSubAggregateName = false;

			while (true)
			{
				switch (propertyName)
				{
					case Parser.From:
						// TODO: handle string "from" e.g. ip_range
						if (reader.GetCurrentJsonToken() == JsonToken.Number)
							fromDouble = reader.ReadDouble();
						else
							reader.ReadNext();
						break;
					case Parser.To:
						// TODO: handle string "to" e.g. ip_range
						if (reader.GetCurrentJsonToken() == JsonToken.Number)
							toDouble = reader.ReadDouble();
						else
							reader.ReadNext();
						break;
					case Parser.Key:
						key = reader.ReadString();
						break;
					case Parser.FromAsString:
						fromAsString = reader.ReadString();
						break;
					case Parser.ToAsString:
						toAsString = reader.ReadString();
						break;
					case Parser.DocCount:
						docCount = reader.ReadNullableLong().GetValueOrDefault(0);
						break;
					default:
						isSubAggregateName = true;
						break;
				}

				if (isSubAggregateName)
					break;

				if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				{
					reader.ReadNext();
					break;
				}

				reader.ReadNext(); // ,
				propertyName = reader.ReadPropertyName();
			}

			Dictionary<string, IAggregate> subAggregates = null;

			if (isSubAggregateName)
				subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);

			var bucket = new RangeBucket(subAggregates)
			{
				Key = key,
				From = fromDouble,
				To = toDouble,
				DocCount = docCount.GetValueOrDefault(),
				FromAsString = fromAsString,
				ToAsString = toAsString,
			};

			return bucket;
		}

		private IBucket GetDateHistogramBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var keyAsString = reader.ReadString();

			reader.ReadNext(); // ,
			reader.ReadNext(); // "key"
			reader.ReadNext(); // :
			var key = reader.ReadNullableLong().GetValueOrDefault(0);
			reader.ReadNext(); // ,
			reader.ReadNext(); // "doc_count"
			reader.ReadNext(); // :
			var docCount = reader.ReadNullableLong().GetValueOrDefault(0);
			reader.ReadNext(); // ,

			var propertyName = reader.ReadPropertyName();
			var subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);

			var dateHistogram = new DateHistogramBucket(subAggregates)
			{
				Key = key,
				KeyAsString = keyAsString,
				DocCount = docCount,
			};

			return dateHistogram;
		}

		private IBucket GetKeyedBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.BeginObject)
				return GetCompositeBucket(ref reader, formatterResolver);

			object key;
			if (token == JsonToken.String)
				key = reader.ReadString();
			else
				key = reader.ReadDouble();

			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			if (propertyName == Parser.From || propertyName == Parser.To)
				return GetRangeBucket(ref reader, formatterResolver, (string)key, propertyName);

			string keyAsString = null;
			if (propertyName == Parser.KeyAsString)
			{
				keyAsString = reader.ReadString();
				reader.ReadNext(); // ,
				reader.ReadNext(); // "doc_count"
			}

			var docCount = reader.ReadInt64();
			Dictionary<string, IAggregate> subAggregates = null;
			long? docCountErrorUpperBound = null;

			token = reader.GetCurrentJsonToken();
			if (token == JsonToken.ValueSeparator)
			{
				reader.ReadNext();

				propertyName = reader.ReadPropertyName();
				if (propertyName == Parser.Score)
					return GetSignificantTermsBucket(ref reader, formatterResolver, key, keyAsString, docCount);

				if (propertyName == Parser.DocCountErrorUpperBound)
				{
					docCountErrorUpperBound = reader.ReadNullableLong();
					token = reader.GetCurrentJsonToken();
					if (token == JsonToken.ValueSeparator)
					{
						reader.ReadNext(); // ,
						propertyName = reader.ReadPropertyName();
						subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
					}
					else
						reader.ReadIsEndObjectWithVerify();
				}
				else
					subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			}
			else
				reader.ReadIsEndObjectWithVerify();

			return new KeyedBucket<object>(subAggregates)
			{
				Key = key,
				KeyAsString = keyAsString,
				DocCount = docCount,
				DocCountErrorUpperBound = docCountErrorUpperBound
			};
		}

		private IBucket GetCompositeBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var readonlyDictionaryFormatter = formatterResolver.GetFormatter<IReadOnlyDictionary<string, object>>();
			var key = new CompositeKey(readonlyDictionaryFormatter.Deserialize(ref reader, formatterResolver));
			reader.ReadNext(); // ,
			long? docCount = null;
			string propertyName = null;
			if (reader.GetCurrentJsonToken() == JsonToken.String && (propertyName = reader.ReadPropertyName()) == Parser.DocCount)
			{
				docCount = reader.ReadNullableLong();
				reader.ReadNext(); // ,
				propertyName = reader.ReadPropertyName();
			}

			var nestedAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			return new CompositeBucket(nestedAggregates, key) { DocCount = docCount };
		}

		private IBucket GetSignificantTermsBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver, object key, string keyAsString,
			long? docCount
		)
		{
			var score = reader.ReadDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "bg_count"
			reader.ReadNext(); // :
			var bgCount = reader.ReadInt64();

			Dictionary<string, IAggregate> subAggregates = null;

			if (reader.GetCurrentJsonToken() == JsonToken.ValueSeparator)
			{
				reader.ReadNext(); // ,
				var propertyName = reader.ReadPropertyName();
				subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			}
			else
				reader.ReadNext(); // }

			var significantTermItem = new SignificantTermsBucket(subAggregates)
			{
				Key = (string)key,
				DocCount = docCount.GetValueOrDefault(0),
				BgCount = bgCount,
				Score = score
			};
			return significantTermItem;
		}

		private IBucket GetFiltersBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			//reader.ReadNext();
			var docCount = reader.ReadNullableLong().GetValueOrDefault(0);
			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			var subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			var filtersBucketItem = new FiltersBucketItem(subAggregates)
			{
				DocCount = docCount
			};
			return filtersBucketItem;
		}

		private static class Parser
		{
			public const string AfterKey = "after_key";

			public const string AsStringSuffix = "_as_string";
			public const string BgCount = "bg_count";
			public const string BottomRight = "bottom_right";
			public const string Bounds = "bounds";
			public const string Buckets = "buckets";
			public const string Count = "count";
			public const string DocCount = "doc_count";
			public const string DocCountErrorUpperBound = "doc_count_error_upper_bound";
			public const string Fields = "fields";
			public const string From = "from";

			public const string FromAsString = "from_as_string";
			public const string Hits = "hits";

			public const string Key = "key";
			public const string KeyAsString = "key_as_string";
			public const string Keys = "keys";
			public const string Location = "location";
			public const string Lower = "lower";
			public const string MaxScore = "max_score";
			public const string Meta = "meta";

			public const string Score = "score";
			public const string StdDeviationBoundsAsString = "std_deviation_bounds_as_string";

			public const string SumOtherDocCount = "sum_other_doc_count";
			public const string To = "to";
			public const string ToAsString = "to_as_string";

			public const string TopLeft = "top_left";

			public const string Total = "total";

			public const string Upper = "upper";
			public const string Value = "value";

			public const string ValueAsString = "value_as_string";
			public const string Values = "values";
		}
	}
}
