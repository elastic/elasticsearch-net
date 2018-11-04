using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null) =>
			CatIndices(selector.InvokeOrDefault(new CatIndicesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request) =>
			DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request,
				LowLevelDispatch.CatIndicesDispatch<CatResponse<CatIndicesRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatIndicesAsync(selector.InvokeOrDefault(new CatIndicesDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, cancellationToken,
				LowLevelDispatch.CatIndicesDispatchAsync<CatResponse<CatIndicesRecord>>);
	}
}
