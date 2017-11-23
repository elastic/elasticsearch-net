using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatSnapshotsRecord> CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatSnapshotsRecord> CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null) =>
			this.CatSnapshots(selector.InvokeOrDefault(new CatSnapshotsDescriptor().RepositoryName(repositories)));

		/// <inheritdoc/>
		public ICatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request) =>
			this.DoCat<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request, this.LowLevelDispatch.CatSnapshotsDispatch<CatResponse<CatSnapshotsRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.CatSnapshotsAsync(selector.InvokeOrDefault(new CatSnapshotsDescriptor().RepositoryName(repositories)), cancellationToken);

		/// <inheritdoc/>
		public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.DoCatAsync<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request, cancellationToken, this.LowLevelDispatch.CatSnapshotsDispatchAsync<CatResponse<CatSnapshotsRecord>>);

	}
}
