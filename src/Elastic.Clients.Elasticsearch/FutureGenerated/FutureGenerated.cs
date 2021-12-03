// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading;
using Elastic.Transport;
using System.Text.Json;
using Elastic.Clients.Elasticsearch.QueryDsl;
using System.Text;
using System.Linq;
using System.Collections;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Clients.Elasticsearch.Aggregations;
using System.Linq.Expressions;

namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class Buckets<TBucket>
	{
		public IReadOnlyCollection<TBucket> Items => Item2 is not null ? Item2 : Array.Empty<TBucket>();

		//public Buckets<TBucket> Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		//{
		//	// TODO - This is prototype code and not complete

		//	Type itemOneType, itemTwoType;

		//	itemOneType = GetType().BaseType.GetGenericArguments()[0];
		//	itemTwoType = GetType().BaseType.GetGenericArguments()[1];
			
		//	var item = JsonSerializer.Deserialize(ref reader, itemTwoType, options);

		//	var type = itemTwoType.GetGenericArguments()[0];

		//	return (Buckets<TBucket>)Activator.CreateInstance(typeof(Buckets<>).MakeGenericType(type), item);
		//}
	}



	//internal class BucketsFactory<TBucket> : UnionFactory<Buckets<TBucket>>
	//{
	//	internal Delegate DeserializeDelegate => Deserialize

	//	internal override Buckets<TBucket> Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options)
	//	{
	//		// TODO - This is prototype code and not complete

	//		Type itemOneType, itemTwoType;

	//		itemOneType = GetType().BaseType.GetGenericArguments()[0];
	//		itemTwoType = GetType().BaseType.GetGenericArguments()[1];

	//		var item = JsonSerializer.Deserialize(ref reader, itemTwoType, options);

	//		var type = itemTwoType.GetGenericArguments()[0];

	//		return (Buckets<TBucket>)Activator.CreateInstance(typeof(Buckets<>).MakeGenericType(type), item);
	//	}
	//}

	internal interface IAggregationContainerDescriptor
	{
		string NameValue { get; }
	}

	/// <summary>
	/// Concept only, not yet used
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class AggregationDescriptorBase<T> : DescriptorBase<T>, IAggregationContainerDescriptor where T : DescriptorBase<T>
	{
		string IAggregationContainerDescriptor.NameValue => NameValue;

		internal string? NameValue { get; }

		public AggregationDescriptorBase(string name) => NameValue = name;
	}

	public partial class AggregationContainer
	{
		internal string ContainedVariantName { get; private set; }

		internal object ContainerVariantDescriptorAction { get; private set; }

		internal Action<Utf8JsonWriter, JsonSerializerOptions> SerializeFluent { get; private set; }
		
		internal AggregationContainer(string variant, object descriptorAction)
		{
			ContainedVariantName = variant;
			ContainerVariantDescriptorAction = descriptorAction; 
		}

		private AggregationContainer(string variant) => ContainedVariantName = variant;

		internal static AggregationContainer CreateWithAction<T>(string variantName, Action<T> configure) where T : new()
		{
			var container = new AggregationContainer(variantName);
			container.SetAction(configure);
			return container;
		}

		internal static AggregationContainer CreateWithAction<T>(string variantName, Action<T> configure, T descriptor) where T : new()
		{
			var container = new AggregationContainer(variantName);
			container.SetAction(configure, descriptor);
			return container;
		}

		private void SetAction<T>(Action<T> configure) where T : new()
			=> SerializeFluent = (writer, options) =>
				{
					var descriptor = new T();
					configure(descriptor);
					JsonSerializer.Serialize(writer, descriptor, options);
				};

		private void SetAction<T>(Action<T> configure, T descriptor) where T : new()
			=> SerializeFluent = (writer, options) =>
			{
				//var descriptor = new T();
				configure(descriptor);
				JsonSerializer.Serialize(writer, descriptor, options);
			};

		public static implicit operator AggregationContainer(AggregationBase aggregator)
		{
			if (aggregator == null)
				return null;

			// TODO: Reimplement this fully - as neccesary!

			var container = new AggregationContainer((IAggregationContainerVariant)aggregator)
			{
				Meta = aggregator.Meta
			};

			//aggregator.WrapInContainer(container);

			var bucket = aggregator as BucketAggregationBase;

			//container.Aggregations = bucket?.Aggregations;

			var combinator = aggregator as AggregationCombinator;
			if (combinator?.Aggregations != null)
			{
				var dict = new AggregationDictionary();
				//	foreach (var agg in combinator.Aggregations)
				//		dict.Add(((IAggregation)agg).Name, agg);
				//	container.Aggregations = dict;
			}

			return container;
		}
	}

	public partial class AggregationContainerDescriptor<T> : DescriptorBase<AggregationContainerDescriptor<T>>
	{
		internal AggregationDictionary Aggregations { get; set; }

		private AggregationContainerDescriptor<T> SetContainer(string key, AggregationContainer container)
		{
			if (Self.Aggregations == null)
				Self.Aggregations = new AggregationDictionary();

			Self.Aggregations[key] = container;

			return this;
		}

		public AggregationContainerDescriptor<T> Average(string name, Action<AverageAggregationDescriptor<T>> configure) => SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));

		public AggregationContainerDescriptor<T> WeightedAverage(string name, Action<WeightedAverageAggregationDescriptor<T>> configure) => SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
	}

	public class EmptyTermsAggregate : TermsAggregateBase<EmptyTermsBucket>
	{
	}

	public class EmptyTermsBucket { }

	public class TermsAggregate : TermsAggregateBase<TermsBucket>
	{
	}

	public class TermsBucket : TermsBucketBase
	{
		public object Key { get; init; }
		public string? KeyAsString { get; init; }
	}

	internal static class TermsAggregateSerializationHelper
	{
		private static readonly byte[] s_buckets = Encoding.UTF8.GetBytes("buckets");
		private static readonly byte[] s_key = Encoding.UTF8.GetBytes("key");
		private static readonly byte s_period = (byte)'.';

		public static bool TryDeserialiseTermsAggregate(ref Utf8JsonReader reader, JsonSerializerOptions options, out AggregateBase? aggregate)
		{
			aggregate = null;

			// We take a copy here so we can read forward to establish the term key type before we resume with final deserialisation.
			var readerCopy = reader;

			if (JsonHelper.TryReadUntilStringPropertyValue(ref readerCopy, s_buckets))
			{
				if (readerCopy.TokenType != JsonTokenType.StartArray)
					throw new Exception("TODO");

				readerCopy.Read();

				if (readerCopy.TokenType == JsonTokenType.EndArray) // We have no buckets
				{
					var agg = JsonSerializer.Deserialize<EmptyTermsAggregate>(ref reader, options);
					aggregate = agg;
					return true;
				}
				else
				{
					if (readerCopy.TokenType != JsonTokenType.StartObject)
						throw new Exception("TODO"); // TODO!

					if (JsonHelper.TryReadUntilStringPropertyValue(ref readerCopy, s_key))
					{
						if (readerCopy.TokenType == JsonTokenType.String)
						{
							var agg = JsonSerializer.Deserialize<StringTermsAggregate>(ref reader, options);
							aggregate = agg;
							return true;
						}
						else if (readerCopy.TokenType == JsonTokenType.Number)
						{
							var value = readerCopy.ValueSpan; // TODO - May need to check for sequence

							if (value.IndexOf(s_period) > -1 && readerCopy.TryGetDouble(out _))
							{
								var agg = JsonSerializer.Deserialize<DoubleTermsAggregate>(ref reader, options);
								aggregate = agg;
								return true;
							}
							else if (readerCopy.TryGetInt64(out _))
							{
								var agg = JsonSerializer.Deserialize<LongTermsAggregate>(ref reader, options);
								aggregate = agg;
								return true;
							}
						}
						else if (readerCopy.TokenType == JsonTokenType.StartArray)
						{
							var agg = JsonSerializer.Deserialize<MultiTermsAggregate>(ref reader, options);
							aggregate = agg;
							return true;
						}
						else
						{
							throw new JsonException("Unhandled token type when parsing the terms aggregate response");
						}
					}
				}
			}

			return false;
		}
	}

	internal sealed class AggregateDictionaryConverter : JsonConverter<AggregateDictionary>
	{
		public override AggregateDictionary? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var dictionary = new Dictionary<string, AggregateBase>();

			if (reader.TokenType != JsonTokenType.StartObject)
				return new AggregateDictionary(dictionary);

			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					break;

				var name = reader.GetString(); // TODO: Future optimisation, get raw bytes span and parse based on those

				reader.Read();

				var nameParts = name.Split('#');

				if (nameParts.Length != 2)
					throw new JsonException("Unable to parse typed-key from aggregation name");

				// Bucket-based Aggregates

				if (nameParts[0] == "terms")
				{
					if (TermsAggregateSerializationHelper.TryDeserialiseTermsAggregate(ref reader, options, out var agg))
					{
						dictionary.Add(nameParts[1], agg);
					}
				}
				else if (nameParts[0] == "adjacency_matrix")
				{
					var agg = JsonSerializer.Deserialize<AdjacencyMatrixAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "auto_date_histogram")
				{
					var agg = JsonSerializer.Deserialize<AutoDateHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "children")
				{
					var agg = JsonSerializer.Deserialize<ChildrenAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "composite")
				{
					var agg = JsonSerializer.Deserialize<CompositeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "date_histogram")
				{
					var agg = JsonSerializer.Deserialize<DateHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "date_range")
				{
					var agg = JsonSerializer.Deserialize<DateRangeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "diversified_sampler")
				{
					// TODO
				}
				else if (nameParts[0] == "filter")
				{
					var agg = JsonSerializer.Deserialize<FilterAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "filters")
				{
					var agg = JsonSerializer.Deserialize<FiltersAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "geo_distance")
				{
					var agg = JsonSerializer.Deserialize<GeoDistanceAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "geohash_grid")
				{
					var agg = JsonSerializer.Deserialize<GeoHashGridAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "geotile_grid")
				{
					var agg = JsonSerializer.Deserialize<GeoTileGridAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "global")
				{
					var agg = JsonSerializer.Deserialize<GlobalAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "histogram")
				{
					var agg = JsonSerializer.Deserialize<HistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "ip_range")
				{
					var agg = JsonSerializer.Deserialize<IpRangeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "missing")
				{
					var agg = JsonSerializer.Deserialize<MissingAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "multi-terms")
				{
					var agg = JsonSerializer.Deserialize<MultiTermsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "nested")
				{
					var agg = JsonSerializer.Deserialize<NestedAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "parent")
				{
					// TODO
				}
				else if (nameParts[0] == "range")
				{
					var agg = JsonSerializer.Deserialize<RangeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "rare_terms")
				{
					// TODO
				}
				else if (nameParts[0] == "reverse_nested")
				{
					var agg = JsonSerializer.Deserialize<ReverseNestedAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "sampler")
				{
					var agg = JsonSerializer.Deserialize<SamplerAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "significant_terms")
				{
					// TODO - As per terms
				}
				else if (nameParts[0] == "significant_text")
				{
					// TODO
					throw new Exception("The aggregate in response is not yet supported");
				}
				else if (nameParts[0] == "variable_width_histogram")
				{
					var agg = JsonSerializer.Deserialize<VariableWidthHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "variable_width_histogram")
				{
					var agg = JsonSerializer.Deserialize<VariableWidthHistogramAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}

				// Metrics-based Aggregates

				else if (nameParts[0] == "avg")
				{
					var agg = JsonSerializer.Deserialize<AvgAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "boxplot")
				{
					var agg = JsonSerializer.Deserialize<BoxPlotAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "cardinality")
				{
					var agg = JsonSerializer.Deserialize<CardinalityAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "extended_stats")
				{
					var agg = JsonSerializer.Deserialize<ExtendedStatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "geo_bounds")
				{
					var agg = JsonSerializer.Deserialize<GeoBoundsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "geo_centroid")
				{
					var agg = JsonSerializer.Deserialize<GeoCentroidAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "geo_line")
				{
					var agg = JsonSerializer.Deserialize<GeoLineAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "matrix_stats")
				{
					var agg = JsonSerializer.Deserialize<MatrixStatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "max")
				{
					var agg = JsonSerializer.Deserialize<MaxAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "median_absolute_deviation")
				{
					var agg = JsonSerializer.Deserialize<MedianAbsoluteDeviationAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "min")
				{
					var agg = JsonSerializer.Deserialize<MinAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "percentile_ranks")
				{
					// TODO
				}
				else if (nameParts[0] == "percentiles")
				{
					// TODO
				}
				else if (nameParts[0] == "rate")
				{
					var agg = JsonSerializer.Deserialize<RateAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "scripted_metric")
				{
					var agg = JsonSerializer.Deserialize<ScriptedMetricAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "stats")
				{
					var agg = JsonSerializer.Deserialize<StatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "string_stats")
				{
					var agg = JsonSerializer.Deserialize<StringStatsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "sum")
				{
					var agg = JsonSerializer.Deserialize<SumAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "t_test")
				{
					var agg = JsonSerializer.Deserialize<TTestAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "top_hits")
				{
					var agg = JsonSerializer.Deserialize<TopHitsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "top_metrics")
				{
					var agg = JsonSerializer.Deserialize<TopMetricsAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "value_count")
				{
					var agg = JsonSerializer.Deserialize<ValueCountAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "weighted_avg")
				{
					var agg = JsonSerializer.Deserialize<WeightedAvgAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}

				// Pipeline-based Aggregates

				else if (nameParts[0] == "avg_bucket")
				{
					// TODO
				}
				else if (nameParts[0] == "bucket_script")
				{
					// TODO
				}
				else if (nameParts[0] == "bucket_count_ks_test")
				{
					// TODO
				}
				else if (nameParts[0] == "bucket_correlation")
				{
					// TODO
				}
				else if (nameParts[0] == "bucket_selector")
				{
					// TODO
				}
				else if (nameParts[0] == "bucket_sort")
				{
					// TODO
				}
				else if (nameParts[0] == "cumulative_cardinality")
				{
					var agg = JsonSerializer.Deserialize<CumulativeCardinalityAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "cumulative_sum")
				{
					// TODO
				}
				else if (nameParts[0] == "derivative")
				{
					var agg = JsonSerializer.Deserialize<DerivativeAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "extended_stats_bucket")
				{
					var agg = JsonSerializer.Deserialize<ExtendedStatsBucketAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "inference")
				{
					var agg = JsonSerializer.Deserialize<InferenceAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "max_bucket")
				{
					// TODO
				}
				else if (nameParts[0] == "min_bucket")
				{
					// TODO
				}
				else if (nameParts[0] == "moving_avg")
				{
					// TODO
				}
				else if (nameParts[0] == "moving_fn")
				{
					// TODO
				}
				else if (nameParts[0] == "moving_percentiles")
				{
					// TODO
				}
				else if (nameParts[0] == "normalize")
				{
					// TODO
				}
				else if (nameParts[0] == "percentiles_bucket")
				{
					var agg = JsonSerializer.Deserialize<PercentilesBucketAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "serial_diff")
				{
					// TODO
				}
				else if (nameParts[0] == "stats_bucket")
				{
					var agg = JsonSerializer.Deserialize<StatsBucketAggregate>(ref reader, options);
					dictionary.Add(nameParts[1], agg);
				}
				else if (nameParts[0] == "sum_bucket")
				{
					// TODO
				}
			}

			return new AggregateDictionary(dictionary);
		}

		public override void Write(Utf8JsonWriter writer, AggregateDictionary value, JsonSerializerOptions options) => throw new NotImplementedException();
	}

	public partial class AggregateDictionary
	{
		public EmptyTermsAggregate? EmptyTerms(string key) => TryGet<EmptyTermsAggregate?>(key);

		public bool IsEmptyTerms(string key) => !BackingDictionary.TryGetValue(key, out var agg) || agg is EmptyTermsAggregate;

		public bool TryGetStringTerms(string key, out StringTermsAggregate? aggregate)
		{
			aggregate = null;

			if (BackingDictionary.TryGetValue(key, out var agg) && agg is StringTermsAggregate stringTermsAgg)
			{
				aggregate = stringTermsAgg;
				return true;
			}

			return false;
		}

		public Elastic.Clients.Elasticsearch.Aggregations.AvgAggregate? Average(string key) => TryGet<Elastic.Clients.Elasticsearch.Aggregations.AvgAggregate?>(key);

		public TermsAggregate Terms(string key)
		{
			if (!BackingDictionary.TryGetValue(key, out var agg))
			{
				return null;
			}

			switch (agg)
			{
				case EmptyTermsAggregate empty:
					return new TermsAggregate
					{
						Buckets = new Buckets<TermsBucket>(Array.Empty<TermsBucket>().ToReadOnlyCollection()),
						Meta = empty.Meta,
						DocCountErrorUpperBound = empty.DocCountErrorUpperBound,
						SumOtherDocCount = empty.SumOtherDocCount
					};
				case StringTermsAggregate stringTerms:
					var buckets = stringTerms.Buckets.Items.Select(b => new TermsBucket { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = b.Key, KeyAsString = b.Key }).ToReadOnlyCollection();
					return new TermsAggregate
					{
						Buckets = new Buckets<TermsBucket>(buckets),
						Meta = stringTerms.Meta,
						DocCountErrorUpperBound = stringTerms.DocCountErrorUpperBound,
						SumOtherDocCount = stringTerms.SumOtherDocCount
					};
				case DoubleTermsAggregate doubleTerms:
					var doubleTermsBuckets = doubleTerms.Buckets.Items.Select(b => new TermsBucket { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = b.Key, KeyAsString = b.Key.ToString() }).ToReadOnlyCollection();
					return new TermsAggregate
					{
						Buckets = new Buckets<TermsBucket>(doubleTermsBuckets),
						Meta = doubleTerms.Meta,
						DocCountErrorUpperBound = doubleTerms.DocCountErrorUpperBound,
						SumOtherDocCount = doubleTerms.SumOtherDocCount
					};
				case LongTermsAggregate longTerms:
					var longTermsBuckets = longTerms.Buckets.Items.Select(b => new TermsBucket { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = b.Key, KeyAsString = b.Key.ToString() }).ToReadOnlyCollection();
					return new TermsAggregate
					{
						Buckets = new Buckets<TermsBucket>(longTermsBuckets),
						Meta = longTerms.Meta,
						DocCountErrorUpperBound = longTerms.DocCountErrorUpperBound,
						SumOtherDocCount = longTerms.SumOtherDocCount
					};

				// TODO - Multi-terms
			}

			return null;
		}
	}
}

