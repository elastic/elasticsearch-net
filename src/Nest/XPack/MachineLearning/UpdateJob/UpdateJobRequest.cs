using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Update a machine learning Job
	/// </summary>
	[MapsApi("ml.update_job.json")]
	public partial interface IUpdateJobRequest
	{
		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// Requires a restart.
		/// </summary>
		[DataMember(Name ="analysis_limits")]
		IAnalysisMemoryLimit AnalysisLimits { get; set; }

		/// <summary>
		/// The time between each periodic persistence of the model. The default value is a randomized value between 3 to 4 hours,
		/// which avoids all jobs persisting at exactly the same time. The smallest allowed value is 1 hour.
		/// Requires a restart.
		/// </summary>
		[DataMember(Name ="background_persist_interval")]
		Time BackgroundPersistInterval { get; set; }

		/// <summary>
		/// Contains custom meta data about the job.
		/// </summary>
		[DataMember(Name ="custom_settings")]
		[JsonFormatter(typeof(VerbatimDictionaryKeysFormatter<string, object>))]
		Dictionary<string, object> CustomSettings { get; set; }

		/// <summary>
		/// An optional description of the job
		/// </summary>
		[DataMember(Name ="description")]
		string Description { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities
		/// </summary>
		[DataMember(Name ="model_plot_config")]
		IModelPlotConfigEnabled ModelPlotConfig { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 1 day.
		/// </summary>
		[DataMember(Name ="model_snapshot_retention_days")]
		long? ModelSnapshotRetentionDays { get; set; }

		/// <summary>
		/// The period over which adjustments to the score are applied, as new data is seen.
		/// </summary>
		[DataMember(Name ="renormalization_window_days")]
		long? RenormalizationWindowDays { get; set; }

		/// <summary>
		/// The number of days for which job results are retained. Once per day at 00:30 (server time),
		/// results older than this period are deleted from Elasticsearch. The default value is null,
		/// which means results are retained.
		/// </summary>
		[DataMember(Name ="results_retention_days")]
		long? ResultsRetentionDays { get; set; }

		/// <summary>
		/// Advanced configuration option. Whether this job should be allowed to open when there is insufficient machine learning
		/// node capacity for it to be immediately assigned to a node. The default is false, which means that the
		/// machine learning open job will return an error if a machine learning node with capacity to run the job cannot immediately be found.
		/// (However, this is also subject to the cluster-wide xpack.ml.max_lazy_ml_nodes setting.)
		/// If this option is set to true then the machine learning open job will not return an error, and the job will wait in the opening
		/// state until sufficient machine learning node capacity is available.
		/// </summary>
		[DataMember(Name ="allow_lazy_open")]
		bool? AllowLazyOpen { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateJobRequest
	{
		/// <inheritdoc />
		public IAnalysisMemoryLimit AnalysisLimits { get; set; }

		/// <inheritdoc />
		public Time BackgroundPersistInterval { get; set; }

		/// <inheritdoc />
		public Dictionary<string, object> CustomSettings { get; set; }

		/// <inheritdoc />
		public string Description { get; set; }

		/// <inheritdoc />
		public IModelPlotConfigEnabled ModelPlotConfig { get; set; }

		/// <inheritdoc />
		public long? ModelSnapshotRetentionDays { get; set; }

		/// <inheritdoc />
		public long? RenormalizationWindowDays { get; set; }

		/// <inheritdoc />
		public long? ResultsRetentionDays { get; set; }

		/// <inheritdoc />
		public bool? AllowLazyOpen { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateJobDescriptor<TDocument> where TDocument : class
	{
		IAnalysisMemoryLimit IUpdateJobRequest.AnalysisLimits { get; set; }
		Time IUpdateJobRequest.BackgroundPersistInterval { get; set; }
		Dictionary<string, object> IUpdateJobRequest.CustomSettings { get; set; }
		string IUpdateJobRequest.Description { get; set; }
		IModelPlotConfigEnabled IUpdateJobRequest.ModelPlotConfig { get; set; }
		long? IUpdateJobRequest.ModelSnapshotRetentionDays { get; set; }
		long? IUpdateJobRequest.RenormalizationWindowDays { get; set; }
		long? IUpdateJobRequest.ResultsRetentionDays { get; set; }

		bool? IUpdateJobRequest.AllowLazyOpen { get; set; }

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> AnalysisLimits(Func<AnalysisMemoryLimitDescriptor, IAnalysisMemoryLimit> selector) =>
			Assign(selector, (a, v) => a.AnalysisLimits = v?.Invoke(new AnalysisMemoryLimitDescriptor()));

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> BackgroundPersistInterval(Time backgroundPersistInterval) =>
			Assign(backgroundPersistInterval, (a, v) => a.BackgroundPersistInterval = v);

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> CustomSettings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> customSettingsDictionary
		) =>
			Assign(customSettingsDictionary(new FluentDictionary<string, object>()), (a, v) => a.CustomSettings = v);

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> ModelPlot(Func<ModelPlotConfigEnabledDescriptor<TDocument>, IModelPlotConfigEnabled> selector) =>
			Assign(selector, (a, v) => a.ModelPlotConfig = v?.Invoke(new ModelPlotConfigEnabledDescriptor<TDocument>()));

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays) =>
			Assign(modelSnapshotRetentionDays, (a, v) => a.ModelSnapshotRetentionDays = v);

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> RenormalizationWindowDays(long? renormalizationWindowDays) =>
			Assign(renormalizationWindowDays, (a, v) => a.RenormalizationWindowDays = v);

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> ResultsRetentionDays(long? resultsRetentionDays) => Assign(resultsRetentionDays, (a, v) => a.ResultsRetentionDays = v);

		/// <inheritdoc />
		public UpdateJobDescriptor<TDocument> AllowLazyOpen(bool? allowLazyOpen = true) =>
			Assign(allowLazyOpen, (a, v) => a.AllowLazyOpen = v);
	}
}
