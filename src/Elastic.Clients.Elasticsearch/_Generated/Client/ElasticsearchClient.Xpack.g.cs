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

namespace Elastic.Clients.Elasticsearch.Xpack;

public partial class XpackNamespacedClient : Elastic.Clients.Elasticsearch.NamespacedClientProxy
{
	/// <summary>
	/// <para>
	/// Initializes a new instance of the <see cref="Elastic.Clients.Elasticsearch.Xpack.XpackNamespacedClient"/> class for mocking.
	/// </para>
	/// </summary>
	protected XpackNamespacedClient() : base()
	{
	}

	internal XpackNamespacedClient(Elastic.Clients.Elasticsearch.ElasticsearchClient client) : base(client)
	{
	}

	public virtual Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse Info(Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest, Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse, Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse Info()
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest, Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse, Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse Info(System.Action<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest, Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse, Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse> InfoAsync(Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest, Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse, Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse> InfoAsync(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest, Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse, Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse> InfoAsync(System.Action<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequest, Elastic.Clients.Elasticsearch.Xpack.XpackInfoResponse, Elastic.Clients.Elasticsearch.Xpack.XpackInfoRequestParameters>(request, cancellationToken);
	}

	public virtual Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse Usage(Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest request)
	{
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest, Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse, Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse Usage()
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest, Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse, Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestParameters>(request);
	}

	public virtual Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse Usage(System.Action<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequest<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest, Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse, Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestParameters>(request);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse> UsageAsync(Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest request, System.Threading.CancellationToken cancellationToken = default)
	{
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest, Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse, Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse> UsageAsync(System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestDescriptor();
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest, Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse, Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestParameters>(request, cancellationToken);
	}

	public virtual System.Threading.Tasks.Task<Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse> UsageAsync(System.Action<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestDescriptor> action, System.Threading.CancellationToken cancellationToken = default)
	{
		var builder = new Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestDescriptor();
		action.Invoke(builder);
		var request = builder.Instance;
		request.BeforeRequest();
		return DoRequestAsync<Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequest, Elastic.Clients.Elasticsearch.Xpack.XpackUsageResponse, Elastic.Clients.Elasticsearch.Xpack.XpackUsageRequestParameters>(request, cancellationToken);
	}
}