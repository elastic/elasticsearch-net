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

namespace Elastic.Clients.Elasticsearch.Graph;

public partial class GraphNamespacedClient : Elastic.Clients.Elasticsearch.NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="Elastic.Clients.Elasticsearch.Graph.GraphNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected GraphNamespacedClient() : base()
	{
	}

	internal GraphNamespacedClient(Elastic.Clients.Elasticsearch.ElasticsearchClient client) : base(client)
	{
	}

	public virtual Elastic.Clients.Elasticsearch.Graph.ExploreResponse Explore(Elastic.Clients.Elasticsearch.Graph.ExploreRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Graph.ExploreResponse Explore(Elastic.Clients.Elasticsearch.Indices indices)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor(indices);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Graph.ExploreResponse Explore(Elastic.Clients.Elasticsearch.Indices indices, System.Action<Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor(indices);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Graph.ExploreResponse Explore<TDocument>()
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Graph.ExploreResponse Explore<TDocument>(System.Action<Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Graph.ExploreResponse Explore<TDocument>(Elastic.Clients.Elasticsearch.Indices indices, System.Action<Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>(indices);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Graph.ExploreResponse> ExploreAsync(Elastic.Clients.Elasticsearch.Graph.ExploreRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Graph.ExploreResponse> ExploreAsync(Elastic.Clients.Elasticsearch.Indices indices, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor(indices);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Graph.ExploreResponse> ExploreAsync(Elastic.Clients.Elasticsearch.Indices indices, System.Action<Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor(indices);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Graph.ExploreResponse> ExploreAsync<TDocument>(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Graph.ExploreResponse> ExploreAsync<TDocument>(System.Action<Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Graph.ExploreResponse> ExploreAsync<TDocument>(Elastic.Clients.Elasticsearch.Indices indices, System.Action<Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Graph.ExploreRequestDescriptor<TDocument>(indices);
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Graph.ExploreRequest, Elastic.Clients.Elasticsearch.Graph.ExploreResponse, Elastic.Clients.Elasticsearch.Graph.ExploreRequestParameters>(request, cancellationToken);
	}
}