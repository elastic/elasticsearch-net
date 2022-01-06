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
using Elastic.Clients.Elasticsearch.Serialization;
using System.Linq.Expressions;
using System.IO;

namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class TopMetricsValue
	{
		public TopMetricsValue(Field field) => Field = field;
	}

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
	/// <typeparam name="TDocument"></typeparam>
	public abstract class AggregationDescriptorBase<TDocument> : DescriptorBase<TDocument>, IAggregationContainerDescriptor where TDocument : DescriptorBase<TDocument>
	{
		string IAggregationContainerDescriptor.NameValue => NameValue;

		internal string? NameValue { get; }

		public AggregationDescriptorBase(string name) => NameValue = name;
	}

	public partial class AggregationContainer
	{
		internal string ContainedVariantName { get; set; }

		internal Action<Utf8JsonWriter, JsonSerializerOptions> SerializeFluent { get; private set; }

		private AggregationContainer(string variant) => ContainedVariantName = variant;

		internal static AggregationContainer CreateWithAction<T>(string variantName, Action<T> configure) where T : new()
		{
			var container = new AggregationContainer(variantName);
			container.SetAction(configure);
			return container;
		}

		private void SetAction<T>(Action<T> configure) where T : new()
			=> SerializeFluent = (writer, options) =>
				{
					var descriptor = new T();
					configure(descriptor);
					JsonSerializer.Serialize(writer, descriptor, options);
				};

		public static implicit operator AggregationContainer(AggregationBase aggregator)
		{
			if (aggregator == null)
				return null;

			// TODO: Reimplement this fully - as neccesary!

			var container = new AggregationContainer(aggregator)
			{
				//Meta = aggregator.Meta
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


	public partial class AggregationContainerDescriptor<TDocument> : DescriptorBase<AggregationContainerDescriptor<TDocument>>
	{
		internal AggregationDictionary Aggregations { get; set; }

		private AggregationContainerDescriptor<TDocument> SetContainer(string key, AggregationContainer container)
		{
			if (Self.Aggregations == null)
				Self.Aggregations = new AggregationDictionary();

			Self.Aggregations[key] = container;

			return this;
		}

		public AggregationContainerDescriptor<TDocument> Average(string name, Action<AverageAggregationDescriptor<TDocument>> configure) => SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));

		public AggregationContainerDescriptor<TDocument> WeightedAverage(string name, Action<WeightedAverageAggregationDescriptor<TDocument>> configure) => SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));

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

				var aggregateName = nameParts[0];

				switch (aggregateName)
				{
					case "terms":
					case "lterms":
						{
							if (TermsAggregateSerializationHelper.TryDeserialiseTermsAggregate(ref reader, options, out var agg))
							{
								dictionary.Add(nameParts[1], agg);
							}

							break;
						}

					case "adjacency_matrix":
						{
							var agg = JsonSerializer.Deserialize<AdjacencyMatrixAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "auto_date_histogram":
						{
							var agg = JsonSerializer.Deserialize<AutoDateHistogramAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "children":
						{
							var agg = JsonSerializer.Deserialize<ChildrenAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "composite":
						{
							var agg = JsonSerializer.Deserialize<CompositeAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "date_histogram":
						{
							var agg = JsonSerializer.Deserialize<DateHistogramAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "date_range":
						{
							var agg = JsonSerializer.Deserialize<DateRangeAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "diversified_sampler":
						break;
					case "filter":
						{
							var agg = JsonSerializer.Deserialize<FilterAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "filters":
						{
							var agg = JsonSerializer.Deserialize<FiltersAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "geo_distance":
						{
							var agg = JsonSerializer.Deserialize<GeoDistanceAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "geohash_grid":
						{
							var agg = JsonSerializer.Deserialize<GeoHashGridAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "geotile_grid":
						{
							var agg = JsonSerializer.Deserialize<GeoTileGridAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "global":
						{
							var agg = JsonSerializer.Deserialize<GlobalAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "histogram":
						{
							var agg = JsonSerializer.Deserialize<HistogramAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "ip_range":
						{
							var agg = JsonSerializer.Deserialize<IpRangeAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "missing":
						{
							var agg = JsonSerializer.Deserialize<MissingAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "multi-terms":
						{
							var agg = JsonSerializer.Deserialize<MultiTermsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "nested":
						{
							var agg = JsonSerializer.Deserialize<NestedAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "parent":
						break;
					case "range":
						{
							var agg = JsonSerializer.Deserialize<RangeAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "rare_terms":
						break;
					case "reverse_nested":
						{
							var agg = JsonSerializer.Deserialize<ReverseNestedAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "sampler":
						{
							var agg = JsonSerializer.Deserialize<SamplerAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "significant_terms":
						break;

					case "significant_text":
						// TODO
						throw new Exception("The aggregate in response is not yet supported");

					case "variable_width_histogram":
						{
							var agg = JsonSerializer.Deserialize<VariableWidthHistogramAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "avg":
						{
							var agg = JsonSerializer.Deserialize<AvgAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "boxplot":
						{
							var agg = JsonSerializer.Deserialize<BoxPlotAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "cardinality":
						{
							var agg = JsonSerializer.Deserialize<CardinalityAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "extended_stats":
						{
							var agg = JsonSerializer.Deserialize<ExtendedStatsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "geo_bounds":
						{
							var agg = JsonSerializer.Deserialize<GeoBoundsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "geo_centroid":
						{
							var agg = JsonSerializer.Deserialize<GeoCentroidAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "geo_line":
						{
							var agg = JsonSerializer.Deserialize<GeoLineAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "matrix_stats":
						{
							var agg = JsonSerializer.Deserialize<MatrixStatsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "max":
						{
							var agg = JsonSerializer.Deserialize<MaxAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "median_absolute_deviation":
						{
							var agg = JsonSerializer.Deserialize<MedianAbsoluteDeviationAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "min":
						{
							var agg = JsonSerializer.Deserialize<MinAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "percentile_ranks":
						break;
					case "tdigest_percentile_ranks":
						{
							var agg = JsonSerializer.Deserialize<TDigestPercentileRanksAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "percentiles":
						break;
					case "rate":
						{
							var agg = JsonSerializer.Deserialize<RateAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "scripted_metric":
						{
							var agg = JsonSerializer.Deserialize<ScriptedMetricAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "stats":
						{
							var agg = JsonSerializer.Deserialize<StatsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "string_stats":
						{
							var agg = JsonSerializer.Deserialize<StringStatsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "sum":
						{
							var agg = JsonSerializer.Deserialize<SumAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "t_test":
						{
							var agg = JsonSerializer.Deserialize<TTestAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "top_hits":
						{
							var agg = JsonSerializer.Deserialize<TopHitsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "top_metrics":
						{
							var agg = JsonSerializer.Deserialize<TopMetricsAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "value_count":
						{
							var agg = JsonSerializer.Deserialize<ValueCountAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "weighted_avg":
						{
							var agg = JsonSerializer.Deserialize<WeightedAvgAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "avg_bucket":
						break;
					case "bucket_script":
						break;
					case "bucket_count_ks_test":
						break;
					case "bucket_correlation":
						break;
					case "bucket_selector":
						break;
					case "bucket_sort":
						break;
					case "cumulative_cardinality":
						{
							var agg = JsonSerializer.Deserialize<CumulativeCardinalityAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "cumulative_sum":
						break;
					case "derivative":
						{
							var agg = JsonSerializer.Deserialize<DerivativeAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "extended_stats_bucket":
						{
							var agg = JsonSerializer.Deserialize<ExtendedStatsBucketAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "inference":
						{
							var agg = JsonSerializer.Deserialize<InferenceAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "max_bucket":
						break;
					case "min_bucket":
						break;
					case "moving_avg":
						break;
					case "moving_fn":
						break;
					case "moving_percentiles":
						break;
					case "normalize":
						break;
					case "percentiles_bucket":
						{
							var agg = JsonSerializer.Deserialize<PercentilesBucketAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "serial_diff":
						break;
					case "stats_bucket":
						{
							var agg = JsonSerializer.Deserialize<StatsBucketAggregate>(ref reader, options);
							dictionary.Add(nameParts[1], agg);
							break;
						}

					case "sum_bucket":
						break;
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
	public partial class ElasticClient
	{
		public SourceResponse<TDocument> Source<TDocument>(DocumentPath<TDocument> id, Action<SourceRequestDescriptor<TDocument>> configure = null)
		{
			var descriptor = new SourceRequestDescriptor<TDocument>(documentWithId: id.Document, index: id?.Self?.Index, id: id?.Self?.Id);
			configure?.Invoke(descriptor);
			return DoRequest<SourceRequestDescriptor<TDocument>, SourceResponse<TDocument>>(descriptor);
		}
	}

	public partial interface IElasticClient
	{
		public SourceResponse<TDocument> Source<TDocument>(DocumentPath<TDocument> id, Action<SourceRequestDescriptor<TDocument>> configure = null);
	}

	public abstract partial class BulkResponseItemBase
	{
		// TODO

		/// <summary>
		/// Deserialize the <see cref="Get"/> property as a GetResponse<TDocument> type, where TDocument is the document type.
		/// </summary>
		//public GetResponse<TDocument> GetResponse<TDocument>() where TDocument : class => Get.Source?.AsUsingRequestResponseSerializer<GetResponse<TDocument>>();
	}

	public sealed partial class SourceRequestDescriptor<TDocument>
	{
		public SourceRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }

		/// <summary>
		/// The name of the index.
		/// </summary>
		public SourceRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));

		/// <summary>
		/// A shortcut into calling Index(typeof(TOther)).
		/// </summary>
		public SourceRequestDescriptor<TDocument> Index<TOther>() => Assign(typeof(TOther), (a, v) => a.RouteValues.Required("index", (IndexName)v));
	}

	public partial class SourceResponse<TDocument> : ISelfDeserializable
	{
		public TDocument Body { get; set; }

		public void Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);

			using var stream = new MemoryStream();

			var writer = new Utf8JsonWriter(stream);
			jsonDoc.WriteTo(writer);
			writer.Flush();
			stream.Position = 0;

			var body = settings.SourceSerializer.Deserialize<TDocument>(stream);

			Body = body;
		}
	}


	[JsonConverter(typeof(FieldTypeConverter))]
	public enum FieldType
	{
		Date,
	}

	public partial class CountRequest<TDocument> : CountRequest
	{
		//protected CountRequest<TDocument> TypedSelf => this;

		///<summary>/{index}/_count</summary>
		public CountRequest() : base(typeof(TDocument))
		{
		}

		///<summary>/{index}/_count</summary>
		///<param name = "index">Optional, accepts null</param>
		public CountRequest(Indices index) : base(index)
		{
		}
	}

	public partial class BulkRequest : IStreamSerializable
	{
		protected IRequest Self => this;

		public BulkOperationsCollection Operations { get; set; }

		protected override string ContentType => "application/x-ndjson";

		protected override string Accept => "application/json";

		public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (Operations is null)
				return;

			var index = Self.RouteValues.Get<IndexName>("index");

			foreach (var op in Operations)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				op.PrepareIndex(index);

				serializable.Serialize(stream, settings, formatting);
				stream.WriteByte((byte)'\n');
			}
		}

		public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (Operations is null)
				return;

			var index = Self.RouteValues.Get<IndexName>("index");

			foreach (var op in Operations)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				op.PrepareIndex(index);

				await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
				stream.WriteByte((byte)'\n');
			}
		}
	}

	public abstract partial class BulkResponseItemBase
	{
		public abstract string Operation { get; }

		public bool IsValid
		{
			get
			{
				if (Error is not null)
					return false;

				return Operation.ToLowerInvariant() switch
				{
					"delete" => Status == 200 || Status == 404,
					"update" or "index" or "create" => Status == 200 || Status == 201,
					_ => false,
				};
			}
		}
	}

	public partial class StoredScriptId
	{
		public StoredScriptId(Id id) => Id = id;
	}

	public partial class BulkResponse
	{
		[JsonConverter(typeof(BulkResponseItemConverter)), JsonPropertyName("items")]
		public IReadOnlyList<BulkResponseItemBase> Items { get; init; }

		[JsonIgnore]
		public IEnumerable<BulkResponseItemBase> ItemsWithErrors => !Items.HasAny()
			? Enumerable.Empty<BulkResponseItemBase>()
			: Items.Where(i => !i.IsValid);

		public override bool IsValid => base.IsValid && !Errors && !ItemsWithErrors.HasAny();

		protected override void DebugIsValid(StringBuilder sb)
		{
			if (Items == null)
				return;

			sb.AppendLine($"# Invalid Bulk items:");
			foreach (var i in Items.Select((item, i) => new { item, i }).Where(i => !i.item.IsValid))
				sb.AppendLine($"  operation[{i.i}]: {i.item}");
		}
	}

	public sealed partial class BulkRequestDescriptor : IStreamSerializable
	{
		protected override string ContentType => "application/x-ndjson";

		protected override string Accept => "application/json";

		private readonly BulkOperationsCollection _operations = new();

		public BulkRequestDescriptor Index(string index) => Assign(index, (a, v) => a.RouteValues.Optional("index", IndexName.Parse(v)));

		public BulkRequestDescriptor Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));
			
		public BulkRequestDescriptor Create<TSource>(TSource document, Action<BulkCreateOperationDescriptor<TSource>> configure = null)
		{
			var descriptor = new BulkCreateOperationDescriptor<TSource>(document);
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Create<TSource>(TSource document, IndexName index, Action<BulkCreateOperationDescriptor<TSource>> configure = null)
		{
			var descriptor = new BulkCreateOperationDescriptor<TSource>(document, index);
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Index<TSource>(TSource document, Action<BulkIndexOperationDescriptor<TSource>> configure = null)
		{
			var descriptor = new BulkIndexOperationDescriptor<TSource>(document);
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Index<TSource>(TSource document, IndexName index, Action<BulkIndexOperationDescriptor<TSource>> configure = null)
		{
			var descriptor = new BulkIndexOperationDescriptor<TSource>(document, index);
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Update(BulkUpdateOperationBase update)
		{
			_operations.Add(update);
			return this;
		}

		public BulkRequestDescriptor Update<TSource, TPartialDocument>(Action<BulkUpdateOperationDescriptor<TSource, TPartialDocument>> configure)
		{
			var descriptor = new BulkUpdateOperationDescriptor<TSource, TPartialDocument>();
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Update<T>(Action<BulkUpdateOperationDescriptor<T, T>> configure) =>
			Update<T, T>(configure);

		public BulkRequestDescriptor Delete(Id id, Action<BulkDeleteOperationDescriptor> configure = null)
		{
			var descriptor = new BulkDeleteOperationDescriptor(id);
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Delete(string id, Action<BulkDeleteOperationDescriptor> configure = null)
		{
			var descriptor = new BulkDeleteOperationDescriptor(id);
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Delete(Action<BulkDeleteOperationDescriptor> configure)
		{
			var descriptor = new BulkDeleteOperationDescriptor();
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Delete<TSource>(TSource documentToDelete, Action<BulkDeleteOperationDescriptor> configure = null)
		{
			var descriptor = new BulkDeleteOperationDescriptor(new Id(documentToDelete));
			configure?.Invoke(descriptor);
			_operations.Add(descriptor);
			return this;
		}

		public BulkRequestDescriptor Delete<TSource>(Action<BulkDeleteOperationDescriptor> configure) => Delete(configure);

		public BulkRequestDescriptor CreateMany<TSource>(IEnumerable<TSource> documents, Action<BulkCreateOperationDescriptor<TSource>, TSource> bulkCreateSelector = null) =>
			AddOperations(documents, bulkCreateSelector, o => new BulkCreateOperationDescriptor<TSource>(o));

		public BulkRequestDescriptor IndexMany<TSource>(IEnumerable<TSource> documents, Action<BulkIndexOperationDescriptor<TSource>, TSource> bulkIndexSelector = null) =>
			AddOperations(documents, bulkIndexSelector, o => new BulkIndexOperationDescriptor<TSource>(o));

		public BulkRequestDescriptor UpdateMany<TSource>(IEnumerable<TSource> objects, Action<BulkUpdateOperationDescriptor<TSource, TSource>, TSource> bulkIndexSelector = null) =>
			AddOperations(objects, bulkIndexSelector, o => new BulkUpdateOperationDescriptor<TSource, TSource>().IdFrom(o));

		public BulkRequestDescriptor DeleteMany<TSource>(IEnumerable<string> ids, Action<BulkDeleteOperationDescriptor, string> bulkDeleteSelector = null) =>
			AddOperations(ids, bulkDeleteSelector, id => new BulkDeleteOperationDescriptor(id));

		public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (_operations is null)
				return;

			var index = Self.RouteValues.Get<IndexName>("index");

			foreach (var op in _operations)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				op.PrepareIndex(index);

				serializable.Serialize(stream, settings, formatting);
				stream.WriteByte((byte)'\n');
			}
		}

		public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			if (_operations is null)
				return;

			var index = Self.RouteValues.Get<IndexName>("index");

			foreach (var op in _operations)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				op.PrepareIndex(index);

				await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
				stream.WriteByte((byte)'\n');
			}
		}

		private BulkRequestDescriptor AddOperations<TSource, TDescriptor>(
			IEnumerable<TSource> objects,
			Action<TDescriptor, TSource> configureDescriptor,
			Func<TSource, TDescriptor> createDescriptor
		) where TDescriptor : IBulkOperation
		{
			if (@objects == null)
				return this;

			var objectsList = @objects.ToList();
			var operations = new List<IBulkOperation>(objectsList.Count());

			foreach (var o in objectsList)
			{
				var descriptor = createDescriptor(o);

				if (configureDescriptor is not null)
				{
					configureDescriptor(descriptor, o);
				}

				operations.Add(descriptor);
			}

			return Assign(operations, (a, v) => a._operations.AddRange(v));
		}
	}

	/// <summary>
	/// Used to mark types which expect to directly serialise into a stream. This supports non-json compliant output such as NDJSON.
	/// </summary>
	internal interface IStreamSerializable
	{
		/// <summary>
		/// Serialize the object into the supplied <see cref="Stream"/>.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="settings"></param>
		/// <param name="formatting"></param>
		void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		/// <summary>
		/// Asynchronously serialize the object into the supplied <see cref="Stream"/>.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="settings"></param>
		/// <param name="formatting"></param>
		/// <returns></returns>
		Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);
	}

	internal class FieldTypeConverter : JsonConverter<FieldType>
	{
		public override FieldType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var enumString = reader.GetString();
			switch (enumString)
			{
				case "date":
					return FieldType.Date;
			}

			ThrowHelper.ThrowJsonException("Unexpected field type value.");
			return default;
		}

		public override void Write(Utf8JsonWriter writer, FieldType value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case FieldType.Date:
					writer.WriteStringValue("date");
					return;
			}

			writer.WriteNullValue();
		}
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

	public partial struct OpType : IStringable
	{
		public static OpType Index = new("index");
		public static OpType Create = new("create");

		public OpType(string value) => Value = value;

		public string Value { get; }

		public static implicit operator OpType(string v) => new(v);

		public string GetString() => Value ?? string.Empty;
	}

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

	public sealed partial class CreateRequest<TDocument> : ICustomJsonWriter
	{

		public CreateRequest(Id id) : this(typeof(TDocument), id)
		{
		}

		public CreateRequest(TDocument documentWithId, IndexName index = null, Id id = null)
			: this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) =>
				Document = documentWithId;

		public void WriteJson(Utf8JsonWriter writer, SerializerBase sourceSerializer) => SourceSerialisation.Serialize(Document, writer, sourceSerializer);
	}

	public sealed partial class CreateRequestDescriptor<TDocument> : ICustomJsonWriter
	{
		public CreateRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Elasticsearch.Id.From(documentWithId)) => DocumentFromPath(documentWithId);

		private void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);

		public CreateRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));

		public void WriteJson(Utf8JsonWriter writer, SerializerBase sourceSerializer) => SourceSerialisation.Serialize(DocumentValue, writer, sourceSerializer);

		// TODO: We should be able to generate these for optional params
		public CreateRequestDescriptor<TDocument> Id(Id id)
		{
			RouteValues.Optional("id", id);
			return this;
		}
	}

	public sealed partial class CreateRequestDescriptor<TDocument>
	{
		public CreateRequestDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
	}

	public sealed partial class UpdateRequestDescriptor<TDocument, TPartialDocument>
	{
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
		public UpdateRequestDescriptor<TDocument, TPartialDocument> PartialDocument(TPartialDocument document) => this;
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

		protected override string ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings)
		{
			if (Pit is not null && !string.IsNullOrEmpty(Pit.Id.StringOrLongValue ?? string.Empty) && routeValues.ContainsKey("index"))
			{
				routeValues.Remove("index");
			}

			return base.ResolveUrl(routeValues, settings);
		}
	}

	public sealed partial class PointInTimeReferenceDescriptor
	{
		public PointInTimeReferenceDescriptor(Id id) => IdValue = id;
	}

	public sealed partial class SearchRequestDescriptor<TDocument>
	{
		internal Type ClrType => typeof(TDocument);

		public SearchRequestDescriptor<TDocument> Index(Indices index) => Assign(index, (a, v) => a.RouteValues.Optional("index", v));

		public SearchRequestDescriptor<TDocument> Pit(Id id, Action<PointInTimeReferenceDescriptor> configure)
		{
			PitValue = null;
			PitDescriptorAction = null;
			configure += a => a.Id(id);
			return Assign(configure, (a, v) => a.PitDescriptorAction = v);
		}

		internal override void BeforeRequest()
		{
			if (AggregationsValue is not null || AggregationsDescriptor is not null || AggregationsDescriptorAction is not null)
			{
				TypedKeys(true);
			}
		}

		protected override string ResolveUrl(RouteValues routeValues, IElasticsearchClientSettings settings)
		{
			if ((Self.PitValue is not null || Self.PitDescriptor is not null || Self.PitDescriptorAction is not null) && routeValues.ContainsKey("index"))
			{
				routeValues.Remove("index");
			}

			return base.ResolveUrl(routeValues, settings);
		}

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
		}
	}

	public sealed partial class CountRequestDescriptor<TDocument>
	{
		public CountRequestDescriptor<TDocument> Query(Func<QueryContainerDescriptor<TDocument>, QueryContainer> configure)
		{
			var container = configure?.Invoke(new QueryContainerDescriptor<TDocument>());
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
		public static implicit operator QueryContainer(TermQuery termQuery) => new(termQuery);
	}

	public partial class MatchAllQuery
	{
		public static implicit operator QueryContainer(MatchAllQuery matchAllQuery) => new(matchAllQuery);
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

	public sealed partial class QueryContainerDescriptor<TDocument>
	{
		public void MatchAll() => Set(new MatchAllQuery(), "match_all");

		public void Term<TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null, string name = null) =>
			Term(t => t.Field(field).Value(value).Boost(boost).Name(name));
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
