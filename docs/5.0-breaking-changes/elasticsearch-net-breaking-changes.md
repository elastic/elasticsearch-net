#Breaking Changes

This lists all the **binary** breaking changes in Elasticsearch.Net. 

## Enums

**public enum Elasticsearch.Net.Consistency** *Removed (Breaking)* 

**public enum Elasticsearch.Net.Bytes** *Declaration changed (Breaking)*

2.x
```csharp
public enum Bytes
{
     B = 0,
     K = 1,
     M = 2,
     G = 3
}
```

5.x
```csharp
public enum Bytes
{
     B = 0,
     K = 1,
     Kb = 2,
     M = 3,
     Mb = 4,
     G = 5,
     Gb = 6,
     T = 7,
     Tb = 8,
     P = 9,
     Pb = 10
}
```

**public enum Elasticsearch.Net.Feature** *Declaration changed (Breaking)*

2.x
```csharp
[FlagsAttribute]
public enum Feature
{
     Settings = 1,
     Mappings = 2,
     Warmers = 4,
     Aliases = 8
}
```

5.x
```csharp
[FlagsAttribute]
public enum Feature
{
     Settings = 1,
     Mappings = 2,
     Aliases = 4
}
```

**public enum Elasticsearch.Net.NodesStatsMetric** *Declaration changed (Breaking)*

2.x
```csharp
[FlagsAttribute]
public enum NodesStatsMetric
{
     Breaker = 1,
     Fs = 2,
     Http = 4,
     Indices = 8,
     Jvm = 16,
     Os = 32,
     Process = 64,
     ThreadPool = 128,
     Transport = 256,
     All = 512
}
```

5.x
```csharp
[FlagsAttribute]
public enum NodesStatsMetric
{
     Breaker = 1,
     Fs = 2,
     Http = 4,
     Indices = 8,
     Jvm = 16,
     Os = 32,
     Process = 64,
     ThreadPool = 128,
     Transport = 256,
     Discovery = 512,
     All = 1024
}
```

**public enum Elasticsearch.Net.SearchType** *Declaration changed (Breaking)*

2.x
```csharp
public enum SearchType
{
     QueryThenFetch = 0,
     QueryAndFetch = 1,
     DfsQueryThenFetch = 2,
     DfsQueryAndFetch = 3,
     Count = 4,
     Scan = 5
}
```

5.x
```csharp
public enum SearchType
{
     QueryThenFetch = 0,
     DfsQueryThenFetch = 1
}
```

## Refresh no longer just a bool

Now an enum that can also send `wait_for` 

**public method Elasticsearch.Net.BulkRequestParameters.Refresh** *Declaration changed (Breaking)*

2.x: `public BulkRequestParameters Refresh(bool refresh)`  
5.x: `public BulkRequestParameters Refresh(Refresh refresh)`  

**public method Elasticsearch.Net.DeleteRequestParameters.Refresh** *Declaration changed (Breaking)*  
**public method Elasticsearch.Net.IndexRequestParameters.Refresh** *Declaration changed (Breaking)*  
**public method Elasticsearch.Net.UpdateRequestParameters.Refresh** *Declaration changed (Breaking)*  

## WaitForActiveShards now takes a string

So you can send `all` 

**public method Elasticsearch.Net.ClusterHealthRequestParameters.WaitForActiveShards** *Declaration changed (Breaking)*

2.x: `public ClusterHealthRequestParameters WaitForActiveShards(long wait_for_active_shards)`  
5.x: `public ClusterHealthRequestParameters WaitForActiveShards(string wait_for_active_shards)`  

## Visibility changes

These were types/methods/properties/constructors that were public but had no business being so.

**public method Elasticsearch.Net.RequestData..ctor** *Visibility was changed from public to private (Breaking)*

2.x
```csharp
[ObsoleteAttribute("This constructor is scheduled to become private in 5.0.0")]
public  .ctor(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestConfiguration local, IMemoryStreamFactory memoryStreamFactory)
```

