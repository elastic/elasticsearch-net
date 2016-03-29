using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest request);

		/// <inheritdoc/>
		IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);

		/// <inheritdoc/>
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request);

		/// <inheritdoc/>
		Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IUpgradeStatusResponse UpgradeStatus(IUpgradeStatusRequest request) => 
			this.Dispatcher.Dispatch<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatch<UpgradeStatusResponse>(p)
			);

		/// <inheritdoc/>
		public IUpgradeStatusResponse UpgradeStatus(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null) =>
			this.UpgradeStatus(selector.InvokeOrDefault(new UpgradeStatusDescriptor()));

		/// <inheritdoc/>
		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(IUpgradeStatusRequest request) => 
			this.Dispatcher.DispatchAsync<IUpgradeStatusRequest, UpgradeStatusRequestParameters, UpgradeStatusResponse, IUpgradeStatusResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesGetUpgradeDispatchAsync<UpgradeStatusResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IUpgradeStatusResponse> UpgradeStatusAsync(Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null) => 
			this.UpgradeStatusAsync(selector.InvokeOrDefault(new UpgradeStatusDescriptor()));
	}
}
