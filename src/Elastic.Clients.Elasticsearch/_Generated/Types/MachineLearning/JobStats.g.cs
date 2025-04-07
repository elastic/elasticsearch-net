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

internal sealed partial class JobStatsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.JobStats>
{
	private static readonly System.Text.Json.JsonEncodedText PropAssignmentExplanation = System.Text.Json.JsonEncodedText.Encode("assignment_explanation");
	private static readonly System.Text.Json.JsonEncodedText PropDataCounts = System.Text.Json.JsonEncodedText.Encode("data_counts");
	private static readonly System.Text.Json.JsonEncodedText PropDeleting = System.Text.Json.JsonEncodedText.Encode("deleting");
	private static readonly System.Text.Json.JsonEncodedText PropForecastsStats = System.Text.Json.JsonEncodedText.Encode("forecasts_stats");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropModelSizeStats = System.Text.Json.JsonEncodedText.Encode("model_size_stats");
	private static readonly System.Text.Json.JsonEncodedText PropNode = System.Text.Json.JsonEncodedText.Encode("node");
	private static readonly System.Text.Json.JsonEncodedText PropOpenTime = System.Text.Json.JsonEncodedText.Encode("open_time");
	private static readonly System.Text.Json.JsonEncodedText PropState = System.Text.Json.JsonEncodedText.Encode("state");
	private static readonly System.Text.Json.JsonEncodedText PropTimingStats = System.Text.Json.JsonEncodedText.Encode("timing_stats");

	public override Elastic.Clients.Elasticsearch.MachineLearning.JobStats Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propAssignmentExplanation = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataCounts> propDataCounts = default;
		LocalJsonValue<bool?> propDeleting = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics> propForecastsStats = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats> propModelSizeStats = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeCompact?> propNode = default;
		LocalJsonValue<System.DateTimeOffset?> propOpenTime = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobState> propState = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats> propTimingStats = default;
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

			if (propDeleting.TryReadProperty(ref reader, options, PropDeleting, null))
			{
				continue;
			}

			if (propForecastsStats.TryReadProperty(ref reader, options, PropForecastsStats, null))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propModelSizeStats.TryReadProperty(ref reader, options, PropModelSizeStats, null))
			{
				continue;
			}

			if (propNode.TryReadProperty(ref reader, options, PropNode, null))
			{
				continue;
			}

			if (propOpenTime.TryReadProperty(ref reader, options, PropOpenTime, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propState.TryReadProperty(ref reader, options, PropState, null))
			{
				continue;
			}

			if (propTimingStats.TryReadProperty(ref reader, options, PropTimingStats, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.JobStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AssignmentExplanation = propAssignmentExplanation.Value,
			DataCounts = propDataCounts.Value,
			Deleting = propDeleting.Value,
			ForecastsStats = propForecastsStats.Value,
			JobId = propJobId.Value,
			ModelSizeStats = propModelSizeStats.Value,
			Node = propNode.Value,
			OpenTime = propOpenTime.Value,
			State = propState.Value,
			TimingStats = propTimingStats.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.JobStats value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAssignmentExplanation, value.AssignmentExplanation, null, null);
		writer.WriteProperty(options, PropDataCounts, value.DataCounts, null, null);
		writer.WriteProperty(options, PropDeleting, value.Deleting, null, null);
		writer.WriteProperty(options, PropForecastsStats, value.ForecastsStats, null, null);
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropModelSizeStats, value.ModelSizeStats, null, null);
		writer.WriteProperty(options, PropNode, value.Node, null, null);
		writer.WriteProperty(options, PropOpenTime, value.OpenTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropState, value.State, null, null);
		writer.WriteProperty(options, PropTimingStats, value.TimingStats, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.JobStatsConverter))]
public sealed partial class JobStats
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public JobStats(Elastic.Clients.Elasticsearch.MachineLearning.DataCounts dataCounts, Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics forecastsStats, string jobId, Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats modelSizeStats, Elastic.Clients.Elasticsearch.MachineLearning.JobState state, Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats timingStats)
	{
		DataCounts = dataCounts;
		ForecastsStats = forecastsStats;
		JobId = jobId;
		ModelSizeStats = modelSizeStats;
		State = state;
		TimingStats = timingStats;
	}
#if NET7_0_OR_GREATER
	public JobStats()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public JobStats()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal JobStats(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// For open anomaly detection jobs only, contains messages relating to the selection of a node to run the job.
	/// </para>
	/// </summary>
	public string? AssignmentExplanation { get; set; }

	/// <summary>
	/// <para>
	/// An object that describes the quantity of input to the job and any related error counts.
	/// The <c>data_count</c> values are cumulative for the lifetime of a job.
	/// If a model snapshot is reverted or old results are deleted, the job counts are not reset.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.DataCounts DataCounts { get; set; }

	/// <summary>
	/// <para>
	/// Indicates that the process of deleting the job is in progress but not yet completed. It is only reported when <c>true</c>.
	/// </para>
	/// </summary>
	public bool? Deleting { get; set; }

	/// <summary>
	/// <para>
	/// An object that provides statistical information about forecasts belonging to this job.
	/// Some statistics are omitted if no forecasts have been made.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JobForecastStatistics ForecastsStats { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }

	/// <summary>
	/// <para>
	/// An object that provides information about the size and contents of the model.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.ModelSizeStats ModelSizeStats { get; set; }

	/// <summary>
	/// <para>
	/// Contains properties for the node that runs the job.
	/// This information is available only for open jobs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DiscoveryNodeCompact? Node { get; set; }

	/// <summary>
	/// <para>
	/// For open jobs only, the elapsed time for which the job has been open.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? OpenTime { get; set; }

	/// <summary>
	/// <para>
	/// The status of the anomaly detection job, which can be one of the following values: <c>closed</c>, <c>closing</c>, <c>failed</c>, <c>opened</c>, <c>opening</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JobState State { get; set; }

	/// <summary>
	/// <para>
	/// An object that provides statistical information about timing aspect of this job.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JobTimingStats TimingStats { get; set; }
}