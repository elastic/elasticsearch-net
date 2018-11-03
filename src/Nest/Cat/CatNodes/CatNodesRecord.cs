using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class CatNodesRecord : ICatRecord
	{
		public string Build => _b ?? _build;
		public string CompletionSize => _completionSize ?? _cs ?? _completion_size;

		[JsonProperty("cpu")]
		public string CPU { get; internal set; }

		public string DiskAvailable => _diskAvail ?? _disk ?? _d ?? _disk_avail;
		public string FielddataEvictions => _fielddataEvictions ?? _fe ?? _fielddata_evictions;
		public string FielddataMemory => _fielddataMemory ?? _fm ?? _fielddata_memory_size;
		public int? FileDescriptorCurrent => _fileDescriptorCurrent ?? _fdc ?? _file_desc_current;
		public int? FileDescriptorMax => _fileDescriptorMax ?? _fdm ?? _file_desc_max;
		public int? FileDescriptorPercent => _fileDescriptorPercent ?? _fdp ?? _file_desc_percent;
		public string FilterCacheEvictions => _filterCacheEvictions ?? _fce ?? _filter_cache_evictions;
		public string FilterCacheMemory => _filterCacheMemory ?? _fcm ?? _filter_cache_memory_size;
		public string FlushTotal => _flushTotal ?? _ft ?? _flush_total;
		public string FlushTotalTime => _flushTotalTime ?? _ftt ?? _flush_total_time;
		public string GetCurrent => _getCurrent ?? _gc ?? _get_current;
		public string GetExistsTime => _getExistsTime ?? _geti ?? _get_exists_time;
		public string GetExistsTotal => _getExistsTotal ?? _geto ?? _get_exists_total;
		public string GetMissingTime => _getMissingTime ?? _gmti ?? _get_missing_time;
		public string GetMissingTotal => _getMissingTotal ?? _gmto ?? _get_missing_total;
		public string GetTime => _getTime ?? _gti ?? _get_time;
		public string GetTotal => _getTotal ?? _gto ?? _get_total;
		public string HeapCurrent => _heapCurrent ?? _hc ?? _heap_current;
		public string HeapMax => _heapMax ?? _hm ?? _heap_max;
		public string HeapPercent => _heapPercent ?? _hp ?? _heap_percent;
		public string IdCacheMemory => _idCacheMemory ?? _im ?? _id_cache_memory_size;
		public string IndexingDeleteCurrent => _indexingDeleteCurrent ?? _idcs ?? _indexing_delete_current;
		public string IndexingDeleteTime => _indexingDeleteTime ?? _idti ?? _indexing_delete_time;
		public string IndexingDeleteTotal => _indexingDeleteTotal ?? _idto ?? _indexing_delete_total;
		public string IndexingIndexCurrent => _indexingIndexCurrent ?? _iic ?? _indexing_index_current;
		public string IndexingIndexTime => _indexingIndexTime ?? _iiti ?? _indexing_index_time;
		public string IndexingIndexTotal => _indexingIndexTotal ?? _iito ?? _indexing_index_total;
		public string Ip => _i ?? _ip;
		public string Jdk => _j ?? _jdk;

		[JsonProperty("load_15m")]
		public string LoadFifteenMinute { get; internal set; }

		[JsonProperty("load_5m")]
		public string LoadFiveMinute { get; internal set; }

		[JsonProperty("load_1m")]
		public string LoadOneMinute { get; internal set; }

		public string Master => _m ?? _master;
		public string MergesCurrent => _mergesCurrent ?? _mc ?? _merges_current;
		public string MergesCurrentDocs => _mergesCurrentDocs ?? _mcd ?? _merges_current_docs;
		public string MergesCurrentSize => _mergesCurrentSize ?? _mcs ?? _merges_current_size;
		public string MergesTotal => _mergesTotal ?? _mt ?? _merges_total;
		public string MergesTotalDocs => _mergesTotalDocs ?? _mtd ?? _merges_total_docs;
		public string MergesTotalTime => _mergesTotalTime ?? _mtt ?? _merges_total_time;
		public string Name => _n ?? _name;
		public string NodeId => _id ?? _nodeId;
		public string NodeRole => _nodeRole ?? _data_client ?? _dc ?? _r ?? _node_role;
		public string PercolateCurrent => _percolateCurrent ?? _pc ?? _percolate_current;
		public string PercolateMemory => _percolateMemory ?? _pm ?? _percolate_memory_size;
		public string PercolateQueries => _percolate_queries ?? _pq ?? _percolate_queries;
		public string PercolateTime => _percolateTime ?? _pti ?? _percolate_time;
		public string PercolateTotal => _percolateTotal ?? _pto ?? _percolate_total;
		public string Pid => _p ?? _pid;
		public string Port => _po ?? _port;
		public string RamCurrent => _ramCurrent ?? _rc ?? _ram_current;
		public string RamMax => _ramMax ?? _rm ?? _ram_max;
		public string RamPercent => _ramPercent ?? _rp ?? _ram_percent;
		public string RefreshTime => _refreshTime ?? _rti ?? _refreshTime;
		public string RefreshTotal => _refreshTotal ?? _rto ?? _refresh_total;
		public string SearchFetchCurrent => _searchFetchCurrent ?? _sfc ?? _search_fetch_current;
		public string SearchFetchTime => _searchFetchTime ?? _sfti ?? _search_fetch_time;
		public string SearchFetchTotal => _searchFetchTotal ?? _sfto ?? _searchFetchTotal;
		public string SearchOpenContexts => _searchOpenContexts ?? _so ?? _search_open_contexts;
		public string SearchQueryCurrent => _searchQueryCurrent ?? _sqc ?? _search_query_current;
		public string SearchQueryTime => _searchQueryTime ?? _sqti ?? _search_query_time;
		public string SearchQueryTotal => _searchQueryTotal ?? _sqto ?? _search_query_total;
		public string SegmentsCount => _segmentsCount ?? _sc ?? _segmentsCount;
		public string SegmentsIndexWriterMaxMemory => _segmentsIndexWriterMaxMemory ?? _siwmx ?? _segments_index_writer_max_memory;
		public string SegmentsIndexWriterMemory => _segmentsIndexWriterMemory ?? _siwm ?? _segments_index_writer_memory;
		public string SegmentsMemory => _segmentsMemory ?? _sm ?? _segments_memory;
		public string SegmentsVersionMapMemory => _segmentsVersionMapMemory ?? _svmm ?? _segments_version_map_memory;
		public string Uptime => _u ?? _uptime;
		public string Version => _v ?? _version;

		[JsonProperty("b")]
		internal string _b { get; set; }

		[JsonProperty("build")]
		internal string _build { get; set; }

		[JsonProperty("completion.size")]
		internal string _completion_size { get; set; }

		[JsonProperty("completionSize")]
		internal string _completionSize { get; set; }

		[JsonProperty("cs")]
		internal string _cs { get; set; }

		[JsonProperty("d")]
		internal string _d { get; set; }

		[JsonProperty("data/client")]
		internal string _data_client { get; set; }

		[JsonProperty("dc")]
		internal string _dc { get; set; }

		[JsonProperty("disk")]
		internal string _disk { get; set; }

		[JsonProperty("disk.avail")]
		internal string _disk_avail { get; set; }

		[JsonProperty("diskAvail")]
		internal string _diskAvail { get; set; }

		[JsonProperty("fce")]
		internal string _fce { get; set; }

		[JsonProperty("fcm")]
		internal string _fcm { get; set; }

		[JsonProperty("fdc")]
		internal int? _fdc { get; set; }

		[JsonProperty("fdm")]
		internal int? _fdm { get; set; }

		[JsonProperty("fdp")]
		internal int? _fdp { get; set; }

		[JsonProperty("fe")]
		internal string _fe { get; set; }

		[JsonProperty("fielddata.evictions")]
		internal string _fielddata_evictions { get; set; }

		[JsonProperty("fielddata.memory_size")]
		internal string _fielddata_memory_size { get; set; }

		[JsonProperty("fielddataEvictions")]
		internal string _fielddataEvictions { get; set; }

		[JsonProperty("fielddataMemory")]
		internal string _fielddataMemory { get; set; }

		[JsonProperty("file_desc.current")]
		internal int? _file_desc_current { get; set; }

		[JsonProperty("file_desc.max")]
		internal int? _file_desc_max { get; set; }

		[JsonProperty("file_desc.percent")]
		internal int? _file_desc_percent { get; set; }

		[JsonProperty("fileDescriptorCurrent")]
		internal int? _fileDescriptorCurrent { get; set; }

		[JsonProperty("fileDescriptorMax")]
		internal int? _fileDescriptorMax { get; set; }

		[JsonProperty("fileDescriptorPercent")]
		internal int? _fileDescriptorPercent { get; set; }

		[JsonProperty("filter_cache.evictions")]
		internal string _filter_cache_evictions { get; set; }

		[JsonProperty("filter_cache.memory_size")]
		internal string _filter_cache_memory_size { get; set; }

		[JsonProperty("filterCacheEvictions")]
		internal string _filterCacheEvictions { get; set; }

		[JsonProperty("filterCacheMemory")]
		internal string _filterCacheMemory { get; set; }

		[JsonProperty("flush.total")]
		internal string _flush_total { get; set; }

		[JsonProperty("flush.total_time")]
		internal string _flush_total_time { get; set; }

		[JsonProperty("flushTotal")]
		internal string _flushTotal { get; set; }

		[JsonProperty("flushTotalTime")]
		internal string _flushTotalTime { get; set; }

		[JsonProperty("fm")]
		internal string _fm { get; set; }

		[JsonProperty("ft")]
		internal string _ft { get; set; }

		[JsonProperty("ftt")]
		internal string _ftt { get; set; }

		[JsonProperty("gc")]
		internal string _gc { get; set; }

		[JsonProperty("get.current")]
		internal string _get_current { get; set; }

		[JsonProperty("get.exists_time")]
		internal string _get_exists_time { get; set; }

		[JsonProperty("get.exists_total")]
		internal string _get_exists_total { get; set; }

		[JsonProperty("get.missing_time")]
		internal string _get_missing_time { get; set; }

		[JsonProperty("get.missing_total")]
		internal string _get_missing_total { get; set; }

		[JsonProperty("get.time")]
		internal string _get_time { get; set; }

		[JsonProperty("get.total")]
		internal string _get_total { get; set; }

		[JsonProperty("getCurrent")]
		internal string _getCurrent { get; set; }

		[JsonProperty("getExistsTime")]
		internal string _getExistsTime { get; set; }

		[JsonProperty("getExistsTotal")]
		internal string _getExistsTotal { get; set; }

		[JsonProperty("geti")]
		internal string _geti { get; set; }

		[JsonProperty("getMissingTime")]
		internal string _getMissingTime { get; set; }

		[JsonProperty("getMissingTotal")]
		internal string _getMissingTotal { get; set; }

		[JsonProperty("geto")]
		internal string _geto { get; set; }

		[JsonProperty("getTime")]
		internal string _getTime { get; set; }

		[JsonProperty("getTotal")]
		internal string _getTotal { get; set; }

		[JsonProperty("gmti")]
		internal string _gmti { get; set; }

		[JsonProperty("gmto")]
		internal string _gmto { get; set; }

		[JsonProperty("gti")]
		internal string _gti { get; set; }

		[JsonProperty("gto")]
		internal string _gto { get; set; }

		[JsonProperty("hc")]
		internal string _hc { get; set; }

		[JsonProperty("heap.current")]
		internal string _heap_current { get; set; }

		[JsonProperty("heap.max")]
		internal string _heap_max { get; set; }

		[JsonProperty("heap.percent")]
		internal string _heap_percent { get; set; }

		[JsonProperty("heapCurrent")]
		internal string _heapCurrent { get; set; }

		[JsonProperty("heapMax")]
		internal string _heapMax { get; set; }

		[JsonProperty("heapPercent")]
		internal string _heapPercent { get; set; }

		[JsonProperty("hm")]
		internal string _hm { get; set; }

		[JsonProperty("hp")]
		internal string _hp { get; set; }

		[JsonProperty("i")]
		internal string _i { get; set; }

		[JsonProperty("id")]
		internal string _id { get; set; }

		[JsonProperty("id_cache.memory_size")]
		internal string _id_cache_memory_size { get; set; }

		[JsonProperty("idCacheMemory")]
		internal string _idCacheMemory { get; set; }

		[JsonProperty("idc")]
		internal string _idcs { get; set; }

		[JsonProperty("idti")]
		internal string _idti { get; set; }

		[JsonProperty("idto")]
		internal string _idto { get; set; }

		[JsonProperty("iic")]
		internal string _iic { get; set; }

		[JsonProperty("iiti")]
		internal string _iiti { get; set; }

		[JsonProperty("iito")]
		internal string _iito { get; set; }

		[JsonProperty("im")]
		internal string _im { get; set; }

		[JsonProperty("indexing.delete_current")]
		internal string _indexing_delete_current { get; set; }

		[JsonProperty("indexing.delete_time")]
		internal string _indexing_delete_time { get; set; }

		[JsonProperty("indexing.delete_total")]
		internal string _indexing_delete_total { get; set; }

		[JsonProperty("indexing.index_current")]
		internal string _indexing_index_current { get; set; }

		[JsonProperty("indexing.index_time")]
		internal string _indexing_index_time { get; set; }

		[JsonProperty("indexing.index_total")]
		internal string _indexing_index_total { get; set; }

		[JsonProperty("indexingDeleteCurrent")]
		internal string _indexingDeleteCurrent { get; set; }

		[JsonProperty("indexingDeleteTime")]
		internal string _indexingDeleteTime { get; set; }

		[JsonProperty("indexingDeleteTotal")]
		internal string _indexingDeleteTotal { get; set; }

		[JsonProperty("indexingIndexCurrent")]
		internal string _indexingIndexCurrent { get; set; }

		[JsonProperty("indexingIndexTime")]
		internal string _indexingIndexTime { get; set; }

		[JsonProperty("indexingIndexTotal")]
		internal string _indexingIndexTotal { get; set; }

		[JsonProperty("ip")]
		internal string _ip { get; set; }

		[JsonProperty("j")]
		internal string _j { get; set; }

		[JsonProperty("jdk")]
		internal string _jdk { get; set; }

		[JsonProperty("m")]
		internal string _m { get; set; }

		[JsonProperty("master")]
		internal string _master { get; set; }

		[JsonProperty("mc")]
		internal string _mc { get; set; }

		[JsonProperty("mcd")]
		internal string _mcd { get; set; }

		[JsonProperty("mcs")]
		internal string _mcs { get; set; }

		[JsonProperty("merges.current")]
		internal string _merges_current { get; set; }

		[JsonProperty("merges.current_docs")]
		internal string _merges_current_docs { get; set; }

		[JsonProperty("merges.current_size")]
		internal string _merges_current_size { get; set; }

		[JsonProperty("merges.total")]
		internal string _merges_total { get; set; }

		[JsonProperty("merges.total_docs")]
		internal string _merges_total_docs { get; set; }

		[JsonProperty("merges.total_time")]
		internal string _merges_total_time { get; set; }

		[JsonProperty("mergesCurrent")]
		internal string _mergesCurrent { get; set; }

		[JsonProperty("mergesCurrentDocs")]
		internal string _mergesCurrentDocs { get; set; }

		[JsonProperty("mergesCurrentSize")]
		internal string _mergesCurrentSize { get; set; }

		[JsonProperty("mergesTotal")]
		internal string _mergesTotal { get; set; }

		[JsonProperty("mergesTotalDocs")]
		internal string _mergesTotalDocs { get; set; }

		[JsonProperty("mergesTotalTime")]
		internal string _mergesTotalTime { get; set; }

		[JsonProperty("mt")]
		internal string _mt { get; set; }

		[JsonProperty("mtd")]
		internal string _mtd { get; set; }

		[JsonProperty("mtt")]
		internal string _mtt { get; set; }

		[JsonProperty("n")]
		internal string _n { get; set; }

		[JsonProperty("name")]
		internal string _name { get; set; }

		[JsonProperty("node.role")]
		internal string _node_role { get; set; }

		[JsonProperty("nodeId")]
		internal string _nodeId { get; set; }

		[JsonProperty("nodeRole")]
		internal string _nodeRole { get; set; }

		[JsonProperty("p")]
		internal string _p { get; set; }

		[JsonProperty("pc")]
		internal string _pc { get; set; }

		[JsonProperty("percolate.current")]
		internal string _percolate_current { get; set; }

		[JsonProperty("percolate.memory_size")]
		internal string _percolate_memory_size { get; set; }

		[JsonProperty("percolate.queries")]
		internal string _percolate_queries { get; set; }

		[JsonProperty("percolate.time")]
		internal string _percolate_time { get; set; }

		[JsonProperty("percolate.total")]
		internal string _percolate_total { get; set; }

		[JsonProperty("percolateCurrent")]
		internal string _percolateCurrent { get; set; }

		[JsonProperty("percolateMemory")]
		internal string _percolateMemory { get; set; }

		[JsonProperty("percolateQueries")]
		internal string _percolateQueries { get; set; }

		[JsonProperty("percolateTime")]
		internal string _percolateTime { get; set; }

		[JsonProperty("percolateTotal")]
		internal string _percolateTotal { get; set; }

		[JsonProperty("pid")]
		internal string _pid { get; set; }

		[JsonProperty("pm")]
		internal string _pm { get; set; }

		[JsonProperty("po")]
		internal string _po { get; set; }

		[JsonProperty("port")]
		internal string _port { get; set; }

		[JsonProperty("pq")]
		internal string _pq { get; set; }

		[JsonProperty("pti")]
		internal string _pti { get; set; }

		[JsonProperty("pto")]
		internal string _pto { get; set; }

		[JsonProperty("r")]
		internal string _r { get; set; }

		[JsonProperty("ram.current")]
		internal string _ram_current { get; set; }

		[JsonProperty("ram.max")]
		internal string _ram_max { get; set; }

		[JsonProperty("ram.percent")]
		internal string _ram_percent { get; set; }

		[JsonProperty("ramCurrent")]
		internal string _ramCurrent { get; set; }

		[JsonProperty("ramMax")]
		internal string _ramMax { get; set; }

		[JsonProperty("ramPercent")]
		internal string _ramPercent { get; set; }

		[JsonProperty("rc")]
		internal string _rc { get; set; }

		[JsonProperty("refresh.time")]
		internal string _refresh_time { get; set; }

		[JsonProperty("refresh.total")]
		internal string _refresh_total { get; set; }

		[JsonProperty("refreshTime")]
		internal string _refreshTime { get; set; }

		[JsonProperty("refreshTotal")]
		internal string _refreshTotal { get; set; }

		[JsonProperty("rm")]
		internal string _rm { get; set; }

		[JsonProperty("rp")]
		internal string _rp { get; set; }

		[JsonProperty("rti")]
		internal string _rti { get; set; }

		[JsonProperty("rto")]
		internal string _rto { get; set; }

		[JsonProperty("sc")]
		internal string _sc { get; set; }

		[JsonProperty("search.fetch_current")]
		internal string _search_fetch_current { get; set; }

		[JsonProperty("search.fetch_time")]
		internal string _search_fetch_time { get; set; }

		[JsonProperty("search.fetch_total")]
		internal string _search_fetch_total { get; set; }

		[JsonProperty("search.open_contexts")]
		internal string _search_open_contexts { get; set; }

		[JsonProperty("search.query_current")]
		internal string _search_query_current { get; set; }

		[JsonProperty("search.query_time")]
		internal string _search_query_time { get; set; }

		[JsonProperty("search.query_total")]
		internal string _search_query_total { get; set; }

		[JsonProperty("searchFetchCurrent")]
		internal string _searchFetchCurrent { get; set; }

		[JsonProperty("searchFetchTime")]
		internal string _searchFetchTime { get; set; }

		[JsonProperty("searchFetchTotal")]
		internal string _searchFetchTotal { get; set; }

		[JsonProperty("searchOpenContexts")]
		internal string _searchOpenContexts { get; set; }

		[JsonProperty("searchQueryCurrent")]
		internal string _searchQueryCurrent { get; set; }

		[JsonProperty("searchQueryTime")]
		internal string _searchQueryTime { get; set; }

		[JsonProperty("searchQueryTotal")]
		internal string _searchQueryTotal { get; set; }

		[JsonProperty("segments.count")]
		internal string _segments_count { get; set; }

		[JsonProperty("segments.index_writer_max_memory")]
		internal string _segments_index_writer_max_memory { get; set; }

		[JsonProperty("segments.index_writer_memory")]
		internal string _segments_index_writer_memory { get; set; }

		[JsonProperty("segments.memory")]
		internal string _segments_memory { get; set; }

		[JsonProperty("segments.version_map_memory")]
		internal string _segments_version_map_memory { get; set; }

		[JsonProperty("segmentsCount")]
		internal string _segmentsCount { get; set; }

		[JsonProperty("segmentsIndexWriterMaxMemory")]
		internal string _segmentsIndexWriterMaxMemory { get; set; }

		[JsonProperty("segmentsIndexWriterMemory")]
		internal string _segmentsIndexWriterMemory { get; set; }

		[JsonProperty("segmentsMemory")]
		internal string _segmentsMemory { get; set; }

		[JsonProperty("segmentsVersionMapMemory")]
		internal string _segmentsVersionMapMemory { get; set; }

		[JsonProperty("sfc")]
		internal string _sfc { get; set; }

		[JsonProperty("sfti")]
		internal string _sfti { get; set; }

		[JsonProperty("sfto")]
		internal string _sfto { get; set; }

		[JsonProperty("siwm")]
		internal string _siwm { get; set; }

		[JsonProperty("siwmx")]
		internal string _siwmx { get; set; }

		[JsonProperty("sm")]
		internal string _sm { get; set; }

		[JsonProperty("so")]
		internal string _so { get; set; }

		[JsonProperty("sqc")]
		internal string _sqc { get; set; }

		[JsonProperty("sqti")]
		internal string _sqti { get; set; }

		[JsonProperty("sqto")]
		internal string _sqto { get; set; }

		[JsonProperty("svmm")]
		internal string _svmm { get; set; }

		[JsonProperty("u")]
		internal string _u { get; set; }

		[JsonProperty("uptime")]
		internal string _uptime { get; set; }

		[JsonProperty("v")]
		internal string _v { get; set; }

		[JsonProperty("version")]
		internal string _version { get; set; }
	}
}
