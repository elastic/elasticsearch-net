/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
