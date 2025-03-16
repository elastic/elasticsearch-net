// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;

namespace Elastic.Clients.Elasticsearch;

public abstract class NamespacedClientProxy
{
	private const string InvalidOperation = "The client has not been initialised for proper usage as may have been partially mocked. Ensure you are using a " +
		"new instance of ElasticsearchClient to perform requests over a network to Elasticsearch.";

	protected ElasticsearchClient Client { get; }

	/// <summary>
	/// Initializes a new instance for mocking.
	/// </summary>
	protected NamespacedClientProxy() { }

	internal NamespacedClientProxy(ElasticsearchClient client) => Client = client;

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(TRequest request)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
			=> DoRequest<TRequest, TResponse, TRequestParameters>(request, null);

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (Client is null)
			ThrowHelper.ThrowInvalidOperationException(InvalidOperation);

		return Client.DoRequest<TRequest, TResponse, TRequestParameters>(request, forceConfiguration);
	}

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
			=> DoRequestAsync<TRequest, TResponse, TRequestParameters>(request, null, cancellationToken);

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : TransportResponse, new()
		where TRequestParameters : RequestParameters, new()
	{
		if (Client is null)
			ThrowHelper.ThrowInvalidOperationException(InvalidOperation);

		return Client.DoRequestAsync<TRequest, TResponse, TRequestParameters>(request, forceConfiguration, cancellationToken);
	}
}
