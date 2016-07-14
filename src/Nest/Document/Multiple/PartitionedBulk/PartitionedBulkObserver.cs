using System;
using System.Threading;

namespace Nest
{
	public class PartitionedBulkObserver : CoordinatedRequestObserverBase<IBulkResponse>
	{
		private long _totalNumberOfFailedBuffers;
		private long _totalNumberOfRetries;

		public long TotalNumberOfRetries => _totalNumberOfRetries;
		public long TotalNumberOfFailedBuffers => _totalNumberOfFailedBuffers;

		internal void IncrementTotalNumberOfRetries() => Interlocked.Increment(ref _totalNumberOfRetries);
		internal void IncrementTotalNumberOfFailedBuffers() => Interlocked.Increment(ref _totalNumberOfFailedBuffers);

		public PartitionedBulkObserver(
			Action<IBulkResponse> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null
			)
			: base(onNext, onError, onCompleted)
		{
		}

	}
}
