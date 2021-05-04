// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net.Utf8Json;
using static Nest.FixedIndexSettings;
using static Nest.IndexSortSettings;
using static Nest.UpdatableIndexSettings;

namespace Nest
{
	internal class IndexSettingsFormatter : IJsonFormatter<IIndexSettings>
	{
		private static readonly DynamicIndexSettingsFormatter DynamicIndexSettingsFormatter =
			new DynamicIndexSettingsFormatter();

		public void Serialize(ref JsonWriter writer, IIndexSettings value, IJsonFormatterResolver formatterResolver) =>
			DynamicIndexSettingsFormatter.Serialize(ref writer, value, formatterResolver);

		public IIndexSettings Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			(IIndexSettings)DynamicIndexSettingsFormatter.Deserialize(ref reader, formatterResolver);
	}

	internal class DynamicIndexSettingsFormatter : IJsonFormatter<IDynamicIndexSettings>
	{
		private static readonly IndexSettingsDictionaryFormatter Formatter = new IndexSettingsDictionaryFormatter();

		public IDynamicIndexSettings Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var indexSettings = new IndexSettings();
			SetKnownIndexSettings(ref reader, formatterResolver, indexSettings);
			return indexSettings;
		}

		public void Serialize(ref JsonWriter writer, IDynamicIndexSettings value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			IDictionary<string, object> d = value;

			void Set(string knownKey, object newValue)
			{
				if (newValue != null) d[knownKey] = newValue;
			}

			Set(NumberOfReplicas, value.NumberOfReplicas);
			Set(RefreshInterval, value.RefreshInterval);
			Set(DefaultPipeline, value.DefaultPipeline);
#pragma warning disable 618
			Set(RequiredPipeline, value.RequiredPipeline);
#pragma warning restore 618
			Set(FinalPipeline, value.FinalPipeline);
			Set(BlocksReadOnly, value.BlocksReadOnly);
			Set(BlocksRead, value.BlocksRead);
			Set(BlocksWrite, value.BlocksWrite);
			Set(BlocksMetadata, value.BlocksMetadata);
			Set(BlocksReadOnlyAllowDelete, value.BlocksReadOnlyAllowDelete);
			Set(Priority, value.Priority);
			Set(UpdatableIndexSettings.AutoExpandReplicas, value.AutoExpandReplicas);
			Set(UpdatableIndexSettings.RecoveryInitialShards, value.RecoveryInitialShards);
			Set(RequestsCacheEnable, value.RequestsCacheEnabled);
			Set(RoutingAllocationTotalShardsPerNode, value.RoutingAllocationTotalShardsPerNode);
			Set(UnassignedNodeLeftDelayedTimeout, value.UnassignedNodeLeftDelayedTimeout);

			var translog = value.Translog;
			Set(TranslogSyncInterval, translog?.SyncInterval);
			Set(UpdatableIndexSettings.TranslogDurability, translog?.Durability);

			var flush = value.Translog?.Flush;
			Set(TranslogFlushThresholdSize, flush?.ThresholdSize);
			Set(TranslogFlushThresholdPeriod, flush?.ThresholdPeriod);

			Set(MergePolicyExpungeDeletesAllowed, value.Merge?.Policy?.ExpungeDeletesAllowed);
			Set(MergePolicyFloorSegment, value.Merge?.Policy?.FloorSegment);
			Set(MergePolicyMaxMergeAtOnce, value.Merge?.Policy?.MaxMergeAtOnce);
			Set(MergePolicyMaxMergeAtOnceExplicit, value.Merge?.Policy?.MaxMergeAtOnceExplicit);
			Set(MergePolicyMaxMergedSegment, value.Merge?.Policy?.MaxMergedSegment);
			Set(MergePolicySegmentsPerTier, value.Merge?.Policy?.SegmentsPerTier);
			Set(MergePolicyReclaimDeletesWeight, value.Merge?.Policy?.ReclaimDeletesWeight);

			Set(MergeSchedulerMaxThreadCount, value.Merge?.Scheduler?.MaxThreadCount);
			Set(MergeSchedulerAutoThrottle, value.Merge?.Scheduler?.AutoThrottle);

			var log = value.SlowLog;
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

			Set(UpdatableIndexSettings.Analysis, value.Analysis);
			Set(Similarity, value.Similarity);

			if (value is IIndexSettings indexSettings)
			{
				Set(StoreType, indexSettings.FileSystemStorageImplementation);
				Set(QueriesCacheEnabled, indexSettings.Queries?.Cache?.Enabled);
				Set(NumberOfShards, indexSettings.NumberOfShards);
				Set(NumberOfRoutingShards, indexSettings.NumberOfRoutingShards);
				Set(RoutingPartitionSize, indexSettings.RoutingPartitionSize);

				Set(StoreType, indexSettings.FileSystemStorageImplementation);
				Set(QueriesCacheEnabled, indexSettings.Queries?.Cache?.Enabled);
				Set(NumberOfShards, indexSettings.NumberOfShards);
				Set(NumberOfRoutingShards, indexSettings.NumberOfRoutingShards);
				Set(RoutingPartitionSize, indexSettings.RoutingPartitionSize);
				Set(Hidden, indexSettings.Hidden);
				if (indexSettings.SoftDeletes != null)
				{
#pragma warning disable 618
					Set(SoftDeletesEnabled, indexSettings.SoftDeletes.Enabled);
#pragma warning restore 618
					Set(SoftDeletesRetentionOperations, indexSettings.SoftDeletes.Retention?.Operations);
				}

				if (indexSettings?.Sorting != null)
				{
					Set(IndexSortSettings.Fields, AsArrayOrSingleItem(indexSettings.Sorting.Fields));
					Set(Order, AsArrayOrSingleItem(indexSettings.Sorting.Order));
					Set(Mode, AsArrayOrSingleItem(indexSettings.Sorting.Mode));
					Set(IndexSortSettings.Missing, AsArrayOrSingleItem(indexSettings.Sorting.Missing));
				}
			}

			Formatter.Serialize(ref writer, d, formatterResolver);
		}

