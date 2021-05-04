// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public interface IShardRebalancingSettings
	{
		/// <summary>Specify when shard rebalancing is allowed</summary>
		AllowRebalance? AllowRebalance { get; set; }

		/// <summary>Allow to control how many concurrent shard rebalances are allowed cluster wide. Defaults to 2.</summary>
		int? ConcurrentRebalance { get; set; }

		/// <summary>Enable or disable rebalancing for specific kinds of shards</summary>
		RebalanceEnable? RebalanceEnable { get; set; }
	}

	public class ShardRebalancingSettings : IShardRebalancingSettings
	{
		/// <inheritdoc />
		public AllowRebalance? AllowRebalance { get; set; }

		/// <inheritdoc />
		public int? ConcurrentRebalance { get; set; }

		/// <inheritdoc />
		public RebalanceEnable? RebalanceEnable { get; set; }
	}

	public class ShardRebalancingSettingsDescriptor
		: DescriptorBase<ShardRebalancingSettingsDescriptor, IShardRebalancingSettings>, IShardRebalancingSettings
	{
		AllowRebalance? IShardRebalancingSettings.AllowRebalance { get; set; }

		int? IShardRebalancingSettings.ConcurrentRebalance { get; set; }
		RebalanceEnable? IShardRebalancingSettings.RebalanceEnable { get; set; }

		/// <inheritdoc />
		public ShardRebalancingSettingsDescriptor RebalanceEnable(RebalanceEnable? enable) => Assign(enable, (a, v) => a.RebalanceEnable = v);

		/// <inheritdoc />
		public ShardRebalancingSettingsDescriptor AllowRebalance(AllowRebalance? enable) => Assign(enable, (a, v) => a.AllowRebalance = v);

		/// <inheritdoc />
		public ShardRebalancingSettingsDescriptor ConcurrentRebalance(int? concurrent) => Assign(concurrent, (a, v) => a.ConcurrentRebalance = v);
	}
}
