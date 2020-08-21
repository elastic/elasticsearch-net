// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;

namespace Nest
{
	public class ReindexObserver : BulkAllObserver
	{
		private long _seenScrollDocuments;
		private long _seenScrollOperations;

		public ReindexObserver(
			Action<BulkAllResponse> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null
		)
			: base(onNext, onError, onCompleted) { }

		public long SeenScrollDocuments => _seenScrollDocuments;
		public long SeenScrollOperations => _seenScrollOperations;

		internal void IncrementSeenScrollDocuments(long documentCount) => Interlocked.Add(ref _seenScrollDocuments, documentCount);

		internal void IncrementSeenScrollOperations() => Interlocked.Increment(ref _seenScrollOperations);
	}
}
