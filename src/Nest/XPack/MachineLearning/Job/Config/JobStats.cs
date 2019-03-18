using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Provides statistics about the operation progress of a machine learning job.
	/// </summary>
	[JsonObject]
	public class JobStats
	{
		/// <summary>
		/// For open jobs only, contains messages relating to the selection of a node to run the job.
		/// </summary>
		[JsonProperty("assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		/// <summary>
		/// Describes the number of records processed and any related error counts.
		/// </summary>
		[JsonProperty("data_counts")]
		public DataCounts DataCounts { get; internal set; }

		/// <summary>
		/// A unique identifier for the job.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Provides information about the size and contents of the model.
		/// </summary>
		[JsonProperty("model_size_stats")]
		public ModelSizeStats ModelSizeStats { get; internal set; }

		/// <summary>
		/// For open jobs only, contains information about the node where the job runs.
		/// </summary>
		[JsonProperty("node")]
		public DiscoveryNode Node { get; internal set; }

		/// <summary>
		/// For open jobs only, the elapsed time for which the job has been open.
		/// </summary>
		[JsonProperty("open_time")]
		public Time OpenTime { get; internal set; }

		/// <summary>
		/// The status of the job.
		/// </summary>
		[JsonProperty("state")]
		public JobState State { get; internal set; }

		/// <summary>
		///  Contains job statistics if job contains a forecast.
		/// </summary>
		[JsonProperty("forecasts_stats")]
		public JobForecastStatistics Forecasts { get; internal set; }
	}

	public class JobForecastStatistics
	{
		/// <summary>
		/// The number of forecasts currently available for this model.
		/// </summary>
		[JsonProperty("total")]
		public long Total { get; internal set; }

		/// <summary>
		/// Statistics about the memory usage: minimum, maximum, average and total.
		/// </summary>
		[JsonProperty("memory_bytes")]
		public JobStatistics MemoryBytes { get; internal set; }

		/// <summary>
		/// Statistics about the forecast runtime in milliseconds: minimum, maximum, average and total.
		/// </summary>
		[JsonProperty("processing_time_ms")]
		public JobStatistics ProcessingTimeMilliseconds { get; internal set; }

		/// <summary>
		/// Statistics about the number of forecast records: minimum, maximum, average and total.
		/// </summary>
		[JsonProperty("records")]
		public JobStatistics Records { get; internal set; }

		/// <summary>
		/// Counts per forecast status.
		/// </summary>
		[JsonProperty("status")]
		public IReadOnlyDictionary<string, long> Status { get; internal set; }
			= EmptyReadOnly<string, long>.Dictionary;

		public class JobStatistics
		{
			[JsonProperty("avg")]
			public double Average { get; internal set; }

			[JsonProperty("max")]
			public double Maximum { get; internal set; }

			[JsonProperty("min")]
			public double Minimum { get; internal set; }

			[JsonProperty("total")]
			public double Total { get; internal set; }
		}
	}
}
