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
		CatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null) =>
			CatThreadPool(selector.InvokeOrDefault(new CatThreadPoolDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request) =>
			DoCat<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken ct = default
		) => CatThreadPoolAsync(selector.InvokeOrDefault(new CatThreadPoolDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, ct);
	}
}
