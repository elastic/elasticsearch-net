using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class IndexSettingsConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ds = value as IDynamicIndexSettings;
			if (ds == null) return;
			var dict = (ds as IWrapDictionary)?.BackingDictionary;
			if (dict == null) return;
			dict["index.number_of_replicas"] = ds.NumberOfReplicas;
			dict["index.auto_expand_replicas"] = ds.AutoExpandReplicas;
			dict["index.refresh_interval"] = ds.RefreshInterval;
			dict["index.blocks.read_only"] = ds.BlocksReadOnly;
			dict["index.blocks.read"] = ds.BlocksRead;
			dict["index.blocks.write"] = ds.BlocksWrite;
			dict["index.blocks.metadata"] = ds.BlocksMetadata;
			dict["index.priority"] = ds.Priority;
			dict["index.recovery.initial_shards"] = ds.RecoveryInitialShards;
			dict["index.routing.allocation.total_shards_per_node"] =
				ds.RoutingAllocationTotalShardsPerNode;
			dict["index.unassigned.node_left.delayed_timeout"] =
				ds.UnassignedNodeLeftDelayedTimeout;

			var translog = ds.Translog;
			dict["index.translog.sync_interval"] = translog.SyncInterval;
			dict["index.translog.durability"] = translog.Durability;
			dict["index.translog.fs.type"] = translog.FileSystemType;

			var flush = ds.Translog?.Flush;
			dict["index.translog.flush_threshold_size"] = flush.TresholdSize;
			dict["index.translog.flush_threshold_ops"] = flush.TresholdOps;
			dict["index.translog.flush_threshold_period"] = flush.TresholdPeriod;
			dict["index.translog.interval"] = flush.Interval;

			dict["index.merge.policy.expunge_deletes_allowed"] = ds.Merge?.Policy.ExpungeDeletesAllowed;
			dict["index.merge.policy.floor_segment"] = ds.Merge?.Policy.FloorSegment;
			dict["index.merge.policy.max_merge_at_once"] = ds.Merge?.Policy.MaxMergeAtOnce;
			dict["index.merge.policy.max_merge_at_once_explicit"] = ds.Merge?.Policy.MaxMergeAtOnceExplicit;
			dict["index.merge.policy.max_merged_segment"] = ds.Merge?.Policy.MaxMergedSegment;
			dict["index.merge.policy.segments_per_tier"] = ds.Merge?.Policy.SegmentsPerTier;
			dict["index.merge.policy.reclaim_deletes_weight"] = ds.Merge?.Policy.ReclaimDeletesWeight;

			dict["index.merge.scheduler.max_thread_count"] = ds.Merge?.Scheduler?.MaxThreadCount;
			dict["index.merge.scheduler.auto_throttle"] = ds.Merge?.Scheduler?.AutoThrottle;

			var log = ds.SlowLog;
			var search = log?.Search;
			var indexing = log?.Indexing;

			dict["index.search.slowlog.threshold.query.warn"] = search?.Query?.TresholdWarn;
			dict["index.search.slowlog.threshold.query.info"] = search?.Query?.TresholdInfo;
			dict["index.search.slowlog.threshold.query.debug"] = search?.Query?.TresholdDebug;
			dict["index.search.slowlog.threshold.query.trace"] = search?.Query?.TresholdTrace;

			dict["index.search.slowlog.threshold.fetch.warn"] = search?.Fetch?.TresholdWarn;
			dict["index.search.slowlog.threshold.fetch.info"] = search?.Fetch?.TresholdInfo;
			dict["index.search.slowlog.threshold.fetch.debug"] = search?.Fetch?.TresholdDebug;
			dict["index.search.slowlog.threshold.fetch.trace"] = search?.Fetch?.TresholdTrace;
			dict["index.search.slowlog.level"] = search?.LogLevel;

			dict["index.indexing.slowlog.threshold.index.warn"] = indexing?.TresholdWarn;
			dict["index.indexing.slowlog.threshold.index.info"] = indexing?.TresholdInfo;
			dict["index.indexing.slowlog.threshold.index.debug"] = indexing?.TresholdDebug;
			dict["index.indexing.slowlog.threshold.index.trace"] = indexing?.TresholdTrace;
			dict["index.indexing.slowlog.level"] = indexing?.LogLevel;
			dict["index.indexing.slowlog.source"] = indexing?.Source;

			var indexSettings = value as IIndexSettings;
			dict["index.number_of_shards"] = indexSettings?.NumberOfShards;
			dict["index.store.type"] = indexSettings?.FileSystemStorageImplementation;

			serializer.Serialize(writer, dict);

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}

	}
}