using System;
using System.Runtime.CompilerServices;

namespace Nest
{
	internal static class Fluent
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static TDescriptor Assign<TDescriptor, TInterface, TValue>(TDescriptor self, TValue value, Action<TInterface, TValue> assign)
			where TDescriptor : class, TInterface
		{
			assign(self, value);
			return self;
		}
	}
}
