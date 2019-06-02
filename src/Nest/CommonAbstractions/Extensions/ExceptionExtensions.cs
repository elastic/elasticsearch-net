using System;

namespace Nest
{
	internal static class ExceptionExtensions
	{
		internal static T ThrowWhen<T>(this T @object, Func<T, bool> predicate, string exceptionMessage)
		{
			var x = predicate?.Invoke(@object);
			if (x.GetValueOrDefault(false))
				throw new ArgumentException(exceptionMessage);

			return @object;
		}
	}
}
