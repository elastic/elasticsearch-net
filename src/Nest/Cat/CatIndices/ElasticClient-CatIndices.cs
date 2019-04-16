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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null) =>
			CatIndices(selector.InvokeOrDefault(new CatIndicesDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request) =>
			DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken ct = default
		) => CatIndicesAsync(selector.InvokeOrDefault(new CatIndicesDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, ct);
	}
}
