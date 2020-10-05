// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
