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

public sealed partial class FunctionScoreQuery
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
	/// Defines how he newly computed score is combined with the score of the query
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("boost_mode")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionBoostMode? BoostMode { get; set; }

	/// <summary>
	/// <para>
	/// One or more functions that compute a new score for each document returned by the query.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("functions")]
	public ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScore>? Functions { get; set; }

	/// <summary>
	/// <para>
	/// Restricts the new score to not exceed the provided limit.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_boost")]
	public double? MaxBoost { get; set; }

	/// <summary>
	/// <para>
	/// Excludes documents that do not meet the provided score threshold.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("min_score")]
	public double? MinScore { get; set; }

	/// <summary>
	/// <para>
	/// A query that determines the documents for which a new score is computed.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("query")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? Query { get; set; }
	[JsonInclude, JsonPropertyName("_name")]
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Specifies how the computed scores are combined
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("score_mode")]
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreMode? ScoreMode { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query(FunctionScoreQuery functionScoreQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query.FunctionScore(functionScoreQuery);
}

public sealed partial class FunctionScoreQueryDescriptor<TDocument> : SerializableDescriptor<FunctionScoreQueryDescriptor<TDocument>>
{
	internal FunctionScoreQueryDescriptor(Action<FunctionScoreQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FunctionScoreQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionBoostMode? BoostModeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScore>? FunctionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument> FunctionsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument>> FunctionsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument>>[] FunctionsDescriptorActions { get; set; }
	private double? MaxBoostValue { get; set; }
	private double? MinScoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> QueryDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreMode? ScoreModeValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines how he newly computed score is combined with the score of the query
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> BoostMode(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionBoostMode? boostMode)
	{
		BoostModeValue = boostMode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// One or more functions that compute a new score for each document returned by the query.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> Functions(ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScore>? functions)
	{
		FunctionsDescriptor = null;
		FunctionsDescriptorAction = null;
		FunctionsDescriptorActions = null;
		FunctionsValue = functions;
		return Self;
	}

	public FunctionScoreQueryDescriptor<TDocument> Functions(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument> descriptor)
	{
		FunctionsValue = null;
		FunctionsDescriptorAction = null;
		FunctionsDescriptorActions = null;
		FunctionsDescriptor = descriptor;
		return Self;
	}

	public FunctionScoreQueryDescriptor<TDocument> Functions(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument>> configure)
	{
		FunctionsValue = null;
		FunctionsDescriptor = null;
		FunctionsDescriptorActions = null;
		FunctionsDescriptorAction = configure;
		return Self;
	}

	public FunctionScoreQueryDescriptor<TDocument> Functions(params Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument>>[] configure)
	{
		FunctionsValue = null;
		FunctionsDescriptor = null;
		FunctionsDescriptorAction = null;
		FunctionsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Restricts the new score to not exceed the provided limit.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> MaxBoost(double? maxBoost)
	{
		MaxBoostValue = maxBoost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Excludes documents that do not meet the provided score threshold.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> MinScore(double? minScore)
	{
		MinScoreValue = minScore;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A query that determines the documents for which a new score is computed.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public FunctionScoreQueryDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public FunctionScoreQueryDescriptor<TDocument> Query(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	public FunctionScoreQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies how the computed scores are combined
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor<TDocument> ScoreMode(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreMode? scoreMode)
	{
		ScoreModeValue = scoreMode;
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

		if (BoostModeValue is not null)
		{
			writer.WritePropertyName("boost_mode");
			JsonSerializer.Serialize(writer, BoostModeValue, options);
		}

		if (FunctionsDescriptor is not null)
		{
			writer.WritePropertyName("functions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, FunctionsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (FunctionsDescriptorAction is not null)
		{
			writer.WritePropertyName("functions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument>(FunctionsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (FunctionsDescriptorActions is not null)
		{
			writer.WritePropertyName("functions");
			writer.WriteStartArray();
			foreach (var action in FunctionsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor<TDocument>(action), options);
			}

			writer.WriteEndArray();
		}
		else if (FunctionsValue is not null)
		{
			writer.WritePropertyName("functions");
			JsonSerializer.Serialize(writer, FunctionsValue, options);
		}

		if (MaxBoostValue.HasValue)
		{
			writer.WritePropertyName("max_boost");
			writer.WriteNumberValue(MaxBoostValue.Value);
		}

		if (MinScoreValue.HasValue)
		{
			writer.WritePropertyName("min_score");
			writer.WriteNumberValue(MinScoreValue.Value);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor<TDocument>(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (ScoreModeValue is not null)
		{
			writer.WritePropertyName("score_mode");
			JsonSerializer.Serialize(writer, ScoreModeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class FunctionScoreQueryDescriptor : SerializableDescriptor<FunctionScoreQueryDescriptor>
{
	internal FunctionScoreQueryDescriptor(Action<FunctionScoreQueryDescriptor> configure) => configure.Invoke(this);

	public FunctionScoreQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionBoostMode? BoostModeValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScore>? FunctionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor FunctionsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor> FunctionsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor>[] FunctionsDescriptorActions { get; set; }
	private double? MaxBoostValue { get; set; }
	private double? MinScoreValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? QueryValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor QueryDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> QueryDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreMode? ScoreModeValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Defines how he newly computed score is combined with the score of the query
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor BoostMode(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionBoostMode? boostMode)
	{
		BoostModeValue = boostMode;
		return Self;
	}

	/// <summary>
	/// <para>
	/// One or more functions that compute a new score for each document returned by the query.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor Functions(ICollection<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScore>? functions)
	{
		FunctionsDescriptor = null;
		FunctionsDescriptorAction = null;
		FunctionsDescriptorActions = null;
		FunctionsValue = functions;
		return Self;
	}

	public FunctionScoreQueryDescriptor Functions(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor descriptor)
	{
		FunctionsValue = null;
		FunctionsDescriptorAction = null;
		FunctionsDescriptorActions = null;
		FunctionsDescriptor = descriptor;
		return Self;
	}

	public FunctionScoreQueryDescriptor Functions(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor> configure)
	{
		FunctionsValue = null;
		FunctionsDescriptor = null;
		FunctionsDescriptorActions = null;
		FunctionsDescriptorAction = configure;
		return Self;
	}

	public FunctionScoreQueryDescriptor Functions(params Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor>[] configure)
	{
		FunctionsValue = null;
		FunctionsDescriptor = null;
		FunctionsDescriptorAction = null;
		FunctionsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Restricts the new score to not exceed the provided limit.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor MaxBoost(double? maxBoost)
	{
		MaxBoostValue = maxBoost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Excludes documents that do not meet the provided score threshold.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor MinScore(double? minScore)
	{
		MinScoreValue = minScore;
		return Self;
	}

	/// <summary>
	/// <para>
	/// A query that determines the documents for which a new score is computed.
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query? query)
	{
		QueryDescriptor = null;
		QueryDescriptorAction = null;
		QueryValue = query;
		return Self;
	}

	public FunctionScoreQueryDescriptor Query(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor descriptor)
	{
		QueryValue = null;
		QueryDescriptorAction = null;
		QueryDescriptor = descriptor;
		return Self;
	}

	public FunctionScoreQueryDescriptor Query(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor> configure)
	{
		QueryValue = null;
		QueryDescriptor = null;
		QueryDescriptorAction = configure;
		return Self;
	}

	public FunctionScoreQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specifies how the computed scores are combined
	/// </para>
	/// </summary>
	public FunctionScoreQueryDescriptor ScoreMode(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreMode? scoreMode)
	{
		ScoreModeValue = scoreMode;
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

		if (BoostModeValue is not null)
		{
			writer.WritePropertyName("boost_mode");
			JsonSerializer.Serialize(writer, BoostModeValue, options);
		}

		if (FunctionsDescriptor is not null)
		{
			writer.WritePropertyName("functions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, FunctionsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (FunctionsDescriptorAction is not null)
		{
			writer.WritePropertyName("functions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor(FunctionsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (FunctionsDescriptorActions is not null)
		{
			writer.WritePropertyName("functions");
			writer.WriteStartArray();
			foreach (var action in FunctionsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.FunctionScoreDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (FunctionsValue is not null)
		{
			writer.WritePropertyName("functions");
			JsonSerializer.Serialize(writer, FunctionsValue, options);
		}

		if (MaxBoostValue.HasValue)
		{
			writer.WritePropertyName("max_boost");
			writer.WriteNumberValue(MaxBoostValue.Value);
		}

		if (MinScoreValue.HasValue)
		{
			writer.WritePropertyName("min_score");
			writer.WriteNumberValue(MinScoreValue.Value);
		}

		if (QueryDescriptor is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryDescriptor, options);
		}
		else if (QueryDescriptorAction is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.QueryDsl.QueryDescriptor(QueryDescriptorAction), options);
		}
		else if (QueryValue is not null)
		{
			writer.WritePropertyName("query");
			JsonSerializer.Serialize(writer, QueryValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (ScoreModeValue is not null)
		{
			writer.WritePropertyName("score_mode");
			JsonSerializer.Serialize(writer, ScoreModeValue, options);
		}

		writer.WriteEndObject();
	}
}