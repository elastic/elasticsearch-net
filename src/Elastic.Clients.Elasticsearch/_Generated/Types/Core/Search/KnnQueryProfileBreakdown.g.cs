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

internal sealed partial class KnnQueryProfileBreakdownConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown>
{
	private static readonly System.Text.Json.JsonEncodedText PropAdvance = System.Text.Json.JsonEncodedText.Encode("advance");
	private static readonly System.Text.Json.JsonEncodedText PropAdvanceCount = System.Text.Json.JsonEncodedText.Encode("advance_count");
	private static readonly System.Text.Json.JsonEncodedText PropBuildScorer = System.Text.Json.JsonEncodedText.Encode("build_scorer");
	private static readonly System.Text.Json.JsonEncodedText PropBuildScorerCount = System.Text.Json.JsonEncodedText.Encode("build_scorer_count");
	private static readonly System.Text.Json.JsonEncodedText PropComputeMaxScore = System.Text.Json.JsonEncodedText.Encode("compute_max_score");
	private static readonly System.Text.Json.JsonEncodedText PropComputeMaxScoreCount = System.Text.Json.JsonEncodedText.Encode("compute_max_score_count");
	private static readonly System.Text.Json.JsonEncodedText PropCountWeight = System.Text.Json.JsonEncodedText.Encode("count_weight");
	private static readonly System.Text.Json.JsonEncodedText PropCountWeightCount = System.Text.Json.JsonEncodedText.Encode("count_weight_count");
	private static readonly System.Text.Json.JsonEncodedText PropCreateWeight = System.Text.Json.JsonEncodedText.Encode("create_weight");
	private static readonly System.Text.Json.JsonEncodedText PropCreateWeightCount = System.Text.Json.JsonEncodedText.Encode("create_weight_count");
	private static readonly System.Text.Json.JsonEncodedText PropMatch = System.Text.Json.JsonEncodedText.Encode("match");
	private static readonly System.Text.Json.JsonEncodedText PropMatchCount = System.Text.Json.JsonEncodedText.Encode("match_count");
	private static readonly System.Text.Json.JsonEncodedText PropNextDoc = System.Text.Json.JsonEncodedText.Encode("next_doc");
	private static readonly System.Text.Json.JsonEncodedText PropNextDocCount = System.Text.Json.JsonEncodedText.Encode("next_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropScore = System.Text.Json.JsonEncodedText.Encode("score");
	private static readonly System.Text.Json.JsonEncodedText PropScoreCount = System.Text.Json.JsonEncodedText.Encode("score_count");
	private static readonly System.Text.Json.JsonEncodedText PropSetMinCompetitiveScore = System.Text.Json.JsonEncodedText.Encode("set_min_competitive_score");
	private static readonly System.Text.Json.JsonEncodedText PropSetMinCompetitiveScoreCount = System.Text.Json.JsonEncodedText.Encode("set_min_competitive_score_count");
	private static readonly System.Text.Json.JsonEncodedText PropShallowAdvance = System.Text.Json.JsonEncodedText.Encode("shallow_advance");
	private static readonly System.Text.Json.JsonEncodedText PropShallowAdvanceCount = System.Text.Json.JsonEncodedText.Encode("shallow_advance_count");

	public override Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long> propAdvance = default;
		LocalJsonValue<long> propAdvanceCount = default;
		LocalJsonValue<long> propBuildScorer = default;
		LocalJsonValue<long> propBuildScorerCount = default;
		LocalJsonValue<long> propComputeMaxScore = default;
		LocalJsonValue<long> propComputeMaxScoreCount = default;
		LocalJsonValue<long> propCountWeight = default;
		LocalJsonValue<long> propCountWeightCount = default;
		LocalJsonValue<long> propCreateWeight = default;
		LocalJsonValue<long> propCreateWeightCount = default;
		LocalJsonValue<long> propMatch = default;
		LocalJsonValue<long> propMatchCount = default;
		LocalJsonValue<long> propNextDoc = default;
		LocalJsonValue<long> propNextDocCount = default;
		LocalJsonValue<long> propScore = default;
		LocalJsonValue<long> propScoreCount = default;
		LocalJsonValue<long> propSetMinCompetitiveScore = default;
		LocalJsonValue<long> propSetMinCompetitiveScoreCount = default;
		LocalJsonValue<long> propShallowAdvance = default;
		LocalJsonValue<long> propShallowAdvanceCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAdvance.TryReadProperty(ref reader, options, PropAdvance, null))
			{
				continue;
			}

