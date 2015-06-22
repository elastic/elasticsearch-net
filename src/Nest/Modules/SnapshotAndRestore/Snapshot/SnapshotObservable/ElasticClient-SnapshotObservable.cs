using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, Func<SnapshotDescriptor, SnapshotDescriptor> snapshotSelector = null)
		{
			snapshotSelector.ThrowIfNull("snapshotSelector");

			var snapshotDescriptor = snapshotSelector(new SnapshotDescriptor());
			var observable = new SnapshotObservable(this, snapshotDescriptor);
			return observable;
		}

		/// <inheritdoc />
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest snapshotRequest)
		{
			snapshotRequest.ThrowIfNull("snapshotRequest");
			var observable = new SnapshotObservable(this, snapshotRequest);
			return observable;
		}

	}
}