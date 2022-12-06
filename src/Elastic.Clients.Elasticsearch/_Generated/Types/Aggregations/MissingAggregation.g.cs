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
internal sealed class MissingAggregationConverter : JsonConverter<MissingAggregation>
{
	public override MissingAggregation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		reader.Read();
		var aggName = reader.GetString();
		if (aggName != "missing")
			throw new JsonException("Unexpected JSON detected.");
		var agg = new MissingAggregation(aggName);
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
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

	public override void Write(Utf8JsonWriter writer, MissingAggregation value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("missing");
		writer.WriteStartObject();
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

[JsonConverter(typeof(MissingAggregationConverter))]
public sealed partial class MissingAggregation : SearchAggregation
{
	public MissingAggregation(string name) => Name = name;
	internal MissingAggregation()
	{
	}

	public Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? Aggregations { get; set; }

	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	public IDictionary<string, object>? Meta { get; set; }

	public FieldValue? Missing { get; set; }

	public override string? Name { get; internal set; }
}

public sealed partial class MissingAggregationDescriptor<TDocument> : SerializableDescriptor<MissingAggregationDescriptor<TDocument>>
{
	internal MissingAggregationDescriptor(Action<MissingAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);
	public MissingAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument> AggregationsDescriptor { get; set; }

	private Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument>> AggregationsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private IDictionary<string, object>? MetaValue { get; set; }

	private FieldValue? MissingValue { get; set; }

	public MissingAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
	{
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = null;
		AggregationsValue = aggregations;
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument> descriptor)
	{
		AggregationsValue = null;
		AggregationsDescriptorAction = null;
		AggregationsDescriptor = descriptor;
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor<TDocument>> configure)
	{
		AggregationsValue = null;
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = configure;
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Missing(FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("missing");
		writer.WriteStartObject();
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

public sealed partial class MissingAggregationDescriptor : SerializableDescriptor<MissingAggregationDescriptor>
{
	internal MissingAggregationDescriptor(Action<MissingAggregationDescriptor> configure) => configure.Invoke(this);
	public MissingAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? AggregationsValue { get; set; }

	private Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor AggregationsDescriptor { get; set; }

	private Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor> AggregationsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }

	private IDictionary<string, object>? MetaValue { get; set; }

	private FieldValue? MissingValue { get; set; }

	public MissingAggregationDescriptor Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDictionary? aggregations)
	{
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = null;
		AggregationsValue = aggregations;
		return Self;
	}

	public MissingAggregationDescriptor Aggregations(Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor descriptor)
	{
		AggregationsValue = null;
		AggregationsDescriptorAction = null;
		AggregationsDescriptor = descriptor;
		return Self;
	}

	public MissingAggregationDescriptor Aggregations(Action<Elastic.Clients.Elasticsearch.Aggregations.AggregationDescriptor> configure)
	{
		AggregationsValue = null;
		AggregationsDescriptor = null;
		AggregationsDescriptorAction = configure;
		return Self;
	}

	public MissingAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public MissingAggregationDescriptor Missing(FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("missing");
		writer.WriteStartObject();
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