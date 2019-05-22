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
		CatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatIndicesRecord> CatIndices(Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null) =>
			CatIndices(selector.InvokeOrDefault(new CatIndicesDescriptor()));

		/// <inheritdoc />
		public CatResponse<CatIndicesRecord> CatIndices(ICatIndicesRequest request) =>
			DoCat<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(
			Func<CatIndicesDescriptor, ICatIndicesRequest> selector = null,
			CancellationToken ct = default
		) => CatIndicesAsync(selector.InvokeOrDefault(new CatIndicesDescriptor()), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatIndicesRecord>> CatIndicesAsync(ICatIndicesRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatIndicesRequest, CatIndicesRequestParameters, CatIndicesRecord>(request, ct);
	}
}
