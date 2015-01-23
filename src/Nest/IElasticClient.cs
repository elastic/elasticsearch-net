using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;

namespace Nest
{
	public interface IElasticClient
	{
		IConnection Connection { get; }
		INestSerializer Serializer { get; }
		IElasticsearchClient Raw { get; }
		ElasticInferrer Infer { get; }

		/// <summary>
		/// Helper method that allows you to reindex from one index into another using SCAN and SCROLL.
		/// </summary>
		/// <returns>An IObservable you can subscribe to to listen to the progress of the reindexation process</returns>
		IObservable<IReindexResponse<T>> Reindex<T>(Func<ReindexDescriptor<T>, ReindexDescriptor<T>> reindexSelector)
			where T : class;

		/// <summary>
		/// A search request can be scrolled by specifying the scroll parameter. 
		/// <para>The scroll parameter is a time value parameter (for example: scroll=5m), 
		/// indicating for how long the nodes that participate in the search will maintain relevant resources in
		/// order to continue and support it.</para><para> 
		/// This is very similar in its idea to opening a cursor against a database.</para>
		/// <para> </para><para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html</para>
		/// </summary>
		/// <typeparam name="T">The type that represents the result hits</typeparam>
		/// <param name="scrollRequest">A descriptor that describes the scroll operation</param>
		/// <returns>A query response holding T hits as well as the ScrollId for the next scroll operation</returns>
		ISearchResponse<T> Scroll<T>(IScrollRequest scrollRequest)
			where T : class;

		///<inheritdoc />
		ISearchResponse<T> Scroll<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector)
			where T : class;

		///<inheritdoc />
		Task<ISearchResponse<T>> ScrollAsync<T>(IScrollRequest scrollRequest)
			where T : class;

		///<inheritdoc />
		Task<ISearchResponse<T>> ScrollAsync<T>(Func<ScrollDescriptor<T>, ScrollDescriptor<T>> scrollSelector)
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

		/// <inheritdoc />
		IUpdateResponse Update<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class;

		/// <inheritdoc />
		IUpdateResponse Update<T>(IUpdateRequest<T, T> updateRequest)
			where T : class;

		/// <inheritdoc />
		IUpdateResponse Update<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class;

		/// <inheritdoc />
		Task<IUpdateResponse> UpdateAsync<T>(Func<UpdateDescriptor<T, T>, UpdateDescriptor<T, T>> updateSelector)
			where T : class;

		/// <inheritdoc />
		Task<IUpdateResponse> UpdateAsync<T>(IUpdateRequest<T, T> updateRequest)
			where T : class;

		/// <inheritdoc />
		Task<IUpdateResponse> UpdateAsync<T, K>(Func<UpdateDescriptor<T, K>, UpdateDescriptor<T, K>> updateSelector)
			where T : class
			where K : class;

		/// <inheritdoc />
		Task<IUpdateResponse> UpdateAsync<T, K>(IUpdateRequest<T, K> updateRequest)
			where T : class
			where K : class;

		/// <summary>
		/// Change specific index level settings in real time. Note not all index settings CAN be updated.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-update-settings.html
		/// </summary>
		/// <param name="updateSettingsSelector">A descriptor that strongly types all the updateable settings</param>
		IAcknowledgedResponse UpdateSettings(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector);

		/// <inheritdoc />
		IAcknowledgedResponse UpdateSettings(IUpdateSettingsRequest updateSettingsRequest);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> UpdateSettingsAsync(Func<UpdateSettingsDescriptor, UpdateSettingsDescriptor> updateSettingsSelector);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> UpdateSettingsAsync(IUpdateSettingsRequest updateSettingsRequest);

