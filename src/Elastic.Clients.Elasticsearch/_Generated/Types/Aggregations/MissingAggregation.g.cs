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

public sealed partial class MissingAggregation
{
	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Field? Field { get; set; }
	[JsonInclude, JsonPropertyName("missing")]
	public Elastic.Clients.Elasticsearch.FieldValue? Missing { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Aggregations.Aggregation(MissingAggregation missingAggregation) => Elastic.Clients.Elasticsearch.Aggregations.Aggregation.Missing(missingAggregation);
}

public sealed partial class MissingAggregationDescriptor<TDocument> : SerializableDescriptor<MissingAggregationDescriptor<TDocument>>
{
	internal MissingAggregationDescriptor(Action<MissingAggregationDescriptor<TDocument>> configure) => configure.Invoke(this);

	public MissingAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }

	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	public MissingAggregationDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	public MissingAggregationDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	public MissingAggregationDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor<TDocument> Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
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
	}
}

public sealed partial class MissingAggregationDescriptor : SerializableDescriptor<MissingAggregationDescriptor>
{
	internal MissingAggregationDescriptor(Action<MissingAggregationDescriptor> configure) => configure.Invoke(this);

	public MissingAggregationDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field? FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue? MissingValue { get; set; }

	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	public MissingAggregationDescriptor Field(Elastic.Clients.Elasticsearch.Field? field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	public MissingAggregationDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The name of the field.
	/// </para>
	/// </summary>
	public MissingAggregationDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public MissingAggregationDescriptor Missing(Elastic.Clients.Elasticsearch.FieldValue? missing)
	{
		MissingValue = missing;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
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
	}
}