namespace Elastic.Clients.Elasticsearch
{
	public enum FieldType
	{
		// TODO: Generate this
	}

	public partial struct WaitForActiveShards : IStringable
	{
		public static WaitForActiveShards All = new("all");

		public WaitForActiveShards(string value) => Value = value;

		public string Value { get; }

		public static implicit operator WaitForActiveShards(int v) => new(v.ToString());
		public static implicit operator WaitForActiveShards(string v) => new(v);

		public string GetString() => Value ?? string.Empty;
	}

	// COULD ALSO BE AN ENUM AS IN EXISTING NEST?
	public partial struct Refresh : IStringable
	{
		public static Refresh WaitFor = new("wait_for");
		public static Refresh True = new("true");
		public static Refresh False = new("false");

		public Refresh(string value) => Value = value;

		public string Value { get; }

		public string GetString() => Value ?? string.Empty;
	}

	public partial class InlineScript
	{
		public InlineScript(string source) => Source = source;
	}

	public partial class Script
	{
		public static implicit operator Script(InlineScript inlineScript) => new (inlineScript);
	}

	public class DocType { }

	public partial interface IElasticClient
	{
		DeleteResponse Delete<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest);

		Task<DeleteResponse> DeleteAsync<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default);

		CreateResponse Create<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest);

		Task<CreateResponse> CreateAsync<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default);

		IndexResponse Index<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest);

		Task<IndexResponse> IndexAsync<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default);

		Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null, CancellationToken cancellationToken = default);

		UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null);
	}

	public partial class ElasticClient
	{
		public IndexResponse Index<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new IndexRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<IndexRequestDescriptor<TDocument>, IndexResponse>(descriptor);
		}

		public Task<IndexResponse> IndexAsync<TDocument>(TDocument document, Action<IndexRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new IndexRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<IndexRequestDescriptor<TDocument>, IndexResponse>(descriptor);
		}

		public CreateResponse Create<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public Task<CreateResponse> CreateAsync<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(documentWithId: document);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public DeleteResponse Delete<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest)
		{
			var descriptor = new DeleteRequestDescriptor<TDocument>(id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<DeleteRequestDescriptor<TDocument>, DeleteResponse>(descriptor);
		}

		public Task<DeleteResponse> DeleteAsync<TDocument>(Id id, Action<DeleteRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new DeleteRequestDescriptor<TDocument>(id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<DeleteRequestDescriptor<TDocument>, DeleteResponse>(descriptor);
		}

		public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequestAsync<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}

		public UpdateResponse<TDocument> Update<TDocument, TPartialDocument>(IndexName index, Id id, Action<UpdateRequestDescriptor<TDocument, TPartialDocument>> configureRequest = null)
		{
			var descriptor = new UpdateRequestDescriptor<TDocument, TPartialDocument>(index, id);
			configureRequest?.Invoke(descriptor);
			return DoRequest<UpdateRequestDescriptor<TDocument, TPartialDocument>, UpdateResponse<TDocument>>(descriptor);
		}
	}

	public sealed partial class DeleteRequestDescriptor<TDocument> : RequestDescriptorBase<DeleteRequestDescriptor<TDocument>, DeleteRequestParameters>
	{
		public DeleteRequestDescriptor(IndexName index, Id id) : base(r => r.Required("index", index).Required("id", id))
		{
		}

		public DeleteRequestDescriptor(Id id) : this(typeof(TDocument), id)
		{
		}

		public DeleteRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }

		internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceDelete;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;
		protected override bool SupportsBody => false;
		public DeleteRequestDescriptor<TDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
		public DeleteRequestDescriptor<TDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
		public DeleteRequestDescriptor<TDocument> Refresh(Refresh? refresh) => Qs("refresh", refresh);
		public DeleteRequestDescriptor<TDocument> Routing(string? routing) => Qs("routing", routing);
		public DeleteRequestDescriptor<TDocument> Timeout(Time? timeout) => Qs("timeout", timeout);
		public DeleteRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
		public DeleteRequestDescriptor<TDocument> VersionType(VersionType? versionType) => Qs("version_type", versionType);
		public DeleteRequestDescriptor<TDocument> WaitForActiveShards(WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

		public DeleteRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => throw new NotImplementedException();
	}

	//public sealed partial class CountRequestDescriptor
	//{
	//	//public CountRequestDescriptor Query(Action<QueryContainerDescriptor> configureContainer) => Assign(query, (a, v) => a._query = v);
	//}

	public sealed partial class CreateRequest<TDocument>
	{

		public CreateRequest(Id id) : this(typeof(TDocument), id)
		{
		}

		public CreateRequest(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) => Document = documentWithId;
	}

	public sealed partial class CreateRequestDescriptor<TDocument> : ICustomJsonWriter
	{
		public CreateRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Elasticsearch.Id.From(documentWithId)) => DocumentFromPath(documentWithId);

		private void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);

		public CreateRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));

		public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(DocumentValue, writer, sourceSerializer);

		// TODO: We should be able to generate these for optional params
		public CreateRequestDescriptor<TDocument> Id(Id id)
		{
			RouteValues.Optional("id", id);
			return this;
		}
	}

	public sealed partial class CreateRequestDescriptor<TDocument>
	{
		// TODO: Codegen
		public CreateRequestDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
	}

	public sealed partial class UpdateRequestDescriptor<TDocument, TPartialDocument>
	{
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> PartialDocument(TPartialDocument document) => this;  // TODO
	}

	public sealed partial class DeleteRequest<TDocument> : DeleteRequest
	{
		public DeleteRequest(IndexName index, Id id) : base(index, id) { }

		public DeleteRequest(Id id) : this(typeof(TDocument), id)
		{
		}

		public DeleteRequest(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }
	}

	public partial class SearchRequest
	{
		internal override void BeforeRequest()
		{
			if (Aggregations is not null)
			{
				TypedKeys = true;
			}
		}
	}

	public sealed partial class SearchRequestDescriptor<T>
	{
		internal Type ClrType => typeof(T);

		internal Action<AggregationContainerDescriptor<T>> AggregationsAction { get; private set; }

		public SearchRequestDescriptor<T> Aggregations(Action<AggregationContainerDescriptor<T>>? configure) => Assign(configure, (a, v) => a.AggregationsAction = v);

		public SearchRequestDescriptor<T> Index(Indices index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));

		internal override void BeforeRequest()
		{
			if (AggregationsValue is not null || AggregationsAction is not null)
			{
				TypedKeys(true);
			}
		}

		protected override string ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings) =>
			//if (Self.PointInTime is object && !string.IsNullOrEmpty(Self.PointInTime.Id) && routeValues.ContainsKey("index"))
			//{
			//	routeValues.Remove("index");
			//}

			base.ResolveUrl(routeValues, settings);

		//internal AggregationContainerDescriptor<T> AggregationContainerDescriptor { get; private set; }
		//internal Action<AggregationContainerDescriptor<T>> AggregationContainerDescriptorAction { get; private set; }

		//public SearchRequestDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> configure)
		//{
		//	var container = configure?.Invoke(new QueryContainerDescriptor<T>());
		//	return Assign(container, (a, v) => a.QueryValue = v);
		//}

		//public SearchRequestDescriptor<T> Aggregations(Action<AggregationContainerDescriptor<T>> configure)
		//{
		//	return Assign(configure, (a, v) => a.AggregationContainerDescriptorAction = v);
		//}

		//public SearchRequestDescriptor<T> Aggregations(AggregationContainerDescriptor<T> configure)
		//{
		//	var descriptor = new AggregationContainerDescriptor<T>();
		//	configure?.Invoke(descriptor);
		//	return Assign(descriptor, (a, v) => a.AggregationContainerDescriptor = v);
		//}

		private partial void AfterStartObject(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (AggregationsAction is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, new AggregationContainerDescriptor<T>(AggregationsAction), options);
			}
		}
	}

	public sealed partial class CountRequestDescriptor<T>
	{
		public CountRequestDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> configure)
		{
			var container = configure?.Invoke(new QueryContainerDescriptor<T>());
			return Assign(container, (a, v) => a.QueryValue = v);
		}
	}
	public partial class SearchRequest<TInferDocument>
	{
		public SearchRequest(Indices? indices) : base(indices)
		{
		}
	}
}

