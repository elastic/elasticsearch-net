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
using System.Net.Http;

namespace Elasticsearch.Net
{
	/// <summary>
	/// This a marker used to check if the underlying handler should be disposed. HttpClients
	/// share a reference to an instance of this class, and when it goes out of scope the inner handler
	/// is eligible to be disposed.
	/// <para>https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.Http/src/LifetimeTrackingHttpMessageHandler.cs</para>
	/// </summary>
	internal class LifetimeTrackingHttpMessageHandler : DelegatingHandler
	{
		public LifetimeTrackingHttpMessageHandler(HttpMessageHandler innerHandler)
			: base(innerHandler) { }

		protected override void Dispose(bool disposing)
		{
			// The lifetime of this is tracked separately by ActiveHandlerTrackingEntry
		}
	}
}
#endif
