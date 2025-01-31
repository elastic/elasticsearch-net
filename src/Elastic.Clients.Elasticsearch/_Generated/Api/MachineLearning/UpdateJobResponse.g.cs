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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class UpdateJobResponseConverter : System.Text.Json.Serialization.JsonConverter<UpdateJobResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowLazyOpen = System.Text.Json.JsonEncodedText.Encode("allow_lazy_open");
	private static readonly System.Text.Json.JsonEncodedText PropAnalysisConfig = System.Text.Json.JsonEncodedText.Encode("analysis_config");
	private static readonly System.Text.Json.JsonEncodedText PropAnalysisLimits = System.Text.Json.JsonEncodedText.Encode("analysis_limits");
	private static readonly System.Text.Json.JsonEncodedText PropBackgroundPersistInterval = System.Text.Json.JsonEncodedText.Encode("background_persist_interval");
	private static readonly System.Text.Json.JsonEncodedText PropCreateTime = System.Text.Json.JsonEncodedText.Encode("create_time");
	private static readonly System.Text.Json.JsonEncodedText PropCustomSettings = System.Text.Json.JsonEncodedText.Encode("custom_settings");
	private static readonly System.Text.Json.JsonEncodedText PropDailyModelSnapshotRetentionAfterDays = System.Text.Json.JsonEncodedText.Encode("daily_model_snapshot_retention_after_days");
	private static readonly System.Text.Json.JsonEncodedText PropDataDescription = System.Text.Json.JsonEncodedText.Encode("data_description");
	private static readonly System.Text.Json.JsonEncodedText PropDatafeedConfig = System.Text.Json.JsonEncodedText.Encode("datafeed_config");
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

	public override UpdateJobResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAllowLazyOpen = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigRead> propAnalysisConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits> propAnalysisLimits = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propBackgroundPersistInterval = default;
		LocalJsonValue<long> propCreateTime = default;
		LocalJsonValue<IReadOnlyDictionary<string, string>?> propCustomSettings = default;
		LocalJsonValue<long> propDailyModelSnapshotRetentionAfterDays = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataDescription> propDataDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Datafeed?> propDatafeedConfig = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<long?> propFinishedTime = default;
		LocalJsonValue<IReadOnlyCollection<string>?> propGroups = default;
		LocalJsonValue<string> propJobId = default;
		LocalJsonValue<string> propJobType = default;
		LocalJsonValue<string> propJobVersion = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig?> propModelPlotConfig = default;
		LocalJsonValue<string?> propModelSnapshotId = default;
		LocalJsonValue<long> propModelSnapshotRetentionDays = default;
		LocalJsonValue<long?> propRenormalizationWindowDays = default;
		LocalJsonValue<string> propResultsIndexName = default;
		LocalJsonValue<long?> propResultsRetentionDays = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowLazyOpen.TryRead(ref reader, options, PropAllowLazyOpen))
			{
				continue;
			}

			if (propAnalysisConfig.TryRead(ref reader, options, PropAnalysisConfig))
			{
				continue;
			}

			if (propAnalysisLimits.TryRead(ref reader, options, PropAnalysisLimits))
			{
				continue;
			}

			if (propBackgroundPersistInterval.TryRead(ref reader, options, PropBackgroundPersistInterval))
			{
				continue;
			}

			if (propCreateTime.TryRead(ref reader, options, PropCreateTime))
			{
				continue;
			}

			if (propCustomSettings.TryRead(ref reader, options, PropCustomSettings))
			{
				continue;
			}

			if (propDailyModelSnapshotRetentionAfterDays.TryRead(ref reader, options, PropDailyModelSnapshotRetentionAfterDays))
			{
				continue;
			}

			if (propDataDescription.TryRead(ref reader, options, PropDataDescription))
			{
				continue;
			}

			if (propDatafeedConfig.TryRead(ref reader, options, PropDatafeedConfig))
			{
				continue;
			}

			if (propDescription.TryRead(ref reader, options, PropDescription))
			{
				continue;
			}

			if (propFinishedTime.TryRead(ref reader, options, PropFinishedTime))
			{
				continue;
			}

			if (propGroups.TryRead(ref reader, options, PropGroups))
			{
				continue;
			}

			if (propJobId.TryRead(ref reader, options, PropJobId))
			{
				continue;
			}

			if (propJobType.TryRead(ref reader, options, PropJobType))
			{
				continue;
			}

			if (propJobVersion.TryRead(ref reader, options, PropJobVersion))
			{
				continue;
			}

			if (propModelPlotConfig.TryRead(ref reader, options, PropModelPlotConfig))
			{
				continue;
			}

			if (propModelSnapshotId.TryRead(ref reader, options, PropModelSnapshotId))
			{
				continue;
			}

			if (propModelSnapshotRetentionDays.TryRead(ref reader, options, PropModelSnapshotRetentionDays))
			{
				continue;
			}

			if (propRenormalizationWindowDays.TryRead(ref reader, options, PropRenormalizationWindowDays))
			{
				continue;
			}

			if (propResultsIndexName.TryRead(ref reader, options, PropResultsIndexName))
			{
				continue;
			}

			if (propResultsRetentionDays.TryRead(ref reader, options, PropResultsRetentionDays))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new UpdateJobResponse
		{
			AllowLazyOpen = propAllowLazyOpen.Value
,
			AnalysisConfig = propAnalysisConfig.Value
,
			AnalysisLimits = propAnalysisLimits.Value
,
			BackgroundPersistInterval = propBackgroundPersistInterval.Value
,
			CreateTime = propCreateTime.Value
,
			CustomSettings = propCustomSettings.Value
,
			DailyModelSnapshotRetentionAfterDays = propDailyModelSnapshotRetentionAfterDays.Value
,
			DataDescription = propDataDescription.Value
,
			DatafeedConfig = propDatafeedConfig.Value
,
			Description = propDescription.Value
,
			FinishedTime = propFinishedTime.Value
,
			Groups = propGroups.Value
,
			JobId = propJobId.Value
,
			JobType = propJobType.Value
,
			JobVersion = propJobVersion.Value
,
			ModelPlotConfig = propModelPlotConfig.Value
,
			ModelSnapshotId = propModelSnapshotId.Value
,
			ModelSnapshotRetentionDays = propModelSnapshotRetentionDays.Value
,
			RenormalizationWindowDays = propRenormalizationWindowDays.Value
,
			ResultsIndexName = propResultsIndexName.Value
,
			ResultsRetentionDays = propResultsRetentionDays.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, UpdateJobResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowLazyOpen, value.AllowLazyOpen);
		writer.WriteProperty(options, PropAnalysisConfig, value.AnalysisConfig);
		writer.WriteProperty(options, PropAnalysisLimits, value.AnalysisLimits);
		writer.WriteProperty(options, PropBackgroundPersistInterval, value.BackgroundPersistInterval);
		writer.WriteProperty(options, PropCreateTime, value.CreateTime);
		writer.WriteProperty(options, PropCustomSettings, value.CustomSettings);
		writer.WriteProperty(options, PropDailyModelSnapshotRetentionAfterDays, value.DailyModelSnapshotRetentionAfterDays);
		writer.WriteProperty(options, PropDataDescription, value.DataDescription);
		writer.WriteProperty(options, PropDatafeedConfig, value.DatafeedConfig);
		writer.WriteProperty(options, PropDescription, value.Description);
		writer.WriteProperty(options, PropFinishedTime, value.FinishedTime);
		writer.WriteProperty(options, PropGroups, value.Groups);
		writer.WriteProperty(options, PropJobId, value.JobId);
		writer.WriteProperty(options, PropJobType, value.JobType);
		writer.WriteProperty(options, PropJobVersion, value.JobVersion);
		writer.WriteProperty(options, PropModelPlotConfig, value.ModelPlotConfig);
		writer.WriteProperty(options, PropModelSnapshotId, value.ModelSnapshotId);
		writer.WriteProperty(options, PropModelSnapshotRetentionDays, value.ModelSnapshotRetentionDays);
		writer.WriteProperty(options, PropRenormalizationWindowDays, value.RenormalizationWindowDays);
		writer.WriteProperty(options, PropResultsIndexName, value.ResultsIndexName);
		writer.WriteProperty(options, PropResultsRetentionDays, value.ResultsRetentionDays);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(UpdateJobResponseConverter))]
public sealed partial class UpdateJobResponse : ElasticsearchResponse
{
	public bool AllowLazyOpen { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigRead AnalysisConfig { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits AnalysisLimits { get; init; }
	public Elastic.Clients.Elasticsearch.Duration? BackgroundPersistInterval { get; init; }
	public long CreateTime { get; init; }
	public IReadOnlyDictionary<string, string>? CustomSettings { get; init; }
	public long DailyModelSnapshotRetentionAfterDays { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.DataDescription DataDescription { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.Datafeed? DatafeedConfig { get; init; }
	public string? Description { get; init; }
	public long? FinishedTime { get; init; }
	public IReadOnlyCollection<string>? Groups { get; init; }
	public string JobId { get; init; }
	public string JobType { get; init; }
	public string JobVersion { get; init; }
	public Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig? ModelPlotConfig { get; init; }
	public string? ModelSnapshotId { get; init; }
	public long ModelSnapshotRetentionDays { get; init; }
	public long? RenormalizationWindowDays { get; init; }
	public string ResultsIndexName { get; init; }
	public long? ResultsRetentionDays { get; init; }
}