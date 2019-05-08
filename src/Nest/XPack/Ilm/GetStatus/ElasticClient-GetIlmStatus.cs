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
		IGetIlmStatusResponse GetIlmStatus(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		IGetIlmStatusResponse GetIlmStatus(IGetIlmStatusRequest request);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		Task<IGetIlmStatusResponse> GetIlmStatusAsync(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		Task<IGetIlmStatusResponse> GetIlmStatusAsync(IGetIlmStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public IGetIlmStatusResponse GetIlmStatus(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null) =>
			GetIlmStatus(selector.InvokeOrDefault(new GetIlmStatusDescriptor()));

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public IGetIlmStatusResponse GetIlmStatus(IGetIlmStatusRequest request) =>
			Dispatcher.Dispatch<IGetIlmStatusRequest, GetIlmStatusRequestParameters, GetIlmStatusResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmGetStatusDispatch<GetIlmStatusResponse>(p)
			);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public Task<IGetIlmStatusResponse> GetIlmStatusAsync(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetIlmStatusAsync(selector.InvokeOrDefault(new GetIlmStatusDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public Task<IGetIlmStatusResponse> GetIlmStatusAsync(IGetIlmStatusRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetIlmStatusRequest, GetIlmStatusRequestParameters, GetIlmStatusResponse, IGetIlmStatusResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmGetStatusDispatchAsync<GetIlmStatusResponse>(p, c)
			);
	}
}
