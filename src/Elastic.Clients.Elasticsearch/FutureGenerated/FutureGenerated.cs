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

namespace Elastic.Clients.Elasticsearch.Sql
{
	public partial class SqlTranslateResponse
	{
		[JsonInclude]
		[JsonPropertyName("query")]
		public QueryContainer Query { get; set; }
	}

	public static class SqlTranslateResponseExtensions
	{
		public static SearchRequest AsSearchRequest(this SqlTranslateResponse response)
			=> new()
			{
				Query = response.Query,
				//Size = response.Size,
				//Fields = response.Fields
			};
	}
}

namespace Elastic.Clients.Elasticsearch
{
	[JsonConverter(typeof(SourceConfigConverter))]
	public partial class SourceConfig
	{
		public bool HasBoolValue => Item1.HasValue;

		public bool HasSourceFilterValue => Item2 is not null;

		public bool TryGetBool(out bool? value)
		{
			if (Item1.HasValue)
			{
				value = Item1.Value;
				return true;
			}

			value = null;
			return false;
		}

		public bool TryGetSourceFilter(out SourceFilter? value)
		{
			if (Item2 is not null)
			{
				value = Item2;
				return true;
			}

			value = null;
			return false;
		}
	}

	internal class SourceConfigConverter : JsonConverter<SourceConfig>
	{
		public override SourceConfig? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			switch (reader.TokenType)
			{
				case JsonTokenType.True:
				case JsonTokenType.False:
					var value = reader.GetBoolean();
					return new SourceConfig(value);

				case JsonTokenType.StartObject:
					var sourceFilter = JsonSerializer.Deserialize<SourceFilter>(ref reader, options);
					return new SourceConfig(sourceFilter);
			}

			return null;
		}

		public override void Write(Utf8JsonWriter writer, SourceConfig value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}

namespace Elastic.Clients.Elasticsearch.Aggregations
{
	//public partial class TopMetricsValue
	//{
	//	public TopMetricsValue(Field field) => Field = field;
	//}

	public partial class Buckets<TBucket>
	{
		//public IReadOnlyCollection<TBucket> Items => Item2 is not null ? Item2 : Array.Empty<TBucket>();

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


	public partial class AggregationContainerDescriptor
	{
		internal AggregationDictionary Aggregations { get; set; }

		private AggregationContainerDescriptor SetContainer(string key, AggregationContainer container)
		{
			if (Self.Aggregations == null)
				Self.Aggregations = new AggregationDictionary();

			Self.Aggregations[key] = container;

			return this;
		}

		//public AggregationContainerDescriptor Average(string name, Action<AverageAggregationDescriptor> configure) => SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));

		//public AggregationContainerDescriptor WeightedAverage(string name, Action<WeightedAverageAggregationDescriptor> configure) => SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
	}

	public partial class AggregationContainerDescriptor<TDocument>
	{
		internal AggregationDictionary Aggregations { get; set; }

		private AggregationContainerDescriptor<TDocument> SetContainer(string key, AggregationContainer container)
		{
			if (Self.Aggregations == null)
				Self.Aggregations = new AggregationDictionary();

			Self.Aggregations[key] = container;

			return this;
		}

		//public AggregationContainerDescriptor Average(string name, Action<AverageAggregationDescriptor> configure) => SetContainer(name, AggregationContainer.CreateWithAction("avg", configure));

		//public AggregationContainerDescriptor WeightedAverage(string name, Action<WeightedAverageAggregationDescriptor> configure) => SetContainer(name, AggregationContainer.CreateWithAction("weighted_avg", configure));

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => JsonSerializer.Serialize(writer, Aggregations, options);
	}

	public class EmptyTermsAggregate : TermsAggregateBase<EmptyTermsBucket>
	{
	}

	public abstract partial class MultiBucketAggregateBase<TBucket>
	{
		[JsonInclude]
		[JsonPropertyName("buckets")]
		public IReadOnlyCollection<TBucket> Buckets { get; init; }
	}

	public class EmptyTermsBucket { }

