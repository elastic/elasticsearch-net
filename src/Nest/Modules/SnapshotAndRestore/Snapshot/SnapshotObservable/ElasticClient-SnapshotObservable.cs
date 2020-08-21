// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IObservable<SnapshotStatusResponse> SnapshotObservable(Name repository, Name snapshotName, TimeSpan interval,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null
		);

		/// <inheritdoc />
		IObservable<SnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IObservable<SnapshotStatusResponse> SnapshotObservable(Name repository, Name snapshotName, TimeSpan interval,
			Func<SnapshotDescriptor, ISnapshotRequest> selector = null
		)
		{
			var snapshotDescriptor = selector.InvokeOrDefault(new SnapshotDescriptor(repository, snapshotName));
			return new SnapshotObservable(this, snapshotDescriptor);
		}

		/// <inheritdoc />
		public IObservable<SnapshotStatusResponse> SnapshotObservable(TimeSpan interval, ISnapshotRequest request) =>
			new SnapshotObservable(this, request);
	}
}
