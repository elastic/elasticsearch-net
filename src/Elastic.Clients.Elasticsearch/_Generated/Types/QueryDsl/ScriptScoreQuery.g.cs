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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class ScriptScoreQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropMinScore = System.Text.Json.JsonEncodedText.Encode("min_score");
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("query");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropScript = System.Text.Json.JsonEncodedText.Encode("script");

	public override Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<float?> propMinScore = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query> propQuery = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script> propScript = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propMinScore.TryReadProperty(ref reader, options, PropMinScore, null))
			{
				continue;
			}

			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propScript.TryReadProperty(ref reader, options, PropScript, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			MinScore = propMinScore.Value,
			Query = propQuery.Value,
			QueryName = propQueryName.Value,
			Script = propScript.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropMinScore, value.MinScore, null, null);
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropScript, value.Script, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryConverter))]
public sealed partial class ScriptScoreQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptScoreQuery(Elastic.Clients.Elasticsearch.QueryDsl.Query query, Elastic.Clients.Elasticsearch.Script script)
	{
		Query = query;
		Script = script;
	}
#if NET7_0_OR_GREATER
	public ScriptScoreQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ScriptScoreQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ScriptScoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }

	/// <summary>
	/// <para>
	/// Documents with a score lower than this floating point number are excluded from the search results.
	/// </para>
	/// </summary>
	public float? MinScore { get; set; }

	/// <summary>
	/// <para>
	/// Query used to return documents.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.QueryDsl.Query Query { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Script Script { get; set; }
}

public readonly partial struct ScriptScoreQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptScoreQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptScoreQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Documents with a score lower than this floating point number are excluded from the search results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> MinScore(float? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to return documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to return documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> Script(Elastic.Clients.Elasticsearch.Script value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument> Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct ScriptScoreQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptScoreQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ScriptScoreQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Documents with a score lower than this floating point number are excluded from the search results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor MinScore(float? value)
	{
		Instance.MinScore = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to return documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to return documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Query used to return documents.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Script(Elastic.Clients.Elasticsearch.Script value)
	{
		Instance.Script = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Script()
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Script used to compute the score of documents returned by the query.
	/// Important: final relevance scores from the <c>script_score</c> query cannot be negative.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor Script(System.Action<Elastic.Clients.Elasticsearch.ScriptDescriptor>? action)
	{
		Instance.Script = Elastic.Clients.Elasticsearch.ScriptDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.ScriptScoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}