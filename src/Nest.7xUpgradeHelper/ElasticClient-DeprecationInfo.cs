using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		public static DeprecationInfoResponse DeprecationInfo(this IElasticClient client,Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		public static DeprecationInfoResponse DeprecationInfo(this IElasticClient client,IDeprecationInfoRequest request);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		public static Task<DeprecationInfoResponse> DeprecationInfoAsync(this IElasticClient client,Func<DeprecationInfoDescriptor, IDeprecationInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <summary>
		/// Retrieves information about different cluster, node, and index level settings that use deprecated
		/// features that will be removed or changed in the next major version.
		/// </summary>
		public static Task<DeprecationInfoResponse> DeprecationInfoAsync(this IElasticClient client,IDeprecationInfoRequest request,
			CancellationToken ct = default
		);
	}

}
