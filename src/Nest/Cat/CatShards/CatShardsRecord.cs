// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatShardsRecord : ICatRecord
	{
		[DataMember(Name ="completion.size")]
		public string CompletionSize { get; set; }

		[DataMember(Name ="docs")]
		public string Docs { get; set; }

		[DataMember(Name ="fielddata.evictions")]
		public string FielddataEvictions { get; set; }

		[DataMember(Name ="fielddata.memory_size")]
		public string FielddataMemorySize { get; set; }

		[DataMember(Name ="filter_cache.memory_size")]
		public string FilterCacheMemorySize { get; set; }

		[DataMember(Name ="flush.total")]
		public string FlushTotal { get; set; }

		[DataMember(Name ="flush.total_time")]
		public string FlushTotalTime { get; set; }

		[DataMember(Name ="get.current")]
		public string GetCurrent { get; set; }

		[DataMember(Name ="get.exists_time")]
		public string GetExistsTime { get; set; }

		[DataMember(Name ="get.exists_total")]
		public string GetExistsTotal { get; set; }

		[DataMember(Name ="get.missing_time")]
		public string GetMissingTime { get; set; }

		[DataMember(Name ="get.missing_total")]
		public string GetMissingTotal { get; set; }

		[DataMember(Name ="get.time")]
		public string GetTime { get; set; }

		[DataMember(Name ="get.total")]
		public string GetTotal { get; set; }

		[DataMember(Name ="id")]
		public string Id { get; set; }

		[DataMember(Name ="id_cache.memory_size")]
		public string IdCacheMemorySize { get; set; }

		[DataMember(Name ="index")]
		public string Index { get; set; }

		[DataMember(Name ="indexing.delete_current")]
		public string IndexingDeleteCurrent { get; set; }

		[DataMember(Name ="indexing.delete_time")]
		public string IndexingDeleteTime { get; set; }

		[DataMember(Name ="indexing.delete_total")]
		public string IndexingDeleteTotal { get; set; }

		[DataMember(Name ="indexing.index_current")]
		public string IndexingIndexCurrent { get; set; }

		[DataMember(Name ="indexing.index_time")]
		public string IndexingIndexTime { get; set; }

		[DataMember(Name ="indexing.index_total")]
		public string IndexingIndexTotal { get; set; }

		[DataMember(Name ="ip")]
		public string Ip { get; set; }

		[DataMember(Name ="merges.current")]
		public string MergesCurrent { get; set; }

		[DataMember(Name ="merges.current_docs")]
		public string MergesCurrentDocs { get; set; }

		[DataMember(Name ="merges.current_size")]
		public string MergesCurrentSize { get; set; }

		[DataMember(Name ="merges.total_docs")]
		public string MergesTotalDocs { get; set; }

		[DataMember(Name ="merges.total_size")]
		public string MergesTotalSize { get; set; }

		[DataMember(Name ="merges.total_time")]
		public string MergesTotalTime { get; set; }

		[DataMember(Name ="node")]
		public string Node { get; set; }

		[DataMember(Name ="percolate.current")]
		public string PercolateCurrent { get; set; }

		[DataMember(Name ="percolate.memory_size")]
		public string PercolateMemorySize { get; set; }

		[DataMember(Name ="percolate.queries")]
		public string PercolateQueries { get; set; }

		[DataMember(Name ="percolate.time")]
		public string PercolateTime { get; set; }

		[DataMember(Name ="percolate.total")]
		public string PercolateTotal { get; set; }

		[DataMember(Name ="prirep")]
		public string PrimaryOrReplica { get; set; }

		[DataMember(Name ="refresh.time")]
		public string RefreshTime { get; set; }

		[DataMember(Name ="refresh.total")]
		public string RefreshTotal { get; set; }

		[DataMember(Name ="search.fetch_current")]
		public string SearchFetchCurrent { get; set; }

		[DataMember(Name ="search.fetch_time")]
		public string SearchFetchTime { get; set; }

		[DataMember(Name ="search.fetch_total")]
		public string SearchFetchTotal { get; set; }

		[DataMember(Name ="search.open_contexts")]
		public string SearchOpenContexts { get; set; }

		[DataMember(Name ="search.query_current")]
		public string SearchQueryCurrent { get; set; }

		[DataMember(Name ="search.query_time")]
		public string SearchQueryTime { get; set; }

		[DataMember(Name ="search.query_total")]
		public string SearchQueryTotal { get; set; }

		[DataMember(Name ="segments.count")]
		public string SegmentsCount { get; set; }

		[DataMember(Name ="segments.fixed_bitset_memory")]
		public string SegmentsFixedBitsetMemory { get; set; }

		[DataMember(Name ="segments.index_writer_max_memory")]
		public string SegmentsIndexWriterMaxMemory { get; set; }

		[DataMember(Name ="segments.index_writer_memory")]
		public string SegmentsIndexWriterMemory { get; set; }

		[DataMember(Name ="segments.memory")]
		public string SegmentsMemory { get; set; }

		[DataMember(Name ="segments.version_map_memory")]
		public string SegmentsVersionMapMemory { get; set; }

		[DataMember(Name ="shard")]
		public string Shard { get; set; }

		[DataMember(Name ="state")]
		public string State { get; set; }

		[DataMember(Name ="store")]
		public string Store { get; set; }

		[DataMember(Name ="warmer.current")]
		public string WarmerCurrent { get; set; }

		[DataMember(Name ="warmer.total")]
		public string WarmerTotal { get; set; }

		[DataMember(Name ="warmer.total_time")]
		public string WarmerTotalTime { get; set; }
	}
}
