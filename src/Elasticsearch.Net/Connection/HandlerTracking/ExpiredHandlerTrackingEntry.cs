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

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if DOTNETCORE
using System;
using System.Net.Http;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Thread-safety: This class is immutable
	/// <para>https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.Http/src/ExpiredHandlerTrackingEntry.cs</para>
	/// </summary>
	internal class ExpiredHandlerTrackingEntry
	{
		private readonly WeakReference _livenessTracker;

		// IMPORTANT: don't cache a reference to `other` or `other.Handler` here.
		// We need to allow it to be GC'ed.
		public ExpiredHandlerTrackingEntry(ActiveHandlerTrackingEntry other)
		{
			Key = other.Key;

			_livenessTracker = new WeakReference(other.Handler);
			InnerHandler = other.Handler.InnerHandler;
		}

		public bool CanDispose => !_livenessTracker.IsAlive;

		public HttpMessageHandler InnerHandler { get; }

		public int Key { get; }
	}
}
#endif
