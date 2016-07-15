using System;
using System.Threading;

namespace Nest
{
	public class BulkAllObserver : CoordinatedRequestObserverBase<IBulkAllResponse>
	{
		private long _totalNumberOfFailedBuffers;
		private long _totalNumberOfRetries;

		public long TotalNumberOfRetries => _totalNumberOfRetries;
		public long TotalNumberOfFailedBuffers => _totalNumberOfFailedBuffers;

		internal void IncrementTotalNumberOfRetries() => Interlocked.Increment(ref _totalNumberOfRetries);
		internal void IncrementTotalNumberOfFailedBuffers() => Interlocked.Increment(ref _totalNumberOfFailedBuffers);

		public BulkAllObserver(
			Action<IBulkAllResponse> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null
			)
			: base(onNext, onError, onCompleted)
		{
		}

	}
}