namespace Elastic.Clients.Elasticsearch.Analysis
{
	// TODO: Generator should handle these

	//public sealed partial class ShingleTokenFilterDescriptor : ITokenFilterDefinitionsVariant { }

	//public sealed partial class TokenFiltersDescriptor : IsADictionaryDescriptorBase<TokenFiltersDescriptor, TokenFilters, string, ITokenFilterDefinitionsVariant>
	//{
	//	public TokenFiltersDescriptor() : base(new TokenFilters()) { }

	//	public TokenFiltersDescriptor UserDefined(string name, ITokenFilterDefinitionsVariant analyzer) => Assign(name, analyzer);

	//	public TokenFiltersDescriptor Shingle(string name, Action<ShingleTokenFilterDescriptor> configure)
	//	{
	//		var descriptor = new ShingleTokenFilterDescriptor();
	//		configure?.Invoke(descriptor);
	//		return Assign(name, descriptor);
	//	}
	//}

	// TODO: IndexRequestDescriptorConverter - As per https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-converters-how-to?pivots=dotnet-5-0#sample-factory-pattern-converter
}

namespace Elastic.Clients.Elasticsearch.IndexManagement
{
	//public sealed partial class IndexSettingsAnalysisDescriptor
	//{
	//	internal TokenFilters _tokenFilters;

