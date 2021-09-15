//using JetBrains.Profiler.Api;
//using Elastic.Clients.Elasticsearch;
//using Elastic.Clients.Elasticsearch.IndexManagement;

//var req1 = new Elastic.Clients.Elasticsearch.IndexManagement.DeleteRequest("test");

////var list = new List<IndexName>();
////IEnumerable<IndexName> items = new IndexName[] { "a", "b" };

//MemoryProfiler.ForceGc();

//MemoryProfiler.CollectAllocations(true);

//MemoryProfiler.GetSnapshot();

////var req = new DeleteRequest("test");

////var i = Indices.Parse("test");

//var i = Indices.Single("test");

//MemoryProfiler.GetSnapshot();

////if (req.AllowNoIndices.HasValue)
////{

////}

//_ = i.ToString();

//MemoryProfiler.CollectAllocations(false);

////var source = new IndexName[] { "index-01", "index-02" };

////var indices = new Indices(source);
////var indicesList = new IndicesList(source);

////MemoryProfiler.CollectAllocations(true);

////MemoryProfiler.GetSnapshot();

////var indices2 = new Indices(source);

////MemoryProfiler.GetSnapshot();

////var indicesList2 = new IndicesList(source);

////MemoryProfiler.GetSnapshot();

////MemoryProfiler.CollectAllocations(false);

////// Ensure no GC between snapshots
////_ = indices2.Values.Count;
////_ = indicesList2.Values.Count;
