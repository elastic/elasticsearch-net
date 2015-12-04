using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IObservable<ISnapshotStatusResponse> SnapshotObservable(Name repository, Name snapshotName, TimeSpan interval, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null);

		/// <inheritdoc/>
		IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest snapshotRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(Name repository, Name snapshotName, TimeSpan interval, Func<SnapshotDescriptor, SnapshotDescriptor> selector = null)
		{
			var snapshotDescriptor = selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName));
			return new SnapshotObservable(this, snapshotDescriptor);
		}

		/// <inheritdoc/>
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest snapshotRequest) => new SnapshotObservable(this, snapshotRequest);
	}
}