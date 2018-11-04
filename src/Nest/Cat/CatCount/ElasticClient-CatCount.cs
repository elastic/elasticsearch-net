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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatCountRecord> CatCount(Func<CatCountDescriptor, ICatCountRequest> selector = null) =>
			CatCount(selector.InvokeOrDefault(new CatCountDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatCountRecord> CatCount(ICatCountRequest request) =>
			DoCat<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request,
				LowLevelDispatch.CatCountDispatch<CatResponse<CatCountRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(Func<CatCountDescriptor, ICatCountRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			CatCountAsync(selector.InvokeOrDefault(new CatCountDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatCountRecord>> CatCountAsync(ICatCountRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatCountRequest, CatCountRequestParameters, CatCountRecord>(request, cancellationToken,
				LowLevelDispatch.CatCountDispatchAsync<CatResponse<CatCountRecord>>);
	}
}
