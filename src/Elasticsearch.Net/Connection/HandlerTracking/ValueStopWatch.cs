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
using System.Threading;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A convenience API for interacting with System.Threading.Timer in a way
	/// that doesn't capture the ExecutionContext. We should be using this (or equivalent)
	/// everywhere we use timers to avoid rooting any values stored in asynclocals.
	/// <para> https://github.com/dotnet/runtime/blob/master/src/libraries/Common/src/Extensions/ValueStopwatch/ValueStopwatch.cs </para>
	/// </summary>
	internal static class NonCapturingTimer
	{
		public static Timer Create(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			if (callback == null) throw new ArgumentNullException(nameof(callback));

			// Don't capture the current ExecutionContext and its AsyncLocals onto the timer
			var restoreFlow = false;
			try
			{
				if (ExecutionContext.IsFlowSuppressed()) return new Timer(callback, state, dueTime, period);

				ExecutionContext.SuppressFlow();
				restoreFlow = true;

				return new Timer(callback, state, dueTime, period);
			}
			finally
			{
				// Restore the current ExecutionContext
				if (restoreFlow) ExecutionContext.RestoreFlow();
			}
		}
	}
}
#endif
