#if DOTNETCORE
using System;
using System.Diagnostics;
using System.Threading;

namespace Tests.Framework
{
	public static class CoreFxExtensions
	{
		internal static bool WaitOne(this WaitHandle waitHandle, TimeSpan timeSpan, bool exitContext) => waitHandle.WaitOne(timeSpan);

		internal static void Close(this Process process) => process.Dispose();
	}
}
#endif
