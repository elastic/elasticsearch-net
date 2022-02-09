// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// </summary>
	/// <remarks>
	///     Not intended to be used directly.
	/// </remarks>
	public class NamespacedClientProxy
	{
		private readonly ElasticsearchClient _client;

		protected NamespacedClientProxy(ElasticsearchClient client) => _client = client;

		internal TResponse DoRequest<TRequest, TResponse>(
			TRequest request,
			IRequestParameters parameters,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
			_client.DoRequest<TRequest, TResponse>(request, parameters, forceConfiguration);

		internal TResponse DoRequest<TRequest, TResponse>(
			TRequest request,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
				_client.DoRequest<TRequest, TResponse>(request, forceConfiguration);

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest request,
			IRequestParameters parameters,
			CancellationToken cancellationToken = default)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
			_client.DoRequestAsync<TRequest, TResponse>(request, parameters, cancellationToken: cancellationToken);

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest request,
			CancellationToken cancellationToken = default)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
			_client.DoRequestAsync<TRequest, TResponse>(request, cancellationToken: cancellationToken);

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest request,
			IRequestParameters parameters,
			Action<IRequestConfiguration>? forceConfiguration,
			CancellationToken cancellationToken = default)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
			_client.DoRequestAsync<TRequest, TResponse>(request, parameters, forceConfiguration, cancellationToken);
	}
}
