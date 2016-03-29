using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class CatShardsRecord : ICatRecord
	{
		[JsonProperty("index")]
		public string Index { get; set; }

		[JsonProperty("shard")]
		public string Shard { get; set; }

		[JsonProperty("prirep")]
		public string PrimaryOrReplica { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("docs")]
		public string Docs { get; set; }

		[JsonProperty("store")]
		public string Store { get; set; }

		[JsonProperty("ip")]
		public string Ip { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }

		[JsonProperty("completion.size")]
		public string CompletionSize { get; set; }

		[JsonProperty("fielddata.memory_size")]
		public string FielddataMemorySize { get; set; }

		[JsonProperty("fielddata.evictions")]
		public string FielddataEvictions { get; set; }

		[JsonProperty("filter_cache.memory_size")]
		public string FilterCacheMemorySize { get; set; }
		
		[JsonProperty("flush.total")]
		public string FlushTotal { get; set; }

		[JsonProperty("flush.total_time")]
		public string FlushTotalTime { get; set; }

		[JsonProperty("get.current")]
		public string GetCurrent { get; set; }

		[JsonProperty("get.time")]
		public string GetTime { get; set; }

		[JsonProperty("get.total")]
		public string GetTotal { get; set; }

		[JsonProperty("get.exists_time")]
		public string GetExistsTime { get; set; }

		[JsonProperty("get.exists_total")]
		public string GetExistsTotal { get; set; }

		[JsonProperty("get.missing_time")]
		public string GetMissingTime { get; set; }

		[JsonProperty("get.missing_total")]
		public string GetMissingTotal { get; set; }

		[JsonProperty("id_cache.memory_size")]
		public string IdCacheMemorySize { get; set; }

		[JsonProperty("indexing.delete_current")]
		public string IndexingDeleteCurrent { get; set; }

		[JsonProperty("indexing.delete_time")]
		public string IndexingDeleteTime { get; set; }

		[JsonProperty("indexing.delete_total")]
		public string IndexingDeleteTotal { get; set; }

		[JsonProperty("indexing.index_current")]
		public string IndexingIndexCurrent { get; set; }

		[JsonProperty("indexing.index_time")]
		public string IndexingIndexTime { get; set; }

		[JsonProperty("indexing.index_total")]
		public string IndexingIndexTotal { get; set; }

		[JsonProperty("merges.current")]
		public string MergesCurrent { get; set; }

		[JsonProperty("merges.current_docs")]
		public string MergesCurrentDocs { get; set; }

		[JsonProperty("merges.current_size")]
		public string MergesCurrentSize { get; set; }
		
		[JsonProperty("merges.total_docs")]
		public string MergesTotalDocs { get; set; }
		
		[JsonProperty("merges.total_size")]
		public string MergesTotalSize { get; set; }
	
		[JsonProperty("merges.total_time")]
		public string MergesTotalTime { get; set; }
	
		[JsonProperty("percolate.current")]
		public string PercolateCurrent { get; set; }
		
		[JsonProperty("percolate.memory_size")]
		public string PercolateMemorySize { get; set; }
		
		[JsonProperty("percolate.queries")]
		public string PercolateQueries { get; set; }
	
		[JsonProperty("percolate.time")]
		public string PercolateTime { get; set; }
		
		[JsonProperty("percolate.total")]
		public string PercolateTotal { get; set; }
		
		[JsonProperty("refresh.total")]
		public string RefreshTotal { get; set; }
		
		[JsonProperty("refresh.time")]
		public string RefreshTime { get; set; }
		
		[JsonProperty("search.fetch_current")]
		public string SearchFetchCurrent { get; set; }
		
		[JsonProperty("search.fetch_time")]
		public string SearchFetchTime { get; set; }
		
		[JsonProperty("search.fetch_total")]
		public string SearchFetchTotal { get; set; }
		
		[JsonProperty("search.open_contexts")]
		public string SearchOpenContexts { get; set; }
		
		[JsonProperty("search.query_current")]
		public string SearchQueryCurrent { get; set; }
		
		[JsonProperty("search.query_time")]
		public string SearchQueryTime { get; set; }
		
		[JsonProperty("search.query_total")]
		public string SearchQueryTotal { get; set; }
		
		[JsonProperty("segments.count")]
		public string SegmentsCount { get; set; }
		
		[JsonProperty("segments.memory")]
		public string SegmentsMemory { get; set; }
		
		[JsonProperty("segments.index_writer_memory")]
		public string SegmentsIndexWriterMemory { get; set; }
		
		[JsonProperty("segments.index_writer_max_memory")]
		public string SegmentsIndexWriterMaxMemory { get; set; }
		
		[JsonProperty("segments.version_map_memory")]
		public string SegmentsVersionMapMemory { get; set; }
		
		[JsonProperty("segments.fixed_bitset_memory")]
		public string SegmentsFixedBitsetMemory { get; set; }
		
		[JsonProperty("warmer.current")]
		public string WarmerCurrent { get; set; }
		
		[JsonProperty("warmer.total")]
		public string WarmerTotal { get; set; }
		
		[JsonProperty("warmer.total_time")]
		public string WarmerTotalTime { get; set; }
	}
}