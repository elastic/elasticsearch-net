using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Nest.FixedIndexSettings;
using static Nest.IndexSortSettings;
using static Nest.UpdatableIndexSettings;

namespace Nest
{
	internal class IndexSettingsConverter : VerbatimDictionaryKeysJsonConverter<string, object>
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var ds = value as IDynamicIndexSettings ?? (value as IUpdateIndexSettingsRequest)?.IndexSettings;

			if (ds == null) return;
			IDictionary<string,object> d = ds;

			void Set(string knownKey, object newValue)
			{
				if (newValue != null) d[knownKey] = newValue;
			}

			Set(NumberOfReplicas, ds.NumberOfReplicas);
			Set(RefreshInterval, ds.RefreshInterval);
			Set(BlocksReadOnly, ds.BlocksReadOnly);
			Set(BlocksRead, ds.BlocksRead);
			Set(BlocksWrite, ds.BlocksWrite);
			Set(BlocksMetadata, ds.BlocksMetadata);
			Set(Priority, ds.Priority);
			Set(UpdatableIndexSettings.AutoExpandReplicas, ds.AutoExpandReplicas);
			Set(UpdatableIndexSettings.RecoveryInitialShards, ds.RecoveryInitialShards);
			Set(RequestsCacheEnable, ds.RequestsCacheEnabled);
			Set(RoutingAllocationTotalShardsPerNode, ds.RoutingAllocationTotalShardsPerNode);
			Set(UnassignedNodeLeftDelayedTimeout, ds.UnassignedNodeLeftDelayedTimeout);

			var translog = ds.Translog;
			Set(TranslogSyncInterval, translog?.SyncInterval);
			Set(UpdatableIndexSettings.TranslogDurability, translog?.Durability);

			var flush = ds.Translog?.Flush;
			Set(TranslogFlushThresholdSize, flush?.ThresholdSize);
			Set(TranslogFlushThresholdPeriod, flush?.ThresholdPeriod);

			Set(MergePolicyExpungeDeletesAllowed, ds.Merge?.Policy?.ExpungeDeletesAllowed);
			Set(MergePolicyFloorSegment, ds.Merge?.Policy?.FloorSegment);
			Set(MergePolicyMaxMergeAtOnce, ds.Merge?.Policy?.MaxMergeAtOnce);
			Set(MergePolicyMaxMergeAtOnceExplicit, ds.Merge?.Policy?.MaxMergeAtOnceExplicit);
			Set(MergePolicyMaxMergedSegment, ds.Merge?.Policy?.MaxMergedSegment);
			Set(MergePolicySegmentsPerTier, ds.Merge?.Policy?.SegmentsPerTier);
			Set(MergePolicyReclaimDeletesWeight, ds.Merge?.Policy?.ReclaimDeletesWeight);

			Set(MergeSchedulerMaxThreadCount, ds.Merge?.Scheduler?.MaxThreadCount);
			Set(MergeSchedulerAutoThrottle, ds.Merge?.Scheduler?.AutoThrottle);

			var log = ds.SlowLog;
			var search = log?.Search;
			var indexing = log?.Indexing;

			Set(SlowlogSearchThresholdQueryWarn, search?.Query?.ThresholdWarn);
			Set(SlowlogSearchThresholdQueryInfo, search?.Query?.ThresholdInfo);
			Set(SlowlogSearchThresholdQueryDebug, search?.Query?.ThresholdDebug);
			Set(SlowlogSearchThresholdQueryTrace, search?.Query?.ThresholdTrace);

			Set(SlowlogSearchThresholdFetchWarn, search?.Fetch?.ThresholdWarn);
			Set(SlowlogSearchThresholdFetchInfo, search?.Fetch?.ThresholdInfo);
			Set(SlowlogSearchThresholdFetchDebug, search?.Fetch?.ThresholdDebug);
			Set(SlowlogSearchThresholdFetchTrace, search?.Fetch?.ThresholdTrace);
			Set(SlowlogSearchLevel, search?.LogLevel);

			Set(SlowlogIndexingThresholdFetchWarn, indexing?.ThresholdWarn);
			Set(SlowlogIndexingThresholdFetchInfo, indexing?.ThresholdInfo);
			Set(SlowlogIndexingThresholdFetchDebug, indexing?.ThresholdDebug);
			Set(SlowlogIndexingThresholdFetchTrace, indexing?.ThresholdTrace);
			Set(SlowlogIndexingLevel, indexing?.LogLevel);
			Set(SlowlogIndexingSource, indexing?.Source);

			Set(UpdatableIndexSettings.Analysis, ds.Analysis);
			Set(Similarity, ds.Similarity);

			var indexSettings = value as IIndexSettings;

            Set(StoreType, indexSettings?.FileSystemStorageImplementation);
            Set(QueriesCacheEnabled, indexSettings?.Queries?.Cache?.Enabled);
			Set(NumberOfShards, indexSettings?.NumberOfShards);
			Set(NumberOfRoutingShards, indexSettings?.NumberOfRoutingShards);
            Set(RoutingPartitionSize, indexSettings?.RoutingPartitionSize);

