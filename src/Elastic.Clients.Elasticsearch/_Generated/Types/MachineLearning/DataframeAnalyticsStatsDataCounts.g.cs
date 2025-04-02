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

internal sealed partial class DataframeAnalyticsStatsDataCountsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts>
{
	private static readonly System.Text.Json.JsonEncodedText PropSkippedDocsCount = System.Text.Json.JsonEncodedText.Encode("skipped_docs_count");
	private static readonly System.Text.Json.JsonEncodedText PropTestDocsCount = System.Text.Json.JsonEncodedText.Encode("test_docs_count");
	private static readonly System.Text.Json.JsonEncodedText PropTrainingDocsCount = System.Text.Json.JsonEncodedText.Encode("training_docs_count");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propSkippedDocsCount = default;
		LocalJsonValue<int> propTestDocsCount = default;
		LocalJsonValue<int> propTrainingDocsCount = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propSkippedDocsCount.TryReadProperty(ref reader, options, PropSkippedDocsCount, null))
			{
				continue;
			}

			if (propTestDocsCount.TryReadProperty(ref reader, options, PropTestDocsCount, null))
			{
				continue;
			}

			if (propTrainingDocsCount.TryReadProperty(ref reader, options, PropTrainingDocsCount, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			SkippedDocsCount = propSkippedDocsCount.Value,
			TestDocsCount = propTestDocsCount.Value,
			TrainingDocsCount = propTrainingDocsCount.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropSkippedDocsCount, value.SkippedDocsCount, null, null);
		writer.WriteProperty(options, PropTestDocsCount, value.TestDocsCount, null, null);
		writer.WriteProperty(options, PropTrainingDocsCount, value.TrainingDocsCount, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCountsConverter))]
public sealed partial class DataframeAnalyticsStatsDataCounts
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalyticsStatsDataCounts(int skippedDocsCount, int testDocsCount, int trainingDocsCount)
	{
		SkippedDocsCount = skippedDocsCount;
		TestDocsCount = testDocsCount;
		TrainingDocsCount = trainingDocsCount;
	}
#if NET7_0_OR_GREATER
	public DataframeAnalyticsStatsDataCounts()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataframeAnalyticsStatsDataCounts()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeAnalyticsStatsDataCounts(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of documents that are skipped during the analysis because they contained values that are not supported by the analysis. For example, outlier detection does not support missing fields so it skips documents with missing fields. Likewise, all types of analysis skip documents that contain arrays with more than one element.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int SkippedDocsCount { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that are not used for training the model and can be used for testing.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int TestDocsCount { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that are used for training the model.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int TrainingDocsCount { get; set; }
}