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


		IObservable<IReindexResponse<T>> Reindex<T>(Func<ReindexDescriptor<T>, ReindexDescriptor<T>> reindexSelector) where T : class;
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
		IIndicesShardResponse Snapshot(Func<SnapshotDescriptor, SnapshotDescriptor> snapShotSelector = null);
		Task<IIndicesShardResponse> SnapshotAsync(Func<SnapshotDescriptor, SnapshotDescriptor> snapShotSelector = null);
		IIndicesShardResponse Refresh(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null);
		Task<IIndicesShardResponse> RefreshAsync(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null);
		ISegmentsResponse Segments(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null);
		Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null);
		IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null);
		Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null);
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
		IUnregisterPercolateResponse UnregisterPercolator(string name, Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector = null);
		Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(string name, Func<UnregisterPercolatorDescriptor, UnregisterPercolatorDescriptor> selector = null);
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
		IIndicesResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null);
		Task<IIndicesResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null);
		IIndicesOperationResponse CreateIndex(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector);
		Task<IIndicesOperationResponse> CreateIndexAsync(string index, Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector);
		IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null);
		Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null);

		IGlobalStatsResponse IndicesStats(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null);
		Task<IGlobalStatsResponse> IndicesStatsAsync(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null);
		INodeInfoResponse ClusterNodeInfo(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector = null);
		Task<INodeInfoResponse> ClusterNodeInfoAsync(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector = null);
		INodeStatsResponse ClusterNodeStats(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector = null);
		Task<INodeStatsResponse> ClusterNodeStatsAsync(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector = null);
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

		IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector);
		Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector);
		ICountResponse Count<T>(Func<CountDescriptor<T>, CountDescriptor<T>> countSelector) where T : class;
		Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, CountDescriptor<T>> countSelector) where T : class;
		IDeleteResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class;
		Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector) where T : class;
		IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector);
		Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector);
		IBulkResponse DeleteMany<T>(IEnumerable<T> @objects, string index = null, string type = null) where T : class;
		Task<IBulkResponse> DeleteManyAsync<T>(IEnumerable<T> objects, string index = null, string type = null) where T : class;

		IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class;

		Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class;

		IDeleteResponse Delete<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector) where T : class;
		Task<IDeleteResponse> DeleteAsync<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector) where T : class;
		IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector);
		Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector);
		FieldSelection<T> SourceFields<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class;
		Task<FieldSelection<T>> SourceFieldsAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class;
		T Source<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class;
		Task<T> SourceAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class;
		IGetResponse<T> Get<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class;
		Task<IGetResponse<T>> GetAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector) where T : class;
		IIndicesOperationResponse Alias(Func<AliasDescriptor, AliasDescriptor> aliasSelector);
		Task<IIndicesOperationResponse> AliasAsync(Func<AliasDescriptor, AliasDescriptor> aliasSelector);
		IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor);
		Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor);
		IBulkResponse IndexMany<T>(IEnumerable<T> @objects, string index = null, string type = null) where T : class;

		Task<IBulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, string index = null, string type = null)
			where T : class;

		IIndicesOperationResponse Optimize(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null);
		Task<IIndicesOperationResponse> OptimizeAsync(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null);
	}
}
