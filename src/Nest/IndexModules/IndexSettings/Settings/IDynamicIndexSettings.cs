using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexSettingsConverter))]
	public interface IDynamicIndexSettings
	{
		/// <summary>
		///The number of replicas each primary shard has. Defaults to 1. 
		/// </summary>
		int? NumberOfReplicas { get; set; }

		/// <summary>
		///Auto-expand the number of replicas based on the number of available nodes. 
		/// Set to a dash delimited lower and upper bound (e.g. 0-5) or use all for the upper bound (e.g. 0-all). Defaults to false (i.e. disabled). 
		/// </summary>
		//TODO SPECIAL TYPE FOR THIS INSTEAD OF JUST STRING
		string AutoExpandReplicas { get; set; }

		/// <summary>
		/// How often to perform a refresh operation, which makes recent changes to the index visible to search.
		/// Defaults to 1s. Can be set to -1 to disable refresh.
		/// </summary>
		TimeUnitExpression RefreshInterval { get; set; }

		/// <summary>
		/// Set to true to make the index and index metadata read only, false to allow writes and metadata changes.
		/// </summary>
		bool? BlocksReadOnly { get; set; }

		/// <summary>
		/// Set to true to disable read operations against the index.
		/// </summary>
		bool? BlocksRead { get; set; }

		/// <summary>
		/// Set to true to disable write operations against the index.
		/// </summary>
		bool? BlocksWrite { get; set; }

		/// <summary>
		/// Set to true to disable index metadata reads and writes.
		/// </summary>
		bool? BlocksMetadata { get; set; }

		/// <summary>
		/// Unallocated shards are recovered in order of priority when set
		/// </summary>
		int? Priority { get; set; }

		/// <summary>
		/// A primary shard is only recovered only if there are
		/// enough nodes available to allocate sufficient replicas to form a quorum.
		/// </summary>
		Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }

		/// <summary>
		/// The allocation of replica shards which become unassigned because a node has left can be 
		/// delayed with this dynamic setting, which defaults to 1m.
		/// </summary>
		TimeUnitExpression UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <summary>
		/// The maximum number of shards (replicas and primaries) that will be allocated to a single node. Defaults to unbounded.
		/// </summary>
		int? RoutingAllocationTotalShardsPerNode { get; set; }

		/// <summary>
		/// All of the settings exposed in the merge module are expert only and may be obsoleted in the future at any time!
		/// </summary>
		IMergeSettings Merge { get; set; }

		/// <summary>
		/// Configure logging thresholds and levels in elasticsearch for search/fetch and indexing
		/// </summary>
		ISlowLog SlowLog { get; set; }

		/// <summary>
		/// Configure translog settings, EXPERT MODE ONLY!
		/// </summary>
		ITranslogSettings Translog { get; set; }

	}

	public class DynamicIndexSettings : WrapDictionary<string, object>, IDynamicIndexSettings
	{
		public DynamicIndexSettings() : base() { }
		public DynamicIndexSettings(IDictionary<string, object> container) : base(container) { }
		public DynamicIndexSettings(Dictionary<string, object> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <inheritdoc/ >
		public string AutoExpandReplicas { get; set; }

		/// <inheritdoc/ >
		public bool? BlocksMetadata { get; set; }

		/// <inheritdoc/ >
		public bool? BlocksRead { get; set; }

		/// <inheritdoc/ >
		public bool? BlocksReadOnly { get; set; }
		
		/// <inheritdoc/ >
		public bool? BlocksWrite { get; set; }
		
		/// <inheritdoc/ >
		public int? Priority { get; set; }
		
		/// <inheritdoc/ >
		public IMergeSettings Merge { get; set; }
		
		/// <inheritdoc/ >
		public int? NumberOfReplicas { get; set; }
		
		/// <inheritdoc/ >
		public Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }
		
		/// <inheritdoc/ >
		public TimeUnitExpression RefreshInterval { get; set; }
		
		/// <inheritdoc/ >
		public int? RoutingAllocationTotalShardsPerNode { get; set; }
		
		/// <inheritdoc/ >
		public ISlowLog SlowLog { get; set; }
		
		/// <inheritdoc/ >
		public ITranslogSettings Translog { get; set; }
		
		/// <inheritdoc/ >
		public TimeUnitExpression UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(string setting, object value) => _backingDictionary.Add(setting, value);
	}
	
	public class DynamicIndexSettingsDescriptor : DynamicIndexSettingsDescriptor<DynamicIndexSettingsDescriptor>
	{

	}

	public abstract class DynamicIndexSettingsDescriptor<TIndexSettings> : WrapDictionary<string, object>, IDynamicIndexSettings
		where TIndexSettings : DynamicIndexSettingsDescriptor<TIndexSettings> 
	{
		protected TIndexSettings Assign(Action<IDynamicIndexSettings> assigner) =>
			Fluent.Assign((TIndexSettings)this, assigner);

		public DynamicIndexSettingsDescriptor() : base() { }
		protected DynamicIndexSettingsDescriptor(IDictionary<string, object> container) : base(container) { }

		int? IDynamicIndexSettings.NumberOfReplicas { get; set; }
		string IDynamicIndexSettings.AutoExpandReplicas { get; set; }
		bool? IDynamicIndexSettings.BlocksMetadata { get; set; }
		bool? IDynamicIndexSettings.BlocksRead { get; set; }
		bool? IDynamicIndexSettings.BlocksReadOnly { get; set; }
		bool? IDynamicIndexSettings.BlocksWrite { get; set; }
		int? IDynamicIndexSettings.Priority { get; set; }
		IMergeSettings IDynamicIndexSettings.Merge { get; set; }
		Union<int, RecoveryInitialShards> IDynamicIndexSettings.RecoveryInitialShards { get; set; }
		TimeUnitExpression IDynamicIndexSettings.RefreshInterval { get; set; }
		int? IDynamicIndexSettings.RoutingAllocationTotalShardsPerNode { get; set; }
		ISlowLog IDynamicIndexSettings.SlowLog { get; set; }
		ITranslogSettings IDynamicIndexSettings.Translog { get; set; }
		TimeUnitExpression IDynamicIndexSettings.UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public TIndexSettings Add(string setting, object value)
		{
			_backingDictionary.Add(setting, value);
			return (TIndexSettings)this;
		}

		/// <inheritdoc/ >
		public TIndexSettings NumberOfReplicas(int? numberOfReplicas) =>
			Assign(a => a.NumberOfReplicas = numberOfReplicas);

		/// <inheritdoc/ >
		public TIndexSettings AutoExpandReplicas(string AutoExpandReplicas) =>
			Assign(a => a.AutoExpandReplicas = AutoExpandReplicas);

		/// <inheritdoc/ >
		public TIndexSettings BlocksMetadata(bool? blocksMetadata = true) =>
			Assign(a => a.BlocksMetadata = blocksMetadata);

		/// <inheritdoc/ >
		public TIndexSettings BlocksRead(bool? blocksRead = true) =>
			Assign(a => a.BlocksRead = blocksRead);

		/// <inheritdoc/ >
		public TIndexSettings BlocksReadOnly(bool? blocksReadOnly = true) =>
			Assign(a => a.BlocksReadOnly = blocksReadOnly);

		/// <inheritdoc/ >
		public TIndexSettings BlocksWrite(bool? blocksWrite = true) =>
			Assign(a => a.BlocksWrite = blocksWrite);

		/// <inheritdoc/ >
		public TIndexSettings Priority(int? priority) =>
			Assign(a => a.Priority = priority);

		/// <inheritdoc/ >
		public TIndexSettings Merge(Func<MergeSettingsDescriptor, IMergeSettings> merge) =>
			Assign(a => a.Merge = merge?.Invoke(new MergeSettingsDescriptor()));

		/// <inheritdoc/ >
		public TIndexSettings RecoveryInitialShards(Union<int, RecoveryInitialShards> initialShards) =>
			Assign(a => a.RecoveryInitialShards = initialShards);

		/// <inheritdoc/ >
		public TIndexSettings RefreshInterval(TimeUnitExpression time) =>
			Assign(a => a.RefreshInterval = time);

		/// <inheritdoc/ >
		public TIndexSettings TotalShardsPerNode(int? totalShardsPerNode) =>
			Assign(a => a.RoutingAllocationTotalShardsPerNode = totalShardsPerNode);

		/// <inheritdoc/ >
		public TIndexSettings SlowLog(Func<SlowLogDescriptor, ISlowLog> slowLogSelector) =>
			Assign(a => a.SlowLog = slowLogSelector?.Invoke(new SlowLogDescriptor()));

		/// <inheritdoc/ >
		public TIndexSettings Translog(Func<TranslogSettingsDescriptor, ITranslogSettings> translogSelector) =>
			Assign(a => a.Translog = translogSelector?.Invoke(new TranslogSettingsDescriptor()));

		/// <inheritdoc/ >
		public TIndexSettings UnassignedNodeLeftDelayedTimeout(TimeUnitExpression time) =>
			Assign(a => a.UnassignedNodeLeftDelayedTimeout = time);

		//TODO DSL for shard allocation filtering

	}

}