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

internal sealed partial class JobStatisticsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics>
{
	private static readonly System.Text.Json.JsonEncodedText PropAvg = System.Text.Json.JsonEncodedText.Encode("avg");
	private static readonly System.Text.Json.JsonEncodedText PropMax = System.Text.Json.JsonEncodedText.Encode("max");
	private static readonly System.Text.Json.JsonEncodedText PropMin = System.Text.Json.JsonEncodedText.Encode("min");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");

	public override Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<double> propAvg = default;
		LocalJsonValue<double> propMax = default;
		LocalJsonValue<double> propMin = default;
		LocalJsonValue<double> propTotal = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAvg.TryReadProperty(ref reader, options, PropAvg, null))
			{
				continue;
			}

			if (propMax.TryReadProperty(ref reader, options, PropMax, null))
			{
				continue;
			}

			if (propMin.TryReadProperty(ref reader, options, PropMin, null))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Avg = propAvg.Value,
			Max = propMax.Value,
			Min = propMin.Value,
			Total = propTotal.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAvg, value.Avg, null, null);
		writer.WriteProperty(options, PropMax, value.Max, null, null);
		writer.WriteProperty(options, PropMin, value.Min, null, null);
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.JobStatisticsConverter))]
public sealed partial class JobStatistics
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JobStatistics(double avg, double max, double min, double total)
	{
		Avg = avg;
		Max = max;
		Min = min;
		Total = total;
	}
#if NET7_0_OR_GREATER
	public JobStatistics()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public JobStatistics()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JobStatistics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	double Avg { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Max { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Min { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Total { get; set; }
}