using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a machine learning filter.
		/// </summary>
		IPutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null);

		/// <inheritdoc cref="PutFilter(Nest.Id,System.Func{Nest.PutFilterDescriptor,Nest.IPutFilterRequest})" />
		IPutFilterResponse PutFilter(IPutFilterRequest request);

		/// <inheritdoc cref="PutFilter(Nest.Id,System.Func{Nest.PutFilterDescriptor,Nest.IPutFilterRequest})" />
		Task<IPutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="PutFilter(Nest.Id,System.Func{Nest.PutFilterDescriptor,Nest.IPutFilterRequest})" />
		Task<IPutFilterResponse> PutFilterAsync(IPutFilterRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IPutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null)
			=> PutFilter(selector.InvokeOrDefault(new PutFilterDescriptor(filterId)));

		/// <inheritdoc />
		public IPutFilterResponse PutFilter(IPutFilterRequest request) =>
			Dispatcher.Dispatch<IPutFilterRequest, PutFilterRequestParameters, PutFilterResponse>(
				request,
				LowLevelDispatch.XpackMlPutFilterDispatch<PutFilterResponse>
			);

		/// <inheritdoc />
		public Task<IPutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutFilterAsync(selector.InvokeOrDefault(new PutFilterDescriptor(filterId)), cancellationToken);

		/// <inheritdoc />
		public Task<IPutFilterResponse> PutFilterAsync(IPutFilterRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPutFilterRequest, PutFilterRequestParameters, PutFilterResponse, IPutFilterResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackMlPutFilterDispatchAsync<PutFilterResponse>
			);
	}
}
