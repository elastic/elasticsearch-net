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

namespace Elastic.Clients.Elasticsearch.Tasks;

public partial class TasksNamespacedClient : Elastic.Clients.Elasticsearch.NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="Elastic.Clients.Elasticsearch.Tasks.TasksNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected TasksNamespacedClient() : base()
	{
	}

	internal TasksNamespacedClient(Elastic.Clients.Elasticsearch.ElasticsearchClient client) : base(client)
	{
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.CancelResponse Cancel(Elastic.Clients.Elasticsearch.Tasks.CancelRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.CancelResponse Cancel()
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.CancelResponse Cancel(System.Action<Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.CancelResponse Cancel(Elastic.Clients.Elasticsearch.TaskId taskId)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor(taskId);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.CancelResponse Cancel(Elastic.Clients.Elasticsearch.TaskId taskId, System.Action<Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor(taskId);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.CancelResponse> CancelAsync(Elastic.Clients.Elasticsearch.Tasks.CancelRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.CancelResponse> CancelAsync(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.CancelResponse> CancelAsync(System.Action<Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.CancelResponse> CancelAsync(Elastic.Clients.Elasticsearch.TaskId taskId, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor(taskId);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.CancelResponse> CancelAsync(Elastic.Clients.Elasticsearch.TaskId taskId, System.Action<Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.CancelRequestDescriptor(taskId);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.CancelRequest, Elastic.Clients.Elasticsearch.Tasks.CancelResponse, Elastic.Clients.Elasticsearch.Tasks.CancelRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse Get(Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest, Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse Get(Elastic.Clients.Elasticsearch.Id taskId)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(taskId);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest, Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse Get(Elastic.Clients.Elasticsearch.Id taskId, System.Action<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(taskId);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest, Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse> GetAsync(Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest, Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse> GetAsync(Elastic.Clients.Elasticsearch.Id taskId, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(taskId);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest, Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse> GetAsync(Elastic.Clients.Elasticsearch.Id taskId, System.Action<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestDescriptor(taskId);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.GetTasksRequest, Elastic.Clients.Elasticsearch.Tasks.GetTasksResponse, Elastic.Clients.Elasticsearch.Tasks.GetTasksRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.ListResponse List(Elastic.Clients.Elasticsearch.Tasks.ListRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.ListRequest, Elastic.Clients.Elasticsearch.Tasks.ListResponse, Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.ListResponse List()
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.ListRequest, Elastic.Clients.Elasticsearch.Tasks.ListResponse, Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Tasks.ListResponse List(System.Action<Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Tasks.ListRequest, Elastic.Clients.Elasticsearch.Tasks.ListResponse, Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.ListResponse> ListAsync(Elastic.Clients.Elasticsearch.Tasks.ListRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.ListRequest, Elastic.Clients.Elasticsearch.Tasks.ListResponse, Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.ListResponse> ListAsync(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.ListRequest, Elastic.Clients.Elasticsearch.Tasks.ListResponse, Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Tasks.ListResponse> ListAsync(System.Action<Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Tasks.ListRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Tasks.ListRequest, Elastic.Clients.Elasticsearch.Tasks.ListResponse, Elastic.Clients.Elasticsearch.Tasks.ListRequestParameters>(request, cancellationToken);
	}
}