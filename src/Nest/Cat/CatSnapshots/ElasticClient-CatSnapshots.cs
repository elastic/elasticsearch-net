using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		ICatResponse<CatSnapshotsRecord> CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null);

		/// <inheritdoc/>
		ICatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request);

		/// <inheritdoc/>
		Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null);

		/// <inheritdoc/>
		Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public ICatResponse<CatSnapshotsRecord> CatSnapshots(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null) =>
			this.CatSnapshots(selector.InvokeOrDefault(new CatSnapshotsDescriptor(repositories)));

		/// <inheritdoc/>
		public ICatResponse<CatSnapshotsRecord> CatSnapshots(ICatSnapshotsRequest request) =>
			this.DoCat<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request, this.LowLevelDispatch.CatSnapshotsDispatch<CatResponse<CatSnapshotsRecord>>);

		/// <inheritdoc/>
		public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(Names repositories, Func<CatSnapshotsDescriptor, ICatSnapshotsRequest> selector = null) =>
			this.CatSnapshotsAsync(selector.InvokeOrDefault(new CatSnapshotsDescriptor(repositories)));

		/// <inheritdoc/>
		public Task<ICatResponse<CatSnapshotsRecord>> CatSnapshotsAsync(ICatSnapshotsRequest request) =>
			this.DoCatAsync<ICatSnapshotsRequest, CatSnapshotsRequestParameters, CatSnapshotsRecord>(request, this.LowLevelDispatch.CatSnapshotsDispatchAsync<CatResponse<CatSnapshotsRecord>>);

	}
}
