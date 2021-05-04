// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[DataContract]
	public class ClusterAllocationExplainResponse : ResponseBase
	{
		[DataMember(Name = "allocate_explanation")]
		public string AllocateExplanation { get; internal set; }

		[DataMember(Name = "allocation_delay")]
		public string AllocationDelay { get; internal set; }

		[DataMember(Name = "allocation_delay_in_millis")]
		public long AllocationDelayInMilliseconds { get; internal set; }

		[DataMember(Name = "can_allocate")]
		public Decision? CanAllocate { get; internal set; }

		[DataMember(Name = "can_move_to_other_node")]
		public Decision? CanMoveToOtherNode { get; internal set; }

		[DataMember(Name = "can_rebalance_cluster")]
		public Decision? CanRebalanceCluster { get; internal set; }

		[DataMember(Name = "can_rebalance_cluster_decisions")]
		public IReadOnlyCollection<AllocationDecision> CanRebalanceClusterDecisions { get; internal set; }
			= EmptyReadOnly<AllocationDecision>.Collection;

		[DataMember(Name = "can_rebalance_to_other_node")]
		public Decision? CanRebalanceToOtherNode { get; internal set; }

		[DataMember(Name = "can_remain_decisions")]
		public IReadOnlyCollection<AllocationDecision> CanRemainDecisions { get; internal set; }
			= EmptyReadOnly<AllocationDecision>.Collection;

		[DataMember(Name = "can_remain_on_current_node")]
		public Decision? CanRemainOnCurrentNode { get; internal set; }

		[DataMember(Name = "configured_delay")]
		public string ConfiguredDelay { get; internal set; }

		[DataMember(Name = "configured_delay_in_mills")]
		public long ConfiguredDelayInMilliseconds { get; internal set; }

		[DataMember(Name = "current_node")]
		public CurrentNode CurrentNode { get; internal set; }

		[DataMember(Name = "current_state")]
		public string CurrentState { get; internal set; }

		[DataMember(Name = "index")]
		public string Index { get; internal set; }

		[DataMember(Name = "move_explanation")]
		public string MoveExplanation { get; internal set; }

		[DataMember(Name = "node_allocation_decisions")]
		public IReadOnlyCollection<NodeAllocationExplanation> NodeAllocationDecisions { get; internal set; }
			= EmptyReadOnly<NodeAllocationExplanation>.Collection;

		[DataMember(Name = "primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name = "rebalance_explanation")]
		public string RebalanceExplanation { get; internal set; }

		[DataMember(Name = "remaining_delay")]
		public string RemainingDelay { get; internal set; }

		[DataMember(Name = "remaining_delay_in_millis")]
		public long RemainingDelayInMilliseconds { get; internal set; }

		[DataMember(Name = "shard")]
		public int Shard { get; internal set; }

		[DataMember(Name = "unassigned_info")]
		public UnassignedInformation UnassignedInformation { get; internal set; }
	}


	[DataContract]
	public class CurrentNode
	{
		[DataMember(Name = "id")]
		public string Id { get; internal set; }

		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "attributes")]
		public IReadOnlyDictionary<string, string> NodeAttributes { get; set; } = EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }

		[DataMember(Name = "weight_ranking")]
		public int WeightRanking { get; internal set; }
	}

	[StringEnum]
	public enum AllocationExplainDecision
	{
		[EnumMember(Value = "NO")]
		No,

		[EnumMember(Value = "YES")]
		Yes,

		[EnumMember(Value = "THROTTLE")]
		Throttle,

		[EnumMember(Value = "ALWAYS")]
		Always,
	}

	[DataContract]
	public class NodeAllocationExplanation
	{
		[DataMember(Name = "deciders")]
		public IReadOnlyCollection<AllocationDecision> Deciders { get; set; } = EmptyReadOnly<AllocationDecision>.Collection;

		[DataMember(Name = "node_attributes")]
		public IReadOnlyDictionary<string, string> NodeAttributes { get; set; } = EmptyReadOnly<string, string>.Dictionary;

		[DataMember(Name = "node_decision")]
		public Decision? NodeDecision { get; set; }

		[DataMember(Name = "node_id")]
		public string NodeId { get; set; }

		[DataMember(Name = "node_name")]
		public string NodeName { get; set; }

		[DataMember(Name = "store")]
		public AllocationStore Store { get; set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; set; }

		[DataMember(Name = "weight_ranking")]
		public int? WeightRanking { get; set; }
	}

	[StringEnum]
	public enum Decision
	{
		[EnumMember(Value = "yes")]
		Yes,

		[EnumMember(Value = "no")]
		No,

		[EnumMember(Value = "worse_balance")]
		WorseBalance,

		[EnumMember(Value = "throttled")]
		Throttled,

		[EnumMember(Value = "awaiting_info")]
		AwaitingInfo,

		[EnumMember(Value = "allocation_delayed")]
		AllocationDelayed,

		[EnumMember(Value = "no_valid_shard_copy")]
		NoValidShardCopy,

		[EnumMember(Value = "no_attempt")]
		NoAttempt
	}

	[StringEnum]
	public enum StoreCopy
	{
		[EnumMember(Value = "NONE")]
		None,

		[EnumMember(Value = "AVAILABLE")]
		Available,

		[EnumMember(Value = "CORRUPT")]
		Corrupt,

		[EnumMember(Value = "IO_ERROR")]
		IOError,

		[EnumMember(Value = "STALE")]
		Stale,

		[EnumMember(Value = "UNKNOWN")]
		Unknown
	}

	[DataContract]
	public class AllocationStore
	{
		[DataMember(Name = "allocation_id")]
		public string AllocationId { get; set; }

		[DataMember(Name = "found")]
		public bool? Found { get; set; }

		[DataMember(Name = "in_sync")]
		public bool? InSync { get; set; }

		[DataMember(Name = "matching_size_in_bytes")]
		public long? MatchingSizeInBytes { get; set; }

		[DataMember(Name = "matching_sync_id")]
		public bool? MatchingSyncId { get; set; }

		[DataMember(Name = "store_exception")]
		public string StoreException { get; set; }
	}

	[DataContract]
	public class AllocationDecision
	{
		[DataMember(Name = "decider")]
		public string Decider { get; set; }

		[DataMember(Name = "decision")]
		public AllocationExplainDecision Decision { get; set; }

		[DataMember(Name = "explanation")]
		public string Explanation { get; set; }
	}

	public class UnassignedInformation
	{
		[DataMember(Name = "at")]
		public DateTime At { get; set; }

		[DataMember(Name = "last_allocation_status")]
		public string LastAllocationStatus { get; set; }

		[DataMember(Name = "reason")]
		public UnassignedInformationReason Reason { get; set; }
	}

	public class ShardAllocationExplanation
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "index")]
		public IndexName Index { get; set; }

		[DataMember(Name = "index_uuid")]
		public string IndexUniqueId { get; set; }

		[DataMember(Name = "primary")]
		public bool Primary { get; set; }
	}

	[StringEnum]
	public enum UnassignedInformationReason
	{
		/// <summary>
		///  Unassigned as a result of an API creation of an index.
		/// </summary>
		[EnumMember(Value = "INDEX_CREATED")]
		IndexCreated,

		/// <summary>
		///  Unassigned as a result of a full cluster recovery.
		/// </summary>
		[EnumMember(Value = "CLUSTER_RECOVERED")]
		ClusterRecovered,

		/// <summary>
		///  Unassigned as a result of opening a closed index.
		/// </summary>
		[EnumMember(Value = "INDEX_REOPENED")]
		IndexReopened,

		/// <summary>
		///  Unassigned as a result of importing a dangling index.
		/// </summary>
		[EnumMember(Value = "DANGLING_INDEX_IMPORTED")]
		DanglingIndexImported,

		/// <summary>
		///  Unassigned as a result of restoring into a new index.
		/// </summary>
		[EnumMember(Value = "NEW_INDEX_RESTORED")]
		NewIndexRestored,

		/// <summary>
		///  Unassigned as a result of restoring into a closed index.
		/// </summary>
		[EnumMember(Value = "EXISTING_INDEX_RESTORED")]
		ExistingIndexRestored,

		/// <summary>
		///  Unassigned as a result of explicit addition of a replica.
		/// </summary>
		[EnumMember(Value = "REPLICA_ADDED")]
		ReplicaAdded,

		/// <summary>
		///  Unassigned as a result of a failed allocation of the shard.
		/// </summary>
		[EnumMember(Value = "ALLOCATION_FAILED")]
		AllocationFailed,

		/// <summary>
		///  Unassigned as a result of the node hosting it leaving the cluster.
		/// </summary>
		[EnumMember(Value = "NODE_LEFT")]
		NodeLeft,

		/// <summary>
		///  Unassigned as a result of explicit cancel reroute command.
		/// </summary>
		[EnumMember(Value = "REROUTE_CANCELLED")]
		RerouteCancelled,

		/// <summary>
		///  When a shard moves from started back to initializing, for example, during shadow replica
		/// </summary>
		[EnumMember(Value = "REINITIALIZED")]
		Reinitialized,

		/// <summary>
		///  A better replica location is identified and causes the existing replica allocation to be cancelled.
		/// </summary>
		[EnumMember(Value = "REALLOCATED_REPLICA")]
		ReallocatedReplica,

		/// <summary>
		///  Unassigned as a result of a failed primary while the replica was initializing.
		/// </summary>
		[EnumMember(Value = "PRIMARY_FAILED")]
		PrimaryFailed,

		/// <summary>
		///  Unassigned after forcing an empty primary
		/// </summary>
		[EnumMember(Value = "FORCED_EMPTY_PRIMARY")]
		ForcedEmptyPrimary,

		/// <summary>
		///  Forced manually to allocate
		/// </summary>
		[EnumMember(Value = "MANUAL_ALLOCATION")]
		ManualAllocation
	}
}
