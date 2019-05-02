using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		DeprecationInfoResponse DeprecationInfo(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		DeprecationInfoResponse DeprecationInfo(IDeprecationInfoRequest request);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		Task<DeprecationInfoResponse> DeprecationInfoAsync(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		Task<DeprecationInfoResponse> DeprecationInfoAsync(IDeprecationInfoRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public DeprecationInfoResponse DeprecationInfo(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null) =>
			DeprecationInfo(selector.InvokeOrDefault(new DeprecationInfoDescriptor()));

		/// <inheritdoc />
		public DeprecationInfoResponse DeprecationInfo(IDeprecationInfoRequest request) =>
			DoRequest<IDeprecationInfoRequest, DeprecationInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<DeprecationInfoResponse> DeprecationInfoAsync(
			Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null,
			CancellationToken ct = default
		) => DeprecationInfoAsync(selector.InvokeOrDefault(new DeprecationInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<DeprecationInfoResponse> DeprecationInfoAsync(IDeprecationInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IDeprecationInfoRequest, DeprecationInfoResponse>
				(request, request.RequestParameters, ct);
	}
}
