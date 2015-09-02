using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.Serialization;

namespace Nest
{
	public partial interface IElasticClient
	{
		IElasticsearchSerializer Serializer { get; }
		IElasticsearchClient Raw { get; }
		ElasticInferrer Infer { get; }

		ElasticsearchResponse<T> DoRequest<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;

		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(HttpMethod method, string path, PostData<object> data = null, IRequestParameters requestParameters = null)
			where T : class;

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
		/// </summary>
		/// <returns>An IObservable you can subscribe to to listen to the progress of the reindexation process</returns>
		IObservable<IReindexResponse<T>> Reindex<T>(Func<ReindexDescriptor<T>, ReindexDescriptor<T>> reindexSelector)
			where T : class;

		/// <summary>
		/// The update API allows to update a document based on a script provided. 
		/// <para>The operation gets the document (collocated with the shard) from the index, runs the script 
		/// (with optional script language and parameters), and index back the result 
		/// (also allows to delete, or ignore the operation). </para>
		/// <para>It uses versioning to make sure no updates have happened during the "get" and "reindex".</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-update.html
		/// </summary>
		/// <typeparam name="T">The type to describe the document to be updated</typeparam>
		/// <param name="updateSelector">a descriptor that describes the update operation</param>
		IUpdateResponse Update<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector)
			where T : class;

		/// <inheritdoc/>
		IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class;

		/// <inheritdoc/>
		IUpdateResponse Update<T>(IUpdateRequest<T, T> updateRequest)
			where T : class;

		/// <inheritdoc/>
		IUpdateResponse Update<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T>(IUpdateRequest<T, T> updateRequest)
			where T : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class;