	public class TermsAggregate<TKey> : TermsAggregateBase<TermsBucket<TKey>>
	{
	}

	public class TermsBucket<TKey> : TermsBucketBase
	{
		public TKey Key { get; init; }
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

		public AvgAggregate? Average(string key) => TryGet<AvgAggregate?>(key);

		public TermsAggregate<string> Terms(string key) => Terms<string>(key);

		public TermsAggregate<TKey> Terms<TKey>(string key)
		{
			if (!BackingDictionary.TryGetValue(key, out var agg))
			{
				return null;
			}

			switch (agg)
			{
				case EmptyTermsAggregate empty:
					return new TermsAggregate<TKey>
					{
						Buckets = Array.Empty<TermsBucket<TKey>>().ToReadOnlyCollection(),
						Meta = empty.Meta,
						DocCountErrorUpperBound = empty.DocCountErrorUpperBound,
						SumOtherDocCount = empty.SumOtherDocCount
					};
				case StringTermsAggregate stringTerms:
					var buckets = stringTerms.Buckets.Select(b => new TermsBucket<TKey> { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.Key }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = buckets,
						Meta = stringTerms.Meta,
						DocCountErrorUpperBound = stringTerms.DocCountErrorUpperBound,
						SumOtherDocCount = stringTerms.SumOtherDocCount
					};
				case DoubleTermsAggregate doubleTerms:
					var doubleTermsBuckets = doubleTerms.Buckets.Select(b => new TermsBucket<TKey> { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.KeyAsString }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = doubleTermsBuckets,
						Meta = doubleTerms.Meta,
						DocCountErrorUpperBound = doubleTerms.DocCountErrorUpperBound,
						SumOtherDocCount = doubleTerms.SumOtherDocCount
					};
				case LongTermsAggregate longTerms:
					var longTermsBuckets = longTerms.Buckets.Select(b => new TermsBucket<TKey> { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.KeyAsString }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = longTermsBuckets,
						Meta = longTerms.Meta,
						DocCountErrorUpperBound = longTerms.DocCountErrorUpperBound,
						SumOtherDocCount = longTerms.SumOtherDocCount
					};

					// TODO - Multi-terms
			}

			return null;
		}

		private static TKey GetKeyFromBucketKey<TKey>(object key) =>
			typeof(TKey).IsEnum
				? (TKey)Enum.Parse(typeof(TKey), key.ToString(), true)
				: (TKey)Convert.ChangeType(key, typeof(TKey));
	}
}

namespace Elastic.Clients.Elasticsearch
{
	public sealed partial class SearchRequestDescriptor<TDocument>
	{
		public SearchRequestDescriptor<TDocument> Index(Indices indices)
		{
			Self.RouteValues.Optional("index", indices);
			return Self;
		}
	}

	public partial class InlineScript : ISelfTwoWaySerializable
	{
		void ISelfTwoWaySerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();

			if (Language is not null)
			{
				writer.WritePropertyName("lang");
				JsonSerializer.Serialize(writer, Language, options);
			}

			if (Options is not null)
			{
				writer.WritePropertyName("options");
				JsonSerializer.Serialize(writer, Options, options);
			}

			writer.WritePropertyName("source");
			writer.WriteStringValue(Source);

			if (Params is not null)
			{
				writer.WritePropertyName("params");
				SourceSerialisation.SerializeParams(Params, writer, settings);
			}

			writer.WriteEndObject();
		}

		void ISelfTwoWaySerializable.Deserialize(ref Utf8JsonReader reader, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("lang"))
					{
						reader.Read();
						var value = reader.GetString();
						if (value is not null)
						{
							Language = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("options"))
					{
						var value = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);
						if (value is not null)
						{
							Options = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("source"))
					{
						reader.Read();
						var value = reader.GetString();
						if (value is not null)
						{
							Source = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("params"))
					{
						var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
						if (value is not null)
						{
							Params = value;
						}

						continue;
					}
				}
			}
		}

		public InlineScript(string source) => Source = source;

		internal InlineScript() { }
	}

