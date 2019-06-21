using System;

namespace Nest 
{
	internal static class UpgradeAssistantInvokeExtensions
	{
		internal static TReturn InvokeOrDefault<T, TReturn>(this Func<T, TReturn> func, T @default)
			where T : class, TReturn where TReturn : class =>
			func?.Invoke(@default) ?? @default;
	}
}