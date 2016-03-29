#if DOTNETCORE
using System;
using System.Threading;
using System.Diagnostics;

namespace Tests.Framework
{
	public static class CoreFxExtensions
	{
		internal static bool WaitOne(this WaitHandle waitHandle, TimeSpan timeSpan, bool exitContext)
		{
			return waitHandle.WaitOne(timeSpan);
		}

		internal static void Close(this Process process)
		{
			process.Dispose();
		}
	}
}
#endif