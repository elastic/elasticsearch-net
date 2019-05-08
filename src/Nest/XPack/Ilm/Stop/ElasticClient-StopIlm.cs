using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stop the index lifecycle management (ILM) plugin.
		/// Halts all lifecycle management operations and stops the ILM plugin. This is useful when you are performing
		/// maintenance on the cluster and need to prevent ILM from performing any actions on your indices.
		/// The API returns as soon as the stop request has been acknowledged, but the plugin might continue to run until
		/// in-progress operations complete and the plugin can be safely stopped. Use the Get ILM Status API to see if ILM is running.
		/// </summary>
		IStopIlmResponse StopIlm(Func<StopIlmDescriptor, IStopIlmRequest> selector = null);

		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		IStopIlmResponse StopIlm(IStopIlmRequest request);

		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		Task<IStopIlmResponse> StopIlmAsync(Func<StopIlmDescriptor, IStopIlmRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		Task<IStopIlmResponse> StopIlmAsync(IStopIlmRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		public IStopIlmResponse StopIlm(Func<StopIlmDescriptor, IStopIlmRequest> selector = null) =>
			StopIlm(selector.InvokeOrDefault(new StopIlmDescriptor()));

		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		public IStopIlmResponse StopIlm(IStopIlmRequest request) =>
			Dispatcher.Dispatch<IStopIlmRequest, StopIlmRequestParameters, StopIlmResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmStopDispatch<StopIlmResponse>(p)
			);

		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		public Task<IStopIlmResponse> StopIlmAsync(Func<StopIlmDescriptor, IStopIlmRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			StopIlmAsync(selector.InvokeOrDefault(new StopIlmDescriptor()), cancellationToken);

		/// <inheritdoc cref="StopIlm(System.Func{Nest.StopIlmDescriptor,Nest.IStopIlmRequest})" />
		public Task<IStopIlmResponse> StopIlmAsync(IStopIlmRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IStopIlmRequest, StopIlmRequestParameters, StopIlmResponse, IStopIlmResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmStopDispatchAsync<StopIlmResponse>(p, c)
			);
	}
}