	//	public IndexSettingsAnalysisDescriptor TokenFilters(Func<TokenFiltersDescriptor, IPromise<TokenFilters>> selector) =>
	//		Assign(selector, (a, v) => _tokenFilters = v?.Invoke(new TokenFiltersDescriptor())?.Value);
	//}
}

namespace Elastic.Clients.Elasticsearch.Aggregations
{
	
}

//TERM QUERY

//internal override TermQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//{
//	if (reader.TokenType != JsonTokenType.StartObject)
//		throw new JsonException("TODO");

//	var termQuery = new TermQuery();

//	while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
//	{
//		if (reader.TokenType == JsonTokenType.PropertyName)
//		{
//			var property = reader.GetString();

//			if (property == "value")
//			{
//				termQuery.Value = JsonSerializer.Deserialize<object>(ref reader, options);
//				continue;
//			}

//			if (property == "case_insensitive")
//			{
//				termQuery.CaseInsensitive = reader.GetBoolean();
//				continue;
//			}
//		}
//	}

//	return termQuery;
//}

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public partial class TermQuery
	{
		public static implicit operator QueryContainer(TermQuery termQuery) => new QueryContainer(termQuery);
	}

		//public sealed partial class BoolQueryDescriptor
		//{
		//	internal BoolQuery ToQuery()
		//	{
		//		var query = new BoolQuery();

		//		if (_filter is not null)
		//			query.Filter = _filter;

		//		// TODO - More

		//		return query;
		//	}
		//}

		//public sealed partial class MatchQueryDescriptor
		//{
		//	public MatchQueryDescriptor Query(string query) => Assign(query, (a, v) => a._query = v);

		//	internal MatchQuery ToQuery()
		//	{
		//		var query = new MatchQuery();

		//		if (_field is not null)
		//			query.Field = _field;

		//		if (_query is not null)
		//			query.Query = _query;

		//		return query;
		//	}
		//}

		//internal sealed class MatchQueryConverter : FieldNameQueryConverterBase<MatchQuery>
		//{
		//	internal override MatchQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		//	{
		//		if (reader.TokenType != JsonTokenType.StartObject)
		//		{
		//			throw new JsonException();
		//		}

		//		string queryValue = default;

		//		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		//		{
		//			var property = reader.GetString();

		//			if (property == "query")
		//			{
		//				reader.Read();
		//				queryValue = reader.GetString();
		//			}
		//		}

		//		var query = new MatchQuery()
		//		{
		//			Query = queryValue
		//		};

		//		return query;
		//	}

		//	internal override void WriteInternal(Utf8JsonWriter writer, MatchQuery value, JsonSerializerOptions options)
		//	{
		//		writer.WriteStartObject();
		//		if (!string.IsNullOrEmpty(value.Query))
		//		{
		//			writer.WritePropertyName("query");
		//			writer.WriteStringValue(value.Query);
		//		}
		//		writer.WriteEndObject();
		//	}
		//}

		//internal sealed class TermQueryConverter : FieldNameQueryConverterBase<TermQuery>
		//{
		//	internal override TermQuery ReadInternal(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();

		//	internal override void WriteInternal(Utf8JsonWriter writer, TermQuery value, JsonSerializerOptions options)
		//	{
		//		writer.WriteStartObject();
		//		if (value.Value is not null)
		//		{
		//			writer.WritePropertyName("value");
		//			JsonSerializer.Serialize(writer, value.Value, options);
		//		}
		//		writer.WriteEndObject();
		//	}
		//}

		public sealed partial class QueryContainerDescriptor<T>
	{
		public void MatchAll() => Set(new MatchAllQuery(), "match_all");

		public void Term<TValue>(Expression<Func<T, TValue>> field, object value, double? boost = null, string name = null) =>
			Term(t => t.Field(field).Value(value)/*.Boost(boost)*/.Name(name));
	}


	//public sealed partial class QueryContainerDescriptor
	//{
	//	//public QueryContainerDescriptor QueryString(Action<BoolQueryDescriptor> configure)
	//	//{
	//	//	var descriptor = new BoolQueryDescriptor();
	//	//	configure?.Invoke(descriptor);
	//	//	return Assign(descriptor, (d, v) => d._boolQueryDescriptor = v);
	//	//}

	//	//internal QueryContainerDescriptor(Action<QueryContainerDescriptor> configure) => configure.Invoke(this);

	//	//public void Bool(Action<BoolQueryDescriptor> configure) => Set((Action<IQueryContainerVariantDescriptor>)configure, "bool");
	//	//{
	//		//if (_containsVariant)
	//		//	throw new Exception("TODO");

	//		//QueryContainerDescriptorAction = (Action<IQueryContainerVariantDescriptor>)configure;

	//		//_containedVariant = "bool";
	//		//_containsVariant = true;

	//		//if (configure is null)
	//		//	return new QueryContainer(new BoolQuery());

	//		//var descriptor = new BoolQueryDescriptor();
	//		//configure.Invoke(descriptor);
	//		//Assign(descriptor, (d, v) => d._variantDescriptor = v);

	//		//return ToQueryContainer();
	//	//}

	//	//private void Set(object descriptorAction, string variantName)
	//	//{
	//	//	if (ContainsVariant)
	//	//		throw new Exception("TODO");

	//	//	ContainerVariantDescriptorAction = descriptorAction;

	//	//	ContainedVariantName = variantName;
	//	//	ContainsVariant = true;
	//	//}

	//	//private void Set(IQueryContainerVariant variant, string variantName)
	//	//{
	//	//	if (ContainsVariant)
	//	//		throw new Exception("TODO");

	//	//	Container = new QueryContainer(variant);

	//	//	ContainedVariantName = variantName;
	//	//	ContainsVariant = true;
	//	//}

	//	//internal QueryContainer ToQueryContainer()
	//	//{
	//	//	if (!_containsQuery)
	//	//		throw new Exception("TODO");

	//	//	if (ContainedVariant is not null)
	//	//		return ContainedVariant;

	//	//	if (_descriptorType == "bool")
	//	//	{
	//	//		var descriptor = new BoolQueryDescriptor();
	//	//		QueryContainerDescriptorAction.Invoke(descriptor);

	//	//	}

	//	//	ContainedVariant = _descriptorType switch
	//	//	{
	//	//		"bool" => new QueryContainer(variant.ToQuery()),
	//	//		MatchQueryDescriptor variant => new QueryContainer(variant.ToQuery()),
	//	//		_ => null,
	//	//	};

	//	//	return ContainedVariant;
	//	//}
	//}
}


