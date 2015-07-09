using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class CatNodesRecord : ICatRecord
	{
		[JsonProperty("id")]
		internal string _id { get; set; }
		[JsonProperty("nodeId")]
		internal string _nodeId { get; set; }
		public string NodeId { get { return this._id ?? this._nodeId; }}

		[JsonProperty("pid")]
		internal string _pid { get; set; }
		[JsonProperty("p")]
		internal string _p { get; set; }
		public string Pid { get { return this._p ?? this._pid; }}

		[JsonProperty("host")]
		internal string _host { get; set; }
		[JsonProperty("h")]
		internal string _h { get; set; }
		public string Host { get { return this._h ?? this._host; }}

		[JsonProperty("ip")]
		internal string _ip { get; set; }
		[JsonProperty("i")]
		internal string _i { get; set; }
		public string Ip { get { return this._i ?? this._ip; }}

		[JsonProperty("port")]
		internal string _port { get; set; }
		[JsonProperty("po")]
		internal string _po { get; set; }
		public string Port { get { return this._po ?? this._port; }}

		[JsonProperty("version")]
		internal string _version { get; set; }
		[JsonProperty("v")]
		internal string _v { get; set; }
		public string Version { get { return this._v ?? this._version; }}

		[JsonProperty("build")]
		internal string _build { get; set; }
		[JsonProperty("b")]
		internal string _b { get; set; }
		public string Build { get { return this._b ?? this._build; }}

		[JsonProperty("jdk")]
		internal string _jdk { get; set; }
		[JsonProperty("j")]
		internal string _j { get; set; }
		public string Jdk { get { return this._j ?? this._jdk; }}

		[JsonProperty("disk.avail")]
		internal string _disk_avail { get; set; }
		[JsonProperty("d")]
		internal string _d { get; set; }
		[JsonProperty("disk")]
		internal string _disk { get; set; }
		[JsonProperty("diskAvail")]
		internal string _diskAvail { get; set; }
		public string DiskAvailable { get { return this._diskAvail ?? this._disk ?? this._d ?? this._disk_avail; }}

		[JsonProperty("heap.current")]
		internal string _heap_current { get; set; }
		[JsonProperty("hc")]
		internal string _hc { get; set; }
		[JsonProperty("heapCurrent")]
		internal string _heapCurrent { get; set; }
		public string HeapCurrent { get { return this._heapCurrent ?? this._hc ?? this._heap_current; }}

		[JsonProperty("heap.percent")]
		internal string _heap_percent { get; set; }
		[JsonProperty("hp")]
		internal string _hp { get; set; }
		[JsonProperty("heapPercent")]
		internal string _heapPercent { get; set; }
		public string HeapPercent { get { return this._heapPercent ?? this._hp ?? this._heap_percent; }}

		[JsonProperty("heap.max")]
		internal string _heap_max { get; set; }
		[JsonProperty("hm")]
		internal string _hm { get; set; }
		[JsonProperty("heapMax")]
		internal string _heapMax { get; set; }
		public string HeapMax { get { return this._heapMax ?? this._hm ?? this._heap_max; }}

		[JsonProperty("ram.current")]
		internal string _ram_current { get; set; }
		[JsonProperty("rc")]
		internal string _rc { get; set; }
		[JsonProperty("ramCurrent")]
		internal string _ramCurrent { get; set; }
		public string RamCurrent { get { return this._ramCurrent ?? this._rc ?? this._ram_current; }}

		[JsonProperty("ram.percent")]
		internal string _ram_percent { get; set; }
		[JsonProperty("rp")]
		internal string _rp { get; set; }
		[JsonProperty("ramPercent")]
		internal string _ramPercent { get; set; }
		public string RamPercent { get { return this._ramPercent ?? this._rp ?? this._ram_percent; }}

		[JsonProperty("ram.max")]
		internal string _ram_max { get; set; }
		[JsonProperty("rm")]
		internal string _rm { get; set; }
		[JsonProperty("ramMax")]
		internal string _ramMax { get; set; }
		public string RamMax { get { return this._ramMax ?? this._rm ?? this._ram_max; }}

		[JsonProperty("load")]
		internal string _load { get; set; }
		[JsonProperty("l")]
		internal string _l { get; set; }
		public string Load { get { return this._l ?? this._load; }}

		[JsonProperty("uptime")]
		internal string _uptime { get; set; }
		[JsonProperty("u")]
		internal string _u { get; set; }
		public string Uptime { get { return this._u ?? this._uptime; }}

		[JsonProperty("node.role")]
		internal string _node_role { get; set; }
		[JsonProperty("r")]
		internal string _r { get; set; }
		[JsonProperty("dc")]
		internal string _dc { get; set; }
		[JsonProperty("data/client")]
		internal string _data_client { get; set; }
		[JsonProperty("nodeRole")]
		internal string _nodeRole { get; set; }
		public string NodeRole { get { return this._nodeRole ?? this._data_client ?? this._dc ?? this._r ?? this._node_role; }}

		[JsonProperty("master")]
		internal string _master { get; set; }
		[JsonProperty("m")]
		internal string _m { get; set; }
		public string Master { get { return this._m ?? this._master; }}

		[JsonProperty("name")]
		internal string _name { get; set; }
		[JsonProperty("n")]
		internal string _n { get; set; }
		public string Name { get { return this._n ?? this._name; }}

		[JsonProperty("completion.size")]
		internal string _completion_size { get; set; }
		[JsonProperty("cs")]
		internal string _cs { get; set; }
		[JsonProperty("completionSize")]
		internal string _completionSize { get; set; }
		public string CompletionSize { get { return this._completionSize ?? this._cs ?? this._completion_size; }}

		[JsonProperty("fielddata.memory_size")]
		internal string _fielddata_memory_size { get; set; }
		[JsonProperty("fm")]
		internal string _fm { get; set; }
		[JsonProperty("fielddataMemory")]
		internal string _fielddataMemory { get; set; }
		public string FielddataMemory { get { return this._fielddataMemory ?? this._fm ?? this._fielddata_memory_size; }}

		[JsonProperty("fielddata.evictions")]
		internal string _fielddata_evictions { get; set; }
		[JsonProperty("fe")]
		internal string _fe { get; set; }
		[JsonProperty("fielddataEvictions")]
		internal string _fielddataEvictions { get; set; }
		public string FielddataEvictions { get { return this._fielddataEvictions ?? this._fe ?? this._fielddata_evictions; }}

		[JsonProperty("filter_cache.memory_size")]
		internal string _filter_cache_memory_size { get; set; }
		[JsonProperty("fcm")]
		internal string _fcm { get; set; }
		[JsonProperty("filterCacheMemory")]
		internal string _filterCacheMemory { get; set; }
		public string FilterCacheMemory { get { return this._filterCacheMemory ?? this._fcm ?? this._filter_cache_memory_size; }}

		[JsonProperty("filter_cache.evictions")]
		internal string _filter_cache_evictions { get; set; }
		[JsonProperty("fce")]
		internal string _fce { get; set; }
		[JsonProperty("filterCacheEvictions")]
		internal string _filterCacheEvictions { get; set; }
		public string FilterCacheEvictions { get { return this._filterCacheEvictions ?? this._fce ?? this._filter_cache_evictions; }}

		[JsonProperty("flush.total")]
		internal string _flush_total { get; set; }
		[JsonProperty("ft")]
		internal string _ft { get; set; }
		[JsonProperty("flushTotal")]
		internal string _flushTotal { get; set; }
		public string FlushTotal { get { return this._flushTotal ?? this._ft ?? this._flush_total; }}

		[JsonProperty("flush.total_time")]
		internal string _flush_total_time { get; set; }
		[JsonProperty("ftt")]
		internal string _ftt { get; set; }
		[JsonProperty("flushTotalTime")]
		internal string _flushTotalTime { get; set; }
		public string FlushTotalTime { get { return this._flushTotalTime ?? this._ftt ?? this._flush_total_time; }}

		[JsonProperty("file_desc.current")]
		internal int? _file_desc_current { get; set; }
		[JsonProperty("fdc")]
		internal int? _fdc { get; set; }
		[JsonProperty("fileDescriptorCurrent")]
		internal int? _fileDescriptorCurrent { get; set; }
		public int? FileDescriptorCurrent { get { return this._fileDescriptorCurrent ?? this._fdc ?? this._file_desc_current; }}

		[JsonProperty("file_desc.percent")]
		internal int? _file_desc_percent { get; set; }
		[JsonProperty("fdp")]
		internal int? _fdp { get; set; }
		[JsonProperty("fileDescriptorPercent")]
		internal int? _fileDescriptorPercent { get; set; }
		public int? FileDescriptorPercent { get { return this._fileDescriptorPercent ?? this._fdp ?? this._file_desc_percent; }}

		[JsonProperty("file_desc.max")]
		internal int? _file_desc_max { get; set; }
		[JsonProperty("fdm")]
		internal int? _fdm { get; set; }
		[JsonProperty("fileDescriptorMax")]
		internal int? _fileDescriptorMax { get; set; }
		public int? FileDescriptorMax { get { return this._fileDescriptorMax ?? this._fdm ?? this._file_desc_max; }}

		[JsonProperty("get.current")]
		internal string _get_current { get; set; }
		[JsonProperty("gc")]
		internal string _gc { get; set; }
		[JsonProperty("getCurrent")]
		internal string _getCurrent { get; set; }
		public string GetCurrent { get { return this._getCurrent ?? this._gc ?? this._get_current; }}

		[JsonProperty("get.time")]
		internal string _get_time { get; set; }
		[JsonProperty("gti")]
		internal string _gti { get; set; }
		[JsonProperty("getTime")]
		internal string _getTime { get; set; }
		public string GetTime { get { return this._getTime ?? this._gti ?? this._get_time; }}

		[JsonProperty("get.total")]
		internal string _get_total { get; set; }
		[JsonProperty("gto")]
		internal string _gto { get; set; }
		[JsonProperty("getTotal")]
		internal string _getTotal { get; set; }
		public string GetTotal { get { return this._getTotal ?? this._gto ?? this._get_total; }}

		[JsonProperty("get.exists_time")]
		internal string _get_exists_time { get; set; }
		[JsonProperty("geti")]
		internal string _geti { get; set; }
		[JsonProperty("getExistsTime")]
		internal string _getExistsTime { get; set; }
		public string GetExistsTime { get { return this._getExistsTime ?? this._geti ?? this._get_exists_time; }}

		[JsonProperty("get.exists_total")]
		internal string _get_exists_total { get; set; }
		[JsonProperty("geto")]
		internal string _geto { get; set; }
		[JsonProperty("getExistsTotal")]
		internal string _getExistsTotal { get; set; }
		public string GetExistsTotal { get { return this._getExistsTotal ?? this._geto ?? this._get_exists_total; }}

		[JsonProperty("get.missing_time")]
		internal string _get_missing_time { get; set; }
		[JsonProperty("gmti")]
		internal string _gmti { get; set; }
		[JsonProperty("getMissingTime")]
		internal string _getMissingTime { get; set; }
		public string GetMissingTime { get { return this._getMissingTime ?? this._gmti ?? this._get_missing_time; }}

		[JsonProperty("get.missing_total")]
		internal string _get_missing_total { get; set; }
		[JsonProperty("gmto")]
		internal string _gmto { get; set; }
		[JsonProperty("getMissingTotal")]
		internal string _getMissingTotal { get; set; }
		public string GetMissingTotal { get { return this._getMissingTotal ?? this._gmto ?? this._get_missing_total; }}

		[JsonProperty("id_cache.memory_size")]
		internal string _id_cache_memory_size { get; set; }
		[JsonProperty("im")]
		internal string _im { get; set; }
		[JsonProperty("idCacheMemory")]
		internal string _idCacheMemory { get; set; }
		public string IdCacheMemory { get { return this._idCacheMemory ?? this._im ?? this._id_cache_memory_size; }}

		[JsonProperty("indexing.delete_current")]
		internal string _indexing_delete_current { get; set; }
		[JsonProperty("idc")]
		internal string _idcs { get; set; }
		[JsonProperty("indexingDeleteCurrent")]
		internal string _indexingDeleteCurrent { get; set; }
		public string IndexingDeleteCurrent { get { return this._indexingDeleteCurrent ?? this._idcs ?? this._indexing_delete_current; }}

		[JsonProperty("indexing.delete_time")]
		internal string _indexing_delete_time { get; set; }
		[JsonProperty("idti")]
		internal string _idti { get; set; }
		[JsonProperty("indexingDeleteTime")]
		internal string _indexingDeleteTime { get; set; }
		public string IndexingDeleteTime { get { return this._indexingDeleteTime ?? this._idti ?? this._indexing_delete_time; }}

		[JsonProperty("indexing.delete_total")]
		internal string _indexing_delete_total { get; set; }
		[JsonProperty("idto")]
		internal string _idto { get; set; }
		[JsonProperty("indexingDeleteTotal")]
		internal string _indexingDeleteTotal { get; set; }
		public string IndexingDeleteTotal { get { return this._indexingDeleteTotal ?? this._idto ?? this._indexing_delete_total; }}

		[JsonProperty("indexing.index_current")]
		internal string _indexing_index_current { get; set; }
		[JsonProperty("iic")]
		internal string _iic { get; set; }
		[JsonProperty("indexingIndexCurrent")]
		internal string _indexingIndexCurrent { get; set; }
		public string IndexingIndexCurrent { get { return this._indexingIndexCurrent ?? this._iic ?? this._indexing_index_current; }}

		[JsonProperty("indexing.index_time")]
		internal string _indexing_index_time { get; set; }
		[JsonProperty("iiti")]
		internal string _iiti { get; set; }
		[JsonProperty("indexingIndexTime")]
		internal string _indexingIndexTime { get; set; }
		public string IndexingIndexTime { get { return this._indexingIndexTime ?? this._iiti ?? this._indexing_index_time; }}

		[JsonProperty("indexing.index_total")]
		internal string _indexing_index_total { get; set; }
		[JsonProperty("iito")]
		internal string _iito { get; set; }
		[JsonProperty("indexingIndexTotal")]
		internal string _indexingIndexTotal { get; set; }
		public string IndexingIndexTotal { get { return this._indexingIndexTotal ?? this._iito ?? this._indexing_index_total; }}

		[JsonProperty("merges.current")]
		internal string _merges_current { get; set; }
		[JsonProperty("mc")]
		internal string _mc { get; set; }
		[JsonProperty("mergesCurrent")]
		internal string _mergesCurrent { get; set; }
		public string MergesCurrent { get { return this._mergesCurrent ?? this._mc ?? this._merges_current; }}

		[JsonProperty("merges.current_docs")]
		internal string _merges_current_docs { get; set; }
		[JsonProperty("mcd")]
		internal string _mcd { get; set; }
		[JsonProperty("mergesCurrentDocs")]
		internal string _mergesCurrentDocs { get; set; }
		public string MergesCurrentDocs { get { return this._mergesCurrentDocs ?? this._mcd ?? this._merges_current_docs; }}

		[JsonProperty("merges.current_size")]
		internal string _merges_current_size { get; set; }
		[JsonProperty("mcs")]
		internal string _mcs { get; set; }
		[JsonProperty("mergesCurrentSize")]
		internal string _mergesCurrentSize { get; set; }
		public string MergesCurrentSize { get { return this._mergesCurrentSize ?? this._mcs ?? this._merges_current_size; }}

		[JsonProperty("merges.total")]
		internal string _merges_total { get; set; }
		[JsonProperty("mt")]
		internal string _mt { get; set; }
		[JsonProperty("mergesTotal")]
		internal string _mergesTotal { get; set; }
		public string MergesTotal { get { return this._mergesTotal ?? this._mt ?? this._merges_total; }}

		[JsonProperty("merges.total_docs")]
		internal string _merges_total_docs { get; set; }
		[JsonProperty("mtd")]
		internal string _mtd { get; set; }
		[JsonProperty("mergesTotalDocs")]
		internal string _mergesTotalDocs { get; set; }
		public string MergesTotalDocs { get { return this._mergesTotalDocs ?? this._mtd ?? this._merges_total_docs; }}

		[JsonProperty("merges.total_time")]
		internal string _merges_total_time { get; set; }
		[JsonProperty("mtt")]
		internal string _mtt { get; set; }
		[JsonProperty("mergesTotalTime")]
		internal string _mergesTotalTime { get; set; }
		public string MergesTotalTime { get { return this._mergesTotalTime ?? this._mtt ?? this._merges_total_time; }}

		[JsonProperty("percolate.current")]
		internal string _percolate_current { get; set; }
		[JsonProperty("pc")]
		internal string _pc { get; set; }
		[JsonProperty("percolateCurrent")]
		internal string _percolateCurrent { get; set; }
		public string PercolateCurrent { get { return this._percolateCurrent ?? this._pc ?? this._percolate_current; }}

		[JsonProperty("percolate.memory_size")]
		internal string _percolate_memory_size { get; set; }
		[JsonProperty("pm")]
		internal string _pm { get; set; }
		[JsonProperty("percolateMemory")]
		internal string _percolateMemory { get; set; }
		public string PercolateMemory { get { return this._percolateMemory ?? this._pm ?? this._percolate_memory_size; }}

		[JsonProperty("percolate.queries")]
		internal string _percolate_queries { get; set; }
		[JsonProperty("pq")]
		internal string _pq { get; set; }
		[JsonProperty("percolateQueries")]
		internal string _percolateQueries { get; set; }
		public string PercolateQueries { get { return this._percolate_queries ?? this._pq ?? this._percolate_queries; }}

		[JsonProperty("percolate.time")]
		internal string _percolate_time { get; set; }
		[JsonProperty("pti")]
		internal string _pti { get; set; }
		[JsonProperty("percolateTime")]
		internal string _percolateTime { get; set; }
		public string PercolateTime { get { return this._percolateTime ?? this._pti ?? this._percolate_time; }}

		[JsonProperty("percolate.total")]
		internal string _percolate_total { get; set; }
		[JsonProperty("pto")]
		internal string _pto { get; set; }
		[JsonProperty("percolateTotal")]
		internal string _percolateTotal { get; set; }
		public string PercolateTotal { get { return this._percolateTotal ?? this._pto ?? this._percolate_total; }}

		[JsonProperty("refresh.total")]
		internal string _refresh_total { get; set; }
		[JsonProperty("rto")]
		internal string _rto { get; set; }
		[JsonProperty("refreshTotal")]
		internal string _refreshTotal { get; set; }
		public string RefreshTotal { get { return this._refreshTotal ?? this._rto ?? this._refresh_total; }}

		[JsonProperty("refresh.time")]
		internal string _refresh_time { get; set; }
		[JsonProperty("rti")]
		internal string _rti { get; set; }
		[JsonProperty("refreshTime")]
		internal string _refreshTime { get; set; }
		public string RefreshTime { get { return this._refreshTime ?? this._rti ?? this._refreshTime; }}

		[JsonProperty("search.fetch_current")]
		internal string _search_fetch_current { get; set; }
		[JsonProperty("sfc")]
		internal string _sfc { get; set; }
		[JsonProperty("searchFetchCurrent")]
		internal string _searchFetchCurrent { get; set; }
		public string SearchFetchCurrent { get { return this._searchFetchCurrent ?? this._sfc ?? this._search_fetch_current; }}

		[JsonProperty("search.fetch_time")]
		internal string _search_fetch_time { get; set; }
		[JsonProperty("sfti")]
		internal string _sfti { get; set; }
		[JsonProperty("searchFetchTime")]
		internal string _searchFetchTime { get; set; }
		public string SearchFetchTime { get { return this._searchFetchTime ?? this._sfti ?? this._search_fetch_time; }}

		[JsonProperty("search.fetch_total")]
		internal string _search_fetch_total { get; set; }
		[JsonProperty("sfto")]
		internal string _sfto { get; set; }
		[JsonProperty("searchFetchTotal")]
		internal string _searchFetchTotal { get; set; }
		public string SearchFetchTotal { get { return this._searchFetchTotal ?? this._sfto ?? this._searchFetchTotal; }}

		[JsonProperty("search.open_contexts")]
		internal string _search_open_contexts { get; set; }
		[JsonProperty("so")]
		internal string _so { get; set; }
		[JsonProperty("searchOpenContexts")]
		internal string _searchOpenContexts { get; set; }
		public string SearchOpenContexts { get { return this._searchOpenContexts ?? this._so ?? this._search_open_contexts; }}

		[JsonProperty("search.query_current")]
		internal string _search_query_current { get; set; }
		[JsonProperty("sqc")]
		internal string _sqc { get; set; }
		[JsonProperty("searchQueryCurrent")]
		internal string _searchQueryCurrent { get; set; }
		public string SearchQueryCurrent { get { return this._searchQueryCurrent ?? this._sqc ?? this._search_query_current; }}

		[JsonProperty("search.query_time")]
		internal string _search_query_time { get; set; }
		[JsonProperty("sqti")]
		internal string _sqti { get; set; }
		[JsonProperty("searchQueryTime")]
		internal string _searchQueryTime { get; set; }
		public string SearchQueryTime { get { return this._searchQueryTime ?? this._sqti ?? this._search_query_time; }}

		[JsonProperty("search.query_total")]
		internal string _search_query_total { get; set; }
		[JsonProperty("sqto")]
		internal string _sqto { get; set; }
		[JsonProperty("searchQueryTotal")]
		internal string _searchQueryTotal { get; set; }
		public string SearchQueryTotal { get { return this._searchQueryTotal ?? this._sqto ?? this._search_query_total; }}

		[JsonProperty("segments.count")]
		internal string _segments_count { get; set; }
		[JsonProperty("sc")]
		internal string _sc { get; set; }
		[JsonProperty("segmentsCount")]
		internal string _segmentsCount { get; set; }
		public string SegmentsCount { get { return this._segmentsCount ?? this._sc ?? this._segmentsCount; }}

		[JsonProperty("segments.memory")]
		internal string _segments_memory { get; set; }
		[JsonProperty("sm")]
		internal string _sm { get; set; }
		[JsonProperty("segmentsMemory")]
		internal string _segmentsMemory { get; set; }
		public string SegmentsMemory { get { return this._segmentsMemory ?? this._sm ?? this._segments_memory; }}

		[JsonProperty("segments.index_writer_memory")]
		internal string _segments_index_writer_memory { get; set; }
		[JsonProperty("siwm")]
		internal string _siwm { get; set; }
		[JsonProperty("segmentsIndexWriterMemory")]
		internal string _segmentsIndexWriterMemory { get; set; }
		public string SegmentsIndexWriterMemory { get { return this._segmentsIndexWriterMemory ?? this._siwm ?? this._segments_index_writer_memory; }}

		[JsonProperty("segments.index_writer_max_memory")]
		internal string _segments_index_writer_max_memory { get; set; }
		[JsonProperty("siwmx")]
		internal string _siwmx { get; set; }
		[JsonProperty("segmentsIndexWriterMaxMemory")]
		internal string _segmentsIndexWriterMaxMemory { get; set; }
		public string SegmentsIndexWriterMaxMemory { get { return this._segmentsIndexWriterMaxMemory ?? this._siwmx ?? this._segments_index_writer_max_memory; }}

		[JsonProperty("segments.version_map_memory")]
		internal string _segments_version_map_memory { get; set; }
		[JsonProperty("svmm")]
		internal string _svmm { get; set; }
		[JsonProperty("segmentsVersionMapMemory")]
		internal string _segmentsVersionMapMemory { get; set; }
		public string SegmentsVersionMapMemory { get { return this._segmentsVersionMapMemory ?? this._svmm ?? this._segments_version_map_memory; }}


	}
}