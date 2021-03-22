using System;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Nest
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Not intended to be used directly.
	/// </remarks>
	public class NamespacedClientProxy
	{
		private readonly ElasticClient _client;

		protected NamespacedClientProxy(ElasticClient client) => _client = client;

		internal TResponse DoRequest<TRequest, TResponse>(
			TRequest request,
			IRequestParameters parameters,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
				_client.DoRequest<TRequest, TResponse>(request, parameters, forceConfiguration);

		internal Task<TResponse> DoRequestAsync<TRequest, TResponse>(
			TRequest request,
			IRequestParameters parameters,
			CancellationToken cancellationToken = default,
			Action<IRequestConfiguration>? forceConfiguration = null)
			where TRequest : class, IRequest
			where TResponse : class, ITransportResponse, new() =>
				_client.DoRequestAsync<TRequest, TResponse>(request, parameters, cancellationToken, forceConfiguration);
	}
}
