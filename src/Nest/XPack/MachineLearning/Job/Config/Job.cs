// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class Job
	{
		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[DataMember(Name = "analysis_config")]
		public IAnalysisConfig AnalysisConfig { get; set; }

		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// </summary>
		[DataMember(Name = "analysis_limits")]
		public IAnalysisLimits AnalysisLimits { get; set; }

		/// <summary>
		/// Advanced configuration option. The time between each periodic persistence of the model.
		/// The default value is a randomized value between 3 to 4 hours, which avoids all jobs persisting
		/// at exactly the same time. The smallest allowed value is 1 hour.
		/// </summary>
		/// <remarks>
		/// For very large models (several GB), persistence could take 10-20 minutes, so do not set the value too low.
		/// </remarks>
		[DataMember(Name = "background_persist_interval")]
		public Time BackgroundPersistInterval { get; set; }

		/// <summary>
		/// The time the job was created.
		/// </summary>
		[DataMember(Name = "create_time")]
		[JsonFormatter(typeof(DateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset CreateTime { get; set; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty.
		/// </summary>
		[DataMember(Name = "data_description")]
		public IDataDescription DataDescription { get; set; }

		/// <summary>
		/// An optional description of the job.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// If the job closed or failed, this is the time the job finished, otherwise it is null.
		/// </summary>
		[DataMember(Name = "finished_time")]
		[JsonFormatter(typeof(NullableDateTimeOffsetEpochMillisecondsFormatter))]
		public DateTimeOffset? FinishedTime { get; set; }

		/// <summary>
		/// The unique identifier for the job.
		/// </summary>
		[DataMember(Name = "job_id")]
		public string JobId { get; set; }

		/// <summary>
		/// The job type.
		/// </summary>
		/// <remarks>
		/// Reserved for future use.
		/// </remarks>
		[DataMember(Name = "job_type")]
		public string JobType { get; set; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities.
		/// </summary>
		[DataMember(Name = "model_plot")]
		public IModelPlotConfig ModelPlotConfig { get; set; }

		/// <summary>
		/// A numerical character string that uniquely identifies the model snapshot.
		/// </summary>
		[DataMember(Name = "model_snapshot_id")]
		public string ModelSnapshotId { get; set; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 10 days in Elasticsearch 7.8.0+
		/// and 1 day in older versions.
		/// </summary>
		[DataMember(Name = "model_snapshot_retention_days")]
		public long? ModelSnapshotRetentionDays { get; set; }

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
		[DataMember(Name = "daily_model_snapshot_retention_after_days")]
		public long? DailyModelSnapshotRetentionAfterDays { get; set; }

		/// <summary>
		/// Advanced configuration option. The period over which adjustments to the score are applied, as new data
		/// is seen. The default value is the longer of 30 days or 100 bucket spans.
		/// </summary>
		[DataMember(Name = "renormalization_window_days")]
		public long? RenormalizationWindowDays { get; set; }

		/// <summary>
		/// The name of the index in which to store the machine learning results.
		/// The default value is shared (which corresponds to the index name .ml-anomalies-shared).
		/// </summary>
		[DataMember(Name = "results_index_name")]
		public string ResultsIndexName { get; set; }

		/// <summary>
		/// Advanced configuration option. The number of days for which job results are retained.
		/// Once per day at 00:30 (server time), results older than this period are deleted from Elasticsearch.
		/// The default value is null, which means results are retained.
		/// </summary>
		[DataMember(Name = "results_retention_days")]
		public long? ResultsRetentionDays { get; set; }

		/// <summary>
		/// Advanced configuration option. Whether this job should be allowed to open when there is insufficient machine learning
		/// node capacity for it to be immediately assigned to a node. The default is false, which means that the
		/// machine learning open job will return an error if a machine learning node with capacity to run the job cannot immediately be found.
		/// (However, this is also subject to the cluster-wide xpack.ml.max_lazy_ml_nodes setting.)
		/// If this option is set to true then the machine learning open job will not return an error, and the job will wait in the opening
		/// state until sufficient machine learning node capacity is available.
		/// </summary>
		[DataMember(Name ="allow_lazy_open")]
		public bool? AllowLazyOpen { get; set; }
	}
}
