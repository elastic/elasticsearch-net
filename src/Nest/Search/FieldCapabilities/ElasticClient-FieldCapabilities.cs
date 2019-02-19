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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IFieldCapabilitiesResponse FieldCapabilities(Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null
		) =>
			FieldCapabilities(selector.InvokeOrDefault(new FieldCapabilitiesDescriptor().Index(indices)));

		/// <inheritdoc />
		public IFieldCapabilitiesResponse FieldCapabilities(IFieldCapabilitiesRequest request) =>
			Dispatcher.Dispatch<IFieldCapabilitiesRequest, FieldCapabilitiesRequestParameters, FieldCapabilitiesResponse>(
				request, (p, d) => LowLevelDispatch.FieldCapsDispatch<FieldCapabilitiesResponse>(p)
			);

		/// <inheritdoc />
		public Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(Indices indices,
			Func<FieldCapabilitiesDescriptor, IFieldCapabilitiesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			FieldCapabilitiesAsync(selector.InvokeOrDefault(new FieldCapabilitiesDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IFieldCapabilitiesResponse> FieldCapabilitiesAsync(IFieldCapabilitiesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			=> Dispatcher
				.DispatchAsync<IFieldCapabilitiesRequest, FieldCapabilitiesRequestParameters, FieldCapabilitiesResponse, IFieldCapabilitiesResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.FieldCapsDispatchAsync<FieldCapabilitiesResponse>(p, c)
				);
	}
}
