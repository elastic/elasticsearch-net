using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static UpgradeStatusResponse UpgradeStatus(this IElasticClient client,IUpgradeStatusRequest request);

		/// <inheritdoc />
		public static UpgradeStatusResponse UpgradeStatus(this IElasticClient client,Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null);

		/// <inheritdoc />
		public static Task<UpgradeStatusResponse> UpgradeStatusAsync(this IElasticClient client,IUpgradeStatusRequest request,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<UpgradeStatusResponse> UpgradeStatusAsync(this IElasticClient client,Func<UpgradeStatusDescriptor, IUpgradeStatusRequest> selector = null,
			CancellationToken ct = default
		);
	}

}
