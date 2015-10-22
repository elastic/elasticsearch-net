using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(IndexSettingsConverter))]
	public interface IDynamicIndexSettings : IHasADictionary
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
		/// Index warmup can be disabled by setting index.warmer.enabled to false. This can be handy when 
		/// doing initial bulk indexing: disable pre registered warmers to make indexing faster 
		/// and less expensive and then enable it.
		/// </summary>
		bool? WarmersEnabled { get; set; }

		/// <summary>
		/// When a search request is run against an index or against many indices, each involved shard executes the search locally and
	   ///  returns its local results to the coordinating node, which combines these shard-level results into a “global” result set.
		///<para>
		/// The shard-level request cache module caches the local results on each shard.This allows frequently used 
		/// (and potentially heavy) search requests to return results almost instantly.</para>
		/// </summary>
		bool? RequestCacheEnabled { get; set; }

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

		IAnalysis Analysis { get; set; }
	}

	public class DynamicIndexSettings : IsADictionary<string, object>, IDynamicIndexSettings
	{
		public DynamicIndexSettings() : base() { }
		public DynamicIndexSettings(IDictionary<string, object> container) : base(container) { }
		public DynamicIndexSettings(Dictionary<string, object> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value))
		{ }

		/// <inheritdoc/>
		public string AutoExpandReplicas { get; set; }

		/// <inheritdoc/>
		public bool? BlocksMetadata { get; set; }

		/// <inheritdoc/>
		public bool? BlocksRead { get; set; }

		/// <inheritdoc/>
		public bool? BlocksReadOnly { get; set; }
		
		/// <inheritdoc/>
		public bool? BlocksWrite { get; set; }
		
		/// <inheritdoc/>
		public int? Priority { get; set; }
		
		/// <inheritdoc/>
		public bool? WarmersEnabled { get; set; }
		
		/// <inheritdoc/>
		public bool? RequestCacheEnabled { get; set; }
		
		/// <inheritdoc/>
		public IMergeSettings Merge { get; set; }
		
		/// <inheritdoc/>
		public int? NumberOfReplicas { get; set; }
		
		/// <inheritdoc/>
		public Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }
		
		/// <inheritdoc/>
		public TimeUnitExpression RefreshInterval { get; set; }
		
		/// <inheritdoc/>
		public int? RoutingAllocationTotalShardsPerNode { get; set; }
		
		/// <inheritdoc/>
		public ISlowLog SlowLog { get; set; }
		
		/// <inheritdoc/>
		public ITranslogSettings Translog { get; set; }
		
		/// <inheritdoc/>
		public TimeUnitExpression UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <inheritdoc/>
		public IAnalysis Analysis { get; set; }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(string setting, object value) => this.BackingDictionary.Add(setting, value);

	}
	
	public class DynamicIndexSettingsDescriptor : DynamicIndexSettingsDescriptor<DynamicIndexSettingsDescriptor>
	{

	}

	public abstract class DynamicIndexSettingsDescriptor<TIndexSettings> 
		: IsADictionaryDescriptor<DynamicIndexSettingsDescriptor<TIndexSettings>, IDynamicIndexSettings, string, object>, IDynamicIndexSettings
		where TIndexSettings : DynamicIndexSettingsDescriptor<TIndexSettings> 
	{
		protected TIndexSettings Set(Action<IDynamicIndexSettings> assigner) => Fluent.Assign((TIndexSettings)this, assigner);

		public DynamicIndexSettingsDescriptor() : base() { }
		protected DynamicIndexSettingsDescriptor(IDictionary<string, object> container) : base(container) { }

		int? IDynamicIndexSettings.NumberOfReplicas { get; set; }
		string IDynamicIndexSettings.AutoExpandReplicas { get; set; }
		bool? IDynamicIndexSettings.BlocksMetadata { get; set; }
		bool? IDynamicIndexSettings.BlocksRead { get; set; }
		bool? IDynamicIndexSettings.BlocksReadOnly { get; set; }
		bool? IDynamicIndexSettings.BlocksWrite { get; set; }
		int? IDynamicIndexSettings.Priority { get; set; }
		bool? IDynamicIndexSettings.WarmersEnabled { get; set; }
		bool? IDynamicIndexSettings.RequestCacheEnabled { get; set; }
		IMergeSettings IDynamicIndexSettings.Merge { get; set; }
		Union<int, RecoveryInitialShards> IDynamicIndexSettings.RecoveryInitialShards { get; set; }
		TimeUnitExpression IDynamicIndexSettings.RefreshInterval { get; set; }
		int? IDynamicIndexSettings.RoutingAllocationTotalShardsPerNode { get; set; }
		ISlowLog IDynamicIndexSettings.SlowLog { get; set; }
		ITranslogSettings IDynamicIndexSettings.Translog { get; set; }
		TimeUnitExpression IDynamicIndexSettings.UnassignedNodeLeftDelayedTimeout { get; set; }
		IAnalysis IDynamicIndexSettings.Analysis { get; set; }
		
		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public TIndexSettings Setting(string setting, object value)
		{
			this.BackingDictionary.Add(setting, value);
			return (TIndexSettings)this;
		}

		/// <inheritdoc/>
		public TIndexSettings NumberOfReplicas(int? numberOfReplicas) => Set(a => a.NumberOfReplicas = numberOfReplicas);

		/// <inheritdoc/>
		public TIndexSettings AutoExpandReplicas(string AutoExpandReplicas) => Set(a => a.AutoExpandReplicas = AutoExpandReplicas);

		/// <inheritdoc/>
		public TIndexSettings BlocksMetadata(bool? blocksMetadata = true) => Set(a => a.BlocksMetadata = blocksMetadata);

		/// <inheritdoc/>
		public TIndexSettings BlocksRead(bool? blocksRead = true) => Set(a => a.BlocksRead = blocksRead);

		/// <inheritdoc/>
		public TIndexSettings BlocksReadOnly(bool? blocksReadOnly = true) => Set(a => a.BlocksReadOnly = blocksReadOnly);

		/// <inheritdoc/>
		public TIndexSettings BlocksWrite(bool? blocksWrite = true) => Set(a => a.BlocksWrite = blocksWrite);

		/// <inheritdoc/>
		public TIndexSettings Priority(int? priority) => Set(a => a.Priority = priority);

		/// <inheritdoc/>
		public TIndexSettings WarmersEnabled(bool enabled = true) => Set(a => a.WarmersEnabled = enabled);

		/// <inheritdoc/>
		public TIndexSettings RequestCacheEnabled(bool enabled = true) => Set(a => a.RequestCacheEnabled = enabled);

		/// <inheritdoc/>
		public TIndexSettings Merge(Func<MergeSettingsDescriptor, IMergeSettings> merge) =>
			Set(a => a.Merge = merge?.Invoke(new MergeSettingsDescriptor()));

		/// <inheritdoc/>
		public TIndexSettings RecoveryInitialShards(Union<int, RecoveryInitialShards> initialShards) =>
			Set(a => a.RecoveryInitialShards = initialShards);

		/// <inheritdoc/>
		public TIndexSettings RefreshInterval(TimeUnitExpression time) => Set(a => a.RefreshInterval = time);

		/// <inheritdoc/>
		public TIndexSettings TotalShardsPerNode(int? totalShardsPerNode) =>
			Set(a => a.RoutingAllocationTotalShardsPerNode = totalShardsPerNode);

		/// <inheritdoc/>
		public TIndexSettings SlowLog(Func<SlowLogDescriptor, ISlowLog> slowLogSelector) =>
			Set(a => a.SlowLog = slowLogSelector?.Invoke(new SlowLogDescriptor()));

		/// <inheritdoc/>
		public TIndexSettings Translog(Func<TranslogSettingsDescriptor, ITranslogSettings> translogSelector) =>
			Set(a => a.Translog = translogSelector?.Invoke(new TranslogSettingsDescriptor()));

		/// <inheritdoc/>
		public TIndexSettings UnassignedNodeLeftDelayedTimeout(TimeUnitExpression time) =>
			Set(a => a.UnassignedNodeLeftDelayedTimeout = time);

		//TODO DSL for shard allocation filtering

		public TIndexSettings Analysis(Func<AnalysisDescriptor, IAnalysis> selector) =>
			Set(a => a.Analysis = selector?.Invoke(new AnalysisDescriptor()));
	}

}