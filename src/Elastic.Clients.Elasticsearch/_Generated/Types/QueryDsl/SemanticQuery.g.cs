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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public sealed partial class SemanticQuery
{
	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost")]
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// The field to query, which must be a semantic_text field type
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("field")]
	public string Field { get; set; }

	/// <summary>
	/// <para>
	/// The query text
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public string Query { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(SemanticQuery semanticQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Semantic(semanticQuery);
}

public sealed partial class SemanticQueryDescriptor : SerializableDescriptor<SemanticQueryDescriptor>
{
	internal SemanticQueryDescriptor(Action<SemanticQueryDescriptor> configure) => configure.Invoke(this);

	public SemanticQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private string FieldValue { get; set; }
	private string QueryValue { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public SemanticQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The field to query, which must be a semantic_text field type
	/// </para>
	/// </summary>
	public SemanticQueryDescriptor Field(string field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The query text
	/// </para>
	/// </summary>
	public SemanticQueryDescriptor Query(string query)
	{
		QueryValue = query;
		return Self;
	}

	public SemanticQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		writer.WritePropertyName("field");
		writer.WriteStringValue(FieldValue);
		writer.WritePropertyName("query");
		writer.WriteStringValue(QueryValue);
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}