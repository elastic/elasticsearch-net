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

namespace Elastic.Clients.Elasticsearch.Core.RankEval;

internal sealed partial class RankEvalMetricPrecisionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision>
{
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreUnlabeled = System.Text.Json.JsonEncodedText.Encode("ignore_unlabeled");
	private static readonly System.Text.Json.JsonEncodedText PropK = System.Text.Json.JsonEncodedText.Encode("k");
	private static readonly System.Text.Json.JsonEncodedText PropRelevantRatingThreshold = System.Text.Json.JsonEncodedText.Encode("relevant_rating_threshold");

	public override Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propIgnoreUnlabeled = default;
		LocalJsonValue<int?> propK = default;
		LocalJsonValue<int?> propRelevantRatingThreshold = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propIgnoreUnlabeled.TryReadProperty(ref reader, options, PropIgnoreUnlabeled, null))
			{
				continue;
			}

			if (propK.TryReadProperty(ref reader, options, PropK, null))
			{
				continue;
			}

			if (propRelevantRatingThreshold.TryReadProperty(ref reader, options, PropRelevantRatingThreshold, null))
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
		return new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			IgnoreUnlabeled = propIgnoreUnlabeled.Value,
			K = propK.Value,
			RelevantRatingThreshold = propRelevantRatingThreshold.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropIgnoreUnlabeled, value.IgnoreUnlabeled, null, null);
		writer.WriteProperty(options, PropK, value.K, null, null);
		writer.WriteProperty(options, PropRelevantRatingThreshold, value.RelevantRatingThreshold, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Precision at K (P@k)
/// </para>
/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-rank-eval.html#k-precision">Learn more about this API in the Elasticsearch documentation.</see></para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionConverter))]
public sealed partial class RankEvalMetricPrecision
{
#if NET7_0_OR_GREATER
	public RankEvalMetricPrecision()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public RankEvalMetricPrecision()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal RankEvalMetricPrecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Controls how unlabeled documents in the search results are counted. If set to true, unlabeled documents are ignored and neither count as relevant or irrelevant. Set to false (the default), they are treated as irrelevant.
	/// </para>
	/// </summary>
	public bool? IgnoreUnlabeled { get; set; }

	/// <summary>
	/// <para>
	/// Sets the maximum number of documents retrieved per query. This value will act in place of the usual size parameter in the query.
	/// </para>
	/// </summary>
	public int? K { get; set; }

	/// <summary>
	/// <para>
	/// Sets the rating threshold above which documents are considered to be "relevant".
	/// </para>
	/// </summary>
	public int? RelevantRatingThreshold { get; set; }
}

/// <summary>
/// <para>
/// Precision at K (P@k)
/// </para>
/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/current/search-rank-eval.html#k-precision">Learn more about this API in the Elasticsearch documentation.</see></para>
/// </summary>
public readonly partial struct RankEvalMetricPrecisionDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankEvalMetricPrecisionDescriptor(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public RankEvalMetricPrecisionDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision instance) => new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Controls how unlabeled documents in the search results are counted. If set to true, unlabeled documents are ignored and neither count as relevant or irrelevant. Set to false (the default), they are treated as irrelevant.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor IgnoreUnlabeled(bool? value = true)
	{
		Instance.IgnoreUnlabeled = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets the maximum number of documents retrieved per query. This value will act in place of the usual size parameter in the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor K(int? value)
	{
		Instance.K = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets the rating threshold above which documents are considered to be "relevant".
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor RelevantRatingThreshold(int? value)
	{
		Instance.RelevantRatingThreshold = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision Build(System.Action<Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecisionDescriptor(new Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}