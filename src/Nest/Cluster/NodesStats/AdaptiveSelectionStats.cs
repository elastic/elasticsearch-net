using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class AdaptiveSelectionStats
	{
		/// <summary>
		/// The number of outstanding search requests from the node these stats are for to the keyed node.
		/// </summary>
		[JsonProperty("outgoing_searches")]
		public long OutgoingSearches { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average queue size of search requests on the keyed node.
		/// </summary>
		[JsonProperty("avg_queue_size")]
		public long AverageQueueSize { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average service time of search requests on the keyed node.
		/// </summary>
		/// <remarks>only set when requesting human readable response</remarks>
		[JsonProperty("avg_service_time")]
		public string AverageServiceTime { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average service time of search requests on the keyed node.
		/// </summary>
		[JsonProperty("avg_service_time_ns")]
		public long AverageServiceTimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average response time of search requests on the keyed node.
		/// </summary>
		/// <remarks>only set when requesting human readable response</remarks>
		[JsonProperty("avg_response_time")]
		public long AverageResponseTime { get; internal set; }
		
		/// <summary>
		/// The exponentially weighted moving average response time of search requests on the keyed node.
		/// </summary>
		[JsonProperty("avg_response_time_ns")]
		public long AverageResponseTimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The rank of this node; used for shard selection when routing search requests.
		/// </summary>
		[JsonProperty("rank")]
		public string Rank { get; internal set; }
	}
}