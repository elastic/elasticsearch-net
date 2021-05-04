// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;

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
		/// Set to true to disable read operations, but allow delete operations, against the index.
		/// </summary>
		bool? BlocksReadOnlyAllowDelete { get; set; }

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
		/// Configure logging thresholds and levels in Elasticsearch for search/fetch and indexing
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

		/// <summary>
		/// The required ingest pipeline for this index. Index requests will fail if the required pipeline is set and the pipeline
		/// does not exist. The required pipeline can not be overridden with the pipeline parameter. A default pipeline and a required pipeline
		/// can not both be set. The special pipeline name _none indicates no ingest pipeline will run.
		/// </summary>
		[Obsolete("Use FinalPipeline")]
		string RequiredPipeline { get; set; }

		/// <summary>
		/// The final ingest pipeline for this index. Index requests will fail if the final pipeline is set and the pipeline does not exist.
		/// The final pipeline always runs after the request pipeline (if specified) and the default pipeline (if it exists). The special pipeline
		/// name `_none` indicates no ingest pipeline will run.
		/// </summary>
		string FinalPipeline { get; set; }
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

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksReadOnlyAllowDelete" />
		public bool? BlocksReadOnlyAllowDelete { get; set; }

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

		/// <inheritdoc cref="IDynamicIndexSettings.RequiredPipeline" />
		[Obsolete("Use FinalPipeline")]
		public string RequiredPipeline { get; set; }

		/// <inheritdoc cref="IDynamicIndexSettings.FinalPipeline" />
		public string FinalPipeline { get; set; }

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
		public TDescriptor NumberOfReplicas(int? numberOfReplicas) => Assign(numberOfReplicas, (a, v) => a.NumberOfReplicas = v);

		/// <inheritdoc cref="IDynamicIndexSettings.AutoExpandReplicas" />
		public TDescriptor AutoExpandReplicas(AutoExpandReplicas autoExpandReplicas) => Assign(autoExpandReplicas, (a, v) => a.AutoExpandReplicas = v);

		/// <inheritdoc cref="IDynamicIndexSettings.DefaultPipeline" />
		public TDescriptor DefaultPipeline(string defaultPipeline) => Assign(defaultPipeline, (a, v) => a.DefaultPipeline = v);

		/// <inheritdoc cref="IDynamicIndexSettings.RequiredPipeline" />
		[Obsolete("Use FinalPipeline")]
		public TDescriptor RequiredPipeline(string requiredPipeline) => Assign(requiredPipeline, (a, v) => a.RequiredPipeline = v);

		/// <inheritdoc cref="IDynamicIndexSettings.RequiredPipeline" />
		public TDescriptor FinalPipeline(string finalPipeline) => Assign(finalPipeline, (a, v) => a.FinalPipeline = v);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksMetadata" />
		public TDescriptor BlocksMetadata(bool? blocksMetadata = true) => Assign(blocksMetadata, (a, v) => a.BlocksMetadata = v);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksRead" />
		public TDescriptor BlocksRead(bool? blocksRead = true) => Assign(blocksRead, (a, v) => a.BlocksRead = v);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksReadOnly" />
		public TDescriptor BlocksReadOnly(bool? blocksReadOnly = true) => Assign(blocksReadOnly, (a, v) => a.BlocksReadOnly = v);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksWrite" />
		public TDescriptor BlocksWrite(bool? blocksWrite = true) => Assign(blocksWrite, (a, v) => a.BlocksWrite = v);

		/// <inheritdoc cref="IDynamicIndexSettings.BlocksReadOnlyAllowDelete" />
		public TDescriptor BlocksReadOnlyAllowDelete(bool? blocksReadOnlyAllowDelete = true) => Assign(blocksReadOnlyAllowDelete, (a, v) => a.BlocksReadOnlyAllowDelete = v);

		/// <inheritdoc cref="IDynamicIndexSettings.Priority" />
		public TDescriptor Priority(int? priority) => Assign(priority, (a, v) => a.Priority = v);

		/// <inheritdoc cref="IDynamicIndexSettings.Merge" />
		public TDescriptor Merge(Func<MergeSettingsDescriptor, IMergeSettings> merge) =>
			Assign(merge, (a, v) => a.Merge = v?.Invoke(new MergeSettingsDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.RecoveryInitialShards" />
		public TDescriptor RecoveryInitialShards(Union<int, RecoveryInitialShards> initialShards) =>
			Assign(initialShards, (a, v) => a.RecoveryInitialShards = v);

		/// <inheritdoc cref="IDynamicIndexSettings.RequestsCacheEnabled" />
		public TDescriptor RequestsCacheEnabled(bool? enable = true) =>
			Assign(enable, (a, v) => a.RequestsCacheEnabled = v);

		/// <inheritdoc cref="IDynamicIndexSettings.RefreshInterval" />
		public TDescriptor RefreshInterval(Time time) => Assign(time, (a, v) => a.RefreshInterval = v);

		/// <inheritdoc cref="IDynamicIndexSettings.RoutingAllocationTotalShardsPerNode" />
		public TDescriptor RoutingAllocationTotalShardsPerNode(int? totalShardsPerNode) =>
			Assign(totalShardsPerNode, (a, v) => a.RoutingAllocationTotalShardsPerNode = v);

		/// <inheritdoc cref="IDynamicIndexSettings.SlowLog" />
		public TDescriptor SlowLog(Func<SlowLogDescriptor, ISlowLog> slowLogSelector) =>
			Assign(slowLogSelector, (a, v) => a.SlowLog = v?.Invoke(new SlowLogDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.Translog" />
		public TDescriptor Translog(Func<TranslogSettingsDescriptor, ITranslogSettings> translogSelector) =>
			Assign(translogSelector, (a, v) => a.Translog = v?.Invoke(new TranslogSettingsDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.UnassignedNodeLeftDelayedTimeout" />
		public TDescriptor UnassignedNodeLeftDelayedTimeout(Time time) =>
			Assign(time, (a, v) => a.UnassignedNodeLeftDelayedTimeout = v);

		/// <inheritdoc cref="IDynamicIndexSettings.Analysis" />
		public TDescriptor Analysis(Func<AnalysisDescriptor, IAnalysis> selector) =>
			Assign(selector, (a, v) => a.Analysis = v?.Invoke(new AnalysisDescriptor()));

		/// <inheritdoc cref="IDynamicIndexSettings.Similarity" />
		public TDescriptor Similarity(Func<SimilaritiesDescriptor, IPromise<ISimilarities>> selector) =>
			Assign(selector, (a, v) => a.Similarity = v?.Invoke(new SimilaritiesDescriptor())?.Value);
	}
}
