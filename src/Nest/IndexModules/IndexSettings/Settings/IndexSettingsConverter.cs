using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class IndexSettingsConverter : VerbatimDictionaryKeysJsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ds = value as IDynamicIndexSettings ?? (value as IUpdateIndexSettingsRequest)?.IndexSettings;
			;
			if (ds == null) return;

			IDictionary d = ds;

			d[UpdatableIndexSettings.NumberOfReplicas] = ds.NumberOfReplicas;
			d[UpdatableIndexSettings.AutoExpandReplicas] = ds.AutoExpandReplicas;
			d[UpdatableIndexSettings.RefreshInterval] = ds.RefreshInterval;
			d[UpdatableIndexSettings.BlocksReadOnly] = ds.BlocksReadOnly;
			d[UpdatableIndexSettings.BlocksRead] = ds.BlocksRead;
			d[UpdatableIndexSettings.BlocksWrite] = ds.BlocksWrite;
			d[UpdatableIndexSettings.BlocksMetadata] = ds.BlocksMetadata;
			d[UpdatableIndexSettings.Priority] = ds.Priority;
			d[UpdatableIndexSettings.WarmersEnabled] = ds.WarmersEnabled;
			d[UpdatableIndexSettings.RequestCacheEnable] = ds.RequestCacheEnabled;
			d[UpdatableIndexSettings.RecoveryInitialShards] = ds.RecoveryInitialShards;
			d[UpdatableIndexSettings.RoutingAllocationTotalShardsPerNode] =
				ds.RoutingAllocationTotalShardsPerNode;
			d[UpdatableIndexSettings.UnassignedNodeLeftDelayedTimeout] = ds.UnassignedNodeLeftDelayedTimeout;

			var translog = ds.Translog;
			d[UpdatableIndexSettings.TranslogSyncInterval] = translog?.SyncInterval;
			d[UpdatableIndexSettings.TranslogDurability] = translog?.Durability;
			d[UpdatableIndexSettings.TranslogFsType] = translog?.FileSystemType;

			var flush = ds.Translog?.Flush;
			d[UpdatableIndexSettings.TranslogFlushThresholdSize] = flush?.ThresholdSize;
			d[UpdatableIndexSettings.TranslogFlushTreshHoldOps] = flush?.ThresholdOps;
			d[UpdatableIndexSettings.TranslogFlushThresholdPeriod] = flush?.ThresholdPeriod;
			d[UpdatableIndexSettings.TranslogInterval] = flush?.Interval;

			d[UpdatableIndexSettings.MergePolicyExpungeDeletesAllowed] = ds.Merge?.Policy.ExpungeDeletesAllowed;
			d[UpdatableIndexSettings.MergePolicyFloorSegment] = ds.Merge?.Policy.FloorSegment;
			d[UpdatableIndexSettings.MergePolicyMaxMergeAtOnce] = ds.Merge?.Policy.MaxMergeAtOnce;
			d[UpdatableIndexSettings.MergePolicyMaxMergeAtOnceExplicit] = ds.Merge?.Policy.MaxMergeAtOnceExplicit;
			d[UpdatableIndexSettings.MergePolicyMaxMergedSegment] = ds.Merge?.Policy.MaxMergedSegment;
			d[UpdatableIndexSettings.MergePolicySegmentsPerTier] = ds.Merge?.Policy.SegmentsPerTier;
			d[UpdatableIndexSettings.MergePolicyReclaimDeletesWeight] = ds.Merge?.Policy.ReclaimDeletesWeight;

			d[UpdatableIndexSettings.MergeSchedulerMaxThreadCount] = ds.Merge?.Scheduler?.MaxThreadCount;
			d[UpdatableIndexSettings.MergeSchedulerAutoThrottle] = ds.Merge?.Scheduler?.AutoThrottle;

			var log = ds.SlowLog;
			var search = log?.Search;
			var indexing = log?.Indexing;

			d[UpdatableIndexSettings.SlowlogSearchThresholdQueryWarn] = search?.Query?.ThresholdWarn;
			d[UpdatableIndexSettings.SlowlogSearchThresholdQueryInfo] = search?.Query?.ThresholdInfo;
			d[UpdatableIndexSettings.SlowlogSearchThresholdQueryDebug] = search?.Query?.ThresholdDebug;
			d[UpdatableIndexSettings.SlowlogSearchThresholdQueryTrace] = search?.Query?.ThresholdTrace;

			d[UpdatableIndexSettings.SlowlogSearchThresholdFetchWarn] = search?.Fetch?.ThresholdWarn;
			d[UpdatableIndexSettings.SlowlogSearchThresholdFetchInfo] = search?.Fetch?.ThresholdInfo;
			d[UpdatableIndexSettings.SlowlogSearchThresholdFetchDebug] = search?.Fetch?.ThresholdDebug;
			d[UpdatableIndexSettings.SlowlogSearchThresholdFetchTrace] = search?.Fetch?.ThresholdTrace;
			d[UpdatableIndexSettings.SlowlogSearchLevel] = search?.LogLevel;

			d[UpdatableIndexSettings.SlowlogIndexingThresholdFetchWarn] = indexing?.ThresholdWarn;
			d[UpdatableIndexSettings.SlowlogIndexingThresholdFetchInfo] = indexing?.ThresholdInfo;
			d[UpdatableIndexSettings.SlowlogIndexingThresholdFetchDebug] = indexing?.ThresholdDebug;
			d[UpdatableIndexSettings.SlowlogIndexingThresholdFetchTrace] = indexing?.ThresholdTrace;
			d[UpdatableIndexSettings.SlowlogIndexingLevel] = indexing?.LogLevel;
			d[UpdatableIndexSettings.SlowlogIndexingSource] = indexing?.Source;


			var indexSettings = value as IIndexSettings;
			d["index.number_of_shards"] = indexSettings?.NumberOfShards;
			d["index.store.type"] = indexSettings?.FileSystemStorageImplementation;

			d["analysis"] = ds.Analysis;
			base.WriteJson(writer, d, serializer);
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
			SetKnownIndexSettings(reader, serializer, s);
			if (!typeof (IUpdateIndexSettingsRequest).IsAssignableFrom(objectType)) return s;

			var request = new UpdateIndexSettingsRequest() { IndexSettings =  s};
			return request;
		}

		private void SetKnownIndexSettings(JsonReader reader, JsonSerializer serializer, IIndexSettings s)
		{
			var settings = Flatten(JObject.Load(reader)).Properties().ToDictionary(kv => kv.Name);

			Set<int?>(s, settings, UpdatableIndexSettings.NumberOfReplicas, v => s.NumberOfReplicas = v);
			Set<string>(s, settings, UpdatableIndexSettings.AutoExpandReplicas, v => s.AutoExpandReplicas = v);
			Set<Time>(s, settings, UpdatableIndexSettings.RefreshInterval, v => s.RefreshInterval = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.BlocksReadOnly, v => s.BlocksReadOnly = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.BlocksRead, v => s.BlocksRead = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.BlocksWrite, v => s.BlocksWrite = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.BlocksMetadata, v => s.BlocksMetadata = v);
			Set<int?>(s, settings, UpdatableIndexSettings.Priority, v => s.Priority = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.WarmersEnabled, v => s.WarmersEnabled = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.RequestCacheEnable, v => s.RequestCacheEnabled = v);
			Set<Union<int, RecoveryInitialShards>>(s, settings, UpdatableIndexSettings.RecoveryInitialShards,
				v => s.RecoveryInitialShards = v);
			Set<int?>(s, settings, UpdatableIndexSettings.RoutingAllocationTotalShardsPerNode,
				v => s.RoutingAllocationTotalShardsPerNode = v);
			Set<Time>(s, settings, UpdatableIndexSettings.UnassignedNodeLeftDelayedTimeout,
				v => s.UnassignedNodeLeftDelayedTimeout = v);

			var t = s.Translog = new TranslogSettings();
			Set<Time>(s, settings, UpdatableIndexSettings.TranslogSyncInterval, v => t.SyncInterval = v);
			Set<TranslogDurability?>(s, settings, UpdatableIndexSettings.TranslogDurability, v => t.Durability = v);
			Set<TranslogWriteMode?>(s, settings, UpdatableIndexSettings.TranslogFsType, v => t.FileSystemType = v);

			var tf = s.Translog.Flush = new TranslogFlushSettings();
			Set<string>(s, settings, UpdatableIndexSettings.TranslogFlushThresholdSize, v => tf.ThresholdSize = v);
			Set<int?>(s, settings, UpdatableIndexSettings.TranslogFlushTreshHoldOps, v => tf.ThresholdOps = v);
			Set<Time>(s, settings, UpdatableIndexSettings.TranslogFlushThresholdPeriod, v => tf.ThresholdPeriod = v);
			Set<Time>(s, settings, UpdatableIndexSettings.TranslogInterval, v => tf.Interval = v);

			s.Merge = new MergeSettings();
			var p = s.Merge.Policy = new MergePolicySettings();
			Set<int?>(s, settings, UpdatableIndexSettings.MergePolicyExpungeDeletesAllowed, v => p.ExpungeDeletesAllowed = v);
			Set<string>(s, settings, UpdatableIndexSettings.MergePolicyFloorSegment, v => p.FloorSegment = v);
			Set<int?>(s, settings, UpdatableIndexSettings.MergePolicyMaxMergeAtOnce, v => p.MaxMergeAtOnce = v);
			Set<int?>(s, settings, UpdatableIndexSettings.MergePolicyMaxMergeAtOnceExplicit, v => p.MaxMergeAtOnceExplicit = v);
			Set<string>(s, settings, UpdatableIndexSettings.MergePolicyMaxMergedSegment, v => p.MaxMergedSegment = v);
			Set<int?>(s, settings, UpdatableIndexSettings.MergePolicySegmentsPerTier, v => p.SegmentsPerTier = v);
			Set<double?>(s, settings, UpdatableIndexSettings.MergePolicyReclaimDeletesWeight, v => p.ReclaimDeletesWeight = v);

			var ms = s.Merge.Scheduler = new MergeSchedulerSettings();
			Set<int?>(s, settings, UpdatableIndexSettings.MergeSchedulerMaxThreadCount, v => ms.MaxThreadCount = v);
			Set<bool?>(s, settings, UpdatableIndexSettings.MergeSchedulerAutoThrottle, v => ms.AutoThrottle = v);

			var slowlog = s.SlowLog = new SlowLog();
			var search = s.SlowLog.Search = new SlowLogSearch();
			Set<LogLevel?>(s, settings, UpdatableIndexSettings.SlowlogSearchLevel, v => search.LogLevel = v);
			var query = s.SlowLog.Search.Query = new SlowLogSearchQuery();
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryWarn, v => query.ThresholdWarn = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryInfo, v => query.ThresholdInfo = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryDebug,
				v => query.ThresholdDebug = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdQueryTrace,
				v => query.ThresholdTrace = v);

			var fetch = s.SlowLog.Search.Fetch = new SlowLogSearchFetch();
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchWarn, v => fetch.ThresholdWarn = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchInfo, v => fetch.ThresholdInfo = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchDebug,
				v => fetch.ThresholdDebug = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogSearchThresholdFetchTrace,
				v => fetch.ThresholdTrace = v);

			var indexing = s.SlowLog.Indexing = new SlowLogIndexing();
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchWarn,
				v => indexing.ThresholdWarn = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchInfo,
				v => indexing.ThresholdInfo = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchDebug,
				v => indexing.ThresholdDebug = v);
			Set<Time>(s, settings, UpdatableIndexSettings.SlowlogIndexingThresholdFetchTrace,
				v => indexing.ThresholdTrace = v);
			Set<LogLevel?>(s, settings, UpdatableIndexSettings.SlowlogIndexingLevel, v => indexing.LogLevel = v);
			Set<int?>(s, settings, UpdatableIndexSettings.SlowlogIndexingSource, v => indexing.Source = v);
			Set<int?>(s, settings, "index.number_of_shards", v => s.NumberOfShards = v);
			Set<FileSystemStorageImplementation?>(s, settings, "index.store.type", v => s.FileSystemStorageImplementation = v,
				serializer);

			IDictionary dict = s;
			foreach (var kv in settings)
			{
				var setting = kv.Value;
				if (kv.Key == "analysis" || kv.Key == "index.analysis")
					s.Analysis = setting.Value.Value<JObject>().ToObject<Analysis>(serializer);
				else
				{
					dict?.Add(kv.Key, serializer.Deserialize(kv.Value.Value.CreateReader()));
				}
			}
		}

		private static void Set<T>(IIndexSettings s, IDictionary<string, JProperty> settings, string key, Action<T> assign, JsonSerializer serializer = null)
		{
			if (!settings.ContainsKey(key)) return;
			var v = settings[key];
			T value = serializer == null ? v.Value.ToObject<T>() : v.Value.ToObject<T>(serializer);
			assign(value);
			s.Add(key, value);
			settings.Remove(key);
		}
	}
}