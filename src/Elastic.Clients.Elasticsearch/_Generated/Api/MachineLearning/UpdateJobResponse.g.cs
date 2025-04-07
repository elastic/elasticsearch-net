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

internal sealed partial class UpdateJobResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.UpdateJobResponse>
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

	public override Elastic.Clients.Elasticsearch.MachineLearning.UpdateJobResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propAllowLazyOpen = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigRead> propAnalysisConfig = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits> propAnalysisLimits = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propBackgroundPersistInterval = default;
		LocalJsonValue<System.DateTimeOffset> propCreateTime = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>?> propCustomSettings = default;
		LocalJsonValue<long> propDailyModelSnapshotRetentionAfterDays = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.DataDescription> propDataDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.Datafeed?> propDatafeedConfig = default;
		LocalJsonValue<string?> propDescription = default;
		LocalJsonValue<System.DateTimeOffset?> propFinishedTime = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>?> propGroups = default;
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

			if (propCreateTime.TryReadProperty(ref reader, options, PropCreateTime, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propCustomSettings.TryReadProperty(ref reader, options, PropCustomSettings, static System.Collections.Generic.IReadOnlyDictionary<string, string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)))
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

			if (propDescription.TryReadProperty(ref reader, options, PropDescription, null))
			{
				continue;
			}

			if (propFinishedTime.TryReadProperty(ref reader, options, PropFinishedTime, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.UpdateJobResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowLazyOpen = propAllowLazyOpen.Value,
			AnalysisConfig = propAnalysisConfig.Value,
			AnalysisLimits = propAnalysisLimits.Value,
			BackgroundPersistInterval = propBackgroundPersistInterval.Value,
			CreateTime = propCreateTime.Value,
			CustomSettings = propCustomSettings.Value,
			DailyModelSnapshotRetentionAfterDays = propDailyModelSnapshotRetentionAfterDays.Value,
			DataDescription = propDataDescription.Value,
			DatafeedConfig = propDatafeedConfig.Value,
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

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.UpdateJobResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowLazyOpen, value.AllowLazyOpen, null, null);
		writer.WriteProperty(options, PropAnalysisConfig, value.AnalysisConfig, null, null);
		writer.WriteProperty(options, PropAnalysisLimits, value.AnalysisLimits, null, null);
		writer.WriteProperty(options, PropBackgroundPersistInterval, value.BackgroundPersistInterval, null, null);
		writer.WriteProperty(options, PropCreateTime, value.CreateTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropCustomSettings, value.CustomSettings, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string>? v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropDailyModelSnapshotRetentionAfterDays, value.DailyModelSnapshotRetentionAfterDays, null, null);
		writer.WriteProperty(options, PropDataDescription, value.DataDescription, null, null);
		writer.WriteProperty(options, PropDatafeedConfig, value.DatafeedConfig, null, null);
		writer.WriteProperty(options, PropDescription, value.Description, null, null);
		writer.WriteProperty(options, PropFinishedTime, value.FinishedTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
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

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.UpdateJobResponseConverter))]
public sealed partial class UpdateJobResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateJobResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UpdateJobResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
		required
#endif
		bool AllowLazyOpen { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.MachineLearning.AnalysisConfigRead AnalysisConfig { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.MachineLearning.AnalysisLimits AnalysisLimits { get; set; }
	public Elastic.Clients.Elasticsearch.Duration? BackgroundPersistInterval { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		System.DateTimeOffset CreateTime { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, string>? CustomSettings { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		long DailyModelSnapshotRetentionAfterDays { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		Elastic.Clients.Elasticsearch.MachineLearning.DataDescription DataDescription { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.Datafeed? DatafeedConfig { get; set; }
	public string? Description { get; set; }
	public System.DateTimeOffset? FinishedTime { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<string>? Groups { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string JobId { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string JobType { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string JobVersion { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.ModelPlotConfig? ModelPlotConfig { get; set; }
	public string? ModelSnapshotId { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		long ModelSnapshotRetentionDays { get; set; }
	public long? RenormalizationWindowDays { get; set; }
	public
#if NET7_0_OR_GREATER
		required
#endif
		string ResultsIndexName { get; set; }
	public long? ResultsRetentionDays { get; set; }
}