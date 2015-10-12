using System;
using System.Collections.Generic;
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
			dict[UpdatableSettings.TranslogSyncInterval] = translog?.SyncInterval;
			dict[UpdatableSettings.TranslogDurability] = translog?.Durability;
			dict[UpdatableSettings.TranslogFsType] = translog?.FileSystemType;

			var flush = ds.Translog?.Flush;
			dict[UpdatableSettings.TranslogFlushThresholdSize] = flush?.ThresholdSize;
			dict[UpdatableSettings.TranslogFlushTreshHoldOps] = flush?.ThresholdOps;
			dict[UpdatableSettings.TranslogFlushThresholdPeriod] = flush?.ThresholdPeriod;
			dict[UpdatableSettings.TranslogInterval] = flush?.Interval;

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

			dict["analysis"] = ds.Analysis;
			serializer.Serialize(writer, dict);

		}


		public JObject Flatten(JObject original, string prefix = "", JObject newObject = null)
		{
			newObject = newObject ?? new JObject();
			foreach (var property in original.Properties())
			{
				if (property.Value is JObject && property.Name != "analysis") Flatten(property.Value.Value<JObject>(), property.Name + ".", newObject);
				else newObject.Add(prefix + property.Name, property.Value);
			}
			return newObject;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var s = new IndexSettings();

			var settings = Flatten(JObject.Load(reader)).Properties().ToDictionary(kv=> kv.Name);
			Set<int?>(settings, UpdatableSettings.NumberOfReplicas, v => s.NumberOfReplicas = v);
			Set<string>(settings, UpdatableSettings.AutoExpandReplicas, v => s.AutoExpandReplicas = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.RefreshInterval, v => s.RefreshInterval = v);
			Set<bool?>(settings, UpdatableSettings.BlocksReadOnly, v => s.BlocksReadOnly = v);
			Set<bool?>(settings, UpdatableSettings.BlocksRead, v => s.BlocksRead = v);
			Set<bool?>(settings, UpdatableSettings.BlocksWrite, v => s.BlocksWrite = v);
			Set<bool?>(settings, UpdatableSettings.BlocksMetadata, v => s.BlocksMetadata = v);
			Set<int?>(settings, UpdatableSettings.Priority, v => s.Priority = v);
			Set<bool?>(settings, UpdatableSettings.WarmersEnabled, v => s.WarmersEnabled = v);
			Set<bool?>(settings, UpdatableSettings.RequestCacheEnable, v => s.RequestCacheEnabled = v);
			Set<Union<int, RecoveryInitialShards>>(settings, UpdatableSettings.RecoveryInitialShards, v => s.RecoveryInitialShards = v);
			Set<int?>(settings, UpdatableSettings.RoutingAllocationTotalShardsPerNode, v => s.RoutingAllocationTotalShardsPerNode = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.UnassignedNodeLeftDelayedTimeout, v => s.UnassignedNodeLeftDelayedTimeout = v);

			var t = s.Translog = new TranslogSettings();
			Set<TimeUnitExpression>(settings, UpdatableSettings.TranslogSyncInterval, v => t.SyncInterval = v);
			Set<TranslogDurability?>(settings, UpdatableSettings.TranslogDurability, v => t.Durability = v);
			Set<TranslogWriteMode?>(settings, UpdatableSettings.TranslogFsType, v => t.FileSystemType = v);

			var tf = s.Translog.Flush = new TranslogFlushSettings();
			Set<string>(settings, UpdatableSettings.TranslogFlushThresholdSize, v => tf.ThresholdSize = v);
			Set<int?>(settings, UpdatableSettings.TranslogFlushTreshHoldOps, v => tf.ThresholdOps = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.TranslogFlushThresholdPeriod, v => tf.ThresholdPeriod = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.TranslogInterval, v => tf.Interval = v);

			s.Merge = new MergeSettings();
			var p = s.Merge.Policy = new MergePolicySettings();
			Set<int?>(settings, UpdatableSettings.MergePolicyExpungeDeletesAllowed, v => p.ExpungeDeletesAllowed = v);
			Set<string>(settings, UpdatableSettings.MergePolicyFloorSegment, v => p.FloorSegment = v);
			Set<int?>(settings, UpdatableSettings.MergePolicyMaxMergeAtOnce, v => p.MaxMergeAtOnce = v);
			Set<int?>(settings, UpdatableSettings.MergePolicyMaxMergeAtOnceExplicit, v => p.MaxMergeAtOnceExplicit = v);
			Set<string>(settings, UpdatableSettings.MergePolicyMaxMergedSegment, v => p.MaxMergedSegment = v);
			Set<int?>(settings, UpdatableSettings.MergePolicySegmentsPerTier, v => p.SegmentsPerTier = v);
			Set<double?>(settings, UpdatableSettings.MergePolicyReclaimDeletesWeight, v => p.ReclaimDeletesWeight = v);

			var ms = s.Merge.Scheduler = new MergeSchedulerSettings();
			Set<int?>(settings, UpdatableSettings.MergeSchedulerMaxThreadCount, v => ms.MaxThreadCount = v);
			Set<bool?>(settings, UpdatableSettings.MergeSchedulerAutoThrottle, v => ms.AutoThrottle = v);

			var slowlog = s.SlowLog = new SlowLog();
			var search = s.SlowLog.Search = new SlowLogSearch();
			Set<SlowLogLevel?>(settings, UpdatableSettings.SlowlogSearchLevel, v => search.LogLevel = v);
			var query = s.SlowLog.Search.Query = new SlowLogSearchQuery();
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdQueryWarn, v => query.ThresholdWarn = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdQueryInfo, v => query.ThresholdInfo = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdQueryDebug, v => query.ThresholdDebug = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdQueryTrace, v => query.ThresholdTrace = v);

			var fetch = s.SlowLog.Search.Fetch = new SlowLogSearchFetch();
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdFetchWarn, v => fetch.ThresholdWarn = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdFetchInfo, v => fetch.ThresholdInfo = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdFetchDebug, v => fetch.ThresholdDebug = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogSearchThresholdFetchTrace, v => fetch.ThresholdTrace = v);

			var indexing = s.SlowLog.Indexing = new SlowLogIndexing();
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogIndexingThresholdFetchWarn, v => indexing.ThresholdWarn = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogIndexingThresholdFetchInfo, v => indexing.ThresholdInfo = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogIndexingThresholdFetchDebug, v => indexing.ThresholdDebug = v);
			Set<TimeUnitExpression>(settings, UpdatableSettings.SlowlogIndexingThresholdFetchTrace, v => indexing.ThresholdTrace = v);
			Set<SlowLogLevel?>(settings, UpdatableSettings.SlowlogIndexingLevel, v => indexing.LogLevel = v);
			Set<int?>(settings, UpdatableSettings.SlowlogIndexingSource, v => indexing.Source = v);
			Set<int?>(settings, "index.number_of_shards", v => s.NumberOfShards = v);
			Set<FileSystemStorageImplementation?>(settings, "index.store.type", v => s.FileSystemStorageImplementation = v, serializer);
			foreach (var kv in settings)
			{
				var setting = kv.Value;
				if (kv.Key == "analysis" || kv.Key == "index.analysis")
					s.Analysis = setting.Value.Value<JObject>().ToObject<Analysis>(serializer);
				else
				{
					((IHasADictionary)s).Dictionary.Add(kv.Key, serializer.Deserialize(kv.Value.Value.CreateReader()));
				}
			}
			return s;
		}

		public bool Set<T>(IDictionary<string, JProperty> settings, string key, Action<T> assign, JsonSerializer serializer = null)
		{
			if (!settings.ContainsKey(key)) return false;
			var v = settings[key];
			assign(serializer == null ? v.Value.ToObject<T>() : v.Value.ToObject<T>(serializer));
			settings.Remove(key);
			return true;
		}
	}
}