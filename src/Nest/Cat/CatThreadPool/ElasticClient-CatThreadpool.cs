using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null) =>
			CatThreadPool(selector.InvokeOrDefault(new CatThreadPoolDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request) =>
			DoCat<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request,
				LowLevelDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatThreadPoolAsync(selector.InvokeOrDefault(new CatThreadPoolDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, cancellationToken,
				LowLevelDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
	}
}
