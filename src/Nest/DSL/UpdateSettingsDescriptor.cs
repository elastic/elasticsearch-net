using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class UpdateSettingsDescriptor
		: IndexOptionalPathDescriptorBase<UpdateSettingsDescriptor, UpdateQueryString>
	{
		[JsonProperty("index.number_of_replicas")]
		internal int? _NumberOfReplicas { get; set; }

		/// <summary>
		/// The number of replicas each shard has.
		/// </summary>
		public UpdateSettingsDescriptor NumberOfReplicas(int numberOfReplicas)
		{
			this._NumberOfReplicas = numberOfReplicas;
			return this;
		}

		[JsonProperty("index.auto_expand_replicas")]
		internal object _AutoExpandReplicas { get; set; }
		/// <summary>
		/// Set to an actual value (like 0-all) or false to disable it.
		/// </summary>
		public UpdateSettingsDescriptor AutoExpandReplicas(bool autoExpandReplicas = true)
		{
			this._AutoExpandReplicas = autoExpandReplicas;
			return this;
		}
		public UpdateSettingsDescriptor AutoExpandReplicas(string autoExpandReplicas)
		{
			this._AutoExpandReplicas = autoExpandReplicas;
			return this;
		}

		[JsonProperty("index.blocks.read_only")]
		internal bool? _BlocksReadOnly { get; set; }
		///<summary>
		/// Set to true to have the index read only, false to allow writes and metadta changes.
		/// </summary>
		public UpdateSettingsDescriptor BlockReadonly(bool blocksReadonly = true)
		{
			this._BlocksReadOnly = blocksReadonly;
			return this;
		}

		[JsonProperty("index.blocks.read")]
		internal bool? _BlocksRead { get; set; }
		///<summary>
		/// Set to true to disable read operations againstthe index.
		/// </summary>
		public UpdateSettingsDescriptor BlocksRead(bool blocksRead = true)
		{
			this._BlocksRead = blocksRead;
			return this;
		}

		[JsonProperty("index.blocks.write")]
		internal bool? _BlocksWrite { get; set; }
		/// <summary>
		///Set to true to disable write operations against the index. 
		/// </summary>
		public UpdateSettingsDescriptor BlocksWrite(bool blocksWrite = true)
		{
			this._BlocksWrite = blocksWrite;
			return this;
		}

		[JsonProperty("index.blocks.metadata")]
		internal bool? _BlocksMetadata { get; set; }
		///<summary>
		/// Set to true to disable metadata operations against the index.
		/// </summary>
		public UpdateSettingsDescriptor BlocksMetadata(bool blocksMetaData = true)
		{
			this._BlocksMetadata = blocksMetaData;
			return this;
		}

		[JsonProperty("index.refresh_interval")]
		internal string _RefreshInterval { get; set; }
		/// <summary>
		///The async refresh interval of a shard. 
		/// </summary>
		public UpdateSettingsDescriptor RefreshInterval(string refreshInterval)
		{
			this._RefreshInterval = refreshInterval;
			return this;
		}

		[JsonProperty("index.index_concurrency")]
		internal int? _IndexConcurrency { get; set; }
		/// <summary>
		///Defaults to 8. 
		/// </summary>
		public UpdateSettingsDescriptor IndexConcurrency(int indexConcurrency)
		{
			this._IndexConcurrency = indexConcurrency;
			return this;
		}

		[JsonProperty("index.codec")]
		internal string _Codec { get; set; }
		/// <summary>
		///Codec. Default to default. 
		/// </summary>
		public UpdateSettingsDescriptor Codec(string codec)
		{
			this._Codec = codec;
			return this;
		}

		[JsonProperty("index.codec.bloom.load")]
		internal bool? _CodecBloomLoad { get; set; }
		/// <summary>
		/// Whether to load the bloom filter. Defaults to true. 
		/// [coming in 0.90.9] Coming in 0.90.9.. See the section called “Bloom filter posting format”.
		/// </summary>
		public UpdateSettingsDescriptor CodeBloomLoad(bool codecBloomLoad = true)
		{
			this._CodecBloomLoad = codecBloomLoad;
			return this;
		}

		[JsonProperty("index.fail_on_merge_failure")]
		internal bool? _FailOnMergeFailure { get; set; }
		/// <summary>
		/// Default to true.
		/// </summary>
		public UpdateSettingsDescriptor FailOnMergeFailure(bool failOnMergeFailure = true)
		{
			this._FailOnMergeFailure = failOnMergeFailure;
			return this;
		}

		[JsonProperty("index.translog.flush_threshold_ops")]
		internal string _TranslogFlushTreshHoldOps { get; set; }
		/// <summary>
		///  When to flush based on operations.
		/// </summary>
		public UpdateSettingsDescriptor TranslogFlushThresholdOps(string translogFlushThresholdOps)
		{
			this._TranslogFlushTreshHoldOps = translogFlushThresholdOps;
			return this;
		}

		[JsonProperty("index.translog.flush_threshold_size")]
		internal string _TranslogFlushThresholdSize { get; set; }
		/// <summary>
		/// When to flush based on translog (bytes) size.
		/// </summary>
		public UpdateSettingsDescriptor TranslogFlushThresholdSize(string numberOfReplicas)
		{
			this._TranslogFlushThresholdSize  = numberOfReplicas;
			return this;
		}

		[JsonProperty("index.translog.flush_threshold_period")]
		internal string _TranslogFlushThresholdPeriod { get; set; }
		/// <summary>
		/// When to flush based on a period of not flushing.
		/// </summary>
		public UpdateSettingsDescriptor TranslogFlushThresholdPeriod(string translogFlushThresholdPeriod)
		{
			this._TranslogFlushThresholdPeriod  = translogFlushThresholdPeriod;
			return this;
		}

		[JsonProperty("index.translog.disable_flush")]
		internal bool? _TranslogDisableFlush { get; set; }
		/// <summary>
		/// Disables flushing. Note, should be set for a short interval and then enabled. 
		/// </summary>
		public UpdateSettingsDescriptor TranslogDisableFlush(bool translogDisableFlush = true)
		{
			this._TranslogDisableFlush  = translogDisableFlush;
			return this;
		}

		[JsonProperty("index.cache.filter.max_size")]
		internal string _CacheFilterMaxSize { get; set; }
		/// <summary>
		/// The maximum size of filter cache (per segment in shard). Set to -1 to disable.
		///  </summary>
		public UpdateSettingsDescriptor CacheFilterMaxSize(string cacheFilterMaxSize)
		{
			this._CacheFilterMaxSize  = cacheFilterMaxSize;
			return this;
		}

		[JsonProperty("index.cache.filter.expire")]
		internal string _CacheFilterExpire { get; set; }
		/// <summary>
		/// The expire after access time for filter cache. Set to -1 to disable.
		/// </summary>
		public UpdateSettingsDescriptor CacheFilterExpire(string cacheFilterExpire)
		{
			this._CacheFilterExpire  = cacheFilterExpire;
			return this;
		}

		[JsonProperty("index.gateway.snapshot_interval")]
		internal string _GatewaySnapshotInterval { get; set; }
		/// <summary>
		/// The gateway snapshot interval (only applies to shared gateways). Defaults to 10s.
		/// </summary>
		public UpdateSettingsDescriptor GatewaySnapshotInterval(string gatewaySnapshotInterval)
		{
			this._GatewaySnapshotInterval = gatewaySnapshotInterval;
			return this;
		}

		[JsonProperty("index.routing.allocation.include")]
		internal IDictionary<string, object> _RoutingAllocationInclude { get; set; }
		/// <summary>
		/// A node matching any rule will be allowed to host shards from the index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationInclude(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			this._RoutingAllocationInclude  = selector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty("index.routing.allocation.exclude")]
		internal IDictionary<string, object> _RoutingAllocationExclude { get; set; }
		///	<summary>
		/// A node matching any rule will NOT be allowed to host shards from the index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationExclude(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			this._RoutingAllocationExclude  = selector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty("index.routing.allocation.require")]
		internal IDictionary<string, object> _RoutingAllocationRequire { get; set; }
		/// <summary> 
		/// Only nodes matching all rules will be allowed to host shards from the index.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationRequire(
			Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			this._RoutingAllocationRequire  = selector(new FluentDictionary<string, object>());
			return this;
		}

		[JsonProperty("index.routing.allocation.disable_allocation")]
		internal bool? _RoutingAllocationDisableAllication { get; set; }
		/// <summary>
		/// Disable allocation. Defaults to false.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationDisableAllocation(bool disable = true)
		{
			this._RoutingAllocationDisableAllication  = disable;
			return this;
		}

		[JsonProperty("index.routing.allocation.disable_new_allocation")]
		internal bool? _RoutingAllocationDisableNewAllocation { get; set; }
		/// <summary>
		/// Disable new allocation. Defaults to false.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationDisableNewAllocation(bool disable = true)
		{
			this._RoutingAllocationDisableNewAllocation  = disable;
			return this;
		}

		[JsonProperty("index.routing.allocation.disable_replica_allocation")]
		internal bool? _RoutingAllocationDisableReplicaAllocation { get; set; }
		/// <summary> 
		/// Disable replica allocation. Defaults to false.
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationDisableReplicateAllocation(bool disable = true)
		{
			this._RoutingAllocationDisableReplicaAllocation = disable;
			return this;
		}

		[JsonProperty("index.routing.allocation.total_shards_per_node")]
		internal int? _RoutingAllocationTotalShardsPerNode { get; set; }
		/// <summary>
		/// Controls the total number of shards allowed to be allocated on a single node. Defaults to unbounded (-1).
		/// </summary>
		public UpdateSettingsDescriptor RoutingAllocationTotalShardsPerNode(int totalShardsPerNode)
		{
			this._RoutingAllocationTotalShardsPerNode  = totalShardsPerNode;
			return this;
		}

		[JsonProperty("index.recovery.initial_shards")]
		internal string _RecoveryInitialShards { get; set; }
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
			this._RecoveryInitialShards  = recoveryInitialShards;
			return this;
		}

		[JsonProperty("index.gc_deletes")]
		internal bool? _GcDeletes { get; set; }
		/// <summary>
		/// Disables temporarily the purge of expired docs.
		/// </summary>
		public UpdateSettingsDescriptor GcDeletes(bool gcDeletes = true)
		{
			this._GcDeletes  = gcDeletes;
			return this;
		}
		
		[JsonProperty("index.ttl.disable_purge")]
		internal bool? _TtlDisablePurge { get; set; }
		/// <summary>
		/// Disables temporarily the purge of expired docs.
		/// </summary>
		public UpdateSettingsDescriptor TtlDisablePurge(bool ttlDisablePurge = true)
		{
			this._TtlDisablePurge  = ttlDisablePurge;
			return this;
		}
		
		[JsonProperty("index.translog.fs.type")]
		internal string _TranslogFsType { get; set; }
		/// <summary>
		/// Either simple or buffered (default).
		/// </summary>
		public UpdateSettingsDescriptor TranslogFsType(string translogFsType)
		{
			this._TranslogFsType  = translogFsType;
			return this;
		}

		[JsonProperty("index.compound_format")]
		internal bool? _CompoundFormat { get; set; }
		///<summary>
		/// See index.compound_format in the section called “Index Settings”. 
		/// </summary>
		public UpdateSettingsDescriptor CompoundFormat(bool compoundFormat = true)
		{
			this._CompoundFormat = compoundFormat;
			return this;
		}

		[JsonProperty("index.compound_on_flush")]
		internal bool? _CompoundOnFlush { get; set; }
		///<summary>
		/// See `index.compound_on_flush in the section called “Index Settings”.
		/// </summary>
		public UpdateSettingsDescriptor CompoundOnFlush(bool compoundOnFlush = true)
		{
			this._CompoundOnFlush  = compoundOnFlush;
			return this;
		}

		[JsonProperty("index.warmer.enabled")]
		internal bool? _WarmersEnabled { get; set; }
		
		///<summary>
		/// See Warmers. Defaults to true. 
		/// </summary>
		public UpdateSettingsDescriptor WarmersEnabled(bool warmersEnabled = true)
		{
			this._WarmersEnabled = warmersEnabled;
			return this;
		}

		[JsonProperty("analysis")]
		internal AnalysisSettings _Analysis { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public UpdateSettingsDescriptor Analysis(Func<AnalysisDescriptor, AnalysisDescriptor> analysisSelector)
		{
			analysisSelector.ThrowIfNull("analysisSelector");
			var descriptor = analysisSelector(new AnalysisDescriptor());
			this._Analysis = descriptor != null ? descriptor._AnalysisSettings : null;
			return this;
		}

		internal new ElasticSearchPathInfo<UpdateSettingsQueryString> ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<UpdateSettingsQueryString>(settings);
			pathInfo.QueryString = this._QueryString;
			pathInfo.HttpMethod = PathInfoHttpMethod.PUT;

			return pathInfo;
		}

		public int? numberOfReplicas { get; set; }
	}
}
