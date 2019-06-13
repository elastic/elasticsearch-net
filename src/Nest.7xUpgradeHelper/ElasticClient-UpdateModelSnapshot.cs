using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Updates a machine learning model snapshot.
		/// </summary>
		public static UpdateModelSnapshotResponse UpdateModelSnapshot(this IElasticClient client,Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null
		);

		/// <inheritdoc />
		public static UpdateModelSnapshotResponse UpdateModelSnapshot(this IElasticClient client,IUpdateModelSnapshotRequest request);

		/// <inheritdoc />
		public static Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(this IElasticClient client,Id jobId, Id snapshotId,
			Func<UpdateModelSnapshotDescriptor, IUpdateModelSnapshotRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<UpdateModelSnapshotResponse> UpdateModelSnapshotAsync(this IElasticClient client,IUpdateModelSnapshotRequest request,
			CancellationToken ct = default
		);
	}

}
