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
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

public partial class SqlNamespacedClient : Elastic.Clients.Elasticsearch.NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="Elastic.Clients.Elasticsearch.Sql.SqlNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected SqlNamespacedClient() : base()
	{
	}

	internal SqlNamespacedClient(Elastic.Clients.Elasticsearch.ElasticsearchClient client) : base(client)
	{
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse ClearCursor(Elastic.Clients.Elasticsearch.Sql.ClearCursorRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.ClearCursorRequest, Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse, Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse ClearCursor(System.Action<Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.ClearCursorRequest, Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse, Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse> ClearCursorAsync(Elastic.Clients.Elasticsearch.Sql.ClearCursorRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.ClearCursorRequest, Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse, Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse> ClearCursorAsync(System.Action<Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.ClearCursorRequest, Elastic.Clients.Elasticsearch.Sql.ClearCursorResponse, Elastic.Clients.Elasticsearch.Sql.ClearCursorRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse DeleteAsync(Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse DeleteAsync(Elastic.Clients.Elasticsearch.Id id)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestDescriptor(id);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse DeleteAsync(Elastic.Clients.Elasticsearch.Id id, System.Action<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestDescriptor(id);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse> DeleteAsyncAsync(Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse> DeleteAsyncAsync(Elastic.Clients.Elasticsearch.Id id, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestDescriptor(id);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse> DeleteAsyncAsync(Elastic.Clients.Elasticsearch.Id id, System.Action<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestDescriptor(id);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequest, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncResponse, Elastic.Clients.Elasticsearch.Sql.DeleteAsyncRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse GetAsync(Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse GetAsync(Elastic.Clients.Elasticsearch.Id id)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestDescriptor(id);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse GetAsync(Elastic.Clients.Elasticsearch.Id id, System.Action<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestDescriptor(id);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse> GetAsyncAsync(Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse> GetAsyncAsync(Elastic.Clients.Elasticsearch.Id id, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestDescriptor(id);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse> GetAsyncAsync(Elastic.Clients.Elasticsearch.Id id, System.Action<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestDescriptor(id);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.GetAsyncRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse GetAsyncStatus(Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse GetAsyncStatus(Elastic.Clients.Elasticsearch.Id id)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(id);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse GetAsyncStatus(Elastic.Clients.Elasticsearch.Id id, System.Action<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(id);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse> GetAsyncStatusAsync(Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse> GetAsyncStatusAsync(Elastic.Clients.Elasticsearch.Id id, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(id);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse> GetAsyncStatusAsync(Elastic.Clients.Elasticsearch.Id id, System.Action<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestDescriptor(id);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequest, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusResponse, Elastic.Clients.Elasticsearch.Sql.GetAsyncStatusRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.QueryResponse Query(Elastic.Clients.Elasticsearch.Sql.QueryRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.QueryResponse Query()
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.QueryResponse Query(System.Action<Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.QueryResponse Query<TDocument>()
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor<TDocument>();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.QueryResponse Query<TDocument>(System.Action<Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.QueryResponse> QueryAsync(Elastic.Clients.Elasticsearch.Sql.QueryRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.QueryResponse> QueryAsync(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.QueryResponse> QueryAsync(System.Action<Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.QueryResponse> QueryAsync<TDocument>(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor<TDocument>();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.QueryResponse> QueryAsync<TDocument>(System.Action<Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor<TDocument>> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.QueryRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.QueryRequest, Elastic.Clients.Elasticsearch.Sql.QueryResponse, Elastic.Clients.Elasticsearch.Sql.QueryRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.TranslateResponse Translate(Elastic.Clients.Elasticsearch.Sql.TranslateRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.TranslateRequest, Elastic.Clients.Elasticsearch.Sql.TranslateResponse, Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.TranslateResponse Translate(System.Action<Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.TranslateRequest, Elastic.Clients.Elasticsearch.Sql.TranslateResponse, Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Sql.TranslateResponse Translate<TDocument>(System.Action<Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Sql.TranslateRequest, Elastic.Clients.Elasticsearch.Sql.TranslateResponse, Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.TranslateResponse> TranslateAsync(Elastic.Clients.Elasticsearch.Sql.TranslateRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.TranslateRequest, Elastic.Clients.Elasticsearch.Sql.TranslateResponse, Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.TranslateResponse> TranslateAsync(System.Action<Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.TranslateRequest, Elastic.Clients.Elasticsearch.Sql.TranslateResponse, Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Sql.TranslateResponse> TranslateAsync<TDocument>(System.Action<Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Sql.TranslateRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Sql.TranslateRequest, Elastic.Clients.Elasticsearch.Sql.TranslateResponse, Elastic.Clients.Elasticsearch.Sql.TranslateRequestParameters>(request, cancellationToken);
	}
}