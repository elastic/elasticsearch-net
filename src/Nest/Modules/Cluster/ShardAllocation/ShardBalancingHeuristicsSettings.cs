namespace Nest
{
	public interface IShardBalancingHeuristicsSettings
	{
		/// <summary>Defines the weight factor for shards allocated on a node (float). Defaults to 0.45f.
		///  Raising this raises the tendency to equalize the number of shards across all nodes in the cluster.</summary>
		float? BalanceShard { get; set; }

		/// <summary>Defines a factor to the number of shards per index allocated on a specific node (float). Defaults to 0.55f. 
		/// Raising this raises the tendency to equalize the number of shards per index across all nodes in the cluster.</summary>
		float? BalanceIndex { get; set; }

		/// <summary>Minimal optimization value of operations that should be performed (non negative float). Defaults to 1.0f.
		///  Raising this will cause the cluster to be less aggressive about optimizing the shard balance</summary>
		float? BalanceThreshold { get; set; }
	}

	public class ShardBalancingHeuristicsSettings : IShardBalancingHeuristicsSettings
	{
		///<inheritdoc/>
		public float? BalanceShard { get; set; }

		///<inheritdoc/>
		public float? BalanceIndex { get; set; }

		///<inheritdoc/>
		public float? BalanceThreshold { get; set; }
	}

	public class ShardBalancingHeuristicsSettingsDescriptor 
		: DescriptorBase<ShardBalancingHeuristicsSettingsDescriptor, IShardBalancingHeuristicsSettings>, IShardBalancingHeuristicsSettings
	{
		float? IShardBalancingHeuristicsSettings.BalanceShard { get; set; }

		float? IShardBalancingHeuristicsSettings.BalanceIndex { get; set; }

		float? IShardBalancingHeuristicsSettings.BalanceThreshold { get; set; }

		///<inheritdoc/>
		public ShardBalancingHeuristicsSettingsDescriptor BalanceShard(float? balance) => Assign(a => a.BalanceShard = balance);

		///<inheritdoc/>
		public ShardBalancingHeuristicsSettingsDescriptor BalanceIndex(float? balance) => Assign(a => a.BalanceIndex = balance);

		///<inheritdoc/>
		public ShardBalancingHeuristicsSettingsDescriptor BalanceThreshold(float? balance) => Assign(a => a.BalanceThreshold = balance);


	}
}