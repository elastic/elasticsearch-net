using System.Collections.Generic;

namespace Tests.Framework.Extensions
{
	public static class FullFrameworkExtensions
	{
		internal static void Deconstruct<T, TValue>(this KeyValuePair<T, TValue> pair, out T key, out TValue value)
		{
			key = pair.Key;
			value = pair.Value;
		}
	}
}
