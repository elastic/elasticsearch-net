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

public sealed partial class BoostingQuery
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
	/// Query used to decrease the relevance score of matching documents.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("negative")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query Negative { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number between 0 and 1.0 used to decrease the relevance scores of documents matching the <c>negative</c> query.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("negative_boost")]
	public double NegativeBoost { get; set; }

	/// <summary>
	/// <para>
	/// Any returned documents must match this query.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("positive")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query Positive { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query(BoostingQuery boostingQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query.Boosting(boostingQuery);
}

public sealed partial class BoostingQueryDescriptor<TDocument> : SerializableDescriptor<BoostingQueryDescriptor<TDocument>>
{
	internal BoostingQueryDescriptor(Action<BoostingQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public BoostingQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query NegativeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> NegativeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> NegativeDescriptorAction { get; set; }
	private double NegativeBoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query PositiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> PositiveDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> PositiveDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Query used to decrease the relevance score of matching documents.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor<TDocument> Negative(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query negative)
	{
		NegativeDescriptor = null;
		NegativeDescriptorAction = null;
		NegativeValue = negative;
		return Self;
	}

	public BoostingQueryDescriptor<TDocument> Negative(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		NegativeValue = null;
		NegativeDescriptorAction = null;
		NegativeDescriptor = descriptor;
		return Self;
	}

	public BoostingQueryDescriptor<TDocument> Negative(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		NegativeValue = null;
		NegativeDescriptor = null;
		NegativeDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Floating point number between 0 and 1.0 used to decrease the relevance scores of documents matching the <c>negative</c> query.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor<TDocument> NegativeBoost(double negativeBoost)
	{
		NegativeBoostValue = negativeBoost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Any returned documents must match this query.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor<TDocument> Positive(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query positive)
	{
		PositiveDescriptor = null;
		PositiveDescriptorAction = null;
		PositiveValue = positive;
		return Self;
	}

	public BoostingQueryDescriptor<TDocument> Positive(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		PositiveValue = null;
		PositiveDescriptorAction = null;
		PositiveDescriptor = descriptor;
		return Self;
	}

	public BoostingQueryDescriptor<TDocument> Positive(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		PositiveValue = null;
		PositiveDescriptor = null;
		PositiveDescriptorAction = configure;
		return Self;
	}

	public BoostingQueryDescriptor<TDocument> QueryName(string? queryName)
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

		if (NegativeDescriptor is not null)
		{
			writer.WritePropertyName("negative");
			JsonSerializer.Serialize(writer, NegativeDescriptor, options);
		}
		else if (NegativeDescriptorAction is not null)
		{
			writer.WritePropertyName("negative");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>(NegativeDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("negative");
			JsonSerializer.Serialize(writer, NegativeValue, options);
		}

		writer.WritePropertyName("negative_boost");
		writer.WriteNumberValue(NegativeBoostValue);
		if (PositiveDescriptor is not null)
		{
			writer.WritePropertyName("positive");
			JsonSerializer.Serialize(writer, PositiveDescriptor, options);
		}
		else if (PositiveDescriptorAction is not null)
		{
			writer.WritePropertyName("positive");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>(PositiveDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("positive");
			JsonSerializer.Serialize(writer, PositiveValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class BoostingQueryDescriptor : SerializableDescriptor<BoostingQueryDescriptor>
{
	internal BoostingQueryDescriptor(Action<BoostingQueryDescriptor> configure) => configure.Invoke(this);

	public BoostingQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query NegativeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor NegativeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> NegativeDescriptorAction { get; set; }
	private double NegativeBoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query PositiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor PositiveDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> PositiveDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Query used to decrease the relevance score of matching documents.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor Negative(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query negative)
	{
		NegativeDescriptor = null;
		NegativeDescriptorAction = null;
		NegativeValue = negative;
		return Self;
	}

	public BoostingQueryDescriptor Negative(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor descriptor)
	{
		NegativeValue = null;
		NegativeDescriptorAction = null;
		NegativeDescriptor = descriptor;
		return Self;
	}

	public BoostingQueryDescriptor Negative(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> configure)
	{
		NegativeValue = null;
		NegativeDescriptor = null;
		NegativeDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Floating point number between 0 and 1.0 used to decrease the relevance scores of documents matching the <c>negative</c> query.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor NegativeBoost(double negativeBoost)
	{
		NegativeBoostValue = negativeBoost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Any returned documents must match this query.
	/// </para>
	/// </summary>
	public BoostingQueryDescriptor Positive(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query positive)
	{
		PositiveDescriptor = null;
		PositiveDescriptorAction = null;
		PositiveValue = positive;
		return Self;
	}

	public BoostingQueryDescriptor Positive(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor descriptor)
	{
		PositiveValue = null;
		PositiveDescriptorAction = null;
		PositiveDescriptor = descriptor;
		return Self;
	}

	public BoostingQueryDescriptor Positive(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> configure)
	{
		PositiveValue = null;
		PositiveDescriptor = null;
		PositiveDescriptorAction = configure;
		return Self;
	}

	public BoostingQueryDescriptor QueryName(string? queryName)
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

		if (NegativeDescriptor is not null)
		{
			writer.WritePropertyName("negative");
			JsonSerializer.Serialize(writer, NegativeDescriptor, options);
		}
		else if (NegativeDescriptorAction is not null)
		{
			writer.WritePropertyName("negative");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor(NegativeDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("negative");
			JsonSerializer.Serialize(writer, NegativeValue, options);
		}

		writer.WritePropertyName("negative_boost");
		writer.WriteNumberValue(NegativeBoostValue);
		if (PositiveDescriptor is not null)
		{
			writer.WritePropertyName("positive");
			JsonSerializer.Serialize(writer, PositiveDescriptor, options);
		}
		else if (PositiveDescriptorAction is not null)
		{
			writer.WritePropertyName("positive");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor(PositiveDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("positive");
			JsonSerializer.Serialize(writer, PositiveValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}