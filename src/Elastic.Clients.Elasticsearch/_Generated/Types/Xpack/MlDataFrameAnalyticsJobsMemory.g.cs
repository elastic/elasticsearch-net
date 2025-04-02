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

namespace Elastic.Clients.Elasticsearch.Xpack;

internal sealed partial class MlDataFrameAnalyticsJobsMemoryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.MlDataFrameAnalyticsJobsMemory>
{
	private static readonly System.Text.Json.JsonEncodedText PropPeakUsageBytes = System.Text.Json.JsonEncodedText.Encode("peak_usage_bytes");

	public override Elastic.Clients.Elasticsearch.Xpack.MlDataFrameAnalyticsJobsMemory Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics> propPeakUsageBytes = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPeakUsageBytes.TryReadProperty(ref reader, options, PropPeakUsageBytes, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.MlDataFrameAnalyticsJobsMemory(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			PeakUsageBytes = propPeakUsageBytes.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.MlDataFrameAnalyticsJobsMemory value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropPeakUsageBytes, value.PeakUsageBytes, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.MlDataFrameAnalyticsJobsMemoryConverter))]
public sealed partial class MlDataFrameAnalyticsJobsMemory
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MlDataFrameAnalyticsJobsMemory(Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics peakUsageBytes)
	{
		PeakUsageBytes = peakUsageBytes;
	}
#if NET7_0_OR_GREATER
	public MlDataFrameAnalyticsJobsMemory()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MlDataFrameAnalyticsJobsMemory()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MlDataFrameAnalyticsJobsMemory(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics PeakUsageBytes { get; set; }
}