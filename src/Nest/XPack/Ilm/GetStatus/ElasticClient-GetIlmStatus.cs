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
		GetIlmStatusResponse GetIlmStatus(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		GetIlmStatusResponse GetIlmStatus(IGetIlmStatusRequest request);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		Task<GetIlmStatusResponse> GetIlmStatusAsync(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		Task<GetIlmStatusResponse> GetIlmStatusAsync(IGetIlmStatusRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public GetIlmStatusResponse GetIlmStatus(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null) =>
			GetIlmStatus(selector.InvokeOrDefault(new GetIlmStatusDescriptor()));

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public GetIlmStatusResponse GetIlmStatus(IGetIlmStatusRequest request) =>
			DoRequest<IGetIlmStatusRequest, GetIlmStatusResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public Task<GetIlmStatusResponse> GetIlmStatusAsync(Func<GetIlmStatusDescriptor, IGetIlmStatusRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetIlmStatusAsync(selector.InvokeOrDefault(new GetIlmStatusDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetIlmStatus(System.Func{Nest.GetIlmStatusDescriptor,Nest.IGetIlmStatusRequest})" />
		public Task<GetIlmStatusResponse> GetIlmStatusAsync(IGetIlmStatusRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IGetIlmStatusRequest, GetIlmStatusResponse>(request, request.RequestParameters, cancellationToken);
	}
}
