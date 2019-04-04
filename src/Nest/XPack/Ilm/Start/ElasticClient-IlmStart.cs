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
		IIlmStartResponse IlmStart(Func<IlmStartDescriptor, IIlmStartRequest> selector = null);

		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		IIlmStartResponse IlmStart(IIlmStartRequest request);

		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		Task<IIlmStartResponse> IlmStartAsync(Func<IlmStartDescriptor, IIlmStartRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		Task<IIlmStartResponse> IlmStartAsync(IIlmStartRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		public IIlmStartResponse IlmStart(Func<IlmStartDescriptor, IIlmStartRequest> selector = null) =>
			IlmStart(selector.InvokeOrDefault(new IlmStartDescriptor()));

		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		public IIlmStartResponse IlmStart(IIlmStartRequest request) =>
			Dispatcher.Dispatch<IIlmStartRequest, IlmStartRequestParameters, IlmStartResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmStartDispatch<IlmStartResponse>(p)
			);

		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		public Task<IIlmStartResponse> IlmStartAsync(Func<IlmStartDescriptor, IIlmStartRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmStartAsync(selector.InvokeOrDefault(new IlmStartDescriptor()), cancellationToken);

		/// <inheritdoc cref="IlmStart(System.Func{Nest.IlmStartDescriptor,Nest.IIlmStartRequest})" />
		public Task<IIlmStartResponse> IlmStartAsync(IIlmStartRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmStartRequest, IlmStartRequestParameters, IlmStartResponse, IIlmStartResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmStartDispatchAsync<IlmStartResponse>(p, c)
			);
	}
}
