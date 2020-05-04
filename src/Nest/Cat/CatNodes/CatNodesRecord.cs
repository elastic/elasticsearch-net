// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class CatNodesRecord : ICatRecord
	{
		public string Build => _b ?? _build;
		public string CompletionSize => _completionSize ?? _cs ?? _completion_size;

		[DataMember(Name ="cpu")]
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

		[DataMember(Name ="load_15m")]
		public string LoadFifteenMinute { get; internal set; }

		[DataMember(Name ="load_5m")]
		public string LoadFiveMinute { get; internal set; }

		[DataMember(Name ="load_1m")]
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

		// ReSharper disable InconsistentNaming
		[DataMember(Name ="b")]
		internal string _b { get; set; }

		[DataMember(Name ="build")]
		internal string _build { get; set; }

		[DataMember(Name ="completion.size")]
		internal string _completion_size { get; set; }

		[DataMember(Name ="completionSize")]
		internal string _completionSize { get; set; }

		[DataMember(Name ="cs")]
		internal string _cs { get; set; }

		[DataMember(Name ="d")]
		internal string _d { get; set; }

		[DataMember(Name ="data/client")]
		internal string _data_client { get; set; }

		[DataMember(Name ="dc")]
		internal string _dc { get; set; }

		[DataMember(Name ="disk")]
		internal string _disk { get; set; }

		[DataMember(Name ="disk.avail")]
		internal string _disk_avail { get; set; }

		[DataMember(Name ="diskAvail")]
		internal string _diskAvail { get; set; }

		[DataMember(Name ="fce")]
		internal string _fce { get; set; }

		[DataMember(Name ="fcm")]
		internal string _fcm { get; set; }

		[DataMember(Name ="fdc")]
		internal int? _fdc { get; set; }

		[DataMember(Name ="fdm")]
		internal int? _fdm { get; set; }

		[DataMember(Name ="fdp")]
		internal int? _fdp { get; set; }

		[DataMember(Name ="fe")]
		internal string _fe { get; set; }

		[DataMember(Name ="fielddata.evictions")]
		internal string _fielddata_evictions { get; set; }

		[DataMember(Name ="fielddata.memory_size")]
		internal string _fielddata_memory_size { get; set; }

		[DataMember(Name ="fielddataEvictions")]
		internal string _fielddataEvictions { get; set; }

		[DataMember(Name ="fielddataMemory")]
		internal string _fielddataMemory { get; set; }

		[DataMember(Name ="file_desc.current")]
		internal int? _file_desc_current { get; set; }

		[DataMember(Name ="file_desc.max")]
		internal int? _file_desc_max { get; set; }

		[DataMember(Name ="file_desc.percent")]
		internal int? _file_desc_percent { get; set; }

		[DataMember(Name ="fileDescriptorCurrent")]
		internal int? _fileDescriptorCurrent { get; set; }

		[DataMember(Name ="fileDescriptorMax")]
		internal int? _fileDescriptorMax { get; set; }

		[DataMember(Name ="fileDescriptorPercent")]
		internal int? _fileDescriptorPercent { get; set; }

		[DataMember(Name ="filter_cache.evictions")]
		internal string _filter_cache_evictions { get; set; }

		[DataMember(Name ="filter_cache.memory_size")]
		internal string _filter_cache_memory_size { get; set; }

		[DataMember(Name ="filterCacheEvictions")]
		internal string _filterCacheEvictions { get; set; }

		[DataMember(Name ="filterCacheMemory")]
		internal string _filterCacheMemory { get; set; }

		[DataMember(Name ="flush.total")]
		internal string _flush_total { get; set; }

		[DataMember(Name ="flush.total_time")]
		internal string _flush_total_time { get; set; }

		[DataMember(Name ="flushTotal")]
		internal string _flushTotal { get; set; }

		[DataMember(Name ="flushTotalTime")]
		internal string _flushTotalTime { get; set; }

		[DataMember(Name ="fm")]
		internal string _fm { get; set; }

		[DataMember(Name ="ft")]
		internal string _ft { get; set; }

		[DataMember(Name ="ftt")]
		internal string _ftt { get; set; }

		[DataMember(Name ="gc")]
		internal string _gc { get; set; }

		[DataMember(Name ="get.current")]
		internal string _get_current { get; set; }

		[DataMember(Name ="get.exists_time")]
		internal string _get_exists_time { get; set; }

		[DataMember(Name ="get.exists_total")]
		internal string _get_exists_total { get; set; }

		[DataMember(Name ="get.missing_time")]
		internal string _get_missing_time { get; set; }

		[DataMember(Name ="get.missing_total")]
		internal string _get_missing_total { get; set; }

		[DataMember(Name ="get.time")]
		internal string _get_time { get; set; }

		[DataMember(Name ="get.total")]
		internal string _get_total { get; set; }

		[DataMember(Name ="getCurrent")]
		internal string _getCurrent { get; set; }

		[DataMember(Name ="getExistsTime")]
		internal string _getExistsTime { get; set; }

		[DataMember(Name ="getExistsTotal")]
		internal string _getExistsTotal { get; set; }

		[DataMember(Name ="geti")]
		internal string _geti { get; set; }

		[DataMember(Name ="getMissingTime")]
		internal string _getMissingTime { get; set; }

		[DataMember(Name ="getMissingTotal")]
		internal string _getMissingTotal { get; set; }

		[DataMember(Name ="geto")]
		internal string _geto { get; set; }

		[DataMember(Name ="getTime")]
		internal string _getTime { get; set; }

		[DataMember(Name ="getTotal")]
		internal string _getTotal { get; set; }

		[DataMember(Name ="gmti")]
		internal string _gmti { get; set; }

		[DataMember(Name ="gmto")]
		internal string _gmto { get; set; }

		[DataMember(Name ="gti")]
		internal string _gti { get; set; }

		[DataMember(Name ="gto")]
		internal string _gto { get; set; }

		[DataMember(Name ="hc")]
		internal string _hc { get; set; }

		[DataMember(Name ="heap.current")]
		internal string _heap_current { get; set; }

		[DataMember(Name ="heap.max")]
		internal string _heap_max { get; set; }

		[DataMember(Name ="heap.percent")]
		internal string _heap_percent { get; set; }

		[DataMember(Name ="heapCurrent")]
		internal string _heapCurrent { get; set; }

		[DataMember(Name ="heapMax")]
		internal string _heapMax { get; set; }

		[DataMember(Name ="heapPercent")]
		internal string _heapPercent { get; set; }

		[DataMember(Name ="hm")]
		internal string _hm { get; set; }

		[DataMember(Name ="hp")]
		internal string _hp { get; set; }

		[DataMember(Name ="i")]
		internal string _i { get; set; }

		[DataMember(Name ="id")]
		internal string _id { get; set; }

		[DataMember(Name ="id_cache.memory_size")]
		internal string _id_cache_memory_size { get; set; }

		[DataMember(Name ="idCacheMemory")]
		internal string _idCacheMemory { get; set; }

		[DataMember(Name ="idc")]
		internal string _idcs { get; set; }

		[DataMember(Name ="idti")]
		internal string _idti { get; set; }

		[DataMember(Name ="idto")]
		internal string _idto { get; set; }

		[DataMember(Name ="iic")]
		internal string _iic { get; set; }

		[DataMember(Name ="iiti")]
		internal string _iiti { get; set; }

		[DataMember(Name ="iito")]
		internal string _iito { get; set; }

		[DataMember(Name ="im")]
		internal string _im { get; set; }

		[DataMember(Name ="indexing.delete_current")]
		internal string _indexing_delete_current { get; set; }

		[DataMember(Name ="indexing.delete_time")]
		internal string _indexing_delete_time { get; set; }

		[DataMember(Name ="indexing.delete_total")]
		internal string _indexing_delete_total { get; set; }

		[DataMember(Name ="indexing.index_current")]
		internal string _indexing_index_current { get; set; }

		[DataMember(Name ="indexing.index_time")]
		internal string _indexing_index_time { get; set; }

		[DataMember(Name ="indexing.index_total")]
		internal string _indexing_index_total { get; set; }

		[DataMember(Name ="indexingDeleteCurrent")]
		internal string _indexingDeleteCurrent { get; set; }

		[DataMember(Name ="indexingDeleteTime")]
		internal string _indexingDeleteTime { get; set; }

		[DataMember(Name ="indexingDeleteTotal")]
		internal string _indexingDeleteTotal { get; set; }

		[DataMember(Name ="indexingIndexCurrent")]
		internal string _indexingIndexCurrent { get; set; }

		[DataMember(Name ="indexingIndexTime")]
		internal string _indexingIndexTime { get; set; }

		[DataMember(Name ="indexingIndexTotal")]
		internal string _indexingIndexTotal { get; set; }

		[DataMember(Name ="ip")]
		internal string _ip { get; set; }

		[DataMember(Name ="j")]
		internal string _j { get; set; }

		[DataMember(Name ="jdk")]
		internal string _jdk { get; set; }

		[DataMember(Name ="m")]
		internal string _m { get; set; }

		[DataMember(Name ="master")]
		internal string _master { get; set; }

		[DataMember(Name ="mc")]
		internal string _mc { get; set; }

		[DataMember(Name ="mcd")]
		internal string _mcd { get; set; }

		[DataMember(Name ="mcs")]
		internal string _mcs { get; set; }

		[DataMember(Name ="merges.current")]
		internal string _merges_current { get; set; }

		[DataMember(Name ="merges.current_docs")]
		internal string _merges_current_docs { get; set; }

		[DataMember(Name ="merges.current_size")]
		internal string _merges_current_size { get; set; }

		[DataMember(Name ="merges.total")]
		internal string _merges_total { get; set; }

		[DataMember(Name ="merges.total_docs")]
		internal string _merges_total_docs { get; set; }

		[DataMember(Name ="merges.total_time")]
		internal string _merges_total_time { get; set; }

		[DataMember(Name ="mergesCurrent")]
		internal string _mergesCurrent { get; set; }

		[DataMember(Name ="mergesCurrentDocs")]
		internal string _mergesCurrentDocs { get; set; }

		[DataMember(Name ="mergesCurrentSize")]
		internal string _mergesCurrentSize { get; set; }

		[DataMember(Name ="mergesTotal")]
		internal string _mergesTotal { get; set; }

		[DataMember(Name ="mergesTotalDocs")]
		internal string _mergesTotalDocs { get; set; }

		[DataMember(Name ="mergesTotalTime")]
		internal string _mergesTotalTime { get; set; }

		[DataMember(Name ="mt")]
		internal string _mt { get; set; }

		[DataMember(Name ="mtd")]
		internal string _mtd { get; set; }

		[DataMember(Name ="mtt")]
		internal string _mtt { get; set; }

		[DataMember(Name ="n")]
		internal string _n { get; set; }

		[DataMember(Name ="name")]
		internal string _name { get; set; }

		[DataMember(Name ="node.role")]
		internal string _node_role { get; set; }

		[DataMember(Name ="nodeId")]
		internal string _nodeId { get; set; }

		[DataMember(Name ="nodeRole")]
		internal string _nodeRole { get; set; }

		[DataMember(Name ="p")]
		internal string _p { get; set; }

		[DataMember(Name ="pc")]
		internal string _pc { get; set; }

		[DataMember(Name ="percolate.current")]
		internal string _percolate_current { get; set; }

		[DataMember(Name ="percolate.memory_size")]
		internal string _percolate_memory_size { get; set; }

		[DataMember(Name ="percolate.queries")]
		internal string _percolate_queries { get; set; }

		[DataMember(Name ="percolate.time")]
		internal string _percolate_time { get; set; }

		[DataMember(Name ="percolate.total")]
		internal string _percolate_total { get; set; }

		[DataMember(Name ="percolateCurrent")]
		internal string _percolateCurrent { get; set; }

		[DataMember(Name ="percolateMemory")]
		internal string _percolateMemory { get; set; }

		[DataMember(Name ="percolateQueries")]
		internal string _percolateQueries { get; set; }

		[DataMember(Name ="percolateTime")]
		internal string _percolateTime { get; set; }

		[DataMember(Name ="percolateTotal")]
		internal string _percolateTotal { get; set; }

		[DataMember(Name ="pid")]
		internal string _pid { get; set; }

		[DataMember(Name ="pm")]
		internal string _pm { get; set; }

		[DataMember(Name ="po")]
		internal string _po { get; set; }

		[DataMember(Name ="port")]
		internal string _port { get; set; }

		[DataMember(Name ="pq")]
		internal string _pq { get; set; }

		[DataMember(Name ="pti")]
		internal string _pti { get; set; }

		[DataMember(Name ="pto")]
		internal string _pto { get; set; }

		[DataMember(Name ="r")]
		internal string _r { get; set; }

		[DataMember(Name ="ram.current")]
		internal string _ram_current { get; set; }

		[DataMember(Name ="ram.max")]
		internal string _ram_max { get; set; }

		[DataMember(Name ="ram.percent")]
		internal string _ram_percent { get; set; }

		[DataMember(Name ="ramCurrent")]
		internal string _ramCurrent { get; set; }

		[DataMember(Name ="ramMax")]
		internal string _ramMax { get; set; }

		[DataMember(Name ="ramPercent")]
		internal string _ramPercent { get; set; }

		[DataMember(Name ="rc")]
		internal string _rc { get; set; }

		[DataMember(Name ="refresh.time")]
		internal string _refresh_time { get; set; }

		[DataMember(Name ="refresh.total")]
		internal string _refresh_total { get; set; }

		[DataMember(Name ="refreshTime")]
		internal string _refreshTime { get; set; }

		[DataMember(Name ="refreshTotal")]
		internal string _refreshTotal { get; set; }

		[DataMember(Name ="rm")]
		internal string _rm { get; set; }

		[DataMember(Name ="rp")]
		internal string _rp { get; set; }

		[DataMember(Name ="rti")]
		internal string _rti { get; set; }

		[DataMember(Name ="rto")]
		internal string _rto { get; set; }

		[DataMember(Name ="sc")]
		internal string _sc { get; set; }

		[DataMember(Name ="search.fetch_current")]
		internal string _search_fetch_current { get; set; }

		[DataMember(Name ="search.fetch_time")]
		internal string _search_fetch_time { get; set; }

		[DataMember(Name ="search.fetch_total")]
		internal string _search_fetch_total { get; set; }

		[DataMember(Name ="search.open_contexts")]
		internal string _search_open_contexts { get; set; }

		[DataMember(Name ="search.query_current")]
		internal string _search_query_current { get; set; }

		[DataMember(Name ="search.query_time")]
		internal string _search_query_time { get; set; }

		[DataMember(Name ="search.query_total")]
		internal string _search_query_total { get; set; }

		[DataMember(Name ="searchFetchCurrent")]
		internal string _searchFetchCurrent { get; set; }

		[DataMember(Name ="searchFetchTime")]
		internal string _searchFetchTime { get; set; }

		[DataMember(Name ="searchFetchTotal")]
		internal string _searchFetchTotal { get; set; }

		[DataMember(Name ="searchOpenContexts")]
		internal string _searchOpenContexts { get; set; }

		[DataMember(Name ="searchQueryCurrent")]
		internal string _searchQueryCurrent { get; set; }

		[DataMember(Name ="searchQueryTime")]
		internal string _searchQueryTime { get; set; }

		[DataMember(Name ="searchQueryTotal")]
		internal string _searchQueryTotal { get; set; }

		[DataMember(Name ="segments.count")]
		internal string _segments_count { get; set; }

		[DataMember(Name ="segments.index_writer_max_memory")]
		internal string _segments_index_writer_max_memory { get; set; }

		[DataMember(Name ="segments.index_writer_memory")]
		internal string _segments_index_writer_memory { get; set; }

		[DataMember(Name ="segments.memory")]
		internal string _segments_memory { get; set; }

		[DataMember(Name ="segments.version_map_memory")]
		internal string _segments_version_map_memory { get; set; }

		[DataMember(Name ="segmentsCount")]
		internal string _segmentsCount { get; set; }

		[DataMember(Name ="segmentsIndexWriterMaxMemory")]
		internal string _segmentsIndexWriterMaxMemory { get; set; }

		[DataMember(Name ="segmentsIndexWriterMemory")]
		internal string _segmentsIndexWriterMemory { get; set; }

		[DataMember(Name ="segmentsMemory")]
		internal string _segmentsMemory { get; set; }

		[DataMember(Name ="segmentsVersionMapMemory")]
		internal string _segmentsVersionMapMemory { get; set; }

		[DataMember(Name ="sfc")]
		internal string _sfc { get; set; }

		[DataMember(Name ="sfti")]
		internal string _sfti { get; set; }

		[DataMember(Name ="sfto")]
		internal string _sfto { get; set; }

		[DataMember(Name ="siwm")]
		internal string _siwm { get; set; }

		[DataMember(Name ="siwmx")]
		internal string _siwmx { get; set; }

		[DataMember(Name ="sm")]
		internal string _sm { get; set; }

		[DataMember(Name ="so")]
		internal string _so { get; set; }

		[DataMember(Name ="sqc")]
		internal string _sqc { get; set; }

		[DataMember(Name ="sqti")]
		internal string _sqti { get; set; }

		[DataMember(Name ="sqto")]
		internal string _sqto { get; set; }

		[DataMember(Name ="svmm")]
		internal string _svmm { get; set; }

		[DataMember(Name ="u")]
		internal string _u { get; set; }

		[DataMember(Name ="uptime")]
		internal string _uptime { get; set; }

		[DataMember(Name ="v")]
		internal string _v { get; set; }

		[DataMember(Name ="version")]
		internal string _version { get; set; }
		// ReSharper restore InconsistentNaming
	}
}