namespace Elastic.Clients.Elasticsearch
{
	public class MyRequest
	{
		public string? Something { get; set; }

		public MyRequest WrappedRequest { get; set; }

		public Thing Thing { get; set; }
	}

	public class Thing
	{
		public string Name { get; set; }
	}

	// Main downside is we always allocate a MyRequest per descriptor instead of relying on casting the descriptor to
	// the interface.
	public class MyRequestDescriptor : TestDescriptorBase<MyRequestDescriptor, MyRequest>
	{
		public MyRequestDescriptor() : base(new MyRequest()) { }

		public MyRequest Request => Target;

		public MyRequestDescriptor Something(string? something) => Assign(something, (r, v) => r.Something = v);

		public MyRequestDescriptor WrappedRequest(MyRequest request) => Assign(request, (r, v) => r.WrappedRequest = v);

		// Could be added to the partial class by hand at a later date if we then determine a descriptor is needed.
		public MyRequestDescriptor WrappedRequest(Action<MyRequestDescriptor> anotherOne)
		{
			var descriptor = new MyRequestDescriptor();
			anotherOne.Invoke(descriptor);
			Target.WrappedRequest = descriptor.Request;
			return this;
		}

		public MyRequestDescriptor Thing(Thing thing)
		{
			Target.Thing = thing;
			return this;
		}