		/// <inheritdoc/>
		Task<IUpdateResponse> UpdateAsync<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class;

		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="openIndexSelector">A descriptor thata describes the open index operation</param>
		IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector);

		/// <inheritdoc/>
		IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> OpenIndexAsync(IOpenIndexRequest openIndexRequest);

		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="closeIndexSelector">A descriptor thata describes the close index operation</param>
		IIndicesOperationResponse CloseIndex(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector);

		/// <inheritdoc/>
		IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest);

		/// <summary>
		/// The cluster state API allows to get a comprehensive state information of the whole cluster.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html
		/// </summary>
		/// <param name="clusterStateSelector">A descriptor that describes the parameters for the cluster state operation</param>
		IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null);

		/// <inheritdoc/>
		IClusterStateResponse ClusterState(IClusterStateRequest clusterStateRequest);

		/// <inheritdoc/>
		Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null);

		/// <inheritdoc/>
		Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest clusterStateRequest);



		/// <summary>
		/// The get settings API allows to retrieve settings of index/indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-settings.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get index settings operation</param>
		IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc/>
		IIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest getIndexSettingsRequest);

		/// <inheritdoc/>
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, IGetIndexSettingsRequest> selector);

		/// <inheritdoc/>
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest getIndexSettingsRequest);

		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		IIndicesResponse DeleteIndex(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector);

		/// <inheritdoc/>
		IIndicesResponse DeleteIndex(IDeleteIndexRequest deleteIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesResponse> DeleteIndexAsync(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector);

		/// <inheritdoc/>
		Task<IIndicesResponse> DeleteIndexAsync(IDeleteIndexRequest deleteIndexRequest);

		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="createIndexSelector">A descriptor that describes the parameters for the create index operation</param>
		IIndicesOperationResponse CreateIndex(IndexName indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null);

		/// <inheritdoc/>
		IIndicesOperationResponse CreateIndex(ICreateIndexRequest createIndexRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CreateIndexAsync(IndexName indexName, Func<CreateIndexDescriptor, ICreateIndexRequest> createIndexSelector = null);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest createIndexRequest);

		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null);

		/// <inheritdoc/>
		IRootInfoResponse RootNodeInfo(IInfoRequest infoRequest);

		/// <inheritdoc/>
		Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null);

		/// <inheritdoc/>
		Task<IRootInfoResponse> RootNodeInfoAsync(IInfoRequest infoRequest);

		/// <summary>
		/// The cluster nodes info API allows to retrieve one or more (or all) of the cluster nodes information.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-info.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes info operation</param>
		INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null);

		/// <inheritdoc/>
		INodeInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest);

		/// <inheritdoc/>
		Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null);

		/// <inheritdoc/>
		Task<INodeInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest);

		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-stats.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes stats operation</param>
		INodeStatsResponse NodesStats(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null);

		/// <inheritdoc/>
		INodeStatsResponse NodesStats(INodesStatsRequest nodesStatsRequest);

		/// <inheritdoc/>
		Task<INodeStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null);

		/// <inheritdoc/>
		Task<INodeStatsResponse> NodesStatsAsync(INodesStatsRequest nodesStatsRequest);

		/// <summary>
		/// An API allowing to get the current hot threads on each node in the cluster.
		/// </summary>
		/// <param name="selector"></param>
		/// <returns>An optional descriptor to further describe the nodes hot threads operation</returns>
		INodesHotThreadsResponse NodesHotThreads(Func<NodesHotThreadsDescriptor, NodesHotThreadsDescriptor> selector = null);

		/// <inheritdoc/>
		INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest nodesHotThreadsRequest);

		/// <inheritdoc/>
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, NodesHotThreadsDescriptor> selector = null);

		/// <inheritdoc/>
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest nodesHotThreadsRequest);

		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html
		/// </summary>
		/// <param name="clusterHealthSelector">An optional descriptor to further describe the cluster health operation</param>
		IHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null);

		/// <inheritdoc/>
		IHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest);

		/// <inheritdoc/>
		Task<IHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null);

		/// <inheritdoc/>
		Task<IHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest);

		/// <summary>
		/// allows to retrieve statistics from a cluster wide perspective. The API returns basic index metrics 
		/// (shard numbers, store size, memory usage) and information about the current nodes that form the 
		/// cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
		/// </summary>
		/// <param name="clusterStatsSelector">A descriptor that describes the cluster stats operation</param>
		IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, ClusterStatsDescriptor> clusterStatsSelector = null);

		/// <inheritdoc/>
		Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, ClusterStatsDescriptor> clusterStatsSelector = null);

		/// <inheritdoc/>
		IClusterStatsResponse ClusterStats(IClusterStatsRequest clusterStatsRequest);

		/// <inheritdoc/>
		Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest clusterStatsRequest);

		/// <summary>
		/// Allows to explicitly execute a cluster reroute allocation command including specific commands. 
		/// For example, a shard can be moved from one node to another explicitly, an allocation can be canceled, 
		/// or an unassigned shard can be explicitly allocated on a specific node.
		/// </summary>
		IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, ClusterRerouteDescriptor> clusterRerouteSelector);

		/// <inheritdoc/>
		Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, ClusterRerouteDescriptor> clusterRerouteSelector);

		/// <inheritdoc/>
		IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest clusterRerouteRequest);

		/// <inheritdoc/>
		Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest clusterRerouteRequest);

		/// <summary>
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="analyzeSelector">A descriptor that describes the analyze operation</param>
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector);

		/// <inheritdoc/>
		IAnalyzeResponse Analyze(IAnalyzeRequest analyzeRequest);

		/// <inheritdoc/>
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector);

		/// <inheritdoc/>
		Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest analyzeRequest);


		/// <summary>
		/// The /_search/template endpoint allows to use the mustache language to pre render search 
		/// requests, before they are executed and fill existing templates with template parameters.
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		/// <returns></returns>
		ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc/>
		ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc/>
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc/>
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;



		/// <summary>
		/// The delete by query API allows to delete documents from one or more indices and one or more types based on a query.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="deleteByQuerySelector">An optional descriptor to further describe the delete by query operation</param>
		IDeleteByQueryResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector)
			where T : class;

		/// <inheritdoc/>
		IDeleteByQueryResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest);

		/// <inheritdoc/>
		Task<IDeleteByQueryResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector)
			where T : class;

		/// <inheritdoc/>
		Task<IDeleteByQueryResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest);

		/// <summary>
		/// The bulk API makes it possible to perform many index/delete operations in a single API call. 
		/// This can greatly increase the indexing speed.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="bulkRequest">A descriptor the describe the index/create/delete operation for this bulk operation</param>
		IBulkResponse Bulk(IBulkRequest bulkRequest);

		/// <inheritdoc/>
		IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(IBulkRequest bulkRequest);

		/// <inheritdoc/>
		Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector);


		/// <summary>
		/// The index API adds or updates a typed JSON document in a specific index, making it searchable. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="object">The object to be indexed, Id will be inferred (Id property or IdProperty attribute on type)</param>
		/// <param name="indexSelector">Optionally furter describe the index operation i.e override type/index/id</param>
		IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null)
			where T : class;

		/// <inheritdoc/>
		IIndexResponse Index<T>(IIndexRequest<T> indexRequest)
			where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IIndexRequest<T>> indexSelector = null)
			where T : class;

		/// <inheritdoc/>
		Task<IIndexResponse> IndexAsync<T>(IIndexRequest<T> indexRequest)
			where T : class;

		/// <summary>
		///The delete API allows to delete a typed JSON document from a specific index based on its id. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="deleteSelector">Describe the delete operation, i.e type/index/id</param>
		IDeleteResponse Delete<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector)
			where T : class;

		/// <inheritdoc/>
		IDeleteResponse Delete(IDeleteRequest deleteRequest);

		/// <inheritdoc/>
		Task<IDeleteResponse> DeleteAsync<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IDeleteResponse> DeleteAsync(IDeleteRequest deleteRequest);

		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing). 
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document 
		/// provided by the get API.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <param name="multiGetSelector">A descriptor describing which documents should be fetched</param>
		IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector);

		/// <inheritdoc/>
		IMultiGetResponse MultiGet(IMultiGetRequest multiGetRequest);

		/// <inheritdoc/>
		Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector);

		/// <inheritdoc/>
		Task<IMultiGetResponse> MultiGetAsync(IMultiGetRequest multiGetRequest);

		/// <summary>
		/// Use the /{index}/{type}/{id}/_source endpoint to get just the _source field of the document, 
		/// without any additional content around it. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="getSelector">A descriptor that describes which document's source to fetch</param>
		T Source<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector)
			where T : class;

		/// <inheritdoc/>
		T Source<T>(ISourceRequest sourceRequest)
			where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector)
			where T : class;

		/// <inheritdoc/>
		Task<T> SourceAsync<T>(ISourceRequest sourceRequest)
			where T : class;

		/// <summary>
		/// Use the /{index}/{type}/{id} to get the document and its metadata
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html#_source
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="getSelector">A descriptor that describes which document's source to fetch</param>
		IGetResponse<T> Get<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector)
			where T : class;

		/// <inheritdoc/>
		IGetResponse<T> Get<T>(IGetRequest getRequest)
			where T : class;

		/// <inheritdoc/>
		Task<IGetResponse<T>> GetAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IGetResponse<T>> GetAsync<T>(IGetRequest getRequest)
			where T : class;

		/// <summary>
		/// APIs in elasticsearch accept an index name when working against a specific index, and several indices when applicable. 
		/// <para>The index aliases API allow to alias an index with a name, with all APIs automatically converting the alias name to the 
		/// actual index name.</para><para> An alias can also be mapped to more than one index, and when specifying it, the alias 
		/// will automatically expand to the aliases indices.i</para><para> An alias can also be associated with a filter that will 
		/// automatically be applied when searching, and routing values.</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html
		/// </summary>
		/// <param name="aliasSelector">A desriptor that describes the parameters for the alias operation</param>
		IIndicesOperationResponse Alias(Func<BulkAliasDescriptor, BulkAliasDescriptor> aliasSelector);

		/// <inheritdoc/>
		IIndicesOperationResponse Alias(IBulkAliasRequest aliasRequest);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> AliasAsync(Func<BulkAliasDescriptor, BulkAliasDescriptor> aliasSelector);

		/// <inheritdoc/>
		Task<IIndicesOperationResponse> AliasAsync(IBulkAliasRequest aliasRequest);

		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches 
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> Difference with GetAlias is that this call will also return indices without aliases set</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="getAliasDescriptor">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, GetAliasDescriptor> getAliasDescriptor);

		/// <inheritdoc/>
		IGetAliasesResponse GetAlias(IGetAliasRequest getAliasRequest);

		/// <inheritdoc/>
		Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, GetAliasDescriptor> getAliasDescriptor);

		/// <inheritdoc/>
		Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest getAliasRequest);

		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches 
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="getAliasesDescriptor">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor);

		/// <inheritdoc/>
		IGetAliasesResponse GetAliases(IGetAliasesRequest getAliasesRequest);

		/// <inheritdoc/>
		Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor);

		/// <inheritdoc/>
		Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest getAliasesRequest);

		/// <summary>
		/// Add a single index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-adding
		/// </summary>
		/// <param name="putAliasRequest">A descriptor that describes the put alias request</param>
		IPutAliasResponse PutAlias(IPutAliasRequest putAliasRequest);

		/// <inheritdoc/>
		Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest putAliasRequest);

		/// <inheritdoc/>
		IPutAliasResponse PutAlias(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor);

		/// <inheritdoc/>
		Task<IPutAliasResponse> PutAliasAsync(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor);

		/// <summary>
		/// Delete an index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#deleting
		/// </summary>
		/// <param name="deleteAliasRequest">A descriptor that describes the delete alias request</param>
		IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest deleteAliasRequest);

		/// <inheritdoc/>
		Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest deleteAliasRequest);

		/// <inheritdoc/>
		IDeleteAliasResponse DeleteAlias<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor) where T : class;

		/// <inheritdoc/>
		Task<IDeleteAliasResponse> DeleteAliasAsync<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor) where T : class;

		/// <summary>
		/// Returns information and statistics on terms in the fields of a particular document as stored in the index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="termVectorSelector"></param>
		ITermVectorsResponse TermVectors<T>(Func<TermVectorsDescriptor<T>, TermVectorsDescriptor<T>> termVectorSelector)
			where T : class;

		/// <inheritdoc/>
		ITermVectorsResponse TermVectors(ITermVectorsRequest termvectorRequest);

		/// <inheritdoc/>
		Task<ITermVectorsResponse> TermVectorsAsync<T>(Func<TermVectorsDescriptor<T>, TermVectorsDescriptor<T>> termVectorSelector)
			where T : class;

		/// <inheritdoc/>
		Task<ITermVectorsResponse> TermVectorsAsync(ITermVectorsRequest termvectorRequest);

		/// <summary>
		/// Multi termvectors API allows to get multiple termvectors based on an index, type and id.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="multiTermVectorsSelector">The descriptor describing the multi termvectors operation</param>
		IMultiTermVectorResponse MultiTermVectors<T>(Func<MultiTermVectorsDescriptor<T>, MultiTermVectorsDescriptor<T>> multiTermVectorsSelector)
			where T : class;

		/// <inheritdoc/>
		IMultiTermVectorResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest);

		/// <inheritdoc/>
		Task<IMultiTermVectorResponse> MultiTermVectorsAsync<T>(Func<MultiTermVectorsDescriptor<T>, MultiTermVectorsDescriptor<T>> multiTermVectorsSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IMultiTermVectorResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest);



		/// <summary>
		/// Check if a document exists without returning its contents
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="existsSelector">Describe what document we are looking for</param>
		IExistsResponse DocumentExists<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class;

		/// <inheritdoc/>
		IExistsResponse DocumentExists(IDocumentExistsRequest documentExistsRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> DocumentExistsAsync<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class;

		/// <inheritdoc/>
		Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest documentExistsRequest);

		/// <summary>
		/// Allows to update cluster wide specific settings. Settings updated can either be persistent 
		/// (applied cross restarts) or transient (will not survive a full cluster restart). 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterPutSettingsResponse ClusterSettings(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> clusterHealthSelector);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> clusterHealthSelector);

		/// <inheritdoc/>
		IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest);

		/// <inheritdoc/>
		Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest);

		/// <summary>
		/// Gets cluster wide specific settings. Settings updated can either be persistent 
		/// (applied cross restarts) or transient (will not survive a full cluster restart). 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector);

		/// <inheritdoc/>
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector);

		/// <inheritdoc/>
		IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest = null);

		/// <inheritdoc/>
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest = null);

		/// <summary>
		/// Returns a list of any cluster-level changes (e.g. create index, update mapping, allocate or fail shard) which have not yet been executed.
		/// </summary>
		IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, ClusterPendingTasksDescriptor> pendingTasksSelector = null);

		/// <inheritdoc/>
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, ClusterPendingTasksDescriptor> pendingTasksSelector = null);

		/// <inheritdoc/>
		IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest pendingTasksRequest);

		/// <inheritdoc/>
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest pendingTasksRequest);

		/// <inheritdoc/>
		IExistsResponse AliasExists(Func<AliasExistsDescriptor, AliasExistsDescriptor> selector);

		/// <inheritdoc/>
		IExistsResponse AliasExists(IAliasExistsRequest AliasRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, AliasExistsDescriptor> selector);

		/// <inheritdoc/>
		Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest AliasRequest);

		/// <inheritdoc/>
		IExistsResponse TypeExists(Func<TypeExistsDescriptor, TypeExistsDescriptor> selector);

		/// <inheritdoc/>
		IExistsResponse TypeExists(ITypeExistsRequest TypeRequest);

		/// <inheritdoc/>
		Task<IExistsResponse> TypeExistsAsync(Func<TypeExistsDescriptor, TypeExistsDescriptor> selector);

		/// <inheritdoc/>
		Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest TypeRequest);

		/// <summary>
		/// Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		IPingResponse Ping(Func<PingDescriptor, PingDescriptor> pingSelector = null);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(Func<PingDescriptor, PingDescriptor> pingSelector = null);

		/// <inheritdoc/>
		IPingResponse Ping(IPingRequest pingRequest);

		/// <inheritdoc/>
		Task<IPingResponse> PingAsync(IPingRequest pingRequest);


		/// <inheritdoc/>
		IGetIndexResponse GetIndex(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector);

		/// <inheritdoc/>
		IGetIndexResponse GetIndex(IGetIndexRequest createIndexRequest);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector);

		/// <inheritdoc/>
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest createIndexRequest);

		/// <inheritdoc/>
		ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null);

		ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request);
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null);
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request);

		/// <inheritdoc/>
		ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null);

		ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request);
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null);
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request);

		/// <inheritdoc/>
		ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, CatCountDescriptor> selector = null);

		ICatResponse<CatCountRecord> CatCount(ICatCountRequest request);
		Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, CatCountDescriptor> selector = null);
		Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request);

		/// <inheritdoc/>
		ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null);

		ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request);
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null);
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request);

		/// <inheritdoc/>
		ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null);

		ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request);
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null);
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request);

		/// <inheritdoc/>
		ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null);

		ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request);
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null);
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request);

		/// <inheritdoc/>
		ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null);

		ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request);
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null);
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request);

		/// <inheritdoc/>
		ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null);

		ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request);
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null);
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request);

		/// <inheritdoc/>
		ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null);

		ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request);
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null);
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request);

		/// <inheritdoc/>
		ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null);

		ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request);
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null);
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request);

		/// <inheritdoc/>
		ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null);

		ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request);
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null);
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request);

		/// <inheritdoc/>
		ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null);

		ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request);
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null);
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request);

		/// <inheritdoc/>
		ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null);

		ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request);
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null);
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request);

		/// <inheritdoc/>
		ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, CatSegmentsDescriptor> selector = null);
		ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request);
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(Func<CatSegmentsDescriptor, CatSegmentsDescriptor> selector = null);
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request);

	}
}
