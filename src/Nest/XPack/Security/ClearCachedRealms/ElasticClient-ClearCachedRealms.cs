using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null);

		/// <inheritdoc />
		IClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request);

		/// <inheritdoc />
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null
		) =>
			ClearCachedRealms(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)));

		/// <inheritdoc />
		public IClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request) =>
			Dispatcher.Dispatch<IClearCachedRealmsRequest, ClearCachedRealmsRequestParameters, ClearCachedRealmsResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityClearCachedRealmsDispatch<ClearCachedRealmsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ClearCachedRealmsAsync(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)), cancellationToken);

		/// <inheritdoc />
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IClearCachedRealmsRequest, ClearCachedRealmsRequestParameters, ClearCachedRealmsResponse, IClearCachedRealmsResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SecurityClearCachedRealmsDispatchAsync<ClearCachedRealmsResponse>(p, c)
				);
	}
}
