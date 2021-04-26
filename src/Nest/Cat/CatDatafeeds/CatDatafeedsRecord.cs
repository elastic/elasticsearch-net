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
	public class CatDatafeedsRecord : ICatRecord
	{
		/// <summary>
		/// For started datafeeds only, contains messages relating to the selection of a node.
		/// </summary>
		[DataMember(Name="assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		/// <summary>
		/// (Default) The number of buckets processed.
		/// </summary>
		[DataMember(Name="buckets.count")]
		public string BucketsCount { get; internal set; }

		/// <summary>
		/// (Default) A numerical character string that uniquely identifies the datafeed. This identifier can contain lowercase alphanumeric
		/// characters (a-z and 0-9), hyphens, and underscores. It must start and end with alphanumeric characters.
		/// </summary>
		[DataMember(Name="id")]
		public string Id { get; internal set; }

		/// <summary>
		/// The network address of the node. For started datafeeds only, this information pertains to the node upon which the datafeed is started.
		/// </summary>
		[DataMember(Name="node.address")]
		public string NodeAddress { get; internal set; }

		/// <summary>
		/// The ephemeral ID of the node. For started datafeeds only, this information pertains to the node upon which the datafeed is started.
		/// </summary>
		[DataMember(Name="node.ephemeral_id")]
		public string NodeEphemeralId { get; internal set; }

		/// <summary>
		/// The unique identifier of the node. For started datafeeds only, this information pertains to the node upon which the datafeed is started.
		/// </summary>
		[DataMember(Name="node.id")]
		public string NodeId { get; internal set; }

		/// <summary>
		/// The node name. For started datafeeds only, this information pertains to the node upon which the datafeed is started.
		/// </summary>
		[DataMember(Name="node.name")]
		public string NodeName { get; internal set; }

		/// <summary>
		/// The average search time per bucket, in milliseconds.
		/// </summary>
		[DataMember(Name="search.bucket_avg")]
		public string SearchBucketAvg { get; internal set; }

		/// <summary>
		/// (Default) The number of searches run by the datafeed.
		/// </summary>
		[DataMember(Name="search.count")]
		public string SearchCount { get; internal set; }

		/// <summary>
		/// The exponential average search time per hour, in milliseconds.
		/// </summary>
		[DataMember(Name="search.exp_avg_hour")]
		public string SearchExpAvgHour { get; internal set; }

		/// <summary>
		/// The total time the datafeed spent searching, in milliseconds.
		/// </summary>
		[DataMember(Name="search.time")]
		public string SearchTime { get; internal set; }

		/// <summary>
		/// The status of the datafeed.
		/// </summary>
		[DataMember(Name="state")]
		public DatafeedState State { get; internal set; }
	}
}
