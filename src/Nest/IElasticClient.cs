using System;
using System.Threading.Tasks;
using Nest.Domain;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IElasticClient
	{
		IConnection Connection { get; }
		ElasticSerializer Serializer { get;  }
		IRawElasticClient Raw { get; }
		ElasticInferrer Infer { get; }

		IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector);
		IBulkResponse Bulk(BulkDescriptor bulkDescriptor);
		Task<IBulkResponse> BulkAsync(BulkDescriptor bulkDescriptor);

		ICountResponse Count(Func<QueryDescriptor, BaseQuery> querySelector);
		ICountResponse Count(IEnumerable<string> indices, Func<QueryDescriptor, BaseQuery> querySelector);
		ICountResponse Count(IEnumerable<string> indices, IEnumerable<string> types, Func<QueryDescriptor, BaseQuery> querySelector);

		ICountResponse Count<T>(Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;
		ICountResponse Count<T>(IEnumerable<string> indices, Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;
		ICountResponse Count<T>(IEnumerable<string> indices, IEnumerable<string> types, Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;

		ICountResponse CountAll(Func<QueryDescriptor, BaseQuery> querySelector);
		ICountResponse CountAll<T>(Func<QueryDescriptor<T>, BaseQuery> querySelector) where T : class;

		IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<T> objects) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index, string type) where T : class;
		IBulkResponse DeleteMany<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;

		IDeleteResponse Delete<T>(T @object) where T : class;
		IDeleteResponse Delete<T>(T @object, DeleteParameters deleteParameters) where T : class;
		IDeleteResponse Delete<T>(T @object, string index) where T : class;
		IDeleteResponse Delete<T>(T @object, string index, DeleteParameters deleteParameters) where T : class;
		IDeleteResponse Delete<T>(T @object, string index, string type) where T : class;
		IDeleteResponse Delete<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class;
		 
		Task<IDeleteResponse> DeleteAsync<T>(T @object) where T : class;
		Task<IDeleteResponse> DeleteAsync<T>(T @object, DeleteParameters deleteParameters) where T : class;
		Task<IDeleteResponse> DeleteAsync<T>(T @object, string index) where T : class;
		Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, DeleteParameters deleteParameters) where T : class;
		Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, string type) where T : class;
		Task<IDeleteResponse> DeleteAsync<T>(T @object, string index, string type, DeleteParameters deleteParameters) where T : class;

		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters, string type) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index, string type) where T : class;


		IDeleteResponse DeleteById(string index, string type, int id);
		IDeleteResponse DeleteById(string index, string type, int id, DeleteParameters deleteParameters);
		IDeleteResponse DeleteById(string index, string type, string id);
		IDeleteResponse DeleteById(string index, string type, string id, DeleteParameters deleteParameters);
		IDeleteResponse DeleteById<T>(int id) where T : class;
		IDeleteResponse DeleteById<T>(int id, DeleteParameters deleteParameters) where T : class;
		IDeleteResponse DeleteById<T>(string id) where T : class;
		IDeleteResponse DeleteById<T>(string id, DeleteParameters deleteParameters) where T : class;
		Task<IDeleteResponse> DeleteByIdAsync(string index, string type, int id);
		Task<IDeleteResponse> DeleteByIdAsync(string index, string type, int id, DeleteParameters deleteParameters);
		Task<IDeleteResponse> DeleteByIdAsync(string index, string type, string id);
		Task<IDeleteResponse> DeleteByIdAsync(string index, string type, string id, DeleteParameters deleteParameters);
		Task<IDeleteResponse> DeleteByIdAsync<T>(int id) where T : class;
		Task<IDeleteResponse> DeleteByIdAsync<T>(int id, DeleteParameters deleteParameters) where T : class;
		Task<IDeleteResponse> DeleteByIdAsync<T>(string id) where T : class;
		Task<IDeleteResponse> DeleteByIdAsync<T>(string id, DeleteParameters deleteParameters) where T : class;

		IDeleteResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class;
		Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> query, DeleteByQueryParameters parameters = null) where T : class;

		T Get<T>(int id) where T : class;
		T Get<T>(string id) where T : class;
		T Get<T>(string index, string type, int id) where T : class;
		T Get<T>(string index, string type, string id) where T : class;
		T Get<T>(Action<GetDescriptor<T>> getSelector) where T : class;

		FieldSelection<T> GetFieldSelection<T>(Action<GetDescriptor<T>> getSelector) where T : class;

		IGetResponse<T> GetFull<T>(int id) where T : class;
		IGetResponse<T> GetFull<T>(string id) where T : class;
		IGetResponse<T> GetFull<T>(string index, string type, int id) where T : class;
		IGetResponse<T> GetFull<T>(string index, string type, string id) where T : class;
		IGetResponse<T> GetFull<T>(Action<GetDescriptor<T>> getSelector) where T : class;

		IEnumerable<T> MultiGet<T>(IEnumerable<int> ids) where T : class;
		IEnumerable<T> MultiGet<T>(IEnumerable<string> ids) where T : class;
		IEnumerable<T> MultiGet<T>(string index, IEnumerable<int> ids) where T : class;
		IEnumerable<T> MultiGet<T>(string index, IEnumerable<string> ids) where T : class;
		
		IEnumerable<T> MultiGet<T>(string index, string type, IEnumerable<int> ids) where T : class;
		IEnumerable<T> MultiGet<T>(string index, string type, IEnumerable<string> ids) where T : class;

		MultiGetResponse MultiGetFull(Action<MultiGetDescriptor> multiGetSelector);

		IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<T> objects) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index, string type) where T : class;
		IBulkResponse IndexMany<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;

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

		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<BulkParameters<T>> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index, SimpleBulkParameters bulkParameters) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index, string type) where T : class;
		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index, string type, SimpleBulkParameters bulkParameters) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, IndexParameters indexParameters) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, IndexParameters indexParameters) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, IndexParameters indexParameters) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, int id) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, int id, IndexParameters indexParameters) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, string id) where T : class;
		Task<IIndexResponse> IndexAsync<T>(T @object, string index, string type, string id, IndexParameters indexParameters) where T : class;
		

		IObservable<IReindexResponse<T>> Reindex<T>(Func<ReindexDescriptor<T>, ReindexDescriptor<T>> reindexSelector) where T : class;

		//alias
		IIndicesOperationResponse Alias(AliasParams aliasParams);
		IIndicesOperationResponse Alias(IEnumerable<AliasParams> aliases);
		IIndicesOperationResponse Alias(IEnumerable<string> aliases);
		IIndicesOperationResponse Alias(IEnumerable<string> indices, string alias);
		IIndicesOperationResponse Alias(string alias);
		IIndicesOperationResponse Alias(string index, IEnumerable<string> aliases);
		IIndicesOperationResponse Alias(string index, string alias);
		IIndicesOperationResponse RemoveAlias(AliasParams aliasParams);
		IIndicesOperationResponse RemoveAlias(IEnumerable<string> aliases);
		IIndicesOperationResponse RemoveAlias(string alias);
		IIndicesOperationResponse RemoveAlias(string index, IEnumerable<string> aliases);
		IIndicesOperationResponse RemoveAlias(string index, string alias);
		IIndicesOperationResponse RemoveAliases(IEnumerable<AliasParams> aliases);
		
		IEnumerable<string> GetIndicesPointingToAlias(string alias);
		IIndicesOperationResponse Swap(string alias, IEnumerable<string> oldIndices, IEnumerable<string> newIndices);
		IIndicesOperationResponse Rename(string index, string oldAlias, string newAlias);
		//end alias
		
		//converted

		IQueryResponse<T> Scroll<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector)
			where T : class;
		Task<IQueryResponse<T>> ScrollAsync<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector)
			where T : class;
		
		IUpdateResponse Update<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class;
		IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector) 
			where T : class
			where K : class;
		Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector) where T : class;
		Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector) 
			where T : class
			where K : class;
		ISettingsOperationResponse UpdateSettings(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector);
		Task<ISettingsOperationResponse> UpdateSettingsAsync(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector);
		IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector) where T : class;
		Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector) where T : class;
		IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector);
		Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector);
		IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector);
		Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector);
		IIndicesShardResponse Snapshot(Func<SnapshotDescriptor, SnapshotDescriptor> snapShotSelector);
		Task<IIndicesShardResponse> SnapshotAsync(Func<SnapshotDescriptor, SnapshotDescriptor> snapShotSelector);
		IIndicesShardResponse Refresh(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector);
		Task<IIndicesShardResponse> RefreshAsync(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector);
		ISegmentsResponse Segments(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector);
		Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector);
		IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector);
		Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector);
		IIndicesOperationResponse PutWarmer(Func<PutWarmerDescriptor, PutWarmerDescriptor> selector);
		Task<IIndicesOperationResponse> PutWarmerAsync(Func<PutWarmerDescriptor, PutWarmerDescriptor> selector);
		IWarmerResponse GetWarmer(Func<GetWarmerDescriptor, GetWarmerDescriptor> selector);
		Task<IWarmerResponse> GetWarmerAsync(Func<GetWarmerDescriptor, GetWarmerDescriptor> selector);
		IIndicesOperationResponse DeleteWarmer(Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector);
		Task<IIndicesOperationResponse> DeleteWarmerAsync(Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector);
		ITemplateResponse GetTemplate(Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector);
		Task<ITemplateResponse> GetTemplateAsync(Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector);
		IIndicesOperationResponse PutTemplate(Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector);
		Task<IIndicesOperationResponse> PutTemplateAsync(Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector);
		IIndicesOperationResponse DeleteTemplate(Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector);
		Task<IIndicesOperationResponse> DeleteTemplateAync(Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector);
		IUnregisterPercolateResponse UnregisterPercolator(Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector);
		Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector);
		IRegisterPercolateResponse RegisterPercolator<T>(
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class;
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(
			Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector) where T : class;
		IPercolateResponse Percolate<T>(
			Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class;
		Task<IPercolateResponse> PercolateAsync<T>(
			Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector) where T : class;
		IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector) where T : class;
		Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector) where T : class;
		IGetMappingResponse GetMapping(Func<GetMappingDescriptor, GetMappingDescriptor> selector);
		Task<IGetMappingResponse> GetMappingAsync(Func<GetMappingDescriptor, GetMappingDescriptor> selector);
		IIndicesResponse DeleteMapping(Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector);
		Task<IIndicesResponse> DeleteMappingAsync(Func<DeleteMappingDescriptor, DeleteMappingDescriptor> selector);
		IIndicesOperationResponse Flush(Func<FlushDescriptor, FlushDescriptor> selector);
		Task<IIndicesOperationResponse> FlushAsync(Func<FlushDescriptor, FlushDescriptor> selector);
		IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector);
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector);
		IIndicesResponse DeleteIndex(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector);
		Task<IIndicesResponse> DeleteIndexAsync(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector);
		IIndicesResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector);
		Task<IIndicesResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector);
		IIndicesOperationResponse CreateIndex(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector);
		Task<IIndicesOperationResponse> CreateIndexAsync(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector);
		IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null);
		Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null);

		IGlobalStatsResponse IndicesStats(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector);
		Task<IGlobalStatsResponse> IndicesStatsAsync(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector);
		INodeInfoResponse ClusterNodeInfo(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector);
		Task<INodeInfoResponse> ClusterNodeInfoAsync(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector);
		INodeStatsResponse ClusterNodeStats(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector);
		Task<INodeStatsResponse> ClusterNodeStatsAsync(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector);
		IIndexExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector);
		Task<IIndexExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector);
		IQueryResponse<T> MoreLikeThis<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class;
		Task<IQueryResponse<T>> MoreLikeThisAsync<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector) where T : class;
		IHealthResponse Health(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector);
		Task<IHealthResponse> HealthAsync(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector);
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector);
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector);

		IQueryResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class;
		IQueryResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class;

		Task<IQueryResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class;
		Task<IQueryResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class;
	}
}