			if (indexSettings?.Sorting != null)
			{
				Set(IndexSortSettings.Fields, AsArrayOrSingleItem(indexSettings.Sorting.Fields));
				Set(Order, AsArrayOrSingleItem(indexSettings.Sorting.Order));
				Set(Mode, AsArrayOrSingleItem(indexSettings.Sorting.Mode));
				Set(Missing, AsArrayOrSingleItem(indexSettings.Sorting.Missing));
			}

			base.WriteJson(writer, d, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var s = new IndexSettings();
			SetKnownIndexSettings(reader, serializer, s);
			if (!typeof (IUpdateIndexSettingsRequest).IsAssignableFrom(objectType)) return s;

			var request = new UpdateIndexSettingsRequest() { IndexSettings =  s};
			return request;
		}

		private static object AsArrayOrSingleItem<T>(IEnumerable<T> items)
		{
			if (items == null || !items.Any())
				return null;

			if (items.Count() == 1)
				return items.First();

			return items;
		}

		private static JObject Flatten(JObject original, string prefix = "", JObject newObject = null)
		{
			newObject = newObject ?? new JObject();
			foreach (var property in original.Properties())
			{
				if (property.Value is JObject &&
				    property.Name != UpdatableIndexSettings.Analysis &&
				    property.Name != Similarity)
					Flatten(property.Value.Value<JObject>(), prefix + property.Name + ".", newObject);
				else newObject.Add(prefix + property.Name, property.Value);
			}
			return newObject;
		}

