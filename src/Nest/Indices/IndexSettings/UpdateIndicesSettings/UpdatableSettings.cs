namespace Nest
{
	public class UpdatableSettings
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
}