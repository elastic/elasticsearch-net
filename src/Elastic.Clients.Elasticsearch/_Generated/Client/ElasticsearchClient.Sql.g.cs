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
namespace Elastic.Clients.Elasticsearch.Sql;
public sealed partial class SqlNamespace : NamespacedClientProxy
{
	internal SqlNamespace(ElasticsearchClient client) : base(client)
	{
	}

	public SqlClearCursorResponse ClearCursor(SqlClearCursorRequest request)
	{
		request.BeforeRequest();
		return DoRequest<SqlClearCursorRequest, SqlClearCursorResponse, SqlClearCursorRequestParameters>(request);
	}

	public Task<SqlClearCursorResponse> ClearCursorAsync(SqlClearCursorRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<SqlClearCursorRequest, SqlClearCursorResponse, SqlClearCursorRequestParameters>(request, cancellationToken);
	}

	public SqlClearCursorResponse ClearCursor()
	{
		var descriptor = new SqlClearCursorRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequest<SqlClearCursorRequestDescriptor, SqlClearCursorResponse, SqlClearCursorRequestParameters>(descriptor);
	}

	public SqlClearCursorResponse ClearCursor(Action<SqlClearCursorRequestDescriptor> configureRequest)
	{
		var descriptor = new SqlClearCursorRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlClearCursorRequestDescriptor, SqlClearCursorResponse, SqlClearCursorRequestParameters>(descriptor);
	}

	public Task<SqlClearCursorResponse> ClearCursorAsync(CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlClearCursorRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlClearCursorRequestDescriptor, SqlClearCursorResponse, SqlClearCursorRequestParameters>(descriptor);
	}

	public Task<SqlClearCursorResponse> ClearCursorAsync(Action<SqlClearCursorRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlClearCursorRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlClearCursorRequestDescriptor, SqlClearCursorResponse, SqlClearCursorRequestParameters>(descriptor);
	}

	public SqlDeleteAsyncResponse DeleteAsyncSearch(SqlDeleteAsyncRequest request)
	{
		request.BeforeRequest();
		return DoRequest<SqlDeleteAsyncRequest, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(request);
	}

	public Task<SqlDeleteAsyncResponse> DeleteAsyncSearchAsync(SqlDeleteAsyncRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<SqlDeleteAsyncRequest, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(request, cancellationToken);
	}

	public SqlDeleteAsyncResponse DeleteAsyncSearch(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new SqlDeleteAsyncRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<SqlDeleteAsyncRequestDescriptor, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(descriptor);
	}

	public SqlDeleteAsyncResponse DeleteAsyncSearch(Elastic.Clients.Elasticsearch.Id id, Action<SqlDeleteAsyncRequestDescriptor> configureRequest)
	{
		var descriptor = new SqlDeleteAsyncRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlDeleteAsyncRequestDescriptor, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(descriptor);
	}

