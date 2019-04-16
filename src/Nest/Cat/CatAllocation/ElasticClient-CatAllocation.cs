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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatAllocationRecord> CatAllocation(Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null) =>
			CatAllocation(selector.InvokeOrDefault(new CatAllocationDescriptor()));

		/// <inheritdoc />
		public ICatResponse<CatAllocationRecord> CatAllocation(ICatAllocationRequest request) =>
			DoCat<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request);

		/// <inheritdoc />
		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null,
			CancellationToken ct = default
		) => CatAllocationAsync(selector.InvokeOrDefault(new CatAllocationDescriptor()), ct);

		/// <inheritdoc />
		public Task<ICatResponse<CatAllocationRecord>> CatAllocationAsync(ICatAllocationRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatAllocationRequest, CatAllocationRequestParameters, CatAllocationRecord>(request,ct);
	}
}
