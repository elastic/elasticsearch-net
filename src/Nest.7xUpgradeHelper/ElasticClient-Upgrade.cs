using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static UpgradeResponse Upgrade(this IElasticClient client,IUpgradeRequest request);

		/// <inheritdoc />
		public static UpgradeResponse Upgrade(this IElasticClient client,Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null);

		/// <inheritdoc />
		public static Task<UpgradeResponse> UpgradeAsync(this IElasticClient client,IUpgradeRequest request, CancellationToken ct = default);

		/// <inheritdoc />
		public static Task<UpgradeResponse> UpgradeAsync(this IElasticClient client,Indices indices, Func<UpgradeDescriptor, IUpgradeRequest> selector = null,
			CancellationToken ct = default
		);
	}

}
