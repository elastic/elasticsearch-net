using System;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IObservable<ISnapshotStatusResponse> SnapshotObservable(Name repository, Name snapshotName, TimeSpan interval, Func<SnapshotDescriptor, ISnapshotRequest> selector = null);

		/// <inheritdoc/>
		IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(Name repository, Name snapshotName, TimeSpan interval, Func<SnapshotDescriptor, ISnapshotRequest> selector = null)
		{
			var snapshotDescriptor = selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName));
			return new SnapshotObservable(this, snapshotDescriptor);
		}

		/// <inheritdoc/>
		public IObservable<ISnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest request) => new SnapshotObservable(this, request);
	}
}