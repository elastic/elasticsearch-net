// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Nest.Utf8Json;

namespace Nest
{
	internal class AggregateFormatter : IJsonFormatter<IAggregate>
	{
		private static readonly byte[] BgCountField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.BgCount);

		private static readonly AutomataDictionary BucketFields = new AutomataDictionary
		{
			{ Parser.Key, 0 },
			{ Parser.From, 1 },
			{ Parser.To, 2 },
			{ Parser.KeyAsString, 3 },
			{ Parser.DocCount, 4 }
		};

		private static readonly byte[] BucketsField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.Buckets);
		private static readonly byte[] DocCountErrorUpperBound = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.DocCountErrorUpperBound);
		private static readonly byte[] FieldsField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.Fields);

		private static readonly AutomataDictionary GeoBoundsFields = new AutomataDictionary
		{
			{ Parser.TopLeft, 0 },
			{ Parser.BottomRight, 1 },
		};

		private static readonly byte[] KeysField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.Keys);
		private static readonly byte[] MetaField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.Meta);
		private static readonly byte[] MinLengthField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.MinLength);

		private static readonly AutomataDictionary RootFields = new AutomataDictionary
		{
			{ Parser.Values, 0 },
			{ Parser.Value, 1 },
			{ Parser.AfterKey, 2 },
			{ Parser.Buckets, 3 },
			{ Parser.DocCountErrorUpperBound, 4 },
			{ Parser.Count, 5 },
			{ Parser.DocCount, 6 },
			{ Parser.Bounds, 7 },
			{ Parser.Hits, 8 },
			{ Parser.Location, 9 },
			{ Parser.Fields, 10 },
			{ Parser.Min, 11 },
			{ Parser.Top, 12 },
		};

		private static readonly byte[] SumOtherDocCount = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.SumOtherDocCount);

		private static readonly AutomataDictionary TopHitsFields = new AutomataDictionary
		{
			{ Parser.Total, 0 },
			{ Parser.MaxScore, 1 },
			{ Parser.Hits, 2 },
		};

		private static readonly AutomataDictionary ExtendedStatsFields = new AutomataDictionary
		{
			{ "variance", 0 },
			{ "std_deviation", 1 },
			{ "std_deviation_bounds", 2 },
			{ "variance_population", 3 },
			{ "variance_sampling", 4 },
			{ "std_deviation_population", 5 },
			{ "std_deviation_sampling", 6 },
		};

		private static readonly byte[] ValueAsStringField = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.ValueAsString);

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
				+ " could throw its heuristics off course. We are working on a solution in Elasticsearch itself to make"
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
			reader.ReadIsBeginObjectWithVerify();
			IAggregate aggregate = null;

			if (reader.ReadIsEndObject()) return null;

			var propertyName = reader.ReadPropertyNameSegmentRaw();
			Dictionary<string, object> meta = null;

			if (propertyName.EqualsBytes(MetaField))
			{
				meta = GetMetadata(ref reader, formatterResolver);
				reader.ReadIsValueSeparatorWithVerify();
				propertyName = reader.ReadPropertyNameSegmentRaw();
			}

			if (RootFields.TryGetValue(propertyName, out var value))
			{
				switch (value)
				{
					case 0:
						aggregate = GetPercentilesAggregate(ref reader, meta);
						break;
					case 1:
						aggregate = GetValueAggregate(ref reader, formatterResolver, meta);
						break;
					case 2:
						var compositeKeyFormatter = formatterResolver.GetFormatter<CompositeKey>();
						var afterKey = compositeKeyFormatter.Deserialize(ref reader, formatterResolver);
						reader.ReadNext(); // ,
						propertyName = reader.ReadPropertyNameSegmentRaw();
						var bucketAggregate = propertyName.EqualsBytes(BucketsField)
							? GetMultiBucketAggregate(ref reader, formatterResolver, ref propertyName, meta) as BucketAggregate ?? new BucketAggregate { Meta = meta }
							: new BucketAggregate { Meta = meta };
						bucketAggregate.AfterKey = afterKey;
						aggregate = bucketAggregate;
						break;
					case 3:
					case 4:
						aggregate = GetMultiBucketAggregate(ref reader, formatterResolver, ref propertyName, meta);
						break;
					case 5:
						aggregate = GetStatsAggregate(ref reader, formatterResolver, meta);
						break;
					case 6:
						aggregate = GetSingleBucketAggregate(ref reader, formatterResolver, meta);
						break;
					case 7:
						aggregate = GetGeoBoundsAggregate(ref reader, formatterResolver, meta);
						break;
					case 8:
						aggregate = GetTopHitsAggregate(ref reader, formatterResolver, meta);
						break;
					case 9:
						aggregate = GetGeoCentroidAggregate(ref reader, formatterResolver, meta);
						break;
					case 10:
						aggregate = GetMatrixStatsAggregate(ref reader, formatterResolver, meta);
						break;
					case 11:
						aggregate = GetBoxplotAggregate(ref reader, meta);
						break;
					case 12:
						aggregate = GetTopMetricsAggregate(ref reader, formatterResolver, meta);
						break;
				}
			}
			else
				reader.ReadNextBlock();

			reader.ReadIsEndObjectWithVerify();
			return aggregate;
		}

		private IBucket ReadBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			reader.ReadNext(); // {
			var property = reader.ReadPropertyNameSegmentRaw();

			IBucket item = null;
			if (BucketFields.TryGetValue(property, out var value))
			{
				switch (value)
				{
					case 0:
						item = GetKeyedBucket(ref reader, formatterResolver);
						break;
					case 1:
					case 2:
						item = GetRangeBucket(ref reader, formatterResolver, null, property.Utf8String());
						break;
					case 3:
						item = GetDateHistogramBucket(ref reader, formatterResolver);
						break;
					case 4:
						item = GetFiltersBucket(ref reader, formatterResolver);
						break;
				}
			}
			else
				reader.ReadNextBlock();

			reader.ReadNext(); // }

			return item;
		}

		private Dictionary<string, object> GetMetadata(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var meta = formatterResolver.GetFormatter<Dictionary<string, object>>().Deserialize(ref reader, formatterResolver);
			return meta;
		}

		private IAggregate GetMatrixStatsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta,
			long? docCount = null
		)
		{
			var matrixStats = new MatrixStatsAggregate { DocCount = docCount.GetValueOrDefault(), Meta = meta };
			var matrixStatsListFormatter = formatterResolver.GetFormatter<List<MatrixStatsField>>();
			matrixStats.Fields = matrixStatsListFormatter.Deserialize(ref reader, formatterResolver);
			return matrixStats;
		}

		private IAggregate GetBoxplotAggregate(ref JsonReader reader, IReadOnlyDictionary<string, object> meta)
		{
			var boxplot = new BoxplotAggregate
			{
				Min = reader.ReadDouble(),
				Meta = meta
			};
			reader.ReadNext(); // ,
			reader.ReadNext(); // "max"
			reader.ReadNext(); // :
			boxplot.Max = reader.ReadDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "q1"
			reader.ReadNext(); // :
			boxplot.Q1 = reader.ReadDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "q2"
			reader.ReadNext(); // :
			boxplot.Q2 = reader.ReadDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "q3"
			reader.ReadNext(); // :
			boxplot.Q3 = reader.ReadDouble();

			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.EndObject)
			{
				reader.ReadNext(); // ,
				reader.ReadNext(); // "lower"
				reader.ReadNext(); // :
				boxplot.Lower = reader.ReadDouble();
				reader.ReadNext(); // ,
				reader.ReadNext(); // "upper"
				reader.ReadNext(); // :
				boxplot.Upper = reader.ReadDouble();
			}

			return boxplot;
    }

		private IAggregate GetTopMetricsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta)
		{
			var topMetrics = new TopMetricsAggregate { Meta = meta };
			var formatter = formatterResolver.GetFormatter<List<TopMetric>>();
			topMetrics.Top = formatter.Deserialize(ref reader, formatterResolver);
			return topMetrics;
		}

		private IAggregate GetTopHitsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta)
		{
			var count = 0;
			double? maxScore = null;
			TotalHits total = null;
			List<LazyDocument> topHits = null;

			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (TopHitsFields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							var hitsFormatter = formatterResolver.GetFormatter<TotalHits>();
							total = hitsFormatter.Deserialize(ref reader, formatterResolver);
							break;
						case 1:
							maxScore = reader.ReadNullableDouble();
							break;
						case 2:
							var lazyDocumentsFormatter = formatterResolver.GetFormatter<List<LazyDocument>>();
							topHits = lazyDocumentsFormatter.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
					reader.ReadNextBlock();
			}

			return new TopHitsAggregate(topHits, formatterResolver)
			{
				Total = total,
				MaxScore = maxScore,
				Meta = meta
			};
		}

		private IAggregate GetGeoCentroidAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta)
		{
			var geoLocationFormatter = formatterResolver.GetFormatter<GeoLocation>();
			var geoCentroid = new GeoCentroidAggregate
			{
				Location = geoLocationFormatter.Deserialize(ref reader, formatterResolver),
				Meta = meta
			};

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				return geoCentroid;

			reader.ReadNext(); // ,

			if (reader.ReadPropertyName() == Parser.Count)
				geoCentroid.Count = reader.ReadInt64();

			return geoCentroid;
		}

		private IAggregate GetGeoBoundsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var geoBoundsMetric = new GeoBoundsAggregate { Meta = meta };
			var latLonFormatter = formatterResolver.GetFormatter<LatLon>();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (GeoBoundsFields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							geoBoundsMetric.Bounds.TopLeft =
								latLonFormatter.Deserialize(ref reader, formatterResolver);
							break;
						case 1:
							geoBoundsMetric.Bounds.BottomRight =
								latLonFormatter.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
					reader.ReadNextBlock();
			}

			return geoBoundsMetric;
		}

		private IAggregate GetPercentilesAggregate(ref JsonReader reader, IReadOnlyDictionary<string, object> meta)
		{
			var metric = new PercentilesAggregate { Meta = meta };
			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.BeginObject && token != JsonToken.BeginArray)
			{
				reader.ReadNextBlock();
				return metric;
			}

			var count = 0;
			if (token == JsonToken.BeginObject)
			{
				while (reader.ReadIsInObject(ref count))
				{
					var propertyName = reader.ReadPropertyName();
					if (propertyName.Contains(Parser.AsStringSuffix))
					{
						reader.ReadNextBlock();
						continue;
					}

					metric.Items.Add(new PercentileItem
					{
						Percentile = double.Parse(propertyName, CultureInfo.InvariantCulture),
						Value = reader.ReadNullableDouble()
					});
				}
			}
			else
			{
				while (reader.ReadIsInArray(ref count))
				{
					reader.ReadNext(); // {
					reader.ReadNext(); // "key"
					reader.ReadNext(); // :
					var percentile = reader.ReadDouble();
					reader.ReadNext(); // ,
					reader.ReadNext(); // "value"
					reader.ReadNext(); // :
					metric.Items.Add(new PercentileItem
					{
						Percentile = percentile,
						Value = reader.ReadNullableDouble()
					});
					reader.ReadNext(); // }
				}
			}

			return metric;
		}

		private IAggregate GetSingleBucketAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta)
		{
			var docCount = reader.ReadInt64();
			var token = reader.GetCurrentJsonToken();
			Dictionary<string, IAggregate> subAggregates = null;
			if (token == JsonToken.ValueSeparator)
			{
				reader.ReadNext(); // ,

				long bgCount = 0;
				var propertyName = reader.ReadPropertyNameSegmentRaw();

				if (propertyName.EqualsBytes(BgCountField))
				{
					bgCount = reader.ReadInt64();
					reader.ReadIsValueSeparatorWithVerify();
					propertyName = reader.ReadPropertyNameSegmentRaw();
				}

				if (propertyName.EqualsBytes(FieldsField))
					return GetMatrixStatsAggregate(ref reader, formatterResolver, meta, docCount);

				if (propertyName.EqualsBytes(BucketsField))
				{
					var b = GetMultiBucketAggregate(ref reader, formatterResolver, ref propertyName, meta) as BucketAggregate;
					return new BucketAggregate
					{
						BgCount = bgCount,
						DocCount = docCount,
						Items = b?.Items ?? EmptyReadOnly<IBucket>.Collection,
						Meta = meta
					};
				}

				subAggregates = GetSubAggregates(ref reader, propertyName.Utf8String(), formatterResolver);
			}

			return new SingleBucketAggregate(subAggregates) { DocCount = docCount, Meta = meta };
		}

		private IAggregate GetStringStatsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver,
			IReadOnlyDictionary<string, object> meta, long count
		)
		{
			// string stats aggregation
			var minLength = reader.ReadInt32();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "max_length"
			reader.ReadNext(); // :
			var maxLength = reader.ReadInt32();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "avg_length"
			reader.ReadNext(); // :
			var avgLength = reader.ReadDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "entropy"
			reader.ReadNext(); // :
			var entropy = reader.ReadDouble();

			var aggregate = new StringStatsAggregate
			{
				Meta = meta,
				Count = count,
				MinLength = minLength,
				MaxLength = maxLength,
				AverageLength = avgLength,
				Entropy = entropy
			};

			if (reader.ReadIsValueSeparator())
			{
				reader.ReadNext(); // "distribution"
				reader.ReadNext(); // :
				var distribution = formatterResolver
					.GetFormatter<IReadOnlyDictionary<string, double>>()
					.Deserialize(ref reader, formatterResolver);

				// only set distribution if present, leaving empty dictionary when absent
				aggregate.Distribution = distribution;
			}

			return aggregate;
		}

		private IAggregate GetStatsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta
		)
		{
			var count = reader.ReadNullableLong().GetValueOrDefault(0);

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				return new GeoCentroidAggregate { Count = count, Meta = meta };

			reader.ReadNext(); // ,

			var property = reader.ReadPropertyNameSegmentRaw();

			// string stats aggregation
			if (property.EqualsBytes(MinLengthField))
				return GetStringStatsAggregate(ref reader, formatterResolver, meta, count);

			// stats or extended stats aggregation
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
			var sum = reader.ReadDouble();

			var statsMetric = new StatsAggregate
			{
				Average = average,
				Count = count,
				Max = max,
				Min = min,
				Sum = sum,
				Meta = meta
			};

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				return statsMetric;

			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			while (reader.GetCurrentJsonToken() != JsonToken.EndObject && propertyName.EndsWith(Parser.AsStringSuffix))
			{
				reader.ReadNext(); // <value>
				if (reader.GetCurrentJsonToken() == JsonToken.ValueSeparator)
				{
					reader.ReadNext(); // ,
					propertyName = reader.ReadPropertyName();
				}
			}

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				return statsMetric;

			return GetExtendedStatsAggregate(ref reader, formatterResolver, statsMetric);
		}

		private IAggregate GetExtendedStatsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, StatsAggregate statsMetric)
		{
			var extendedStatsMetric = new ExtendedStatsAggregate
			{
				Average = statsMetric.Average,
				Count = statsMetric.Count,
				Max = statsMetric.Max,
				Min = statsMetric.Min,
				Sum = statsMetric.Sum,
				Meta = statsMetric.Meta
			};

			extendedStatsMetric.SumOfSquares = reader.ReadNullableDouble();
			reader.ReadNext(); // ,

			while (reader.GetCurrentJsonToken() != JsonToken.EndObject)
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (ExtendedStatsFields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							extendedStatsMetric.Variance = reader.ReadNullableDouble();
							break;
						case 1:
							extendedStatsMetric.StdDeviation = reader.ReadNullableDouble();
							break;
						case 2:
							extendedStatsMetric.StdDeviationBounds =
								formatterResolver.GetFormatter<StandardDeviationBounds>().Deserialize(ref reader, formatterResolver);
							break;
						case 3:
							extendedStatsMetric.VariancePopulation = reader.ReadNullableDouble();
							break;
						case 4:
							extendedStatsMetric.VarianceSampling = reader.ReadNullableDouble();
							break;
						case 5:
							extendedStatsMetric.StdDeviationPopulation = reader.ReadNullableDouble();
							break;
						case 6:
							extendedStatsMetric.StdDeviationSampling = reader.ReadNullableDouble();
							break;
					}
				}
				else
					reader.ReadNextBlock();

				reader.ReadIsValueSeparator();
			}

			return extendedStatsMetric;
		}

		private Dictionary<string, IAggregate> GetSubAggregates(ref JsonReader reader, string name, IJsonFormatterResolver formatterResolver)
		{
			var subAggregates = new Dictionary<string, IAggregate>();

			// deserialize the first aggregate
			var aggregate = Deserialize(ref reader, formatterResolver);
			subAggregates.Add(name, aggregate);

			// keep reading sibling aggregates
			while (reader.GetCurrentJsonToken() == JsonToken.ValueSeparator)
			{
				reader.ReadNext(); // ,
				name = reader.ReadPropertyName();
				aggregate = Deserialize(ref reader, formatterResolver);
				subAggregates.Add(name, aggregate);
			}

			return subAggregates;
		}

		private IAggregate GetMultiBucketAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver,
			ref ArraySegment<byte> propertyName, IReadOnlyDictionary<string, object> meta)
		{
			var bucket = new BucketAggregate { Meta = meta };
			if (propertyName.EqualsBytes(DocCountErrorUpperBound))
			{
				bucket.DocCountErrorUpperBound = reader.ReadNullableLong();
				reader.ReadIsValueSeparatorWithVerify();
				propertyName = reader.ReadPropertyNameSegmentRaw();
			}

			if (propertyName.EqualsBytes(SumOtherDocCount))
			{
				bucket.SumOtherDocCount = reader.ReadNullableLong();
				reader.ReadIsValueSeparatorWithVerify();
				reader.ReadNext(); // "buckets"
				reader.ReadNext(); // :
			}

			var items = new List<IBucket>();
			bucket.Items = items;
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
				return new FiltersAggregate(filterAggregates) { Meta = meta };
			}

			while (reader.ReadIsInArray(ref count))
			{
				var item = ReadBucket(ref reader, formatterResolver);
				items.Add(item);
			}

			token = reader.GetCurrentJsonToken();
			if (token == JsonToken.ValueSeparator)
			{
				reader.ReadNext();
				propertyName = reader.ReadPropertyNameSegmentRaw();
				if (propertyName.EqualsBytes(JsonWriter.GetEncodedPropertyNameWithoutQuotation("interval")))
					bucket.Interval = formatterResolver.GetFormatter<DateMathTime>().Deserialize(ref reader, formatterResolver);
				else
					// skip for now
					reader.ReadNextBlock();
			}

			return bucket;
		}

		private IAggregate GetValueAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IReadOnlyDictionary<string, object> meta)
		{
			var token = reader.GetCurrentJsonToken();
			if (token == JsonToken.Number || token == JsonToken.Null)
			{
				var value = reader.ReadNullableDouble();
				string valueAsString = null;
				token = reader.GetCurrentJsonToken();
				if (token != JsonToken.EndObject)
				{
					reader.ReadNext(); // ,

					var propertyName = reader.ReadPropertyNameSegmentRaw();
					if (propertyName.EqualsBytes(ValueAsStringField))
					{
						valueAsString = reader.ReadString();
						token = reader.GetCurrentJsonToken();

						if (token == JsonToken.EndObject)
							return new ValueAggregate
							{
								Value = value,
								ValueAsString = valueAsString,
								Meta = meta
							};

						reader.ReadNext(); // ,
						propertyName = reader.ReadPropertyNameSegmentRaw();
					}

					if (propertyName.EqualsBytes(KeysField))
					{
						var keyedValueMetric = new KeyedValueAggregate
						{
							Value = value,
							Meta = meta
						};

						var formatter = formatterResolver.GetFormatter<List<string>>();
						keyedValueMetric.Keys = formatter.Deserialize(ref reader, formatterResolver);
						return keyedValueMetric;
					}

					// skip any remaining properties for now
					while (token != JsonToken.EndObject)
					{
						reader.ReadNextBlock();
						token = reader.GetCurrentJsonToken();
					}
				}

				return new ValueAggregate
				{
					Value = value,
					ValueAsString = valueAsString,
					Meta = meta
				};
			}

			var scriptedMetric = reader.ReadNextBlockSegment();
			var bytes = BinaryUtil.ToArray(ref scriptedMetric);
			var doc = new LazyDocument(bytes, formatterResolver);
			return new ScriptedMetricAggregate(doc)
			{
				Meta = meta
			};
		}

		public IBucket GetRangeBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver, string key, string propertyName)
		{
			string fromAsString = null;
			string fromString = null;
			string toAsString = null;
			string toString = null;
			long? docCount = null;
			double? toDouble = null;
			double? fromDouble = null;
			var isSubAggregateName = false;

			while (true)
			{
				switch (propertyName)
				{
					case Parser.From:
						var currentFromJsonToken = reader.GetCurrentJsonToken();
						if (currentFromJsonToken == JsonToken.Number)
							fromDouble = reader.ReadDouble();
						else if (currentFromJsonToken == JsonToken.String)
							fromString = reader.ReadString();
						else
							reader.ReadNext();
						break;
					case Parser.To:
						var currentJsonToToken = reader.GetCurrentJsonToken();
						if (currentJsonToToken == JsonToken.Number)
							toDouble = reader.ReadDouble();
						else if (currentJsonToToken == JsonToken.String)
							toString = reader.ReadString();
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

				if (isSubAggregateName || reader.GetCurrentJsonToken() == JsonToken.EndObject)
					break;

				reader.ReadNext(); // ,
				propertyName = reader.ReadPropertyName();
			}

			Dictionary<string, IAggregate> subAggregates = null;

			if (isSubAggregateName)
				subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);

			IBucket bucket;
			if (fromString != null || toString != null)
				bucket = new IpRangeBucket(subAggregates)
				{
					Key = key,
					DocCount = docCount.GetValueOrDefault(),
					From = fromString,
					To = toString,
				};
			else
				bucket = new RangeBucket(subAggregates)
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
			var key = reader.ReadInt64();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "doc_count"
			reader.ReadNext(); // :
			var docCount = reader.ReadInt64();

			Dictionary<string, IAggregate> subAggregates = null;
			if (reader.GetCurrentJsonToken() == JsonToken.ValueSeparator)
			{
				reader.ReadNext(); // ,
				var propertyName = reader.ReadPropertyName();
				subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			}

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
			{
				var numberSegment = reader.ReadNumberSegment();
				if (numberSegment.IsLong())
					key = NumberConverter.ReadInt64(numberSegment.Array, numberSegment.Offset, out _);
				else
					key = NumberConverter.ReadDouble(numberSegment.Array, numberSegment.Offset, out _);
			}

			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			if (propertyName == Parser.From || propertyName == Parser.To)
			{
				var rangeKey = key is double d
					? d.ToString("#.#")
					: key.ToString();
				return GetRangeBucket(ref reader, formatterResolver, rangeKey, propertyName);
			}

			string keyAsString = null;
			if (propertyName == Parser.KeyAsString)
			{
				keyAsString = reader.ReadString();
				reader.ReadNext(); // ,
				reader.ReadNext(); // "doc_count"
				reader.ReadNext(); // :
			}

			var docCount = reader.ReadInt64();
			Dictionary<string, IAggregate> subAggregates = null;
			long? docCountErrorUpperBound = null;

			token = reader.GetCurrentJsonToken();
			if (token == JsonToken.ValueSeparator)
			{
				reader.ReadNext();
				propertyName = reader.ReadPropertyName();
				switch (propertyName)
				{
					case Parser.Score:
						return GetSignificantTermsBucket(ref reader, formatterResolver, key, docCount);
					case Parser.DocCountErrorUpperBound:
					{
						docCountErrorUpperBound = reader.ReadNullableLong();
						token = reader.GetCurrentJsonToken();
						if (token == JsonToken.ValueSeparator)
						{
							reader.ReadNext(); // ,
							propertyName = reader.ReadPropertyName();
							subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
						}
						break;
					}
					default:
						subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
						break;
				}
			}

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
			long? docCount = null;
			Dictionary<string, IAggregate> nestedAggregates = null;

			while (reader.GetCurrentJsonToken() == JsonToken.ValueSeparator)
			{
				reader.ReadNext(); // ,
				var propertyName = reader.ReadPropertyName();
				if (propertyName == Parser.DocCount)
					docCount = reader.ReadNullableLong();
				else
					nestedAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			}

			return new CompositeBucket(nestedAggregates, key) { DocCount = docCount };
		}

		private IBucket GetSignificantTermsBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver, object key,
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

			return new SignificantTermsBucket<object>(subAggregates)
			{
				Key = key,
				DocCount = docCount.GetValueOrDefault(0),
				BgCount = bgCount,
				Score = score
			};
		}

		private IBucket GetFiltersBucket(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var docCount = reader.ReadNullableLong().GetValueOrDefault(0);
			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
				return new FiltersBucketItem(EmptyReadOnly<string, IAggregate>.Dictionary)
				{
					DocCount = docCount
				};

			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			var subAggregates = GetSubAggregates(ref reader, propertyName, formatterResolver);
			return new FiltersBucketItem(subAggregates)
			{
				DocCount = docCount
			};
		}

		[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
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
			public const string Top = "top";

			public const string FromAsString = "from_as_string";
			public const string Hits = "hits";

			public const string Key = "key";
			public const string KeyAsString = "key_as_string";
			public const string Keys = "keys";
			public const string Location = "location";
			public const string MaxScore = "max_score";
			public const string Meta = "meta";
			public const string Min = "min";
			public const string MinLength = "min_length";

			public const string Score = "score";

			public const string SumOtherDocCount = "sum_other_doc_count";
			public const string To = "to";
			public const string ToAsString = "to_as_string";

			public const string TopLeft = "top_left";

			public const string Total = "total";
			public const string Value = "value";

			public const string ValueAsString = "value_as_string";
			public const string Values = "values";
		}
	}
}
