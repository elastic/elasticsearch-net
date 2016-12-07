using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public interface IClusterAllocationExplainResponse : IResponse
	{
		[JsonProperty("shard")]
		ShardAllocationExplanation Shard { get; }

		[JsonProperty("assigned")]
		bool Assigned { get; }

		[JsonProperty("assigned_node_id")]
		string AssignedNodeId { get; }

		[JsonProperty("shard_state_fetch_pending")]
		bool ShardStateFetchPending { get; }

		[JsonProperty("unassigned_info")]
		UnassignedInformation UnassignedInformation { get; }

		[JsonProperty("allocation_delay")]
		string AllocationDelay { get; }

		[JsonProperty("allocation_delay_ms")]
		long AllocationDelayInMilliseconds { get; }

		[JsonProperty("remaining_delay")]
		string RemainingDelay { get; }

		[JsonProperty("remaining_delay_ms")]
		long RemainingDelayInMilliseconds { get; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, NodeAllocationExplanation>))]
		IReadOnlyDictionary<string, NodeAllocationExplanation> Nodes { get; }
	}

	public class ClusterAllocationExplainResponse : ResponseBase, IClusterAllocationExplainResponse
	{
		public ShardAllocationExplanation Shard { get; internal set; }

		public bool Assigned { get; internal set; }

		public string AssignedNodeId { get; internal set; }

		public bool ShardStateFetchPending { get; internal set; }

		public UnassignedInformation UnassignedInformation { get; internal set; }

		public string AllocationDelay { get; internal set; }

		public long AllocationDelayInMilliseconds { get; internal set; }

		public string RemainingDelay { get; internal set; }

		public long RemainingDelayInMilliseconds { get; internal set; }

		public IReadOnlyDictionary<string, NodeAllocationExplanation> Nodes { get; internal set; } = EmptyReadOnly<string, NodeAllocationExplanation>.Dictionary;
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
		[JsonProperty("node_name")]
		public string NodeName { get; set; }

		[JsonProperty("node_attributes")]
		public IReadOnlyDictionary<string, string> NodeAttributes { get; set; } = EmptyReadOnly<string, string>.Dictionary;

		[JsonProperty("store")]
		public AllocationStore Store { get; set; }

		[JsonProperty("final_decision")]
		public FinalDecision FinalDecision { get; set; }

		[JsonProperty("final_explanation")]
		public string FinalExplanation { get; set; }

		[JsonProperty("weight")]
		public float Weight { get; set; }

		[JsonProperty("decisions")]
		public IReadOnlyCollection<AllocationDecision> Decisions { get; set; } = EmptyReadOnly<AllocationDecision>.Collection;
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum FinalDecision
	{
		[EnumMember(Value = "YES")]
		Yes,

		[EnumMember(Value = "NO")]
		No,

		[EnumMember(Value = "ALREADY_ASSIGNED")]
		AlreadyAssigned
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
		[JsonProperty("shard_copy")]
		public StoreCopy ShardCopy { get; set; }
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
