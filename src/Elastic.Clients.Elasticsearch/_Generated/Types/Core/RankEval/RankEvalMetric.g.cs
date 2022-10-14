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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Core.RankEval;
public sealed partial class RankEvalMetric
{
	[JsonInclude]
	[JsonPropertyName("dcg")]
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDiscountedCumulativeGain? Dcg { get; set; }

	[JsonInclude]
	[JsonPropertyName("expected_reciprocal_rank")]
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricExpectedReciprocalRank? ExpectedReciprocalRank { get; set; }

	[JsonInclude]
	[JsonPropertyName("mean_reciprocal_rank")]
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricMeanReciprocalRank? MeanReciprocalRank { get; set; }

	[JsonInclude]
	[JsonPropertyName("precision")]
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision? Precision { get; set; }

	[JsonInclude]
	[JsonPropertyName("recall")]
	public Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricRecall? Recall { get; set; }
}

public sealed partial class RankEvalMetricDescriptor : SerializableDescriptor<RankEvalMetricDescriptor>
{
	internal RankEvalMetricDescriptor(Action<RankEvalMetricDescriptor> configure) => configure.Invoke(this);
	public RankEvalMetricDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDiscountedCumulativeGain? DcgValue { get; set; }

	private RankEvalMetricDiscountedCumulativeGainDescriptor DcgDescriptor { get; set; }

	private Action<RankEvalMetricDiscountedCumulativeGainDescriptor> DcgDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricExpectedReciprocalRank? ExpectedReciprocalRankValue { get; set; }

	private RankEvalMetricExpectedReciprocalRankDescriptor ExpectedReciprocalRankDescriptor { get; set; }

	private Action<RankEvalMetricExpectedReciprocalRankDescriptor> ExpectedReciprocalRankDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricMeanReciprocalRank? MeanReciprocalRankValue { get; set; }

	private RankEvalMetricMeanReciprocalRankDescriptor MeanReciprocalRankDescriptor { get; set; }

	private Action<RankEvalMetricMeanReciprocalRankDescriptor> MeanReciprocalRankDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision? PrecisionValue { get; set; }

	private RankEvalMetricPrecisionDescriptor PrecisionDescriptor { get; set; }

	private Action<RankEvalMetricPrecisionDescriptor> PrecisionDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricRecall? RecallValue { get; set; }

	private RankEvalMetricRecallDescriptor RecallDescriptor { get; set; }

	private Action<RankEvalMetricRecallDescriptor> RecallDescriptorAction { get; set; }

	public RankEvalMetricDescriptor Dcg(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricDiscountedCumulativeGain? dcg)
	{
		DcgDescriptor = null;
		DcgDescriptorAction = null;
		DcgValue = dcg;
		return Self;
	}

	public RankEvalMetricDescriptor Dcg(RankEvalMetricDiscountedCumulativeGainDescriptor descriptor)
	{
		DcgValue = null;
		DcgDescriptorAction = null;
		DcgDescriptor = descriptor;
		return Self;
	}

	public RankEvalMetricDescriptor Dcg(Action<RankEvalMetricDiscountedCumulativeGainDescriptor> configure)
	{
		DcgValue = null;
		DcgDescriptor = null;
		DcgDescriptorAction = configure;
		return Self;
	}

	public RankEvalMetricDescriptor ExpectedReciprocalRank(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricExpectedReciprocalRank? expectedReciprocalRank)
	{
		ExpectedReciprocalRankDescriptor = null;
		ExpectedReciprocalRankDescriptorAction = null;
		ExpectedReciprocalRankValue = expectedReciprocalRank;
		return Self;
	}

	public RankEvalMetricDescriptor ExpectedReciprocalRank(RankEvalMetricExpectedReciprocalRankDescriptor descriptor)
	{
		ExpectedReciprocalRankValue = null;
		ExpectedReciprocalRankDescriptorAction = null;
		ExpectedReciprocalRankDescriptor = descriptor;
		return Self;
	}

	public RankEvalMetricDescriptor ExpectedReciprocalRank(Action<RankEvalMetricExpectedReciprocalRankDescriptor> configure)
	{
		ExpectedReciprocalRankValue = null;
		ExpectedReciprocalRankDescriptor = null;
		ExpectedReciprocalRankDescriptorAction = configure;
		return Self;
	}

	public RankEvalMetricDescriptor MeanReciprocalRank(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricMeanReciprocalRank? meanReciprocalRank)
	{
		MeanReciprocalRankDescriptor = null;
		MeanReciprocalRankDescriptorAction = null;
		MeanReciprocalRankValue = meanReciprocalRank;
		return Self;
	}

	public RankEvalMetricDescriptor MeanReciprocalRank(RankEvalMetricMeanReciprocalRankDescriptor descriptor)
	{
		MeanReciprocalRankValue = null;
		MeanReciprocalRankDescriptorAction = null;
		MeanReciprocalRankDescriptor = descriptor;
		return Self;
	}