	//internal sealed class InlineScriptConverter : JsonConverter<InlineScript>
	//{
	//	public override InlineScript? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
	//	public override void Write(Utf8JsonWriter writer, InlineScript value, JsonSerializerOptions options)
	//	{
	//		writer.WriteStartObject();
	//		if (value.Language is not null)
	//		{
	//			writer.WritePropertyName("lang");
	//			JsonSerializer.Serialize(writer, value.Language, options);
	//		}

	//		if (value.Options is not null)
	//		{
	//			writer.WritePropertyName("options");
	//			JsonSerializer.Serialize(writer, value.Options, options);
	//		}

	//		writer.WritePropertyName("source");
	//		writer.WriteStringValue(value.Source);

	//		if (value.Params is not null)
	//		{
	//			writer.WritePropertyName("params");
	//			SourceSerialisation.SerializeParams(value.Params, writer, settings);
	//		}

	//		writer.WriteEndObject();
	//	}
	//}

	[JsonConverter(typeof(TermsOrderConverter))]
	public readonly struct TermsOrder : IEquatable<TermsOrder>
	{
		public TermsOrder(string key, SortOrder order) => (Key, Order) = (key, order);

		public static TermsOrder CountAscending => new() { Key = "_count", Order = SortOrder.Asc };
		public static TermsOrder CountDescending => new() { Key = "_count", Order = SortOrder.Desc };
		public static TermsOrder KeyAscending => new() { Key = "_key", Order = SortOrder.Asc };
		public static TermsOrder KeyDescending => new() { Key = "_key", Order = SortOrder.Desc };

		public string Key { get; init; }
		public SortOrder Order { get; init; }

		public bool Equals(TermsOrder other) => Key == other.Key && Order == other.Order;
		public override bool Equals(object obj) => obj is TermsOrder other && Equals(other);
		public override int GetHashCode() => (Key, Order).GetHashCode();
		public static bool operator ==(TermsOrder lhs, TermsOrder rhs) => lhs.Equals(rhs);
		public static bool operator !=(TermsOrder lhs, TermsOrder rhs) => !(lhs == rhs);
	}

	internal sealed class TermsOrderConverter : JsonConverter<TermsOrder>
	{
		public override TermsOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				return default;

			reader.Read();
			var key = reader.GetString();

			reader.Read();
			var valueString = reader.GetString();
			var value = valueString switch
			{
				"asc" => SortOrder.Asc,
				"desc" => SortOrder.Desc,
				_ => throw new JsonException("Unexpected sort order in JSON"),
			};

			reader.Read();

			if (reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException("JSON did not conform to expected shape");

			return new TermsOrder(key, value);
		}

		public override void Write(Utf8JsonWriter writer, TermsOrder value, JsonSerializerOptions options)
		{
			if (string.IsNullOrEmpty(value.Key))
			{
				writer.WriteNullValue();
				return;
			}

			writer.WriteStartObject();
			writer.WritePropertyName(value.Key);
			switch (value.Order)
			{
				case SortOrder.Asc:
					writer.WriteStringValue("asc");
					break;
				case SortOrder.Desc:
					writer.WriteStringValue("desc");
					break;
				default:
					throw new JsonException("Unknown sort order specified.");
			}
			writer.WriteEndObject();
		}
	}

	public class TermsOrderDescriptor : DescriptorPromiseBase<TermsOrderDescriptor, IList<TermsOrder>>
	{
		public TermsOrderDescriptor() : base(new List<TermsOrder>()) { }

		public TermsOrderDescriptor CountAscending() => Assign(a => a.Add(TermsOrder.CountAscending));

		public TermsOrderDescriptor CountDescending() => Assign(a => a.Add(TermsOrder.CountDescending));

		public TermsOrderDescriptor KeyAscending() => Assign(a => a.Add(TermsOrder.KeyAscending));

		public TermsOrderDescriptor KeyDescending() => Assign(a => a.Add(TermsOrder.KeyDescending));

		public TermsOrderDescriptor Ascending(string key) =>
			string.IsNullOrWhiteSpace(key) ? this : Assign(key, (a, v) => a.Add(new TermsOrder { Key = v, Order = SortOrder.Asc }));

