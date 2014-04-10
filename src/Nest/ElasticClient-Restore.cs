using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAcknowledgedResponse Restore(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.Dispatch<RestoreDescriptor, RestoreRequestParameters, AcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatch<AcknowledgedResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IAcknowledgedResponse> RestoreAsync(string repository, string snapshotName, Func<RestoreDescriptor, RestoreDescriptor> selector = null)
		{
			snapshotName.ThrowIfNullOrEmpty("name");
			repository.ThrowIfNullOrEmpty("repository");
			selector = selector ?? (s => s);
			return this.DispatchAsync<RestoreDescriptor, RestoreRequestParameters, AcknowledgedResponse, IAcknowledgedResponse>(
				s => selector(s.Snapshot(snapshotName).Repository(repository)),
				(p, d) => this.RawDispatch.SnapshotRestoreDispatchAsync<AcknowledgedResponse>(p, d)
			);
		}
	}
}