		private static void SetKnownIndexSettings(JsonReader reader, JsonSerializer serializer, IIndexSettings s)
		{
			var settings = Flatten(JObject.Load(reader)).Properties().ToDictionary(kv => kv.Name);

			Set<int?>(s, settings, NumberOfReplicas, v => s.NumberOfReplicas = v);
			Set<AutoExpandReplicas>(s, settings, UpdatableIndexSettings.AutoExpandReplicas, v => s.AutoExpandReplicas = v);
			Set<Time>(s, settings, RefreshInterval, v => s.RefreshInterval = v);
			Set<bool?>(s, settings, BlocksReadOnly, v => s.BlocksReadOnly = v);
			Set<bool?>(s, settings, BlocksRead, v => s.BlocksRead = v);
			Set<bool?>(s, settings, BlocksWrite, v => s.BlocksWrite = v);
			Set<bool?>(s, settings, BlocksMetadata, v => s.BlocksMetadata = v);
			Set<int?>(s, settings, Priority, v => s.Priority = v);
			Set<Union<int, RecoveryInitialShards>>(s, settings, UpdatableIndexSettings.RecoveryInitialShards,
				v => s.RecoveryInitialShards = v, serializer);
			Set<bool?>(s, settings, RequestsCacheEnable, v => s.RequestsCacheEnabled = v);
			Set<int?>(s, settings, RoutingAllocationTotalShardsPerNode,
				v => s.RoutingAllocationTotalShardsPerNode = v);
			Set<Time>(s, settings, UnassignedNodeLeftDelayedTimeout,
				v => s.UnassignedNodeLeftDelayedTimeout = v);

			var t = s.Translog = new TranslogSettings();
			Set<Time>(s, settings, TranslogSyncInterval, v => t.SyncInterval = v);
			Set<TranslogDurability?>(s, settings, UpdatableIndexSettings.TranslogDurability, v => t.Durability = v);

			var tf = s.Translog.Flush = new TranslogFlushSettings();
			Set<string>(s, settings, TranslogFlushThresholdSize, v => tf.ThresholdSize = v);
			Set<Time>(s, settings, TranslogFlushThresholdPeriod, v => tf.ThresholdPeriod = v);

			s.Merge = new MergeSettings();
			var p = s.Merge.Policy = new MergePolicySettings();
			Set<int?>(s, settings, MergePolicyExpungeDeletesAllowed, v => p.ExpungeDeletesAllowed = v);
			Set<string>(s, settings, MergePolicyFloorSegment, v => p.FloorSegment = v);
			Set<int?>(s, settings, MergePolicyMaxMergeAtOnce, v => p.MaxMergeAtOnce = v);
			Set<int?>(s, settings, MergePolicyMaxMergeAtOnceExplicit, v => p.MaxMergeAtOnceExplicit = v);
			Set<string>(s, settings, MergePolicyMaxMergedSegment, v => p.MaxMergedSegment = v);
			Set<int?>(s, settings, MergePolicySegmentsPerTier, v => p.SegmentsPerTier = v);
			Set<double?>(s, settings, MergePolicyReclaimDeletesWeight, v => p.ReclaimDeletesWeight = v);

			var ms = s.Merge.Scheduler = new MergeSchedulerSettings();
			Set<int?>(s, settings, MergeSchedulerMaxThreadCount, v => ms.MaxThreadCount = v);
			Set<bool?>(s, settings, MergeSchedulerAutoThrottle, v => ms.AutoThrottle = v);

			var slowlog = s.SlowLog = new SlowLog();
			var search = s.SlowLog.Search = new SlowLogSearch();
			Set<LogLevel?>(s, settings, SlowlogSearchLevel, v => search.LogLevel = v);
			var query = s.SlowLog.Search.Query = new SlowLogSearchQuery();
			Set<Time>(s, settings, SlowlogSearchThresholdQueryWarn, v => query.ThresholdWarn = v);
			Set<Time>(s, settings, SlowlogSearchThresholdQueryInfo, v => query.ThresholdInfo = v);
			Set<Time>(s, settings, SlowlogSearchThresholdQueryDebug,
				v => query.ThresholdDebug = v);
			Set<Time>(s, settings, SlowlogSearchThresholdQueryTrace,
				v => query.ThresholdTrace = v);

			var fetch = s.SlowLog.Search.Fetch = new SlowLogSearchFetch();
			Set<Time>(s, settings, SlowlogSearchThresholdFetchWarn, v => fetch.ThresholdWarn = v);
			Set<Time>(s, settings, SlowlogSearchThresholdFetchInfo, v => fetch.ThresholdInfo = v);
			Set<Time>(s, settings, SlowlogSearchThresholdFetchDebug,
				v => fetch.ThresholdDebug = v);
			Set<Time>(s, settings, SlowlogSearchThresholdFetchTrace,
				v => fetch.ThresholdTrace = v);

			var indexing = s.SlowLog.Indexing = new SlowLogIndexing();
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchWarn,
				v => indexing.ThresholdWarn = v);
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchInfo,
				v => indexing.ThresholdInfo = v);
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchDebug,
				v => indexing.ThresholdDebug = v);
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchTrace,
				v => indexing.ThresholdTrace = v);
			Set<LogLevel?>(s, settings, SlowlogIndexingLevel, v => indexing.LogLevel = v);
			Set<int?>(s, settings, SlowlogIndexingSource, v => indexing.Source = v);
			Set<int?>(s, settings, NumberOfShards, v => s.NumberOfShards = v);
			Set<int?>(s, settings, NumberOfRoutingShards, v => s.NumberOfRoutingShards = v);
			Set<int?>(s, settings, RoutingPartitionSize, v => s.RoutingPartitionSize = v);
			Set<FileSystemStorageImplementation?>(s, settings, StoreType, v => s.FileSystemStorageImplementation = v, serializer);

			var sorting = s.Sorting = new SortingSettings();
			SetArray<string[], string>(s, settings, IndexSortSettings.Fields, v => sorting.Fields = v, v => sorting.Fields = new [] { v });
			SetArray<IndexSortOrder[], IndexSortOrder>(s, settings, Order, v => sorting.Order = v, v => sorting.Order = new [] { v });
			SetArray<IndexSortMode[], IndexSortMode>(s, settings, Mode, v => sorting.Mode = v, v => sorting.Mode = new [] { v });
			SetArray<IndexSortMissing[], IndexSortMissing>(s, settings, Missing, v => sorting.Missing = v, v => sorting.Missing = new [] { v });

			var queries = s.Queries = new QueriesSettings();
			var queriesCache = s.Queries.Cache = new QueriesCacheSettings();
			Set<bool?>(s, settings, QueriesCacheEnabled, v => queriesCache.Enabled = v);

			IDictionary<string,object> dict = s;
			foreach (var kv in settings)
			{
				var setting = kv.Value;
				if (kv.Key == UpdatableIndexSettings.Analysis || kv.Key == "index.analysis")
					s.Analysis = setting.Value.Value<JObject>().ToObject<Analysis>(serializer);
				if (kv.Key == Similarity || kv.Key == "index.similarity")
					s.Similarity = setting.Value.Value<JObject>().ToObject<Similarities>(serializer);
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
			var value = serializer == null ? v.Value.ToObject<T>() : v.Value.ToObject<T>(serializer);
			assign(value);
			s.Add(key, value);
			settings.Remove(key);
		}

		private static void SetArray<TArray, TItem>(IIndexSettings s, IDictionary<string, JProperty> settings, string key, Action<TArray> assign, Action<TItem> assign2, JsonSerializer serializer = null)
		{
			if (!settings.ContainsKey(key)) return;
			var v = settings[key];
			if (v.Value is JArray)
			{
				var value = serializer == null ? v.Value.ToObject<TArray>() : v.Value.ToObject<TArray>(serializer);
				assign(value);
				s.Add(key, value);
			}
			else
			{
				var value = serializer == null ? v.Value.ToObject<TItem>() : v.Value.ToObject<TItem>(serializer);
				assign2(value);
				s.Add(key, value);
			}
			settings.Remove(key);
		}
	}
}
