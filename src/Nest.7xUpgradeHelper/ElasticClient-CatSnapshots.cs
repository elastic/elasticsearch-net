using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static CatResponse<CatSnapshotsRecord> CatSnapshots(this IElasticClient client,Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null);

		/// <inheritdoc />
		public static CatResponse<CatSnapshotsRecord> CatSnapshots(this IElasticClient client,ICatSnapshotsRequest request);

		/// <inheritdoc />
		public static Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(this IElasticClient client,
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(this IElasticClient client,ICatSnapshotsRequest request,
			CancellationToken ct = default
		);
	}

	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public CatResponse<CatSnapshotsRecord>
			CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null) =>
			public static CatSnapshots(selector.InvokeOrDefault(new CatSnapshotsDescriptor().RepositoryName(this IElasticClient client,repositories)));

		/// <inheritdoc />
		public CatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request) =>
			DoCat<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request);

		/// <inheritdoc />
		public Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken ct = default
		) => CatSnapshotsAsync(selector.InvokeOrDefault(new CatSnapshotsDescriptor().RepositoryName(repositories)), ct);

		/// <inheritdoc />
		public Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request, CancellationToken ct = default) =>
			DoCatAsync<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request, ct);
	}
}
