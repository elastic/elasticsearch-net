using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster health operation</param>
		IRemoteInfoResponse RemoteInfo(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null);

		/// <inheritdoc/>
		IRemoteInfoResponse RemoteInfo(IRemoteInfoRequest request);

		/// <inheritdoc/>
		Task<IRemoteInfoResponse> RemoteInfoAsync(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IRemoteInfoResponse> RemoteInfoAsync(IRemoteInfoRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IRemoteInfoResponse RemoteInfo(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null) =>
			this.RemoteInfo(selector.InvokeOrDefault(new RemoteInfoDescriptor()));

		/// <inheritdoc/>
		public IRemoteInfoResponse RemoteInfo(IRemoteInfoRequest request) =>
			this.Dispatcher.Dispatch<IRemoteInfoRequest, RemoteInfoRequestParameters, RemoteInfoResponse>(
				request,
				(p, d) => this.LowLevelDispatch.RemoteInfoDispatch<RemoteInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IRemoteInfoResponse> RemoteInfoAsync(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.RemoteInfoAsync(selector.InvokeOrDefault(new RemoteInfoDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IRemoteInfoResponse> RemoteInfoAsync(IRemoteInfoRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IRemoteInfoRequest, RemoteInfoRequestParameters, RemoteInfoResponse, IRemoteInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.RemoteInfoDispatchAsync<RemoteInfoResponse>(p, c)
			);
	}
}
