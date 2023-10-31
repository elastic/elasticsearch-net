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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

public sealed partial class TopMetricsValue
{
	/// <summary>
	/// <para>A field to return as a metric.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }
}

public sealed partial class TopMetricsValueDescriptor<TDocument> : SerializableDescriptor<TopMetricsValueDescriptor<TDocument>>
{
	internal TopMetricsValueDescriptor(Action<TopMetricsValueDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TopMetricsValueDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }

	/// <summary>
	/// <para>A field to return as a metric.</para>
	/// </summary>
	public TopMetricsValueDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>A field to return as a metric.</para>
	/// </summary>
	public TopMetricsValueDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WriteEndObject();
	}
}

public sealed partial class TopMetricsValueDescriptor : SerializableDescriptor<TopMetricsValueDescriptor>
{
	internal TopMetricsValueDescriptor(Action<TopMetricsValueDescriptor> configure) => configure.Invoke(this);

	public TopMetricsValueDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }

	/// <summary>
	/// <para>A field to return as a metric.</para>
	/// </summary>
	public TopMetricsValueDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>A field to return as a metric.</para>
	/// </summary>
	public TopMetricsValueDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>A field to return as a metric.</para>
	/// </summary>
	public TopMetricsValueDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("field");
		JsonSerializer.Serialize(writer, FieldValue, options);
		writer.WriteEndObject();
	}
}