	public SqlDeleteAsyncResponse DeleteAsyncSearch<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<SqlDeleteAsyncRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new SqlDeleteAsyncRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlDeleteAsyncRequestDescriptor<TDocument>, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(descriptor);
	}

	public Task<SqlDeleteAsyncResponse> DeleteAsyncSearchAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlDeleteAsyncRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlDeleteAsyncRequestDescriptor, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(descriptor);
	}

	public Task<SqlDeleteAsyncResponse> DeleteAsyncSearchAsync(Elastic.Clients.Elasticsearch.Id id, Action<SqlDeleteAsyncRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlDeleteAsyncRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlDeleteAsyncRequestDescriptor, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(descriptor);
	}

	public Task<SqlDeleteAsyncResponse> DeleteAsyncSearchAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<SqlDeleteAsyncRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlDeleteAsyncRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlDeleteAsyncRequestDescriptor<TDocument>, SqlDeleteAsyncResponse, SqlDeleteAsyncRequestParameters>(descriptor);
	}

	public SqlGetAsyncResponse GetAsyncSearch(SqlGetAsyncRequest request)
	{
		request.BeforeRequest();
		return DoRequest<SqlGetAsyncRequest, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(request);
	}

	public Task<SqlGetAsyncResponse> GetAsyncSearchAsync(SqlGetAsyncRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncRequest, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(request, cancellationToken);
	}

	public SqlGetAsyncResponse GetAsyncSearch(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new SqlGetAsyncRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<SqlGetAsyncRequestDescriptor, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(descriptor);
	}

	public SqlGetAsyncResponse GetAsyncSearch(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncRequestDescriptor> configureRequest)
	{
		var descriptor = new SqlGetAsyncRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlGetAsyncRequestDescriptor, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(descriptor);
	}

	public SqlGetAsyncResponse GetAsyncSearch<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new SqlGetAsyncRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlGetAsyncRequestDescriptor<TDocument>, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(descriptor);
	}

	public Task<SqlGetAsyncResponse> GetAsyncSearchAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlGetAsyncRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncRequestDescriptor, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(descriptor);
	}

	public Task<SqlGetAsyncResponse> GetAsyncSearchAsync(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlGetAsyncRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncRequestDescriptor, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(descriptor);
	}

	public Task<SqlGetAsyncResponse> GetAsyncSearchAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlGetAsyncRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncRequestDescriptor<TDocument>, SqlGetAsyncResponse, SqlGetAsyncRequestParameters>(descriptor);
	}

	public SqlGetAsyncStatusResponse GetAsyncSearchStatus(SqlGetAsyncStatusRequest request)
	{
		request.BeforeRequest();
		return DoRequest<SqlGetAsyncStatusRequest, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(request);
	}

	public Task<SqlGetAsyncStatusResponse> GetAsyncSearchStatusAsync(SqlGetAsyncStatusRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncStatusRequest, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(request, cancellationToken);
	}

	public SqlGetAsyncStatusResponse GetAsyncSearchStatus(Elastic.Clients.Elasticsearch.Id id)
	{
		var descriptor = new SqlGetAsyncStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequest<SqlGetAsyncStatusRequestDescriptor, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(descriptor);
	}

	public SqlGetAsyncStatusResponse GetAsyncSearchStatus(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncStatusRequestDescriptor> configureRequest)
	{
		var descriptor = new SqlGetAsyncStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlGetAsyncStatusRequestDescriptor, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(descriptor);
	}

	public SqlGetAsyncStatusResponse GetAsyncSearchStatus<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncStatusRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new SqlGetAsyncStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlGetAsyncStatusRequestDescriptor<TDocument>, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(descriptor);
	}

	public Task<SqlGetAsyncStatusResponse> GetAsyncSearchStatusAsync(Elastic.Clients.Elasticsearch.Id id, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlGetAsyncStatusRequestDescriptor(id);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncStatusRequestDescriptor, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(descriptor);
	}

	public Task<SqlGetAsyncStatusResponse> GetAsyncSearchStatusAsync(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncStatusRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlGetAsyncStatusRequestDescriptor(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncStatusRequestDescriptor, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(descriptor);
	}

	public Task<SqlGetAsyncStatusResponse> GetAsyncSearchStatusAsync<TDocument>(Elastic.Clients.Elasticsearch.Id id, Action<SqlGetAsyncStatusRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlGetAsyncStatusRequestDescriptor<TDocument>(id);
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlGetAsyncStatusRequestDescriptor<TDocument>, SqlGetAsyncStatusResponse, SqlGetAsyncStatusRequestParameters>(descriptor);
	}

	public SqlQueryResponse Query(SqlQueryRequest request)
	{
		request.BeforeRequest();
		return DoRequest<SqlQueryRequest, SqlQueryResponse, SqlQueryRequestParameters>(request);
	}

	public Task<SqlQueryResponse> QueryAsync(SqlQueryRequest request, CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<SqlQueryRequest, SqlQueryResponse, SqlQueryRequestParameters>(request, cancellationToken);
	}

	public SqlQueryResponse Query()
	{
		var descriptor = new SqlQueryRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequest<SqlQueryRequestDescriptor, SqlQueryResponse, SqlQueryRequestParameters>(descriptor);
	}

	public SqlQueryResponse Query(Action<SqlQueryRequestDescriptor> configureRequest)
	{
		var descriptor = new SqlQueryRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlQueryRequestDescriptor, SqlQueryResponse, SqlQueryRequestParameters>(descriptor);
	}

	public SqlQueryResponse Query<TDocument>(Action<SqlQueryRequestDescriptor<TDocument>> configureRequest)
	{
		var descriptor = new SqlQueryRequestDescriptor<TDocument>();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequest<SqlQueryRequestDescriptor<TDocument>, SqlQueryResponse, SqlQueryRequestParameters>(descriptor);
	}

	public Task<SqlQueryResponse> QueryAsync(CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlQueryRequestDescriptor();
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlQueryRequestDescriptor, SqlQueryResponse, SqlQueryRequestParameters>(descriptor);
	}

	public Task<SqlQueryResponse> QueryAsync(Action<SqlQueryRequestDescriptor> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlQueryRequestDescriptor();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlQueryRequestDescriptor, SqlQueryResponse, SqlQueryRequestParameters>(descriptor);
	}

	public Task<SqlQueryResponse> QueryAsync<TDocument>(Action<SqlQueryRequestDescriptor<TDocument>> configureRequest, CancellationToken cancellationToken = default)
	{
		var descriptor = new SqlQueryRequestDescriptor<TDocument>();
		configureRequest?.Invoke(descriptor);
		descriptor.BeforeRequest();
		return DoRequestAsync<SqlQueryRequestDescriptor<TDocument>, SqlQueryResponse, SqlQueryRequestParameters>(descriptor);
	}
}