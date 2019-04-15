using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster remote info API allows to retrieve all of the configured remote cluster information.
		/// <para> </para>
		/// <a href="http://www.elastic.co/guide/en/elasticsearch/reference/master/remote-info.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/remote-info.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the remote info operation</param>
		RemoteInfoResponse RemoteInfo(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null);

		/// <inheritdoc />
		RemoteInfoResponse RemoteInfo(IRemoteInfoRequest request);

		/// <inheritdoc />
		Task<RemoteInfoResponse> RemoteInfoAsync(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<RemoteInfoResponse> RemoteInfoAsync(IRemoteInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public RemoteInfoResponse RemoteInfo(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null) =>
			RemoteInfo(selector.InvokeOrDefault(new RemoteInfoDescriptor()));

		/// <inheritdoc />
		public RemoteInfoResponse RemoteInfo(IRemoteInfoRequest request) =>
			DoRequest<IRemoteInfoRequest, RemoteInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<RemoteInfoResponse> RemoteInfoAsync(Func<RemoteInfoDescriptor, IRemoteInfoRequest> selector = null,
			CancellationToken ct = default
		) =>
			RemoteInfoAsync(selector.InvokeOrDefault(new RemoteInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<RemoteInfoResponse> RemoteInfoAsync(IRemoteInfoRequest request, CancellationToken ct = default
		) =>
			DoRequestAsync<IRemoteInfoRequest, RemoteInfoResponse, RemoteInfoResponse>(request, request.RequestParameters, ct);
	}
}
