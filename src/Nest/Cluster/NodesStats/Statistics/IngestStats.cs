using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class IngestStats
	{
		/// <summary>
		/// The total number of document ingested during the lifetime of this node
		/// </summary>
		[DataMember(Name ="count")]
		public long Count { get; set; }

		/// <summary>
		/// The total number of documents currently being ingested.
		/// </summary>
		[DataMember(Name ="current")]
		public long Current { get; set; }

		/// <summary>
		/// The total number ingest preprocessing operations failed during the lifetime of this node
		/// </summary>
		[DataMember(Name ="failed")]
		public long Failed { get; set; }

		/// <summary>
		/// The total time spent on ingest preprocessing documents during the lifetime of this node
		/// </summary>
		[DataMember(Name ="time_in_millis")]
		public long TimeInMilliseconds { get; set; }
	}
}
