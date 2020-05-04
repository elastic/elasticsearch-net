// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class AdaptiveSelectionStats
	{
		/// <summary>
		/// The exponentially weighted moving average queue size of search requests on the keyed node.
		/// </summary>
		[DataMember(Name ="avg_queue_size")]
		public long AverageQueueSize { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average response time of search requests on the keyed node.
		/// </summary>
		/// <remarks>only set when requesting human readable response</remarks>
		[DataMember(Name ="avg_response_time")]
		public long AverageResponseTime { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average response time of search requests on the keyed node.
		/// </summary>
		[DataMember(Name ="avg_response_time_ns")]
		public long AverageResponseTimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average service time of search requests on the keyed node.
		/// </summary>
		/// <remarks>only set when requesting human readable response</remarks>
		[DataMember(Name ="avg_service_time")]
		public string AverageServiceTime { get; internal set; }

		/// <summary>
		/// The exponentially weighted moving average service time of search requests on the keyed node.
		/// </summary>
		[DataMember(Name ="avg_service_time_ns")]
		public long AverageServiceTimeInNanoseconds { get; internal set; }

		/// <summary>
		/// The number of outstanding search requests from the node these stats are for to the keyed node.
		/// </summary>
		[DataMember(Name ="outgoing_searches")]
		public long OutgoingSearches { get; internal set; }

		/// <summary>
		/// The rank of this node; used for shard selection when routing search requests.
		/// </summary>
		[DataMember(Name ="rank")]
		public string Rank { get; internal set; }
	}
}
