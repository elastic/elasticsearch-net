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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class DataframeEvaluationSummaryAucRocCurveItemConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationSummaryAucRocCurveItem>
{
	private static readonly System.Text.Json.JsonEncodedText PropFpr = System.Text.Json.JsonEncodedText.Encode("fpr");
	private static readonly System.Text.Json.JsonEncodedText PropThreshold = System.Text.Json.JsonEncodedText.Encode("threshold");
	private static readonly System.Text.Json.JsonEncodedText PropTpr = System.Text.Json.JsonEncodedText.Encode("tpr");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationSummaryAucRocCurveItem Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double> propFpr = default;
		LocalJsonValue<double> propThreshold = default;
		LocalJsonValue<double> propTpr = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFpr.TryReadProperty(ref reader, options, PropFpr, null))
			{
				continue;
			}

			if (propThreshold.TryReadProperty(ref reader, options, PropThreshold, null))
			{
				continue;
			}

			if (propTpr.TryReadProperty(ref reader, options, PropTpr, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationSummaryAucRocCurveItem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Fpr = propFpr.Value,
			Threshold = propThreshold.Value,
			Tpr = propTpr.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationSummaryAucRocCurveItem value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFpr, value.Fpr, null, null);
		writer.WriteProperty(options, PropThreshold, value.Threshold, null, null);
		writer.WriteProperty(options, PropTpr, value.Tpr, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeEvaluationSummaryAucRocCurveItemConverter))]
public sealed partial class DataframeEvaluationSummaryAucRocCurveItem
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeEvaluationSummaryAucRocCurveItem(double fpr, double threshold, double tpr)
	{
		Fpr = fpr;
		Threshold = threshold;
		Tpr = tpr;
	}
#if NET7_0_OR_GREATER
	public DataframeEvaluationSummaryAucRocCurveItem()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataframeEvaluationSummaryAucRocCurveItem()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeEvaluationSummaryAucRocCurveItem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	double Fpr { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Threshold { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Tpr { get; set; }
}