		public TermsOrderDescriptor Descending(string key) =>
			string.IsNullOrWhiteSpace(key) ? this : Assign(key, (a, v) => a.Add(new TermsOrder { Key = v, Order = SortOrder.Desc }));
	}


	public sealed partial class InlineScriptDescriptor
	{
		public InlineScriptDescriptor(string source) => SourceValue = source;
	}

	public sealed partial class ScriptDescriptor : DescriptorBase<ScriptDescriptor>
	{
		internal ScriptDescriptor(Action<ScriptDescriptor> configure) => configure.Invoke(this);

		internal InlineScriptDescriptor InlineScriptDescriptor { get; private set; }

		internal StoredScriptId StoredScriptId { get; private set; }

		/// <summary>
		/// A script that has been stored in Elasticsearch with the specified <paramref name="id"/>.
		/// </summary>
		public ScriptDescriptor Id(string id) => Assign(id, (a, v) => a.StoredScriptId = new StoredScriptId(v));

		/// <summary>
		/// An inline script to execute.
		/// </summary>
		public ScriptDescriptor Source(string script) => Assign(script, (a, v) => a.InlineScriptDescriptor = new InlineScriptDescriptor(v));

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			if (InlineScriptDescriptor is not null)
			{
				JsonSerializer.Serialize(writer, InlineScriptDescriptor, options);
				return;
			}

