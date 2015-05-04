using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{ 
	public class SettingNames
	{
		public const string NumberOfReplicas = "index.number_of_replicas";
		public const string AutoExpandReplicas = "index.auto_expand_replicas";
		public const string BlocksReadOnly = "index.blocks.read_only";
		public const string BlocksRead = "index.blocks.read";
		public const string BlocksWrite = "index.blocks.write";
		public const string BlocksMetadata = "index.blocks.metadata";
		public const string RefreshInterval = "index.refresh_interval";
		public const string IndexConcurrency = "index.index_concurrency";
		public const string Codec = "index.codec";
		public const string CodecBloomLoad = "index.codec.bloom.load";
		public const string FailOnMergeFailure = "index.fail_on_merge_failure";
		public const string TranslogFlushTreshHoldOps = "index.translog.flush_threshold_ops";
		public const string TranslogFlushThresholdSize = "index.translog.flush_threshold_size";
		public const string TranslogFlushThresholdPeriod = "index.translog.flush_threshold_period";
		public const string TranslogDisableFlush = "index.translog.disable_flush";
		public const string CacheFilterMaxSize = "index.cache.filter.max_size";
		public const string CacheFilterExpire = "index.cache.filter.expire";
		public const string CacheQueryEnable = "index.cache.query.enable";
		public const string GatewaySnapshotInterval = "index.gateway.snapshot_interval";
		public const string RoutingAllocationInclude = "index.routing.allocation.include";
		public const string RoutingAllocationExclude = "index.routing.allocation.exclude";
		public const string RoutingAllocationRequire = "index.routing.allocation.require";
		public const string RoutingAllocationEnable = "index.routing.allocation.enable";
		public const string RoutingAllocationDisableAllication = "index.routing.allocation.disable_allocation";
		public const string RoutingAllocationDisableNewAllocation = "index.routing.allocation.disable_new_allocation";
		public const string RoutingAllocationDisableReplicaAllocation = "index.routing.allocation.disable_replica_allocation";
		public const string RoutingAllocationTotalShardsPerNode = "index.routing.allocation.total_shards_per_node";
		public const string RecoveryInitialShards = "index.recovery.initial_shards";
		public const string GcDeletes = "index.gc_deletes";
		public const string TtlDisablePurge = "index.ttl.disable_purge";
		public const string TranslogFsType = "index.translog.fs.type";
		public const string CompoundFormat = "index.compound_format";
		public const string CompoundOnFlush = "index.compound_on_flush";
		public const string WarmersEnabled = "index.warmer.enabled";
		public const string Analysis = "analysis";
	}

	public interface IUpdateSettingsRequest : IIndexOptionalPath<UpdateSettingsRequestParameters>
	{
		[JsonProperty(SettingNames.NumberOfReplicas)]
		int? NumberOfReplicas { get; set; }

		[JsonProperty(SettingNames.AutoExpandReplicas)]
		object AutoExpandReplicas { get; set; }

		[JsonProperty(SettingNames.BlocksReadOnly)]
		bool? BlocksReadOnly { get; set; }

		[JsonProperty(SettingNames.BlocksRead)]
		bool? BlocksRead { get; set; }

		[JsonProperty(SettingNames.BlocksWrite)]
		bool? BlocksWrite { get; set; }

		[JsonProperty(SettingNames.BlocksMetadata)]
		bool? BlocksMetadata { get; set; }

		[JsonProperty(SettingNames.RefreshInterval)]
		string RefreshInterval { get; set; }

		[JsonProperty(SettingNames.IndexConcurrency)]
		int? IndexConcurrency { get; set; }

		[JsonProperty(SettingNames.Codec)]
		string Codec { get; set; }

		[JsonProperty(SettingNames.CodecBloomLoad)]
		bool? CodecBloomLoad { get; set; }

		[JsonProperty(SettingNames.FailOnMergeFailure)]
		bool? FailOnMergeFailure { get; set; }

		[JsonProperty(SettingNames.TranslogFlushTreshHoldOps)]
		string TranslogFlushTreshHoldOps { get; set; }

		[JsonProperty(SettingNames.TranslogFlushThresholdSize)]
		string TranslogFlushThresholdSize { get; set; }

		[JsonProperty(SettingNames.TranslogFlushThresholdPeriod)]
		string TranslogFlushThresholdPeriod { get; set; }

		[JsonProperty(SettingNames.TranslogDisableFlush)]
		bool? TranslogDisableFlush { get; set; }

		[JsonProperty(SettingNames.CacheFilterMaxSize)]
		string CacheFilterMaxSize { get; set; }

		[JsonProperty(SettingNames.CacheFilterExpire)]
		string CacheFilterExpire { get; set; }

		[JsonProperty(SettingNames.CacheQueryEnable)]
		bool? CacheQueryEnable { get; set; }

		[JsonProperty(SettingNames.GatewaySnapshotInterval)]
		string GatewaySnapshotInterval { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationInclude)]
		IDictionary<string, object> RoutingAllocationInclude { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationExclude)]
		IDictionary<string, object> RoutingAllocationExclude { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationRequire)]
		IDictionary<string, object> RoutingAllocationRequire { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationEnable)]
		RoutingAllocationEnableOption? RoutingAllocationEnable { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationDisableAllication)]
		bool? RoutingAllocationDisableAllication { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationDisableNewAllocation)]
		bool? RoutingAllocationDisableNewAllocation { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationDisableReplicaAllocation)]
		bool? RoutingAllocationDisableReplicaAllocation { get; set; }

		[JsonProperty(SettingNames.RoutingAllocationTotalShardsPerNode)]
		int? RoutingAllocationTotalShardsPerNode { get; set; }

		[JsonProperty(SettingNames.RecoveryInitialShards)]
		string RecoveryInitialShards { get; set; }

		[JsonProperty(SettingNames.GcDeletes)]
		bool? GcDeletes { get; set; }

		[JsonProperty(SettingNames.TtlDisablePurge)]
		bool? TtlDisablePurge { get; set; }

		[JsonProperty(SettingNames.TranslogFsType)]
		string TranslogFsType { get; set; }

		[JsonProperty(SettingNames.CompoundFormat)]
		bool? CompoundFormat { get; set; }

		[JsonProperty(SettingNames.CompoundOnFlush)]
		bool? CompoundOnFlush { get; set; }

		[JsonProperty(SettingNames.WarmersEnabled)]
		bool? WarmersEnabled { get; set; }

		[JsonProperty(SettingNames.Analysis)]
		AnalysisSettings Analysis { get; set; }
	}

	internal static class UpdateSettingsPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpdateSettingsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;
		}
	}

	public partial class UpdateSettingsRequest : IndexOptionalPathBase<UpdateSettingsRequestParameters>, IUpdateSettingsRequest
	{
		public int? NumberOfReplicas { get; set; }

		public object AutoExpandReplicas { get; set; }

		public bool? BlocksReadOnly { get; set; }

		public bool? BlocksRead { get; set; }

		public bool? BlocksWrite { get; set; }

		public bool? BlocksMetadata { get; set; }

		public string RefreshInterval { get; set; }

		public int? IndexConcurrency { get; set; }

		public string Codec { get; set; }

		public bool? CodecBloomLoad { get; set; }

		public bool? FailOnMergeFailure { get; set; }

		public string TranslogFlushTreshHoldOps { get; set; }

		public string TranslogFlushThresholdSize { get; set; }

		public string TranslogFlushThresholdPeriod { get; set; }

		public bool? TranslogDisableFlush { get; set; }

		public string CacheFilterMaxSize { get; set; }

		public string CacheFilterExpire { get; set; }

		public bool? CacheQueryEnable { get; set; }

		public string GatewaySnapshotInterval { get; set; }

		public IDictionary<string, object> RoutingAllocationInclude { get; set; }

		public IDictionary<string, object> RoutingAllocationExclude { get; set; }

		public IDictionary<string, object> RoutingAllocationRequire { get; set; }

		public RoutingAllocationEnableOption? RoutingAllocationEnable { get; set; }

		public bool? RoutingAllocationDisableAllication { get; set; }

		public bool? RoutingAllocationDisableNewAllocation { get; set; }

		public bool? RoutingAllocationDisableReplicaAllocation { get; set; }

		public int? RoutingAllocationTotalShardsPerNode { get; set; }

		public string RecoveryInitialShards { get; set; }

		public bool? GcDeletes { get; set; }

		public bool? TtlDisablePurge { get; set; }

		public string TranslogFsType { get; set; }

		public bool? CompoundFormat { get; set; }

		public bool? CompoundOnFlush { get; set; }

		public bool? WarmersEnabled { get; set; }

		public AnalysisSettings Analysis { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpdateSettingsRequestParameters> pathInfo)
		{
			UpdateSettingsPathInfo.Update(settings, pathInfo);
		}
	}

	[DescriptorFor("IndicesPutSettings")]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public partial class UpdateSettingsDescriptor
		: IndexOptionalPathDescriptorBase<UpdateSettingsDescriptor, UpdateSettingsRequestParameters>, IUpdateSettingsRequest
	{
		private IUpdateSettingsRequest Self { get { return this; } }

		int? IUpdateSettingsRequest.NumberOfReplicas { get; set; }

		object IUpdateSettingsRequest.AutoExpandReplicas { get; set; }

		bool? IUpdateSettingsRequest.BlocksReadOnly { get; set; }

		bool? IUpdateSettingsRequest.BlocksRead { get; set; }

		bool? IUpdateSettingsRequest.BlocksWrite { get; set; }

		bool? IUpdateSettingsRequest.BlocksMetadata { get; set; }

		string IUpdateSettingsRequest.RefreshInterval { get; set; }

		int? IUpdateSettingsRequest.IndexConcurrency { get; set; }

		string IUpdateSettingsRequest.Codec { get; set; }

		bool? IUpdateSettingsRequest.CodecBloomLoad { get; set; }

		bool? IUpdateSettingsRequest.FailOnMergeFailure { get; set; }

		string IUpdateSettingsRequest.TranslogFlushTreshHoldOps { get; set; }

		string IUpdateSettingsRequest.TranslogFlushThresholdSize { get; set; }

		string IUpdateSettingsRequest.TranslogFlushThresholdPeriod { get; set; }

		bool? IUpdateSettingsRequest.TranslogDisableFlush { get; set; }

		string IUpdateSettingsRequest.CacheFilterMaxSize { get; set; }

		string IUpdateSettingsRequest.CacheFilterExpire { get; set; }

		bool? IUpdateSettingsRequest.CacheQueryEnable { get; set; }

		string IUpdateSettingsRequest.GatewaySnapshotInterval { get; set; }

		IDictionary<string, object> IUpdateSettingsRequest.RoutingAllocationInclude { get; set; }

		IDictionary<string, object> IUpdateSettingsRequest.RoutingAllocationExclude { get; set; }

		IDictionary<string, object> IUpdateSettingsRequest.RoutingAllocationRequire { get; set; }

		RoutingAllocationEnableOption? IUpdateSettingsRequest.RoutingAllocationEnable { get; set; }

		bool? IUpdateSettingsRequest.RoutingAllocationDisableAllication { get; set; }

		bool? IUpdateSettingsRequest.RoutingAllocationDisableNewAllocation { get; set; }

		bool? IUpdateSettingsRequest.RoutingAllocationDisableReplicaAllocation { get; set; }

		int? IUpdateSettingsRequest.RoutingAllocationTotalShardsPerNode { get; set; }

		string IUpdateSettingsRequest.RecoveryInitialShards { get; set; }

		bool? IUpdateSettingsRequest.GcDeletes { get; set; }

		bool? IUpdateSettingsRequest.TtlDisablePurge { get; set; }

		string IUpdateSettingsRequest.TranslogFsType { get; set; }

		bool? IUpdateSettingsRequest.CompoundFormat { get; set; }

		bool? IUpdateSettingsRequest.CompoundOnFlush { get; set; }

		bool? IUpdateSettingsRequest.WarmersEnabled { get; set; }

		AnalysisSettings IUpdateSettingsRequest.Analysis { get; set; }

		IndexNameMarker IIndexOptionalPath<UpdateSettingsRequestParameters>.Index { get; set; }

		bool? IIndexOptionalPath<UpdateSettingsRequestParameters>.AllIndices { get; set; }

		/// <summary>
		/// The number of replicas each shard has.
		/// </summary>
		public UpdateSettingsDescriptor NumberOfReplicas(int numberOfReplicas)
		{
			this.Self.NumberOfReplicas = numberOfReplicas;
			return this;
		}

		/// <summary>
		/// Set to an actual value (like 0-all) or false to disable it.
		/// </summary>
		public UpdateSettingsDescriptor AutoExpandReplicas(bool autoExpandReplicas = true)
		{
			this.Self.AutoExpandReplicas = autoExpandReplicas;
			return this;
		}

		public UpdateSettingsDescriptor AutoExpandReplicas(string autoExpandReplicas)
		{
			this.Self.AutoExpandReplicas = autoExpandReplicas;
			return this;
		}

		///<summary>
		/// Set to true to have the index read only, false to allow writes and metadta changes.
		/// </summary>
		public UpdateSettingsDescriptor BlockReadonly(bool blocksReadonly = true)
		{
			this.Self.BlocksReadOnly = blocksReadonly;
			return this;
		}

		///<summary>
		/// Set to true to disable read operations againstthe index.
		/// </summary>
		public UpdateSettingsDescriptor BlocksRead(bool blocksRead = true)
		{
			this.Self.BlocksRead = blocksRead;
			return this;
		}

		/// <summary>
		///Set to true to disable write operations against the index. 
		/// </summary>
		public UpdateSettingsDescriptor BlocksWrite(bool blocksWrite = true)
		{
			this.Self.BlocksWrite = blocksWrite;
			return this;
		}

		///<summary>
		/// Set to true to disable metadata operations against the index.
		/// </summary>
		public UpdateSettingsDescriptor BlocksMetadata(bool blocksMetaData = true)
		{
			this.Self.BlocksMetadata = blocksMetaData;
			return this;
		}

		/// <summary>
		///The async refresh interval of a shard. 
		/// </summary>
		public UpdateSettingsDescriptor RefreshInterval(string refreshInterval)
		{
			this.Self.RefreshInterval = refreshInterval;
			return this;
		}

		/// <summary>
		///Defaults to 8. 
		/// </summary>
		public UpdateSettingsDescriptor IndexConcurrency(int indexConcurrency)
		{
			this.Self.IndexConcurrency = indexConcurrency;
			return this;
		}

		/// <summary>
		///Codec. Default to default. 
		/// </summary>
		public UpdateSettingsDescriptor Codec(string codec)
		{
			this.Self.Codec = codec;
			return this;
		}

		/// <summary>
		/// Whether to load the bloom filter. Defaults to true. 
		/// [coming in 0.90.9] Coming in 0.90.9.. See the section called “Bloom filter posting format”.
		/// </summary>
		public UpdateSettingsDescriptor CodeBloomLoad(bool codecBloomLoad = true)
		{
			this.Self.CodecBloomLoad = codecBloomLoad;
			return this;
		}

		/// <summary>
		/// Default to true.
		/// </summary>
		public UpdateSettingsDescriptor FailOnMergeFailure(bool failOnMergeFailure = true)
		{
			this.Self.FailOnMergeFailure = failOnMergeFailure;
			return this;
		}

		/// <summary>
		///  When to flush based on operations.
		/// </summary>
		public UpdateSettingsDescriptor TranslogFlushThresholdOps(string translogFlushThresholdOps)
		{
			this.Self.TranslogFlushTreshHoldOps = translogFlushThresholdOps;
			return this;
		}

		/// <summary>
		/// When to flush based on translog (bytes) size.
		/// </summary>
		public UpdateSettingsDescriptor TranslogFlushThresholdSize(string numberOfReplicas)
		{
			this.Self.TranslogFlushThresholdSize = numberOfReplicas;
			return this;
		}

		/// <summary>
		/// When to flush based on a period of not flushing.
		/// </summary>
		public UpdateSettingsDescriptor TranslogFlushThresholdPeriod(string translogFlushThresholdPeriod)
		{
			this.Self.TranslogFlushThresholdPeriod = translogFlushThresholdPeriod;
			return this;
		}

		/// <summary>
		/// Disables flushing. Note, should be set for a short interval and then enabled. 
		/// </summary>
		public UpdateSettingsDescriptor TranslogDisableFlush(bool translogDisableFlush = true)
		{
			this.Self.TranslogDisableFlush = translogDisableFlush;
			return this;
		}

		/// <summary>
		/// The maximum size of filter cache (per segment in shard). Set to -1 to disable.
		///  </summary>
		public UpdateSettingsDescriptor CacheFilterMaxSize(string cacheFilterMaxSize)
		{
			this.Self.CacheFilterMaxSize = cacheFilterMaxSize;
			return this;
		}

		/// <summary>
		/// The expire after access time for filter cache. Set to -1 to disable.
		/// </summary>
		public UpdateSettingsDescriptor CacheFilterExpire(string cacheFilterExpire)
		{
			this.Self.CacheFilterExpire = cacheFilterExpire;
			return this;
		}

		/// <summary>
		/// Enable or disable the query cache.
		/// </summary>
		public UpdateSettingsDescriptor CacheQueryEnable(bool cacheQueryEnable = true)
		{
			this.Self.CacheQueryEnable = cacheQueryEnable;
			return this;
		}

		/// <summary>
		/// The gateway snapshot interval (only applies to shared gateways). Defaults to 10s.
		/// </summary>
		public UpdateSettingsDescriptor GatewaySnapshotInterval(string gatewaySnapshotInterval)
		{
			this.Self.GatewaySnapshotInterval = gatewaySnapshotInterval;
			return this;
		}

		/// <summary>
		/// A node matching any rule will be allowed to host shards from the index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationInclude(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			this.Self.RoutingAllocationInclude = selector(new FluentDictionary<string, object>());
			return this;
		}

		///	<summary>
		/// A node matching any rule will NOT be allowed to host shards from the index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationExclude(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			this.Self.RoutingAllocationExclude = selector(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary> 
		/// Only nodes matching all rules will be allowed to host shards from the index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationRequire(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			this.Self.RoutingAllocationRequire = selector(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// Enables shard allocation for a specific index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationEnable(RoutingAllocationEnableOption option)
		{
			this.Self.RoutingAllocationEnable = option;
			return this;
		}

		/// <summary>
		/// Disable allocation. Defaults to false.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationDisableAllocation(bool disable = true)
		{
			this.Self.RoutingAllocationDisableAllication = disable;
			return this;
		}

		/// <summary>
		/// Disable new allocation. Defaults to false.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationDisableNewAllocation(bool disable = true)
		{
			this.Self.RoutingAllocationDisableNewAllocation = disable;
			return this;
		}

		/// <summary> 
		/// Disable replica allocation. Defaults to false.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationDisableReplicateAllocation(bool disable = true)
		{
			this.Self.RoutingAllocationDisableReplicaAllocation = disable;
			return this;
		}

		/// <summary>
		/// Controls the total number of shards allowed to be allocated on a single node. Defaults to unbounded (-1).
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationTotalShardsPerNode(int totalShardsPerNode)
		{
			this.Self.RoutingAllocationTotalShardsPerNode = totalShardsPerNode;
			return this;
		}

		///<summary>
		/// When using local gateway a particular shard is recovered only if there can be allocated quorum shards in the cluster. 
		///It can be set to:
		///quorum (default)
		///quorum-1 (or half)
		///full
		///full-1.
		///Number values are also supported, e.g. 1.
		///</summary>
		public UpdateSettingsDescriptor RecoveryInitialShards(string recoveryInitialShards)
		{
			this.Self.RecoveryInitialShards = recoveryInitialShards;
			return this;
		}

		/// <summary>
		/// Disables temporarily the purge of expired docs.
		/// </summary>
		public UpdateSettingsDescriptor GcDeletes(bool gcDeletes = true)
		{
			this.Self.GcDeletes = gcDeletes;
			return this;
		}

		/// <summary>
		/// Disables temporarily the purge of expired docs.
		/// </summary>
		public UpdateSettingsDescriptor TtlDisablePurge(bool ttlDisablePurge = true)
		{
			this.Self.TtlDisablePurge = ttlDisablePurge;
			return this;
		}

		/// <summary>
		/// Either simple or buffered (default).
		/// </summary>
		public UpdateSettingsDescriptor TranslogFsType(string translogFsType)
		{
			this.Self.TranslogFsType = translogFsType;
			return this;
		}

		///<summary>
		/// See index.compound_format in the section called “Index Settings”. 
		/// </summary>
		public UpdateSettingsDescriptor CompoundFormat(bool compoundFormat = true)
		{
			this.Self.CompoundFormat = compoundFormat;
			return this;
		}

		///<summary>
		/// See `index.compound_on_flush in the section called “Index Settings”.
		/// </summary>
		public UpdateSettingsDescriptor CompoundOnFlush(bool compoundOnFlush = true)
		{
			this.Self.CompoundOnFlush = compoundOnFlush;
			return this;
		}

		///<summary>
		/// See Warmers. Defaults to true. 
		/// </summary>
		public UpdateSettingsDescriptor WarmersEnabled(bool warmersEnabled = true)
		{
			this.Self.WarmersEnabled = warmersEnabled;
			return this;
		}

		/// <summary>
		/// When updating analysis settings you need to close and open the index prior and afterwards
		/// </summary>
		public UpdateSettingsDescriptor Analysis(Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector)
		{
			analysisSelector.ThrowIfNull("analysisSelector");
			var descriptor = analysisSelector(new AnalysisDescriptor());
			this.Self.Analysis = descriptor != null ? descriptor._AnalysisSettings : null;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<UpdateSettingsRequestParameters> pathInfo)
		{
			UpdateSettingsPathInfo.Update(settings, pathInfo);
		}
	}
}
