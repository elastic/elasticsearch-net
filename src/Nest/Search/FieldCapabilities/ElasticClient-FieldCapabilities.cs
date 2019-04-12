using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IFieldCapabilitiesResponse FieldCapabilities(Indices indices, Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null);

		/// <inheritdoc />
		IFieldCapabilitiesResponse FieldCapabilities(IFieldCapabilitiesRequest request);

		/// <inheritdoc />
		Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IFieldCapabilitiesResponse FieldCapabilities(
			Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null
		) => FieldCapabilities(selector.InvokeOrDefault(new FieldCapabilitiesDescriptor().Index(indices)));

		/// <inheritdoc />
		public IFieldCapabilitiesResponse FieldCapabilities(IFieldCapabilitiesRequest request) =>
			DoRequest<IFieldCapabilitiesRequest, FieldCapabilitiesResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(
			Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null,
			CancellationToken ct = default
		) => FieldCapabilitiesAsync(selector.InvokeOrDefault(new FieldCapabilitiesDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IFieldCapabilitiesRequest, IFieldCapabilitiesResponse, FieldCapabilitiesResponse>(request, request.RequestParameters, ct);

	}
}
