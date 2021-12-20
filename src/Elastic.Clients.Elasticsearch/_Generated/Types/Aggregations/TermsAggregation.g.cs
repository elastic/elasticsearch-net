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
	internal sealed class TermsAggregationConverter : JsonConverter<TermsAggregation>
	{
		public override TermsAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unexpected JSON detected.");
			reader.Read();
			var aggName = reader.GetString();
			if (aggName != "terms")
				throw new JsonException("Unexpected JSON detected.");
			var agg = new TermsAggregation(aggName);
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("collect_mode"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode?>(ref reader, options);
						if (value is not null)
						{
							agg.CollectMode = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("exclude"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsExclude?>(ref reader, options);
						if (value is not null)
						{
							agg.Exclude = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("execution_hint"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint?>(ref reader, options);
						if (value is not null)
						{
							agg.ExecutionHint = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("field"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
						if (value is not null)
						{
							agg.Field = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("include"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsInclude?>(ref reader, options);
						if (value is not null)
						{
							agg.Include = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("min_doc_count"))
					{
						var value = JsonSerializer.Deserialize<int?>(ref reader, options);
						if (value is not null)
						{
							agg.MinDocCount = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("missing"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.Missing?>(ref reader, options);
						if (value is not null)
						{
							agg.Missing = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("missing_order"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MissingOrder?>(ref reader, options);
						if (value is not null)
						{
							agg.MissingOrder = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("missing_bucket"))
					{
						var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
						if (value is not null)
						{
							agg.MissingBucket = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("value_type"))
					{
						var value = JsonSerializer.Deserialize<string?>(ref reader, options);
						if (value is not null)
						{
							agg.ValueType = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("order"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationOrder?>(ref reader, options);
						if (value is not null)
						{
							agg.Order = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("script"))
					{
						var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Script?>(ref reader, options);
						if (value is not null)
						{
							agg.Script = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("shard_size"))
					{
						var value = JsonSerializer.Deserialize<int?>(ref reader, options);
						if (value is not null)
						{
							agg.ShardSize = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("show_term_doc_count_error"))
					{
						var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
						if (value is not null)
						{
							agg.ShowTermDocCountError = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("size"))
					{
						var value = JsonSerializer.Deserialize<int?>(ref reader, options);
						if (value is not null)
						{
							agg.Size = value;
						}

						continue;
					}
				}
			}

			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					if (reader.ValueTextEquals("meta"))
					{
						var value = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
						if (value is not null)
						{
							agg.Meta = value;
						}

						continue;
					}

					if (reader.ValueTextEquals("aggs") || reader.ValueTextEquals("aggregations"))
					{
						var value = JsonSerializer.Deserialize<AggregationDictionary>(ref reader, options);
						if (value is not null)
						{
							agg.Aggregations = value;
						}

						continue;
					}
				}
			}

			reader.Read();
			return agg;
		}

		public override void Write(Utf8JsonWriter writer, TermsAggregation value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("terms");
			writer.WriteStartObject();
			if (value.CollectMode is not null)
			{
				writer.WritePropertyName("collect_mode");
				JsonSerializer.Serialize(writer, value.CollectMode, options);
			}

			if (value.Exclude is not null)
			{
				writer.WritePropertyName("exclude");
				JsonSerializer.Serialize(writer, value.Exclude, options);
			}

			if (value.ExecutionHint is not null)
			{
				writer.WritePropertyName("execution_hint");
				JsonSerializer.Serialize(writer, value.ExecutionHint, options);
			}

			if (value.Field is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, value.Field, options);
			}

			if (value.Include is not null)
			{
				writer.WritePropertyName("include");
				JsonSerializer.Serialize(writer, value.Include, options);
			}

			if (value.MinDocCount.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(value.MinDocCount.Value);
			}

			if (value.Missing is not null)
			{
				writer.WritePropertyName("missing");
				JsonSerializer.Serialize(writer, value.Missing, options);
			}

			if (value.MissingOrder is not null)
			{
				writer.WritePropertyName("missing_order");
				JsonSerializer.Serialize(writer, value.MissingOrder, options);
			}

			if (value.MissingBucket.HasValue)
			{
				writer.WritePropertyName("missing_bucket");
				writer.WriteBooleanValue(value.MissingBucket.Value);
			}

			if (!string.IsNullOrEmpty(value.ValueType))
			{
				writer.WritePropertyName("value_type");
				writer.WriteStringValue(value.ValueType);
			}

			if (value.Order is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, value.Order, options);
			}

			if (value.Script is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, value.Script, options);
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

			writer.WriteEndObject();
			if (value.Meta is not null)
			{
				writer.WritePropertyName("meta");
				JsonSerializer.Serialize(writer, value.Meta, options);
			}

			if (value.Aggregations is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, value.Aggregations, options);
			}

			writer.WriteEndObject();
		}
	}

	[JsonConverter(typeof(TermsAggregationConverter))]
	public partial class TermsAggregation : Aggregations.BucketAggregationBase, TransformManagement.IPivotGroupByContainerVariant
	{
		public TermsAggregation(string name) : base(name)
		{
		}

		[JsonIgnore]
		string TransformManagement.IPivotGroupByContainerVariant.PivotGroupByContainerVariantName => "terms";
		[JsonInclude]
		[JsonPropertyName("collect_mode")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectMode { get; set; }

		[JsonInclude]
		[JsonPropertyName("exclude")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? Exclude { get; set; }

		[JsonInclude]
		[JsonPropertyName("execution_hint")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[JsonInclude]
		[JsonPropertyName("field")]
		public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

		[JsonInclude]
		[JsonPropertyName("include")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? Include { get; set; }

		[JsonInclude]
		[JsonPropertyName("min_doc_count")]
		public int? MinDocCount { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing")]
		public Elastic.Clients.Elasticsearch.Aggregations.Missing? Missing { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing_order")]
		public Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrder { get; set; }

		[JsonInclude]
		[JsonPropertyName("missing_bucket")]
		public bool? MissingBucket { get; set; }

		[JsonInclude]
		[JsonPropertyName("value_type")]
		public string? ValueType { get; set; }

		[JsonInclude]
		[JsonPropertyName("order")]
		public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationOrder? Order { get; set; }

		[JsonInclude]
		[JsonPropertyName("script")]
		public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

		[JsonInclude]
		[JsonPropertyName("shard_size")]
		public int? ShardSize { get; set; }

		[JsonInclude]
		[JsonPropertyName("show_term_doc_count_error")]
		public bool? ShowTermDocCountError { get; set; }

		[JsonInclude]
		[JsonPropertyName("size")]
		public int? Size { get; set; }
	}

	public sealed partial class TermsAggregationDescriptor<TDocument> : DescriptorBase<TermsAggregationDescriptor<TDocument>>
	{
		public TermsAggregationDescriptor()
		{
		}

		internal TermsAggregationDescriptor(Action<TermsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
		internal Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectModeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? ExcludeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHintValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Field? FieldValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? IncludeValue { get; private set; }

		internal int? MinDocCountValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.Missing? MissingValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrderValue { get; private set; }

		internal bool? MissingBucketValue { get; private set; }

		internal string? ValueTypeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationOrder? OrderValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Script? ScriptValue { get; private set; }

		internal int? ShardSizeValue { get; private set; }

		internal bool? ShowTermDocCountErrorValue { get; private set; }

		internal int? SizeValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; private set; }

		internal Dictionary<string, object>? MetaValue { get; private set; }

		internal Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument> AggregationsDescriptor { get; private set; }

		internal Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument>> AggregationsDescriptorAction { get; private set; }

		public TermsAggregationDescriptor<TDocument> CollectMode(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? collectMode) => Assign(collectMode, (a, v) => a.CollectModeValue = v);
		public TermsAggregationDescriptor<TDocument> Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? exclude) => Assign(exclude, (a, v) => a.ExcludeValue = v);
		public TermsAggregationDescriptor<TDocument> ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? executionHint) => Assign(executionHint, (a, v) => a.ExecutionHintValue = v);
		public TermsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field) => Assign(field, (a, v) => a.FieldValue = v);
		public TermsAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field) => Assign(field, (a, v) => a.FieldValue = v);
		public TermsAggregationDescriptor<TDocument> Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? include) => Assign(include, (a, v) => a.IncludeValue = v);
		public TermsAggregationDescriptor<TDocument> MinDocCount(int? minDocCount) => Assign(minDocCount, (a, v) => a.MinDocCountValue = v);
		public TermsAggregationDescriptor<TDocument> Missing(Elastic.Clients.Elasticsearch.Aggregations.Missing? missing) => Assign(missing, (a, v) => a.MissingValue = v);
		public TermsAggregationDescriptor<TDocument> MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? missingOrder) => Assign(missingOrder, (a, v) => a.MissingOrderValue = v);
		public TermsAggregationDescriptor<TDocument> MissingBucket(bool? missingBucket = true) => Assign(missingBucket, (a, v) => a.MissingBucketValue = v);
		public TermsAggregationDescriptor<TDocument> ValueType(string? valueType) => Assign(valueType, (a, v) => a.ValueTypeValue = v);
		public TermsAggregationDescriptor<TDocument> Order(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationOrder? order) => Assign(order, (a, v) => a.OrderValue = v);
		public TermsAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script) => Assign(script, (a, v) => a.ScriptValue = v);
		public TermsAggregationDescriptor<TDocument> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSizeValue = v);
		public TermsAggregationDescriptor<TDocument> ShowTermDocCountError(bool? showTermDocCountError = true) => Assign(showTermDocCountError, (a, v) => a.ShowTermDocCountErrorValue = v);
		public TermsAggregationDescriptor<TDocument> Size(int? size) => Assign(size, (a, v) => a.SizeValue = v);
		public TermsAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
		{
			AggregationsDescriptor = null;
			AggregationsDescriptorAction = null;
			return Assign(aggregations, (a, v) => a.AggregationsValue = v);
		}

		public TermsAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument> descriptor)
		{
			AggregationsValue = null;
			AggregationsDescriptorAction = null;
			return Assign(descriptor, (a, v) => a.AggregationsDescriptor = v);
		}

		public TermsAggregationDescriptor<TDocument> Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationContainerDescriptor<TDocument>> configure)
		{
			AggregationsValue = null;
			AggregationsDescriptorAction = null;
			return Assign(configure, (a, v) => a.AggregationsDescriptorAction = v);
		}

		public TermsAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) => Assign(selector, (a, v) => a.MetaValue = v?.Invoke(new FluentDictionary<string, object>()));
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("terms");
			writer.WriteStartObject();
			if (CollectModeValue is not null)
			{
				writer.WritePropertyName("collect_mode");
				JsonSerializer.Serialize(writer, CollectModeValue, options);
			}

			if (ExcludeValue is not null)
			{
				writer.WritePropertyName("exclude");
				JsonSerializer.Serialize(writer, ExcludeValue, options);
			}

			if (ExecutionHintValue is not null)
			{
				writer.WritePropertyName("execution_hint");
				JsonSerializer.Serialize(writer, ExecutionHintValue, options);
			}

			if (FieldValue is not null)
			{
				writer.WritePropertyName("field");
				JsonSerializer.Serialize(writer, FieldValue, options);
			}

			if (IncludeValue is not null)
			{
				writer.WritePropertyName("include");
				JsonSerializer.Serialize(writer, IncludeValue, options);
			}

			if (MinDocCountValue.HasValue)
			{
				writer.WritePropertyName("min_doc_count");
				writer.WriteNumberValue(MinDocCountValue.Value);
			}

			if (MissingValue is not null)
			{
				writer.WritePropertyName("missing");
				JsonSerializer.Serialize(writer, MissingValue, options);
			}

			if (MissingOrderValue is not null)
			{
				writer.WritePropertyName("missing_order");
				JsonSerializer.Serialize(writer, MissingOrderValue, options);
			}

			if (MissingBucketValue.HasValue)
			{
				writer.WritePropertyName("missing_bucket");
				writer.WriteBooleanValue(MissingBucketValue.Value);
			}

			if (!string.IsNullOrEmpty(ValueTypeValue))
			{
				writer.WritePropertyName("value_type");
				writer.WriteStringValue(ValueTypeValue);
			}

			if (OrderValue is not null)
			{
				writer.WritePropertyName("order");
				JsonSerializer.Serialize(writer, OrderValue, options);
			}

			if (ScriptValue is not null)
			{
				writer.WritePropertyName("script");
				JsonSerializer.Serialize(writer, ScriptValue, options);
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

			if (AggregationsDescriptor is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsDescriptor, options);
			}
			else if (AggregationsDescriptorAction is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, new AggregationContainerDescriptor<TDocument>(AggregationsDescriptorAction), options);
			}
			else if (AggregationsValue is not null)
			{
				writer.WritePropertyName("aggregations");
				JsonSerializer.Serialize(writer, AggregationsValue, options);
			}

			writer.WriteEndObject();
		}
	}
}