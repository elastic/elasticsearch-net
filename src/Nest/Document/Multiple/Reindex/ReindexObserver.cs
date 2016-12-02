using System;
using System.Threading;

namespace Nest
{
	public class ReindexObserver<T> : BulkAllObserver where T : class
	{
		private long _seenScrollDocuments = 0;
		private long _seenScrollOperations = 0;

		public long SeenScrollDocuments => _seenScrollDocuments;
		public long SeenScrollOperations => _seenScrollOperations;

		internal void IncrementSeenScrollDocuments(long documentCount) => Interlocked.Add(ref _seenScrollDocuments, documentCount);
		internal void IncrementSeenScrollOperations() => Interlocked.Increment(ref _seenScrollOperations);
		
		public ReindexObserver(
			Action<IBulkAllResponse> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null)
			: base(onNext, onError, onCompleted)
		{
		}

	}
}
