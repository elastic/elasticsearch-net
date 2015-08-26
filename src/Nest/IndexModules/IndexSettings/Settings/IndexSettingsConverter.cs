using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class IndexSettingsConverter: JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ds = value as IDynamicIndexSettings;
			if (ds == null) return;

			var wrapDictionary = (ds as IHasADictionary);
			if (wrapDictionary == null) return;
			var dict = wrapDictionary?.Dictionary ?? new Dictionary<string, object>();

			dict[UpdatableSettings.NumberOfReplicas] = ds.NumberOfReplicas;
			dict[UpdatableSettings.AutoExpandReplicas] = ds.AutoExpandReplicas;
			dict[UpdatableSettings.RefreshInterval] = ds.RefreshInterval;
			dict[UpdatableSettings.BlocksReadOnly] = ds.BlocksReadOnly;
			dict[UpdatableSettings.BlocksRead] = ds.BlocksRead;
			dict[UpdatableSettings.BlocksWrite] = ds.BlocksWrite;
			dict[UpdatableSettings.BlocksMetadata] = ds.BlocksMetadata;
			dict[UpdatableSettings.Priority] = ds.Priority;
			dict[UpdatableSettings.WarmersEnabled] = ds.WarmersEnabled;
			dict[UpdatableSettings.RequestCacheEnable] = ds.RequestCacheEnabled;
			dict[UpdatableSettings.RecoveryInitialShards] = ds.RecoveryInitialShards;
			dict[UpdatableSettings.RoutingAllocationTotalShardsPerNode] =
				ds.RoutingAllocationTotalShardsPerNode;
			dict[UpdatableSettings.UnassignedNodeLeftDelayedTimeout] = ds.UnassignedNodeLeftDelayedTimeout;

			var translog = ds.Translog;
			dict[UpdatableSettings.TranslogSyncInterval] = translog.SyncInterval;
			dict[UpdatableSettings.TranslogDurability] = translog.Durability;
			dict[UpdatableSettings.TranslogFsType] = translog.FileSystemType;

			var flush = ds.Translog?.Flush;
			dict[UpdatableSettings.TranslogFlushThresholdSize] = flush.ThresholdSize;
			dict[UpdatableSettings.TranslogFlushTreshHoldOps] = flush.ThresholdOps;
			dict[UpdatableSettings.TranslogFlushThresholdPeriod] = flush.ThresholdPeriod;
			dict[UpdatableSettings.TranslogInterval] = flush.Interval;

			dict[UpdatableSettings.MergePolicyExpungeDeletesAllowed] = ds.Merge?.Policy.ExpungeDeletesAllowed;
			dict[UpdatableSettings.MergePolicyFloorSegment] = ds.Merge?.Policy.FloorSegment;
			dict[UpdatableSettings.MergePolicyMaxMergeAtOnce] = ds.Merge?.Policy.MaxMergeAtOnce;
			dict[UpdatableSettings.MergePolicyMaxMergeAtOnceExplicit] = ds.Merge?.Policy.MaxMergeAtOnceExplicit;
			dict[UpdatableSettings.MergePolicyMaxMergedSegment] = ds.Merge?.Policy.MaxMergedSegment;
			dict[UpdatableSettings.MergePolicySegmentsPerTier] = ds.Merge?.Policy.SegmentsPerTier;
			dict[UpdatableSettings.MergePolicyReclaimDeletesWeight] = ds.Merge?.Policy.ReclaimDeletesWeight;

			dict[UpdatableSettings.MergeSchedulerMaxThreadCount] = ds.Merge?.Scheduler?.MaxThreadCount;
			dict[UpdatableSettings.MergeSchedulerAutoThrottle] = ds.Merge?.Scheduler?.AutoThrottle;

			var log = ds.SlowLog;
			var search = log?.Search;
			var indexing = log?.Indexing;

			dict[UpdatableSettings.SlowlogSearchThresholdQueryWarn] = search?.Query?.ThresholdWarn;
			dict[UpdatableSettings.SlowlogSearchThresholdQueryInfo] = search?.Query?.ThresholdInfo;
			dict[UpdatableSettings.SlowlogSearchThresholdQueryDebug] = search?.Query?.ThresholdDebug;
			dict[UpdatableSettings.SlowlogSearchThresholdQueryTrace] = search?.Query?.ThresholdTrace;

			dict[UpdatableSettings.SlowlogSearchThresholdFetchWarn] = search?.Fetch?.ThresholdWarn;
			dict[UpdatableSettings.SlowlogSearchThresholdFetchInfo] = search?.Fetch?.ThresholdInfo;
			dict[UpdatableSettings.SlowlogSearchThresholdFetchDebug] = search?.Fetch?.ThresholdDebug;
			dict[UpdatableSettings.SlowlogSearchThresholdFetchTrace] = search?.Fetch?.ThresholdTrace;
			dict[UpdatableSettings.SlowlogSearchLevel] = search?.LogLevel;

			dict[UpdatableSettings.SlowlogIndexingThresholdFetchWarn] = indexing?.ThresholdWarn;
			dict[UpdatableSettings.SlowlogIndexingThresholdFetchInfo] = indexing?.ThresholdInfo;
			dict[UpdatableSettings.SlowlogIndexingThresholdFetchDebug] = indexing?.ThresholdDebug;
			dict[UpdatableSettings.SlowlogIndexingThresholdFetchTrace] = indexing?.ThresholdTrace;
			dict[UpdatableSettings.SlowlogIndexingLevel] = indexing?.LogLevel;
			dict[UpdatableSettings.SlowlogIndexingSource] = indexing?.Source;

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