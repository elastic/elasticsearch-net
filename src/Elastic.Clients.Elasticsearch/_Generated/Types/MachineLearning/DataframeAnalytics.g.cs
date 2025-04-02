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

internal sealed partial class DataframeAnalyticsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics>
{
	private static readonly System.Text.Json.JsonEncodedText PropAssignmentExplanation = System.Text.Json.JsonEncodedText.Encode("assignment_explanation");
	private static readonly System.Text.Json.JsonEncodedText PropDataCounts = System.Text.Json.JsonEncodedText.Encode("data_counts");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropMemoryUsage = System.Text.Json.JsonEncodedText.Encode("memory_usage");
	private static readonly System.Text.Json.JsonEncodedText PropNode = System.Text.Json.JsonEncodedText.Encode("node");
	private static readonly System.Text.Json.JsonEncodedText PropProgress = System.Text.Json.JsonEncodedText.Encode("progress");
	private static readonly System.Text.Json.JsonEncodedText PropState = System.Text.Json.JsonEncodedText.Encode("state");

	public override Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAssignmentExplanation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts> propDataCounts = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsMemoryUsage> propMemoryUsage = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.NodeAttributes?> propNode = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress>> propProgress = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeState> propState = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAssignmentExplanation.TryReadProperty(ref reader, options, PropAssignmentExplanation, null))
			{
				continue;
			}

			if (propDataCounts.TryReadProperty(ref reader, options, PropDataCounts, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propMemoryUsage.TryReadProperty(ref reader, options, PropMemoryUsage, null))
			{
				continue;
			}

			if (propNode.TryReadProperty(ref reader, options, PropNode, null))
			{
				continue;
			}

			if (propProgress.TryReadProperty(ref reader, options, PropProgress, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress>(o, null)!))
			{
				continue;
			}

			if (propState.TryReadProperty(ref reader, options, PropState, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AssignmentExplanation = propAssignmentExplanation.Value,
			DataCounts = propDataCounts.Value,
			Id = propId.Value,
			MemoryUsage = propMemoryUsage.Value,
			Node = propNode.Value,
			Progress = propProgress.Value,
			State = propState.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalytics value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAssignmentExplanation, value.AssignmentExplanation, null, null);
		writer.WriteProperty(options, PropDataCounts, value.DataCounts, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropMemoryUsage, value.MemoryUsage, null, null);
		writer.WriteProperty(options, PropNode, value.Node, null, null);
		writer.WriteProperty(options, PropProgress, value.Progress, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress>(o, v, null));
		writer.WriteProperty(options, PropState, value.State, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsConverter))]
public sealed partial class DataframeAnalytics
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DataframeAnalytics(Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts dataCounts, string id, Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsMemoryUsage memoryUsage, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress> progress, Elastic.Clients.Elasticsearch.MachineLearning.DataframeState state)
	{
		DataCounts = dataCounts;
		Id = id;
		MemoryUsage = memoryUsage;
		Progress = progress;
		State = state;
	}
#if NET7_0_OR_GREATER
	public DataframeAnalytics()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DataframeAnalytics()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DataframeAnalytics(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// For running jobs only, contains messages relating to the selection of a node to run the job.
	/// </para>
	/// </summary>
	public string? AssignmentExplanation { get; set; }

	/// <summary>
	/// <para>
	/// An object that provides counts for the quantity of documents skipped, used in training, or available for testing.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsDataCounts DataCounts { get; set; }

	/// <summary>
	/// <para>
	/// The unique identifier of the data frame analytics job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Id { get; set; }

	/// <summary>
	/// <para>
	/// An object describing memory usage of the analytics. It is present only after the job is started and memory usage is reported.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsMemoryUsage MemoryUsage { get; set; }

	/// <summary>
	/// <para>
	/// Contains properties for the node that runs the job. This information is available only for running jobs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.NodeAttributes? Node { get; set; }

	/// <summary>
	/// <para>
	/// The progress report of the data frame analytics job by phase.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.DataframeAnalyticsStatsProgress> Progress { get; set; }

	/// <summary>
	/// <para>
	/// The status of the data frame analytics job, which can be one of the following values: failed, started, starting, stopping, stopped.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.DataframeState State { get; set; }
}