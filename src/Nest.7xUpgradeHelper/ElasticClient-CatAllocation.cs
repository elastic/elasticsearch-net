using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatAllocationRecord> CatAllocation(this IElasticClient client,
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null
		)
			=> client.Cat.Allocation(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatAllocationRecord> CatAllocation(this IElasticClient client, ICatAllocationRequest request)
			=> client.Cat.Allocation(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatAllocationRecord>> CatAllocationAsync(this IElasticClient client,
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.AllocationAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatAllocationRecord>> CatAllocationAsync(this IElasticClient client, ICatAllocationRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.AllocationAsync(request, ct);
	}
}
