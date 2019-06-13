using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static XPackUsageResponse XPackUsage(this IElasticClient client,Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null);

		/// <inheritdoc />
		public static XPackUsageResponse XPackUsage(this IElasticClient client,IXPackUsageRequest request);

		/// <inheritdoc />
		public static Task<XPackUsageResponse> XPackUsageAsync(this IElasticClient client,Func<XPackUsageDescriptor, IXPackUsageRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<XPackUsageResponse> XPackUsageAsync(this IElasticClient client,IXPackUsageRequest request, CancellationToken ct = default);
	}

}
