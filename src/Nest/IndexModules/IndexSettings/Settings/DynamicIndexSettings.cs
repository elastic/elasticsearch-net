using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(IndexSettingsConverter))]
	public interface IDynamicIndexSettings : IIsADictionary<string, object>
	{
		/// <summary>
		///The number of replicas each primary shard has. Defaults to 1.
		/// </summary>
		int? NumberOfReplicas { get; set; }

		/// <summary>
		///Auto-expand the number of replicas based on the number of available nodes.
		/// Set to a dash delimited lower and upper bound (e.g. 0-5) or use all for the upper bound (e.g. 0-all). Defaults to false (i.e. disabled).
		/// </summary>
		AutoExpandReplicas AutoExpandReplicas { get; set; }

		/// <summary>
		/// How often to perform a refresh operation, which makes recent changes to the index visible to search.
		/// Defaults to 1s. Can be set to -1 to disable refresh.
		/// </summary>
		Time RefreshInterval { get; set; }

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
		[JsonConverter(typeof(UnionJsonConverter))]
		Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }

		/// <summary>
		/// Enables the shard-level request cache. Not enabled by default.
		/// </summary>
		bool? RequestsCacheEnabled { get; set; }

		/// <summary>
		/// The allocation of replica shards which become unassigned because a node has left can be
		/// delayed with this dynamic setting, which defaults to 1m.
		/// </summary>
		Time UnassignedNodeLeftDelayedTimeout { get; set; }

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
		/// Configure translog settings. This should only be used by experts who know what they're doing
		/// </summary>
		ITranslogSettings Translog { get; set; }

		/// <summary>
		/// Configure analysis
		/// </summary>
		IAnalysis Analysis { get; set; }
	}

	public class DynamicIndexSettings : IsADictionaryBase<string, object>, IDynamicIndexSettings
	{
		public DynamicIndexSettings() { }

		public DynamicIndexSettings(IDictionary<string, object> container) : base(container) { }

		/// <inheritdoc/>
		public int? NumberOfReplicas { get; set; }

		/// <inheritdoc/>
		public AutoExpandReplicas AutoExpandReplicas { get; set; }

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
		public bool? RequestsCacheEnabled { get; set; }

		/// <inheritdoc/>
		public IMergeSettings Merge { get; set; }

		/// <inheritdoc/>
		public Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }

		/// <inheritdoc/>
		public Time RefreshInterval { get; set; }

		/// <inheritdoc/>
		public int? RoutingAllocationTotalShardsPerNode { get; set; }

		/// <inheritdoc/>
		public ISlowLog SlowLog { get; set; }

		/// <inheritdoc/>
		public ITranslogSettings Translog { get; set; }

		/// <inheritdoc/>
		public Time UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <inheritdoc/>
		public IAnalysis Analysis { get; set; }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public void Add(string setting, object value) => this.BackingDictionary.Add(setting, value);

	}

	public class DynamicIndexSettingsDescriptor :
		DynamicIndexSettingsDescriptorBase<DynamicIndexSettingsDescriptor, DynamicIndexSettings>
	{
		public DynamicIndexSettingsDescriptor() : base(new DynamicIndexSettings()) { }
	}

	public abstract class DynamicIndexSettingsDescriptorBase<TDescriptor, TIndexSettings> : IsADictionaryDescriptorBase<TDescriptor, TIndexSettings, string, object>
		where TDescriptor : DynamicIndexSettingsDescriptorBase<TDescriptor, TIndexSettings>
		where TIndexSettings : class, IDynamicIndexSettings
	{
		protected DynamicIndexSettingsDescriptorBase(TIndexSettings instance) : base(instance) { }

		/// <summary>
		/// Add any setting to the index
		/// </summary>
		public TDescriptor Setting(string setting, object value)
		{
			this.PromisedValue.Add(setting, value);
			return (TDescriptor)this;
		}

		/// <inheritdoc/>
		public TDescriptor NumberOfReplicas(int? numberOfReplicas) => Assign(a => a.NumberOfReplicas = numberOfReplicas);

		/// <inheritdoc/>
		public TDescriptor AutoExpandReplicas(AutoExpandReplicas autoExpandReplicas) => Assign(a => a.AutoExpandReplicas = autoExpandReplicas);

		/// <inheritdoc/>
		public TDescriptor BlocksMetadata(bool? blocksMetadata = true) => Assign(a => a.BlocksMetadata = blocksMetadata);

		/// <inheritdoc/>
		public TDescriptor BlocksRead(bool? blocksRead = true) => Assign(a => a.BlocksRead = blocksRead);

		/// <inheritdoc/>
		public TDescriptor BlocksReadOnly(bool? blocksReadOnly = true) => Assign(a => a.BlocksReadOnly = blocksReadOnly);

		/// <inheritdoc/>
		public TDescriptor BlocksWrite(bool? blocksWrite = true) => Assign(a => a.BlocksWrite = blocksWrite);

		/// <inheritdoc/>
		public TDescriptor Priority(int? priority) => Assign(a => a.Priority = priority);

		/// <inheritdoc/>
		public TDescriptor Merge(Func<MergeSettingsDescriptor, IMergeSettings> merge) =>
			Assign(a => a.Merge = merge?.Invoke(new MergeSettingsDescriptor()));

		/// <inheritdoc/>
		public TDescriptor RecoveryInitialShards(Union<int, RecoveryInitialShards> initialShards) =>
			Assign(a => a.RecoveryInitialShards = initialShards);

		/// <inheritdoc/>
		public TDescriptor RequestsCacheEnabled(bool? enable = true) =>
			Assign(a => a.RequestsCacheEnabled = enable);

		/// <inheritdoc/>
		public TDescriptor RefreshInterval(Time time) => Assign(a => a.RefreshInterval = time);

		/// <inheritdoc/>
		public TDescriptor TotalShardsPerNode(int? totalShardsPerNode) =>
			Assign(a => a.RoutingAllocationTotalShardsPerNode = totalShardsPerNode);

		/// <inheritdoc/>
		public TDescriptor SlowLog(Func<SlowLogDescriptor, ISlowLog> slowLogSelector) =>
			Assign(a => a.SlowLog = slowLogSelector?.Invoke(new SlowLogDescriptor()));

		/// <inheritdoc/>
		public TDescriptor Translog(Func<TranslogSettingsDescriptor, ITranslogSettings> translogSelector) =>
			Assign(a => a.Translog = translogSelector?.Invoke(new TranslogSettingsDescriptor()));

		/// <inheritdoc/>
		public TDescriptor UnassignedNodeLeftDelayedTimeout(Time time) =>
			Assign(a => a.UnassignedNodeLeftDelayedTimeout = time);

		public TDescriptor Analysis(Func<AnalysisDescriptor, IAnalysis> selector) =>
			Assign(a => a.Analysis = selector?.Invoke(new AnalysisDescriptor()));
	}
}
