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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class RuleRetrieverConverter : System.Text.Json.Serialization.JsonConverter<RuleRetriever>
{
	private static readonly System.Text.Json.JsonEncodedText PropFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText PropMatchCriteria = System.Text.Json.JsonEncodedText.Encode("match_criteria");
	private static readonly System.Text.Json.JsonEncodedText PropMinScore = System.Text.Json.JsonEncodedText.Encode("min_score");
	private static readonly System.Text.Json.JsonEncodedText PropRankWindowSize = System.Text.Json.JsonEncodedText.Encode("rank_window_size");
	private static readonly System.Text.Json.JsonEncodedText PropRetriever = System.Text.Json.JsonEncodedText.Encode("retriever");
	private static readonly System.Text.Json.JsonEncodedText PropRulesetIds = System.Text.Json.JsonEncodedText.Encode("ruleset_ids");

	public override RuleRetriever Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>?> propFilter = default;
		LocalJsonValue<object> propMatchCriteria = default;
		LocalJsonValue<float?> propMinScore = default;
		LocalJsonValue<int?> propRankWindowSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Retriever> propRetriever = default;
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.Id>> propRulesetIds = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFilter.TryReadProperty(ref reader, options, PropFilter, static ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, null)))
			{
				continue;
			}

			if (propMatchCriteria.TryReadProperty(ref reader, options, PropMatchCriteria, null))
			{
				continue;
			}

			if (propMinScore.TryReadProperty(ref reader, options, PropMinScore, null))
			{
				continue;
			}

			if (propRankWindowSize.TryReadProperty(ref reader, options, PropRankWindowSize, null))
			{
				continue;
			}

			if (propRetriever.TryReadProperty(ref reader, options, PropRetriever, null))
			{
				continue;
			}

			if (propRulesetIds.TryReadProperty(ref reader, options, PropRulesetIds, static ICollection<Elastic.Clients.Elasticsearch.Id> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Id>(o, null)!))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new RuleRetriever
		{
			Filter = propFilter.Value
,
			MatchCriteria = propMatchCriteria.Value
,
			MinScore = propMinScore.Value
,
			RankWindowSize = propRankWindowSize.Value
,
			Retriever = propRetriever.Value
,
			RulesetIds = propRulesetIds.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RuleRetriever value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFilter, value.Filter, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.Query>(o, v, null));
		writer.WriteProperty(options, PropMatchCriteria, value.MatchCriteria, null, null);
		writer.WriteProperty(options, PropMinScore, value.MinScore, null, null);
		writer.WriteProperty(options, PropRankWindowSize, value.RankWindowSize, null, null);
		writer.WriteProperty(options, PropRetriever, value.Retriever, null, null);
		writer.WriteProperty(options, PropRulesetIds, value.RulesetIds, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, ICollection<Elastic.Clients.Elasticsearch.Id> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Id>(o, v, null));
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RuleRetrieverConverter))]
public sealed partial class RuleRetriever
{
	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? Filter { get; set; }

	/// <summary>
	/// <para>
	/// The match criteria that will determine if a rule in the provided rulesets should be applied.
	/// </para>
	/// </summary>
	public object MatchCriteria { get; set; }

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public float? MinScore { get; set; }

	/// <summary>
	/// <para>
	/// This value determines the size of the individual result set.
	/// </para>
	/// </summary>
	public int? RankWindowSize { get; set; }

	/// <summary>
	/// <para>
	/// The retriever whose results rules should be applied to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Retriever Retriever { get; set; }

	/// <summary>
	/// <para>
	/// The ruleset IDs containing the rules this retriever is evaluating against.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.Id> RulesetIds { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Retriever(RuleRetriever ruleRetriever) => Elastic.Clients.Elasticsearch.Retriever.Rule(ruleRetriever);
}

