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
		PutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null);

		/// <inheritdoc cref="PutFilter(Nest.Id,System.Func{Nest.PutFilterDescriptor,Nest.IPutFilterRequest})" />
		PutFilterResponse PutFilter(IPutFilterRequest request);

		/// <inheritdoc cref="PutFilter(Nest.Id,System.Func{Nest.PutFilterDescriptor,Nest.IPutFilterRequest})" />
		Task<PutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="PutFilter(Nest.Id,System.Func{Nest.PutFilterDescriptor,Nest.IPutFilterRequest})" />
		Task<PutFilterResponse> PutFilterAsync(IPutFilterRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public PutFilterResponse PutFilter(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null)
			=> PutFilter(selector.InvokeOrDefault(new PutFilterDescriptor(filterId)));

		/// <inheritdoc />
		public PutFilterResponse PutFilter(IPutFilterRequest request) =>
			DoRequest<IPutFilterRequest, PutFilterResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<PutFilterResponse> PutFilterAsync(Id filterId, Func<PutFilterDescriptor, IPutFilterRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			PutFilterAsync(selector.InvokeOrDefault(new PutFilterDescriptor(filterId)), cancellationToken);

		/// <inheritdoc />
		public Task<PutFilterResponse> PutFilterAsync(IPutFilterRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IPutFilterRequest, PutFilterResponse>(request, request.RequestParameters, cancellationToken);
	}
}
