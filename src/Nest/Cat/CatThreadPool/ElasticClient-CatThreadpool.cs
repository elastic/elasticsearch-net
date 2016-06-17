using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null) =>
			this.CatThreadPool(selector.InvokeOrDefault(new CatThreadPoolDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatThreadPoolRecord> CatThreadPool(ICatThreadPoolRequest request) =>
			this.DoCat<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, this.LowLevelDispatch.CatThreadPoolDispatch<CatResponse<CatThreadPoolRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(
			Func<CatThreadPoolDescriptor, ICatThreadPoolRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatThreadPoolAsync(selector.InvokeOrDefault(new CatThreadPoolDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatThreadPoolRecord>> CatThreadPoolAsync(ICatThreadPoolRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatThreadPoolRequest, CatThreadPoolRequestParameters, CatThreadPoolRecord>(request, cancellationToken, this.LowLevelDispatch.CatThreadPoolDispatchAsync<CatResponse<CatThreadPoolRecord>>);
	}
}