			if (StoredScriptId is not null)
			{
				JsonSerializer.Serialize(writer, StoredScriptId, options);
				return;
			}
		}
	}

	//public partial class ElasticsearchClient
	//{
	//	public SourceResponse<TDocument> Source<TDocument>(DocumentPath<TDocument> id, Action<SourceRequestDescriptor<TDocument>> configure = null)
	//	{
	//		var descriptor = new SourceRequestDescriptor<TDocument>(document: id.Document, index: id?.Self?.Index, id: id?.Self?.Id);
	//		configure?.Invoke(descriptor);
	//		return DoRequest<SourceRequestDescriptor<TDocument>, SourceResponse<TDocument>>(descriptor);
	//	}
	//}


	public abstract partial class BulkResponseItemBase
	{
		// TODO

		/// <summary>
		/// Deserialize the <see cref="Get"/> property as a GetResponse<TDocument> type, where TDocument is the document type.
		/// </summary>
		//public GetResponse<TDocument> GetResponse<TDocument>() where TDocument : class => Get.Source?.AsUsingRequestResponseSerializer<GetResponse<TDocument>>();
	}

	// TODO - Should be added as a rule to the descriptor generator
	//public sealed partial class SourceRequestDescriptor<TDocument>
	//{
	//	public SourceRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) => Doc
	//}

	public partial class SourceRequestDescriptor
	{
		/// <summary>
		/// A shortcut into calling Index(typeof(TOther)).
		/// </summary>
		public SourceRequestDescriptor Index<TOther>()
		{
			RouteValues.Required("index", (IndexName)typeof(TOther));
			return Self;
		}
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
		Text,
		Long
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
		public StoredScriptId(string id) => Id = id;
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

		public BulkRequestDescriptor Index(string index)
		{
			RouteValues.Optional("index", IndexName.Parse(index));
			return Self;
		}

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

		public BulkRequestDescriptor CreateMany<TSource>(IEnumerable<TSource> documents, Action<BulkCreateOperationDescriptor<TSource>, TSource> bulkCreateSelector) =>
			AddOperations(documents, bulkCreateSelector, o => new BulkCreateOperationDescriptor<TSource>(o));

		public BulkRequestDescriptor CreateMany<TSource>(IEnumerable<TSource> documents) =>
			AddOperations(documents, null, o => new BulkCreateOperationDescriptor<TSource>(o));

		public BulkRequestDescriptor IndexMany<TSource>(IEnumerable<TSource> documents, Action<BulkIndexOperationDescriptor<TSource>, TSource> bulkIndexSelector) =>
			AddOperations(documents, bulkIndexSelector, o => new BulkIndexOperationDescriptor<TSource>(o));

		public BulkRequestDescriptor IndexMany<TSource>(IEnumerable<TSource> documents) =>
			AddOperations(documents, null, o => new BulkIndexOperationDescriptor<TSource>(o));

		public BulkRequestDescriptor UpdateMany<TSource>(IEnumerable<TSource> objects, Action<BulkUpdateOperationDescriptor<TSource, TSource>, TSource> bulkIndexSelector) =>
			AddOperations(objects, bulkIndexSelector, o => new BulkUpdateOperationDescriptor<TSource, TSource>().IdFrom(o));

		public BulkRequestDescriptor UpdateMany<TSource>(IEnumerable<TSource> objects) =>
			AddOperations(objects, null, o => new BulkUpdateOperationDescriptor<TSource, TSource>().IdFrom(o));

		public BulkRequestDescriptor DeleteMany<T>(IEnumerable<T> objects, Action<BulkDeleteOperationDescriptor, T> bulkDeleteSelector) =>
			AddOperations(objects, bulkDeleteSelector, obj => new BulkDeleteOperationDescriptor(new Id(obj)));

		public BulkRequestDescriptor DeleteMany(IEnumerable<Id> ids, Action<BulkDeleteOperationDescriptor, Id> bulkDeleteSelector) =>
			AddOperations(ids, bulkDeleteSelector, id => new BulkDeleteOperationDescriptor(id));

		public BulkRequestDescriptor DeleteMany<T>(IEnumerable<T> objects) =>
			AddOperations(objects, null, obj => new BulkDeleteOperationDescriptor<T>(obj));

		public BulkRequestDescriptor DeleteMany(IndexName index, IEnumerable<Id> ids) =>
			AddOperations(ids, null, id => new BulkDeleteOperationDescriptor(id).Index(index));

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

			_operations.AddRange(operations);
			return Self;
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
				case "long":
					return FieldType.Long;
				case "text":
					return FieldType.Text;
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
				case FieldType.Long:
					writer.WriteStringValue("long");
					return;
				case FieldType.Text:
					writer.WriteStringValue("text");
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



	//public partial class Script
	//{
	//	public static implicit operator Script(InlineScript inlineScript) => new (inlineScript);
	//}

	public class DocType { }

	public partial class ElasticsearchClient
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
			var descriptor = new CreateRequestDescriptor<TDocument>(document);
			configureRequest?.Invoke(descriptor);
			return DoRequest<CreateRequestDescriptor<TDocument>, CreateResponse>(descriptor);
		}

		public Task<CreateResponse> CreateAsync<TDocument>(TDocument document, Action<CreateRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
		{
			var descriptor = new CreateRequestDescriptor<TDocument>(document);
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

		public SourceResponse<TDocument> Source<TDocument>(DocumentPath<TDocument> id, Action<SourceRequestDescriptor<TDocument>> configureRequest = null)
		{
			var descriptor = new SourceRequestDescriptor<TDocument>(document: id.Document, index: id?.Self?.Index ?? typeof(TDocument), id: id?.Self?.Id ?? Id.From(id.Document));
			configureRequest?.Invoke(descriptor);
			return DoRequest<SourceRequestDescriptor<TDocument>, SourceResponse<TDocument>>(descriptor);
		}

		public CountResponse Count<TDocument>(Action<CountRequestDescriptor<TDocument>> configureRequest = null)
		{
			var descriptor = new CountRequestDescriptor<TDocument>();
			configureRequest?.Invoke(descriptor);
			descriptor.BeforeRequest();
			return DoRequest<CountRequestDescriptor<TDocument>, CountResponse>(descriptor);
		}

		public Task<CountResponse> CountAsync<TDocument>(Action<CountRequestDescriptor<TDocument>> configureRequest = null, CancellationToken cancellationToken = default)
		{
			var descriptor = new CountRequestDescriptor<TDocument>();
			configureRequest?.Invoke(descriptor);
			descriptor.BeforeRequest();
			return DoRequestAsync<CountRequestDescriptor<TDocument>, CountResponse>(descriptor);
		}
	}

	//public sealed partial class DeleteRequestDescriptor<TDocument> : RequestDescriptorBase<DeleteRequestDescriptor<TDocument>, DeleteRequestParameters>
	//{
	//	public DeleteRequestDescriptor(IndexName index, Id id) : base(r => r.Required("index", index).Required("id", id))
	//	{
	//	}

	//	public DeleteRequestDescriptor(Id id) : this(typeof(TDocument), id)
	//	{
	//	}

	//	public DeleteRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Id.From(documentWithId)) { }

	//	internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespaceDelete;
	//	protected override HttpMethod HttpMethod => HttpMethod.DELETE;
	//	protected override bool SupportsBody => false;
	//	public DeleteRequestDescriptor<TDocument> IfPrimaryTerm(long? ifPrimaryTerm) => Qs("if_primary_term", ifPrimaryTerm);
	//	public DeleteRequestDescriptor<TDocument> IfSeqNo(long? ifSeqNo) => Qs("if_seq_no", ifSeqNo);
	//	public DeleteRequestDescriptor<TDocument> Refresh(Refresh? refresh) => Qs("refresh", refresh);
	//	public DeleteRequestDescriptor<TDocument> Routing(string? routing) => Qs("routing", routing);
	//	public DeleteRequestDescriptor<TDocument> Timeout(Time? timeout) => Qs("timeout", timeout);
	//	public DeleteRequestDescriptor<TDocument> Version(long? version) => Qs("version", version);
	//	public DeleteRequestDescriptor<TDocument> VersionType(VersionType? versionType) => Qs("version_type", versionType);
	//	public DeleteRequestDescriptor<TDocument> WaitForActiveShards(WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	//	public DeleteRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));
	//	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings) => throw new NotImplementedException();
	//}

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

		public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer) => SourceSerialisation.Serialize(Document, writer, sourceSerializer);
	}

	//public sealed partial class CreateRequestDescriptor<TDocument> : ICustomJsonWriter
	//{
	//	public CreateRequestDescriptor(TDocument documentWithId, IndexName index = null, Id id = null) : this(index ?? typeof(TDocument), id ?? Elasticsearch.Id.From(documentWithId)) => DocumentFromPath(documentWithId);

	//	private void DocumentFromPath(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);

	//	public CreateRequestDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.RouteValues.Required("index", v));

	//	public void WriteJson(Utf8JsonWriter writer, Serializer sourceSerializer)
	//	{
	//		SourceSerialisation.Serialize(DocumentValue, writer, sourceSerializer);
	//	}

	//	// TODO: We should be able to generate these for optional params
	//	public CreateRequestDescriptor<TDocument> Id(Id id)
	//	{
	//		RouteValues.Optional("id", id);
	//		return this;
	//	}
	//}

	//public sealed partial class CreateRequestDescriptor<TDocument>
	//{
	//	public CreateRequestDescriptor<TDocument> Document(TDocument document) => Assign(document, (a, v) => a.DocumentValue = v);
	//}

	public sealed partial class UpdateRequestDescriptor<TDocument, TPartialDocument>
	{
		public UpdateRequestDescriptor<TDocument, TPartialDocument> Document(TDocument document)
		{
			DocumentValue = document;
			return Self;
		}

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
			if (Pit is not null && !string.IsNullOrEmpty(Pit.Id ?? string.Empty) && routeValues.ContainsKey("index"))
			{
				routeValues.Remove("index");
			}

			return base.ResolveUrl(routeValues, settings);
		}
	}



	public sealed partial class PointInTimeReferenceDescriptor
	{
		public PointInTimeReferenceDescriptor(string id) => IdValue = id;
	}

	public sealed partial class SearchRequestDescriptor
	{
		public SearchRequestDescriptor Index(Indices index)
		{
			RouteValues.Optional("index", index);
			return Self;
		}

		public SearchRequestDescriptor Pit(string id, Action<PointInTimeReferenceDescriptor> configure)
		{
			PitValue = null;
			PitDescriptorAction = null;
			configure += a => a.Id(id);
			PitDescriptorAction = configure;
			return Self;
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

		//private partial void AfterStartObject(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		//{
		//}
	}

	public sealed partial class SearchRequestDescriptor<TDocument>
	{
		public SearchRequestDescriptor<TDocument> Pit(string id, Action<PointInTimeReferenceDescriptor> configure)
		{
			PitValue = null;
			PitDescriptorAction = null;
			configure += a => a.Id(id);
			PitDescriptorAction = configure;
			return Self;
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
	}

	public sealed partial class CountRequestDescriptor
	{
		public CountRequestDescriptor Index(Indices indices)
		{
			RouteValues.Optional("index", indices);
			return Self;
		}

		public CountRequestDescriptor Query(Func<QueryContainerDescriptor, QueryContainer> configure)
		{
			var container = configure?.Invoke(new QueryContainerDescriptor());
			QueryValue = container;
			return Self;
		}
	}

	public partial class SearchRequest<TInferDocument>
	{
		public SearchRequest(Indices? indices) : base(indices)
		{
		}
	}
}

namespace Elastic.Clients.Elasticsearch.Eql
{
	public partial class GetEqlResponse<TEvent>
	{
		private IReadOnlyCollection<HitsEvent<TEvent>> _events;
		private IReadOnlyCollection<HitsSequence<TEvent>> _sequences;


		[JsonIgnore]
		public IReadOnlyCollection<HitsEvent<TEvent>> Events =>
			_events ??= Hits?.Events ?? EmptyReadOnly<HitsEvent<TEvent>>.Collection;

		[JsonIgnore]
		public IReadOnlyCollection<HitsSequence<TEvent>> Sequences =>
			_sequences ??= Hits?.Sequences ?? EmptyReadOnly<HitsSequence<TEvent>>.Collection;

		[JsonIgnore]
		public long Total => Hits?.Total.Value ?? -1;
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

	public partial class QueryContainer
	{
		// TODO - Generate more of these!
		public TermQuery Term => Variant as TermQuery;
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



	public sealed partial class QueryContainerDescriptor/*<TDocument>*/
	{
		public void MatchAll() =>
			Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

		// TODO - NAME IS MISSING

		//public void Term<TDocument, TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null, string name = null) =>
		//	Term(t => t.Field(field).Value(value).Boost(boost).Name(name));

		public void Term<TDocument, TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
				Term(t => t.Field(field).Value(value).Boost(boost));
	}

	public sealed partial class QueryContainerDescriptor<TDocument>
	{
		public void MatchAll() =>
			Set<MatchAllQueryDescriptor>(_ => { }, "match_all");

		public void Term<TValue>(Expression<Func<TDocument, TValue>> field, object value, float? boost = null) =>
			Term(t => t.Field(field).Value(value).Boost(boost));
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
	//public partial class EpochMillis
	//{
	//	public EpochMillis() : base(1) { } // TODO: This is temp
	//}

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

namespace Elastic.Clients.Elasticsearch.AsyncSearch
{
	public partial class GetAsyncSearchRequest
	{
		// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
		internal override void BeforeRequest() => TypedKeys = true;
	}

	public sealed partial class AsyncSearchSubmitRequestDescriptor<TDocument>
	{
		public AsyncSearchSubmitRequestDescriptor<TDocument> MatchAll()
		{
			Query(new MatchAllQuery());
			return Self;
		}
	}

	public sealed partial class GetAsyncSearchRequestDescriptor
	{
		// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
		internal override void BeforeRequest() => TypedKeys(true);
	}

	public sealed partial class GetAsyncSearchRequestDescriptor<TDocument>
	{
		// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
		internal override void BeforeRequest() => TypedKeys(true);
	}

	public partial class AsyncSearchSubmitRequest
	{
		// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
		internal override void BeforeRequest() => TypedKeys = true;
	}

	public partial class AsyncSearchSubmitRequestDescriptor/*<TDocument>*/
	{
		// Any request may contain aggregations so we force typed_keys in order to successfully deserialise them.
		internal override void BeforeRequest() => TypedKeys(true);
	}
}
