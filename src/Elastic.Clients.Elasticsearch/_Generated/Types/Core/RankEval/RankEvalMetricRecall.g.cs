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

namespace Elastic.Clients.Elasticsearch.Core.RankEval;

/// <summary>
/// <para>
/// Recall at K (R@k)
/// </para>
/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.17/search-rank-eval.html#k-recall">Learn more about this API in the Elasticsearch documentation.</see></para>
/// </summary>
public sealed partial class RankEvalMetricRecall
{
	/// <summary>
	/// <para>
	/// Sets the maximum number of documents retrieved per query. This value will act in place of the usual size parameter in the query.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("k")]
	public int? k { get; set; }

	/// <summary>
	/// <para>
	/// Sets the rating threshold above which documents are considered to be "relevant".
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("relevant_rating_threshold")]
	public int? RelevantRatingThreshold { get; set; }
}

/// <summary>
/// <para>
/// Recall at K (R@k)
/// </para>
/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.17/search-rank-eval.html#k-recall">Learn more about this API in the Elasticsearch documentation.</see></para>
/// </summary>
public sealed partial class RankEvalMetricRecallDescriptor : SerializableDescriptor<RankEvalMetricRecallDescriptor>
{
	internal RankEvalMetricRecallDescriptor(Action<RankEvalMetricRecallDescriptor> configure) => configure.Invoke(this);

	public RankEvalMetricRecallDescriptor() : base()
	{
	}

	private int? kValue { get; set; }
	private int? RelevantRatingThresholdValue { get; set; }

	/// <summary>
	/// <para>
	/// Sets the maximum number of documents retrieved per query. This value will act in place of the usual size parameter in the query.
	/// </para>
	/// </summary>
	public RankEvalMetricRecallDescriptor k(int? k)
	{
		kValue = k;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Sets the rating threshold above which documents are considered to be "relevant".
	/// </para>
	/// </summary>
	public RankEvalMetricRecallDescriptor RelevantRatingThreshold(int? relevantRatingThreshold)
	{
		RelevantRatingThresholdValue = relevantRatingThreshold;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (kValue.HasValue)
		{
			writer.WritePropertyName("k");
			writer.WriteNumberValue(kValue.Value);
		}

		if (RelevantRatingThresholdValue.HasValue)
		{
			writer.WritePropertyName("relevant_rating_threshold");
			writer.WriteNumberValue(RelevantRatingThresholdValue.Value);
		}

		writer.WriteEndObject();
	}
}