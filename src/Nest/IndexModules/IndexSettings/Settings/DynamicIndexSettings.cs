using System;
using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// Dynamic index settings
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(DynamicIndexSettingsFormatter))]
	public interface IDynamicIndexSettings : IIsADictionary<string, object>
	{
		/// <summary>
		/// Configure analysis
		/// </summary>
		IAnalysis Analysis { get; set; }

		/// <summary>
		/// Auto-expand the number of replicas based on the number of available nodes.
		///  Set to a dash delimited lower and upper bound (e.g. 0-5) or use all for the upper bound (e.g. 0-all). Defaults to false (i.e. disabled).
		/// </summary>
		AutoExpandReplicas AutoExpandReplicas { get; set; }

		/// <summary>
		/// Set to true to disable index metadata reads and writes.
		/// </summary>
		bool? BlocksMetadata { get; set; }

		/// <summary>
		/// Set to true to disable read operations against the index.
		/// </summary>
		bool? BlocksRead { get; set; }

		/// <summary>
		/// Set to true to make the index and index metadata read only, false to allow writes and metadata changes.
		/// </summary>
		bool? BlocksReadOnly { get; set; }

		/// <summary>
		/// Set to true to disable write operations against the index.
		/// </summary>
		bool? BlocksWrite { get; set; }

		/// <summary>
		/// All of the settings exposed in the merge module are expert only and may be obsoleted in the future at any time!
		/// </summary>
		IMergeSettings Merge { get; set; }

		/// <summary>
		/// The number of replicas each primary shard has. Defaults to 1.
		/// </summary>
		int? NumberOfReplicas { get; set; }

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
		/// How often to perform a refresh operation, which makes recent changes to the index visible to search.
		/// Defaults to 1s. Can be set to -1 to disable refresh.
		/// </summary>
		Time RefreshInterval { get; set; }

		/// <summary>
		/// Enables the shard-level request cache. Not enabled by default.
		/// </summary>
		bool? RequestsCacheEnabled { get; set; }

		/// <summary>
		/// The maximum number of shards (replicas and primaries) that will be allocated to a single node. Defaults to unbounded.
		/// </summary>
		int? RoutingAllocationTotalShardsPerNode { get; set; }

		/// <summary>
		/// Configure similarity
		/// </summary>
		ISimilarities Similarity { get; set; }

		/// <summary>
		/// Configure logging thresholds and levels in elasticsearch for search/fetch and indexing
		/// </summary>
		ISlowLog SlowLog { get; set; }

		/// <summary>
		/// Configure translog settings. This should only be used by experts who know what they're doing
		/// </summary>
		ITranslogSettings Translog { get; set; }

		/// <summary>
		/// The allocation of replica shards which become unassigned because a node has left can be
		/// delayed with this dynamic setting, which defaults to 1m.
		/// </summary>
		Time UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <summary>
		/// The default ingest node pipeline for this index. Index requests will fail if the default pipeline is set and
		/// the pipeline does not exist. The default may be overridden using the pipeline parameter.
		/// The special pipeline name _none indicates no ingest pipeline should be run.`
		/// </summary>
		string DefaultPipeline { get; set; }
	}

	/// <inheritdoc />
	public class DynamicIndexSettings : IsADictionaryBase<string, object>, IDynamicIndexSettings
	{
		private Time _refreshInterval;

		public DynamicIndexSettings() { }

		public DynamicIndexSettings(IDictionary<string, object> container) : base(container) { }

		/// <inheritdoc cref="IDynamicIndexSettings.Analysis" />
		public IAnalysis Analysis { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.AutoExpandReplicas" />
		public AutoExpandReplicas AutoExpandReplicas { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksMetadata" />
		public bool? BlocksMetadata { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksRead" />
		public bool? BlocksRead { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksReadOnly" />
		public bool? BlocksReadOnly { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksWrite" />
		public bool? BlocksWrite { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.Merge" />
		public IMergeSettings Merge { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.NumberOfReplicas" />
		public int? NumberOfReplicas { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.Priority" />
		public int? Priority { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.RecoveryInitialShards" />
		public Union<int, RecoveryInitialShards> RecoveryInitialShards { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.RefreshInterval" />
		public Time RefreshInterval
		{
			get => _refreshInterval;
			set
			{
				BackingDictionary[UpdatableIndexSettings.RefreshInterval] = value;
				_refreshInterval = value;
			}
		}

		/// <inheritdoc cref="IDynamicIndexSettings.RequestsCacheEnabled" />
		public bool? RequestsCacheEnabled { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.RoutingAllocationTotalShardsPerNode" />
		public int? RoutingAllocationTotalShardsPerNode { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.Similarity" />
		public ISimilarities Similarity { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.SlowLog" />
		public ISlowLog SlowLog { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.Translog" />
		public ITranslogSettings Translog { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.UnassignedNodeLeftDelayedTimeout" />
		public Time UnassignedNodeLeftDelayedTimeout { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.DefaultPipeline" />
		public string DefaultPipeline { get; set; }

		/// <summary> Add any setting to the index </summary>
		public void Add(string setting, object value) => BackingDictionary[setting] = value;
	}

	/// <inheritdoc cref="IDynamicIndexSettings" />
	public class DynamicIndexSettingsDescriptor : DynamicIndexSettingsDescriptorBase<DynamicIndexSettingsDescriptor, DynamicIndexSettings>
	{
		public DynamicIndexSettingsDescriptor() : base(new DynamicIndexSettings()) { }
	}

	/// <summary>Base descriptor implementation for dynamic index settings</summary>
	public abstract class DynamicIndexSettingsDescriptorBase<TDescriptor, TIndexSettings>
		: IsADictionaryDescriptorBase<TDescriptor, TIndexSettings, string, object>
		where TDescriptor : DynamicIndexSettingsDescriptorBase<TDescriptor, TIndexSettings>
		where TIndexSettings : class, IDynamicIndexSettings
	{
		protected DynamicIndexSettingsDescriptorBase(TIndexSettings instance) : base(instance) { }

		/// <inheritdoc cref="DynamicIndexSettings.Add" />
		public TDescriptor Setting(string setting, object value)
		{
			PromisedValue[setting] = value;
			return (TDescriptor)this;
		}

		/// <inheritdoc cref="IDynamicIndexSettings.NumberOfReplicas" />
		public TDescriptor NumberOfReplicas(int? numberOfReplicas) => Assign(a => a.NumberOfReplicas = numberOfReplicas);

		/// <inheritdoc cref="IDynamicIndexSettings.AutoExpandReplicas" />
		public TDescriptor AutoExpandReplicas(AutoExpandReplicas autoExpandReplicas) => Assign(a => a.AutoExpandReplicas = autoExpandReplicas);

		/// <inheritdoc cref="IDynamicIndexSettings.DefaultPipeline" />
		public TDescriptor DefaultPipeline(string defaultPipeline) => Assign(a => a.DefaultPipeline = defaultPipeline);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksMetadata" />
		public TDescriptor BlocksMetadata(bool? blocksMetadata = true) => Assign(a => a.BlocksMetadata = blocksMetadata);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksRead" />
		public TDescriptor BlocksRead(bool? blocksRead = true) => Assign(a => a.BlocksRead = blocksRead);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksReadOnly" />
		public TDescriptor BlocksReadOnly(bool? blocksReadOnly = true) => Assign(a => a.BlocksReadOnly = blocksReadOnly);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksWrite" />
		public TDescriptor BlocksWrite(bool? blocksWrite = true) => Assign(a => a.BlocksWrite = blocksWrite);

		/// <inheritdoc cref="IDynamicIndexSettings.Priority" />
		public TDescriptor Priority(int? priority) => Assign(a => a.Priority = priority);

		/// <inheritdoc cref="IDynamicIndexSettings.Merge" />
		public TDescriptor Merge(Func<MergeSettingsDescriptor, IMergeSettings> merge) =>
			Assign(a => a.Merge = merge?.Invoke(new MergeSettingsDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.RecoveryInitialShards" />
		public TDescriptor RecoveryInitialShards(Union<int, RecoveryInitialShards> initialShards) =>
			Assign(a => a.RecoveryInitialShards = initialShards);

		/// <inheritdoc cref="IDynamicIndexSettings.RequestsCacheEnabled" />
		public TDescriptor RequestsCacheEnabled(bool? enable = true) =>
			Assign(a => a.RequestsCacheEnabled = enable);

		/// <inheritdoc cref="IDynamicIndexSettings.RefreshInterval" />
		public TDescriptor RefreshInterval(Time time) => Assign(a => a.RefreshInterval = time);

		// TODO: align name for 7.x
		/// <inheritdoc cref="IDynamicIndexSettings.RoutingAllocationTotalShardsPerNode" />
		public TDescriptor TotalShardsPerNode(int? totalShardsPerNode) =>
			Assign(a => a.RoutingAllocationTotalShardsPerNode = totalShardsPerNode);

		/// <inheritdoc cref="IDynamicIndexSettings.SlowLog" />
		public TDescriptor SlowLog(Func<SlowLogDescriptor, ISlowLog> slowLogSelector) =>
			Assign(a => a.SlowLog = slowLogSelector?.Invoke(new SlowLogDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.Translog" />
		public TDescriptor Translog(Func<TranslogSettingsDescriptor, ITranslogSettings> translogSelector) =>
			Assign(a => a.Translog = translogSelector?.Invoke(new TranslogSettingsDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.UnassignedNodeLeftDelayedTimeout" />
		public TDescriptor UnassignedNodeLeftDelayedTimeout(Time time) =>
			Assign(a => a.UnassignedNodeLeftDelayedTimeout = time);

		/// <inheritdoc cref="IDynamicIndexSettings.Analysis" />
		public TDescriptor Analysis(Func<AnalysisDescriptor, IAnalysis> selector) =>
			Assign(a => a.Analysis = selector?.Invoke(new AnalysisDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.Similarity" />
		public TDescriptor Similarity(Func<SimilaritiesDescriptor, IPromise<ISimilarities>> selector) =>
			Assign(a => a.Similarity = selector?.Invoke(new SimilaritiesDescriptor())?.Value);
	}
}
