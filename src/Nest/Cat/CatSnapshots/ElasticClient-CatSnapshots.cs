using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Api.Cat;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		CatResponse<CatSnapshotsRecord> CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null);

		/// <inheritdoc />
		CatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request);

		/// <inheritdoc />
		Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken ct = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<CatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request,
			CancellationToken ct = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public CatResponse<CatSnapshotsRecord>
			CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null) =>
			CatSnapshots(selector.InvokeOrDefault(new CatSnapshotsDescriptor().RepositoryName(repositories)));

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
