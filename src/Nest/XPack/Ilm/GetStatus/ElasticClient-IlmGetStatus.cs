using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves the current index lifecycle management (ILM) status.
		/// You can change the status of the ILM plugin with the Start ILM and Stop ILM APIs.
		/// </summary>
		IIlmGetStatusResponse IlmGetStatus(Func<IlmGetStatusDescriptor, IIlmGetStatusRequest> selector = null);

		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		IIlmGetStatusResponse IlmGetStatus(IIlmGetStatusRequest request);

		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		Task<IIlmGetStatusResponse> IlmGetStatusAsync(Func<IlmGetStatusDescriptor, IIlmGetStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		Task<IIlmGetStatusResponse> IlmGetStatusAsync(IIlmGetStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		public IIlmGetStatusResponse IlmGetStatus(Func<IlmGetStatusDescriptor, IIlmGetStatusRequest> selector = null) =>
			IlmGetStatus(selector.InvokeOrDefault(new IlmGetStatusDescriptor()));

		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		public IIlmGetStatusResponse IlmGetStatus(IIlmGetStatusRequest request) =>
			Dispatcher.Dispatch<IIlmGetStatusRequest, IlmGetStatusRequestParameters, IlmGetStatusResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmGetStatusDispatch<IlmGetStatusResponse>(p)
			);

		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		public Task<IIlmGetStatusResponse> IlmGetStatusAsync(Func<IlmGetStatusDescriptor, IIlmGetStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmGetStatusAsync(selector.InvokeOrDefault(new IlmGetStatusDescriptor()), cancellationToken);

		/// <inheritdoc cref="IlmGetStatus(System.Func{Nest.IlmGetStatusDescriptor,Nest.IIlmGetStatusRequest})" />
		public Task<IIlmGetStatusResponse> IlmGetStatusAsync(IIlmGetStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmGetStatusRequest, IlmGetStatusRequestParameters, IlmGetStatusResponse, IIlmGetStatusResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmGetStatusDispatchAsync<IlmGetStatusResponse>(p, c)
			);
	}
}