		private static object AsArrayOrSingleItem<T>(IEnumerable<T> items)
		{
			if (items == null || !items.Any())
				return null;

			if (items.Count() == 1)
				return items.First();

			return items;
		}

		private static Dictionary<string, object> Flatten(Dictionary<string, object> original, string prefix = "",
			Dictionary<string, object> current = null
		)
		{
			current ??= new Dictionary<string, object>();
			foreach (var property in original)
			{
				if (property.Value is Dictionary<string, object> objects &&
					property.Key != UpdatableIndexSettings.Analysis &&
					property.Key != Similarity)
					Flatten(objects, prefix + property.Key + ".", current);
				else current.Add(prefix + property.Key, property.Value);
			}
			return current;
		}

		private static void SetKnownIndexSettings(ref JsonReader reader, IJsonFormatterResolver formatterResolver, IIndexSettings s)
		{
			// TODO: Ugly. Could use dynamic dictionary here, and use get traversal logic
			var formatter = formatterResolver.GetFormatter<Dictionary<string, object>>();
			var settings = Flatten(formatter.Deserialize(ref reader, formatterResolver));

			Set<int?>(s, settings, NumberOfReplicas, v => s.NumberOfReplicas = v, formatterResolver);
			Set<AutoExpandReplicas>(s, settings, UpdatableIndexSettings.AutoExpandReplicas, v => s.AutoExpandReplicas = v, formatterResolver);
			Set<Time>(s, settings, RefreshInterval, v => s.RefreshInterval = v, formatterResolver);
			Set<bool?>(s, settings, BlocksReadOnly, v => s.BlocksReadOnly = v, formatterResolver);
			Set<bool?>(s, settings, BlocksRead, v => s.BlocksRead = v, formatterResolver);
			Set<bool?>(s, settings, BlocksWrite, v => s.BlocksWrite = v, formatterResolver);
			Set<bool?>(s, settings, BlocksMetadata, v => s.BlocksMetadata = v, formatterResolver);
			Set<bool?>(s, settings, BlocksReadOnlyAllowDelete, v => s.BlocksReadOnlyAllowDelete = v, formatterResolver);
			Set<int?>(s, settings, Priority, v => s.Priority = v, formatterResolver);
			Set<string>(s, settings, DefaultPipeline, v => s.DefaultPipeline = v, formatterResolver);
#pragma warning disable 618
			Set<string>(s, settings, RequiredPipeline, v => s.RequiredPipeline = v, formatterResolver);
#pragma warning restore 618
			Set<string>(s, settings, FinalPipeline, v => s.FinalPipeline = v, formatterResolver);

			Set<Union<int, RecoveryInitialShards>>(s, settings, UpdatableIndexSettings.RecoveryInitialShards,
				v => s.RecoveryInitialShards = v, formatterResolver);
			Set<bool?>(s, settings, RequestsCacheEnable, v => s.RequestsCacheEnabled = v, formatterResolver);
			Set<int?>(s, settings, RoutingAllocationTotalShardsPerNode,
				v => s.RoutingAllocationTotalShardsPerNode = v, formatterResolver);
			Set<Time>(s, settings, UnassignedNodeLeftDelayedTimeout,
				v => s.UnassignedNodeLeftDelayedTimeout = v, formatterResolver);

			var t = s.Translog = new TranslogSettings();
			Set<Time>(s, settings, TranslogSyncInterval, v => t.SyncInterval = v, formatterResolver);
			Set<TranslogDurability?>(s, settings, UpdatableIndexSettings.TranslogDurability, v => t.Durability = v, formatterResolver);

			var tf = s.Translog.Flush = new TranslogFlushSettings();
			Set<string>(s, settings, TranslogFlushThresholdSize, v => tf.ThresholdSize = v, formatterResolver);
			Set<Time>(s, settings, TranslogFlushThresholdPeriod, v => tf.ThresholdPeriod = v, formatterResolver);

			s.Merge = new MergeSettings();
			var p = s.Merge.Policy = new MergePolicySettings();
			Set<int?>(s, settings, MergePolicyExpungeDeletesAllowed, v => p.ExpungeDeletesAllowed = v, formatterResolver);
			Set<string>(s, settings, MergePolicyFloorSegment, v => p.FloorSegment = v, formatterResolver);
			Set<int?>(s, settings, MergePolicyMaxMergeAtOnce, v => p.MaxMergeAtOnce = v, formatterResolver);
			Set<int?>(s, settings, MergePolicyMaxMergeAtOnceExplicit, v => p.MaxMergeAtOnceExplicit = v, formatterResolver);
			Set<string>(s, settings, MergePolicyMaxMergedSegment, v => p.MaxMergedSegment = v, formatterResolver);
			Set<int?>(s, settings, MergePolicySegmentsPerTier, v => p.SegmentsPerTier = v, formatterResolver);
			Set<double?>(s, settings, MergePolicyReclaimDeletesWeight, v => p.ReclaimDeletesWeight = v, formatterResolver);

			var ms = s.Merge.Scheduler = new MergeSchedulerSettings();
			Set<int?>(s, settings, MergeSchedulerMaxThreadCount, v => ms.MaxThreadCount = v, formatterResolver);
			Set<bool?>(s, settings, MergeSchedulerAutoThrottle, v => ms.AutoThrottle = v, formatterResolver);

			s.SlowLog = new SlowLog();
			var search = s.SlowLog.Search = new SlowLogSearch();
			Set<LogLevel?>(s, settings, SlowlogSearchLevel, v => search.LogLevel = v, formatterResolver);
			var query = s.SlowLog.Search.Query = new SlowLogSearchQuery();
			Set<Time>(s, settings, SlowlogSearchThresholdQueryWarn, v => query.ThresholdWarn = v, formatterResolver);
			Set<Time>(s, settings, SlowlogSearchThresholdQueryInfo, v => query.ThresholdInfo = v, formatterResolver);
			Set<Time>(s, settings, SlowlogSearchThresholdQueryDebug,
				v => query.ThresholdDebug = v, formatterResolver);
			Set<Time>(s, settings, SlowlogSearchThresholdQueryTrace,
				v => query.ThresholdTrace = v, formatterResolver);

			var fetch = s.SlowLog.Search.Fetch = new SlowLogSearchFetch();
			Set<Time>(s, settings, SlowlogSearchThresholdFetchWarn, v => fetch.ThresholdWarn = v, formatterResolver);
			Set<Time>(s, settings, SlowlogSearchThresholdFetchInfo, v => fetch.ThresholdInfo = v, formatterResolver);
			Set<Time>(s, settings, SlowlogSearchThresholdFetchDebug,
				v => fetch.ThresholdDebug = v, formatterResolver);
			Set<Time>(s, settings, SlowlogSearchThresholdFetchTrace,
				v => fetch.ThresholdTrace = v, formatterResolver);

			var indexing = s.SlowLog.Indexing = new SlowLogIndexing();
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchWarn, v => indexing.ThresholdWarn = v, formatterResolver);
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchInfo, v => indexing.ThresholdInfo = v, formatterResolver);
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchDebug, v => indexing.ThresholdDebug = v, formatterResolver);
			Set<Time>(s, settings, SlowlogIndexingThresholdFetchTrace, v => indexing.ThresholdTrace = v, formatterResolver);
			Set<LogLevel?>(s, settings, SlowlogIndexingLevel, v => indexing.LogLevel = v, formatterResolver);
			Set<int?>(s, settings, SlowlogIndexingSource, v => indexing.Source = v, formatterResolver);
			Set<int?>(s, settings, NumberOfShards, v => s.NumberOfShards = v, formatterResolver);
			Set<int?>(s, settings, NumberOfRoutingShards, v => s.NumberOfRoutingShards = v, formatterResolver);
			Set<int?>(s, settings, RoutingPartitionSize, v => s.RoutingPartitionSize = v, formatterResolver);
			Set<bool?>(s, settings, Hidden, v => s.Hidden = v, formatterResolver);
			Set<FileSystemStorageImplementation?>(s, settings, StoreType, v => s.FileSystemStorageImplementation = v, formatterResolver);

			var sorting = s.Sorting = new SortingSettings();
			SetArray<string[], string>(s, settings, IndexSortSettings.Fields, v => sorting.Fields = v, v => sorting.Fields = new[] { v },
				formatterResolver);
			SetArray<IndexSortOrder[], IndexSortOrder>(s, settings, Order, v => sorting.Order = v, v => sorting.Order = new[] { v },
				formatterResolver);
			SetArray<IndexSortMode[], IndexSortMode>(s, settings, Mode, v => sorting.Mode = v, v => sorting.Mode = new[] { v }, formatterResolver);
			SetArray<IndexSortMissing[], IndexSortMissing>(s, settings, IndexSortSettings.Missing, v => sorting.Missing = v, v => sorting.Missing = new[] { v },
				formatterResolver);

			s.Queries = new QueriesSettings();
			var queriesCache = s.Queries.Cache = new QueriesCacheSettings();
			Set<bool?>(s, settings, QueriesCacheEnabled, v => queriesCache.Enabled = v, formatterResolver);

			var softDeletes = s.SoftDeletes = new SoftDeleteSettings();
