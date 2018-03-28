using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		IDeprecationInfoResponse DeprecationInfo(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		IDeprecationInfoResponse DeprecationInfo(IDeprecationInfoRequest request);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		Task<IDeprecationInfoResponse> DeprecationInfoAsync(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		Task<IDeprecationInfoResponse> DeprecationInfoAsync(IDeprecationInfoRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IDeprecationInfoResponse DeprecationInfo(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null) =>
			this.DeprecationInfo(selector.InvokeOrDefault(new DeprecationInfoDescriptor()));

		/// <inheritdoc/>
		public IDeprecationInfoResponse DeprecationInfo(IDeprecationInfoRequest request) =>
			this.Dispatcher.Dispatch<IDeprecationInfoRequest, DeprecationInfoRequestParameters, DeprecationInfoResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackMigrationDeprecationsDispatch<DeprecationInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IDeprecationInfoResponse> DeprecationInfoAsync(Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DeprecationInfoAsync(selector.InvokeOrDefault(new DeprecationInfoDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IDeprecationInfoResponse> DeprecationInfoAsync(IDeprecationInfoRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IDeprecationInfoRequest, DeprecationInfoRequestParameters, DeprecationInfoResponse, IDeprecationInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackMigrationDeprecationsDispatchAsync<DeprecationInfoResponse>(p, c)
			);
	}
}
