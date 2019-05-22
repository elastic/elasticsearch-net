using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Api.Cat;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatCountRecord> CatCount(ICatCountRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null) =>
			CatCount(selector.InvokeOrDefault(new CatCountDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatCountRecord> CatCount(ICatCountRequest request) =>
			DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatCountRecord>> CatCountAsync(
			Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken ct = default
		) =>
			CatCountAsync(selector.InvokeOrDefault(new CatCountDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, ct);
	}
}
