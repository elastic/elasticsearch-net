namespace Nest
{
	public static class UpdatableIndexSettings
	{
		public const string NumberOfReplicas = "index.number_of_replicas";
		public const string AutoExpandReplicas = "index.auto_expand_replicas";
		public const string RefreshInterval = "index.refresh_interval";
		public const string BlocksReadOnly = "index.blocks.read_only";
		public const string BlocksRead = "index.blocks.read";
		public const string BlocksWrite = "index.blocks.write";
		public const string BlocksMetadata = "index.blocks.metadata";
		public const string Priority = "index.priority";
		public const string TranslogFlushTreshHoldOps = "index.translog.flush_threshold_ops";
		public const string TranslogFlushThresholdSize = "index.translog.flush_threshold_size";
		public const string TranslogFlushThresholdPeriod = "index.translog.flush_threshold_period";
		public const string TranslogInterval = "index.translog.interval";
		public const string TranslogFsType = "index.translog.fs.type";
		public const string TranslogDurability = "index.translog.durability";
		public const string TranslogSyncInterval = "index.translog.sync_interval";
		public const string RequestCacheEnable = "index.requests.cache.enable";

		public const string RoutingAllocationInclude = "index.routing.allocation.include";//
		public const string RoutingAllocationExclude = "index.routing.allocation.exclude";//
		public const string RoutingAllocationRequire = "index.routing.allocation.require";//
		public const string RoutingAllocationEnable = "index.routing.allocation.enable";//
		public const string RoutingAllocationDisableAllication = "index.routing.allocation.disable_allocation";//
		public const string RoutingAllocationDisableNewAllocation = "index.routing.allocation.disable_new_allocation";//
		public const string RoutingAllocationDisableReplicaAllocation = "index.routing.allocation.disable_replica_allocation";//
		public const string RoutingAllocationTotalShardsPerNode = "index.routing.allocation.total_shards_per_node";
		public const string RecoveryInitialShards = "index.recovery.initial_shards";
		public const string UnassignedNodeLeftDelayedTimeout = "index.unassigned.node_left.delayed_timeout";

		public const string TtlDisablePurge = "index.ttl.disable_purge";//
		public const string CompoundFormat = "index.compound_format";//
		public const string CompoundOnFlush = "index.compound_on_flush";//
		public const string WarmersEnabled = "index.warmer.enabled";
		public const string Analysis = "analysis";

		public const string MergePolicyExpungeDeletesAllowed = "index.merge.policy.expunge_deletes_allowed";
		public const string MergePolicyFloorSegment = "index.merge.policy.floor_segment";
		public const string MergePolicyMaxMergeAtOnce = "index.merge.policy.max_merge_at_once";
		public const string MergePolicyMaxMergeAtOnceExplicit = "index.merge.policy.max_merge_at_once_explicit";
		public const string MergePolicyMaxMergedSegment = "index.merge.policy.max_merged_segment";
		public const string MergePolicySegmentsPerTier = "index.merge.policy.segments_per_tier";
		public const string MergePolicyReclaimDeletesWeight = "index.merge.policy.reclaim_deletes_weight";

		public const string MergeSchedulerMaxThreadCount = "index.merge.scheduler.max_thread_count";
		public const string MergeSchedulerAutoThrottle = "index.merge.scheduler.auto_throttle";

		public const string SlowlogSearchThresholdQueryWarn = "index.search.slowlog.threshold.query.warn";
		public const string SlowlogSearchThresholdQueryInfo = "index.search.slowlog.threshold.query.info";
		public const string SlowlogSearchThresholdQueryDebug = "index.search.slowlog.threshold.query.debug";
		public const string SlowlogSearchThresholdQueryTrace = "index.search.slowlog.threshold.query.trace";

		public const string SlowlogSearchThresholdFetchWarn = "index.search.slowlog.threshold.fetch.warn";
		public const string SlowlogSearchThresholdFetchInfo = "index.search.slowlog.threshold.fetch.info";
		public const string SlowlogSearchThresholdFetchDebug = "index.search.slowlog.threshold.fetch.debug";
		public const string SlowlogSearchThresholdFetchTrace = "index.search.slowlog.threshold.fetch.trace";

		public const string SlowlogSearchLevel = "index.search.slowlog.level";

		public const string SlowlogIndexingThresholdFetchWarn = "index.indexing.slowlog.threshold.index.warn";
		public const string SlowlogIndexingThresholdFetchInfo = "index.indexing.slowlog.threshold.index.info";
		public const string SlowlogIndexingThresholdFetchDebug = "index.indexing.slowlog.threshold.index.debug";
		public const string SlowlogIndexingThresholdFetchTrace = "index.indexing.slowlog.threshold.index.trace";

		public const string SlowlogIndexingLevel = "index.indexing.slowlog.level";
		public const string SlowlogIndexingSource = "index.indexing.slowlog.source";

	}
}