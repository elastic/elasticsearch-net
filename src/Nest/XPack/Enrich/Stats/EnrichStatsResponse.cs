// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
