using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		FieldCapabilitiesResponse FieldCapabilities(Indices indices, Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null);

		/// <inheritdoc />
		FieldCapabilitiesResponse FieldCapabilities(IFieldCapabilitiesRequest request);

		/// <inheritdoc />
		Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public FieldCapabilitiesResponse FieldCapabilities(
			Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null
		) => FieldCapabilities(selector.InvokeOrDefault(new FieldCapabilitiesDescriptor().Index(indices)));

		/// <inheritdoc />
		public FieldCapabilitiesResponse FieldCapabilities(IFieldCapabilitiesRequest request) =>
			DoRequest<IFieldCapabilitiesRequest, FieldCapabilitiesResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(
			Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		) => FieldCapabilitiesAsync(selector.InvokeOrDefault(new FieldCapabilitiesDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<FieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IFieldCapabilitiesRequest, FieldCapabilitiesResponse>(request, request.RequestParameters, ct);

	}
}