		public MyRequestDescriptor Thing(Action<ThingDescriptor> thing)
		{
			var descriptor = new ThingDescriptor();
			thing.Invoke(descriptor);
			Target.Thing = descriptor.Thing;
			return this;
		}
	}

	public class ThingDescriptor : TestDescriptorBase<ThingDescriptor, Thing>
	{
		public ThingDescriptor() : base(new Thing()) { }

		public Thing Thing => Target;

		public ThingDescriptor Something(string name) => Assign(name, (r, v) => r.Name = v);

		// We could prefer no base class to optimise which avoids the extra field required for _self.
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected ThingDescriptor LocalAssign<TValue>(TValue value, Action<Thing, TValue> assigner)
		{
			assigner(Target, value);
			return this;
		}
	}

	public abstract class TestDescriptorBase<TDescriptor, TTarget> where TDescriptor : TestDescriptorBase<TDescriptor, TTarget>
	{
		private readonly TDescriptor _self;

		protected TestDescriptorBase(TTarget target)
		{
			Target = target;
			_self = (TDescriptor)this;
		}

		protected TTarget Target { get; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected TDescriptor Assign<TValue>(TValue value, Action<TTarget, TValue> assigner)
		{
			assigner(Target, value);
			return _self;
		}
	}

	public class MyClient
	{
		public void DoRequest(IndexName index, Action<MyRequestDescriptor> selector)
		{
			var descriptor = new MyRequestDescriptor();
			selector.Invoke(descriptor);
			var request = descriptor.Request;

			// SEND IT
		}

