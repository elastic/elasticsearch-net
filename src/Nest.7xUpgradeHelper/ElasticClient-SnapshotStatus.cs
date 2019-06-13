using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static SnapshotStatusResponse SnapshotStatus(this IElasticClient client,Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null);

		/// <inheritdoc />
		public static SnapshotStatusResponse SnapshotStatus(this IElasticClient client,ISnapshotStatusRequest request);

		/// <inheritdoc />
		public static Task<SnapshotStatusResponse> SnapshotStatusAsync(this IElasticClient client,Func<SnapshotStatusDescriptor, ISnapshotStatusRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<SnapshotStatusResponse> SnapshotStatusAsync(this IElasticClient client,ISnapshotStatusRequest request,
			CancellationToken ct = default
		);
	}

}
