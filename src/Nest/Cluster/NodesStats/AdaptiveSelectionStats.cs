/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Runtime.Serialization;

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