#pragma warning disable 618
			Set<bool?>(s, settings, SoftDeletesEnabled, v => softDeletes.Enabled = v, formatterResolver);
#pragma warning restore 618
			var softDeletesRetention = s.SoftDeletes.Retention = new SoftDeleteRetentionSettings();
			Set<long?>(s, settings, SoftDeletesEnabled, v => softDeletesRetention.Operations = v, formatterResolver);

			IDictionary<string, object> dict = s;
			foreach (var kv in settings)
			{
				var setting = kv.Value;
				// TODO: Find a nicer way to avoid the serialization/deserialization roundtrip
				if (kv.Key == UpdatableIndexSettings.Analysis || kv.Key == "index.analysis")
					s.Analysis = ReserializeAndDeserialize<Analysis>(setting, formatterResolver);
				if (kv.Key == Similarity || kv.Key == "index.similarity")
					s.Similarity = ReserializeAndDeserialize<Similarities>(setting, formatterResolver);
				else
					dict.Add(kv.Key, setting);
			}
		}

		private static T ReserializeAndDeserialize<T>(object setting, IJsonFormatterResolver formatterResolver)
		{
			var bytes = JsonSerializer.Serialize(setting);
			var formatter = formatterResolver.GetFormatter<T>();
			var reader = new JsonReader(bytes);
			return formatter.Deserialize(ref reader, formatterResolver);
		}

		private static void Set<T>(IIndexSettings s, IDictionary<string, object> settings, string key, Action<T> assign,
			IJsonFormatterResolver formatterResolver
		)
		{
			if (!settings.TryGetValue(key, out var setting))
				return;

			var value = ConvertToValue<T>(setting, formatterResolver);
			assign(value);
			s.Add(key, value);
			settings.Remove(key);
		}

		// TODO: Optimize this
		private static T ConvertToValue<T>(object setting, IJsonFormatterResolver formatterResolver)
		{
			if (setting is T t)
				return t;

			if (setting == null)
				return default;

			if (setting is IConvertible)
			{
				var type = typeof(T).IsNullable()
					? Nullable.GetUnderlyingType(typeof(T))
					: typeof(T);

				try
				{
					return (T)Convert.ChangeType(setting, type);
				}
				catch
				{
					// swallow exception and fall through to reserializing
				}
			}

			var writer = new JsonWriter();
			formatterResolver.GetFormatter<object>().Serialize(ref writer, setting, formatterResolver);
			var reader = new JsonReader(writer.GetBuffer().Array, 0);
			var value = formatterResolver.GetFormatter<T>().Deserialize(ref reader, formatterResolver);

			return value;
		}

		private static void SetArray<TArray, TItem>(IIndexSettings s, IDictionary<string, object> settings, string key, Action<TArray> assign,
			Action<TItem> assign2, IJsonFormatterResolver formatterResolver
		)
			where TArray : IEnumerable<TItem>
		{
			if (!settings.TryGetValue(key, out var v)) return;

			if (!(v is string) && v is IEnumerable)
			{
				var value = ConvertToValue<TArray>(v, formatterResolver);
				assign(value);
				s.Add(key, value);
			}
			else
			{
				var value = ConvertToValue<TItem>(v, formatterResolver);
				assign2(value);
				s.Add(key, value);
			}
			settings.Remove(key);
		}

		private class IndexSettingsDictionaryFormatter : VerbatimDictionaryInterfaceKeysFormatter<string, object>
		{
			protected override bool SkipValue(KeyValuePair<string, object> entry) =>
				entry.Key != RefreshInterval && base.SkipValue(entry);
		}
	}
}
