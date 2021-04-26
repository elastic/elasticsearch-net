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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Tests.Framework.EndpointTests.TestState
{
	/// <summary>
	/// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
	/// </summary>
	/// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
	public class AsyncLazy<T>
	{
		/// <summary>
		/// The underlying lazy task.
		/// </summary>
		private readonly Lazy<Task<T>> _instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;" /> class.
		/// </summary>
		/// <param name="factory">The delegate that is invoked on a background thread to produce the value when it is needed.</param>
		public AsyncLazy(Func<T> factory) => _instance = new Lazy<Task<T>>(() => Task.Run(factory));

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;" /> class.
		/// </summary>
		/// <param name="factory">The asynchronous delegate that is invoked on a background thread to produce the value when it is needed.</param>
		public AsyncLazy(Func<Task<T>> factory) => _instance = new Lazy<Task<T>>(() => Task.Run(factory));

		/// <summary>
		/// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;" /> to be await'ed.
		/// </summary>
		public TaskAwaiter<T> GetAwaiter() => _instance.Value.GetAwaiter();

		/// <summary>
		/// Starts the asynchronous initialization, if it has not already started.
		/// </summary>
		public void Start()
		{
			var unused = _instance.Value;
		}
	}
}
