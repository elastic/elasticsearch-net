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
		IIlmStopResponse IlmStop(Func<IlmStopDescriptor, IIlmStopRequest> selector = null);

		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		IIlmStopResponse IlmStop(IIlmStopRequest request);

		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		Task<IIlmStopResponse> IlmStopAsync(Func<IlmStopDescriptor, IIlmStopRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		Task<IIlmStopResponse> IlmStopAsync(IIlmStopRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		public IIlmStopResponse IlmStop(Func<IlmStopDescriptor, IIlmStopRequest> selector = null) =>
			IlmStop(selector.InvokeOrDefault(new IlmStopDescriptor()));

		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		public IIlmStopResponse IlmStop(IIlmStopRequest request) =>
			Dispatcher.Dispatch<IIlmStopRequest, IlmStopRequestParameters, IlmStopResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmStopDispatch<IlmStopResponse>(p)
			);

		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		public Task<IIlmStopResponse> IlmStopAsync(Func<IlmStopDescriptor, IIlmStopRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmStopAsync(selector.InvokeOrDefault(new IlmStopDescriptor()), cancellationToken);

		/// <inheritdoc cref="IlmStop(System.Func{Nest.IlmStopDescriptor,Nest.IIlmStopRequest})" />
		public Task<IIlmStopResponse> IlmStopAsync(IIlmStopRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmStopRequest, IlmStopRequestParameters, IlmStopResponse, IIlmStopResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmStopDispatchAsync<IlmStopResponse>(p, c)
			);
	}
}
