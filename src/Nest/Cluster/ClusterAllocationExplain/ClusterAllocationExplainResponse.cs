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
		ShardAllocationExplanation Shard { get; set; }

		[JsonProperty("assigned")]
		bool Assigned { get; set; }

		[JsonProperty("unassigned_info")]
		UnassignedInformation UnassignedInformation { get; set; }

		[JsonProperty("nodes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		Dictionary<string, NodeAllocationExplanation> Nodes { get; set; }
	}

	public class ClusterAllocationExplainResponse : ResponseBase, IClusterAllocationExplainResponse
	{
		public ShardAllocationExplanation Shard { get; set; }

		public bool Assigned { get; set; }

		public UnassignedInformation UnassignedInformation { get; set; }

		public Dictionary<string, NodeAllocationExplanation> Nodes { get; set; }
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
		public Dictionary<string,string> NodeAttributes { get; set; }

		// TODO: Are there enum values for this?
		[JsonProperty("final_decision")]
		public string FinalDecision { get; set; }

		[JsonProperty("weight")]
		public float Weight { get; set; }

		[JsonProperty("decisions")]
		public IEnumerable<AllocationDecision> Decisions { get; set; }
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