		public void DoRequest(IndexName index, MyRequest request)
		{
			// SEND IT
		}

		public void DoRequest(IndexName index, MyRequestDescriptor descriptor)
		{
			var request = descriptor.Request;

			// SEND IT
		}
	}

	public class Testing
	{
		public void DoStuff()
		{
			var client = new MyClient();

			client.DoRequest("index", new MyRequest { Something = "Thing" });

			client.DoRequest("index", a => a
				.Something("MainSomething")
				.WrappedRequest(r => r.Something("AnotherSomething"))
				.Thing(t => t.Something("Name")));
		}
	}

	//public readonly partial struct PropertyName : IDictionaryKey
	//{
	//	public string Key => Value;
	//}

	//// This is an incomplete stub implementation and should really be a struct
	//public partial class Indices : IUrlParameter
	//{
	//	public static readonly Indices All = new("_all");

	//	internal Indices(IndexName index) => _indexNameList.Add(index);

	//	public Indices(IEnumerable<IndexName> indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));
	//		_indexNameList.AddRange(indices);
	//	}

	//	public Indices(IEnumerable<string> indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));
	//		_indexNameList.AddRange(indices.Select(s => (IndexName)s));
	//	}

	//	public IReadOnlyCollection<IndexName> Values => _indexNameList.ToArray();

	//	public static Indices Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new Indices(list);

	//	public static Indices Single(string index) => new Indices((IndexName)index);

	//	public static implicit operator Indices(string names) => Parse(names);

	//	string IUrlParameter.GetString(ITransportConfiguration settings)
	//	{
	//		if (settings is not IElasticsearchClientSettings elasticsearchClientSettings)
	//			throw new Exception(
	//				"Tried to pass index names on query sting but it could not be resolved because no Elastic.Clients.Elasticsearch settings are available.");

	//		var indices = _indexNameList.Select(i => i.GetString(settings)).Distinct();

	//		return string.Join(",", indices);
	//	}
	//}

	//public partial struct IndicesList : IUrlParameter
	//{
	//	//public static readonly IndicesList All = new("_all");

	//	private readonly List<IndexName> _indices = new();

	//	internal IndicesList(IndexName index) => _indices.Add(index);

	//	public IndicesList(IEnumerable<IndexName> indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));

	//		// De-duplicating during creation avoids cost when accessing the values.
	//		foreach (var index in indices)
	//			if (!_indices.Contains(index))
	//				_indices.Add(index);
	//	}

	//	public IndicesList(string[] indices)
	//	{
	//		indices.ThrowIfEmpty(nameof(indices));

	//		foreach (var index in indices)
	//			if (!_indices.Contains(index))
	//				_indices.Add(index);
	//	}

	//	public IReadOnlyCollection<IndexName> Values => _indices;

	//	public static IndicesList Parse(string names) => names.IsNullOrEmptyCommaSeparatedList(out var list) ? null : new IndicesList(list);

	//	public static implicit operator IndicesList(string names) => Parse(names);
	//}

	//public partial struct IndicesList { string IUrlParameter.GetString(ITransportConfiguration settings) => ""; }


	public abstract partial class PlainRequestBase<TParameters>
	{
		///<summary>Include the stack trace of returned errors.</summary>
		[JsonIgnore]
		public bool? ErrorTrace
		{
			get => Q<bool?>("error_trace");
			set => Q("error_trace", value);
		}

		/// <summary>
		///     A comma-separated list of filters used to reduce the response.
		///     <para>
		///         Use of response filtering can result in a response from Elasticsearch
		///         that cannot be correctly deserialized to the respective response type for the request.
		///         In such situations, use the low level client to issue the request and handle response deserialization.
		///     </para>
		/// </summary>
		[JsonIgnore]
		public string[] FilterPath
		{
			get => Q<string[]>("filter_path");
			set => Q("filter_path", value);
		}

		///<summary>Return human readable values for statistics.</summary>
		[JsonIgnore]
		public bool? Human
		{
			get => Q<bool?>("human");
			set => Q("human", value);
		}

		///<summary>Pretty format the returned JSON response.</summary>
		[JsonIgnore]
		public bool? Pretty
		{
			get => Q<bool?>("pretty");
			set => Q("pretty", value);
		}

		/// <summary>
		///     The URL-encoded request definition. Useful for libraries that do not accept a request body for non-POST
		///     requests.
		/// </summary>
		[JsonIgnore]
		public string SourceQueryString
		{
			get => Q<string>("source");
			set => Q("source", value);
		}
	}
}

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	//public partial class Like
	//{

	//}

	//public partial class QueryContainerDescriptor : DescriptorBase<QueryContainerDescriptor, IQueryContainer>, IQueryContainer
	//{
	//	private QueryContainer WrapInContainer<TQuery, TQueryInterface>(
	//		Func<TQuery, TQueryInterface> create
	//	)
	//		where TQuery : class, TQueryInterface, new()
	//		where TQueryInterface : class
	//	{
	//		// Invoke the create delegate before assigning container; the create delegate
	//		// may mutate the current QueryContainerDescriptor<T> instance such that it
	//		// contains a query. See https://github.com/elastic/elasticsearch-net/issues/2875
	//		var query = create.InvokeOrDefault(new TQuery());

