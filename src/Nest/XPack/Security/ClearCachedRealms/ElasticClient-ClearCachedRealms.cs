using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null);

		/// <inheritdoc/>
		IClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request);

		/// <inheritdoc/>
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null) =>
			this.ClearCachedRealms(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)));

		/// <inheritdoc/>
		public IClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request) =>
			this.Dispatcher.Dispatch<IClearCachedRealmsRequest, ClearCachedRealmsRequestParameters, ClearCachedRealmsResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackSecurityClearCachedRealmsDispatch<ClearCachedRealmsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.ClearCachedRealmsAsync(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)), cancellationToken);

		/// <inheritdoc/>
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClearCachedRealmsRequest, ClearCachedRealmsRequestParameters, ClearCachedRealmsResponse, IClearCachedRealmsResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityClearCachedRealmsDispatchAsync<ClearCachedRealmsResponse>(p, c)
			);
	}
}
