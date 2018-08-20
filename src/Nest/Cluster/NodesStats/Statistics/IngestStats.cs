using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IngestStats
	{
		/// <summary>
		/// The total number of document ingested during the lifetime of this node
		/// </summary>
		[JsonProperty("count")]
		public long Count { get; set; }

		/// <summary>
		/// The total time spent on ingest preprocessing documents during the lifetime of this node
		/// </summary>
		[JsonProperty("time_in_millis")]
		public long TimeInMilliseconds { get; set; }

		/// <summary>
		/// The total number of documents currently being ingested.
		/// </summary>
		[JsonProperty("current")]
		public long Current { get; set; }

		/// <summary>
		/// The total number ingest preprocessing operations failed during the lifetime of this node
		/// </summary>
		[JsonProperty("failed")]
		public long Failed { get; set; }
	}
}
