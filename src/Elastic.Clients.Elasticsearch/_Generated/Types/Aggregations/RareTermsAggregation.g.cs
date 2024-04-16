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

#nullable restore

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class RareTermsAggregation
{
	/// <summary>
	/// <para>Terms that should be excluded from the aggregation.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("exclude")]
	public Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? Exclude { get; set; }

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }

	/// <summary>
	/// <para>Terms that should be included in the aggregation.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("include")]
	public Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? Include { get; set; }

	/// <summary>
	/// <para>The maximum number of documents a term should appear in.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_doc_count")]
	public long? MaxDocCount { get; set; }

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("missing")]
	public Elastic.Clients.Elasticsearch.FieldValue? Missing { get; set; }

	/// <summary>
	/// <para>The precision of the internal CuckooFilters.<br/>Smaller precision leads to better approximation, but higher memory usage.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("precision")]
	public double? Precision { get; set; }
	[JsonInclude, JsonPropertyName("value_type")]
	public string? ValueType { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(RareTermsAggregation rareTermsAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.RareTerms(rareTermsAggregation);
}

public sealed partial class RareTermsAggregationDescriptor<TDocument> : SerializableDescriptor<RareTermsAggregationDescriptor<TDocument>>
{
	internal RareTermsAggregationDescriptor(Action<RareTermsAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RareTermsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? ExcludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? IncludeValue { get; set; }
	private long? MaxDocCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }
	private double? PrecisionValue { get; set; }
	private string? ValueTypeValue { get; set; }

	/// <summary>
	/// <para>Terms that should be excluded from the aggregation.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? exclude)
	{
		ExcludeValue = exclude;
		return Self;
	}

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Terms that should be included in the aggregation.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? include)
	{
		IncludeValue = include;
		return Self;
	}

	/// <summary>
	/// <para>The maximum number of documents a term should appear in.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> MaxDocCount(long? maxDocCount)
	{
		MaxDocCountValue = maxDocCount;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>The precision of the internal CuckooFilters.<br/>Smaller precision leads to better approximation, but higher memory usage.</para>
	/// </summary>
	public RareTermsAggregationDescriptor<TDocument> Precision(double? precision)
	{
		PrecisionValue = precision;
		return Self;
	}

	public RareTermsAggregationDescriptor<TDocument> ValueType(string? valueType)
	{
		ValueTypeValue = valueType;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExcludeValue is not null)
		{
			writer.WritePropertyName("exclude");
			JsonSerializer.Serialize(writer, ExcludeValue, options);
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

		if (MaxDocCountValue.HasValue)
		{
			writer.WritePropertyName("max_doc_count");
			writer.WriteNumberValue(MaxDocCountValue.Value);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (PrecisionValue.HasValue)
		{
			writer.WritePropertyName("precision");
			writer.WriteNumberValue(PrecisionValue.Value);
		}

		if (!string.IsNullOrEmpty(ValueTypeValue))
		{
			writer.WritePropertyName("value_type");
			writer.WriteStringValue(ValueTypeValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class RareTermsAggregationDescriptor : SerializableDescriptor<RareTermsAggregationDescriptor>
{
	internal RareTermsAggregationDescriptor(Action<RareTermsAggregationDescriptor> configure) => configure.Invoke(this);

	public RareTermsAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? ExcludeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? IncludeValue { get; set; }
	private long? MaxDocCountValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }
	private double? PrecisionValue { get; set; }
	private string? ValueTypeValue { get; set; }

	/// <summary>
	/// <para>Terms that should be excluded from the aggregation.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Exclude(Elastic.Clients.Elasticsearch.Aggregations.TermsExclude? exclude)
	{
		ExcludeValue = exclude;
		return Self;
	}

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>The field from which to return rare terms.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>Terms that should be included in the aggregation.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Include(Elastic.Clients.Elasticsearch.Aggregations.TermsInclude? include)
	{
		IncludeValue = include;
		return Self;
	}

	/// <summary>
	/// <para>The maximum number of documents a term should appear in.</para>
	/// </summary>
	public RareTermsAggregationDescriptor MaxDocCount(long? maxDocCount)
	{
		MaxDocCountValue = maxDocCount;
		return Self;
	}

	/// <summary>
	/// <para>The value to apply to documents that do not have a value.<br/>By default, documents without a value are ignored.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	/// <summary>
	/// <para>The precision of the internal CuckooFilters.<br/>Smaller precision leads to better approximation, but higher memory usage.</para>
	/// </summary>
	public RareTermsAggregationDescriptor Precision(double? precision)
	{
		PrecisionValue = precision;
		return Self;
	}

	public RareTermsAggregationDescriptor ValueType(string? valueType)
	{
		ValueTypeValue = valueType;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExcludeValue is not null)
		{
			writer.WritePropertyName("exclude");
			JsonSerializer.Serialize(writer, ExcludeValue, options);
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

		if (MaxDocCountValue.HasValue)
		{
			writer.WritePropertyName("max_doc_count");
			writer.WriteNumberValue(MaxDocCountValue.Value);
		}

		if (MissingValue is not null)
		{
			writer.WritePropertyName("missing");
			JsonSerializer.Serialize(writer, MissingValue, options);
		}

		if (PrecisionValue.HasValue)
		{
			writer.WritePropertyName("precision");
			writer.WriteNumberValue(PrecisionValue.Value);
		}

		if (!string.IsNullOrEmpty(ValueTypeValue))
		{
			writer.WritePropertyName("value_type");
			writer.WriteStringValue(ValueTypeValue);
		}

		writer.WriteEndObject();
	}
}