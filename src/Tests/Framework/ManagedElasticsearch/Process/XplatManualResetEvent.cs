using System;
using System.Threading;

namespace Tests.Framework.Integration
{
		// Investigate  problem with ManualResetEvent on CoreClr
		// Maybe due to .WaitOne() not taking exitContext?
		public class XplatManualResetEvent
		{
			private readonly object _lock = new object();
			private bool _notified;

			public XplatManualResetEvent(bool initialState)
			{
				_notified = initialState;
			}

			public void Set()
			{
				lock (_lock)
				{
					if (_notified) return;
					_notified = true;
					Monitor.Pulse(_lock);
				}
			}

			public bool WaitOne(TimeSpan timeout, bool exitContext)
			{
				lock (_lock)
				{
					bool exit = true;
					if (_notified) return exit;
					exit = Monitor.Wait(_lock, timeout);
					return exit;
				}
			}
		}
}
