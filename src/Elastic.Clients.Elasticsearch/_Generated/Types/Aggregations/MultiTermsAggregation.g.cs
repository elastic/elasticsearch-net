// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations
{
	internal sealed class MultiTermsAggregationConverter : JsonConverter<MultiTermsAggregation>
	{
		public override MultiTermsAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			var variant = new MultiTermsAggregation();
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					var property = reader.GetString();
					if (property == "collect_mode")
					{
						variant.CollectMode = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode?>(ref reader, options);
						continue;
					}

					if (property == "meta")
					{
						variant.Meta = JsonSerializer.Deserialize<Dictionary<string, object>?>(ref reader, options);
						continue;
					}

					if (property == "min_doc_count")
					{
						variant.MinDocCount = JsonSerializer.Deserialize<long?>(ref reader, options);
						continue;
					}

					if (property == "name")
					{
						variant.Name = JsonSerializer.Deserialize<string?>(ref reader, options);
						continue;
					}

					if (property == "order")
					{
						variant.Order = JsonSerializer.Deserialize<IEnumerable<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>?>(ref reader, options);
						continue;
					}

					if (property == "shard_min_doc_count")
					{
						variant.ShardMinDocCount = JsonSerializer.Deserialize<long?>(ref reader, options);
						continue;
					}

					if (property == "shard_size")
					{
						variant.ShardSize = JsonSerializer.Deserialize<int?>(ref reader, options);
						continue;
					}

					if (property == "show_term_doc_count_error")
					{
						variant.ShowTermDocCountError = JsonSerializer.Deserialize<bool?>(ref reader, options);
						continue;
					}

					if (property == "size")
					{
						variant.Size = JsonSerializer.Deserialize<int?>(ref reader, options);
						continue;
					}

					if (property == "terms")
					{
						variant.Terms = JsonSerializer.Deserialize<IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.MultiTermLookup>>(ref reader, options);
						continue;
					}
				}
			}

			return variant;
		}

		public override void Write(Utf8JsonWriter writer, MultiTermsAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			if (value.CollectMode is not null)
			{
				writer.WritePropertyName("collect_mode");
				JsonSerializer.Serialize(writer, value.CollectMode, options);
			}

			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (value.MinDocCount.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(value.MinDocCount.Value);
			}

			if (!string.IsNullOrEmpty(value.Name))
			{
				writer.WritePropertyName("name");
				writer.WriteStringValue(value.Name);
			}

			if (value.Order is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, value.Order, options);
			}

			if (value.ShardMinDocCount.HasValue)
			{
				writer.WritePropertyName("shard_min_doc_count");
				writer.WriteNumberValue(value.ShardMinDocCount.Value);
			}

			if (value.ShardSize.HasValue)
			{
				writer.WritePropertyName("shard_size");
				writer.WriteNumberValue(value.ShardSize.Value);
			}

			if (value.ShowTermDocCountError.HasValue)
			{
				writer.WritePropertyName("show_term_doc_count_error");
				writer.WriteBooleanValue(value.ShowTermDocCountError.Value);
			}

			if (value.Size.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(value.Size.Value);
			}

			writer.WritePropertyName("terms");
			JsonSerializer.Serialize(writer, value.Terms, options);
			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(MultiTermsAggregationConverter))]
	public sealed partial class MultiTermsAggregation : Aggregation
	{
		public MultiTermsAggregation(string name) => Name = name;
		internal MultiTermsAggregation()
		{
		}

		public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectMode { get; set; }

		public Dictionary<string, object>? Meta { get; set; }

		public long? MinDocCount { get; set; }

		public override string? Name { get; internal set; }

		[JsonConverter(typeof(AggregateOrderConverter))]
		public IEnumerable<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? Order { get; set; }

		public long? ShardMinDocCount { get; set; }

		public int? ShardSize { get; set; }

		public bool? ShowTermDocCountError { get; set; }

		public int? Size { get; set; }

		public IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.MultiTermLookup> Terms { get; set; }
	}

	public sealed partial class MultiTermsAggregationDescriptor<TDocument> : SerializableDescriptorBase<MultiTermsAggregationDescriptor<TDocument>>
	{
		internal MultiTermsAggregationDescriptor(Action<MultiTermsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		public MultiTermsAggregationDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.MultiTermLookup> TermsValue { get; set; }

		private MultiTermLookupDescriptor<TDocument> TermsDescriptor { get; set; }

		private Action<MultiTermLookupDescriptor<TDocument>> TermsDescriptorAction { get; set; }

		private Action<MultiTermLookupDescriptor<TDocument>>[] TermsDescriptorActions { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectModeValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private long? MinDocCountValue { get; set; }

		private IEnumerable<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? OrderValue { get; set; }

		private long? ShardMinDocCountValue { get; set; }

		private int? ShardSizeValue { get; set; }

		private bool? ShowTermDocCountErrorValue { get; set; }

		private int? SizeValue { get; set; }

		public MultiTermsAggregationDescriptor<TDocument> Terms(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.MultiTermLookup> terms)
		{
			TermsDescriptor = null;
			TermsDescriptorAction = null;
			TermsDescriptorActions = null;
			TermsValue = terms;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> Terms(MultiTermLookupDescriptor<TDocument> descriptor)
		{
			TermsValue = null;
			TermsDescriptorAction = null;
			TermsDescriptorActions = null;
			TermsDescriptor = descriptor;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> Terms(Action<MultiTermLookupDescriptor<TDocument>> configure)
		{
			TermsValue = null;
			TermsDescriptor = null;
			TermsDescriptorActions = null;
			TermsDescriptorAction = configure;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> Terms(params Action<MultiTermLookupDescriptor<TDocument>>[] configure)
		{
			TermsValue = null;
			TermsDescriptor = null;
			TermsDescriptorAction = null;
			TermsDescriptorActions = configure;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> CollectMode(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? collectMode)
		{
			CollectModeValue = collectMode;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> MinDocCount(long? minDocCount)
		{
			MinDocCountValue = minDocCount;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> Order(IEnumerable<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? order)
		{
			OrderValue = order;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> ShardMinDocCount(long? shardMinDocCount)
		{
			ShardMinDocCountValue = shardMinDocCount;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> ShardSize(int? shardSize)
		{
			ShardSizeValue = shardSize;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> ShowTermDocCountError(bool? showTermDocCountError = true)
		{
			ShowTermDocCountErrorValue = showTermDocCountError;
			return Self;
		}

		public MultiTermsAggregationDescriptor<TDocument> Size(int? size)
		{
			SizeValue = size;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("multi_terms");
			writer.WriteStartObject();
			if (TermsDescriptor is not null)
			{
				writer.WritePropertyName("terms");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, TermsDescriptor, options);
				writer.WriteEndArray();
			}
			else if (TermsDescriptorAction is not null)
			{
				writer.WritePropertyName("terms");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new MultiTermLookupDescriptor<TDocument>(TermsDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (TermsDescriptorActions is not null)
			{
				writer.WritePropertyName("terms");
				writer.WriteStartArray();
				foreach (var action in TermsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new MultiTermLookupDescriptor<TDocument>(action), options);
				}

				writer.WriteEndArray();
			}
			else
			{
				writer.WritePropertyName("terms");
				JsonSerializer.Serialize(writer, TermsValue, options);
			}

			if (CollectModeValue is not null)
			{
				writer.WritePropertyName("collect_mode");
				JsonSerializer.Serialize(writer, CollectModeValue, options);
			}

			if (MinDocCountValue.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(MinDocCountValue.Value);
			}

			if (OrderValue is not null)
			{
				writer.WritePropertyName("order");
				SingleOrManySerializationHelper.Serialize<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>(OrderValue, writer, options);
			}

			if (ShardMinDocCountValue.HasValue)
			{
				writer.WritePropertyName("shard_min_doc_count");
				writer.WriteNumberValue(ShardMinDocCountValue.Value);
			}

			if (ShardSizeValue.HasValue)
			{
				writer.WritePropertyName("shard_size");
				writer.WriteNumberValue(ShardSizeValue.Value);
			}

			if (ShowTermDocCountErrorValue.HasValue)
			{
				writer.WritePropertyName("show_term_doc_count_error");
				writer.WriteBooleanValue(ShowTermDocCountErrorValue.Value);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class MultiTermsAggregationDescriptor : SerializableDescriptorBase<MultiTermsAggregationDescriptor>
	{
		internal MultiTermsAggregationDescriptor(Action<MultiTermsAggregationDescriptor> configure) => configure.Invoke(this);
		public MultiTermsAggregationDescriptor() : base()
		{
		}

		private IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.MultiTermLookup> TermsValue { get; set; }

		private MultiTermLookupDescriptor TermsDescriptor { get; set; }

		private Action<MultiTermLookupDescriptor> TermsDescriptorAction { get; set; }

		private Action<MultiTermLookupDescriptor>[] TermsDescriptorActions { get; set; }

		private Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectModeValue { get; set; }

		private Dictionary<string, object>? MetaValue { get; set; }

		private long? MinDocCountValue { get; set; }

		private IEnumerable<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? OrderValue { get; set; }

		private long? ShardMinDocCountValue { get; set; }

		private int? ShardSizeValue { get; set; }

		private bool? ShowTermDocCountErrorValue { get; set; }

		private int? SizeValue { get; set; }

		public MultiTermsAggregationDescriptor Terms(IEnumerable<Elastic.Clients.Elasticsearch.Aggregations.MultiTermLookup> terms)
		{
			TermsDescriptor = null;
			TermsDescriptorAction = null;
			TermsDescriptorActions = null;
			TermsValue = terms;
			return Self;
		}

		public MultiTermsAggregationDescriptor Terms(MultiTermLookupDescriptor descriptor)
		{
			TermsValue = null;
			TermsDescriptorAction = null;
			TermsDescriptorActions = null;
			TermsDescriptor = descriptor;
			return Self;
		}

		public MultiTermsAggregationDescriptor Terms(Action<MultiTermLookupDescriptor> configure)
		{
			TermsValue = null;
			TermsDescriptor = null;
			TermsDescriptorActions = null;
			TermsDescriptorAction = configure;
			return Self;
		}

		public MultiTermsAggregationDescriptor Terms(params Action<MultiTermLookupDescriptor>[] configure)
		{
			TermsValue = null;
			TermsDescriptor = null;
			TermsDescriptorAction = null;
			TermsDescriptorActions = configure;
			return Self;
		}

		public MultiTermsAggregationDescriptor CollectMode(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? collectMode)
		{
			CollectModeValue = collectMode;
			return Self;
		}

		public MultiTermsAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
			return Self;
		}

		public MultiTermsAggregationDescriptor MinDocCount(long? minDocCount)
		{
			MinDocCountValue = minDocCount;
			return Self;
		}

		public MultiTermsAggregationDescriptor Order(IEnumerable<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? order)
		{
			OrderValue = order;
			return Self;
		}

		public MultiTermsAggregationDescriptor ShardMinDocCount(long? shardMinDocCount)
		{
			ShardMinDocCountValue = shardMinDocCount;
			return Self;
		}

		public MultiTermsAggregationDescriptor ShardSize(int? shardSize)
		{
			ShardSizeValue = shardSize;
			return Self;
		}

		public MultiTermsAggregationDescriptor ShowTermDocCountError(bool? showTermDocCountError = true)
		{
			ShowTermDocCountErrorValue = showTermDocCountError;
			return Self;
		}

		public MultiTermsAggregationDescriptor Size(int? size)
		{
			SizeValue = size;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("multi_terms");
			writer.WriteStartObject();
			if (TermsDescriptor is not null)
			{
				writer.WritePropertyName("terms");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, TermsDescriptor, options);
				writer.WriteEndArray();
			}
			else if (TermsDescriptorAction is not null)
			{
				writer.WritePropertyName("terms");
				writer.WriteStartArray();
				JsonSerializer.Serialize(writer, new MultiTermLookupDescriptor(TermsDescriptorAction), options);
				writer.WriteEndArray();
			}
			else if (TermsDescriptorActions is not null)
			{
				writer.WritePropertyName("terms");
				writer.WriteStartArray();
				foreach (var action in TermsDescriptorActions)
				{
					JsonSerializer.Serialize(writer, new MultiTermLookupDescriptor(action), options);
				}

				writer.WriteEndArray();
			}
			else
			{
				writer.WritePropertyName("terms");
				JsonSerializer.Serialize(writer, TermsValue, options);
			}

			if (CollectModeValue is not null)
			{
				writer.WritePropertyName("collect_mode");
				JsonSerializer.Serialize(writer, CollectModeValue, options);
			}

			if (MinDocCountValue.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(MinDocCountValue.Value);
			}

			if (OrderValue is not null)
			{
				writer.WritePropertyName("order");
				SingleOrManySerializationHelper.Serialize<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>(OrderValue, writer, options);
			}

			if (ShardMinDocCountValue.HasValue)
			{
				writer.WritePropertyName("shard_min_doc_count");
				writer.WriteNumberValue(ShardMinDocCountValue.Value);
			}

			if (ShardSizeValue.HasValue)
			{
				writer.WritePropertyName("shard_size");
				writer.WriteNumberValue(ShardSizeValue.Value);
			}

			if (ShowTermDocCountErrorValue.HasValue)
			{
				writer.WritePropertyName("show_term_doc_count_error");
				writer.WriteBooleanValue(ShowTermDocCountErrorValue.Value);
			}

			if (SizeValue.HasValue)
			{
				writer.WritePropertyName("size");
				writer.WriteNumberValue(SizeValue.Value);
			}

			writer.WriteEndObject();
			if (MetaValue is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, MetaValue, options);
			}

			writer.WriteEndObject();
		}
	}
}