using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Allocation(), please update this usage.")]
		public static CatResponse<CatAllocationRecord> CatAllocation(this IElasticClient client,
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null
		)
			=> client.Cat.Allocation(selector);

		[Obsolete("Moved to client.Cat.Allocation(), please update this usage.")]
		public static CatResponse<CatAllocationRecord> CatAllocation(this IElasticClient client, ICatAllocationRequest request)
			=> client.Cat.Allocation(request);

		[Obsolete("Moved to client.Cat.AllocationAsync(), please update this usage.")]
		public static Task<CatResponse<CatAllocationRecord>> CatAllocationAsync(this IElasticClient client,
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.AllocationAsync(selector, ct);

		[Obsolete("Moved to client.Cat.AllocationAsync(), please update this usage.")]
		public static Task<CatResponse<CatAllocationRecord>> CatAllocationAsync(this IElasticClient client, ICatAllocationRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.AllocationAsync(request, ct);
	}
}
