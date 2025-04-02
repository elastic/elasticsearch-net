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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class RescoreQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropQuery = System.Text.Json.JsonEncodedText.Encode("rescore_query");
	private static readonly System.Text.Json.JsonEncodedText PropQueryWeight = System.Text.Json.JsonEncodedText.Encode("query_weight");
	private static readonly System.Text.Json.JsonEncodedText PropRescoreQueryWeight = System.Text.Json.JsonEncodedText.Encode("rescore_query_weight");
	private static readonly System.Text.Json.JsonEncodedText PropScoreMode = System.Text.Json.JsonEncodedText.Encode("score_mode");

	public override Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.Query> propQuery = default;
		LocalJsonValue<double?> propQueryWeight = default;
		LocalJsonValue<double?> propRescoreQueryWeight = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Core.Search.ScoreMode?> propScoreMode = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propQuery.TryReadProperty(ref reader, options, PropQuery, null))
			{
				continue;
			}

			if (propQueryWeight.TryReadProperty(ref reader, options, PropQueryWeight, null))
			{
				continue;
			}

			if (propRescoreQueryWeight.TryReadProperty(ref reader, options, PropRescoreQueryWeight, null))
			{
				continue;
			}

			if (propScoreMode.TryReadProperty(ref reader, options, PropScoreMode, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Query = propQuery.Value,
			QueryWeight = propQueryWeight.Value,
			RescoreQueryWeight = propRescoreQueryWeight.Value,
			ScoreMode = propScoreMode.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropQuery, value.Query, null, null);
		writer.WriteProperty(options, PropQueryWeight, value.QueryWeight, null, null);
		writer.WriteProperty(options, PropRescoreQueryWeight, value.RescoreQueryWeight, null, null);
		writer.WriteProperty(options, PropScoreMode, value.ScoreMode, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryConverter))]
public sealed partial class RescoreQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RescoreQuery(Elastic.Clients.Elasticsearch.QueryDsl.Query query)
	{
		Query = query;
	}
#if NET7_0_OR_GREATER
	public RescoreQuery()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public RescoreQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RescoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The query to use for rescoring.
	/// This query is only run on the Top-K results returned by the <c>query</c> and <c>post_filter</c> phases.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.QueryDsl.Query Query { get; set; }

	/// <summary>
	/// <para>
	/// Relative importance of the original query versus the rescore query.
	/// </para>
	/// </summary>
	public double? QueryWeight { get; set; }

	/// <summary>
	/// <para>
	/// Relative importance of the rescore query versus the original query.
	/// </para>
	/// </summary>
	public double? RescoreQueryWeight { get; set; }

	/// <summary>
	/// <para>
	/// Determines how scores are combined.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.ScoreMode? ScoreMode { get; set; }
}

public readonly partial struct RescoreQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RescoreQueryDescriptor(Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RescoreQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery instance) => new Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The query to use for rescoring.
	/// This query is only run on the Top-K results returned by the <c>query</c> and <c>post_filter</c> phases.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument> Query(Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query to use for rescoring.
	/// This query is only run on the Top-K results returned by the <c>query</c> and <c>post_filter</c> phases.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument> Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Relative importance of the original query versus the rescore query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument> QueryWeight(double? value)
	{
		Instance.QueryWeight = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Relative importance of the rescore query versus the original query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument> RescoreQueryWeight(double? value)
	{
		Instance.RescoreQueryWeight = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how scores are combined.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument> ScoreMode(Elastic.Clients.Elasticsearch.Core.Search.ScoreMode? value)
	{
		Instance.ScoreMode = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct RescoreQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RescoreQueryDescriptor(Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RescoreQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor(Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery instance) => new Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The query to use for rescoring.
	/// This query is only run on the Top-K results returned by the <c>query</c> and <c>post_filter</c> phases.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor Query(Elastic.Clients.Elasticsearch.QueryDsl.Query value)
	{
		Instance.Query = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The query to use for rescoring.
	/// This query is only run on the Top-K results returned by the <c>query</c> and <c>post_filter</c> phases.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor Query(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The query to use for rescoring.
	/// This query is only run on the Top-K results returned by the <c>query</c> and <c>post_filter</c> phases.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor Query<T>(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>> action)
	{
		Instance.Query = Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Relative importance of the original query versus the rescore query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor QueryWeight(double? value)
	{
		Instance.QueryWeight = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Relative importance of the rescore query versus the original query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor RescoreQueryWeight(double? value)
	{
		Instance.RescoreQueryWeight = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Determines how scores are combined.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor ScoreMode(Elastic.Clients.Elasticsearch.Core.Search.ScoreMode? value)
	{
		Instance.ScoreMode = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.RescoreQueryDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.RescoreQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}