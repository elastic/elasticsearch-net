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

			dict[UpdatableIndexSettings.NumberOfReplicas] = ds.NumberOfReplicas;
			dict[UpdatableIndexSettings.AutoExpandReplicas] = ds.AutoExpandReplicas;
			dict[UpdatableIndexSettings.RefreshInterval] = ds.RefreshInterval;
			dict[UpdatableIndexSettings.BlocksReadOnly] = ds.BlocksReadOnly;
			dict[UpdatableIndexSettings.BlocksRead] = ds.BlocksRead;
			dict[UpdatableIndexSettings.BlocksWrite] = ds.BlocksWrite;
			dict[UpdatableIndexSettings.BlocksMetadata] = ds.BlocksMetadata;
			dict[UpdatableIndexSettings.Priority] = ds.Priority;
			dict[UpdatableIndexSettings.WarmersEnabled] = ds.WarmersEnabled;
			dict[UpdatableIndexSettings.RequestCacheEnable] = ds.RequestCacheEnabled;
			dict[UpdatableIndexSettings.RecoveryInitialShards] = ds.RecoveryInitialShards;
			dict[UpdatableIndexSettings.RoutingAllocationTotalShardsPerNode] =
				ds.RoutingAllocationTotalShardsPerNode;
			dict[UpdatableIndexSettings.UnassignedNodeLeftDelayedTimeout] = ds.UnassignedNodeLeftDelayedTimeout;

			var translog = ds.Translog;
			dict[UpdatableIndexSettings.TranslogSyncInterval] = translog?.SyncInterval;
			dict[UpdatableIndexSettings.TranslogDurability] = translog?.Durability;
			dict[UpdatableIndexSettings.TranslogFsType] = translog?.FileSystemType;

			var flush = ds.Translog?.Flush;
			dict[UpdatableIndexSettings.TranslogFlushThresholdSize] = flush?.ThresholdSize;
			dict[UpdatableIndexSettings.TranslogFlushTreshHoldOps] = flush?.ThresholdOps;
			dict[UpdatableIndexSettings.TranslogFlushThresholdPeriod] = flush?.ThresholdPeriod;
			dict[UpdatableIndexSettings.TranslogInterval] = flush?.Interval;

			dict[UpdatableIndexSettings.MergePolicyExpungeDeletesAllowed] = ds.Merge?.Policy.ExpungeDeletesAllowed;
			dict[UpdatableIndexSettings.MergePolicyFloorSegment] = ds.Merge?.Policy.FloorSegment;
			dict[UpdatableIndexSettings.MergePolicyMaxMergeAtOnce] = ds.Merge?.Policy.MaxMergeAtOnce;
			dict[UpdatableIndexSettings.MergePolicyMaxMergeAtOnceExplicit] = ds.Merge?.Policy.MaxMergeAtOnceExplicit;
			dict[UpdatableIndexSettings.MergePolicyMaxMergedSegment] = ds.Merge?.Policy.MaxMergedSegment;
			dict[UpdatableIndexSettings.MergePolicySegmentsPerTier] = ds.Merge?.Policy.SegmentsPerTier;
			dict[UpdatableIndexSettings.MergePolicyReclaimDeletesWeight] = ds.Merge?.Policy.ReclaimDeletesWeight;

			dict[UpdatableIndexSettings.MergeSchedulerMaxThreadCount] = ds.Merge?.Scheduler?.MaxThreadCount;
			dict[UpdatableIndexSettings.MergeSchedulerAutoThrottle] = ds.Merge?.Scheduler?.AutoThrottle;

			var log = ds.SlowLog;
			var search = log?.Search;
			var indexing = log?.Indexing;

			dict[UpdatableIndexSettings.SlowlogSearchThresholdQueryWarn] = search?.Query?.ThresholdWarn;
			dict[UpdatableIndexSettings.SlowlogSearchThresholdQueryInfo] = search?.Query?.ThresholdInfo;
			dict[UpdatableIndexSettings.SlowlogSearchThresholdQueryDebug] = search?.Query?.ThresholdDebug;
			dict[UpdatableIndexSettings.SlowlogSearchThresholdQueryTrace] = search?.Query?.ThresholdTrace;

			dict[UpdatableIndexSettings.SlowlogSearchThresholdFetchWarn] = search?.Fetch?.ThresholdWarn;
			dict[UpdatableIndexSettings.SlowlogSearchThresholdFetchInfo] = search?.Fetch?.ThresholdInfo;
			dict[UpdatableIndexSettings.SlowlogSearchThresholdFetchDebug] = search?.Fetch?.ThresholdDebug;
			dict[UpdatableIndexSettings.SlowlogSearchThresholdFetchTrace] = search?.Fetch?.ThresholdTrace;
			dict[UpdatableIndexSettings.SlowlogSearchLevel] = search?.LogLevel;

			dict[UpdatableIndexSettings.SlowlogIndexingThresholdFetchWarn] = indexing?.ThresholdWarn;
			dict[UpdatableIndexSettings.SlowlogIndexingThresholdFetchInfo] = indexing?.ThresholdInfo;
			dict[UpdatableIndexSettings.SlowlogIndexingThresholdFetchDebug] = indexing?.ThresholdDebug;
			dict[UpdatableIndexSettings.SlowlogIndexingThresholdFetchTrace] = indexing?.ThresholdTrace;
			dict[UpdatableIndexSettings.SlowlogIndexingLevel] = indexing?.LogLevel;
			dict[UpdatableIndexSettings.SlowlogIndexingSource] = indexing?.Source;


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
			IIndexSettings s = new IndexSettings();
			if (typeof(IUpdateIndexSettingsRequest).IsAssignableFrom(objectType))
				s = new UpdateIndexSettingsRequest();

			SetKnownIndexSettings(reader, serializer, s);
			return s;
		}

		private void SetKnownIndexSettings(JsonReader reader, JsonSerializer serializer, IIndexSettings s)
		{
			var settings = Flatten(JObject.Load(reader)).Properties().ToDictionary(kv => kv.Name);
			Set<int?>(settings, UpdatableIndexSettings.NumberOfReplicas, v => s.NumberOfReplicas = v);
			Set<string>(settings, UpdatableIndexSettings.AutoExpandReplicas, v => s.AutoExpandReplicas = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.RefreshInterval, v => s.RefreshInterval = v);
			Set<bool?>(settings, UpdatableIndexSettings.BlocksReadOnly, v => s.BlocksReadOnly = v);
			Set<bool?>(settings, UpdatableIndexSettings.BlocksRead, v => s.BlocksRead = v);
			Set<bool?>(settings, UpdatableIndexSettings.BlocksWrite, v => s.BlocksWrite = v);
			Set<bool?>(settings, UpdatableIndexSettings.BlocksMetadata, v => s.BlocksMetadata = v);
			Set<int?>(settings, UpdatableIndexSettings.Priority, v => s.Priority = v);
			Set<bool?>(settings, UpdatableIndexSettings.WarmersEnabled, v => s.WarmersEnabled = v);
			Set<bool?>(settings, UpdatableIndexSettings.RequestCacheEnable, v => s.RequestCacheEnabled = v);
			Set<Union<int, RecoveryInitialShards>>(settings, UpdatableIndexSettings.RecoveryInitialShards,
				v => s.RecoveryInitialShards = v);
			Set<int?>(settings, UpdatableIndexSettings.RoutingAllocationTotalShardsPerNode,
				v => s.RoutingAllocationTotalShardsPerNode = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.UnassignedNodeLeftDelayedTimeout,
				v => s.UnassignedNodeLeftDelayedTimeout = v);

			var t = s.Translog = new TranslogSettings();
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.TranslogSyncInterval, v => t.SyncInterval = v);
			Set<TranslogDurability?>(settings, UpdatableIndexSettings.TranslogDurability, v => t.Durability = v);
			Set<TranslogWriteMode?>(settings, UpdatableIndexSettings.TranslogFsType, v => t.FileSystemType = v);

			var tf = s.Translog.Flush = new TranslogFlushSettings();
			Set<string>(settings, UpdatableIndexSettings.TranslogFlushThresholdSize, v => tf.ThresholdSize = v);
			Set<int?>(settings, UpdatableIndexSettings.TranslogFlushTreshHoldOps, v => tf.ThresholdOps = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.TranslogFlushThresholdPeriod, v => tf.ThresholdPeriod = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.TranslogInterval, v => tf.Interval = v);

			s.Merge = new MergeSettings();
			var p = s.Merge.Policy = new MergePolicySettings();
			Set<int?>(settings, UpdatableIndexSettings.MergePolicyExpungeDeletesAllowed, v => p.ExpungeDeletesAllowed = v);
			Set<string>(settings, UpdatableIndexSettings.MergePolicyFloorSegment, v => p.FloorSegment = v);
			Set<int?>(settings, UpdatableIndexSettings.MergePolicyMaxMergeAtOnce, v => p.MaxMergeAtOnce = v);
			Set<int?>(settings, UpdatableIndexSettings.MergePolicyMaxMergeAtOnceExplicit, v => p.MaxMergeAtOnceExplicit = v);
			Set<string>(settings, UpdatableIndexSettings.MergePolicyMaxMergedSegment, v => p.MaxMergedSegment = v);
			Set<int?>(settings, UpdatableIndexSettings.MergePolicySegmentsPerTier, v => p.SegmentsPerTier = v);
			Set<double?>(settings, UpdatableIndexSettings.MergePolicyReclaimDeletesWeight, v => p.ReclaimDeletesWeight = v);

			var ms = s.Merge.Scheduler = new MergeSchedulerSettings();
			Set<int?>(settings, UpdatableIndexSettings.MergeSchedulerMaxThreadCount, v => ms.MaxThreadCount = v);
			Set<bool?>(settings, UpdatableIndexSettings.MergeSchedulerAutoThrottle, v => ms.AutoThrottle = v);

			var slowlog = s.SlowLog = new SlowLog();
			var search = s.SlowLog.Search = new SlowLogSearch();
			Set<LogLevel?>(settings, UpdatableIndexSettings.SlowlogSearchLevel, v => search.LogLevel = v);
			var query = s.SlowLog.Search.Query = new SlowLogSearchQuery();
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryWarn, v => query.ThresholdWarn = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryInfo, v => query.ThresholdInfo = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryDebug,
				v => query.ThresholdDebug = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryTrace,
				v => query.ThresholdTrace = v);

			var fetch = s.SlowLog.Search.Fetch = new SlowLogSearchFetch();
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchWarn, v => fetch.ThresholdWarn = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchInfo, v => fetch.ThresholdInfo = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchDebug,
				v => fetch.ThresholdDebug = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchTrace,
				v => fetch.ThresholdTrace = v);

			var indexing = s.SlowLog.Indexing = new SlowLogIndexing();
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchWarn,
				v => indexing.ThresholdWarn = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchInfo,
				v => indexing.ThresholdInfo = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchDebug,
				v => indexing.ThresholdDebug = v);
			Set<TimeUnitExpression>(settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchTrace,
				v => indexing.ThresholdTrace = v);
			Set<LogLevel?>(settings, UpdatableIndexSettings.SlowlogIndexingLevel, v => indexing.LogLevel = v);
			Set<int?>(settings, UpdatableIndexSettings.SlowlogIndexingSource, v => indexing.Source = v);
			Set<int?>(settings, "index.number_of_shards", v => s.NumberOfShards = v);
			Set<FileSystemStorageImplementation?>(settings, "index.store.type", v => s.FileSystemStorageImplementation = v,
				serializer);
			foreach (var kv in settings)
			{
				var setting = kv.Value;
				if (kv.Key == "analysis" || kv.Key == "index.analysis")
					s.Analysis = setting.Value.Value<JObject>().ToObject<Analysis>(serializer);
				else
				{
					((IHasADictionary) s).Dictionary.Add(kv.Key, serializer.Deserialize(kv.Value.Value.CreateReader()));
				}
			}
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