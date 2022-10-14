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
public class EqlNamespace : NamespacedClientProxy
{
	internal EqlNamespace(ElasticsearchClient client) : base(client)
	{
	}

	public DeleteEqlResponse Delete(DeleteEqlRequest request)
	{
		request.BeforeRequest();
		return DoRequest<DeleteEqlRequest, DeleteEqlResponse>(request);
	}

	public Task<DeleteEqlResponse> DeleteAsync(DeleteEqlRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<DeleteEqlRequest, DeleteEqlResponse>(request, cancellationToken);
	}

	public DeleteEqlResponse Delete(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new DeleteEqlRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<DeleteEqlRequestDescriptor, DeleteEqlResponse>(descriptor);
	}

	public DeleteEqlResponse Delete(Elastic.Clients.Elasticsearch.Id id, Action<DeleteEqlRequestDescriptor> configureRequest)
	{
		var descriptor = new DeleteEqlRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<DeleteEqlRequestDescriptor, DeleteEqlResponse>(descriptor);
	}

	public DeleteEqlResponse Delete<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<DeleteEqlRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new DeleteEqlRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<DeleteEqlRequestDescriptor<TDocument>, DeleteEqlResponse>(descriptor);
	}

	public Task<DeleteEqlResponse> DeleteAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteEqlRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteEqlRequestDescriptor, DeleteEqlResponse>(descriptor);
	}

	public Task<DeleteEqlResponse> DeleteAsync(Elastic.Clients.Elasticsearch.Id id, Action<DeleteEqlRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteEqlRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteEqlRequestDescriptor, DeleteEqlResponse>(descriptor);
	}

	public Task<DeleteEqlResponse> DeleteAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<DeleteEqlRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new DeleteEqlRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<DeleteEqlRequestDescriptor<TDocument>, DeleteEqlResponse>(descriptor);
	}

	public EqlGetStatusResponse GetStatus(EqlGetStatusRequest request)
	{
		request.BeforeRequest();
		return DoRequest<EqlGetStatusRequest, EqlGetStatusResponse>(request);
	}

	public Task<EqlGetStatusResponse> GetStatusAsync(EqlGetStatusRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequest, EqlGetStatusResponse>(request, cancellationToken);
	}

	public EqlGetStatusResponse GetStatus(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetStatusRequestDescriptor, EqlGetStatusResponse>(descriptor);
	}

	public EqlGetStatusResponse GetStatus(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor> configureRequest)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetStatusRequestDescriptor, EqlGetStatusResponse>(descriptor);
	}

	public EqlGetStatusResponse GetStatus<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new EqlGetStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlGetStatusRequestDescriptor<TDocument>, EqlGetStatusResponse>(descriptor);
	}

	public Task<EqlGetStatusResponse> GetStatusAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequestDescriptor, EqlGetStatusResponse>(descriptor);
	}

	public Task<EqlGetStatusResponse> GetStatusAsync(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequestDescriptor, EqlGetStatusResponse>(descriptor);
	}

	public Task<EqlGetStatusResponse> GetStatusAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<EqlGetStatusRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlGetStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlGetStatusRequestDescriptor<TDocument>, EqlGetStatusResponse>(descriptor);
	}

	public EqlSearchResponse<TEvent> Search<TEvent>(EqlSearchRequest request)
	{
		request.BeforeRequest();
		return DoRequest<EqlSearchRequest, EqlSearchResponse<TEvent>>(request);
	}

	public Task<EqlSearchResponse<TEvent>> SearchAsync<TEvent>(EqlSearchRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<EqlSearchRequest, EqlSearchResponse<TEvent>>(request, cancellationToken);
	}

	public EqlSearchResponse<TEvent> Search<TEvent>(Elastic.Clients.Elasticsearch.Indices indices)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		descriptor.BeforeRequest();
		return DoRequest<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>>(descriptor);
	}

	public EqlSearchResponse<TEvent> Search<TEvent>(Elastic.Clients.Elasticsearch.Indices indices, Action<EqlSearchRequestDescriptor> configureRequest)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>>(descriptor);
	}

	public Task<EqlSearchResponse<TEvent>> SearchAsync<TEvent>(Elastic.Clients.Elasticsearch.Indices indices, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>>(descriptor);
	}

	public Task<EqlSearchResponse<TEvent>> SearchAsync<TEvent>(Elastic.Clients.Elasticsearch.Indices indices, Action<EqlSearchRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new EqlSearchRequestDescriptor(indices);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<EqlSearchRequestDescriptor, EqlSearchResponse<TEvent>>(descriptor);
	}

	public GetEqlResponse<TEvent> Get<TEvent>(GetEqlRequest request)
	{
		request.BeforeRequest();
		return DoRequest<GetEqlRequest, GetEqlResponse<TEvent>>(request);
	}

	public Task<GetEqlResponse<TEvent>> GetAsync<TEvent>(GetEqlRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<GetEqlRequest, GetEqlResponse<TEvent>>(request, cancellationToken);
	}

	public GetEqlResponse<TEvent> Get<TEvent>(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new GetEqlRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<GetEqlRequestDescriptor, GetEqlResponse<TEvent>>(descriptor);
	}

	public GetEqlResponse<TEvent> Get<TEvent>(Elastic.Clients.Elasticsearch.Id id, Action<GetEqlRequestDescriptor> configureRequest)
	{
		var descriptor = new GetEqlRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<GetEqlRequestDescriptor, GetEqlResponse<TEvent>>(descriptor);
	}

	public Task<GetEqlResponse<TEvent>> GetAsync<TEvent>(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetEqlRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetEqlRequestDescriptor, GetEqlResponse<TEvent>>(descriptor);
	}

	public Task<GetEqlResponse<TEvent>> GetAsync<TEvent>(Elastic.Clients.Elasticsearch.Id id, Action<GetEqlRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new GetEqlRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<GetEqlRequestDescriptor, GetEqlResponse<TEvent>>(descriptor);
	}
}