public sealed partial class RuleRetrieverDescriptor<TDocument> : SerializableDescriptor<RuleRetrieverDescriptor<TDocument>>
{
	internal RuleRetrieverDescriptor(Action<RuleRetrieverDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RuleRetrieverDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> FilterDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] FilterDescriptorActions { get; set; }
	private object MatchCriteriaValue { get; set; }
	private float? MinScoreValue { get; set; }
	private int? RankWindowSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Retriever RetrieverValue { get; set; }
	private Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> RetrieverDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>> RetrieverDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Id> RulesetIdsValue { get; set; }

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor<TDocument> Filter(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterValue = filter;
		return Self;
	}

	public RuleRetrieverDescriptor<TDocument> Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument> descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public RuleRetrieverDescriptor<TDocument> Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorActions = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	public RuleRetrieverDescriptor<TDocument> Filter(params Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>>[] configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The match criteria that will determine if a rule in the provided rulesets should be applied.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor<TDocument> MatchCriteria(object matchCriteria)
	{
		MatchCriteriaValue = matchCriteria;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor<TDocument> MinScore(float? minScore)
	{
		MinScoreValue = minScore;
		return Self;
	}

	/// <summary>
	/// <para>
	/// This value determines the size of the individual result set.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor<TDocument> RankWindowSize(int? rankWindowSize)
	{
		RankWindowSizeValue = rankWindowSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The retriever whose results rules should be applied to.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor<TDocument> Retriever(Elastic.Clients.Elasticsearch.Retriever retriever)
	{
		RetrieverDescriptor = null;
		RetrieverDescriptorAction = null;
		RetrieverValue = retriever;
		return Self;
	}

	public RuleRetrieverDescriptor<TDocument> Retriever(Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument> descriptor)
	{
		RetrieverValue = null;
		RetrieverDescriptorAction = null;
		RetrieverDescriptor = descriptor;
		return Self;
	}

	public RuleRetrieverDescriptor<TDocument> Retriever(Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>> configure)
	{
		RetrieverValue = null;
		RetrieverDescriptor = null;
		RetrieverDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The ruleset IDs containing the rules this retriever is evaluating against.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor<TDocument> RulesetIds(ICollection<Elastic.Clients.Elasticsearch.Id> rulesetIds)
	{
		RulesetIdsValue = rulesetIds;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(FilterDescriptorAction), options);
		}
		else if (FilterDescriptorActions is not null)
		{
			writer.WritePropertyName("filter");
			if (FilterDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in FilterDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor<TDocument>(action), options);
			}

			if (FilterDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Query>(FilterValue, writer, options);
		}

		writer.WritePropertyName("match_criteria");
		JsonSerializer.Serialize(writer, MatchCriteriaValue, options);
		if (MinScoreValue.HasValue)
		{
			writer.WritePropertyName("min_score");
			writer.WriteNumberValue(MinScoreValue.Value);
		}

		if (RankWindowSizeValue.HasValue)
		{
			writer.WritePropertyName("rank_window_size");
			writer.WriteNumberValue(RankWindowSizeValue.Value);
		}

		if (RetrieverDescriptor is not null)
		{
			writer.WritePropertyName("retriever");
			JsonSerializer.Serialize(writer, RetrieverDescriptor, options);
		}
		else if (RetrieverDescriptorAction is not null)
		{
			writer.WritePropertyName("retriever");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.RetrieverDescriptor<TDocument>(RetrieverDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("retriever");
			JsonSerializer.Serialize(writer, RetrieverValue, options);
		}

		writer.WritePropertyName("ruleset_ids");
		JsonSerializer.Serialize(writer, RulesetIdsValue, options);
		writer.WriteEndObject();
	}
}

public sealed partial class RuleRetrieverDescriptor : SerializableDescriptor<RuleRetrieverDescriptor>
{
	internal RuleRetrieverDescriptor(Action<RuleRetrieverDescriptor> configure) => configure.Invoke(this);

	public RuleRetrieverDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? FilterValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor FilterDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> FilterDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] FilterDescriptorActions { get; set; }
	private object MatchCriteriaValue { get; set; }
	private float? MinScoreValue { get; set; }
	private int? RankWindowSizeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Retriever RetrieverValue { get; set; }
	private Elastic.Clients.Elasticsearch.RetrieverDescriptor RetrieverDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor> RetrieverDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Id> RulesetIdsValue { get; set; }

	/// <summary>
	/// <para>
	/// Query to filter the documents that can match.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor Filter(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.Query>? filter)
	{
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterValue = filter;
		return Self;
	}

	public RuleRetrieverDescriptor Filter(Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor descriptor)
	{
		FilterValue = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = null;
		FilterDescriptor = descriptor;
		return Self;
	}

	public RuleRetrieverDescriptor Filter(Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor> configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorActions = null;
		FilterDescriptorAction = configure;
		return Self;
	}

	public RuleRetrieverDescriptor Filter(params Action<Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor>[] configure)
	{
		FilterValue = null;
		FilterDescriptor = null;
		FilterDescriptorAction = null;
		FilterDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The match criteria that will determine if a rule in the provided rulesets should be applied.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor MatchCriteria(object matchCriteria)
	{
		MatchCriteriaValue = matchCriteria;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Minimum _score for matching documents. Documents with a lower _score are not included in the top documents.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor MinScore(float? minScore)
	{
		MinScoreValue = minScore;
		return Self;
	}

	/// <summary>
	/// <para>
	/// This value determines the size of the individual result set.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor RankWindowSize(int? rankWindowSize)
	{
		RankWindowSizeValue = rankWindowSize;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The retriever whose results rules should be applied to.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor Retriever(Elastic.Clients.Elasticsearch.Retriever retriever)
	{
		RetrieverDescriptor = null;
		RetrieverDescriptorAction = null;
		RetrieverValue = retriever;
		return Self;
	}

	public RuleRetrieverDescriptor Retriever(Elastic.Clients.Elasticsearch.RetrieverDescriptor descriptor)
	{
		RetrieverValue = null;
		RetrieverDescriptorAction = null;
		RetrieverDescriptor = descriptor;
		return Self;
	}

	public RuleRetrieverDescriptor Retriever(Action<Elastic.Clients.Elasticsearch.RetrieverDescriptor> configure)
	{
		RetrieverValue = null;
		RetrieverDescriptor = null;
		RetrieverDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The ruleset IDs containing the rules this retriever is evaluating against.
	/// </para>
	/// </summary>
	public RuleRetrieverDescriptor RulesetIds(ICollection<Elastic.Clients.Elasticsearch.Id> rulesetIds)
	{
		RulesetIdsValue = rulesetIds;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FilterDescriptor is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, FilterDescriptor, options);
		}
		else if (FilterDescriptorAction is not null)
		{
			writer.WritePropertyName("filter");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(FilterDescriptorAction), options);
		}
		else if (FilterDescriptorActions is not null)
		{
			writer.WritePropertyName("filter");
			if (FilterDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in FilterDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.QueryDescriptor(action), options);
			}

			if (FilterDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else if (FilterValue is not null)
		{
			writer.WritePropertyName("filter");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryDsl.Query>(FilterValue, writer, options);
		}

		writer.WritePropertyName("match_criteria");
		JsonSerializer.Serialize(writer, MatchCriteriaValue, options);
		if (MinScoreValue.HasValue)
		{
			writer.WritePropertyName("min_score");
			writer.WriteNumberValue(MinScoreValue.Value);
		}

		if (RankWindowSizeValue.HasValue)
		{
			writer.WritePropertyName("rank_window_size");
			writer.WriteNumberValue(RankWindowSizeValue.Value);
		}

		if (RetrieverDescriptor is not null)
		{
			writer.WritePropertyName("retriever");
			JsonSerializer.Serialize(writer, RetrieverDescriptor, options);
		}
		else if (RetrieverDescriptorAction is not null)
		{
			writer.WritePropertyName("retriever");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.RetrieverDescriptor(RetrieverDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("retriever");
			JsonSerializer.Serialize(writer, RetrieverValue, options);
		}

		writer.WritePropertyName("ruleset_ids");
		JsonSerializer.Serialize(writer, RulesetIdsValue, options);
		writer.WriteEndObject();
	}
}