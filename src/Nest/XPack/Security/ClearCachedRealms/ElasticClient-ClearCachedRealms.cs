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
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request,
			CancellationToken ct = default
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
			Dispatch2<IClearCachedRealmsRequest, ClearCachedRealmsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(
			Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			ClearCachedRealmsAsync(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)), cancellationToken);

		/// <inheritdoc />
		public Task<IClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IClearCachedRealmsRequest, IClearCachedRealmsResponse, ClearCachedRealmsResponse>(request, request.RequestParameters, ct);
	}
}
