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
internal sealed class CardinalityAggregationConverter : JsonConverter<CardinalityAggregation>
{
	public override CardinalityAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		reader.Read();
		var aggName = reader.GetString();
		if (aggName != "cardinality")
			throw new JsonException("Unexpected JSON detected.");
		var agg = new CardinalityAggregation(aggName);
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals("execution_hint"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Aggregations.CardinalityExecutionMode?>(ref reader, options);
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

				if (reader.ValueTextEquals("precision_threshold"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<int?>(ref reader, options);
					if (value is not null)
					{
						agg.PrecisionThreshold = value;
					}

					continue;
				}

				if (reader.ValueTextEquals("rehash"))
				{
					reader.Read();
					var value = JsonSerializer.Deserialize<bool?>(ref reader, options);
					if (value is not null)
					{
						agg.Rehash = value;
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
			}
		}

		return agg;
	}

	public override void Write(Utf8JsonWriter writer, CardinalityAggregation value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("cardinality");
		writer.WriteStartObject();
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

		if (value.Missing is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, value.Missing, options);
		}

		if (value.PrecisionThreshold.HasValue)
		{
			writer.WritePropertyName("precision_threshold");
			writer.WriteNumberValue(value.PrecisionThreshold.Value);
		}

		if (value.Rehash.HasValue)
		{
			writer.WritePropertyName("rehash");
			writer.WriteBooleanValue(value.Rehash.Value);
		}

		if (value.Script is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, value.Script, options);
		}

		writer.WriteEndObject();
		if (value.Meta is not null)
		{
			writer.WritePropertyName("meta");
			JsonSerializer.Serialize(writer, value.Meta, options);
		}

		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(CardinalityAggregationConverter))]
public sealed partial class CardinalityAggregation : SearchAggregation
{
	public CardinalityAggregation(string name, Field field) : this(name) => Field = field;
	public CardinalityAggregation(string name) => Name = name;
	internal CardinalityAggregation()
	{
	}

	public Elastic.Clients.Elasticsearch.Aggregations.CardinalityExecutionMode? ExecutionHint { get; set; }

	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	public Dictionary<string, object>? Meta { get; set; }

	public FieldValue? Missing { get; set; }

	public override string? Name { get; internal set; }

	public int? PrecisionThreshold { get; set; }

	public bool? Rehash { get; set; }

	public Elastic.Clients.Elasticsearch.Script? Script { get; set; }
}

public sealed partial class CardinalityAggregationDescriptor<TDocument> : SerializableDescriptor<CardinalityAggregationDescriptor<TDocument>>
{
	internal CardinalityAggregationDescriptor(Action<CardinalityAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
	public CardinalityAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.CardinalityExecutionMode? ExecutionHintValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private Dictionary<string, object>? MetaValue { get; set; }

	private FieldValue? MissingValue { get; set; }

	private int? PrecisionThresholdValue { get; set; }

	private bool? RehashValue { get; set; }

	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

	public CardinalityAggregationDescriptor<TDocument> ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.CardinalityExecutionMode? executionHint)
	{
		ExecutionHintValue = executionHint;
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> Missing(FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> PrecisionThreshold(int? precisionThreshold)
	{
		PrecisionThresholdValue = precisionThreshold;
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> Rehash(bool? rehash = true)
	{
		RehashValue = rehash;
		return Self;
	}

	public CardinalityAggregationDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("cardinality");
		writer.WriteStartObject();
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

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (PrecisionThresholdValue.HasValue)
		{
			writer.WritePropertyName("precision_threshold");
			writer.WriteNumberValue(PrecisionThresholdValue.Value);
		}

		if (RehashValue.HasValue)
		{
			writer.WritePropertyName("rehash");
			writer.WriteBooleanValue(RehashValue.Value);
		}

		if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
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

public sealed partial class CardinalityAggregationDescriptor : SerializableDescriptor<CardinalityAggregationDescriptor>
{
	internal CardinalityAggregationDescriptor(Action<CardinalityAggregationDescriptor> configure) => configure.Invoke(this);
	public CardinalityAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.CardinalityExecutionMode? ExecutionHintValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private Dictionary<string, object>? MetaValue { get; set; }

	private FieldValue? MissingValue { get; set; }

	private int? PrecisionThresholdValue { get; set; }

	private bool? RehashValue { get; set; }

	private Elastic.Clients.Elasticsearch.Script? ScriptValue { get; set; }

	public CardinalityAggregationDescriptor ExecutionHint(Elastic.Clients.Elasticsearch.Aggregations.CardinalityExecutionMode? executionHint)
	{
		ExecutionHintValue = executionHint;
		return Self;
	}

	public CardinalityAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public CardinalityAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public CardinalityAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public CardinalityAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public CardinalityAggregationDescriptor Missing(FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	public CardinalityAggregationDescriptor PrecisionThreshold(int? precisionThreshold)
	{
		PrecisionThresholdValue = precisionThreshold;
		return Self;
	}

	public CardinalityAggregationDescriptor Rehash(bool? rehash = true)
	{
		RehashValue = rehash;
		return Self;
	}

	public CardinalityAggregationDescriptor Script(Elastic.Clients.Elasticsearch.Script? script)
	{
		ScriptValue = script;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("cardinality");
		writer.WriteStartObject();
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

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (PrecisionThresholdValue.HasValue)
		{
			writer.WritePropertyName("precision_threshold");
			writer.WriteNumberValue(PrecisionThresholdValue.Value);
		}

		if (RehashValue.HasValue)
		{
			writer.WritePropertyName("rehash");
			writer.WriteBooleanValue(RehashValue.Value);
		}

		if (ScriptValue is not null)
		{
			writer.WritePropertyName("script");
			JsonSerializer.Serialize(writer, ScriptValue, options);
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