using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Nest
{
	// TODO this custom converter is in place because the response changed from ES 5.0 to ES 5.2
	// so we are supporting both formats. In 6.0 we should remove this entirely and only support
	// the new format.
	public class ClusterAllocationExplainResponseJsonConverter : JsonConverter
	{
		public override bool CanRead { get; } = true;

		public override bool CanWrite { get; } = false;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var response = new ClusterAllocationExplainResponse();
			var newResponseStructure = false;
			foreach (var p in o.Properties())
			{
				switch (p.Name)
				{
					case "shard":
						if (p.Value.Type == JTokenType.Object)
						{
							response.Shard = p.Value.ToObject<ShardAllocationExplanation>();
						}
						else if (p.Value.Type == JTokenType.Integer)
						{
							newResponseStructure = true;
							response.ShardId = p.Value.ToObject<int>();
						}
						break;
					case "index":
						response.Index = p.Value.ToString();
						break;
					case "primary":
						response.Primary = p.Value.ToObject<bool>();
						break;
					case "can_allocate":
						response.CanAllocate = p.Value.ToObject<Decision?>();
						break;
					case "allocate_explanation":
						response.AllocateExplanation = p.Value.ToString();
						break;
					case "configured_delay":
						response.ConfiguredDelay = p.Value.ToString();
						break;
					case "configured_delay_in_millis":
						response.ConfiguredDelayInMilliseconds = p.Value.ToObject<long>();
						break;
					case "can_remain_decisions":
						response.CanRemainDecisions = p.Value.ToObject<List<AllocationDecision>>();
						break;
					case "can_move_to_other_node":
						response.CanMoveToOtherNode = p.Value.ToObject<Decision?>();
						break;
					case "move_explanation":
						response.MoveExplanation = p.Value.ToString();
						break;
					case "current_state":
						response.CurrentState = p.Value.ToString();
						break;
					case "current_node":
						response.CurrentNode = p.Value.ToObject<CurrentNode>();
						break;
					case "can_remain_on_current_node":
						response.CanRemainOnCurrentNode = p.Value.ToObject<Decision?>();
						break;
					case "can_rebalance_cluster":
						response.CanRebalanceCluster = p.Value.ToObject<Decision?>();
						break;
					case "can_rebalance_cluster_decisions":
						response.CanRebalanceClusterDecisions = p.Value.ToObject<List<AllocationDecision>>();
						break;
					case "can_rebalance_to_other_node":
						response.CanRebalanceToOtherNode = p.Value.ToObject<Decision?>();
						break;
					case "rebalance_explanation":
						response.RebalanceExplanation = p.Value.ToString();
						break;
					case "assigned":
						response.Assigned = p.Value.ToObject<bool>();
						break;
					case "assigned_node_id":
						response.AssignedNodeId = p.Value.ToString();
						break;
					case "shard_state_fetch_pending":
						response.ShardStateFetchPending = p.Value.ToObject<bool>();
						break;
					case "unassigned_info":
						response.UnassignedInformation = p.Value.ToObject<UnassignedInformation>();
						break;
					case "allocation_delay":
						response.AllocationDelay = p.Value.ToString();
						break;
					case "allocation_delay_in_millis":
					case "allocation_delay_ms":
						response.AllocationDelayInMilliseconds = p.Value.ToObject<long>();
						break;
					case "remaining_delay":
						response.RemainingDelay = p.Value.ToString();
						break;
					case "remaining_delay_in_millis":
					case "remaining_delay_ms":
						response.RemainingDelayInMilliseconds = p.Value.ToObject<long>();
						break;
					case "node_allocation_decisions":
						response.NodeAllocationDecisions = p.Value.ToObject<List<NodeAllocationExplanation>>();
						break;
					case "nodes":
						response.Nodes = p.Value.ToObject<Dictionary<string, NodeAllocationExplanation>>();
						break;
				}
			}

			if (newResponseStructure)
			{
				// Fill in old properties
				response.Shard = new ShardAllocationExplanation
				{
					Id = response.ShardId,
					Index = response.Index,
					Primary = response.Primary
				};

				if (response.NodeAllocationDecisions != null)
				{
					var nodes = new Dictionary<string, NodeAllocationExplanation>();
					foreach (var explanation in response.NodeAllocationDecisions)
					{
						explanation.Decisions = explanation.Deciders;
						nodes.Add(explanation.NodeId, explanation);
					}
				}
			}

			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