			if (propAdvanceCount.TryReadProperty(ref reader, options, PropAdvanceCount, null))
			{
				continue;
			}

			if (propBuildScorer.TryReadProperty(ref reader, options, PropBuildScorer, null))
			{
				continue;
			}

			if (propBuildScorerCount.TryReadProperty(ref reader, options, PropBuildScorerCount, null))
			{
				continue;
			}

			if (propComputeMaxScore.TryReadProperty(ref reader, options, PropComputeMaxScore, null))
			{
				continue;
			}

			if (propComputeMaxScoreCount.TryReadProperty(ref reader, options, PropComputeMaxScoreCount, null))
			{
				continue;
			}

			if (propCountWeight.TryReadProperty(ref reader, options, PropCountWeight, null))
			{
				continue;
			}

			if (propCountWeightCount.TryReadProperty(ref reader, options, PropCountWeightCount, null))
			{
				continue;
			}

			if (propCreateWeight.TryReadProperty(ref reader, options, PropCreateWeight, null))
			{
				continue;
			}

			if (propCreateWeightCount.TryReadProperty(ref reader, options, PropCreateWeightCount, null))
			{
				continue;
			}

			if (propMatch.TryReadProperty(ref reader, options, PropMatch, null))
			{
				continue;
			}

			if (propMatchCount.TryReadProperty(ref reader, options, PropMatchCount, null))
			{
				continue;
			}

			if (propNextDoc.TryReadProperty(ref reader, options, PropNextDoc, null))
			{
				continue;
			}

			if (propNextDocCount.TryReadProperty(ref reader, options, PropNextDocCount, null))
			{
				continue;
			}

			if (propScore.TryReadProperty(ref reader, options, PropScore, null))
			{
				continue;
			}

			if (propScoreCount.TryReadProperty(ref reader, options, PropScoreCount, null))
			{
				continue;
			}

			if (propSetMinCompetitiveScore.TryReadProperty(ref reader, options, PropSetMinCompetitiveScore, null))
			{
				continue;
			}

			if (propSetMinCompetitiveScoreCount.TryReadProperty(ref reader, options, PropSetMinCompetitiveScoreCount, null))
			{
				continue;
			}

			if (propShallowAdvance.TryReadProperty(ref reader, options, PropShallowAdvance, null))
			{
				continue;
			}

