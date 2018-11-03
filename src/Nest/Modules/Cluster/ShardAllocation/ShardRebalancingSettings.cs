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
		public ShardRebalancingSettingsDescriptor RebalanceEnable(RebalanceEnable? enable) => Assign(a => a.RebalanceEnable = enable);

		/// <inheritdoc />
		public ShardRebalancingSettingsDescriptor AllowRebalance(AllowRebalance? enable) => Assign(a => a.AllowRebalance = enable);

		/// <inheritdoc />
		public ShardRebalancingSettingsDescriptor ConcurrentRebalance(int? concurrent) => Assign(a => a.ConcurrentRebalance = concurrent);
	}
}
