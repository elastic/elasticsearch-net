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

using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Ml
{
	public sealed class MlPutJobRequestParameters : RequestParameters<MlPutJobRequestParameters>
	{
	}

	public sealed partial class MlPutJobRequest : PlainRequestBase<MlPutJobRequestParameters>
	{
		public MlPutJobRequest(Elastic.Clients.Elasticsearch.Id job_id) : base(r => r.Required("job_id", job_id))
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutJob;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		[JsonInclude]
		[JsonPropertyName("allow_lazy_open")]
		public bool? AllowLazyOpen { get; set; }

		[JsonInclude]
		[JsonPropertyName("analysis_config")]
		public Elastic.Clients.Elasticsearch.Ml.AnalysisConfig AnalysisConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("analysis_limits")]
		public Elastic.Clients.Elasticsearch.Ml.AnalysisLimits? AnalysisLimits { get; set; }

		[JsonInclude]
		[JsonPropertyName("background_persist_interval")]
		public Elastic.Clients.Elasticsearch.Duration? BackgroundPersistInterval { get; set; }

		[JsonInclude]
		[JsonPropertyName("custom_settings")]
		public object? CustomSettings { get; set; }

		[JsonInclude]
		[JsonPropertyName("daily_model_snapshot_retention_after_days")]
		public long? DailyModelSnapshotRetentionAfterDays { get; set; }

		[JsonInclude]
		[JsonPropertyName("data_description")]
		public Elastic.Clients.Elasticsearch.Ml.DataDescription DataDescription { get; set; }

		[JsonInclude]
		[JsonPropertyName("datafeed_config")]
		public Elastic.Clients.Elasticsearch.Ml.DatafeedConfig? DatafeedConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonInclude]
		[JsonPropertyName("groups")]
		public IEnumerable<string>? Groups { get; set; }

		[JsonInclude]
		[JsonPropertyName("model_plot_config")]
		public Elastic.Clients.Elasticsearch.Ml.ModelPlotConfig? ModelPlotConfig { get; set; }

		[JsonInclude]
		[JsonPropertyName("model_snapshot_retention_days")]
		public long? ModelSnapshotRetentionDays { get; set; }

		[JsonInclude]
		[JsonPropertyName("renormalization_window_days")]
		public long? RenormalizationWindowDays { get; set; }

		[JsonInclude]
		[JsonPropertyName("results_index_name")]
		public Elastic.Clients.Elasticsearch.IndexName? ResultsIndexName { get; set; }

		[JsonInclude]
		[JsonPropertyName("results_retention_days")]
		public long? ResultsRetentionDays { get; set; }
	}

	public sealed partial class MlPutJobRequestDescriptor<TDocument> : RequestDescriptorBase<MlPutJobRequestDescriptor<TDocument>, MlPutJobRequestParameters>
	{
		internal MlPutJobRequestDescriptor(Action<MlPutJobRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
		public MlPutJobRequestDescriptor(Elastic.Clients.Elasticsearch.Id job_id) : base(r => r.Required("job_id", job_id))
		{
		}

		internal MlPutJobRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutJob;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		public MlPutJobRequestDescriptor<TDocument> JobId(Elastic.Clients.Elasticsearch.Id job_id)
		{
			RouteValues.Required("job_id", job_id);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.Ml.AnalysisConfig AnalysisConfigValue { get; set; }

		private AnalysisConfigDescriptor<TDocument> AnalysisConfigDescriptor { get; set; }

		private Action<AnalysisConfigDescriptor<TDocument>> AnalysisConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataDescription DataDescriptionValue { get; set; }

		private DataDescriptionDescriptor<TDocument> DataDescriptionDescriptor { get; set; }

		private Action<DataDescriptionDescriptor<TDocument>> DataDescriptionDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DatafeedConfig? DatafeedConfigValue { get; set; }

		private DatafeedConfigDescriptor<TDocument> DatafeedConfigDescriptor { get; set; }

		private Action<DatafeedConfigDescriptor<TDocument>> DatafeedConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.ModelPlotConfig? ModelPlotConfigValue { get; set; }

		private ModelPlotConfigDescriptor<TDocument> ModelPlotConfigDescriptor { get; set; }

		private Action<ModelPlotConfigDescriptor<TDocument>> ModelPlotConfigDescriptorAction { get; set; }

		private bool? AllowLazyOpenValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.AnalysisLimits? AnalysisLimitsValue { get; set; }

		private AnalysisLimitsDescriptor AnalysisLimitsDescriptor { get; set; }

		private Action<AnalysisLimitsDescriptor> AnalysisLimitsDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? BackgroundPersistIntervalValue { get; set; }

		private object? CustomSettingsValue { get; set; }

		private long? DailyModelSnapshotRetentionAfterDaysValue { get; set; }

		private string? DescriptionValue { get; set; }

		private IEnumerable<string>? GroupsValue { get; set; }

		private long? ModelSnapshotRetentionDaysValue { get; set; }

		private long? RenormalizationWindowDaysValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndexName? ResultsIndexNameValue { get; set; }

		private long? ResultsRetentionDaysValue { get; set; }

		public MlPutJobRequestDescriptor<TDocument> AnalysisConfig(Elastic.Clients.Elasticsearch.Ml.AnalysisConfig analysisConfig)
		{
			AnalysisConfigDescriptor = null;
			AnalysisConfigDescriptorAction = null;
			AnalysisConfigValue = analysisConfig;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> AnalysisConfig(AnalysisConfigDescriptor<TDocument> descriptor)
		{
			AnalysisConfigValue = null;
			AnalysisConfigDescriptorAction = null;
			AnalysisConfigDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> AnalysisConfig(Action<AnalysisConfigDescriptor<TDocument>> configure)
		{
			AnalysisConfigValue = null;
			AnalysisConfigDescriptor = null;
			AnalysisConfigDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DataDescription(Elastic.Clients.Elasticsearch.Ml.DataDescription dataDescription)
		{
			DataDescriptionDescriptor = null;
			DataDescriptionDescriptorAction = null;
			DataDescriptionValue = dataDescription;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DataDescription(DataDescriptionDescriptor<TDocument> descriptor)
		{
			DataDescriptionValue = null;
			DataDescriptionDescriptorAction = null;
			DataDescriptionDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DataDescription(Action<DataDescriptionDescriptor<TDocument>> configure)
		{
			DataDescriptionValue = null;
			DataDescriptionDescriptor = null;
			DataDescriptionDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DatafeedConfig(Elastic.Clients.Elasticsearch.Ml.DatafeedConfig? datafeedConfig)
		{
			DatafeedConfigDescriptor = null;
			DatafeedConfigDescriptorAction = null;
			DatafeedConfigValue = datafeedConfig;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DatafeedConfig(DatafeedConfigDescriptor<TDocument> descriptor)
		{
			DatafeedConfigValue = null;
			DatafeedConfigDescriptorAction = null;
			DatafeedConfigDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DatafeedConfig(Action<DatafeedConfigDescriptor<TDocument>> configure)
		{
			DatafeedConfigValue = null;
			DatafeedConfigDescriptor = null;
			DatafeedConfigDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> ModelPlotConfig(Elastic.Clients.Elasticsearch.Ml.ModelPlotConfig? modelPlotConfig)
		{
			ModelPlotConfigDescriptor = null;
			ModelPlotConfigDescriptorAction = null;
			ModelPlotConfigValue = modelPlotConfig;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> ModelPlotConfig(ModelPlotConfigDescriptor<TDocument> descriptor)
		{
			ModelPlotConfigValue = null;
			ModelPlotConfigDescriptorAction = null;
			ModelPlotConfigDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> ModelPlotConfig(Action<ModelPlotConfigDescriptor<TDocument>> configure)
		{
			ModelPlotConfigValue = null;
			ModelPlotConfigDescriptor = null;
			ModelPlotConfigDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> AllowLazyOpen(bool? allowLazyOpen = true)
		{
			AllowLazyOpenValue = allowLazyOpen;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> AnalysisLimits(Elastic.Clients.Elasticsearch.Ml.AnalysisLimits? analysisLimits)
		{
			AnalysisLimitsDescriptor = null;
			AnalysisLimitsDescriptorAction = null;
			AnalysisLimitsValue = analysisLimits;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> AnalysisLimits(AnalysisLimitsDescriptor descriptor)
		{
			AnalysisLimitsValue = null;
			AnalysisLimitsDescriptorAction = null;
			AnalysisLimitsDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> AnalysisLimits(Action<AnalysisLimitsDescriptor> configure)
		{
			AnalysisLimitsValue = null;
			AnalysisLimitsDescriptor = null;
			AnalysisLimitsDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> BackgroundPersistInterval(Elastic.Clients.Elasticsearch.Duration? backgroundPersistInterval)
		{
			BackgroundPersistIntervalValue = backgroundPersistInterval;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> CustomSettings(object? customSettings)
		{
			CustomSettingsValue = customSettings;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> DailyModelSnapshotRetentionAfterDays(long? dailyModelSnapshotRetentionAfterDays)
		{
			DailyModelSnapshotRetentionAfterDaysValue = dailyModelSnapshotRetentionAfterDays;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> Description(string? description)
		{
			DescriptionValue = description;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> Groups(IEnumerable<string>? groups)
		{
			GroupsValue = groups;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays)
		{
			ModelSnapshotRetentionDaysValue = modelSnapshotRetentionDays;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> RenormalizationWindowDays(long? renormalizationWindowDays)
		{
			RenormalizationWindowDaysValue = renormalizationWindowDays;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> ResultsIndexName(Elastic.Clients.Elasticsearch.IndexName? resultsIndexName)
		{
			ResultsIndexNameValue = resultsIndexName;
			return Self;
		}

		public MlPutJobRequestDescriptor<TDocument> ResultsRetentionDays(long? resultsRetentionDays)
		{
			ResultsRetentionDaysValue = resultsRetentionDays;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (AnalysisConfigDescriptor is not null)
			{
				writer.WritePropertyName("analysis_config");
				JsonSerializer.Serialize(writer, AnalysisConfigDescriptor, options);
			}
			else if (AnalysisConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("analysis_config");
				JsonSerializer.Serialize(writer, new AnalysisConfigDescriptor<TDocument>(AnalysisConfigDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("analysis_config");
				JsonSerializer.Serialize(writer, AnalysisConfigValue, options);
			}

			if (DataDescriptionDescriptor is not null)
			{
				writer.WritePropertyName("data_description");
				JsonSerializer.Serialize(writer, DataDescriptionDescriptor, options);
			}
			else if (DataDescriptionDescriptorAction is not null)
			{
				writer.WritePropertyName("data_description");
				JsonSerializer.Serialize(writer, new DataDescriptionDescriptor<TDocument>(DataDescriptionDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("data_description");
				JsonSerializer.Serialize(writer, DataDescriptionValue, options);
			}

			if (DatafeedConfigDescriptor is not null)
			{
				writer.WritePropertyName("datafeed_config");
				JsonSerializer.Serialize(writer, DatafeedConfigDescriptor, options);
			}
			else if (DatafeedConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("datafeed_config");
				JsonSerializer.Serialize(writer, new DatafeedConfigDescriptor<TDocument>(DatafeedConfigDescriptorAction), options);
			}
			else if (DatafeedConfigValue is not null)
			{
				writer.WritePropertyName("datafeed_config");
				JsonSerializer.Serialize(writer, DatafeedConfigValue, options);
			}

			if (ModelPlotConfigDescriptor is not null)
			{
				writer.WritePropertyName("model_plot_config");
				JsonSerializer.Serialize(writer, ModelPlotConfigDescriptor, options);
			}
			else if (ModelPlotConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("model_plot_config");
				JsonSerializer.Serialize(writer, new ModelPlotConfigDescriptor<TDocument>(ModelPlotConfigDescriptorAction), options);
			}
			else if (ModelPlotConfigValue is not null)
			{
				writer.WritePropertyName("model_plot_config");
				JsonSerializer.Serialize(writer, ModelPlotConfigValue, options);
			}

			if (AllowLazyOpenValue.HasValue)
			{
				writer.WritePropertyName("allow_lazy_open");
				writer.WriteBooleanValue(AllowLazyOpenValue.Value);
			}

			if (AnalysisLimitsDescriptor is not null)
			{
				writer.WritePropertyName("analysis_limits");
				JsonSerializer.Serialize(writer, AnalysisLimitsDescriptor, options);
			}
			else if (AnalysisLimitsDescriptorAction is not null)
			{
				writer.WritePropertyName("analysis_limits");
				JsonSerializer.Serialize(writer, new AnalysisLimitsDescriptor(AnalysisLimitsDescriptorAction), options);
			}
			else if (AnalysisLimitsValue is not null)
			{
				writer.WritePropertyName("analysis_limits");
				JsonSerializer.Serialize(writer, AnalysisLimitsValue, options);
			}

			if (BackgroundPersistIntervalValue is not null)
			{
				writer.WritePropertyName("background_persist_interval");
				JsonSerializer.Serialize(writer, BackgroundPersistIntervalValue, options);
			}

			if (CustomSettingsValue is not null)
			{
				writer.WritePropertyName("custom_settings");
				JsonSerializer.Serialize(writer, CustomSettingsValue, options);
			}

			if (DailyModelSnapshotRetentionAfterDaysValue.HasValue)
			{
				writer.WritePropertyName("daily_model_snapshot_retention_after_days");
				writer.WriteNumberValue(DailyModelSnapshotRetentionAfterDaysValue.Value);
			}

			if (!string.IsNullOrEmpty(DescriptionValue))
			{
				writer.WritePropertyName("description");
				writer.WriteStringValue(DescriptionValue);
			}

			if (GroupsValue is not null)
			{
				writer.WritePropertyName("groups");
				JsonSerializer.Serialize(writer, GroupsValue, options);
			}

			if (ModelSnapshotRetentionDaysValue.HasValue)
			{
				writer.WritePropertyName("model_snapshot_retention_days");
				writer.WriteNumberValue(ModelSnapshotRetentionDaysValue.Value);
			}

			if (RenormalizationWindowDaysValue.HasValue)
			{
				writer.WritePropertyName("renormalization_window_days");
				writer.WriteNumberValue(RenormalizationWindowDaysValue.Value);
			}

			if (ResultsIndexNameValue is not null)
			{
				writer.WritePropertyName("results_index_name");
				JsonSerializer.Serialize(writer, ResultsIndexNameValue, options);
			}

			if (ResultsRetentionDaysValue.HasValue)
			{
				writer.WritePropertyName("results_retention_days");
				writer.WriteNumberValue(ResultsRetentionDaysValue.Value);
			}

			writer.WriteEndObject();
		}
	}

	public sealed partial class MlPutJobRequestDescriptor : RequestDescriptorBase<MlPutJobRequestDescriptor, MlPutJobRequestParameters>
	{
		internal MlPutJobRequestDescriptor(Action<MlPutJobRequestDescriptor> configure) => configure.Invoke(this);
		public MlPutJobRequestDescriptor(Elastic.Clients.Elasticsearch.Id job_id) : base(r => r.Required("job_id", job_id))
		{
		}

		internal MlPutJobRequestDescriptor()
		{
		}

		internal override ApiUrls ApiUrls => ApiUrlsLookups.MachineLearningPutJob;
		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override bool SupportsBody => true;
		public MlPutJobRequestDescriptor JobId(Elastic.Clients.Elasticsearch.Id job_id)
		{
			RouteValues.Required("job_id", job_id);
			return Self;
		}

		private Elastic.Clients.Elasticsearch.Ml.AnalysisConfig AnalysisConfigValue { get; set; }

		private AnalysisConfigDescriptor AnalysisConfigDescriptor { get; set; }

		private Action<AnalysisConfigDescriptor> AnalysisConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DataDescription DataDescriptionValue { get; set; }

		private DataDescriptionDescriptor DataDescriptionDescriptor { get; set; }

		private Action<DataDescriptionDescriptor> DataDescriptionDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.DatafeedConfig? DatafeedConfigValue { get; set; }

		private DatafeedConfigDescriptor DatafeedConfigDescriptor { get; set; }

		private Action<DatafeedConfigDescriptor> DatafeedConfigDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.ModelPlotConfig? ModelPlotConfigValue { get; set; }

		private ModelPlotConfigDescriptor ModelPlotConfigDescriptor { get; set; }

		private Action<ModelPlotConfigDescriptor> ModelPlotConfigDescriptorAction { get; set; }

		private bool? AllowLazyOpenValue { get; set; }

		private Elastic.Clients.Elasticsearch.Ml.AnalysisLimits? AnalysisLimitsValue { get; set; }

		private AnalysisLimitsDescriptor AnalysisLimitsDescriptor { get; set; }

		private Action<AnalysisLimitsDescriptor> AnalysisLimitsDescriptorAction { get; set; }

		private Elastic.Clients.Elasticsearch.Duration? BackgroundPersistIntervalValue { get; set; }

		private object? CustomSettingsValue { get; set; }

		private long? DailyModelSnapshotRetentionAfterDaysValue { get; set; }

		private string? DescriptionValue { get; set; }

		private IEnumerable<string>? GroupsValue { get; set; }

		private long? ModelSnapshotRetentionDaysValue { get; set; }

		private long? RenormalizationWindowDaysValue { get; set; }

		private Elastic.Clients.Elasticsearch.IndexName? ResultsIndexNameValue { get; set; }

		private long? ResultsRetentionDaysValue { get; set; }

		public MlPutJobRequestDescriptor AnalysisConfig(Elastic.Clients.Elasticsearch.Ml.AnalysisConfig analysisConfig)
		{
			AnalysisConfigDescriptor = null;
			AnalysisConfigDescriptorAction = null;
			AnalysisConfigValue = analysisConfig;
			return Self;
		}

		public MlPutJobRequestDescriptor AnalysisConfig(AnalysisConfigDescriptor descriptor)
		{
			AnalysisConfigValue = null;
			AnalysisConfigDescriptorAction = null;
			AnalysisConfigDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor AnalysisConfig(Action<AnalysisConfigDescriptor> configure)
		{
			AnalysisConfigValue = null;
			AnalysisConfigDescriptor = null;
			AnalysisConfigDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor DataDescription(Elastic.Clients.Elasticsearch.Ml.DataDescription dataDescription)
		{
			DataDescriptionDescriptor = null;
			DataDescriptionDescriptorAction = null;
			DataDescriptionValue = dataDescription;
			return Self;
		}

		public MlPutJobRequestDescriptor DataDescription(DataDescriptionDescriptor descriptor)
		{
			DataDescriptionValue = null;
			DataDescriptionDescriptorAction = null;
			DataDescriptionDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor DataDescription(Action<DataDescriptionDescriptor> configure)
		{
			DataDescriptionValue = null;
			DataDescriptionDescriptor = null;
			DataDescriptionDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor DatafeedConfig(Elastic.Clients.Elasticsearch.Ml.DatafeedConfig? datafeedConfig)
		{
			DatafeedConfigDescriptor = null;
			DatafeedConfigDescriptorAction = null;
			DatafeedConfigValue = datafeedConfig;
			return Self;
		}

		public MlPutJobRequestDescriptor DatafeedConfig(DatafeedConfigDescriptor descriptor)
		{
			DatafeedConfigValue = null;
			DatafeedConfigDescriptorAction = null;
			DatafeedConfigDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor DatafeedConfig(Action<DatafeedConfigDescriptor> configure)
		{
			DatafeedConfigValue = null;
			DatafeedConfigDescriptor = null;
			DatafeedConfigDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor ModelPlotConfig(Elastic.Clients.Elasticsearch.Ml.ModelPlotConfig? modelPlotConfig)
		{
			ModelPlotConfigDescriptor = null;
			ModelPlotConfigDescriptorAction = null;
			ModelPlotConfigValue = modelPlotConfig;
			return Self;
		}

		public MlPutJobRequestDescriptor ModelPlotConfig(ModelPlotConfigDescriptor descriptor)
		{
			ModelPlotConfigValue = null;
			ModelPlotConfigDescriptorAction = null;
			ModelPlotConfigDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor ModelPlotConfig(Action<ModelPlotConfigDescriptor> configure)
		{
			ModelPlotConfigValue = null;
			ModelPlotConfigDescriptor = null;
			ModelPlotConfigDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor AllowLazyOpen(bool? allowLazyOpen = true)
		{
			AllowLazyOpenValue = allowLazyOpen;
			return Self;
		}

		public MlPutJobRequestDescriptor AnalysisLimits(Elastic.Clients.Elasticsearch.Ml.AnalysisLimits? analysisLimits)
		{
			AnalysisLimitsDescriptor = null;
			AnalysisLimitsDescriptorAction = null;
			AnalysisLimitsValue = analysisLimits;
			return Self;
		}

		public MlPutJobRequestDescriptor AnalysisLimits(AnalysisLimitsDescriptor descriptor)
		{
			AnalysisLimitsValue = null;
			AnalysisLimitsDescriptorAction = null;
			AnalysisLimitsDescriptor = descriptor;
			return Self;
		}

		public MlPutJobRequestDescriptor AnalysisLimits(Action<AnalysisLimitsDescriptor> configure)
		{
			AnalysisLimitsValue = null;
			AnalysisLimitsDescriptor = null;
			AnalysisLimitsDescriptorAction = configure;
			return Self;
		}

		public MlPutJobRequestDescriptor BackgroundPersistInterval(Elastic.Clients.Elasticsearch.Duration? backgroundPersistInterval)
		{
			BackgroundPersistIntervalValue = backgroundPersistInterval;
			return Self;
		}

		public MlPutJobRequestDescriptor CustomSettings(object? customSettings)
		{
			CustomSettingsValue = customSettings;
			return Self;
		}

		public MlPutJobRequestDescriptor DailyModelSnapshotRetentionAfterDays(long? dailyModelSnapshotRetentionAfterDays)
		{
			DailyModelSnapshotRetentionAfterDaysValue = dailyModelSnapshotRetentionAfterDays;
			return Self;
		}

		public MlPutJobRequestDescriptor Description(string? description)
		{
			DescriptionValue = description;
			return Self;
		}

		public MlPutJobRequestDescriptor Groups(IEnumerable<string>? groups)
		{
			GroupsValue = groups;
			return Self;
		}

		public MlPutJobRequestDescriptor ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays)
		{
			ModelSnapshotRetentionDaysValue = modelSnapshotRetentionDays;
			return Self;
		}

		public MlPutJobRequestDescriptor RenormalizationWindowDays(long? renormalizationWindowDays)
		{
			RenormalizationWindowDaysValue = renormalizationWindowDays;
			return Self;
		}

		public MlPutJobRequestDescriptor ResultsIndexName(Elastic.Clients.Elasticsearch.IndexName? resultsIndexName)
		{
			ResultsIndexNameValue = resultsIndexName;
			return Self;
		}

		public MlPutJobRequestDescriptor ResultsRetentionDays(long? resultsRetentionDays)
		{
			ResultsRetentionDaysValue = resultsRetentionDays;
			return Self;
		}

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();
			if (AnalysisConfigDescriptor is not null)
			{
				writer.WritePropertyName("analysis_config");
				JsonSerializer.Serialize(writer, AnalysisConfigDescriptor, options);
			}
			else if (AnalysisConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("analysis_config");
				JsonSerializer.Serialize(writer, new AnalysisConfigDescriptor(AnalysisConfigDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("analysis_config");
				JsonSerializer.Serialize(writer, AnalysisConfigValue, options);
			}

			if (DataDescriptionDescriptor is not null)
			{
				writer.WritePropertyName("data_description");
				JsonSerializer.Serialize(writer, DataDescriptionDescriptor, options);
			}
			else if (DataDescriptionDescriptorAction is not null)
			{
				writer.WritePropertyName("data_description");
				JsonSerializer.Serialize(writer, new DataDescriptionDescriptor(DataDescriptionDescriptorAction), options);
			}
			else
			{
				writer.WritePropertyName("data_description");
				JsonSerializer.Serialize(writer, DataDescriptionValue, options);
			}

			if (DatafeedConfigDescriptor is not null)
			{
				writer.WritePropertyName("datafeed_config");
				JsonSerializer.Serialize(writer, DatafeedConfigDescriptor, options);
			}
			else if (DatafeedConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("datafeed_config");
				JsonSerializer.Serialize(writer, new DatafeedConfigDescriptor(DatafeedConfigDescriptorAction), options);
			}
			else if (DatafeedConfigValue is not null)
			{
				writer.WritePropertyName("datafeed_config");
				JsonSerializer.Serialize(writer, DatafeedConfigValue, options);
			}

			if (ModelPlotConfigDescriptor is not null)
			{
				writer.WritePropertyName("model_plot_config");
				JsonSerializer.Serialize(writer, ModelPlotConfigDescriptor, options);
			}
			else if (ModelPlotConfigDescriptorAction is not null)
			{
				writer.WritePropertyName("model_plot_config");
				JsonSerializer.Serialize(writer, new ModelPlotConfigDescriptor(ModelPlotConfigDescriptorAction), options);
			}
			else if (ModelPlotConfigValue is not null)
			{
				writer.WritePropertyName("model_plot_config");
				JsonSerializer.Serialize(writer, ModelPlotConfigValue, options);
			}

			if (AllowLazyOpenValue.HasValue)
			{
				writer.WritePropertyName("allow_lazy_open");
				writer.WriteBooleanValue(AllowLazyOpenValue.Value);
			}

			if (AnalysisLimitsDescriptor is not null)
			{
				writer.WritePropertyName("analysis_limits");
				JsonSerializer.Serialize(writer, AnalysisLimitsDescriptor, options);
			}
			else if (AnalysisLimitsDescriptorAction is not null)
			{
				writer.WritePropertyName("analysis_limits");
				JsonSerializer.Serialize(writer, new AnalysisLimitsDescriptor(AnalysisLimitsDescriptorAction), options);
			}
			else if (AnalysisLimitsValue is not null)
			{
				writer.WritePropertyName("analysis_limits");
				JsonSerializer.Serialize(writer, AnalysisLimitsValue, options);
			}

			if (BackgroundPersistIntervalValue is not null)
			{
				writer.WritePropertyName("background_persist_interval");
				JsonSerializer.Serialize(writer, BackgroundPersistIntervalValue, options);
			}

			if (CustomSettingsValue is not null)
			{
				writer.WritePropertyName("custom_settings");
				JsonSerializer.Serialize(writer, CustomSettingsValue, options);
			}

			if (DailyModelSnapshotRetentionAfterDaysValue.HasValue)
			{
				writer.WritePropertyName("daily_model_snapshot_retention_after_days");
				writer.WriteNumberValue(DailyModelSnapshotRetentionAfterDaysValue.Value);
			}

			if (!string.IsNullOrEmpty(DescriptionValue))
			{
				writer.WritePropertyName("description");
				writer.WriteStringValue(DescriptionValue);
			}

			if (GroupsValue is not null)
			{
				writer.WritePropertyName("groups");
				JsonSerializer.Serialize(writer, GroupsValue, options);
			}

			if (ModelSnapshotRetentionDaysValue.HasValue)
			{
				writer.WritePropertyName("model_snapshot_retention_days");
				writer.WriteNumberValue(ModelSnapshotRetentionDaysValue.Value);
			}

			if (RenormalizationWindowDaysValue.HasValue)
			{
				writer.WritePropertyName("renormalization_window_days");
				writer.WriteNumberValue(RenormalizationWindowDaysValue.Value);
			}

			if (ResultsIndexNameValue is not null)
			{
				writer.WritePropertyName("results_index_name");
				JsonSerializer.Serialize(writer, ResultsIndexNameValue, options);
			}

			if (ResultsRetentionDaysValue.HasValue)
			{
				writer.WritePropertyName("results_retention_days");
				writer.WriteNumberValue(ResultsRetentionDaysValue.Value);
			}

			writer.WriteEndObject();
		}
	}
}