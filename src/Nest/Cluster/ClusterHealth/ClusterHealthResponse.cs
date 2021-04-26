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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	public class ClusterHealthResponse : ResponseBase
	{
		[DataMember(Name = "active_primary_shards")]
		public int ActivePrimaryShards { get; internal set; }

		[DataMember(Name = "active_shards")]
		public int ActiveShards { get; internal set; }

		[DataMember(Name = "active_shards_percent_as_number")]
		public double ActiveShardsPercentAsNumber { get; internal set; }

		[DataMember(Name = "cluster_name")]
		public string ClusterName { get; internal set; }

		[DataMember(Name = "delayed_unassigned_shards")]
		public int DelayedUnassignedShards { get; internal set; }

		[DataMember(Name = "indices")]
		[JsonFormatter(typeof(ResolvableReadOnlyDictionaryFormatter<IndexName, IndexHealthStats>))]
		public IReadOnlyDictionary<IndexName, IndexHealthStats> Indices { get; internal set; } =
			EmptyReadOnly<IndexName, IndexHealthStats>.Dictionary;

		[DataMember(Name = "initializing_shards")]
		public int InitializingShards { get; internal set; }

		[DataMember(Name = "number_of_data_nodes")]
		public int NumberOfDataNodes { get; internal set; }

		[DataMember(Name = "number_of_in_flight_fetch")]
		public int NumberOfInFlightFetch { get; internal set; }

		[DataMember(Name = "number_of_nodes")]
		public int NumberOfNodes { get; internal set; }

		[DataMember(Name = "number_of_pending_tasks")]
		public int NumberOfPendingTasks { get; internal set; }

		[DataMember(Name = "relocating_shards")]
		public int RelocatingShards { get; internal set; }

		[DataMember(Name = "status")]
		public Health Status { get; internal set; }

		[DataMember(Name = "task_max_waiting_in_queue_millis")]
		public long TaskMaxWaitTimeInQueueInMilliseconds { get; internal set; }

		[DataMember(Name = "timed_out")]
		public bool TimedOut { get; internal set; }

		[DataMember(Name = "unassigned_shards")]
		public int UnassignedShards { get; internal set; }
	}
}
