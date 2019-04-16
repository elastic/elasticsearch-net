using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null) =>
			CatSegments(selector.InvokeOrDefault(new CatSegmentsDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request) =>
			DoCat<ICatSegmentsRequest, CatSegmentsRequestParameters, CatSegmentsRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null,
			CancellationToken ct = default
		) => CatSegmentsAsync(selector.InvokeOrDefault(new CatSegmentsDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request,
			CancellationToken ct = default
		) =>
			DoCatAsync<ICatSegmentsRequest, CatSegmentsRequestParameters, CatSegmentsRecord>(request, ct);
	}
}
