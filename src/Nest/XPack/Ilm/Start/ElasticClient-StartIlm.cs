using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Start the index lifecycle management (ILM) plugin.
		/// Starts the ILM plugin if it is currently stopped. ILM is started automatically when the cluster is formed.
		/// Restarting ILM is only necessary if it has been stopped using the Stop ILM API.
		/// </summary>
		IStartIlmResponse StartIlm(Func<StartIlmDescriptor, IStartIlmRequest> selector = null);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		IStartIlmResponse StartIlm(IStartIlmRequest request);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		Task<IStartIlmResponse> StartIlmAsync(Func<StartIlmDescriptor, IStartIlmRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		Task<IStartIlmResponse> StartIlmAsync(IStartIlmRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public IStartIlmResponse StartIlm(Func<StartIlmDescriptor, IStartIlmRequest> selector = null) =>
			StartIlm(selector.InvokeOrDefault(new StartIlmDescriptor()));

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public IStartIlmResponse StartIlm(IStartIlmRequest request) =>
			Dispatcher.Dispatch<IStartIlmRequest, StartIlmRequestParameters, StartIlmResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmStartDispatch<StartIlmResponse>(p)
			);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public Task<IStartIlmResponse> StartIlmAsync(Func<StartIlmDescriptor, IStartIlmRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			StartIlmAsync(selector.InvokeOrDefault(new StartIlmDescriptor()), cancellationToken);

		/// <inheritdoc cref="StartIlm(System.Func{Nest.StartIlmDescriptor,Nest.IStartIlmRequest})" />
		public Task<IStartIlmResponse> StartIlmAsync(IStartIlmRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IStartIlmRequest, StartIlmRequestParameters, StartIlmResponse, IStartIlmResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmStartDispatchAsync<StartIlmResponse>(p, c)
			);
	}
}
