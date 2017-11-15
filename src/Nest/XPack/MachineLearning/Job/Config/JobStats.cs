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
		/// A unique identifier for the job.
		/// </summary>
		[JsonProperty("job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Describes the number of records processed and any related error counts.
		/// </summary>
		[JsonProperty("data_counts")]
		public DataCounts DataCounts { get; internal set; }

		/// <summary>
		/// Provides information about the size and contents of the model.
		/// </summary>
		[JsonProperty("model_size_stats")]
		public ModelSizeStats ModelSizeStats { get; internal set; }

		/// <summary>
		/// The status of the job.
		/// </summary>
		[JsonProperty("state")]
		public JobState State { get; internal set; }

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
	}
}