	//		if (query is not IQueryContainerVariant variant)
	//		{
	//			throw new Exception();
	//		}

	//		return variant.ToQueryContainer();

	//		//var container = ContainedQuery == null
	//		//	? this
	//		//	: new QueryContainerDescriptor();

	//		////c.IsVerbatim = query.IsVerbatim;
	//		////c.IsStrict = query.IsStrict;

	//		//assign(query, container);
	//		//container.ContainedQuery = query;

	//		//return container;

	//		//if query is writable (not conditionless or verbatim): return a container that holds the query
	//		//if (query.IsWritable)
	//		//	return container;

	//		//query is conditionless but marked as strict, throw exception
	//		//if (query.IsStrict)
	//		//	throw new ArgumentException("Query is conditionless but strict is turned on");

	//		//query is conditionless return an empty container that can later be rewritten
	//		//return null;
	//	}

	//	//[JsonIgnore]
	//	//internal IQuery ContainedQuery { get; set; }

	//	public QueryContainer QueryString(Func<QueryStringQueryDescriptor, IQueryStringQuery> selector) =>
	//		WrapInContainer(selector);
	//}

	//public partial class QueryStringQueryDescriptor : IQueryContainerVariant
	//{
	//	string IUnionVariant.VariantType => "query_string";

	//	public QueryContainer ToQueryContainer() => new(this);

	//	public QueryStringQueryDescriptor DefaultField(string field) => Assign(field, (a, v) => a.DefaultField = v);
	//}

	//public partial class DistanceFeatureQuery : IQueryContainerVariant
	//{
	//	public void WrapInContainer(IQueryContainer container) => throw new NotImplementedException();
	//}

	//public partial interface ISpanGapQuery : QueryDsl.IQueryContainerVariant, QueryDsl.ISpanQueryVariant
	//{
	//}

	//public partial class SpanGapQuery : Dictionary<string, int>, ISpanGapQuery
	//{
	//	public void WrapInContainer(IQueryContainer container) => throw new NotImplementedException();
	//	public void WrapInContainer(ISpanQuery container) => throw new NotImplementedException();
	//}

	//public partial class PinnedIdsQuery : IPinnedQueryVariant
	//{
	//	public IEnumerable<string> Ids { get; set; }

	//	public void WrapInContainer(IPinnedQuery container) => throw new NotImplementedException();
	//}

	//public partial class PinnedDocsQuery : IPinnedQueryVariant
	//{
	//	public IEnumerable<PinnedDoc> Docs { get; set; }

	//	public void WrapInContainer(IPinnedQuery container) => throw new NotImplementedException();
	//}
}

namespace Elastic.Clients.Elasticsearch
{
	// Stubs until we generate these - Allows the code to compile so we can identify real errors.

	public partial class HttpHeaders : Dictionary<string, Union<string, IReadOnlyCollection<string>>>
	{
	}

	public partial class Metadata : Dictionary<string, object>
	{
	}

	//public partial class RuntimeFields : Dictionary<Field, RuntimeField>
	//{
	//}

	public partial class ApplicationsPrivileges : Dictionary<Name, ResourcePrivileges>
	{
	}

	public partial class Privileges : Dictionary<string, bool>
	{
	}

	public partial class ResourcePrivileges : Dictionary<Name, Privileges>
	{
	}

	//// TODO: Dictionary Examples
	//public partial class IndexHealthStatsDictionary : Dictionary<IndexName, Cluster.Health.IndexHealthStats>
	//{
	//	public Cluster.Health.IndexHealthStats GetStats(IndexName indexName) => base[indexName];
	//}

	//public partial class IndexHealthStatsDictionaryV2
	//{
	//	private readonly Dictionary<IndexName, Cluster.Health.IndexHealthStats> _backingDictionary = new();

	//	public Cluster.Health.IndexHealthStats GetStats(IndexName indexName) => _backingDictionary[indexName];
	//}

	//public partial class Actions : Dictionary<IndexName, ActionStatus>
	//{
	//}

	// TODO: Implement properly
	//[JsonConverter(typeof(UnionConverter<EpochMillis>))]
	public partial class EpochMillis
	{
		public EpochMillis() : base(1) { } // TODO: This is temp
	}

	// TODO: Implement properly
	[JsonConverter(typeof(PercentageConverter))]
	public partial class Percentage
	{
	}



	/// <summary>
	///     Block type for an index.
	/// </summary>
	//public readonly struct IndicesBlockOptions : IUrlParameter
	//{
	//	 TODO - This is currently generated as an enum by the code generator
	//	 ?? Should all enums be generated this way, or just those used in Url parameters

	//	private IndicesBlockOptions(string value) => Value = value;

	//	public string Value { get; }

	//	public string GetString(ITransportConfiguration settings) => Value;

	//	/ <summary>
	//	/     Disable metadata changes, such as closing the index.
	//	/ </summary>
	//	public static IndicesBlockOptions Metadata { get; } = new("metadata");

	//	/ <summary>
	//	/     Disable read operations.
	//	/ </summary>
	//	public static IndicesBlockOptions Read { get; } = new("read");

	//	/ <summary>
	//	/     Disable write operations and metadata changes.
	//	/ </summary>
	//	public static IndicesBlockOptions ReadOnly { get; } = new("read_only");

	//	/ <summary>
	//	/     Disable write operations. However, metadata changes are still allowed.
	//	/ </summary>
	//	public static IndicesBlockOptions Write { get; } = new("write");
	//}



	//public class Aggregate
	//{
	//}

	//public class Property
	//{
	//}

	namespace Global.Search
	{
		public class SortResults
		{
		}
	}




	//[JsonConverter(typeof(NumericAliasConverter<VersionNumber>))]
	//public class VersionNumber
	//{
	//	public VersionNumber(long value) => Value = value;

	//	internal long Value { get; }
	//}

	//public class Types : IUrlParameter
	//{
	//	public string GetString(ITransportConfiguration settings) => throw new NotImplementedException();
	//}
}
