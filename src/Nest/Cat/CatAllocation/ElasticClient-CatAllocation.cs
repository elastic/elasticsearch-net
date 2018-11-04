using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null) =>
			CatAllocation(selector.InvokeOrDefault(new CatAllocationDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request) =>
			DoCat<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request,
				LowLevelDispatch.CatAllocationDispatch<CatResponse<CatAllocationRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatAllocationAsync(selector.InvokeOrDefault(new CatAllocationDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(
				request,
				cancellationToken,
				LowLevelDispatch.CatAllocationDispatchAsync<CatResponse<CatAllocationRecord>>
			);
	}
}
