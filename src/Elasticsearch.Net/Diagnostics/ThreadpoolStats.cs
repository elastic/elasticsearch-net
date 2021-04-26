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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Elasticsearch.Net.Diagnostics
{
	/// <summary>
	/// Retrieves Statistics for thread pools
	/// </summary>
	public class ThreadPoolStats
	{
		private static readonly string WorkerThreads = "Worker";
		private static readonly string CompletionPortThreads = "IOCP";

		/// <summary>
		/// Retrieve thread pool statistics
		/// </summary>
		public static ReadOnlyDictionary<string, ThreadPoolStatistics> GetStats()
		{
			var dictionary = new Dictionary<string, ThreadPoolStatistics>(2);
			ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxIoThreads);
			ThreadPool.GetAvailableThreads(out var freeWorkerThreads, out var freeIoThreads);
			ThreadPool.GetMinThreads(out var minWorkerThreads, out var minIoThreads);
			var busyIoThreads = maxIoThreads - freeIoThreads;
			var busyWorkerThreads = maxWorkerThreads - freeWorkerThreads;

			dictionary.Add(WorkerThreads, new ThreadPoolStatistics(minWorkerThreads, maxWorkerThreads, busyWorkerThreads, freeWorkerThreads));
			dictionary.Add(CompletionPortThreads, new ThreadPoolStatistics(minIoThreads, maxIoThreads, busyIoThreads, freeIoThreads));
			return new ReadOnlyDictionary<string, ThreadPoolStatistics>(dictionary);
		}
	}

	/// <summary>
	/// Statistics for a thread pool
	/// </summary>
	public class ThreadPoolStatistics
	{
		public ThreadPoolStatistics(int min, int max, int busy, int free)
		{
			Min = min;
			Max = max;
			Busy = busy;
			Free = free;
		}

		/// <summary>The difference between the maximum number of thread pool threads returned by
		/// <see cref="Max"/>, and the number currently free.
		/// </summary>
		public int Busy { get; }

		/// <summary>The difference between the maximum number of thread pool threads returned by
		/// <see cref="Max"/>, and the number currently active.
		/// </summary>
		public int Free { get; }

		/// <summary>
		/// The number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until
		/// thread pool threads become available.
		/// </summary>
		public int Max { get; }

		/// <summary>
		/// The minimum number of threads the thread pool creates on demand, as new requests are made, before switching to an algorithm for
		/// managing thread creation and destruction.
		/// </summary>
		public int Min { get; }
	}
}
