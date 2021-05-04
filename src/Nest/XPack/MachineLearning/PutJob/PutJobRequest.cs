// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	/// <summary>
	/// Creates a machine learning job
	/// </summary>
	[MapsApi("ml.put_job.json")]
	public partial interface IPutJobRequest
	{
		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[DataMember(Name ="analysis_config")]
		IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Defines approximate limits on the memory resource requirements for the job.
		/// </summary>
		[DataMember(Name ="analysis_limits")]
		IAnalysisLimits AnalysisLimits { get; set; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty
		/// </summary>
		[DataMember(Name ="data_description")]
		IDataDescription DataDescription { get; set; }

		/// <summary>
		/// An optional description of the job
		/// </summary>
		[DataMember(Name ="description")]
		string Description { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities
		/// </summary>
		[DataMember(Name ="model_plot")]
		IModelPlotConfig ModelPlotConfig { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 10 days in Elasticsearch 7.8.0+
		/// and 1 day in older versions.
		/// </summary>
		[DataMember(Name ="model_snapshot_retention_days")]
		long? ModelSnapshotRetentionDays { get; set; }

		/// <summary>
		/// Specifies a number of days between 0 and the value of <see cref="ModelSnapshotRetentionDays"/>.
		/// After this period of time, only the first model snapshot per day is retained for this job.
		/// Age is calculated relative to the timestamp of the newest model snapshot. For new jobs, the default
		/// value is <c>1</c>, which means that all snapshots are retained for one day. Older snapshots
		/// are thinned out such that only one per day is retained. For jobs that were
		/// created before this setting was available, the default value matches the
		/// <see cref="ModelSnapshotRetentionDays"/> value, which preserves the original behavior
		/// and no thinning out of model snapshots occurs.
		/// <para />
		/// Available in Elasticsearch 7.8.0+
		/// </summary>
		[DataMember(Name ="daily_model_snapshot_retention_after_days")]
		long? DailyModelSnapshotRetentionAfterDays { get; set; }

		/// <summary>
		/// The name of the index in which to store the machine learning results.
		/// The default value is shared, which corresponds to the index name .ml-anomalies-shared.
		/// </summary>
		[DataMember(Name ="results_index_name")]
		IndexName ResultsIndexName { get; set; }

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
	public partial class PutJobRequest
	{
		/// <inheritdoc />
		public IAnalysisConfig AnalysisConfig { get; set; }

		/// <inheritdoc />
		public IAnalysisLimits AnalysisLimits { get; set; }

		/// <inheritdoc />
		public IDataDescription DataDescription { get; set; }

		/// <inheritdoc />
		public string Description { get; set; }

		/// <inheritdoc />
		public IModelPlotConfig ModelPlotConfig { get; set; }

		/// <inheritdoc />
		public long? ModelSnapshotRetentionDays { get; set; }

		/// <inheritdoc />
		public long? DailyModelSnapshotRetentionAfterDays { get; set; }

		/// <inheritdoc />
		public IndexName ResultsIndexName { get; set; }

		/// <inheritdoc />
		public bool? AllowLazyOpen { get; set; }
	}

	/// <inheritdoc />
	public partial class PutJobDescriptor<TDocument> where TDocument : class
	{
		IAnalysisConfig IPutJobRequest.AnalysisConfig { get; set; }
		IAnalysisLimits IPutJobRequest.AnalysisLimits { get; set; }
		IDataDescription IPutJobRequest.DataDescription { get; set; }
		string IPutJobRequest.Description { get; set; }
		IModelPlotConfig IPutJobRequest.ModelPlotConfig { get; set; }
		long? IPutJobRequest.ModelSnapshotRetentionDays { get; set; }
		long? IPutJobRequest.DailyModelSnapshotRetentionAfterDays { get; set; }
		IndexName IPutJobRequest.ResultsIndexName { get; set; }
		bool? IPutJobRequest.AllowLazyOpen { get; set; }

		/// <inheritdoc cref="IPutJobRequest.AnalysisConfig"/>
		public PutJobDescriptor<TDocument> AnalysisConfig(Func<AnalysisConfigDescriptor<TDocument>, IAnalysisConfig> selector) =>
			Assign(selector, (a, v) => a.AnalysisConfig = v?.Invoke(new AnalysisConfigDescriptor<TDocument>()));

		/// <inheritdoc cref="IPutJobRequest.AnalysisLimits"/>
		public PutJobDescriptor<TDocument> AnalysisLimits(Func<AnalysisLimitsDescriptor, IAnalysisLimits> selector) =>
			Assign(selector, (a, v) => a.AnalysisLimits = v?.Invoke(new AnalysisLimitsDescriptor()));

		/// <inheritdoc cref="IPutJobRequest.DataDescription"/>
		public PutJobDescriptor<TDocument> DataDescription(Func<DataDescriptionDescriptor<TDocument>, IDataDescription> selector) =>
			Assign(selector.InvokeOrDefault(new DataDescriptionDescriptor<TDocument>()), (a, v) => a.DataDescription = v);

		/// <inheritdoc cref="IPutJobRequest.Description"/>
		public PutJobDescriptor<TDocument> Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc cref="IPutJobRequest.ModelPlotConfig"/>
		public PutJobDescriptor<TDocument> ModelPlot(Func<ModelPlotConfigDescriptor<TDocument>, IModelPlotConfig> selector) =>
			Assign(selector, (a, v) => a.ModelPlotConfig = v?.Invoke(new ModelPlotConfigDescriptor<TDocument>()));

		/// <inheritdoc cref="IPutJobRequest.ModelSnapshotRetentionDays"/>
		public PutJobDescriptor<TDocument> ModelSnapshotRetentionDays(long? modelSnapshotRetentionDays) =>
			Assign(modelSnapshotRetentionDays, (a, v) => a.ModelSnapshotRetentionDays = v);

		/// <inheritdoc cref="IPutJobRequest.DailyModelSnapshotRetentionAfterDays"/>
		public PutJobDescriptor<TDocument> DailyModelSnapshotRetentionAfterDays(long? dailyModelSnapshotRetentionAfterDays) =>
			Assign(dailyModelSnapshotRetentionAfterDays, (a, v) => a.DailyModelSnapshotRetentionAfterDays = v);

		/// <inheritdoc cref="IPutJobRequest.ResultsIndexName"/>
		public PutJobDescriptor<TDocument> ResultsIndexName(IndexName indexName) =>
			Assign(indexName, (a, v) => a.ResultsIndexName = v);

		/// <inheritdoc cref="IPutJobRequest.ResultsIndexName"/>
		public PutJobDescriptor<TDocument> ResultsIndexName<TIndex>() =>
			Assign(typeof(TIndex), (a, v) => a.ResultsIndexName = v);

		/// <inheritdoc cref="IPutJobRequest.AllowLazyOpen"/>
		public PutJobDescriptor<TDocument> AllowLazyOpen(bool? allowLazyOpen = true) =>
			Assign(allowLazyOpen, (a, v) => a.AllowLazyOpen = v);
	}

	[StringEnum]
	public enum ExcludeFrequent
	{
		[EnumMember(Value = "all")]
		All,

		[EnumMember(Value = "none")]
		None,

		[EnumMember(Value = "by")]
		By,

		[EnumMember(Value = "over")]
		Over
	}
}
