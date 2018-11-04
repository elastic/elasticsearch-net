using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ICatResponse<CatSnapshotsRecord> CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null);

		/// <inheritdoc />
		ICatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request);

		/// <inheritdoc />
		Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ICatResponse<CatSnapshotsRecord>
			CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null) =>
			CatSnapshots(selector.InvokeOrDefault(new CatSnapshotsDescriptor(repositories)));

		/// <inheritdoc />
		public ICatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request) =>
			DoCat<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request,
				LowLevelDispatch.CatSnapshotsDispatch<CatResponse<CatSnapshotsRecord>>);

		/// <inheritdoc />
		public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(
			Names repositories,
			Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CatSnapshotsAsync(selector.InvokeOrDefault(new CatSnapshotsDescriptor(repositories)), cancellationToken);

		/// <inheritdoc />
		public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DoCatAsync<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request, cancellationToken,
				LowLevelDispatch.CatSnapshotsDispatchAsync<CatResponse<CatSnapshotsRecord>>);
	}
}
