using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatSegmentsRecord> CatSegments(Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null) =>
			this.CatSegments(selector.InvokeOrDefault(new CatSegmentsDescriptor()));

		/// <inheritdoc/>
		public ICatResponse<CatSegmentsRecord> CatSegments(ICatSegmentsRequest request) =>
			this.DoCat<ICatSegmentsRequest, CatSegmentsRequestParameters, CatSegmentsRecord>(request, this.LowLevelDispatch.CatSegmentsDispatch<CatResponse<CatSegmentsRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(
			Func<CatSegmentsDescriptor, ICatSegmentsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatSegmentsAsync(selector.InvokeOrDefault(new CatSegmentsDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatSegmentsRecord>> CatSegmentsAsync(ICatSegmentsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatSegmentsRequest, CatSegmentsRequestParameters, CatSegmentsRecord>(request, cancellationToken, this.LowLevelDispatch.CatSegmentsDispatchAsync<CatResponse<CatSegmentsRecord>>);

	}
}
