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

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Eql;
public partial class EqlNamespacedClient : NamespacedClientProxy
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EqlNamespacedClient"/> class for mocking.
	/// </summary>			
	protected EqlNamespacedClient() : base()
	{
	}

	internal EqlNamespacedClient(ElasticsearchClient client) : base(client)
	{
	}

	public virtual EqlDeleteResponse Delete(EqlDeleteRequest request)
	{
		request.BeforeRequest();
		return DoRequest<EqlDeleteRequest, EqlDeleteResponse, EqlDeleteRequestParameters>(request);
	}

	public virtual Task<EqlDeleteResponse> DeleteAsync(EqlDeleteRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<EqlDeleteRequest, EqlDeleteResponse, EqlDeleteRequestParameters>(request, cancellationToken);
	}

	public virtual EqlDeleteResponse Delete(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new EqlDeleteRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<EqlDeleteRequestDescriptor, EqlDeleteResponse, EqlDeleteRequestParameters>(descriptor);
	}

	public virtual EqlDeleteResponse Delete(Elastic.Clients.Elasticsearch.Id id, Action<EqlDeleteRequestDescriptor> configureRequest)
	{
		var descriptor = new EqlDeleteRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlDeleteRequestDescriptor, EqlDeleteResponse, EqlDeleteRequestParameters>(descriptor);
	}

	public virtual EqlDeleteResponse Delete<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<EqlDeleteRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new EqlDeleteRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlDeleteRequestDescriptor<TDocument>, EqlDeleteResponse, EqlDeleteRequestParameters>(descriptor);
	}

	public virtual Task<EqlDeleteResponse> DeleteAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlDeleteRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlDeleteRequestDescriptor, EqlDeleteResponse, EqlDeleteRequestParameters>(descriptor);
	}

	public virtual Task<EqlDeleteResponse> DeleteAsync(Elastic.Clients.Elasticsearch.Id id, Action<EqlDeleteRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlDeleteRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlDeleteRequestDescriptor, EqlDeleteResponse, EqlDeleteRequestParameters>(descriptor);
	}

	public virtual Task<EqlDeleteResponse> DeleteAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<EqlDeleteRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlDeleteRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlDeleteRequestDescriptor<TDocument>, EqlDeleteResponse, EqlDeleteRequestParameters>(descriptor);
	}

	public virtual EqlGetResponse<TEvent> Get<TEvent>(EqlGetRequest request)
	{
		request.BeforeRequest();
		return DoRequest<EqlGetRequest, EqlGetResponse<TEvent>, EqlGetRequestParameters>(request);
	}

	public virtual Task<EqlGetResponse<TEvent>> GetAsync<TEvent>(EqlGetRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<EqlGetRequest, EqlGetResponse<TEvent>, EqlGetRequestParameters>(request, cancellationToken);
	}

	public virtual EqlGetResponse<TEvent> Get<TEvent>(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new EqlGetRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetRequestDescriptor, EqlGetResponse<TEvent>, EqlGetRequestParameters>(descriptor);
	}

	public virtual EqlGetResponse<TEvent> Get<TEvent>(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetRequestDescriptor> configureRequest)
	{
		var descriptor = new EqlGetRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetRequestDescriptor, EqlGetResponse<TEvent>, EqlGetRequestParameters>(descriptor);
	}

	public virtual Task<EqlGetResponse<TEvent>> GetAsync<TEvent>(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetRequestDescriptor, EqlGetResponse<TEvent>, EqlGetRequestParameters>(descriptor);
	}

	public virtual Task<EqlGetResponse<TEvent>> GetAsync<TEvent>(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetRequestDescriptor, EqlGetResponse<TEvent>, EqlGetRequestParameters>(descriptor);
	}

	public virtual EqlSearchResponse<TEvent> Search<TEvent>(EqlSearchRequest request)
	{
		request.BeforeRequest();
		return DoRequest<EqlSearchRequest, EqlSearchResponse<TEvent>, EqlSearchRequestParameters>(request);
	}

	public virtual Task<EqlSearchResponse<TEvent>> SearchAsync<TEvent>(EqlSearchRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<EqlSearchRequest, EqlSearchResponse<TEvent>, EqlSearchRequestParameters>(request, cancellationToken);
	}

	public virtual EqlSearchResponse<TEvent> Search<TEvent>(Elastic.Clients.Elasticsearch.Indices indices)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		descriptor.BeforeRequest();
		return DoRequest<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>, EqlSearchRequestParameters>(descriptor);
	}

	public virtual EqlSearchResponse<TEvent> Search<TEvent>(Elastic.Clients.Elasticsearch.Indices indices, Action<EqlSearchRequestDescriptor> configureRequest)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>, EqlSearchRequestParameters>(descriptor);
	}

	public virtual Task<EqlSearchResponse<TEvent>> SearchAsync<TEvent>(Elastic.Clients.Elasticsearch.Indices indices, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>, EqlSearchRequestParameters>(descriptor);
	}

	public virtual Task<EqlSearchResponse<TEvent>> SearchAsync<TEvent>(Elastic.Clients.Elasticsearch.Indices indices, Action<EqlSearchRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>, EqlSearchRequestParameters>(descriptor);
	}

	public virtual EqlGetStatusResponse GetStatus(EqlGetStatusRequest request)
	{
		request.BeforeRequest();
		return DoRequest<EqlGetStatusRequest, EqlGetStatusResponse, EqlGetStatusRequestParameters>(request);
	}

	public virtual Task<EqlGetStatusResponse> GetStatusAsync(EqlGetStatusRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequest, EqlGetStatusResponse, EqlGetStatusRequestParameters>(request, cancellationToken);
	}

	public virtual EqlGetStatusResponse GetStatus(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetStatusRequestDescriptor, EqlGetStatusResponse, EqlGetStatusRequestParameters>(descriptor);
	}

	public virtual EqlGetStatusResponse GetStatus(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor> configureRequest)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetStatusRequestDescriptor, EqlGetStatusResponse, EqlGetStatusRequestParameters>(descriptor);
	}

	public virtual EqlGetStatusResponse GetStatus<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new EqlGetStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetStatusRequestDescriptor<TDocument>, EqlGetStatusResponse, EqlGetStatusRequestParameters>(descriptor);
	}

	public virtual Task<EqlGetStatusResponse> GetStatusAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequestDescriptor, EqlGetStatusResponse, EqlGetStatusRequestParameters>(descriptor);
	}

	public virtual Task<EqlGetStatusResponse> GetStatusAsync(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequestDescriptor, EqlGetStatusResponse, EqlGetStatusRequestParameters>(descriptor);
	}

	public virtual Task<EqlGetStatusResponse> GetStatusAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequestDescriptor<TDocument>, EqlGetStatusResponse, EqlGetStatusRequestParameters>(descriptor);
	}
}