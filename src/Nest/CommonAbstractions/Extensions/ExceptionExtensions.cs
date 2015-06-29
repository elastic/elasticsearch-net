using System;
using System.Reflection;
using Nest;

namespace Nest
{
    internal static class ExceptionExtensions
    {
        private static readonly Lazy<MethodInfo> preserveStackTraceMethodInfo = new Lazy<MethodInfo>(() =>
            typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic)
        );

        public static void RethrowKeepingStackTrace(this Exception exception)
        {
            // In .Net 4.5 it would be simple : ExceptionDispatchInfo.Capture(exception).Throw();
            // But as NEST target .Net 4.0 the old internal method must be used
			//TODO NEST 2.0 Use ifdef NET45 to call	ExceptionDispatchInfo.Capture(exception).Throw();
            if (preserveStackTraceMethodInfo.Value != null)
            {
                preserveStackTraceMethodInfo.Value.Invoke(exception, null);
            }
            throw exception;
        }

		internal static T ThrowWhen<T>(this T @object, Predicate<T> predicate, string exceptionMessage)
		{
			if ((predicate?.Invoke(@object)).GetValueOrDefault(false))
				throw new DslException(exceptionMessage);

			return @object;
		}
    }
}
