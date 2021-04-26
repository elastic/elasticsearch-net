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

namespace Nest
{
	public class EnrichStatsResponse : ResponseBase
	{
		[DataMember(Name = "executing_policies")]
		public IReadOnlyCollection<ExecutingPolicy> ExecutingPolicies { get; internal set; } = EmptyReadOnly<ExecutingPolicy>.Collection;

		[DataMember(Name = "coordinator_stats")]
		public IReadOnlyCollection<CoordinatorStats> CoordinatorStats { get; internal set; } = EmptyReadOnly<CoordinatorStats>.Collection;
	}

	public class ExecutingPolicy
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "task")]
		public TaskInfo Task { get; internal set; }
	}

	public class CoordinatorStats
	{
		[DataMember(Name = "node_id")]
		public string NodeId { get; internal set; }

		[DataMember(Name = "queue_size")]
		public int QueueSize { get; internal set; }

		[DataMember(Name = "remote_requests_current")]
		public int RemoteRequestsCurrent { get; internal set; }

		[DataMember(Name = "remote_requests_total")]
		public long RemoteRequestsTotal { get; internal set; }

		[DataMember(Name = "executed_searches_total")]
		public long ExecutedSearchesTotal { get; internal set; }
	}
}
