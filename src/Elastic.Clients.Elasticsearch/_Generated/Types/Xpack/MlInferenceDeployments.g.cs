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

internal sealed partial class MlInferenceDeploymentsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeployments>
{
	private static readonly System.Text.Json.JsonEncodedText PropCount = System.Text.Json.JsonEncodedText.Encode("count");
	private static readonly System.Text.Json.JsonEncodedText PropInferenceCounts = System.Text.Json.JsonEncodedText.Encode("inference_counts");
	private static readonly System.Text.Json.JsonEncodedText PropModelSizesBytes = System.Text.Json.JsonEncodedText.Encode("model_sizes_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropTimeMs = System.Text.Json.JsonEncodedText.Encode("time_ms");

	public override Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeployments Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propCount = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics> propInferenceCounts = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics> propModelSizesBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeploymentsTimeMs> propTimeMs = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCount.TryReadProperty(ref reader, options, PropCount, null))
			{
				continue;
			}

			if (propInferenceCounts.TryReadProperty(ref reader, options, PropInferenceCounts, null))
			{
				continue;
			}

			if (propModelSizesBytes.TryReadProperty(ref reader, options, PropModelSizesBytes, null))
			{
				continue;
			}

			if (propTimeMs.TryReadProperty(ref reader, options, PropTimeMs, null))
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
		return new Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeployments(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Count = propCount.Value,
			InferenceCounts = propInferenceCounts.Value,
			ModelSizesBytes = propModelSizesBytes.Value,
			TimeMs = propTimeMs.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeployments value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCount, value.Count, null, null);
		writer.WriteProperty(options, PropInferenceCounts, value.InferenceCounts, null, null);
		writer.WriteProperty(options, PropModelSizesBytes, value.ModelSizesBytes, null, null);
		writer.WriteProperty(options, PropTimeMs, value.TimeMs, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeploymentsConverter))]
public sealed partial class MlInferenceDeployments
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MlInferenceDeployments(int count, Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics inferenceCounts, Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics modelSizesBytes, Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeploymentsTimeMs timeMs)
	{
		Count = count;
		InferenceCounts = inferenceCounts;
		ModelSizesBytes = modelSizesBytes;
		TimeMs = timeMs;
	}
#if NET7_0_OR_GREATER
	public MlInferenceDeployments()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MlInferenceDeployments()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MlInferenceDeployments(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int Count { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics InferenceCounts { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JobStatistics ModelSizesBytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Xpack.MlInferenceDeploymentsTimeMs TimeMs { get; set; }
}