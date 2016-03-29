using System;
using System.Collections.Generic;

namespace Nest
{
	/// <summary> Settings to control where, when, and how shards are allocated to nodes. </summary>
	public interface IClusterModuleSettings
	{
		/// <summary><summary>
		bool? ReadOnly { get; set; }

		/// <summary><summary>
		IDictionary<string, LogLevel> Logger { get; set; }

		/// <summary><summary>
		IAllocationAwarenessSettings AllocationAwareness { get; set; }

		/// <summary><summary>
		IAllocationFilteringSettings AllocationFiltering { get; set; }

		/// <summary><summary>
		IDiskBasedShardAllocationSettings DiskBasedShardAllocation { get; set; }

		/// <summary><summary>
		IShardAllocationSettings ShardAllocation { get; set; }

		/// <summary><summary>
		IShardBalancingHeuristicsSettings ShardBalancingHeuristics { get; set; }

		/// <summary><summary>
		IShardRebalancingSettings ShardRebalancing { get; set; }

	}

	///<inheritdoc/>
	public class ClusterModuleSettings : IClusterModuleSettings
	{
		///<inheritdoc/>
		public bool? ReadOnly { get; set; }

		///<inheritdoc/>
		public IDictionary<string, LogLevel> Logger { get; set; }

		///<inheritdoc/>
		public IAllocationAwarenessSettings AllocationAwareness { get; set; }

		///<inheritdoc/>
		public IAllocationFilteringSettings AllocationFiltering { get; set; }

		///<inheritdoc/>
		public IDiskBasedShardAllocationSettings DiskBasedShardAllocation { get; set; }

		///<inheritdoc/>
		public IShardAllocationSettings ShardAllocation { get; set; }

		///<inheritdoc/>
		public IShardBalancingHeuristicsSettings ShardBalancingHeuristics { get; set; }

		///<inheritdoc/>
		public IShardRebalancingSettings ShardRebalancing { get; set; }

	}

	///<inheritdoc/>
	public class ClusterModuleSettingsDescriptor : DescriptorBase<ClusterModuleSettingsDescriptor, IClusterModuleSettings>, IClusterModuleSettings
	{
		bool? IClusterModuleSettings.ReadOnly { get; set; }

		IDictionary<string, LogLevel> IClusterModuleSettings.Logger { get; set; }

		IAllocationAwarenessSettings IClusterModuleSettings.AllocationAwareness { get; set; }

		IAllocationFilteringSettings IClusterModuleSettings.AllocationFiltering { get; set; }

		IDiskBasedShardAllocationSettings IClusterModuleSettings.DiskBasedShardAllocation { get; set; }

		IShardAllocationSettings IClusterModuleSettings.ShardAllocation { get; set; }

		IShardBalancingHeuristicsSettings IClusterModuleSettings.ShardBalancingHeuristics { get; set; }

		IShardRebalancingSettings IClusterModuleSettings.ShardRebalancing { get; set; }

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor ShardRebalancing(bool? readOnly = true) => Assign(a => a.ReadOnly = readOnly);

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor Logger(IDictionary<string, LogLevel> logger) => Assign(a => a.Logger = logger);

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor Logger(Func<FluentDictionary<string, LogLevel>, FluentDictionary<string, LogLevel>> selector) => 
			Assign(a => a.Logger = selector?.Invoke(new FluentDictionary<string, LogLevel>()));

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor AllocationAwareness(Func<AllocationAwarenessSettings, IAllocationAwarenessSettings> selector) => 
			Assign(a => a.AllocationAwareness = selector?.Invoke(new AllocationAwarenessSettings()));

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor AllocationFiltering(Func<AllocationFilteringSettingsDescriptor, IAllocationFilteringSettings> selector) => 
			Assign(a => a.AllocationFiltering = selector?.Invoke(new AllocationFilteringSettingsDescriptor()));

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor DiskBasedShardAllocation(Func<DiskBasedShardAllocationSettingsDescriptor, IDiskBasedShardAllocationSettings> selector) => 
			Assign(a => a.DiskBasedShardAllocation = selector?.Invoke(new DiskBasedShardAllocationSettingsDescriptor()));

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor ShardAllocation(Func<ShardAllocationSettingsDescriptor, IShardAllocationSettings> selector) => 
			Assign(a => a.ShardAllocation = selector?.Invoke(new ShardAllocationSettingsDescriptor()));

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor ShardBalancingHeuristics(Func<ShardBalancingHeuristicsSettingsDescriptor, IShardBalancingHeuristicsSettings> selector) => 
			Assign(a => a.ShardBalancingHeuristics = selector?.Invoke(new ShardBalancingHeuristicsSettingsDescriptor()));

		///<inheritdoc/>
		public ClusterModuleSettingsDescriptor ShardRebalancing(Func<ShardRebalancingSettingsDescriptor, IShardRebalancingSettings> selector) => 
			Assign(a => a.ShardRebalancing = selector?.Invoke(new ShardRebalancingSettingsDescriptor()));

	}

}