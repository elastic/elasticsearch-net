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

internal sealed partial class JobConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Job>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowLazyOpen = System.Text.Json.JsonEncodedText.Encode("allow_lazy_open");
	private static readonly System.Text.Json.JsonEncodedText PropAnalysisConfig = System.Text.Json.JsonEncodedText.Encode("analysis_config");
	private static readonly System.Text.Json.JsonEncodedText PropAnalysisLimits = System.Text.Json.JsonEncodedText.Encode("analysis_limits");
	private static readonly System.Text.Json.JsonEncodedText PropBackgroundPersistInterval = System.Text.Json.JsonEncodedText.Encode("background_persist_interval");
	private static readonly System.Text.Json.JsonEncodedText PropBlocked = System.Text.Json.JsonEncodedText.Encode("blocked");
	private static readonly System.Text.Json.JsonEncodedText PropCreateTime = System.Text.Json.JsonEncodedText.Encode("create_time");
	private static readonly System.Text.Json.JsonEncodedText PropCustomSettings = System.Text.Json.JsonEncodedText.Encode("custom_settings");
	private static readonly System.Text.Json.JsonEncodedText PropDailyModelSnapshotRetentionAfterDays = System.Text.Json.JsonEncodedText.Encode("daily_model_snapshot_retention_after_days");
	private static readonly System.Text.Json.JsonEncodedText PropDataDescription = System.Text.Json.JsonEncodedText.Encode("data_description");
	private static readonly System.Text.Json.JsonEncodedText PropDatafeedConfig = System.Text.Json.JsonEncodedText.Encode("datafeed_config");
	private static readonly System.Text.Json.JsonEncodedText PropDeleting = System.Text.Json.JsonEncodedText.Encode("deleting");
	private static readonly System.Text.Json.JsonEncodedText PropDescription = System.Text.Json.JsonEncodedText.Encode("description");
	private static readonly System.Text.Json.JsonEncodedText PropFinishedTime = System.Text.Json.JsonEncodedText.Encode("finished_time");
	private static readonly System.Text.Json.JsonEncodedText PropGroups = System.Text.Json.JsonEncodedText.Encode("groups");
	private static readonly System.Text.Json.JsonEncodedText PropJobId = System.Text.Json.JsonEncodedText.Encode("job_id");
	private static readonly System.Text.Json.JsonEncodedText PropJobType = System.Text.Json.JsonEncodedText.Encode("job_type");
	private static readonly System.Text.Json.JsonEncodedText PropJobVersion = System.Text.Json.JsonEncodedText.Encode("job_version");
	private static readonly System.Text.Json.JsonEncodedText PropModelPlotConfig = System.Text.Json.JsonEncodedText.Encode("model_plot_config");
	private static readonly System.Text.Json.JsonEncodedText PropModelSnapshotId = System.Text.Json.JsonEncodedText.Encode("model_snapshot_id");
	private static readonly System.Text.Json.JsonEncodedText PropModelSnapshotRetentionDays = System.Text.Json.JsonEncodedText.Encode("model_snapshot_retention_days");
	private static readonly System.Text.Json.JsonEncodedText PropRenormalizationWindowDays = System.Text.Json.JsonEncodedText.Encode("renormalization_window_days");
	private static readonly System.Text.Json.JsonEncodedText PropResultsIndexName = System.Text.Json.JsonEncodedText.Encode("results_index_name");
	private static readonly System.Text.Json.JsonEncodedText PropResultsRetentionDays = System.Text.Json.JsonEncodedText.Encode("results_retention_days");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Job Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAllowLazyOpen = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig> propAnalysisConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits?> propAnalysisLimits = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propBackgroundPersistInterval = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JobBlocked?> propBlocked = default;
		LocalJsonValue<System.DateTime?> propCreateTime = default;
		LocalJsonValue<object?> propCustomSettings = default;
		LocalJsonValue<long?> propDailyModelSnapshotRetentionAfterDays = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataDescription> propDataDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Datafeed?> propDatafeedConfig = default;
		LocalJsonValue<bool?> propDeleting = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<System.DateTime?> propFinishedTime = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propGroups = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<string?> propJobType = default;
		LocalJsonValue<string?> propJobVersion = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig?> propModelPlotConfig = default;
		LocalJsonValue<string?> propModelSnapshotId = default;
		LocalJsonValue<long> propModelSnapshotRetentionDays = default;
		LocalJsonValue<long?> propRenormalizationWindowDays = default;
		LocalJsonValue<string> propResultsIndexName = default;
		LocalJsonValue<long?> propResultsRetentionDays = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowLazyOpen.TryReadProperty(ref reader, options, PropAllowLazyOpen, null))
			{
				continue;
			}

			if (propAnalysisConfig.TryReadProperty(ref reader, options, PropAnalysisConfig, null))
			{
				continue;
			}

			if (propAnalysisLimits.TryReadProperty(ref reader, options, PropAnalysisLimits, null))
			{
				continue;
			}

			if (propBackgroundPersistInterval.TryReadProperty(ref reader, options, PropBackgroundPersistInterval, null))
			{
				continue;
			}

			if (propBlocked.TryReadProperty(ref reader, options, PropBlocked, null))
			{
				continue;
			}

			if (propCreateTime.TryReadProperty(ref reader, options, PropCreateTime, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propCustomSettings.TryReadProperty(ref reader, options, PropCustomSettings, null))
			{
				continue;
			}

			if (propDailyModelSnapshotRetentionAfterDays.TryReadProperty(ref reader, options, PropDailyModelSnapshotRetentionAfterDays, null))
			{
				continue;
			}

			if (propDataDescription.TryReadProperty(ref reader, options, PropDataDescription, null))
			{
				continue;
			}

			if (propDatafeedConfig.TryReadProperty(ref reader, options, PropDatafeedConfig, null))
			{
				continue;
			}

			if (propDeleting.TryReadProperty(ref reader, options, PropDeleting, null))
			{
				continue;
			}

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propFinishedTime.TryReadProperty(ref reader, options, PropFinishedTime, static System.DateTime? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTime?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propGroups.TryReadProperty(ref reader, options, PropGroups, static System.Collections.Generic.IReadOnlyCollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propJobId.TryReadProperty(ref reader, options, PropJobId, null))
			{
				continue;
			}

			if (propJobType.TryReadProperty(ref reader, options, PropJobType, null))
			{
				continue;
			}

			if (propJobVersion.TryReadProperty(ref reader, options, PropJobVersion, null))
			{
				continue;
			}

			if (propModelPlotConfig.TryReadProperty(ref reader, options, PropModelPlotConfig, null))
			{
				continue;
			}

			if (propModelSnapshotId.TryReadProperty(ref reader, options, PropModelSnapshotId, null))
			{
				continue;
			}

			if (propModelSnapshotRetentionDays.TryReadProperty(ref reader, options, PropModelSnapshotRetentionDays, null))
			{
				continue;
			}

			if (propRenormalizationWindowDays.TryReadProperty(ref reader, options, PropRenormalizationWindowDays, null))
			{
				continue;
			}

			if (propResultsIndexName.TryReadProperty(ref reader, options, PropResultsIndexName, null))
			{
				continue;
			}

			if (propResultsRetentionDays.TryReadProperty(ref reader, options, PropResultsRetentionDays, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Job(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowLazyOpen = propAllowLazyOpen.Value,
			AnalysisConfig = propAnalysisConfig.Value,
			AnalysisLimits = propAnalysisLimits.Value,
			BackgroundPersistInterval = propBackgroundPersistInterval.Value,
			Blocked = propBlocked.Value,
			CreateTime = propCreateTime.Value,
			CustomSettings = propCustomSettings.Value,
			DailyModelSnapshotRetentionAfterDays = propDailyModelSnapshotRetentionAfterDays.Value,
			DataDescription = propDataDescription.Value,
			DatafeedConfig = propDatafeedConfig.Value,
			Deleting = propDeleting.Value,
			Description = propDescription.Value,
			FinishedTime = propFinishedTime.Value,
			Groups = propGroups.Value,
			JobId = propJobId.Value,
			JobType = propJobType.Value,
			JobVersion = propJobVersion.Value,
			ModelPlotConfig = propModelPlotConfig.Value,
			ModelSnapshotId = propModelSnapshotId.Value,
			ModelSnapshotRetentionDays = propModelSnapshotRetentionDays.Value,
			RenormalizationWindowDays = propRenormalizationWindowDays.Value,
			ResultsIndexName = propResultsIndexName.Value,
			ResultsRetentionDays = propResultsRetentionDays.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Job value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowLazyOpen, value.AllowLazyOpen, null, null);
		writer.WriteProperty(options, PropAnalysisConfig, value.AnalysisConfig, null, null);
		writer.WriteProperty(options, PropAnalysisLimits, value.AnalysisLimits, null, null);
		writer.WriteProperty(options, PropBackgroundPersistInterval, value.BackgroundPersistInterval, null, null);
		writer.WriteProperty(options, PropBlocked, value.Blocked, null, null);
		writer.WriteProperty(options, PropCreateTime, value.CreateTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropCustomSettings, value.CustomSettings, null, null);
		writer.WriteProperty(options, PropDailyModelSnapshotRetentionAfterDays, value.DailyModelSnapshotRetentionAfterDays, null, null);
		writer.WriteProperty(options, PropDataDescription, value.DataDescription, null, null);
		writer.WriteProperty(options, PropDatafeedConfig, value.DatafeedConfig, null, null);
		writer.WriteProperty(options, PropDeleting, value.Deleting, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropFinishedTime, value.FinishedTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTime? v) => w.WriteValueEx<System.DateTime?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropGroups, value.Groups, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropJobId, value.JobId, null, null);
		writer.WriteProperty(options, PropJobType, value.JobType, null, null);
		writer.WriteProperty(options, PropJobVersion, value.JobVersion, null, null);
		writer.WriteProperty(options, PropModelPlotConfig, value.ModelPlotConfig, null, null);
		writer.WriteProperty(options, PropModelSnapshotId, value.ModelSnapshotId, null, null);
		writer.WriteProperty(options, PropModelSnapshotRetentionDays, value.ModelSnapshotRetentionDays, null, null);
		writer.WriteProperty(options, PropRenormalizationWindowDays, value.RenormalizationWindowDays, null, null);
		writer.WriteProperty(options, PropResultsIndexName, value.ResultsIndexName, null, null);
		writer.WriteProperty(options, PropResultsRetentionDays, value.ResultsRetentionDays, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.JobConverter))]
public sealed partial class Job
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Job(bool allowLazyOpen, Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig analysisConfig, Elastic.Clients.Elasticsearch.MachineLearning.DataDescription dataDescription, string jobId, long modelSnapshotRetentionDays, string resultsIndexName)
	{
		AllowLazyOpen = allowLazyOpen;
		AnalysisConfig = analysisConfig;
		DataDescription = dataDescription;
		JobId = jobId;
		ModelSnapshotRetentionDays = modelSnapshotRetentionDays;
		ResultsIndexName = resultsIndexName;
	}
#if NET7_0_OR_GREATER
	public Job()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Job()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Job(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Advanced configuration option.
	/// Specifies whether this job can open when there is insufficient machine learning node capacity for it to be immediately assigned to a node.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool AllowLazyOpen { get; set; }

	/// <summary>
	/// <para>
	/// The analysis configuration, which specifies how to analyze the data.
	/// After you create a job, you cannot change the analysis configuration; all the properties are informational.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfig AnalysisConfig { get; set; }

	/// <summary>
	/// <para>
	/// Limits can be applied for the resources required to hold the mathematical models in memory.
	/// These limits are approximate and can be set per job.
	/// They do not control the memory used by other processes, for example the Elasticsearch Java processes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits? AnalysisLimits { get; set; }

	/// <summary>
	/// <para>
	/// Advanced configuration option.
	/// The time between each periodic persistence of the model.
	/// The default value is a randomized value between 3 to 4 hours, which avoids all jobs persisting at exactly the same time.
	/// The smallest allowed value is 1 hour.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? BackgroundPersistInterval { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.JobBlocked? Blocked { get; set; }
	public System.DateTime? CreateTime { get; set; }

	/// <summary>
	/// <para>
	/// Advanced configuration option.
	/// Contains custom metadata about the job.
	/// </para>
	/// </summary>
	public object? CustomSettings { get; set; }

	/// <summary>
	/// <para>
	/// Advanced configuration option, which affects the automatic removal of old model snapshots for this job.
	/// It specifies a period of time (in days) after which only the first snapshot per day is retained.
	/// This period is relative to the timestamp of the most recent snapshot for this job.
	/// Valid values range from 0 to <c>model_snapshot_retention_days</c>.
	/// </para>
	/// </summary>
	public long? DailyModelSnapshotRetentionAfterDays { get; set; }

	/// <summary>
	/// <para>
	/// The data description defines the format of the input data when you send data to the job by using the post data API.
	/// Note that when configuring a datafeed, these properties are automatically set.
	/// When data is received via the post data API, it is not stored in Elasticsearch.
	/// Only the results for anomaly detection are retained.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.DataDescription DataDescription { get; set; }

	/// <summary>
	/// <para>
	/// The datafeed, which retrieves data from Elasticsearch for analysis by the job.
	/// You can associate only one datafeed with each anomaly detection job.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Datafeed? DatafeedConfig { get; set; }

	/// <summary>
	/// <para>
	/// Indicates that the process of deleting the job is in progress but not yet completed.
	/// It is only reported when <c>true</c>.
	/// </para>
	/// </summary>
	public bool? Deleting { get; set; }

	/// <summary>
	/// <para>
	/// A description of the job.
	/// </para>
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// <para>
	/// If the job closed or failed, this is the time the job finished, otherwise it is <c>null</c>.
	/// This property is informational; you cannot change its value.
	/// </para>
	/// </summary>
	public System.DateTime? FinishedTime { get; set; }

	/// <summary>
	/// <para>
	/// A list of job groups.
	/// A job can belong to no groups or many.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<string>? Groups { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the anomaly detection job.
	/// This identifier can contain lowercase alphanumeric characters (a-z and 0-9), hyphens, and underscores.
	/// It must start and end with alphanumeric characters.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string JobId { get; set; }

	/// <summary>
	/// <para>
	/// Reserved for future use, currently set to <c>anomaly_detector</c>.
	/// </para>
	/// </summary>
	public string? JobType { get; set; }

	/// <summary>
	/// <para>
	/// The machine learning configuration version number at which the the job was created.
	/// </para>
	/// </summary>
	public string? JobVersion { get; set; }

	/// <summary>
	/// <para>
	/// This advanced configuration option stores model information along with the results.
	/// It provides a more detailed view into anomaly detection.
	/// Model plot provides a simplified and indicative view of the model and its bounds.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig? ModelPlotConfig { get; set; }
	public string? ModelSnapshotId { get; set; }

	/// <summary>
	/// <para>
	/// Advanced configuration option, which affects the automatic removal of old model snapshots for this job.
	/// It specifies the maximum period of time (in days) that snapshots are retained.
	/// This period is relative to the timestamp of the most recent snapshot for this job.
	/// By default, snapshots ten days older than the newest snapshot are deleted.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	long ModelSnapshotRetentionDays { get; set; }

	/// <summary>
	/// <para>
	/// Advanced configuration option.
	/// The period over which adjustments to the score are applied, as new data is seen.
	/// The default value is the longer of 30 days or 100 <c>bucket_spans</c>.
	/// </para>
	/// </summary>
	public long? RenormalizationWindowDays { get; set; }

	/// <summary>
	/// <para>
	/// A text string that affects the name of the machine learning results index.
	/// The default value is <c>shared</c>, which generates an index named <c>.ml-anomalies-shared</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ResultsIndexName { get; set; }

	/// <summary>
	/// <para>
	/// Advanced configuration option.
	/// The period of time (in days) that results are retained.
	/// Age is calculated relative to the timestamp of the latest bucket result.
	/// If this property has a non-null value, once per day at 00:30 (server time), results that are the specified number of days older than the latest bucket result are deleted from Elasticsearch.
	/// The default value is null, which means all results are retained.
	/// Annotations generated by the system also count as results for retention purposes; they are deleted after the same number of days as results.
	/// Annotations added by users are retained forever.
	/// </para>
	/// </summary>
	public long? ResultsRetentionDays { get; set; }
}