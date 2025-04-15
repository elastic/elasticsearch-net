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

namespace Elastic.Clients.Elasticsearch.TextStructure;

public partial class TextStructureNamespacedClient : Elastic.Clients.Elasticsearch.NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="Elastic.Clients.Elasticsearch.TextStructure.TextStructureNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected TextStructureNamespacedClient() : base()
	{
	}

	internal TextStructureNamespacedClient(Elastic.Clients.Elasticsearch.ElasticsearchClient client) : base(client)
	{
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse FindFieldStructure(Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse FindFieldStructure()
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse FindFieldStructure(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse FindFieldStructure<TDocument>()
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor<TDocument>();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse FindFieldStructure<TDocument>(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse> FindFieldStructureAsync(Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse> FindFieldStructureAsync(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse> FindFieldStructureAsync(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse> FindFieldStructureAsync<TDocument>(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor<TDocument>();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse> FindFieldStructureAsync<TDocument>(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor<TDocument>> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindFieldStructureRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse FindMessageStructure(Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse FindMessageStructure(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse FindMessageStructure<TDocument>(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse> FindMessageStructureAsync(Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse> FindMessageStructureAsync(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse> FindMessageStructureAsync<TDocument>(System.Action<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor<TDocument>> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestDescriptor<TDocument>();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequest, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureResponse, Elastic.Clients.Elasticsearch.TextStructure.FindMessageStructureRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse TestGrokPattern(Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequest, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse TestGrokPattern(System.Action<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequest, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse> TestGrokPatternAsync(Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequest, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse> TestGrokPatternAsync(System.Action<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequest, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternResponse, Elastic.Clients.Elasticsearch.TextStructure.TestGrokPatternRequestParameters>(request, cancellationToken);
	}
}