5.x
```csharp
private  .ctor(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IRequestConfiguration local, IMemoryStreamFactory memoryStreamFactory)
```

**public property Elasticsearch.Net.Transport&lt;TConnectionSettings&gt;.DateTimeProvider** *Visibility was changed from public to private (Breaking)*

2.x: `public IDateTimeProvider DateTimeProvider { get; }`  
5.x: `private IDateTimeProvider DateTimeProvider { get; }`  

**public property Elasticsearch.Net.Transport&lt;TConnectionSettings&gt;.MemoryStreamFactory** *Visibility was changed from public to private (Breaking)*

2.x: `public IMemoryStreamFactory MemoryStreamFactory { get; }`  
5.x: `private IMemoryStreamFactory MemoryStreamFactory { get; }`  

**public property Elasticsearch.Net.Transport&lt;TConnectionSettings&gt;.PipelineProvider** *Visibility was changed from public to private (Breaking)*

2.x: `public IRequestPipelineFactory PipelineProvider { get; }`  
5.x: `private IRequestPipelineFactory PipelineProvider { get; }`  



## Rename of API related methods

Impact low, these have been renamed to match their Method name equivalents


**public method Elasticsearch.Net.ElasticLowLevelClient.CatNodeattrs&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> CatNodeattrs<T>(Func<CatNodeattrsRequestParameters, CatNodeattrsRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> CatNodeattrs<T>(Func<CatNodeAttributesRequestParameters, CatNodeAttributesRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.ElasticLowLevelClient.TasksCancel&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> TasksCancel<T>(string task_id, Func<TasksCancelRequestParameters, TasksCancelRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> TasksCancel<T>(string task_id, Func<CancelTasksRequestParameters, CancelTasksRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.ElasticLowLevelClient.TasksCancel&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> TasksCancel<T>(Func<TasksCancelRequestParameters, TasksCancelRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> TasksCancel<T>(Func<CancelTasksRequestParameters, CancelTasksRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.ElasticLowLevelClient.TasksList&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> TasksList<T>(Func<TasksListRequestParameters, TasksListRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> TasksList<T>(Func<ListTasksRequestParameters, ListTasksRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.IElasticLowLevelClient.CatNodeattrs&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> CatNodeattrs<T>(Func<CatNodeattrsRequestParameters, CatNodeattrsRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> CatNodeattrs<T>(Func<CatNodeAttributesRequestParameters, CatNodeAttributesRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.IElasticLowLevelClient.TasksCancel&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> TasksCancel<T>(string task_id, Func<TasksCancelRequestParameters, TasksCancelRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> TasksCancel<T>(string task_id, Func<CancelTasksRequestParameters, CancelTasksRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.IElasticLowLevelClient.TasksCancel&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> TasksCancel<T>(Func<TasksCancelRequestParameters, TasksCancelRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> TasksCancel<T>(Func<CancelTasksRequestParameters, CancelTasksRequestParameters> requestParameters)`  

**public method Elasticsearch.Net.IElasticLowLevelClient.TasksList&lt;T&gt;** *Declaration changed (Breaking)*

2.x: `public ElasticsearchResponse<T> TasksList<T>(Func<TasksListRequestParameters, TasksListRequestParameters> requestParameters)`  
5.x: `public ElasticsearchResponse<T> TasksList<T>(Func<ListTasksRequestParameters, ListTasksRequestParameters> requestParameters)`  


## Rest spec updates

These are breaking changes due to the Elasticsearch 5.0 rest spec changing

**public method Elasticsearch.Net.DeleteByQueryRequestParameters.Routing** *Declaration changed (Breaking)*

2.x: `public DeleteByQueryRequestParameters Routing(string routing)`  
5.x: `public DeleteByQueryRequestParameters Routing(String[] routing)`  

**public method Elasticsearch.Net.ReindexOnServerRequestParameters.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexOnServerRequestParameters RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexOnServerRequestParameters RequestsPerSecond(long requests_per_second)`  

**public method Elasticsearch.Net.ReindexRethrottleRequestParameters.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public ReindexRethrottleRequestParameters RequestsPerSecond(Single requests_per_second)`  
5.x: `public ReindexRethrottleRequestParameters RequestsPerSecond(long requests_per_second)`  

**public method Elasticsearch.Net.UpdateByQueryRequestParameters.RequestsPerSecond** *Declaration changed (Breaking)*

2.x: `public UpdateByQueryRequestParameters RequestsPerSecond(Single requests_per_second)`  
5.x: `public UpdateByQueryRequestParameters RequestsPerSecond(long requests_per_second)`  


## Removed in 5.x after obsolete period

These are types/properties/methods marked obsolete in NEST 2.x that have now been removed.


**public method Elasticsearch.Net.AnalyzeRequestParameters.Analyzer** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the analyzer to use in the body of the request.")]
public AnalyzeRequestParameters Analyzer(string analyzer)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.CharFilter** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the char filters to use in the body of the request.")]
public AnalyzeRequestParameters CharFilter(String[] char_filter)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.CharFilters** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the char filters to use in the body of the request.")]
public AnalyzeRequestParameters CharFilters(String[] char_filters)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.Field** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the field to use in the body of the request.")]
public AnalyzeRequestParameters Field(string field)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.Filter** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the filters to use in the body of the request.")]
public AnalyzeRequestParameters Filter(String[] filter)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.Filters** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the filters to use in the body of the request.")]
public AnalyzeRequestParameters Filters(String[] filters)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.Text** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the text to use in the body of the request.")]
public AnalyzeRequestParameters Text(String[] text)
```

**public method Elasticsearch.Net.AnalyzeRequestParameters.Tokenizer** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Deprecated. Specify the tokenizer to use in the body of the request.")]
public AnalyzeRequestParameters Tokenizer(string tokenizer)
```

**public property Elasticsearch.Net.BasicAuthenticationCredentials.UserName** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0. Use Username instead, note the lowercase n")]
[CLSCompliantAttribute(False)]
public string UserName { get; set; }
```

**public method Elasticsearch.Net.RequestData..ctor** *Removed (Breaking)*

```csharp
[ObsoleteAttribute("Removed in 5.0.0")]
public  .ctor(HttpMethod method, string path, PostData<object> data, IConnectionConfigurationValues global, IMemoryStreamFactory memoryStreamFactory)
```

# Now IReadOnlyCollection

**public property Elasticsearch.Net.Error.RootCause**  

# CancellationToken

Async methods now expose CancellationToken directly in the method signature, you no longer have to set this on `RequestConfiguration`

**Elasticsearch.Net.ElasticLowLevelClient.BulkAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.BulkAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.BulkAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.BulkPutAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.BulkPutAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.BulkPutAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatAliasesAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatAliasesAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatAllocationAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatAllocationAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatCountAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatCountAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatFielddataAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatFielddataAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatHealthAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatHelpAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatIndicesAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatIndicesAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatMasterAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatNodeattrsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatNodesAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatPendingTasksAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatPluginsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatRecoveryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatRecoveryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatRepositoriesAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatSegmentsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatSegmentsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatSnapshotsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CatThreadPoolAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClearScrollAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterGetSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterHealthAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterHealthAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterPendingTasksAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterPutSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterRerouteAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterStateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterStateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterStateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ClusterStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountPercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountPercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountPercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.CountPercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.DeleteAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.DeleteByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.DeleteByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.DeleteScriptAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.DeleteTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.DoRequestAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ExistsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ExplainAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ExplainGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.FieldStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.FieldStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.FieldStatsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.FieldStatsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.GetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.GetScriptAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.GetSourceAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.GetTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndexAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndexAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndexPutAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndexPutAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesAnalyzeAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesAnalyzeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesAnalyzeGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesAnalyzeGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesClearCacheAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesClearCacheForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesClearCacheGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesClearCacheGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesCloseAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesCreateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesDeleteAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesDeleteAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesDeleteTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesExistsAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesExistsAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesExistsAliasForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesExistsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesExistsTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesExistsTypeAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushSyncedAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushSyncedForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushSyncedGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesFlushSyncedGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesForcemergeAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesForcemergeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetFieldMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetFieldMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetFieldMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetFieldMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetSettingsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetSettingsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetUpgradeAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesGetUpgradeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesOpenAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutAliasPostAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutMappingPostAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutMappingPostForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutSettingsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesPutTemplatePostForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesRecoveryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesRecoveryForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesRefreshAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesRefreshForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesRefreshGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesRefreshGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesSegmentsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesSegmentsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesShardStoresAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesShardStoresForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesUpdateAliasesForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesUpgradeAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesUpgradeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesValidateQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesValidateQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesValidateQueryForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesValidateQueryGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesValidateQueryGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.IndicesValidateQueryGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.InfoAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MgetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MgetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MgetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MgetGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MgetGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MgetGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MpercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MpercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MpercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MpercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MpercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MpercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MsearchAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MsearchAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MsearchAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MsearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MsearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MsearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MtermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MtermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MtermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MtermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MtermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.MtermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesHotThreadsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesHotThreadsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesInfoAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesInfoAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesInfoForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesInfoForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.NodesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PingAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PutScriptAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PutScriptPostAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PutTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.PutTemplatePostAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ReindexAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ReindexRethrottleAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.RenderSearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.RenderSearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.RenderSearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.RenderSearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ScrollAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.ScrollGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchShardsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchShardsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchShardsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotCreateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotCreatePostAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotCreateRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotCreateRepositoryPostAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotDeleteAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotDeleteRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotGetRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotGetRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotRestoreAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotStatusAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotStatusAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotStatusAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SnapshotVerifyRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SuggestAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SuggestAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SuggestGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.SuggestGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TasksCancelAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TasksCancelAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TasksListAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.TermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.UpdateAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.UpdateByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.ElasticLowLevelClient.UpdateByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.HttpConnection.RequestAsync&lt;TReturn&gt;**  
**Elasticsearch.Net.IConnection.RequestAsync&lt;TReturn&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.BulkAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.BulkAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.BulkAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.BulkPutAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.BulkPutAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.BulkPutAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatAliasesAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatAliasesAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatAllocationAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatAllocationAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatCountAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatCountAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatFielddataAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatFielddataAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatHealthAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatHelpAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatIndicesAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatIndicesAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatMasterAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatNodeattrsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatNodesAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatPendingTasksAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatPluginsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatRecoveryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatRecoveryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatRepositoriesAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatSegmentsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatSegmentsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatSnapshotsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CatThreadPoolAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClearScrollAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterGetSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterHealthAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterHealthAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterPendingTasksAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterPutSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterRerouteAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterStateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterStateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterStateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ClusterStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountPercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountPercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountPercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.CountPercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.DeleteAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.DeleteByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.DeleteByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.DeleteScriptAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.DeleteTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.DoRequestAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ExistsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ExplainAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ExplainGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.FieldStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.FieldStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.FieldStatsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.FieldStatsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.GetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.GetScriptAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.GetSourceAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.GetTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndexAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndexAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndexPutAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndexPutAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesAnalyzeAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesAnalyzeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesAnalyzeGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesAnalyzeGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesClearCacheAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesClearCacheForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesClearCacheGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesClearCacheGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesCloseAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesCreateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesDeleteAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesDeleteAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesDeleteTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesExistsAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesExistsAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesExistsAliasForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesExistsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesExistsTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesExistsTypeAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushSyncedAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushSyncedForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushSyncedGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesFlushSyncedGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesForcemergeAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesForcemergeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetFieldMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetFieldMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetFieldMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetFieldMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetSettingsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetSettingsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetUpgradeAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesGetUpgradeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesOpenAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutAliasAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutAliasPostAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutMappingAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutMappingForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutMappingPostAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutMappingPostForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutSettingsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutSettingsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutTemplateForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesPutTemplatePostForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesRecoveryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesRecoveryForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesRefreshAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesRefreshForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesRefreshGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesRefreshGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesSegmentsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesSegmentsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesShardStoresAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesShardStoresForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesUpdateAliasesForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesUpgradeAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesUpgradeForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesValidateQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesValidateQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesValidateQueryForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesValidateQueryGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesValidateQueryGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.IndicesValidateQueryGetForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.InfoAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MgetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MgetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MgetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MgetGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MgetGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MgetGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MpercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MpercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MpercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MpercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MpercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MpercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MsearchAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MsearchAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MsearchAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MsearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MsearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MsearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MtermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MtermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MtermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MtermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MtermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.MtermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesHotThreadsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesHotThreadsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesInfoAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesInfoAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesInfoForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesInfoForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesStatsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.NodesStatsForAllAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PercolateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PercolateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PingAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PutScriptAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PutScriptPostAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PutTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.PutTemplatePostAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ReindexAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ReindexRethrottleAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.RenderSearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.RenderSearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.RenderSearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.RenderSearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ScrollAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.ScrollGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchShardsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchShardsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchShardsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchShardsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchTemplateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SearchTemplateGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotCreateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotCreatePostAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotCreateRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotCreateRepositoryPostAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotDeleteAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotDeleteRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotGetRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotGetRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotRestoreAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotStatusAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotStatusAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotStatusAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SnapshotVerifyRepositoryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SuggestAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SuggestAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SuggestGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.SuggestGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TasksCancelAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TasksCancelAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TasksListAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TermvectorsAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.TermvectorsGetAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.UpdateAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.UpdateByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.IElasticLowLevelClient.UpdateByQueryAsync&lt;T&gt;**  
**Elasticsearch.Net.InMemoryConnection.RequestAsync&lt;TReturn&gt;**  
**Elasticsearch.Net.IRequestPipeline.CallElasticsearchAsync&lt;TReturn&gt;**  
**Elasticsearch.Net.IRequestPipeline.FirstPoolUsageAsync**  
**Elasticsearch.Net.IRequestPipeline.PingAsync**  
**Elasticsearch.Net.IRequestPipeline.SniffAsync**  
**Elasticsearch.Net.IRequestPipeline.SniffOnConnectionFailureAsync**  
**Elasticsearch.Net.IRequestPipeline.SniffOnStaleClusterAsync**  
**Elasticsearch.Net.ITransport&lt;TConnectionSettings&gt;.RequestAsync&lt;T&gt;**  
**Elasticsearch.Net.RequestPipeline.CallElasticsearchAsync&lt;TReturn&gt;**  
**Elasticsearch.Net.RequestPipeline.FirstPoolUsageAsync**  
**Elasticsearch.Net.RequestPipeline.PingAsync**  
**Elasticsearch.Net.RequestPipeline.SniffAsync**  
**Elasticsearch.Net.RequestPipeline.SniffOnConnectionFailureAsync**  
**Elasticsearch.Net.RequestPipeline.SniffOnStaleClusterAsync**  
**Elasticsearch.Net.ResponseBuilder&lt;TReturn&gt;..ctor**  
**Elasticsearch.Net.Transport&lt;TConnectionSettings&gt;.RequestAsync&lt;TReturn&gt;**  

# Removed in 5.x

These no longer exist in 5.x, either they have been renamed or are part of previous mentioned changes

**public method Elasticsearch.Net.BulkRequestParameters.Consistency** *Removed (Breaking)* 
**public class Elasticsearch.Net.CatNodeattrsRequestParameters** *Removed (Breaking)* 
**public method Elasticsearch.Net.CatThreadPoolRequestParameters.FullId** *Removed (Breaking)* 
**public method Elasticsearch.Net.ClusterHealthRequestParameters.WaitForRelocatingShards** *Removed (Breaking)* 
**public method Elasticsearch.Net.DeleteRequestParameters.Consistency** *Removed (Breaking)* 
**public method Elasticsearch.Net.DeleteScriptRequestParameters.Version** *Removed (Breaking)* 
**public method Elasticsearch.Net.DeleteScriptRequestParameters.VersionType** *Removed (Breaking)* 
**public method Elasticsearch.Net.DeleteSearchTemplateRequestParameters.Version** *Removed (Breaking)* 
**public method Elasticsearch.Net.DeleteSearchTemplateRequestParameters.VersionType** *Removed (Breaking)* 
**public class Elasticsearch.Net.DeleteWarmerRequestParameters** *Removed (Breaking)* 
**public method Elasticsearch.Net.DeleteWatchRequestParameters.Force** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExplore&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExplore&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExploreAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExploreAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExploreGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExploreGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExploreGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.GraphExploreGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesCreatePost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesCreatePostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesDeleteWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesDeleteWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesForcemergeGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesForcemergeGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesForcemergeGetForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesForcemergeGetForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliases&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliases&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasesAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasesAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasesForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasesForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasesForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetAliasesForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesGetWarmerForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimize&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeGetForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesOptimizeGetForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerPostForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.IndicesPutWarmerPostForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.LicenseDelete&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.LicenseDeleteAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.LicenseGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.LicenseGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.LicensePost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.LicensePostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExists&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExists&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExists&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.SearchExistsGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldAuthenticate&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldAuthenticateAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldClearCachedRealms&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldClearCachedRealmsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldClearCachedRoles&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldClearCachedRolesAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldClearCachedRolesPut&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldClearCachedRolesPutAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldDeleteRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldDeleteRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldDeleteUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldDeleteUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldGetUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutRolePost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutRolePostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutUserPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.ShieldPutUserPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.TasksList&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.TasksListAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherAckWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherActivateWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherActivateWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherActivateWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherActivateWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherDeactivateWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherDeactivateWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherDeactivateWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherDeactivateWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherDeleteWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherDeleteWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherExecuteWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherGetWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherGetWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherInfo&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherInfoAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherPutWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherPutWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherPutWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherPutWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherRestart&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherRestartAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStart&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStartAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStats&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStats&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStatsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStatsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStop&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ElasticLowLevelClient.WatcherStopAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.ExplainRequestParameters.Fields** *Removed (Breaking)* 
**public class Elasticsearch.Net.GetAliasesRequestParameters** *Removed (Breaking)* 
**public method Elasticsearch.Net.GetRequestParameters.Fields** *Removed (Breaking)* 
**public method Elasticsearch.Net.GetScriptRequestParameters.Version** *Removed (Breaking)* 
**public method Elasticsearch.Net.GetScriptRequestParameters.VersionType** *Removed (Breaking)* 
**public method Elasticsearch.Net.GetSearchTemplateRequestParameters.Version** *Removed (Breaking)* 
**public method Elasticsearch.Net.GetSearchTemplateRequestParameters.VersionType** *Removed (Breaking)* 
**public class Elasticsearch.Net.GetWarmerRequestParameters** *Removed (Breaking)* 
**public property Elasticsearch.Net.IBodyWithApiCallDetails.CallDetails** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExplore&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExplore&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExploreAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExploreAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExploreGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExploreGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExploreGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.GraphExploreGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesCreatePost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesCreatePostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesDeleteWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesDeleteWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesForcemergeGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesForcemergeGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesForcemergeGetForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesForcemergeGetForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliases&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliases&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasesAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasesAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasesForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasesForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasesForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetAliasesForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesGetWarmerForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimize&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeGetForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesOptimizeGetForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmer&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerPostForAll&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.IndicesPutWarmerPostForAllAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.LicenseDelete&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.LicenseDeleteAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.LicenseGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.LicenseGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.LicensePost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.LicensePostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExists&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExists&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExists&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsGet&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.SearchExistsGetAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldAuthenticate&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldAuthenticateAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldClearCachedRealms&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldClearCachedRealmsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldClearCachedRoles&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldClearCachedRolesAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldClearCachedRolesPut&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldClearCachedRolesPutAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldDeleteRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldDeleteRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldDeleteUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldDeleteUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldGetUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutRole&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutRoleAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutRolePost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutRolePostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutUser&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutUserAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutUserPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.ShieldPutUserPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.TasksList&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.TasksListAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherAckWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherActivateWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherActivateWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherActivateWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherActivateWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherDeactivateWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherDeactivateWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherDeactivateWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherDeactivateWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherDeleteWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherDeleteWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherExecuteWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherGetWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherGetWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherInfo&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherInfoAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherPutWatch&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherPutWatchAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherPutWatchPost&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherPutWatchPostAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherRestart&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherRestartAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStart&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStartAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStats&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStats&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStatsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStatsAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStop&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IElasticLowLevelClient.WatcherStopAsync&lt;T&gt;** *Removed (Breaking)* 
**public method Elasticsearch.Net.IndexRequestParameters.Consistency** *Removed (Breaking)* 
**public property Elasticsearch.Net.IRequestConfiguration.CancellationToken** *Removed (Breaking)* 
**public method Elasticsearch.Net.MultiGetRequestParameters.Fields** *Removed (Breaking)* 
**public class Elasticsearch.Net.OptimizeRequestParameters** *Removed (Breaking)* 
**public method Elasticsearch.Net.PutScriptRequestParameters.OpType** *Removed (Breaking)* 
**public method Elasticsearch.Net.PutScriptRequestParameters.Version** *Removed (Breaking)* 
**public method Elasticsearch.Net.PutScriptRequestParameters.VersionType** *Removed (Breaking)* 
**public method Elasticsearch.Net.PutSearchTemplateRequestParameters.OpType** *Removed (Breaking)* 
**public method Elasticsearch.Net.PutSearchTemplateRequestParameters.Version** *Removed (Breaking)* 
**public method Elasticsearch.Net.PutSearchTemplateRequestParameters.VersionType** *Removed (Breaking)* 
**public class Elasticsearch.Net.PutWarmerRequestParameters** *Removed (Breaking)* 
**public method Elasticsearch.Net.ReindexOnServerRequestParameters.Consistency** *Removed (Breaking)* 
**public method Elasticsearch.Net.RequestConfigurationDescriptor.AcceptContentType** *Removed (Breaking)* 
**public method Elasticsearch.Net.RequestConfigurationDescriptor.CancellationToken** *Removed (Breaking)* 
**public property Elasticsearch.Net.RequestData.CancellationToken** *Removed (Breaking)* 
**public class Elasticsearch.Net.SearchExistsRequestParameters** *Removed (Breaking)* 
**public class Elasticsearch.Net.TasksCancelRequestParameters** *Removed (Breaking)* 
**public class Elasticsearch.Net.TasksListRequestParameters** *Removed (Breaking)* 
**public method Elasticsearch.Net.TermVectorsRequestParameters.Dfs** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpdateByQueryRequestParameters.Consistency** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpdateByQueryRequestParameters.Fields** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpdateRequestParameters.Consistency** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpdateRequestParameters.Script** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpdateRequestParameters.ScriptedUpsert** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpdateRequestParameters.ScriptId** *Removed (Breaking)* 
**public method Elasticsearch.Net.UpgradeRequestParameters.AllowNoIndices** *Removed (Breaking)* 
**public class Elasticsearch.Net.WatcherInfoRequestParameters** *Removed (Breaking)* 
