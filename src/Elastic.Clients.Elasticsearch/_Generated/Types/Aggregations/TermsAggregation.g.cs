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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Aggregations;
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
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode?>(ref reader, options);
					if (value is not null)
					{
						agg.CollectMode = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("exclude"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsExclude?>(ref reader, options);
					if (value is not null)
					{
						agg.Exclude = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("execution_hint"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint?>(ref reader, options);
					if (value is not null)
					{
						agg.ExecutionHint = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("field"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Field?>(ref reader, options);
					if (value is not null)
					{
						agg.Field = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("format"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<string?>(ref reader, options);
					if (value is not null)
					{
						agg.Format = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("include"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.TermsInclude?>(ref reader, options);
					if (value is not null)
					{
						agg.Include = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("min_doc_count"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<int?>(ref reader, options);
					if (value is not null)
					{
						agg.MinDocCount = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("missing"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<FieldValue?>(ref reader, options);
					if (value is not null)
					{
						agg.Missing = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("missing_bucket"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
					if (value is not null)
					{
						agg.MissingBucket = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("missing_order"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.MissingOrder?>(ref reader, options);
					if (value is not null)
					{
						agg.MissingOrder = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("order"))
				{
					reader.Read();
					var value = SingleOrManySerializationHelper.Deserialize<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>(ref reader, options);
					if (value is not null)
					{
						agg.Order = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("script"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Script?>(ref reader, options);
					if (value is not null)
					{
						agg.Script = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("shard_size"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<int?>(ref reader, options);
					if (value is not null)
					{
						agg.ShardSize = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("show_term_doc_count_error"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
					if (value is not null)
					{
						agg.ShowTermDocCountError = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("size"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<int?>(ref reader, options);
					if (value is not null)
					{
						agg.Size = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("value_type"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<string?>(ref reader, options);
					if (value is not null)
					{
						agg.ValueType = value;
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

		if (!string.IsNullOrEmpty(value.Format))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(value.Format);
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

		if (value.MissingBucket.HasValue)
		{
			writer.WritePropertyName("missing_bucket");
			writer.WriteBooleanValue(value.MissingBucket.Value);
		}

		if (value.MissingOrder is not null)
		{
			writer.WritePropertyName("missing_order");
			JsonSerializer.Serialize(writer, value.MissingOrder, options);
		}

		if (value.Order is not null)
		{
			writer.WritePropertyName("order");
			SingleOrManySerializationHelper.Serialize<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>(value.Order, writer, options);
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

		if (!string.IsNullOrEmpty(value.ValueType))
		{
			writer.WritePropertyName("value_type");
			writer.WriteStringValue(value.ValueType);
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
public sealed partial class TermsAggregation : SearchAggregation
{
	public TermsAggregation(string name) => Name = name;
	internal TermsAggregation()
	{
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? Aggregations { get; set; }

	public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectMode { get; set; }

	public Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? Exclude { get; set; }

	public Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHint { get; set; }

	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	public string? Format { get; set; }

	public Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? Include { get; set; }

	public Dictionary<string, object>? Meta { get; set; }

	public int? MinDocCount { get; set; }

	public FieldValue? Missing { get; set; }

	public bool? MissingBucket { get; set; }

	public Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrder { get; set; }

	public override string? Name { get; internal set; }

	[JsonConverter(typeof(AggregateOrderConverter))]
	public ICollection<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? Order { get; set; }

	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }

	public int? ShardSize { get; set; }

	public bool? ShowTermDocCountError { get; set; }

	public int? Size { get; set; }

	public string? ValueType { get; set; }
}

public sealed partial class TermsAggregationDescriptor<TDocument> : SerializableDescriptor<TermsAggregationDescriptor<TDocument>>
{
	internal TermsAggregationDescriptor(Action<TermsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
	public TermsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument> AggregationsDescriptor { get; set; }

	private Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument>> AggregationsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectModeValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? ExcludeValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHintValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private string? FormatValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? IncludeValue { get; set; }

	private Dictionary<string, object>? MetaValue { get; set; }

	private int? MinDocCountValue { get; set; }

	private FieldValue? MissingValue { get; set; }

	private bool? MissingBucketValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrderValue { get; set; }

	private ICollection<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? OrderValue { get; set; }

	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

	private int? ShardSizeValue { get; set; }

	private bool? ShowTermDocCountErrorValue { get; set; }

	private int? SizeValue { get; set; }

	private string? ValueTypeValue { get; set; }

	public TermsAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
	{
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = null;
		AggregationsValue = aggregations;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument> descriptor)
	{
		AggregationsValue = null;
		AggregationsDescriptorAction = null;
		AggregationsDescriptor = descriptor;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument>> configure)
	{
		AggregationsValue = null;
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = configure;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> CollectMode(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? collectMode)
	{
		CollectModeValue = collectMode;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? exclude)
	{
		ExcludeValue = exclude;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? executionHint)
	{
		ExecutionHintValue = executionHint;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? include)
	{
		IncludeValue = include;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> MinDocCount(int? minDocCount)
	{
		MinDocCountValue = minDocCount;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Missing(FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> MissingBucket(bool? missingBucket = true)
	{
		MissingBucketValue = missingBucket;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? missingOrder)
	{
		MissingOrderValue = missingOrder;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Order(ICollection<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? order)
	{
		OrderValue = order;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> ShardSize(int? shardSize)
	{
		ShardSizeValue = shardSize;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> ShowTermDocCountError(bool? showTermDocCountError = true)
	{
		ShowTermDocCountErrorValue = showTermDocCountError;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	public TermsAggregationDescriptor<TDocument> ValueType(string? valueType)
	{
		ValueTypeValue = valueType;
		return Self;
	}

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

		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
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

		if (MissingBucketValue.HasValue)
		{
			writer.WritePropertyName("missing_bucket");
			writer.WriteBooleanValue(MissingBucketValue.Value);
		}

		if (MissingOrderValue is not null)
		{
			writer.WritePropertyName("missing_order");
			JsonSerializer.Serialize(writer, MissingOrderValue, options);
		}

		if (OrderValue is not null)
		{
			writer.WritePropertyName("order");
			SingleOrManySerializationHelper.Serialize<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>(OrderValue, writer, options);
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

		if (!string.IsNullOrEmpty(ValueTypeValue))
		{
			writer.WritePropertyName("value_type");
			writer.WriteStringValue(ValueTypeValue);
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
			JsonSerializer.Serialize(writer, new AggregationDescriptor<TDocument>(AggregationsDescriptorAction), options);
		}
		else if (AggregationsValue is not null)
		{
			writer.WritePropertyName("aggregations");
			JsonSerializer.Serialize(writer, AggregationsValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class TermsAggregationDescriptor : SerializableDescriptor<TermsAggregationDescriptor>
{
	internal TermsAggregationDescriptor(Action<TermsAggregationDescriptor> configure) => configure.Invoke(this);
	public TermsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor AggregationsDescriptor { get; set; }

	private Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor> AggregationsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? CollectModeValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? ExcludeValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? ExecutionHintValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private string? FormatValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? IncludeValue { get; set; }

	private Dictionary<string, object>? MetaValue { get; set; }

	private int? MinDocCountValue { get; set; }

	private FieldValue? MissingValue { get; set; }

	private bool? MissingBucketValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? MissingOrderValue { get; set; }

	private ICollection<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? OrderValue { get; set; }

	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

	private int? ShardSizeValue { get; set; }

	private bool? ShowTermDocCountErrorValue { get; set; }

	private int? SizeValue { get; set; }

	private string? ValueTypeValue { get; set; }

	public TermsAggregationDescriptor Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
	{
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = null;
		AggregationsValue = aggregations;
		return Self;
	}

	public TermsAggregationDescriptor Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor descriptor)
	{
		AggregationsValue = null;
		AggregationsDescriptorAction = null;
		AggregationsDescriptor = descriptor;
		return Self;
	}

	public TermsAggregationDescriptor Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor> configure)
	{
		AggregationsValue = null;
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = configure;
		return Self;
	}

	public TermsAggregationDescriptor CollectMode(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationCollectMode? collectMode)
	{
		CollectModeValue = collectMode;
		return Self;
	}

	public TermsAggregationDescriptor Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? exclude)
	{
		ExcludeValue = exclude;
		return Self;
	}

	public TermsAggregationDescriptor ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.TermsAggregationExecutionHint? executionHint)
	{
		ExecutionHintValue = executionHint;
		return Self;
	}

	public TermsAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsAggregationDescriptor Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	public TermsAggregationDescriptor Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? include)
	{
		IncludeValue = include;
		return Self;
	}

	public TermsAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public TermsAggregationDescriptor MinDocCount(int? minDocCount)
	{
		MinDocCountValue = minDocCount;
		return Self;
	}

	public TermsAggregationDescriptor Missing(FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	public TermsAggregationDescriptor MissingBucket(bool? missingBucket = true)
	{
		MissingBucketValue = missingBucket;
		return Self;
	}

	public TermsAggregationDescriptor MissingOrder(Elastic.Clients.Elasticsearch.Aggregations.MissingOrder? missingOrder)
	{
		MissingOrderValue = missingOrder;
		return Self;
	}

	public TermsAggregationDescriptor Order(ICollection<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>? order)
	{
		OrderValue = order;
		return Self;
	}

	public TermsAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	public TermsAggregationDescriptor ShardSize(int? shardSize)
	{
		ShardSizeValue = shardSize;
		return Self;
	}

	public TermsAggregationDescriptor ShowTermDocCountError(bool? showTermDocCountError = true)
	{
		ShowTermDocCountErrorValue = showTermDocCountError;
		return Self;
	}

	public TermsAggregationDescriptor Size(int? size)
	{
		SizeValue = size;
		return Self;
	}

	public TermsAggregationDescriptor ValueType(string? valueType)
	{
		ValueTypeValue = valueType;
		return Self;
	}

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

		if (!string.IsNullOrEmpty(FormatValue))
		{
			writer.WritePropertyName("format");
			writer.WriteStringValue(FormatValue);
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

		if (MissingBucketValue.HasValue)
		{
			writer.WritePropertyName("missing_bucket");
			writer.WriteBooleanValue(MissingBucketValue.Value);
		}

		if (MissingOrderValue is not null)
		{
			writer.WritePropertyName("missing_order");
			JsonSerializer.Serialize(writer, MissingOrderValue, options);
		}

		if (OrderValue is not null)
		{
			writer.WritePropertyName("order");
			SingleOrManySerializationHelper.Serialize<KeyValuePair<Elastic.Clients.Elasticsearch.Field, Elastic.Clients.Elasticsearch.SortOrder>>(OrderValue, writer, options);
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

		if (!string.IsNullOrEmpty(ValueTypeValue))
		{
			writer.WritePropertyName("value_type");
			writer.WriteStringValue(ValueTypeValue);
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
			JsonSerializer.Serialize(writer, new AggregationDescriptor(AggregationsDescriptorAction), options);
		}
		else if (AggregationsValue is not null)
		{
			writer.WritePropertyName("aggregations");
			JsonSerializer.Serialize(writer, AggregationsValue, options);
		}

		writer.WriteEndObject();
	}
}