// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch.Serverless.AsyncSearch;

public partial class AsyncSearchNamespacedClient : NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="AsyncSearchNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected AsyncSearchNamespacedClient() : base()
	{
	}

	internal AsyncSearchNamespacedClient(ElasticsearchClient client) : base(client)
	{
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync(DeleteAsyncSearchRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequest, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync<TDocument>(DeleteAsyncSearchRequestDescriptor<TDocument> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequestDescriptor<TDocument>, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteAsyncSearchRequestDescriptor<TDocument>(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequestDescriptor<TDocument>, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id id, Action<DeleteAsyncSearchRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteAsyncSearchRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequestDescriptor<TDocument>, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync(DeleteAsyncSearchRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequestDescriptor, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync(Elastic.Clients.Elasticsearch.Serverless.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteAsyncSearchRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequestDescriptor, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Delete an async search.
	/// </para>
	/// <para>
	/// If the asynchronous search is still running, it is cancelled.
	/// Otherwise, the saved search results are deleted.
	/// If the Elasticsearch security features are enabled, the deletion of a specific async search is restricted to: the authenticated user that submitted the original search request; users that have the <c>cancel_task</c> cluster privilege.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<DeleteAsyncSearchResponse> DeleteAsync(Elastic.Clients.Elasticsearch.Serverless.Id id, Action<DeleteAsyncSearchRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteAsyncSearchRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteAsyncSearchRequestDescriptor, DeleteAsyncSearchResponse, DeleteAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get async search results.
	/// </para>
	/// <para>
	/// Retrieve the results of a previously submitted asynchronous search request.
	/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetAsyncSearchResponse<TDocument>> GetAsync<TDocument>(GetAsyncSearchRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<GetAsyncSearchRequest, GetAsyncSearchResponse<TDocument>, GetAsyncSearchRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get async search results.
	/// </para>
	/// <para>
	/// Retrieve the results of a previously submitted asynchronous search request.
	/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetAsyncSearchResponse<TDocument>> GetAsync<TDocument>(GetAsyncSearchRequestDescriptor<TDocument> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<GetAsyncSearchRequestDescriptor<TDocument>, GetAsyncSearchResponse<TDocument>, GetAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get async search results.
	/// </para>
	/// <para>
	/// Retrieve the results of a previously submitted asynchronous search request.
	/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetAsyncSearchResponse<TDocument>> GetAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetAsyncSearchRequestDescriptor<TDocument>(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetAsyncSearchRequestDescriptor<TDocument>, GetAsyncSearchResponse<TDocument>, GetAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get async search results.
	/// </para>
	/// <para>
	/// Retrieve the results of a previously submitted asynchronous search request.
	/// If the Elasticsearch security features are enabled, access to the results of a specific async search is restricted to the user or API key that submitted it.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<GetAsyncSearchResponse<TDocument>> GetAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id id, Action<GetAsyncSearchRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetAsyncSearchRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetAsyncSearchRequestDescriptor<TDocument>, GetAsyncSearchResponse<TDocument>, GetAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync(AsyncSearchStatusRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequest, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync<TDocument>(AsyncSearchStatusRequestDescriptor<TDocument> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequestDescriptor<TDocument>, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new AsyncSearchStatusRequestDescriptor<TDocument>(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequestDescriptor<TDocument>, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Id id, Action<AsyncSearchStatusRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new AsyncSearchStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequestDescriptor<TDocument>, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync(AsyncSearchStatusRequestDescriptor descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequestDescriptor, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync(Elastic.Clients.Elasticsearch.Serverless.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new AsyncSearchStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequestDescriptor, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Get the async search status.
	/// </para>
	/// <para>
	/// Get the status of a previously submitted async search request given its identifier, without retrieving search results.
	/// If the Elasticsearch security features are enabled, use of this API is restricted to the <c>monitoring_user</c> role.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<AsyncSearchStatusResponse> StatusAsync(Elastic.Clients.Elasticsearch.Serverless.Id id, Action<AsyncSearchStatusRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new AsyncSearchStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<AsyncSearchStatusRequestDescriptor, AsyncSearchStatusResponse, AsyncSearchStatusRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Run an async search.
	/// </para>
	/// <para>
	/// When the primary sort of the results is an indexed field, shards get sorted based on minimum and maximum value that they hold for that field. Partial results become available following the sort criteria that was requested.
	/// </para>
	/// <para>
	/// Warning: Asynchronous search does not support scroll or search requests that include only the suggest section.
	/// </para>
	/// <para>
	/// By default, Elasticsearch does not allow you to store an async search response larger than 10Mb and an attempt to do this results in an error.
	/// The maximum allowed size for a stored async search response can be set by changing the <c>search.max_async_search_response_size</c> cluster level setting.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<SubmitAsyncSearchResponse<TDocument>> SubmitAsync<TDocument>(SubmitAsyncSearchRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<SubmitAsyncSearchRequest, SubmitAsyncSearchResponse<TDocument>, SubmitAsyncSearchRequestParameters>(request, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Run an async search.
	/// </para>
	/// <para>
	/// When the primary sort of the results is an indexed field, shards get sorted based on minimum and maximum value that they hold for that field. Partial results become available following the sort criteria that was requested.
	/// </para>
	/// <para>
	/// Warning: Asynchronous search does not support scroll or search requests that include only the suggest section.
	/// </para>
	/// <para>
	/// By default, Elasticsearch does not allow you to store an async search response larger than 10Mb and an attempt to do this results in an error.
	/// The maximum allowed size for a stored async search response can be set by changing the <c>search.max_async_search_response_size</c> cluster level setting.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<SubmitAsyncSearchResponse<TDocument>> SubmitAsync<TDocument>(SubmitAsyncSearchRequestDescriptor<TDocument> descriptor, CancellationToken cancellationToken = default)
	{
		descriptor.BeforeRequest();
		return DoRequestAsync<SubmitAsyncSearchRequestDescriptor<TDocument>, SubmitAsyncSearchResponse<TDocument>, SubmitAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Run an async search.
	/// </para>
	/// <para>
	/// When the primary sort of the results is an indexed field, shards get sorted based on minimum and maximum value that they hold for that field. Partial results become available following the sort criteria that was requested.
	/// </para>
	/// <para>
	/// Warning: Asynchronous search does not support scroll or search requests that include only the suggest section.
	/// </para>
	/// <para>
	/// By default, Elasticsearch does not allow you to store an async search response larger than 10Mb and an attempt to do this results in an error.
	/// The maximum allowed size for a stored async search response can be set by changing the <c>search.max_async_search_response_size</c> cluster level setting.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<SubmitAsyncSearchResponse<TDocument>> SubmitAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Indices? indices, CancellationToken cancellationToken = default)
	{
		var descriptor = new SubmitAsyncSearchRequestDescriptor<TDocument>(indices);
		descriptor.BeforeRequest();
		return DoRequestAsync<SubmitAsyncSearchRequestDescriptor<TDocument>, SubmitAsyncSearchResponse<TDocument>, SubmitAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Run an async search.
	/// </para>
	/// <para>
	/// When the primary sort of the results is an indexed field, shards get sorted based on minimum and maximum value that they hold for that field. Partial results become available following the sort criteria that was requested.
	/// </para>
	/// <para>
	/// Warning: Asynchronous search does not support scroll or search requests that include only the suggest section.
	/// </para>
	/// <para>
	/// By default, Elasticsearch does not allow you to store an async search response larger than 10Mb and an attempt to do this results in an error.
	/// The maximum allowed size for a stored async search response can be set by changing the <c>search.max_async_search_response_size</c> cluster level setting.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<SubmitAsyncSearchResponse<TDocument>> SubmitAsync<TDocument>(Elastic.Clients.Elasticsearch.Serverless.Indices? indices, Action<SubmitAsyncSearchRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SubmitAsyncSearchRequestDescriptor<TDocument>(indices);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SubmitAsyncSearchRequestDescriptor<TDocument>, SubmitAsyncSearchResponse<TDocument>, SubmitAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Run an async search.
	/// </para>
	/// <para>
	/// When the primary sort of the results is an indexed field, shards get sorted based on minimum and maximum value that they hold for that field. Partial results become available following the sort criteria that was requested.
	/// </para>
	/// <para>
	/// Warning: Asynchronous search does not support scroll or search requests that include only the suggest section.
	/// </para>
	/// <para>
	/// By default, Elasticsearch does not allow you to store an async search response larger than 10Mb and an attempt to do this results in an error.
	/// The maximum allowed size for a stored async search response can be set by changing the <c>search.max_async_search_response_size</c> cluster level setting.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<SubmitAsyncSearchResponse<TDocument>> SubmitAsync<TDocument>(CancellationToken cancellationToken = default)
	{
		var descriptor = new SubmitAsyncSearchRequestDescriptor<TDocument>();
		descriptor.BeforeRequest();
		return DoRequestAsync<SubmitAsyncSearchRequestDescriptor<TDocument>, SubmitAsyncSearchResponse<TDocument>, SubmitAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}

	/// <summary>
	/// <para>
	/// Run an async search.
	/// </para>
	/// <para>
	/// When the primary sort of the results is an indexed field, shards get sorted based on minimum and maximum value that they hold for that field. Partial results become available following the sort criteria that was requested.
	/// </para>
	/// <para>
	/// Warning: Asynchronous search does not support scroll or search requests that include only the suggest section.
	/// </para>
	/// <para>
	/// By default, Elasticsearch does not allow you to store an async search response larger than 10Mb and an attempt to do this results in an error.
	/// The maximum allowed size for a stored async search response can be set by changing the <c>search.max_async_search_response_size</c> cluster level setting.
	/// </para>
	/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.16/async-search.html">Learn more about this API in the Elasticsearch documentation.</see></para>
	/// </summary>
	public virtual Task<SubmitAsyncSearchResponse<TDocument>> SubmitAsync<TDocument>(Action<SubmitAsyncSearchRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SubmitAsyncSearchRequestDescriptor<TDocument>();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SubmitAsyncSearchRequestDescriptor<TDocument>, SubmitAsyncSearchResponse<TDocument>, SubmitAsyncSearchRequestParameters>(descriptor, cancellationToken);
	}
}