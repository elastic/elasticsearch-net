using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null);

		/// <inheritdoc />
		ClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request);

		/// <inheritdoc />
		Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ClearCachedRealmsResponse ClearCachedRealms(Names realms, Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null
		) =>
			ClearCachedRealms(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)));

		/// <inheritdoc />
		public ClearCachedRealmsResponse ClearCachedRealms(IClearCachedRealmsRequest request) =>
			DoRequest<IClearCachedRealmsRequest, ClearCachedRealmsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(
			Names realms,
			Func<ClearCachedRealmsDescriptor, IClearCachedRealmsRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			ClearCachedRealmsAsync(selector.InvokeOrDefault(new ClearCachedRealmsDescriptor(realms)), cancellationToken);

		/// <inheritdoc />
		public Task<ClearCachedRealmsResponse> ClearCachedRealmsAsync(IClearCachedRealmsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClearCachedRealmsRequest, ClearCachedRealmsResponse>(request, request.RequestParameters, ct);
	}
}
