using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatCountRecord> CatCount(ICatCountRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null) =>
			this.CatCount(selector.InvokeOrDefault(new CatCountDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatCountRecord> CatCount(ICatCountRequest request)=>
			this.DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, this.LowLevelDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.CatCountAsync(selector.InvokeOrDefault(new CatCountDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, cancellationToken, this.LowLevelDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);

	}
}
