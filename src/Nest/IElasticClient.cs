using System;
using Nest.FactoryDsl;
namespace Nest
{
  public interface IElasticClient
  {
    IIndicesOperationResponse Alias(AliasParams aliasParams);
    IIndicesOperationResponse Alias(System.Collections.Generic.IEnumerable<AliasParams> aliases);
    IIndicesOperationResponse Alias(System.Collections.Generic.IEnumerable<string> aliases);
    IIndicesOperationResponse Alias(System.Collections.Generic.IEnumerable<string> indices, string alias);
    IIndicesOperationResponse Alias(string alias);
    IIndicesOperationResponse Alias(string index, System.Collections.Generic.IEnumerable<string> aliases);
    IIndicesOperationResponse Alias(string index, string alias);
    IAnalyzeResponse Analyze(AnalyzeParams analyzeParams, string text);
    IAnalyzeResponse Analyze(string text);
    IAnalyzeResponse Analyze<T>(System.Linq.Expressions.Expression<Func<T, object>> selector, string index, string text) where T : class;
    IAnalyzeResponse Analyze<T>(System.Linq.Expressions.Expression<Func<T, object>> selector, string text) where T : class;
    IIndicesResponse ClearCache();
    IIndicesResponse ClearCache(ClearCacheOptions options);
    IIndicesResponse ClearCache(System.Collections.Generic.IEnumerable<string> indices, ClearCacheOptions options);
    IIndicesResponse ClearCache<T>() where T : class;
    IIndicesResponse ClearCache<T>(ClearCacheOptions options) where T : class;
    IIndicesOperationResponse CloseIndex(string index);
    IIndicesOperationResponse CloseIndex<T>() where T : class;
    ICountResponse Count(Func<QueryDescriptor, BaseQuery> querySelector);
	ICountResponse Count(System.Collections.Generic.IEnumerable<string> indices, Func<QueryDescriptor, BaseQuery> querySelector);
	ICountResponse Count(System.Collections.Generic.IEnumerable<string> indices, System.Collections.Generic.IEnumerable<string> types, Func<QueryDescriptor, BaseQuery> querySelector);
    ICountResponse Count<T>(Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;
    ICountResponse Count<T>(System.Collections.Generic.IEnumerable<string> indices, Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;
    ICountResponse Count<T>(System.Collections.Generic.IEnumerable<string> indices, System.Collections.Generic.IEnumerable<string> types, Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;
    ICountResponse CountAll(Func<QueryDescriptor, BaseQuery> querySelector);
    ICountResponse CountAll(string query);
    ICountResponse CountAll<T>(Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;
    IIndicesResponse CreateIndex(string index, IndexSettings settings);
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<T> objects) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<T> objects, string index) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type) where T : class;
    IBulkResponse Delete<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
    IDeleteResponse Delete<T>(T @object) where T : class;
    IDeleteResponse Delete<T>(T @object, DeleteParameters deleteParameters) where T : class;
    IDeleteResponse Delete<T>(T @object, string index) where T : class;
    IDeleteResponse Delete<T>(T @object, string index, DeleteParameters deleteParameters) where T : class;
    IDeleteResponse Delete<T>(T @object, string index, string type) where T : class;
    IDeleteResponse Delete<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<T> objects) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> DeleteAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteAsync<T>(T @object) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteAsync<T>(T @object, DeleteParameters deleteParameters) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteAsync<T>(T @object, string index) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, DeleteParameters deleteParameters) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, string type) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class;
    IDeleteResponse DeleteById(string index, string type, int id);
    IDeleteResponse DeleteById(string index, string type, int id, DeleteParameters deleteParameters);
    IDeleteResponse DeleteById(string index, string type, string id);
    IDeleteResponse DeleteById(string index, string type, string id, DeleteParameters deleteParameters);
    IDeleteResponse DeleteById<T>(int id) where T : class;
    IDeleteResponse DeleteById<T>(int id, DeleteParameters deleteParameters) where T : class;
    IDeleteResponse DeleteById<T>(string id) where T : class;
    IDeleteResponse DeleteById<T>(string id, DeleteParameters deleteParameters) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync(string index, string type, int id);
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync(string index, string type, int id, DeleteParameters deleteParameters);
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync(string index, string type, string id);
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync(string index, string type, string id, DeleteParameters deleteParameters);
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync<T>(int id) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync<T>(int id, DeleteParameters deleteParameters) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync<T>(string id) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByIdAsync<T>(string id, DeleteParameters deleteParameters) where T : class;
    IDeleteResponse DeleteByQuery(Action<RoutingQueryPathDescriptor> query, DeleteByQueryParameters parameters = null);
    IDeleteResponse DeleteByQuery(string query, DeleteByQueryParameters parameters = null);
    IDeleteResponse DeleteByQuery<T>(Action<RoutingQueryPathDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class;
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByQueryAsync(Action<RoutingQueryPathDescriptor> query, DeleteByQueryParameters parameters = null);
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByQueryAsync(string query, DeleteByQueryParameters parameters = null);
    System.Threading.Tasks.Task<IDeleteResponse> DeleteByQueryAsync<T>(Action<RoutingQueryPathDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class;
    IIndicesResponse DeleteIndex(string index);
    IIndicesResponse DeleteIndex<T>() where T : class;
    IIndicesResponse DeleteMapping<T>() where T : class;
    IIndicesResponse DeleteMapping<T>(string index) where T : class;
    IIndicesResponse DeleteMapping<T>(string index, string type) where T : class;

    IIndicesResponse DeleteMapping(Type t);
    IIndicesResponse DeleteMapping(Type t, string index);
    IIndicesResponse DeleteMapping(Type t, string index, string type);

    IIndicesOperationResponse Flush(bool refresh = false);
    IIndicesOperationResponse Flush(System.Collections.Generic.IEnumerable<string> indices, bool refresh = false);
    IIndicesOperationResponse Flush(string index, bool refresh = false);
    IIndicesOperationResponse Flush<T>(bool refresh = false) where T : class;
    System.Collections.Generic.IEnumerable<T> Get<T>(System.Collections.Generic.IEnumerable<int> ids) where T : class;
    System.Collections.Generic.IEnumerable<T> Get<T>(System.Collections.Generic.IEnumerable<string> ids) where T : class;
    T Get<T>(int id) where T : class;
    T Get<T>(string id) where T : class;
    System.Collections.Generic.IEnumerable<T> Get<T>(string index, string type, System.Collections.Generic.IEnumerable<int> ids) where T : class;
    System.Collections.Generic.IEnumerable<T> Get<T>(string index, string type, System.Collections.Generic.IEnumerable<string> ids) where T : class;
    T Get<T>(string index, string type, int id) where T : class;
    T Get<T>(string index, string type, string id) where T : class;
    IIndexSettingsResponse GetIndexSettings();
    IIndexSettingsResponse GetIndexSettings(string index);
    System.Collections.Generic.IEnumerable<string> GetIndicesPointingToAlias(string alias);
    TypeMapping GetMapping(string index, string type);
    TypeMapping GetMapping<T>() where T : class;
    TypeMapping GetMapping<T>(string index) where T : class;
    TypeMapping GetMapping(Type t);
    TypeMapping GetMapping(Type t, string index);

    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<T> objects) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<T> objects, string index) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type) where T : class;
    IBulkResponse IndexMany<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
    IIndexResponse Index<T>(T @object) where T : class;
    IIndexResponse Index<T>(T @object, IndexParameters indexParameters) where T : class;
    IIndexResponse Index<T>(T @object, string index) where T : class;
    IIndexResponse Index<T>(T @object, string index, IndexParameters indexParameters) where T : class;
    IIndexResponse Index<T>(T @object, string index, string type) where T : class;
    IIndexResponse Index<T>(T @object, string index = null, string type = null, IndexParameters indexParameters = null) where T : class;
    IIndexResponse Index<T>(T @object, string index, string type, int id) where T : class;
    IIndexResponse Index<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class;
    IIndexResponse Index<T>(T @object, string index, string type, string id) where T : class;
    IIndexResponse Index<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<T> objects) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type) where T : class;
    System.Threading.Tasks.Task<IBulkResponse> IndexManyAsync<T>(System.Collections.Generic.IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, IndexParameters indexParameters) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, IndexParameters indexParameters) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, IndexParameters indexParameters) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, int id) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, string id) where T : class;
    System.Threading.Tasks.Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class;
    IIndexExistsResponse IndexExists(string index);
    bool IsValid { get; }
    IIndicesResponse Map(TypeMapping typeMapping);
    IIndicesResponse Map(TypeMapping typeMapping, string index);
    IIndicesResponse Map<T>(int maxRecursion = 0) where T : class;
    IIndicesResponse Map<T>(string index, int maxRecursion = 0) where T : class;
    IIndicesResponse Map<T>(string index, string type, int maxRecursion = 0) where T : class;
    IIndicesResponse Map(Type t, int maxRecursion = 0);
    IIndicesResponse Map(Type t, string index, int maxRecursion = 0);
    IIndicesResponse Map(Type t, string index, string type, int maxRecursion = 0);

    IIndicesOperationResponse OpenIndex(string index);
    IIndicesOperationResponse OpenIndex<T>() where T : class;
    IIndicesOperationResponse Optimize();
    IIndicesOperationResponse Optimize(OptimizeParams optimizeParameters);
    IIndicesOperationResponse Optimize(System.Collections.Generic.IEnumerable<string> indices);
    IIndicesOperationResponse Optimize(System.Collections.Generic.IEnumerable<string> indices, OptimizeParams optimizeParameters);
    IIndicesOperationResponse Optimize(string index);
    IIndicesOperationResponse Optimize(string index, OptimizeParams optimizeParameters);
    IIndicesOperationResponse Optimize<T>() where T : class;
    IIndicesOperationResponse Optimize<T>(OptimizeParams optimizeParameters) where T : class;
    IPercolateResponse Percolate<T>(string index, string type, T @object) where T : class;
    IPercolateResponse Percolate<T>(string index, T @object) where T : class;
    IPercolateResponse Percolate<T>(T @object) where T : class;
    IIndicesShardResponse Refresh();
    IIndicesShardResponse Refresh(System.Collections.Generic.IEnumerable<string> indices);
    IIndicesShardResponse Refresh(string index);
    IIndicesShardResponse Refresh<T>() where T : class;
    IRegisterPercolateResponse RegisterPercolator(string index, string name, string query);
    IRegisterPercolateResponse RegisterPercolator(string name, Action<QueryPathDescriptor<dynamic>> querySelector);
    IRegisterPercolateResponse RegisterPercolator<T>(string name, Action<QueryPathDescriptor<T>> querySelector) where T : class;
    IIndicesOperationResponse RemoveAlias(AliasParams aliasParams);
    IIndicesOperationResponse RemoveAlias(System.Collections.Generic.IEnumerable<string> aliases);
    IIndicesOperationResponse RemoveAlias(string alias);
    IIndicesOperationResponse RemoveAlias(string index, System.Collections.Generic.IEnumerable<string> aliases);
    IIndicesOperationResponse RemoveAlias(string index, string alias);
    IIndicesOperationResponse RemoveAliases(System.Collections.Generic.IEnumerable<AliasParams> aliases);
    IIndicesOperationResponse Rename(string index, string oldAlias, string newAlias);

    IQueryResponse<dynamic> Search(
      SearchBuilder searchBuilder,
      string index = null,
      string type = null,
      string routing = null,
      SearchType? searchType = null);
    IQueryResponse<T> Search<T>(SearchBuilder searchBuilder,
      string index = null,
      string type = null,
      string routing = null,
      SearchType? searchType = null) where T : class;

    IQueryResponse<dynamic> Search(Func<SearchDescriptor<dynamic>, SearchDescriptor<dynamic>> searcher);
    IQueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searcher) where T : class;
    IQueryResponse<T> SearchRaw<T>(string query) where T : class;

    ISegmentsResponse Segments();
    ISegmentsResponse Segments(System.Collections.Generic.IEnumerable<string> indices);
    ISegmentsResponse Segments(string index);
    string Serialize(object @object);
    IIndicesShardResponse Snapshot();
    IIndicesShardResponse Snapshot(System.Collections.Generic.IEnumerable<string> indices);
    IIndicesShardResponse Snapshot(string index);
    IIndicesShardResponse Snapshot<T>() where T : class;
    IGlobalStatsResponse Stats();
    IGlobalStatsResponse Stats(StatsParams parameters);
    IStatsResponse Stats(System.Collections.Generic.IEnumerable<string> indices);
    IStatsResponse Stats(System.Collections.Generic.IEnumerable<string> indices, StatsParams parameters);
    IStatsResponse Stats(string index);
    IIndicesOperationResponse Swap(string alias, System.Collections.Generic.IEnumerable<string> oldIndices, System.Collections.Generic.IEnumerable<string> newIndices);
    bool TryConnect(out ConnectionStatus status);
    IUnregisterPercolateResponse UnregisterPercolator(string index, string name);
    IUnregisterPercolateResponse UnregisterPercolator<T>(string name) where T : class;
    IUpdateResponse Update(Action<UpdateDescriptor<dynamic>> updateSelector);
    IUpdateResponse Update<T>(Action<UpdateDescriptor<T>> updateSelector) where T : class;
    ISettingsOperationResponse UpdateSettings(IndexSettings settings);
    ISettingsOperationResponse UpdateSettings(string index, IndexSettings settings);
    IElasticSearchVersionInfo VersionInfo { get; }
	  IQueryResponse<T> Search<T>(SearchDescriptor<T> descriptor) where T : class;
  }
}
