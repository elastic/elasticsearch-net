// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if DOTNETCORE
using System;
using System.Diagnostics;
using System.Threading;

namespace Elasticsearch.Net
{
	/// <summary>
    /// Thread-safety: We treat this class as immutable except for the timer. Creating a new object
	/// for the 'expiry' pool simplifies the threading requirements significantly.
	/// <para>https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.Http/src/ActiveHandlerTrackingEntry.cs</para>
	/// </summary>
    internal class ActiveHandlerTrackingEntry
    {
        private static readonly TimerCallback TimerCallback = (s) => ((ActiveHandlerTrackingEntry)s).Timer_Tick();
        private readonly object _lock;
        private bool _timerInitialized;
        private Timer _timer;
        private TimerCallback _callback;

        public ActiveHandlerTrackingEntry(
            int key,
            LifetimeTrackingHttpMessageHandler handler,
            TimeSpan lifetime)
        {
            Key = key;
            Handler = handler;
            Lifetime = lifetime;

            _lock = new object();
        }

        public LifetimeTrackingHttpMessageHandler Handler { get; private set; }

        public TimeSpan Lifetime { get; }

        public int Key { get; }

        public void StartExpiryTimer(TimerCallback callback)
        {
            if (Lifetime == Timeout.InfiniteTimeSpan) return;

            if (Volatile.Read(ref _timerInitialized)) return;

            StartExpiryTimerSlow(callback);
        }

        private void StartExpiryTimerSlow(TimerCallback callback)
        {
            Debug.Assert(Lifetime != Timeout.InfiniteTimeSpan);

            lock (_lock)
            {
                if (Volatile.Read(ref _timerInitialized))
                    return;

                _callback = callback;
                _timer = NonCapturingTimer.Create(TimerCallback, this, Lifetime, Timeout.InfiniteTimeSpan);
                _timerInitialized = true;
            }
        }

        private void Timer_Tick()
        {
            Debug.Assert(_callback != null);
            Debug.Assert(_timer != null);

            lock (_lock)
			{
				if (_timer == null) return;

				_timer.Dispose();
				_timer = null;

				_callback(this);
			}
        }
    }
}
#endif
