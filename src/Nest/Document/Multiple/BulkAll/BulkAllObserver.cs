// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Threading;

namespace Nest
{
	public class BulkAllObserver : CoordinatedRequestObserverBase<BulkAllResponse>
	{
		private long _totalNumberOfFailedBuffers;
		private long _totalNumberOfRetries;

		public BulkAllObserver(
			Action<BulkAllResponse> onNext = null,
			Action<Exception> onError = null,
			Action onCompleted = null
		)
			: base(onNext, onError, onCompleted) { }

		public long TotalNumberOfFailedBuffers => _totalNumberOfFailedBuffers;

		public long TotalNumberOfRetries => _totalNumberOfRetries;

		internal void IncrementTotalNumberOfRetries() => Interlocked.Increment(ref _totalNumberOfRetries);

		internal void IncrementTotalNumberOfFailedBuffers() => Interlocked.Increment(ref _totalNumberOfFailedBuffers);
	}
}