			if (propShallowAdvanceCount.TryReadProperty(ref reader, options, PropShallowAdvanceCount, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Advance = propAdvance.Value,
			AdvanceCount = propAdvanceCount.Value,
			BuildScorer = propBuildScorer.Value,
			BuildScorerCount = propBuildScorerCount.Value,
			ComputeMaxScore = propComputeMaxScore.Value,
			ComputeMaxScoreCount = propComputeMaxScoreCount.Value,
			CountWeight = propCountWeight.Value,
			CountWeightCount = propCountWeightCount.Value,
			CreateWeight = propCreateWeight.Value,
			CreateWeightCount = propCreateWeightCount.Value,
			Match = propMatch.Value,
			MatchCount = propMatchCount.Value,
			NextDoc = propNextDoc.Value,
			NextDocCount = propNextDocCount.Value,
			Score = propScore.Value,
			ScoreCount = propScoreCount.Value,
			SetMinCompetitiveScore = propSetMinCompetitiveScore.Value,
			SetMinCompetitiveScoreCount = propSetMinCompetitiveScoreCount.Value,
			ShallowAdvance = propShallowAdvance.Value,
			ShallowAdvanceCount = propShallowAdvanceCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdown value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAdvance, value.Advance, null, null);
		writer.WriteProperty(options, PropAdvanceCount, value.AdvanceCount, null, null);
		writer.WriteProperty(options, PropBuildScorer, value.BuildScorer, null, null);
		writer.WriteProperty(options, PropBuildScorerCount, value.BuildScorerCount, null, null);
		writer.WriteProperty(options, PropComputeMaxScore, value.ComputeMaxScore, null, null);
		writer.WriteProperty(options, PropComputeMaxScoreCount, value.ComputeMaxScoreCount, null, null);
		writer.WriteProperty(options, PropCountWeight, value.CountWeight, null, null);
		writer.WriteProperty(options, PropCountWeightCount, value.CountWeightCount, null, null);
		writer.WriteProperty(options, PropCreateWeight, value.CreateWeight, null, null);
		writer.WriteProperty(options, PropCreateWeightCount, value.CreateWeightCount, null, null);
		writer.WriteProperty(options, PropMatch, value.Match, null, null);
		writer.WriteProperty(options, PropMatchCount, value.MatchCount, null, null);
		writer.WriteProperty(options, PropNextDoc, value.NextDoc, null, null);
		writer.WriteProperty(options, PropNextDocCount, value.NextDocCount, null, null);
		writer.WriteProperty(options, PropScore, value.Score, null, null);
		writer.WriteProperty(options, PropScoreCount, value.ScoreCount, null, null);
		writer.WriteProperty(options, PropSetMinCompetitiveScore, value.SetMinCompetitiveScore, null, null);
		writer.WriteProperty(options, PropSetMinCompetitiveScoreCount, value.SetMinCompetitiveScoreCount, null, null);
		writer.WriteProperty(options, PropShallowAdvance, value.ShallowAdvance, null, null);
		writer.WriteProperty(options, PropShallowAdvanceCount, value.ShallowAdvanceCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.KnnQueryProfileBreakdownConverter))]
public sealed partial class KnnQueryProfileBreakdown
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public KnnQueryProfileBreakdown(long advance, long advanceCount, long buildScorer, long buildScorerCount, long computeMaxScore, long computeMaxScoreCount, long countWeight, long countWeightCount, long createWeight, long createWeightCount, long match, long matchCount, long nextDoc, long nextDocCount, long score, long scoreCount, long setMinCompetitiveScore, long setMinCompetitiveScoreCount, long shallowAdvance, long shallowAdvanceCount)
	{
		Advance = advance;
		AdvanceCount = advanceCount;
		BuildScorer = buildScorer;
		BuildScorerCount = buildScorerCount;
		ComputeMaxScore = computeMaxScore;
		ComputeMaxScoreCount = computeMaxScoreCount;
		CountWeight = countWeight;
		CountWeightCount = countWeightCount;
		CreateWeight = createWeight;
		CreateWeightCount = createWeightCount;
		Match = match;
		MatchCount = matchCount;
		NextDoc = nextDoc;
		NextDocCount = nextDocCount;
		Score = score;
		ScoreCount = scoreCount;
		SetMinCompetitiveScore = setMinCompetitiveScore;
		SetMinCompetitiveScoreCount = setMinCompetitiveScoreCount;
		ShallowAdvance = shallowAdvance;
		ShallowAdvanceCount = shallowAdvanceCount;
	}
#if NET7_0_OR_GREATER
	public KnnQueryProfileBreakdown()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public KnnQueryProfileBreakdown()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal KnnQueryProfileBreakdown(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	long Advance { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long AdvanceCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long BuildScorer { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long BuildScorerCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ComputeMaxScore { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ComputeMaxScoreCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CountWeight { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CountWeightCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CreateWeight { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long CreateWeightCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Match { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long MatchCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long NextDoc { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long NextDocCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Score { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ScoreCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long SetMinCompetitiveScore { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long SetMinCompetitiveScoreCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ShallowAdvance { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ShallowAdvanceCount { get; set; }
}