using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPutJobResponse : IResponse
	{
		/// <summary>
		/// The unique identifier for the job.
		/// </summary>
		[JsonProperty("job_id")]
		string JobId { get; }

		/// <summary>
		/// The job type.
		/// </summary>
		/// <remarks>
		/// Reserved for future use.
		/// </remarks>
		[JsonProperty("job_type")]
		string JobType { get; }

		/// <summary>
		/// An optional description of the job.
		/// </summary>
		[JsonProperty("description")]
		string Description { get; }

		/// <summary>
		/// The time the job was created.
		/// </summary>
		[JsonConverter(typeof(EpochMillisecondsDateTimeJsonConverter))]
		[JsonProperty("create_time")]
		DateTimeOffset CreateTime { get; }

		/// <summary>
		/// The analysis configuration, which specifies how to analyze the data.
		/// </summary>
		[JsonProperty("analysis_config")]
		IAnalysisConfig AnalysisConfig { get; }

		/// <summary>
		/// Optionally specifies runtime limits for the job.
		/// </summary>
		[JsonProperty("analysis_limits")]
		IAnalysisLimits AnalysisLimits { get; }

		/// <summary>
		/// Advanced configuration option. The time between each periodic persistence of the model.
		/// The default value is a randomized value between 3 to 4 hours, which avoids all jobs persisting
		/// at exactly the same time. The smallest allowed value is 1 hour.
		/// </summary>
		/// <remarks>
		/// For very large models (several GB), persistence could take 10-20 minutes,
		/// so do not set the background_persist_interval value too low.
		/// </remarks>
		[JsonProperty("background_persist_interval")]
		Time BackgroundPersistInterval { get; }

		/// <summary>
		/// Describes the format of the input data. This object is required, but it can be empty.
		/// </summary>
		[JsonProperty("data_description")]
		IDataDescription DataDescription { get; }

		/// <summary>
		/// The time in days that model snapshots are retained for the job.
		/// Older snapshots are deleted. The default value is 1 day.
		/// </summary>
		[JsonProperty("model_snapshot_retention_days")]
		long? ModelSnapshotRetentionDays { get; }

		/// <summary>
		/// A numerical character string that uniquely identifies the model snapshot.
		/// </summary>
		[JsonProperty("model_snapshot_id")]
		string ModelSnapshotId { get; }

		/// <summary>
		/// The name of the index in which to store the machine learning results.
		/// The default value is shared, which corresponds to the index name .ml-anomalies-shared.
		/// </summary>
		[JsonProperty("results_index_name")]
		string ResultsIndexName { get; }

		/// <summary>
		/// This advanced configuration option stores model information along with the results.
		/// This adds overhead to the performance of the system and is not feasible for jobs with many entities
		/// </summary>
		[JsonProperty("model_plot")]
		IModelPlotConfig ModelPlotConfig { get; }

		/// <summary>
		/// Advanced configuration option. The period over which adjustments to the score are applied, as new data
		/// is seen. The default value is the longer of 30 days or 100 bucket_spans.
		/// </summary>
		[JsonProperty("renormalization_window_days")]
		long? RenormalizationWindowDays { get;}

		/// <summary>
		/// Advanced configuration option. The number of days for which job results are retained.
		/// Once per day at 00:30 (server time), results older than this period are deleted from Elasticsearch.
		/// The default value is null, which means results are retained.
		/// </summary>
		[JsonProperty("results_retention_days")]
		long? ResultsRetentionDays { get; }
	}

	/// <inheritdoc />
	public class PutJobResponse : ResponseBase, IPutJobResponse
	{
		/// <inheritdoc />
		public string JobId { get; internal set; }

		/// <inheritdoc />
		public string JobType { get; internal set; }

		/// <inheritdoc />
		public string Description { get; internal set; }

		/// <inheritdoc />
		public DateTimeOffset CreateTime { get; internal set; }

		/// <inheritdoc />
		public IAnalysisConfig AnalysisConfig { get; internal set; }

		/// <inheritdoc />
		public IAnalysisLimits AnalysisLimits { get; internal set; }

		/// <inheritdoc />
		public Time BackgroundPersistInterval { get; internal set; }

		/// <inheritdoc />
		public IDataDescription DataDescription { get; internal set; }

		/// <inheritdoc />
		public long? ModelSnapshotRetentionDays { get; internal set; }

		/// <inheritdoc />
		public string ModelSnapshotId { get; internal set; }

		/// <inheritdoc />
		public string ResultsIndexName { get; internal set; }

		/// <inheritdoc />
		public IModelPlotConfig ModelPlotConfig { get; internal set; }

		/// <inheritdoc />
		public long? RenormalizationWindowDays { get; internal set; }

		/// <inheritdoc />
		public long? ResultsRetentionDays { get; internal set; }
	}
}
