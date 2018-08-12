using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IngestStats
	{
		/// <summary>
		/// The total number of document ingested during the lifetime of this node
		/// </summary>
		public long Count { get; set; }
		/// <summary>
		/// The total time spent on ingest preprocessing documents during the lifetime of this node
		/// </summary>
		public long TimeInMilliseconds { get; set; }
		/// <summary>
		/// The total number of documents currently being ingested.
		/// </summary>
		public long Current { get; set; }
		/// <summary>
		/// The total number ingest preprocessing operations failed during the lifetime of this node
		/// </summary>
		public long Failed { get; set; }

	}
}
