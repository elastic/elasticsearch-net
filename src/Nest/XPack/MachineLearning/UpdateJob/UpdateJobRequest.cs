using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// Update a machine learning Job
	/// </summary>
	public partial interface IUpdateJobRequest
	{
		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// Requires a restart.
		/// </summary>
		[JsonProperty("analysis_limits")]
		IAnalysisMemoryLimit AnalysisLimits { get; set; }

		/// <summary>
		/// The time between each periodic persistence of the model. The default value is a randomized value between 3 to 4 hours,
		/// which avoids all jobs persisting at exactly the same time. The smallest allowed value is 1 hour.
		/// Requires a restart.
		/// </summary>
		[JsonProperty("background_persist_interval")]
		Time BackgroundPersistInterval { get; set; }

		/// <summary>
		/// Contains custom meta data about the job.
		/// </summary>
		[JsonProperty("custom_settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		Dictionary<string, object> CustomSettings { get; set; }

		/// <summary>
		/// An optional description of the job
		/// </summary>
		[JsonProperty("description")]
		string Description { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities
		/// </summary>
		[JsonProperty("model_plot_config")]
		IModelPlotConfigEnabled ModelPlotConfig { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 1 day.
		/// </summary>
		[JsonProperty("model_snapshot_retention_days")]
		long? ModelSnapshotRetentionDays { get; set; }

		/// <summary>
		/// The period over which adjustments to the score are applied, as new data is seen.
		/// </summary>
		[JsonProperty("renormalization_window_days")]
		long? RenormalizationWindowDays { get; set; }

		/// <summary>
		/// The number of days for which job results are retained. Once per day at 00:30 (server time),
		/// results older than this period are deleted from Elasticsearch. The default value is null,
		/// which means results are retained.
		/// </summary>
		[JsonProperty("results_retention_days")]
		long? ResultsRetentionDays { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateJobRequest
	{
		public IAnalysisMemoryLimit AnalysisLimits { get; set; }
		public Time BackgroundPersistInterval { get; set; }
		public Dictionary<string, object> CustomSettings { get; set; }
		public string Description { get; set; }
		public IModelPlotConfigEnabled ModelPlotConfig { get; set; }
		public long? ModelSnapshotRetentionDays { get; set; }
		public long? RenormalizationWindowDays { get; set; }
		public long? ResultsRetentionDays { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlUpdateJob")]
	public partial class UpdateJobDescriptor<T> where T : class
	{
		IAnalysisMemoryLimit IUpdateJobRequest.AnalysisLimits { get; set; }
		Time IUpdateJobRequest.BackgroundPersistInterval { get; set; }
		Dictionary<string, object> IUpdateJobRequest.CustomSettings { get; set; }
		string IUpdateJobRequest.Description { get; set; }
		IModelPlotConfigEnabled IUpdateJobRequest.ModelPlotConfig { get; set; }
		long? IUpdateJobRequest.ModelSnapshotRetentionDays { get; set; }
		long? IUpdateJobRequest.RenormalizationWindowDays { get; set; }
		long? IUpdateJobRequest.ResultsRetentionDays { get; set; }

		/// <inheritdoc />
		public UpdateJobDescriptor<T> AnalysisLimits(Func<AnalysisMemoryLimitDescriptor, IAnalysisMemoryLimit> selector) =>
			Assign(a => a.AnalysisLimits = selector?.Invoke(new AnalysisMemoryLimitDescriptor()));

		/// <inheritdoc />
		public UpdateJobDescriptor<T> BackgroundPersistInterval(Time backgroundPersistInterval) => Assign(a => a.BackgroundPersistInterval = backgroundPersistInterval);

		/// <inheritdoc />
		public UpdateJobDescriptor<T> CustomSettings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> customSettingsDictionary) =>
			Assign(a => a.CustomSettings = customSettingsDictionary(new FluentDictionary<string, object>()));

		/// <inheritdoc />
		public UpdateJobDescriptor<T> Description(string description) => Assign(a => a.Description = description);

		/// <inheritdoc />
		public UpdateJobDescriptor<T> ModelPlot(Func<ModelPlotConfigEnabledDescriptor<T>, IModelPlotConfigEnabled> selector) =>
			Assign(a => a.ModelPlotConfig = selector?.Invoke(new ModelPlotConfigEnabledDescriptor<T>()));

		/// <inheritdoc />
		public UpdateJobDescriptor<T> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays) => Assign(a => a.ModelSnapshotRetentionDays = modelSnapshotRetentionDays);

		/// <inheritdoc />
		public UpdateJobDescriptor<T> RenormalizationWindowDays(long? renormalizationWindowDays) => Assign(a => a.RenormalizationWindowDays = renormalizationWindowDays);

		/// <inheritdoc />
		public UpdateJobDescriptor<T> ResultsRetentionDays(long? resultsRetentionDays) => Assign(a => a.ResultsRetentionDays = resultsRetentionDays);
	}
}
