using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Nest
{
	internal static class ExceptionExtensions
	{
		private static readonly Lazy<MethodInfo> preserveStackTraceMethodInfo = new Lazy<MethodInfo>(() =>
			typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic)
		);

		public static void RethrowKeepingStackTrace(this Exception exception)
		{
			// In .Net 4.5 ExceptionDispatchInfo.Capture(exception).Throw();
			// But as NEST also still targets .NET 4.0 the old internal hack must be used
			if (preserveStackTraceMethodInfo.Value != null)
			{
#if NET45
				preserveStackTraceMethodInfo.Value.Invoke(exception, null);
#else
				ExceptionDispatchInfo.Capture(exception).Throw();
#endif
			}
			throw exception;
		}

		internal static T ThrowWhen<T>(this T @object, Func<T, bool> predicate, string exceptionMessage)
		{
			var x = predicate?.Invoke(@object);
			if (x.GetValueOrDefault(false))
				throw new ArgumentException(exceptionMessage);

			return @object;
		}

	}
}
