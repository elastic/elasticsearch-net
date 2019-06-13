using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatAllocationRecord> CatAllocation(this IElasticClient client,Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatAllocationRecord> CatAllocation(this IElasticClient client,ICatAllocationRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatAllocationRecord>> CatAllocationAsync(this IElasticClient client,
			Func<CatAllocationDescriptor, ICatAllocationRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatAllocationRecord>> CatAllocationAsync(this IElasticClient client,ICatAllocationRequest request,
			CancellationToken ct = default
		);
	}
}
