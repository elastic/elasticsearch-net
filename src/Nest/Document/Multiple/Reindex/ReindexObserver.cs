using System;
using System.Threading;

namespace Nest
{
	public class ReindexObserver : BulkAllObserver
	{
		private long _seenScrollDocuments;
		private long _seenScrollOperations;

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
