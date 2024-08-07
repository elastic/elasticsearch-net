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

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

public sealed partial class SpanMultiTermQuery
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
	/// Should be a multi term query (one of <c>wildcard</c>, <c>fuzzy</c>, <c>prefix</c>, <c>range</c>, or <c>regexp</c> query).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("match")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query Match { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query(SpanMultiTermQuery spanMultiTermQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query.SpanMulti(spanMultiTermQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanQuery(SpanMultiTermQuery spanMultiTermQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.SpanQuery.SpanMulti(spanMultiTermQuery);
}

public sealed partial class SpanMultiTermQueryDescriptor<TDocument> : SerializableDescriptor<SpanMultiTermQueryDescriptor<TDocument>>
{
	internal SpanMultiTermQueryDescriptor(Action<SpanMultiTermQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public SpanMultiTermQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query MatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> MatchDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> MatchDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public SpanMultiTermQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Should be a multi term query (one of <c>wildcard</c>, <c>fuzzy</c>, <c>prefix</c>, <c>range</c>, or <c>regexp</c> query).
	/// </para>
	/// </summary>
	public SpanMultiTermQueryDescriptor<TDocument> Match(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query match)
	{
		MatchDescriptor = null;
		MatchDescriptorAction = null;
		MatchValue = match;
		return Self;
	}

	public SpanMultiTermQueryDescriptor<TDocument> Match(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		MatchValue = null;
		MatchDescriptorAction = null;
		MatchDescriptor = descriptor;
		return Self;
	}

	public SpanMultiTermQueryDescriptor<TDocument> Match(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		MatchValue = null;
		MatchDescriptor = null;
		MatchDescriptorAction = configure;
		return Self;
	}

	public SpanMultiTermQueryDescriptor<TDocument> QueryName(string? queryName)
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

		if (MatchDescriptor is not null)
		{
			writer.WritePropertyName("match");
			JsonSerializer.Serialize(writer, MatchDescriptor, options);
		}
		else if (MatchDescriptorAction is not null)
		{
			writer.WritePropertyName("match");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>(MatchDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("match");
			JsonSerializer.Serialize(writer, MatchValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class SpanMultiTermQueryDescriptor : SerializableDescriptor<SpanMultiTermQueryDescriptor>
{
	internal SpanMultiTermQueryDescriptor(Action<SpanMultiTermQueryDescriptor> configure) => configure.Invoke(this);

	public SpanMultiTermQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query MatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor MatchDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> MatchDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public SpanMultiTermQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Should be a multi term query (one of <c>wildcard</c>, <c>fuzzy</c>, <c>prefix</c>, <c>range</c>, or <c>regexp</c> query).
	/// </para>
	/// </summary>
	public SpanMultiTermQueryDescriptor Match(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query match)
	{
		MatchDescriptor = null;
		MatchDescriptorAction = null;
		MatchValue = match;
		return Self;
	}

	public SpanMultiTermQueryDescriptor Match(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor descriptor)
	{
		MatchValue = null;
		MatchDescriptorAction = null;
		MatchDescriptor = descriptor;
		return Self;
	}

	public SpanMultiTermQueryDescriptor Match(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> configure)
	{
		MatchValue = null;
		MatchDescriptor = null;
		MatchDescriptorAction = configure;
		return Self;
	}

	public SpanMultiTermQueryDescriptor QueryName(string? queryName)
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

		if (MatchDescriptor is not null)
		{
			writer.WritePropertyName("match");
			JsonSerializer.Serialize(writer, MatchDescriptor, options);
		}
		else if (MatchDescriptorAction is not null)
		{
			writer.WritePropertyName("match");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor(MatchDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("match");
			JsonSerializer.Serialize(writer, MatchValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}