	public RankEvalMetricDescriptor MeanReciprocalRank(Action<RankEvalMetricMeanReciprocalRankDescriptor> configure)
	{
		MeanReciprocalRankValue = null;
		MeanReciprocalRankDescriptor = null;
		MeanReciprocalRankDescriptorAction = configure;
		return Self;
	}

	public RankEvalMetricDescriptor Precision(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricPrecision? precision)
	{
		PrecisionDescriptor = null;
		PrecisionDescriptorAction = null;
		PrecisionValue = precision;
		return Self;
	}

	public RankEvalMetricDescriptor Precision(RankEvalMetricPrecisionDescriptor descriptor)
	{
		PrecisionValue = null;
		PrecisionDescriptorAction = null;
		PrecisionDescriptor = descriptor;
		return Self;
	}

	public RankEvalMetricDescriptor Precision(Action<RankEvalMetricPrecisionDescriptor> configure)
	{
		PrecisionValue = null;
		PrecisionDescriptor = null;
		PrecisionDescriptorAction = configure;
		return Self;
	}

	public RankEvalMetricDescriptor Recall(Elastic.Clients.Elasticsearch.Core.RankEval.RankEvalMetricRecall? recall)
	{
		RecallDescriptor = null;
		RecallDescriptorAction = null;
		RecallValue = recall;
		return Self;
	}

	public RankEvalMetricDescriptor Recall(RankEvalMetricRecallDescriptor descriptor)
	{
		RecallValue = null;
		RecallDescriptorAction = null;
		RecallDescriptor = descriptor;
		return Self;
	}

	public RankEvalMetricDescriptor Recall(Action<RankEvalMetricRecallDescriptor> configure)
	{
		RecallValue = null;
		RecallDescriptor = null;
		RecallDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DcgDescriptor is not null)
		{
			writer.WritePropertyName("dcg");
			JsonSerializer.Serialize(writer, DcgDescriptor, options);
		}
		else if (DcgDescriptorAction is not null)
		{
			writer.WritePropertyName("dcg");
			JsonSerializer.Serialize(writer, new RankEvalMetricDiscountedCumulativeGainDescriptor(DcgDescriptorAction), options);
		}
		else if (DcgValue is not null)
		{
			writer.WritePropertyName("dcg");
			JsonSerializer.Serialize(writer, DcgValue, options);
		}

		if (ExpectedReciprocalRankDescriptor is not null)
		{
			writer.WritePropertyName("expected_reciprocal_rank");
			JsonSerializer.Serialize(writer, ExpectedReciprocalRankDescriptor, options);
		}
		else if (ExpectedReciprocalRankDescriptorAction is not null)
		{
			writer.WritePropertyName("expected_reciprocal_rank");
			JsonSerializer.Serialize(writer, new RankEvalMetricExpectedReciprocalRankDescriptor(ExpectedReciprocalRankDescriptorAction), options);
		}
		else if (ExpectedReciprocalRankValue is not null)
		{
			writer.WritePropertyName("expected_reciprocal_rank");
			JsonSerializer.Serialize(writer, ExpectedReciprocalRankValue, options);
		}

		if (MeanReciprocalRankDescriptor is not null)
		{
			writer.WritePropertyName("mean_reciprocal_rank");
			JsonSerializer.Serialize(writer, MeanReciprocalRankDescriptor, options);
		}
		else if (MeanReciprocalRankDescriptorAction is not null)
		{
			writer.WritePropertyName("mean_reciprocal_rank");
			JsonSerializer.Serialize(writer, new RankEvalMetricMeanReciprocalRankDescriptor(MeanReciprocalRankDescriptorAction), options);
		}
		else if (MeanReciprocalRankValue is not null)
		{
			writer.WritePropertyName("mean_reciprocal_rank");
			JsonSerializer.Serialize(writer, MeanReciprocalRankValue, options);
		}

		if (PrecisionDescriptor is not null)
		{
			writer.WritePropertyName("precision");
			JsonSerializer.Serialize(writer, PrecisionDescriptor, options);
		}
		else if (PrecisionDescriptorAction is not null)
		{
			writer.WritePropertyName("precision");
			JsonSerializer.Serialize(writer, new RankEvalMetricPrecisionDescriptor(PrecisionDescriptorAction), options);
		}
		else if (PrecisionValue is not null)
		{
			writer.WritePropertyName("precision");
			JsonSerializer.Serialize(writer, PrecisionValue, options);
		}

		if (RecallDescriptor is not null)
		{
			writer.WritePropertyName("recall");
			JsonSerializer.Serialize(writer, RecallDescriptor, options);
		}
		else if (RecallDescriptorAction is not null)
		{
			writer.WritePropertyName("recall");
			JsonSerializer.Serialize(writer, new RankEvalMetricRecallDescriptor(RecallDescriptorAction), options);
		}
		else if (RecallValue is not null)
		{
			writer.WritePropertyName("recall");
			JsonSerializer.Serialize(writer, RecallValue, options);
		}

		writer.WriteEndObject();
	}
}