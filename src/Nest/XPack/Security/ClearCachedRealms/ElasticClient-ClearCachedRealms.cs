using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null);

		/// <inheritdoc/>
		IClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request);

		/// <inheritdoc/>
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null);

		/// <inheritdoc/>
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request);
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
				(p, d) =>this.LowLevelDispatch.ShieldClearCachedRealmsDispatch<ClearCachedRealmsResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null) =>
			this.ClearCachedRealmsAsync(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)));

		/// <inheritdoc/>
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request) =>
			this.Dispatcher.DispatchAsync<IClearCachedRealmsRequest, ClearCachedRealmsRequestParameters, ClearCachedRealmsResponse, IClearCachedRealmsResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldClearCachedRealmsDispatchAsync<ClearCachedRealmsResponse>(p)
			);
	}
}
