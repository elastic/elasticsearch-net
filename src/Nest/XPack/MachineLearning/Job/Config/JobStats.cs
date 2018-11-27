using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Provides statistics about the operation progress of a machine learning job.
	/// </summary>
	[DataContract]
	public class JobStats
	{
		/// <summary>
		/// For open jobs only, contains messages relating to the selection of a node to run the job.
		/// </summary>
		[DataMember(Name ="assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		/// <summary>
		/// Describes the number of records processed and any related error counts.
		/// </summary>
		[DataMember(Name ="data_counts")]
		public DataCounts DataCounts { get; internal set; }

		/// <summary>
		/// A unique identifier for the job.
		/// </summary>
		[DataMember(Name ="job_id")]
		public string JobId { get; internal set; }

		/// <summary>
		/// Provides information about the size and contents of the model.
		/// </summary>
		[DataMember(Name ="model_size_stats")]
		public ModelSizeStats ModelSizeStats { get; internal set; }

		/// <summary>
		/// For open jobs only, contains information about the node where the job runs.
		/// </summary>
		[DataMember(Name ="node")]
		public DiscoveryNode Node { get; internal set; }

		/// <summary>
		/// For open jobs only, the elapsed time for which the job has been open.
		/// </summary>
		[DataMember(Name ="open_time")]
		public Time OpenTime { get; internal set; }

		/// <summary>
		/// The status of the job.
		/// </summary>
		[DataMember(Name ="state")]
		public JobState State { get; internal set; }
	}
}
