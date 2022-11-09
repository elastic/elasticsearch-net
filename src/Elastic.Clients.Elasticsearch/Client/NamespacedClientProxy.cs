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

/// <summary>
/// </summary>
/// <remarks>
///     Not intended to be used directly.
/// </remarks>
public abstract class NamespacedClientProxy
{
	private readonly ElasticsearchClient _client;

	internal NamespacedClientProxy(ElasticsearchClient client) => _client = client;

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		TRequestParameters parameters,
		Action<IRequestConfiguration>? forceConfiguration = null)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new() =>
			_client.DoRequest<TRequest, TResponse, TRequestParameters>(request, parameters, forceConfiguration);

	internal TResponse DoRequest<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		Action<IRequestConfiguration>? forceConfiguration = null)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new() =>
			_client.DoRequest<TRequest, TResponse, TRequestParameters>(request, forceConfiguration);

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		TRequestParameters parameters,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new() =>
			_client.DoRequestAsync<TRequest, TResponse, TRequestParameters>(request, parameters, cancellationToken: cancellationToken);

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new() =>
			_client.DoRequestAsync<TRequest, TResponse, TRequestParameters>(request, cancellationToken: cancellationToken);

	internal Task<TResponse> DoRequestAsync<TRequest, TResponse, TRequestParameters>(
		TRequest request,
		TRequestParameters parameters,
		Action<IRequestConfiguration>? forceConfiguration,
		CancellationToken cancellationToken = default)
		where TRequest : Request<TRequestParameters>
		where TResponse : ElasticsearchResponse, new()
		where TRequestParameters : RequestParameters, new() =>
			_client.DoRequestAsync<TRequest, TResponse, TRequestParameters>(request, parameters, forceConfiguration, cancellationToken);
}
