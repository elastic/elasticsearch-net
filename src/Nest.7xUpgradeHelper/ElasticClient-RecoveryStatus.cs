using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static RecoveryStatusResponse RecoveryStatus(this IElasticClient client,Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null);

		/// <inheritdoc />
		public static RecoveryStatusResponse RecoveryStatus(this IElasticClient client,IRecoveryStatusRequest request);

		/// <inheritdoc />
		public static Task<RecoveryStatusResponse> RecoveryStatusAsync(this IElasticClient client,Indices indices, Func<RecoveryStatusDescriptor, IRecoveryStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<RecoveryStatusResponse> RecoveryStatusAsync(this IElasticClient client,IRecoveryStatusRequest request,
			CancellationToken ct = default
		);
	}

}
