using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatCountRecord> CatCount(ICatCountRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null) =>
			CatCount(selector.InvokeOrDefault(new CatCountDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatCountRecord> CatCount(ICatCountRequest request) =>
			DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(
			Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatCountAsync(selector.InvokeOrDefault(new CatCountDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, ct);
	}
}
