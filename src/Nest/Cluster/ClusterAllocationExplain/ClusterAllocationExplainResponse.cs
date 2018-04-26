using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	public interface IClusterAllocationExplainResponse : IResponse
	{
		[JsonProperty("index")]
		string Index { get; }

		[JsonProperty("shard")]
		int Shard { get; }

		[JsonProperty("primary")]
		bool Primary { get; }

		[JsonProperty("current_state")]
		string CurrentState { get; }

		[JsonProperty("unassigned_info")]
		UnassignedInformation UnassignedInformation { get; }

		[JsonProperty("can_allocate")]
		Decision? CanAllocate { get; }

		[JsonProperty("allocate_explanation")]
		string AllocateExplanation { get; }

		[JsonProperty("configured_delay")]
		string ConfiguredDelay { get; }

		[JsonProperty("configured_delay_in_mills")]
		long ConfiguredDelayInMilliseconds { get; }

		[JsonProperty("current_node")]
		CurrentNode CurrentNode { get; }

		[JsonProperty("can_remain_on_current_node")]
		Decision? CanRemainOnCurrentNode { get; }

		[JsonProperty("can_remain_decisions")]
		IReadOnlyCollection<AllocationDecision> CanRemainDecisions { get; }

		[JsonProperty("can_rebalance_cluster")]
		Decision? CanRebalanceCluster { get; }

		[JsonProperty("can_rebalance_to_other_node")]
		Decision? CanRebalanceToOtherNode { get; }

		[JsonProperty("can_rebalance_cluster_decisions")]
		IReadOnlyCollection<AllocationDecision> CanRebalanceClusterDecisions { get; }

		[JsonProperty("rebalance_explanation")]
		string RebalanceExplanation { get; }

		[JsonProperty("node_allocation_decisions")]
		IReadOnlyCollection<NodeAllocationExplanation> NodeAllocationDecisions { get; }

		[JsonProperty("can_move_to_other_node")]
		Decision? CanMoveToOtherNode { get; }

		[JsonProperty("move_explanation")]
		string MoveExplanation { get; }

		[JsonProperty("allocation_delay")]
		string AllocationDelay { get; }

		[JsonProperty("allocation_delay_in_millis")]
		long AllocationDelayInMilliseconds { get; }

		[JsonProperty("remaining_delay")]
		string RemainingDelay { get; }

		[JsonProperty("remaining_delay_in_millis")]
		long RemainingDelayInMilliseconds { get; }
	}

	public class ClusterAllocationExplainResponse : ResponseBase, IClusterAllocationExplainResponse
	{
		public string Index { get; internal set; }

		public int Shard { get; internal set; }

		public bool Primary { get; internal set; }

		public string CurrentState { get; internal set; }

		public UnassignedInformation UnassignedInformation { get; internal set; }

		public Decision? CanAllocate { get; internal set; }

		public string AllocateExplanation { get; internal set; }

		public string ConfiguredDelay { get; internal set; }

		public long ConfiguredDelayInMilliseconds { get; internal set; }

		public CurrentNode CurrentNode { get; internal set; }

		public Decision? CanRemainOnCurrentNode { get; internal set; }

		public IReadOnlyCollection<AllocationDecision> CanRemainDecisions { get; internal set; }

		public Decision? CanRebalanceCluster { get; internal set; }

		public Decision? CanRebalanceToOtherNode { get; internal set; }

		public IReadOnlyCollection<AllocationDecision> CanRebalanceClusterDecisions { get; internal set; } = EmptyReadOnly<AllocationDecision>.Collection;

		public string RebalanceExplanation { get; internal set; }

		public IReadOnlyCollection<NodeAllocationExplanation> NodeAllocationDecisions { get; internal set; }

		public Decision? CanMoveToOtherNode { get; internal set; }

		public string MoveExplanation { get; internal set; }

		public string AllocationDelay { get; internal set; }

		public long AllocationDelayInMilliseconds { get; internal set; }

		public string RemainingDelay { get; internal set; }

		public long RemainingDelayInMilliseconds { get; internal set; }
	}

	[JsonObject]
	public class CurrentNode
	{
		[JsonProperty("id")]
		public string Id { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("weight_ranking")]
		public string WeightRanking { get; internal set; }

		[JsonProperty("attributes")]
		public IReadOnlyDictionary<string, string> NodeAttributes { get; set; } = EmptyReadOnly<string, string>.Dictionary;
	}

	[JsonConverter(typeof(StringEnumConverter))]
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

	[JsonObject]
	public class NodeAllocationExplanation
	{
		[JsonProperty("node_id")]
		public string NodeId { get; set; }

		[JsonProperty("node_name")]
		public string NodeName { get; set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; set; }

		[JsonProperty("node_decision")]
		public Decision? NodeDecision { get; set; }

		[JsonProperty("node_attributes")]
		public IReadOnlyDictionary<string, string> NodeAttributes { get; set; } = EmptyReadOnly<string, string>.Dictionary;

		[JsonProperty("store")]
		public AllocationStore Store { get; set; }

		[JsonProperty("weight_ranking")]
		public int? WeightRanking { get; set; }

		[JsonProperty("deciders")]
		public IReadOnlyCollection<AllocationDecision> Deciders { get; set; } = EmptyReadOnly<AllocationDecision>.Collection;
	}

	[JsonConverter(typeof(StringEnumConverter))]
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

	[JsonConverter(typeof(StringEnumConverter))]
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

	[JsonObject]
	public class AllocationStore
	{
		[JsonProperty("found")]
		public bool? Found { get; set; }

		[JsonProperty("in_sync")]
		public bool? InSync { get; set; }

		[JsonProperty("allocation_id")]
		public string AllocationId { get; set; }

		[JsonProperty("matching_sync_id")]
		public bool? MatchingSyncId { get; set; }

		[JsonProperty("matching_size_in_bytes")]
		public long? MatchingSizeInBytes { get; set; }

		[JsonProperty("store_exception")]
		public string StoreException { get; set; }
	}

	[JsonObject]
	public class AllocationDecision
	{
		[JsonProperty("decider")]
		public string Decider { get; set; }

		[JsonProperty("decision")]
		public AllocationExplainDecision Decision { get; set; }

		[JsonProperty("explanation")]
		public string Explanation { get; set; }
	}

	public class UnassignedInformation
	{
		[JsonProperty("reason")]
		public UnassignedInformationReason Reason { get; set; }

		[JsonProperty("at")]
		public DateTime At { get; set; }

		[JsonProperty("last_allocation_status")]
		public string LastAllocationStatus { get; set; }
	}

	public class ShardAllocationExplanation
	{
		[JsonProperty("index")]
		public IndexName Index { get; set; }

		[JsonProperty("index_uuid")]
		public string IndexUniqueId { get; set; }

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("primary")]
		public bool Primary { get; set; }
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum UnassignedInformationReason
	{
		///<summary>
		/// Unassigned as a result of an API creation of an index.
		///</summary>
		[EnumMember(Value = "INDEX_CREATED")]
		IndexCreated,

		///<summary>
		/// Unassigned as a result of a full cluster recovery.
		///</summary>
		[EnumMember(Value = "CLUSTER_RECOVERED")]
		ClusterRecovered,

		///<summary>
		/// Unassigned as a result of opening a closed index.
		///</summary>
		[EnumMember(Value = "INDEX_REOPENED")]
		IndexReopened,

		///<summary>
		/// Unassigned as a result of importing a dangling index.
		///</summary>
		[EnumMember(Value = "DANGLING_INDEX_IMPORTED")]
		DanglingIndexImported,

		///<summary>
		/// Unassigned as a result of restoring into a new index.
		///</summary>
		[EnumMember(Value = "NEW_INDEX_RESTORED")]
		NewIndexRestored,

		///<summary>
		/// Unassigned as a result of restoring into a closed index.
		///</summary>
		[EnumMember(Value = "EXISTING_INDEX_RESTORED")]
		ExistingIndexRestored,

		///<summary>
		/// Unassigned as a result of explicit addition of a replica.
		///</summary>
		[EnumMember(Value = "REPLICA_ADDED")]
		ReplicaAdded,

		///<summary>
		/// Unassigned as a result of a failed allocation of the shard.
		///</summary>
		[EnumMember(Value = "ALLOCATION_FAILED")]
		AllocationFailed,

		///<summary>
		/// Unassigned as a result of the node hosting it leaving the cluster.
		///</summary>
		[EnumMember(Value = "NODE_LEFT")]
		NodeLeft,

		///<summary>
		/// Unassigned as a result of explicit cancel reroute command.
		///</summary>
		[EnumMember(Value = "REROUTE_CANCELLED")]
		RerouteCancelled,

		///<summary>
		/// When a shard moves from started back to initializing, for example, during shadow replica
		///</summary>
		[EnumMember(Value = "REINITIALIZED")]
		Reinitialized,

		///<summary>
		/// A better replica location is identified and causes the existing replica allocation to be cancelled.
		///</summary>
		[EnumMember(Value = "REALLOCATED_REPLICA")]
		ReallocatedReplica
	}
}