		/// <summary>
		/// The validate API allows a user to validate a potentially expensive query without executing it.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-validate.html
		/// </summary>
		/// <typeparam name="T">The type used to describe the query</typeparam>
		/// <param name="querySelector">A descriptor that describes the query operation</param>
		IValidateResponse Validate<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector)
			where T : class;

		/// <inheritdoc />
		IValidateResponse Validate(IValidateQueryRequest validateQueryRequest);

		/// <inheritdoc />
		Task<IValidateResponse> ValidateAsync<T>(Func<ValidateQueryDescriptor<T>, ValidateQueryDescriptor<T>> querySelector)
			where T : class;

		/// <inheritdoc />
		Task<IValidateResponse> ValidateAsync(IValidateQueryRequest validateQueryRequest);

		/// <summary>
		/// The open and close index APIs allow to close an index, and later on opening it. 
		/// A closed index has almost no overhead on the cluster (except for maintaining its metadata), and is blocked 
		/// for read/write operations. 
		/// A closed index can be opened which will then go through the normal recovery process.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-open-close.html
		/// </summary>
		/// <param name="openIndexSelector">A descriptor thata describes the open index operation</param>
		IIndicesOperationResponse OpenIndex(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector);

		/// <inheritdoc />
		IIndicesOperationResponse OpenIndex(IOpenIndexRequest openIndexRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> OpenIndexAsync(Func<OpenIndexDescriptor, OpenIndexDescriptor> openIndexSelector);

		/// <inheritdoc />
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

		/// <inheritdoc />
		IIndicesOperationResponse CloseIndex(ICloseIndexRequest closeIndexRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> CloseIndexAsync(Func<CloseIndexDescriptor, CloseIndexDescriptor> closeIndexSelector);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> CloseIndexAsync(ICloseIndexRequest closeIndexRequest);

		/// <summary>
		/// The refresh API allows to explicitly refresh one or more index, making all operations performed since the last refresh 
		/// available for search. The (near) real-time capabilities depend on the index engine used. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-refresh.html
		/// </summary>
		/// <param name="refreshSelector">A descriptor that describes the parameters for the refresh operation</param>
		IShardsOperationResponse Refresh(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null);

		/// <inheritdoc />
		IShardsOperationResponse Refresh(IRefreshRequest refreshRequest);

		/// <inheritdoc />
		Task<IShardsOperationResponse> RefreshAsync(Func<RefreshDescriptor, RefreshDescriptor> refreshSelector = null);

		/// <inheritdoc />
		Task<IShardsOperationResponse> RefreshAsync(IRefreshRequest refreshRequest);

		/// <summary>
		/// Provide low level segments information that a Lucene index (shard level) is built with. 
		/// Allows to be used to provide more information on the state of a shard and an index, possibly optimization information,
		/// data "wasted" on deletes, and so on.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-segments.html
		/// </summary>
		/// <param name="segmentsSelector">A descriptor that describes the parameters for the segments operation</param>
		ISegmentsResponse Segments(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null);

		/// <inheritdoc />
		ISegmentsResponse Segments(ISegmentsRequest segmentsRequest);

		/// <inheritdoc />
		Task<ISegmentsResponse> SegmentsAsync(Func<SegmentsDescriptor, SegmentsDescriptor> segmentsSelector = null);

		/// <inheritdoc />
		Task<ISegmentsResponse> SegmentsAsync(ISegmentsRequest segmentsRequest);

		/// <summary>
		/// The cluster state API allows to get a comprehensive state information of the whole cluster.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html
		/// </summary>
		/// <param name="clusterStateSelector">A descriptor that describes the parameters for the cluster state operation</param>
		IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null);

		/// <inheritdoc />
		IClusterStateResponse ClusterState(IClusterStateRequest clusterStateRequest);

		/// <inheritdoc />
		Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null);

		/// <inheritdoc />
		Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest clusterStateRequest);

		/// <summary>
		/// Allows to put a warmup search request on a specific index (or indices), with the body composing of a regular 
		/// search request. Types can be provided as part of the URI if the search request is designed to be run only 
		/// against the specific types.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#warmer-adding
		/// </summary>
		/// <param name="name">The name for the warmer that you want to register</param>
		/// <param name="selector">A descriptor that further describes what the warmer should look like</param>
		IIndicesOperationResponse PutWarmer(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector);

		/// <inheritdoc />
		IIndicesOperationResponse PutWarmer(IPutWarmerRequest putWarmerRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> PutWarmerAsync(string name, Func<PutWarmerDescriptor, PutWarmerDescriptor> selector);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> PutWarmerAsync(IPutWarmerRequest putWarmerRequest);

		/// <summary>
		/// Getting a warmer for specific index (or alias, or several indices) based on its name. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#warmer-retrieving
		/// </summary>
		/// <param name="name">The name of the warmer to get</param>
		/// <param name="selector">An optional selector specifying additional parameters for the get warmer operation</param>
		IWarmerResponse GetWarmer(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null);

		/// <inheritdoc />
		IWarmerResponse GetWarmer(IGetWarmerRequest getWarmerRequest);

		/// <inheritdoc />
		Task<IWarmerResponse> GetWarmerAsync(string name, Func<GetWarmerDescriptor, GetWarmerDescriptor> selector = null);

		/// <inheritdoc />
		Task<IWarmerResponse> GetWarmerAsync(IGetWarmerRequest getWarmerRequest);

		/// <summary>
		/// Deletes a warmer
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-warmers.html#removing
		/// </summary>
		/// <param name="name">The name of the warmer to delete</param>
		/// <param name="selector">An optional selector specifying additional parameters for the delete warmer operation</param>
		IIndicesOperationResponse DeleteWarmer(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null);

		/// <inheritdoc />
		IIndicesOperationResponse DeleteWarmer(IDeleteWarmerRequest deleteWarmerRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> DeleteWarmerAsync(string name, Func<DeleteWarmerDescriptor, DeleteWarmerDescriptor> selector = null);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> DeleteWarmerAsync(IDeleteWarmerRequest deleteWarmerRequest);

		/// <summary>
		/// Gets an index template
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#getting
		/// </summary>
		/// <param name="name">The name of the template to get</param>
		/// <param name="getTemplateSelector">An optional selector specifying additional parameters for the get template operation</param>
		ITemplateResponse GetTemplate(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null);

		/// <inheritdoc />
		ITemplateResponse GetTemplate(IGetTemplateRequest getTemplateRequest);

		/// <inheritdoc />
		Task<ITemplateResponse> GetTemplateAsync(string name, Func<GetTemplateDescriptor, GetTemplateDescriptor> getTemplateSelector = null);

		/// <inheritdoc />
		Task<ITemplateResponse> GetTemplateAsync(IGetTemplateRequest getTemplateRequest);

		/// <summary>
		/// Index templates allow to define templates that will automatically be applied to new indices created. 
		/// <para>The templates include both settings and mappings, and a simple pattern template that controls if 
		/// the template will be applied to the index created. </para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html
		/// </summary>
		/// <param name="name">The name of the template to register</param>
		/// <param name="putTemplateSelector">An optional selector specifying additional parameters for the put template operation</param>
		IIndicesOperationResponse PutTemplate(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector);

		/// <inheritdoc />
		IIndicesOperationResponse PutTemplate(IPutTemplateRequest putTemplateRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> PutTemplateAsync(string name, Func<PutTemplateDescriptor, PutTemplateDescriptor> putTemplateSelector);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> PutTemplateAsync(IPutTemplateRequest putTemplateRequest);

		/// <summary>
		/// Deletes an index template
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-templates.html#delete
		/// </summary>
		/// <param name="name">The name of the template to delete</param>
		/// <param name="deleteTemplateSelector">An optional selector specifying additional parameters for the delete template operation</param>
		IIndicesOperationResponse DeleteTemplate(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null);

		/// <inheritdoc />
		IIndicesOperationResponse DeleteTemplate(IDeleteTemplateRequest deleteTemplateRequest);

		/// <inheritdoc />
		[Obsolete("Scheduled for removal in 2.0, this method name has a typo")]
		Task<IIndicesOperationResponse> DeleteTemplateAync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null);

		/// <inheritdoc />
		[Obsolete("Scheduled for removal in 2.0, this method name has a typo")]
		Task<IIndicesOperationResponse> DeleteTemplateAync(IDeleteTemplateRequest deleteTemplateRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> DeleteTemplateAsync(string name, Func<DeleteTemplateDescriptor, DeleteTemplateDescriptor> deleteTemplateSelector = null);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> DeleteTemplateAsync(IDeleteTemplateRequest deleteTemplateRequest);

		/// <summary>
		/// Unregister a percolator
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <param name="name">The name for the percolator</param>
		/// <param name="selector">An optional descriptor describing the unregister percolator operation further</param>
		IUnregisterPercolateResponse UnregisterPercolator<T>(string name, Func<UnregisterPercolatorDescriptor<T>, UnregisterPercolatorDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		IUnregisterPercolateResponse UnregisterPercolator(IUnregisterPercolatorRequest unregisterPercolatorRequest);

		/// <inheritdoc />
		Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync<T>(string name, Func<UnregisterPercolatorDescriptor<T>, UnregisterPercolatorDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		Task<IUnregisterPercolateResponse> UnregisterPercolatorAsync(IUnregisterPercolatorRequest unregisterPercolatorRequest);

		/// <summary>
		/// Register a percolator
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, will also be used to strongly type the query</typeparam>
		/// <param name="name">The name for the percolator</param>
		/// <param name="percolatorSelector">An optional descriptor describing the register percolator operation further</param>
		IRegisterPercolateResponse RegisterPercolator<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector)
			where T : class;

		/// <inheritdoc />
		IRegisterPercolateResponse RegisterPercolator(IRegisterPercolatorRequest registerPercolatorRequest);

		/// <inheritdoc />
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync<T>(string name, Func<RegisterPercolatorDescriptor<T>, RegisterPercolatorDescriptor<T>> percolatorSelector)
			where T : class;

		/// <inheritdoc />
		Task<IRegisterPercolateResponse> RegisterPercolatorAsync(IRegisterPercolatorRequest registerPercolatorRequest);

		/// <summary>
		/// Percolate a document
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, and of the object that is being percolated</typeparam>
		/// <param name="percolateSelector">An optional descriptor describing the percolate operation further</param>
		IPercolateResponse Percolate<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector)
			where T : class;

		/// <inheritdoc />
		IPercolateResponse Percolate<T>(IPercolateRequest<T> percolateRequest)
			where T : class;

		/// <inheritdoc />
		Task<IPercolateResponse> PercolateAsync<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> percolateSelector)
			where T : class;

		/// <inheritdoc />
		Task<IPercolateResponse> PercolateAsync<T>(IPercolateRequest<T> percolateRequest)
			where T : class;

		/// <summary>
		/// Percolate a document but only return the number of matches not the matches itself
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-percolate.html
		/// </summary>
		/// <typeparam name="T">The type to infer the index/type from, and of the object that is being percolated</typeparam>
		/// <param name="object">The object to percolator</param>
		/// <param name="percolateSelector">An optional descriptor describing the percolate operation further</param>
		[Obsolete("Scheduled to be removed in 2.0 please use the overload takes a func (descriptor=>descriptor)")]
		IPercolateCountResponse PercolateCount<T>(T @object, Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector = null)
			where T : class;

		IPercolateCountResponse PercolateCount<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector)
			where T : class;

		/// <inheritdoc />
		IPercolateCountResponse PercolateCount<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class;

		/// <inheritdoc />
		[Obsolete("Scheduled to be removed in 2.0 please use the overload takes a func (descriptor=>descriptor)")]
		Task<IPercolateCountResponse> PercolateCountAsync<T>(T @object, Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector = null)
			where T : class;

		Task<IPercolateCountResponse> PercolateCountAsync<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> percolateSelector = null)
			where T : class;

		/// <inheritdoc />
		Task<IPercolateCountResponse> PercolateCountAsync<T>(IPercolateCountRequest<T> percolateCountRequest)
			where T : class;

		/// <summary>
		/// The put mapping API allows to register specific mapping definition for a specific type.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-put-mapping.html
		/// </summary>
		/// <typeparam name="T">The type we want to map in elasticsearch</typeparam>
		/// <param name="mappingSelector">A descriptor to describe the mapping of our type</param>
		IIndicesResponse Map<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class;

		/// <inheritdoc />
		IIndicesResponse Map(IPutMappingRequest putMappingRequest);

		/// <inheritdoc />
		Task<IIndicesResponse> MapAsync<T>(Func<PutMappingDescriptor<T>, PutMappingDescriptor<T>> mappingSelector)
			where T : class;

		/// <inheritdoc />
		Task<IIndicesResponse> MapAsync(IPutMappingRequest putMappingRequest);

		/// <summary>
		/// The get mapping API allows to retrieve mapping definitions for an index or index/type.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-mapping.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get mapping operation</param>
		IGetMappingResponse GetMapping<T>(Func<GetMappingDescriptor<T>, GetMappingDescriptor<T>> selector = null) where T : class;

		/// <inheritdoc />
		IGetMappingResponse GetMapping(IGetMappingRequest getMappingRequest);

		/// <inheritdoc />
		Task<IGetMappingResponse> GetMappingAsync<T>(Func<GetMappingDescriptor<T>, GetMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		Task<IGetMappingResponse> GetMappingAsync(IGetMappingRequest getMappingRequest);

		/// <summary>
		/// Allow to delete a mapping (type) along with its data. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-mapping.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete mapping operation</param>
		IIndicesResponse DeleteMapping<T>(Func<DeleteMappingDescriptor<T>, DeleteMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		IIndicesResponse DeleteMapping(IDeleteMappingRequest deleteMappingRequest);

		/// <inheritdoc />
		Task<IIndicesResponse> DeleteMappingAsync<T>(Func<DeleteMappingDescriptor<T>, DeleteMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		Task<IIndicesResponse> DeleteMappingAsync(IDeleteMappingRequest deleteMappingRequest);

		/// <summary>
		/// The flush API allows to flush one or more indices through an API. The flush process of an index basically 
		/// frees memory from the index by flushing data to the index storage and clearing the internal transaction log. 
		/// By default, Elasticsearch uses memory heuristics in order to automatically trigger 
		/// flush operations as required in order to clear memory.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-flush.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the flush operation</param>
		IShardsOperationResponse Flush(Func<FlushDescriptor, FlushDescriptor> selector);

		/// <inheritdoc />
		IShardsOperationResponse Flush(IFlushRequest flushRequest);

		/// <inheritdoc />
		Task<IShardsOperationResponse> FlushAsync(Func<FlushDescriptor, FlushDescriptor> selector);

		/// <inheritdoc />
		Task<IShardsOperationResponse> FlushAsync(IFlushRequest flushRequest);

		/// <summary>
		/// The get settings API allows to retrieve settings of index/indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-get-settings.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the get index settings operation</param>
		IIndexSettingsResponse GetIndexSettings(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector);

		/// <inheritdoc />
		IIndexSettingsResponse GetIndexSettings(IGetIndexSettingsRequest getIndexSettingsRequest);

		/// <inheritdoc />
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(Func<GetIndexSettingsDescriptor, GetIndexSettingsDescriptor> selector);

		/// <inheritdoc />
		Task<IIndexSettingsResponse> GetIndexSettingsAsync(IGetIndexSettingsRequest getIndexSettingsRequest);

		/// <summary>
		/// The delete index API allows to delete an existing index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-delete-index.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the delete index operation</param>
		IIndicesResponse DeleteIndex(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector);

		/// <inheritdoc />
		IIndicesResponse DeleteIndex(IDeleteIndexRequest deleteIndexRequest);

		/// <inheritdoc />
		Task<IIndicesResponse> DeleteIndexAsync(Func<DeleteIndexDescriptor, DeleteIndexDescriptor> selector);

		/// <inheritdoc />
		Task<IIndicesResponse> DeleteIndexAsync(IDeleteIndexRequest deleteIndexRequest);

		/// <summary>
		/// The clear cache API allows to clear either all caches or specific cached associated with one ore more indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-clearcache.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the clear cache operation</param>
		IShardsOperationResponse ClearCache(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null);

		/// <inheritdoc />
		IShardsOperationResponse ClearCache(IClearCacheRequest clearCacheRequest);

		/// <inheritdoc />
		Task<IShardsOperationResponse> ClearCacheAsync(Func<ClearCacheDescriptor, ClearCacheDescriptor> selector = null);

		/// <inheritdoc />
		Task<IShardsOperationResponse> ClearCacheAsync(IClearCacheRequest clearCacheRequest);

		/// <summary>
		/// The create index API allows to instantiate an index. Elasticsearch provides support for multiple indices, 
		/// including executing operations across several indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-create-index.html
		/// </summary>
		/// <param name="createIndexSelector">A descriptor that describes the parameters for the create index operation</param>
		IIndicesOperationResponse CreateIndex(Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector);

		/// <inheritdoc />
		IIndicesOperationResponse CreateIndex(ICreateIndexRequest createIndexRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> CreateIndexAsync(Func<CreateIndexDescriptor, CreateIndexDescriptor> createIndexSelector);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> CreateIndexAsync(ICreateIndexRequest createIndexRequest);

		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		IRootInfoResponse RootNodeInfo(Func<InfoDescriptor, InfoDescriptor> selector = null);

		/// <inheritdoc />
		IRootInfoResponse RootNodeInfo(IInfoRequest infoRequest);

		/// <inheritdoc />
		Task<IRootInfoResponse> RootNodeInfoAsync(Func<InfoDescriptor, InfoDescriptor> selector = null);

		/// <inheritdoc />
		Task<IRootInfoResponse> RootNodeInfoAsync(IInfoRequest infoRequest);

		/// <summary>
		/// Indices level stats provide statistics on different operations happening on an index. The API provides statistics on 
		/// the index level scope (though most stats can also be retrieved using node level scope).
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-stats.html
		/// </summary>
		/// <param name="selector">Optionaly further describe the indices stats operation</param>
		IGlobalStatsResponse IndicesStats(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null);

		/// <inheritdoc />
		IGlobalStatsResponse IndicesStats(IIndicesStatsRequest indicesStatsRequest);

		/// <inheritdoc />
		Task<IGlobalStatsResponse> IndicesStatsAsync(Func<IndicesStatsDescriptor, IndicesStatsDescriptor> selector = null);

		/// <inheritdoc />
		Task<IGlobalStatsResponse> IndicesStatsAsync(IIndicesStatsRequest indicesStatsRequest);

		/// <summary>
		/// The cluster nodes info API allows to retrieve one or more (or all) of the cluster nodes information.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-info.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes info operation</param>
		INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null);

		/// <inheritdoc />
		INodeInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest);

		/// <inheritdoc />
		Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null);

		/// <inheritdoc />
		Task<INodeInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest);

		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-stats.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes stats operation</param>
		INodeStatsResponse NodesStats(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null);

		/// <inheritdoc />
		INodeStatsResponse NodesStats(INodesStatsRequest nodesStatsRequest);

		/// <inheritdoc />
		Task<INodeStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null);

		/// <inheritdoc />
		Task<INodeStatsResponse> NodesStatsAsync(INodesStatsRequest nodesStatsRequest);

		/// <summary>
		/// An API allowing to get the current hot threads on each node in the cluster.
		/// </summary>
		/// <param name="selector"></param>
		/// <returns>An optional descriptor to further describe the nodes hot threads operation</returns>
		INodesHotThreadsResponse NodesHotThreads(Func<NodesHotThreadsDescriptor, NodesHotThreadsDescriptor> selector = null);

		/// <inheritdoc />
		INodesHotThreadsResponse NodesHotThreads(INodesHotThreadsRequest nodesHotThreadsRequest);

		/// <inheritdoc />
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(Func<NodesHotThreadsDescriptor, NodesHotThreadsDescriptor> selector = null);

		/// <inheritdoc />
		Task<INodesHotThreadsResponse> NodesHotThreadsAsync(INodesHotThreadsRequest nodesHotThreadsRequest);

		/// <summary>
		/// Allows to shutdown one or more (or all) nodes in the cluster.
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-shutdown.html#cluster-nodes-shutdown
		/// </summary>
		/// <param name="nodesShutdownSelector">A descriptor that describes the nodes shutdown operation</param>
		INodesShutdownResponse NodesShutdown(Func<NodesShutdownDescriptor, NodesShutdownDescriptor> nodesShutdownSelector = null);

		/// <inheritdoc />
		Task<INodesShutdownResponse> NodesShutdownAsync(Func<NodesShutdownDescriptor, NodesShutdownDescriptor> nodesShutdownSelector = null);

		/// <inheritdoc />
		INodesShutdownResponse NodesShutdown(INodesShutdownRequest nodesShutdownRequest);

		/// <inheritdoc />
		Task<INodesShutdownResponse> NodesShutdownAsync(INodesShutdownRequest nodesShutdownRequest);

		/// <summary>
		/// Used to check if the index (indices) exists or not. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-exists.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the index exist operation</param>
		IExistsResponse IndexExists(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector);

		/// <inheritdoc />
		IExistsResponse IndexExists(IIndexExistsRequest indexExistsRequest);

		/// <inheritdoc />
		Task<IExistsResponse> IndexExistsAsync(Func<IndexExistsDescriptor, IndexExistsDescriptor> selector);

		/// <inheritdoc />
		Task<IExistsResponse> IndexExistsAsync(IIndexExistsRequest indexExistsRequest);

		/// <summary>
		/// The more like this (mlt) API allows to get documents that are "like" a specified document. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-more-like-this.html
		/// </summary>
		/// <typeparam name="T">Type used to infer the default index and typename and used to describe the search</typeparam>
		/// <param name="mltSelector">A descriptor that describes the more like this operation</param>
		ISearchResponse<T> MoreLikeThis<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<T> MoreLikeThis<T>(IMoreLikeThisRequest moreLikeThisRequest)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> MoreLikeThisAsync<T>(Func<MoreLikeThisDescriptor<T>, MoreLikeThisDescriptor<T>> mltSelector)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> MoreLikeThisAsync<T>(IMoreLikeThisRequest moreLikeThisRequest)
			where T : class;

		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html
		/// </summary>
		/// <param name="clusterHealthSelector">An optional descriptor to further describe the cluster health operation</param>
		IHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null);

		/// <inheritdoc />
		IHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest);

		/// <inheritdoc />
		Task<IHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null);

		/// <inheritdoc />
		Task<IHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest);

		/// <summary>
		/// allows to retrieve statistics from a cluster wide perspective. The API returns basic index metrics 
		/// (shard numbers, store size, memory usage) and information about the current nodes that form the 
		/// cluster (number, roles, os, jvm versions, memory usage, cpu and installed plugins).
		/// </summary>
		/// <param name="clusterStatsSelector">A descriptor that describes the cluster stats operation</param>
		IClusterStatsResponse ClusterStats(Func<ClusterStatsDescriptor, ClusterStatsDescriptor> clusterStatsSelector = null);

		/// <inheritdoc />
		Task<IClusterStatsResponse> ClusterStatsAsync(Func<ClusterStatsDescriptor, ClusterStatsDescriptor> clusterStatsSelector = null);

		/// <inheritdoc />
		IClusterStatsResponse ClusterStats(IClusterStatsRequest clusterStatsRequest);

		/// <inheritdoc />
		Task<IClusterStatsResponse> ClusterStatsAsync(IClusterStatsRequest clusterStatsRequest);

		/// <summary>
		/// Allows to explicitly execute a cluster reroute allocation command including specific commands. 
		/// For example, a shard can be moved from one node to another explicitly, an allocation can be canceled, 
		/// or an unassigned shard can be explicitly allocated on a specific node.
		/// </summary>
		IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, ClusterRerouteDescriptor> clusterRerouteSelector);

		/// <inheritdoc />
		Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, ClusterRerouteDescriptor> clusterRerouteSelector);

		/// <inheritdoc />
		IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest clusterRerouteRequest);

		/// <inheritdoc />
		Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest clusterRerouteRequest);

		/// <summary>
		/// Performs the analysis process on a text and return the tokens breakdown of the text.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-analyze.html
		/// </summary>
		/// <param name="analyzeSelector">A descriptor that describes the analyze operation</param>
		IAnalyzeResponse Analyze(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector);

		/// <inheritdoc />
		IAnalyzeResponse Analyze(IAnalyzeRequest analyzeRequest);

		/// <inheritdoc />
		Task<IAnalyzeResponse> AnalyzeAsync(Func<AnalyzeDescriptor, AnalyzeDescriptor> analyzeSelector);

		/// <inheritdoc />
		Task<IAnalyzeResponse> AnalyzeAsync(IAnalyzeRequest analyzeRequest);

		/// <summary>
		/// The search API allows to execute a search query and get back search hits that match the query.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-search.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="searchSelector">A descriptor that describes the parameters for the search operation</param>
		ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<T> Search<T>(ISearchRequest request)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<TResult> Search<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		ISearchResponse<TResult> Search<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="searchSelector">A descriptor that describes the parameters for the search operation</param>
		Task<ISearchResponse<T>> SearchAsync<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> SearchAsync<T>(ISearchRequest request)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchAsync<T, TResult>(ISearchRequest request)
			where T : class
			where TResult : class;

		/// <summary>
		/// The /_search/template endpoint allows to use the mustache language to pre render search 
		/// requests, before they are executed and fill existing templates with template parameters.
		/// </summary>
		/// <typeparam name="T">The type used to infer the index and typename as well describe the query strongly typed</typeparam>
		/// <param name="selector">A descriptor that describes the parameters for the search operation</param>
		/// <returns></returns>
		ISearchResponse<T> SearchTemplate<T>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<TResult> SearchTemplate<T, TResult>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		ISearchResponse<T> SearchTemplate<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc />
		ISearchResponse<TResult> SearchTemplate<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(Func<SearchTemplateDescriptor<T>, SearchTemplateDescriptor<T>> selector)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		Task<ISearchResponse<T>> SearchTemplateAsync<T>(ISearchTemplateRequest request)
			where T : class;

		/// <inheritdoc />
		Task<ISearchResponse<TResult>> SearchTemplateAsync<T, TResult>(ISearchTemplateRequest request)
			where T : class
			where TResult : class;

		/// <inheritdoc />
		IGetSearchTemplateResponse GetSearchTemplate(string name, Func<GetSearchTemplateDescriptor, GetSearchTemplateDescriptor> selector = null);

		/// <inheritdoc />
		IGetSearchTemplateResponse GetSearchTemplate(IGetSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(string name, Func<GetSearchTemplateDescriptor, GetSearchTemplateDescriptor> selector = null);

		/// <inheritdoc />
		Task<IGetSearchTemplateResponse> GetSearchTemplateAsync(IGetSearchTemplateRequest request);

		/// <inheritdoc />
		IPutSearchTemplateResponse PutSearchTemplate(string name, Func<PutSearchTemplateDescriptor, PutSearchTemplateDescriptor> selector = null);

		/// <inheritdoc />
		IPutSearchTemplateResponse PutSearchTemplate(IPutSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(string name, Func<PutSearchTemplateDescriptor, PutSearchTemplateDescriptor> selector = null);

		/// <inheritdoc />
		Task<IPutSearchTemplateResponse> PutSearchTemplateAsync(IPutSearchTemplateRequest request);

		/// <inheritdoc />
		IDeleteSearchTemplateResponse DeleteSearchTemplate(string name, Func<DeleteSearchTemplateDescriptor, DeleteSearchTemplateDescriptor> selector = null);

		/// <inheritdoc />
		IDeleteSearchTemplateResponse DeleteSearchTemplate(IDeleteSearchTemplateRequest request);

		/// <inheritdoc />
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(string name, Func<DeleteSearchTemplateDescriptor, DeleteSearchTemplateDescriptor> selector = null);

		/// <inheritdoc />
		Task<IDeleteSearchTemplateResponse> DeleteSearchTemplateAsync(IDeleteSearchTemplateRequest request);

		/// <summary>
		/// The multi search API allows to execute several search requests within the same API.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-multi-search.html
		/// </summary>
		/// <param name="multiSearchSelector">A descriptor that describes the search operations on the multi search api</param>
		IMultiSearchResponse MultiSearch(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector);

		/// <inheritdoc />
		IMultiSearchResponse MultiSearch(IMultiSearchRequest multiSearchRequest);

		/// <inheritdoc />
		Task<IMultiSearchResponse> MultiSearchAsync(Func<MultiSearchDescriptor, MultiSearchDescriptor> multiSearchSelector);

		/// <inheritdoc />
		Task<IMultiSearchResponse> MultiSearchAsync(IMultiSearchRequest multiSearchRequest);

		/// <summary>
		/// The count API allows to easily execute a query and get the number of matches for that query. 
		/// It can be executed across one or more indices and across one or more types. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-count.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="countSelector">An optional descriptor to further describe the count operation</param>
		ICountResponse Count<T>(Func<CountDescriptor<T>, CountDescriptor<T>> countSelector = null)
			where T : class;

		/// <inheritdoc />
		ICountResponse Count<T>(ICountRequest countRequest)
			where T : class;

		/// <inheritdoc />
		Task<ICountResponse> CountAsync<T>(Func<CountDescriptor<T>, CountDescriptor<T>> countSelector = null)
			where T : class;

		/// <inheritdoc />
		Task<ICountResponse> CountAsync<T>(ICountRequest countRequest)
			where T : class;

		/// <summary>
		/// The delete by query API allows to delete documents from one or more indices and one or more types based on a query.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-delete-by-query.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename as well as describe the strongly
		///  typed parts of the query</typeparam>
		/// <param name="deleteByQuerySelector">An optional descriptor to further describe the delete by query operation</param>
		IDeleteResponse DeleteByQuery<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector)
			where T : class;

		/// <inheritdoc />
		IDeleteResponse DeleteByQuery(IDeleteByQueryRequest deleteByQueryRequest);

		/// <inheritdoc />
		Task<IDeleteResponse> DeleteByQueryAsync<T>(Func<DeleteByQueryDescriptor<T>, DeleteByQueryDescriptor<T>> deleteByQuerySelector)
			where T : class;

		/// <inheritdoc />
		Task<IDeleteResponse> DeleteByQueryAsync(IDeleteByQueryRequest deleteByQueryRequest);

		/// <summary>
		/// The bulk API makes it possible to perform many index/delete operations in a single API call. 
		/// This can greatly increase the indexing speed.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-bulk.html
		/// </summary>
		/// <param name="bulkRequest">A descriptor the describe the index/create/delete operation for this bulk operation</param>
		IBulkResponse Bulk(IBulkRequest bulkRequest);

		/// <inheritdoc />
		IBulkResponse Bulk(Func<BulkDescriptor, BulkDescriptor> bulkSelector);

		/// <inheritdoc />
		Task<IBulkResponse> BulkAsync(IBulkRequest bulkRequest);

		/// <inheritdoc />
		Task<IBulkResponse> BulkAsync(Func<BulkDescriptor, BulkDescriptor> bulkSelector);


		/// <summary>
		/// The index API adds or updates a typed JSON document in a specific index, making it searchable. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-index_.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="object">The object to be indexed, Id will be inferred (Id property or IdProperty attribute on type)</param>
		/// <param name="indexSelector">Optionally furter describe the index operation i.e override type/index/id</param>
		IIndexResponse Index<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class;

		/// <inheritdoc />
		IIndexResponse Index<T>(IIndexRequest<T> indexRequest)
			where T : class;

		/// <inheritdoc />
		Task<IIndexResponse> IndexAsync<T>(T @object, Func<IndexDescriptor<T>, IndexDescriptor<T>> indexSelector = null)
			where T : class;

		/// <inheritdoc />
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

		/// <inheritdoc />
		IDeleteResponse Delete(IDeleteRequest deleteRequest);

		/// <inheritdoc />
		Task<IDeleteResponse> DeleteAsync<T>(Func<DeleteDescriptor<T>, DeleteDescriptor<T>> deleteSelector)
			where T : class;

		/// <inheritdoc />
		Task<IDeleteResponse> DeleteAsync(IDeleteRequest deleteRequest);

		/// <summary>
		/// Multi GET API allows to get multiple documents based on an index, type (optional) and id (and possibly routing). 
		/// The response includes a docs array with all the fetched documents, each element similar in structure to a document 
		/// provided by the get API.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-get.html
		/// </summary>
		/// <param name="multiGetSelector">A descriptor describing which documents should be fetched</param>
		IMultiGetResponse MultiGet(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector);

		/// <inheritdoc />
		IMultiGetResponse MultiGet(IMultiGetRequest multiGetRequest);

		/// <inheritdoc />
		Task<IMultiGetResponse> MultiGetAsync(Func<MultiGetDescriptor, MultiGetDescriptor> multiGetSelector);

		/// <inheritdoc />
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

		/// <inheritdoc />
		T Source<T>(ISourceRequest sourceRequest)
			where T : class;

		/// <inheritdoc />
		Task<T> SourceAsync<T>(Func<SourceDescriptor<T>, SourceDescriptor<T>> getSelector)
			where T : class;

		/// <inheritdoc />
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

		/// <inheritdoc />
		IGetResponse<T> Get<T>(IGetRequest getRequest)
			where T : class;

		/// <inheritdoc />
		Task<IGetResponse<T>> GetAsync<T>(Func<GetDescriptor<T>, GetDescriptor<T>> getSelector)
			where T : class;

		/// <inheritdoc />
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
		IIndicesOperationResponse Alias(Func<AliasDescriptor, AliasDescriptor> aliasSelector);

		/// <inheritdoc />
		IIndicesOperationResponse Alias(IAliasRequest aliasRequest);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> AliasAsync(Func<AliasDescriptor, AliasDescriptor> aliasSelector);

		/// <inheritdoc />
		Task<IIndicesOperationResponse> AliasAsync(IAliasRequest aliasRequest);

		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches 
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> Difference with GetAlias is that this call will also return indices without aliases set</para>
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="getAliasDescriptor">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasesResponse GetAlias(Func<GetAliasDescriptor, GetAliasDescriptor> getAliasDescriptor);

		/// <inheritdoc />
		IGetAliasesResponse GetAlias(IGetAliasRequest getAliasRequest);

		/// <inheritdoc />
		Task<IGetAliasesResponse> GetAliasAsync(Func<GetAliasDescriptor, GetAliasDescriptor> getAliasDescriptor);

		/// <inheritdoc />
		Task<IGetAliasesResponse> GetAliasAsync(IGetAliasRequest getAliasRequest);

		/// <summary>
		/// The get index alias api allows to filter by alias name and index name. This api redirects to the master and fetches 
		/// the requested index aliases, if available. This api only serialises the found index aliases.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-retrieving
		/// </summary>
		/// <param name="getAliasesDescriptor">A descriptor that describes which aliases/indexes we are interested int</param>
		IGetAliasesResponse GetAliases(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor);

		/// <inheritdoc />
		IGetAliasesResponse GetAliases(IGetAliasesRequest getAliasesRequest);

		/// <inheritdoc />
		Task<IGetAliasesResponse> GetAliasesAsync(Func<GetAliasesDescriptor, GetAliasesDescriptor> getAliasesDescriptor);

		/// <inheritdoc />
		Task<IGetAliasesResponse> GetAliasesAsync(IGetAliasesRequest getAliasesRequest);

		/// <summary>
		/// Add a single index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#alias-adding
		/// </summary>
		/// <param name="putAliasRequest">A descriptor that describes the put alias request</param>
		IPutAliasResponse PutAlias(IPutAliasRequest putAliasRequest);

		/// <inheritdoc />
		Task<IPutAliasResponse> PutAliasAsync(IPutAliasRequest putAliasRequest);

		/// <inheritdoc />
		IPutAliasResponse PutAlias(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor);

		/// <inheritdoc />
		Task<IPutAliasResponse> PutAliasAsync(Func<PutAliasDescriptor, PutAliasDescriptor> putAliasDescriptor);

		/// <summary>
		/// Delete an index alias
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-aliases.html#deleting
		/// </summary>
		/// <param name="deleteAliasRequest">A descriptor that describes the delete alias request</param>
		IDeleteAliasResponse DeleteAlias(IDeleteAliasRequest deleteAliasRequest);

		/// <inheritdoc />
		Task<IDeleteAliasResponse> DeleteAliasAsync(IDeleteAliasRequest deleteAliasRequest);

		/// <inheritdoc />
		IDeleteAliasResponse DeleteAlias<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor) where T : class;

		/// <inheritdoc />
		Task<IDeleteAliasResponse> DeleteAliasAsync<T>(Func<DeleteAliasDescriptor<T>, DeleteAliasDescriptor<T>> deleteAliasDescriptor) where T : class;

		/// <summary>
		/// The optimize API allows to optimize one or more indices through an API. The optimize process basically optimizes 
		/// the index for faster search operations (and relates to the number of segments a Lucene index holds within each shard).
		///  The optimize operation allows to reduce the number of segments by merging them.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-optimize.html
		/// </summary>
		/// <param name="optimizeSelector">An optional descriptor that further describes the optimize operation, i.e limit it to one index</param>
		IShardsOperationResponse Optimize(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null);

		/// <inheritdoc />
		IShardsOperationResponse Optimize(IOptimizeRequest optimizeRequest);

		/// <inheritdoc />
		Task<IShardsOperationResponse> OptimizeAsync(Func<OptimizeDescriptor, OptimizeDescriptor> optimizeSelector = null);

		/// <inheritdoc />
		Task<IShardsOperationResponse> OptimizeAsync(IOptimizeRequest optimizeRequest);

		/// <summary>
		/// The indices status API allows to get a comprehensive status information of one or more indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-status.html
		/// </summary>
		/// <param name="selector">An optional descriptor that further describes the status operation, i.e limiting it to certain indices</param>
		IStatusResponse Status(Func<IndicesStatusDescriptor, IndicesStatusDescriptor> selector = null);

		/// <inheritdoc />
		IStatusResponse Status(IIndicesStatusRequest statusRequest);

		/// <inheritdoc />
		Task<IStatusResponse> StatusAsync(Func<IndicesStatusDescriptor, IndicesStatusDescriptor> selector = null);

		/// <inheritdoc />
		Task<IStatusResponse> StatusAsync(IIndicesStatusRequest statusRequest);

		/// <summary>
		/// Returns information and statistics on terms in the fields of a particular document as stored in the index.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-termvectors.html
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="termVectorSelector"></param>
		ITermVectorResponse TermVector<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
			where T : class;

		/// <inheritdoc />
		ITermVectorResponse TermVector(ITermvectorRequest termvectorRequest);

		/// <inheritdoc />
		Task<ITermVectorResponse> TermVectorAsync<T>(Func<TermvectorDescriptor<T>, TermvectorDescriptor<T>> termVectorSelector)
			where T : class;

		/// <inheritdoc />
		Task<ITermVectorResponse> TermVectorAsync(ITermvectorRequest termvectorRequest);

		/// <summary>
		/// Multi termvectors API allows to get multiple termvectors based on an index, type and id.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-multi-termvectors.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="multiTermVectorsSelector">The descriptor describing the multi termvectors operation</param>
		IMultiTermVectorResponse MultiTermVectors<T>(Func<MultiTermVectorsDescriptor<T>, MultiTermVectorsDescriptor<T>> multiTermVectorsSelector)
			where T : class;

		/// <inheritdoc />
		IMultiTermVectorResponse MultiTermVectors(IMultiTermVectorsRequest multiTermVectorsRequest);

		/// <inheritdoc />
		Task<IMultiTermVectorResponse> MultiTermVectorsAsync<T>(Func<MultiTermVectorsDescriptor<T>, MultiTermVectorsDescriptor<T>> multiTermVectorsSelector)
			where T : class;

		/// <inheritdoc />
		Task<IMultiTermVectorResponse> MultiTermVectorsAsync(IMultiTermVectorsRequest multiTermVectorsRequest);

		/// <summary>
		/// The suggest feature suggests similar looking terms based on a provided text by using a suggester. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-suggesters.html
		/// </summary>
		/// <typeparam name="T">The type used to strongly type parts of the suggest operation</typeparam>
		/// <param name="selector">The suggesters to use this operation (can be multiple)</param>
		ISuggestResponse Suggest<T>(Func<SuggestDescriptor<T>, SuggestDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc />
		ISuggestResponse Suggest(ISuggestRequest suggestRequest);

		/// <inheritdoc />
		Task<ISuggestResponse> SuggestAsync<T>(Func<SuggestDescriptor<T>, SuggestDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc />
		Task<ISuggestResponse> SuggestAsync(ISuggestRequest suggestRequest);


		/// <summary>
		/// Deletes a registered scroll request on the cluster 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-request-scroll.html
		/// </summary>
		/// <param name="clearScrollSelector">Specify the scroll id as well as request specific configuration</param>
		IEmptyResponse ClearScroll(Func<ClearScrollDescriptor, ClearScrollDescriptor> clearScrollSelector);

		/// <inheritdoc />
		IEmptyResponse ClearScroll(IClearScrollRequest clearScrollRequest);

		/// <inheritdoc />
		Task<IEmptyResponse> ClearScrollAsync(Func<ClearScrollDescriptor, ClearScrollDescriptor> clearScrollSelector);

		/// <inheritdoc />
		Task<IEmptyResponse> ClearScrollAsync(IClearScrollRequest clearScrollRequest);

		/// <summary>
		/// Check if a document exists without returning its contents
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/docs-get.html
		/// </summary>
		/// <typeparam name="T">The type used to infer the default index and typename</typeparam>
		/// <param name="existsSelector">Describe what document we are looking for</param>
		IExistsResponse DocumentExists<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class;

		/// <inheritdoc />
		IExistsResponse DocumentExists(IDocumentExistsRequest documentExistsRequest);

		/// <inheritdoc />
		Task<IExistsResponse> DocumentExistsAsync<T>(Func<DocumentExistsDescriptor<T>, DocumentExistsDescriptor<T>> existsSelector)
			where T : class;

		/// <inheritdoc />
		Task<IExistsResponse> DocumentExistsAsync(IDocumentExistsRequest documentExistsRequest);

		/// <summary>
		/// Before any snapshot or restore operation can be performed a snapshot repository should be registered in Elasticsearch. 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repository">The name for the repository</param>
		/// <param name="selector">describe what the repository looks like</param>
		IAcknowledgedResponse CreateRepository(string repository, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector);

		/// <inheritdoc />
		IAcknowledgedResponse CreateRepository(ICreateRepositoryRequest createRepositoryRequest);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> CreateRepositoryAsync(string repository, Func<CreateRepositoryDescriptor, CreateRepositoryDescriptor> selector);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> CreateRepositoryAsync(ICreateRepositoryRequest createRepositoryRequest);

		/// <summary>
		/// Delete a repository, if you have ongoing restore operations be sure to delete the indices being restored into first.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_repositories
		/// </summary>
		/// <param name="repository">The name of the repository</param>
		/// <param name="selector">Optionaly provide the delete operation with more details</param>>
		IAcknowledgedResponse DeleteRepository(string repository, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null);

		/// <inheritdoc />
		IAcknowledgedResponse DeleteRepository(IDeleteRepositoryRequest deleteRepositoryRequest);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> DeleteRepositoryAsync(string repository, Func<DeleteRepositoryDescriptor, DeleteRepositoryDescriptor> selector = null);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> DeleteRepositoryAsync(IDeleteRepositoryRequest deleteRepositoryRequest);

		/// <summary>
		/// A repository can contain multiple snapshots of the same cluster. Snapshot are identified by unique names within the cluster.
		/// /// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The name of the repository we want to create a snapshot in</param>
		/// <param name="snapshotName">The name of the snapshot</param>
		/// <param name="selector">Optionally provide more details about the snapshot operation</param>
		ISnapshotResponse Snapshot(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null);

		/// <inheritdoc />
		ISnapshotResponse Snapshot(ISnapshotRequest snapshotRequest);

		/// <inheritdoc />
		Task<ISnapshotResponse> SnapshotAsync(string repository, string snapshotName, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null);

		/// <inheritdoc />
		Task<ISnapshotResponse> SnapshotAsync(ISnapshotRequest snapshotRequest);

		/// <inheritdoc />
		IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, Func<SnapshotDescriptor, SnapshotDescriptor> snapshotSelector = null);

		/// <inheritdoc />
		IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest snapshotRequest);

		/// <summary>
		/// Delete a snapshot
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshot we want to delete lives</param>
		/// <param name="snapshotName">The name of the snapshot that we want to delete</param>
		/// <param name="selector">Optionally further describe the delete snapshot operation</param>
		IAcknowledgedResponse DeleteSnapshot(string repository, string snapshotName, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null);

		/// <inheritdoc />
		IAcknowledgedResponse DeleteSnapshot(IDeleteSnapshotRequest deleteSnapshotRequest);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> DeleteSnapshotAsync(string repository, string snapshotName, Func<DeleteSnapshotDescriptor, DeleteSnapshotDescriptor> selector = null);

		/// <inheritdoc />
		Task<IAcknowledgedResponse> DeleteSnapshotAsync(IDeleteSnapshotRequest deleteSnapshotRequest);

		/// <summary>
		/// Gets information about one or more snapshots
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_snapshot
		/// </summary>
		/// <param name="repository">The repository name under which the snapshots live</param>
		/// <param name="snapshotName">The names of the snapshots we want information from (can be _all or wildcards)</param>
		/// <param name="selector">Optionally further describe the get snapshot operation</param>
		IGetSnapshotResponse GetSnapshot(string repository, string snapshotName, Func<GetSnapshotDescriptor, GetSnapshotDescriptor> selector = null);

		/// <inheritdoc />
		IGetSnapshotResponse GetSnapshot(IGetSnapshotRequest getSnapshotRequest);

		/// <inheritdoc />
		Task<IGetSnapshotResponse> GetSnapshotAsync(string repository, string snapshotName, Func<GetSnapshotDescriptor, GetSnapshotDescriptor> selector = null);

		/// <inheritdoc />
		Task<IGetSnapshotResponse> GetSnapshotAsync(IGetSnapshotRequest getSnapshotRequest);

		/// <summary>
		/// Restore a snapshot
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/modules-snapshots.html#_restore
		/// </summary>
		/// <param name="repository">The repository name that holds our snapshot</param>
		/// <param name="snapshotName">The name of the snapshot that we want to restore</param>
		/// <param name="selector">Optionally further describe the restore operation</param>
		IRestoreResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null);

		/// <inheritdoc />
		IRestoreResponse Restore(IRestoreRequest restoreRequest);

		/// <inheritdoc />
		Task<IRestoreResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null);

		/// <inheritdoc />
		Task<IRestoreResponse> RestoreAsync(IRestoreRequest restoreRequest);

		/// <inheritdoc />
		IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, Func<RestoreDescriptor, RestoreDescriptor> selector = null);

		/// <inheritdoc />
		IObservable<IRecoveryStatusResponse> RestoreObservable(TimeSpan interval, IRestoreRequest restoreRequest);

		/// <summary>
		/// Allows to update cluster wide specific settings. Settings updated can either be persistent 
		/// (applied cross restarts) or transient (will not survive a full cluster restart). 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterPutSettingsResponse ClusterSettings(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> clusterHealthSelector);

		/// <inheritdoc />
		Task<IClusterPutSettingsResponse> ClusterSettingsAsync(Func<ClusterSettingsDescriptor, ClusterSettingsDescriptor> clusterHealthSelector);

		/// <inheritdoc />
		IClusterPutSettingsResponse ClusterSettings(IClusterSettingsRequest clusterSettingsRequest);

		/// <inheritdoc />
		Task<IClusterPutSettingsResponse> ClusterSettingsAsync(IClusterSettingsRequest clusterSettingsRequest);

		/// <summary>
		/// Gets cluster wide specific settings. Settings updated can either be persistent 
		/// (applied cross restarts) or transient (will not survive a full cluster restart). 
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-update-settings.html
		/// </summary>
		IClusterGetSettingsResponse ClusterGetSettings(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector);

		/// <inheritdoc />
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(Func<ClusterGetSettingsDescriptor, ClusterGetSettingsDescriptor> selector);

		/// <inheritdoc />
		IClusterGetSettingsResponse ClusterGetSettings(IClusterGetSettingsRequest clusterSettingsRequest = null);

		/// <inheritdoc />
		Task<IClusterGetSettingsResponse> ClusterGetSettingsAsync(IClusterGetSettingsRequest clusterSettingsRequest = null);

		/// <summary>
		/// Returns a list of any cluster-level changes (e.g. create index, update mapping, allocate or fail shard) which have not yet been executed.
		/// </summary>
		IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, ClusterPendingTasksDescriptor> pendingTasksSelector = null);

		/// <inheritdoc />
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, ClusterPendingTasksDescriptor> pendingTasksSelector = null);

		/// <inheritdoc />
		IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest pendingTasksRequest);

		/// <inheritdoc />
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest pendingTasksRequest);

		/// <inheritdoc />
		IExistsResponse AliasExists(Func<AliasExistsDescriptor, AliasExistsDescriptor> selector);

		/// <inheritdoc />
		IExistsResponse AliasExists(IAliasExistsRequest AliasRequest);

		/// <inheritdoc />
		Task<IExistsResponse> AliasExistsAsync(Func<AliasExistsDescriptor, AliasExistsDescriptor> selector);

		/// <inheritdoc />
		Task<IExistsResponse> AliasExistsAsync(IAliasExistsRequest AliasRequest);

		/// <inheritdoc />
		IExistsResponse TypeExists(Func<TypeExistsDescriptor, TypeExistsDescriptor> selector);

		/// <inheritdoc />
		IExistsResponse TypeExists(ITypeExistsRequest TypeRequest);

		/// <inheritdoc />
		Task<IExistsResponse> TypeExistsAsync(Func<TypeExistsDescriptor, TypeExistsDescriptor> selector);

		/// <inheritdoc />
		Task<IExistsResponse> TypeExistsAsync(ITypeExistsRequest TypeRequest);

		/// <inheritdoc />
		IExplainResponse<T> Explain<T>(Func<ExplainDescriptor<T>, ExplainDescriptor<T>> querySelector)
			where T : class;

		/// <inheritdoc />
		IExplainResponse<T> Explain<T>(IExplainRequest explainRequest)
			where T : class;

		/// <inheritdoc />
		Task<IExplainResponse<T>> ExplainAsync<T>(Func<ExplainDescriptor<T>, ExplainDescriptor<T>> querySelector)
			where T : class;

		/// <inheritdoc />
		Task<IExplainResponse<T>> ExplainAsync<T>(IExplainRequest explainRequest)
			where T : class;

		/// <inheritdoc />
		IMultiPercolateResponse MultiPercolate(Func<MultiPercolateDescriptor, MultiPercolateDescriptor> multiPercolateSelector);

		/// <inheritdoc />
		IMultiPercolateResponse MultiPercolate(IMultiPercolateRequest multiRequest);

		/// <inheritdoc />
		Task<IMultiPercolateResponse> MultiPercolateAsync(Func<MultiPercolateDescriptor, MultiPercolateDescriptor> multiPercolateSelector);

		/// <inheritdoc />
		Task<IMultiPercolateResponse> MultiPercolateAsync(IMultiPercolateRequest multiPercolateRequest);

		/// <inheritdoc />
		IGetFieldMappingResponse GetFieldMapping<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		IGetFieldMappingResponse GetFieldMapping(IGetFieldMappingRequest getFieldMappingRequest);

		/// <inheritdoc />
		Task<IGetFieldMappingResponse> GetFieldMappingAsync<T>(Func<GetFieldMappingDescriptor<T>, GetFieldMappingDescriptor<T>> selector = null)
			where T : class;

		/// <inheritdoc />
		Task<IGetFieldMappingResponse> GetFieldMappingAsync(IGetFieldMappingRequest getFieldMappingRequest);

		/// <inheritdoc />
		IExistsResponse TemplateExists(Func<TemplateExistsDescriptor, TemplateExistsDescriptor> selector);

		/// <inheritdoc />
		IExistsResponse TemplateExists(ITemplateExistsRequest templateRequest);

		/// <inheritdoc />
		Task<IExistsResponse> TemplateExistsAsync(Func<TemplateExistsDescriptor, TemplateExistsDescriptor> selector);

		/// <inheritdoc />
		Task<IExistsResponse> TemplateExistsAsync(ITemplateExistsRequest templateRequest);

		/// <summary>
		/// Executes a HEAD request to the cluster to determine whether it's up or not.
		/// </summary>
		IPingResponse Ping(Func<PingDescriptor, PingDescriptor> pingSelector = null);

		/// <inheritdoc />
		Task<IPingResponse> PingAsync(Func<PingDescriptor, PingDescriptor> pingSelector = null);

		/// <inheritdoc />
		IPingResponse Ping(IPingRequest pingRequest);

		/// <inheritdoc />
		Task<IPingResponse> PingAsync(IPingRequest pingRequest);

		/// <inheritdoc />
		ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, SearchShardsDescriptor<T>> searchSelector) where T : class;

		ISearchShardsResponse SearchShards(ISearchShardsRequest request);

		/// <inheritdoc />
		Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, SearchShardsDescriptor<T>> searchSelector)
			where T : class;

		Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request);

		/// <inheritdoc />
		IGetRepositoryResponse GetRepository(Func<GetRepositoryDescriptor, GetRepositoryDescriptor> selector);

		/// <inheritdoc />
		IGetRepositoryResponse GetRepository(IGetRepositoryRequest request);

		/// <inheritdoc />
		Task<IGetRepositoryResponse> GetRepositoryAsync(Func<GetRepositoryDescriptor, GetRepositoryDescriptor> selector);

		/// <inheritdoc />
		Task<IGetRepositoryResponse> GetRepositoryAsync(IGetRepositoryRequest request);

		ISnapshotStatusResponse SnapshotStatus(Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor> selector = null);

		/// <inheritdoc />
		ISnapshotStatusResponse SnapshotStatus(ISnapshotStatusRequest getSnapshotRequest);

		/// <inheritdoc />
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor> selector = null);

		/// <inheritdoc />
		Task<ISnapshotStatusResponse> SnapshotStatusAsync(ISnapshotStatusRequest getSnapshotRequest);

		IRecoveryStatusResponse RecoveryStatus(Func<RecoveryStatusDescriptor, RecoveryStatusDescriptor> selector = null);

		/// <inheritdoc />
		IRecoveryStatusResponse RecoveryStatus(IRecoveryStatusRequest statusRequest);

		/// <inheritdoc />
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(Func<RecoveryStatusDescriptor, RecoveryStatusDescriptor> selector = null);

		/// <inheritdoc />
		Task<IRecoveryStatusResponse> RecoveryStatusAsync(IRecoveryStatusRequest statusRequest);

		/// <summary>
		/// Perform any request you want over the configured IConnection synchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>An ElasticsearchResponse of T where T represents the JSON response body</returns>
		ElasticsearchResponse<T> DoRequest<T>(string method, string path, object data = null, IRequestParameters requestParameters = null);

		/// <summary>
		/// Perform any request you want over the configured IConnection asynchronously while taking advantage of the cluster failover.
		/// </summary>
		/// <typeparam name="T">The type representing the response JSON</typeparam>
		/// <param name="method">the HTTP Method to use</param>
		/// <param name="path">The path of the the url that you would like to hit</param>
		/// <param name="data">The body of the request, string and byte[] are posted as is other types will be serialized to JSON</param>
		/// <param name="requestParameters">Optionally configure request specific timeouts, headers</param>
		/// <returns>A task of ElasticsearchResponse of T where T represents the JSON response body</returns>
		Task<ElasticsearchResponse<T>> DoRequestAsync<T>(string method, string path, object data = null, IRequestParameters requestParameters = null);

		/// <inheritdoc />
		IPutScriptResponse PutScript(Func<PutScriptDescriptor, PutScriptDescriptor> putScriptDescriptor);

		/// <inheritdoc />
		Task<IPutScriptResponse> PutScriptAsync(Func<PutScriptDescriptor, PutScriptDescriptor> putScriptDescriptor);

		/// <inheritdoc />
		IGetScriptResponse GetScript(Func<GetScriptDescriptor, GetScriptDescriptor> getScriptDescriptor);

		/// <inheritdoc />
		Task<IGetScriptResponse> GetScriptAsync(Func<GetScriptDescriptor, GetScriptDescriptor> getScriptDescriptor);

		/// <inheritdoc />
		IDeleteScriptResponse DeleteScript(Func<DeleteScriptDescriptor, DeleteScriptDescriptor> deleteScriptDescriptor);

		/// <inheritdoc />
		Task<IDeleteScriptResponse> DeleteScriptAsync(Func<DeleteScriptDescriptor, DeleteScriptDescriptor> deleteScriptDescriptor);

		/// <inheritdoc />
		IGetIndexResponse GetIndex(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector);

		/// <inheritdoc />
		IGetIndexResponse GetIndex(IGetIndexRequest createIndexRequest);

		/// <inheritdoc />
		Task<IGetIndexResponse> GetIndexAsync(Func<GetIndexDescriptor, GetIndexDescriptor> getIndexSelector);

		/// <inheritdoc />
		Task<IGetIndexResponse> GetIndexAsync(IGetIndexRequest createIndexRequest);

		/// <inheritdoc />
		IExistsResponse SearchExists<T>(Func<SearchExistsDescriptor<T>, SearchExistsDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc />
		IExistsResponse SearchExists(ISearchExistsRequest indexRequest);

		/// <inheritdoc />
		Task<IExistsResponse> SearchExistsAsync<T>(Func<SearchExistsDescriptor<T>, SearchExistsDescriptor<T>> selector)
			where T : class;

		/// <inheritdoc />
		Task<IExistsResponse> SearchExistsAsync(ISearchExistsRequest indexRequest);

		/// <inheritdoc />
		IVerifyRepositoryResponse VerifyRepository(string name, Func<VerifyRepositoryDescriptor, VerifyRepositoryDescriptor> selector = null);

		/// <inheritdoc />
		IVerifyRepositoryResponse VerifyRepository(IVerifyRepositoryRequest verifyRepositoryRequest);

		/// <inheritdoc />
		Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(string name, Func<VerifyRepositoryDescriptor, VerifyRepositoryDescriptor> selector = null);

		/// <inheritdoc />
		Task<IVerifyRepositoryResponse> VerifyRepositoryAsync(IVerifyRepositoryRequest verifyRepositoryRequest);

		/// <inheritdoc />
		IUpgradeResponse Upgrade(IUpgradeRequest upgradeRequest);

		/// <inheritdoc />
		IUpgradeResponse Upgrade(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null);

		/// <inheritdoc />
		Task<IUpgradeResponse> UpgradeAsync(IUpgradeRequest upgradeRequest);

		/// <inheritdoc />
		Task<IUpgradeResponse> UpgradeAsync(Func<UpgradeDescriptor, UpgradeDescriptor> upgradeDescriptor = null);

		/// <inheritdoc />
		IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest upgradeRequest);

		/// <inheritdoc />
		IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeDescriptor = null);

		/// <inheritdoc />
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest upgradeRequest);

		/// <inheritdoc />
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, UpgradeStatusDescriptor> upgradeDescriptor = null);

		/// <inheritdoc />
		ICatResponse<CatAliasesRecord> CatAliases(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null);

		ICatResponse<CatAliasesRecord> CatAliases(ICatAliasesRequest request);
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(Func<CatAliasesDescriptor, CatAliasesDescriptor> selector = null);
		Task<ICatResponse<CatAliasesRecord>> CatAliasesAsync(ICatAliasesRequest request);

		/// <inheritdoc />
		ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null);

		ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request);
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(Func<CatAllocationDescriptor, CatAllocationDescriptor> selector = null);
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request);

		/// <inheritdoc />
		ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, CatCountDescriptor> selector = null);

		ICatResponse<CatCountRecord> CatCount(ICatCountRequest request);
		Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, CatCountDescriptor> selector = null);
		Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request);

		/// <inheritdoc />
		ICatResponse<CatHealthRecord> CatHealth(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null);

		ICatResponse<CatHealthRecord> CatHealth(ICatHealthRequest request);
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(Func<CatHealthDescriptor, CatHealthDescriptor> selector = null);
		Task<ICatResponse<CatHealthRecord>> CatHealthAsync(ICatHealthRequest request);

		/// <inheritdoc />
		ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null);

		ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request);
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(Func<CatIndicesDescriptor, CatIndicesDescriptor> selector = null);
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request);

		/// <inheritdoc />
		ICatResponse<CatMasterRecord> CatMaster(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null);

		ICatResponse<CatMasterRecord> CatMaster(ICatMasterRequest request);
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(Func<CatMasterDescriptor, CatMasterDescriptor> selector = null);
		Task<ICatResponse<CatMasterRecord>> CatMasterAsync(ICatMasterRequest request);

		/// <inheritdoc />
		ICatResponse<CatNodesRecord> CatNodes(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null);

		ICatResponse<CatNodesRecord> CatNodes(ICatNodesRequest request);
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(Func<CatNodesDescriptor, CatNodesDescriptor> selector = null);
		Task<ICatResponse<CatNodesRecord>> CatNodesAsync(ICatNodesRequest request);

		/// <inheritdoc />
		ICatResponse<CatPendingTasksRecord> CatPendingTasks(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null);

		ICatResponse<CatPendingTasksRecord> CatPendingTasks(ICatPendingTasksRequest request);
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(Func<CatPendingTasksDescriptor, CatPendingTasksDescriptor> selector = null);
		Task<ICatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(ICatPendingTasksRequest request);

		/// <inheritdoc />
		ICatResponse<CatPluginsRecord> CatPlugins(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null);

		ICatResponse<CatPluginsRecord> CatPlugins(ICatPluginsRequest request);
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(Func<CatPluginsDescriptor, CatPluginsDescriptor> selector = null);
		Task<ICatResponse<CatPluginsRecord>> CatPluginsAsync(ICatPluginsRequest request);

		/// <inheritdoc />
		ICatResponse<CatRecoveryRecord> CatRecovery(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null);

		ICatResponse<CatRecoveryRecord> CatRecovery(ICatRecoveryRequest request);
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(Func<CatRecoveryDescriptor, CatRecoveryDescriptor> selector = null);
		Task<ICatResponse<CatRecoveryRecord>> CatRecoveryAsync(ICatRecoveryRequest request);

		/// <inheritdoc />
		ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null);

		ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request);
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(Func<CatThreadPoolDescriptor, CatThreadPoolDescriptor> selector = null);
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request);

		/// <inheritdoc />
		ICatResponse<CatShardsRecord> CatShards(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null);

		ICatResponse<CatShardsRecord> CatShards(ICatShardsRequest request);
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(Func<CatShardsDescriptor, CatShardsDescriptor> selector = null);
		Task<ICatResponse<CatShardsRecord>> CatShardsAsync(ICatShardsRequest request);

		/// <inheritdoc />
		ICatResponse<CatFielddataRecord> CatFielddata(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null);

		ICatResponse<CatFielddataRecord> CatFielddata(ICatFielddataRequest request);
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(Func<CatFielddataDescriptor, CatFielddataDescriptor> selector = null);
		Task<ICatResponse<CatFielddataRecord>> CatFielddataAsync(ICatFielddataRequest request);
	}
}
