// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	/// <summary>
	/// Collection of Elasticsearch index settings that can be dynamically updated
	/// </summary>
	public static class UpdatableIndexSettings
	{
		public const string Analysis = "analysis";
		public const string AnalyzeMaxTokenCount = "index.analyze.max_token_count";
		public const string AutoExpandReplicas = "index.auto_expand_replicas";
		public const string BlocksMetadata = "index.blocks.metadata";
		public const string BlocksReadOnlyAllowDelete = "index.blocks.read_only_allow_delete";
		public const string BlocksRead = "index.blocks.read";
		public const string BlocksReadOnly = "index.blocks.read_only";
		public const string BlocksWrite = "index.blocks.write";

		public const string CompoundFormat = "index.compound_format";
		public const string CompoundOnFlush = "index.compound_on_flush";

		/// <summary>limits the number of unique nested types per index.</summary>
		public const string MappingNestedFieldsLimit = "index.mapping.nested_fields.limit";
		/// <summary>
		///  limits the number of nested objects that a single document may contain across all nested types,
		/// in order to prevent out of memory errors when a document contains too many nested objects.
		/// </summary>
		public const string MappingNestedObjectsLimit = "index.mapping.nested_objects.limit";

		/// <summary>the maximum difference between min_gram and max_gram for <see cref="INGramTokenizer"/> and <see cref="INGramTokenFilter"/></summary>
		public const string MaxNGramDiff = "index.max_ngram_diff";
		/// <summary>maximum value of from + size on a query</summary>
		public const string MaxResultWindow = "index.max_result_window";
		/// <summary>maximum value of from + size on an individual inner hit definition or top hits aggregation</summary>
		public const string MaxInnerResultWindow = "index.max_inner_result_window";
		/// <summary>maximum value of allowed script_fields that can be retrieved per search request.</summary>
		public const string MaxScriptFields = "index.max_script_fields";
		/// <summary>the maximum difference between min_shingle_size and max_shingle_size for <see cref="IShingleTokenFilter"/></summary>
		public const string MaxShingleDiff = "index.max_shingle_diff";



		public const string MergePolicyExpungeDeletesAllowed = "index.merge.policy.expunge_deletes_allowed";
		public const string MergePolicyFloorSegment = "index.merge.policy.floor_segment";
		public const string MergePolicyMaxMergeAtOnce = "index.merge.policy.max_merge_at_once";
		public const string MergePolicyMaxMergeAtOnceExplicit = "index.merge.policy.max_merge_at_once_explicit";
		public const string MergePolicyMaxMergedSegment = "index.merge.policy.max_merged_segment";
		public const string MergePolicyReclaimDeletesWeight = "index.merge.policy.reclaim_deletes_weight";
		public const string MergePolicySegmentsPerTier = "index.merge.policy.segments_per_tier";
		public const string MergeSchedulerAutoThrottle = "index.merge.scheduler.auto_throttle";

		public const string MergeSchedulerMaxThreadCount = "index.merge.scheduler.max_thread_count";
		public const string NumberOfReplicas = "index.number_of_replicas";
		public const string Priority = "index.priority";

		public const string QueriesCacheEnabled = "index.queries.cache.enabled";

		public const string SoftDeletesEnabled = "index.soft_deletes.enabled";
		public const string SoftDeletesRetentionOperations = "index.soft_deletes.retention.operations";

		public const string RecoveryInitialShards = "index.recovery.initial_shards";
		public const string RefreshInterval = "index.refresh_interval";

		public const string DefaultPipeline = "index.default_pipeline";
		[Obsolete("Use FinalPipeline")]
		public const string RequiredPipeline = "index.required_pipeline";
		public const string FinalPipeline = "index.final_pipeline";

		public const string RequestsCacheEnable = "index.requests.cache.enable";
		public const string RoutingAllocationDisableAllocation = "index.routing.allocation.disable_allocation";
		public const string RoutingAllocationDisableNewAllocation = "index.routing.allocation.disable_new_allocation";
		public const string RoutingAllocationDisableReplicaAllocation = "index.routing.allocation.disable_replica_allocation";
		public const string RoutingAllocationEnable = "index.routing.allocation.enable";
		public const string RoutingAllocationExclude = "index.routing.allocation.exclude";

		public const string RoutingAllocationInclude = "index.routing.allocation.include";
		public const string RoutingAllocationRequire = "index.routing.allocation.require";
		public const string RoutingAllocationTotalShardsPerNode = "index.routing.allocation.total_shards_per_node";

		public const string Similarity = "similarity";

		public const string SlowlogIndexingLevel = "index.indexing.slowlog.level";
		public const string SlowlogIndexingSource = "index.indexing.slowlog.source";
		public const string SlowlogIndexingThresholdFetchDebug = "index.indexing.slowlog.threshold.index.debug";
		public const string SlowlogIndexingThresholdFetchInfo = "index.indexing.slowlog.threshold.index.info";
		public const string SlowlogIndexingThresholdFetchTrace = "index.indexing.slowlog.threshold.index.trace";

		public const string SlowlogIndexingThresholdFetchWarn = "index.indexing.slowlog.threshold.index.warn";

		public const string SlowlogSearchLevel = "index.search.slowlog.level";
		public const string SlowlogSearchThresholdFetchDebug = "index.search.slowlog.threshold.fetch.debug";
		public const string SlowlogSearchThresholdFetchInfo = "index.search.slowlog.threshold.fetch.info";
		public const string SlowlogSearchThresholdFetchTrace = "index.search.slowlog.threshold.fetch.trace";

		public const string SlowlogSearchThresholdFetchWarn = "index.search.slowlog.threshold.fetch.warn";
		public const string SlowlogSearchThresholdQueryDebug = "index.search.slowlog.threshold.query.debug";
		public const string SlowlogSearchThresholdQueryInfo = "index.search.slowlog.threshold.query.info";
		public const string SlowlogSearchThresholdQueryTrace = "index.search.slowlog.threshold.query.trace";

		public const string SlowlogSearchThresholdQueryWarn = "index.search.slowlog.threshold.query.warn";

		public const string StoreType = "index.store.type";
		public const string TranslogDurability = "index.translog.durability";
		public const string TranslogFlushThresholdPeriod = "index.translog.flush_threshold_period";

		public const string TranslogFlushThresholdSize = "index.translog.flush_threshold_size";
		public const string TranslogSyncInterval = "index.translog.sync_interval";

		public const string UnassignedNodeLeftDelayedTimeout = "index.unassigned.node_left.delayed_timeout";
	}
}
