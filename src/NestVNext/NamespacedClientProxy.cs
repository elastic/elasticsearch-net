using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	public class NamespacedClientProxy
	{
		private readonly ElasticClient _client;

		protected NamespacedClientProxy(ElasticClient client) => _client = client;

		internal TResponse DoRequest<TRequest, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
				_client.DoRequest<TRequest, TResponse>(p, parameters, forceConfiguration);

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest p,
			IRequestParameters parameters,
			CancellationToken ct,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
				_client.DoRequestAsync<TRequest, TResponse>(p, parameters, ct, forceConfiguration);
	}
}
