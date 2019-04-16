using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;

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
		};

		private static readonly byte[] SumOtherDocCount = JsonWriter.GetEncodedPropertyNameWithoutQuotation(Parser.SumOtherDocCount);

		private static readonly AutomataDictionary TopHitsFields = new AutomataDictionary
		{
			{ Parser.Total, 0 },
			{ Parser.MaxScore, 1 },
			{ Parser.Hits, 2 },
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
			reader.ReadIsBeginObjectWithVerify();

			IAggregate aggregate = null;
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
						aggregate = GetPercentilesAggregate(ref reader);
						break;
					case 1:
						aggregate = GetValueAggregate(ref reader, formatterResolver);
						break;
					case 2:
						var dictionaryFormatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
						var afterKeys = dictionaryFormatter.Deserialize(ref reader, formatterResolver);
						reader.ReadNext(); // ,
						propertyName = reader.ReadPropertyNameSegmentRaw();
						var bucketAggregate = propertyName.EqualsBytes(BucketsField)
							? GetMultiBucketAggregate(ref reader, formatterResolver, ref propertyName) as BucketAggregate ?? new BucketAggregate()
							: new BucketAggregate();
						bucketAggregate.AfterKey = afterKeys;
						aggregate = bucketAggregate;
						break;
					case 3:
					case 4:
						aggregate = GetMultiBucketAggregate(ref reader, formatterResolver, ref propertyName);
						break;
					case 5:
						aggregate = GetStatsAggregate(ref reader);
						break;
					case 6:
						aggregate = GetSingleBucketAggregate(ref reader, formatterResolver);
						break;
					case 7:
						aggregate = GetGeoBoundsAggregate(ref reader, formatterResolver);
						break;
					case 8:
						aggregate = GetTopHitsAggregate(ref reader, formatterResolver);
						break;
					case 9:
						aggregate = GetGeoCentroidAggregate(ref reader, formatterResolver);
						break;
					case 10:
						aggregate = GetMatrixStatsAggregate(ref reader, formatterResolver);
						break;
				}
			}
			else
				reader.ReadNextBlock();

			if (aggregate != null)
				aggregate.Meta = meta;

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
			double? maxScore = null;
			HitsTotal total = null;
			List<LazyDocument> topHits = null;

			while (reader.ReadIsInObject(ref count))
			{
				var propertyName = reader.ReadPropertyNameSegmentRaw();
				if (TopHitsFields.TryGetValue(propertyName, out var value))
				{
					switch (value)
					{
						case 0:
							var hitsFormatter = formatterResolver.GetFormatter<HitsTotal>();
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

			reader.ReadNext(); // }
			return new TopHitsAggregate(topHits, formatterResolver)
			{
				Total = total,
				MaxScore = maxScore
			};
		}

		private IAggregate GetGeoCentroidAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var geoLocationFormatter = formatterResolver.GetFormatter<GeoLocation>();
			var geoCentroid = new GeoCentroidAggregate
			{
				Location = geoLocationFormatter.Deserialize(ref reader, formatterResolver)
			};

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
			{
				reader.ReadNext(); // }
				return geoCentroid;
			}

			reader.ReadNext(); // ,

			if (reader.ReadPropertyName() == Parser.Count)
				geoCentroid.Count = reader.ReadInt64();

			reader.ReadNext(); // }
			return geoCentroid;
		}

		private IAggregate GetGeoBoundsAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNext();
				return null;
			}

			var geoBoundsMetric = new GeoBoundsAggregate();
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

			reader.ReadNext(); // }
			return geoBoundsMetric;
		}

		private IAggregate GetPercentilesAggregate(ref JsonReader reader)
		{
			var metric = new PercentilesAggregate();

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
						Percentile = double.Parse(propertyName),
						Value = reader.ReadDouble()
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
						Value = reader.ReadDouble()
					});
					reader.ReadNext(); // }
				}
			}

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
				var propertyName = reader.ReadPropertyNameSegmentRaw();

				if (propertyName.EqualsBytes(BgCountField))
				{
					bgCount = reader.ReadInt64();
					reader.ReadIsValueSeparatorWithVerify();
					propertyName = reader.ReadPropertyNameSegmentRaw();
				}

				if (propertyName.EqualsBytes(FieldsField))
					return GetMatrixStatsAggregate(ref reader, formatterResolver, docCount);

				if (propertyName.EqualsBytes(BucketsField))
				{
					var b = GetMultiBucketAggregate(ref reader, formatterResolver, ref propertyName) as BucketAggregate;
					return new BucketAggregate
					{
						BgCount = bgCount,
						DocCount = docCount,
						Items = b?.Items ?? EmptyReadOnly<IBucket>.Collection
					};
				}

				subAggregates = GetSubAggregates(ref reader, propertyName.Utf8String(), formatterResolver);
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

			var statsMetric = new StatsAggregate
			{
				Average = average,
				Count = count,
				Max = max,
				Min = min,
				Sum = sum
			};

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
			{
				reader.ReadNext(); // }
				return statsMetric;
			}

			reader.ReadNext(); // ,
			var propertyName = reader.ReadPropertyName();
			while (reader.GetCurrentJsonToken() != JsonToken.EndObject && propertyName.Contains(Parser.AsStringSuffix))
			{
				reader.ReadNext(); // <value>
				if (reader.GetCurrentJsonToken() == JsonToken.ValueSeparator)
				{
					reader.ReadNext(); // ,
					propertyName = reader.ReadPropertyName();
				}
			}

			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
			{
				reader.ReadNext(); // }
				return statsMetric;
			}

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

			extendedStatsMetric.SumOfSquares = reader.ReadNullableDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "variance"
			reader.ReadNext(); // :
			extendedStatsMetric.Variance = reader.ReadNullableDouble();
			reader.ReadNext(); // ,
			reader.ReadNext(); // "std_deviation"
			reader.ReadNext(); // :
			extendedStatsMetric.StdDeviation = reader.ReadNullableDouble();

			if (reader.GetCurrentJsonToken() != JsonToken.EndObject)
			{
				var bounds = new StandardDeviationBounds();
				reader.ReadNext(); // ,
				reader.ReadNext(); // "std_deviation_bounds"
				reader.ReadNext(); // :
				reader.ReadNext(); // {
				reader.ReadNext(); // "upper"
				reader.ReadNext(); // :
				bounds.Upper = reader.ReadNullableDouble();
				reader.ReadNext(); // ,
				reader.ReadNext(); // "lower"
				reader.ReadNext(); // :
				bounds.Lower = reader.ReadNullableDouble();
				reader.ReadNext(); // }
				extendedStatsMetric.StdDeviationBounds = bounds;
			}

			// read any remaining _as_string fields
			while (reader.GetCurrentJsonToken() != JsonToken.EndObject)
				reader.ReadNextBlock();

			reader.ReadIsEndObjectWithVerify();
			return extendedStatsMetric;
		}

		private Dictionary<string, IAggregate> GetSubAggregates(ref JsonReader reader, string name, IJsonFormatterResolver formatterResolver)
		{
			var subAggregates = new Dictionary<string, IAggregate>();

			// deserialize the first aggregate
			var aggregate = Deserialize(ref reader, formatterResolver);
			subAggregates.Add(name, aggregate);

			// start at 1 to skip the BeginObject check
			var count = 1;
			while (reader.ReadIsInObject(ref count))
			{
				name = reader.ReadPropertyName();
				aggregate = Deserialize(ref reader, formatterResolver);
				subAggregates.Add(name, aggregate);
			}

			return subAggregates;
		}

		private IAggregate GetMultiBucketAggregate(ref JsonReader reader, IJsonFormatterResolver formatterResolver,
			ref ArraySegment<byte> propertyName
		)
		{
			var bucket = new BucketAggregate();
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
						{
							reader.ReadNext();
							return new ValueAggregate
							{
								Value = value,
								ValueAsString = valueAsString
							};
						}

						reader.ReadNext(); // ,
						propertyName = reader.ReadPropertyNameSegmentRaw();
					}

					if (propertyName.EqualsBytes(KeysField))
					{
						var keyedValueMetric = new KeyedValueAggregate
						{
							Value = value
						};

						var formatter = formatterResolver.GetFormatter<List<string>>();
						keyedValueMetric.Keys = formatter.Deserialize(ref reader, formatterResolver);

						reader.ReadNext(); // }
						return keyedValueMetric;
					}

					// skip any remaining properties for now
					while (token != JsonToken.EndObject)
					{
						reader.ReadNextBlock();
						token = reader.GetCurrentJsonToken();
					}
				}

				reader.ReadNext(); // }
				return new ValueAggregate
				{
					Value = value,
					ValueAsString = valueAsString
				};
			}

			var scriptedMetric = reader.ReadNextBlockSegment();
			reader.ReadNext(); // }
			return new ScriptedMetricAggregate(new LazyDocument(BinaryUtil.ToArray(ref scriptedMetric), formatterResolver));
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
