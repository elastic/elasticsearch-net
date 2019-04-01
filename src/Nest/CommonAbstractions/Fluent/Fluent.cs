using System;
using System.Runtime.CompilerServices;

namespace Nest
{
	internal static class Fluent
	{
		[Obsolete("Use the overload that accepts TValue")]
		internal static TDescriptor Assign<TDescriptor, TInterface>(TDescriptor self, Action<TInterface> assign)
			where TDescriptor : class, TInterface
		{
			assign(self);
			return self;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static TDescriptor Assign<TDescriptor, TInterface, TValue>(TDescriptor self, TValue value, Action<TInterface, TValue> assign)
			where TDescriptor : class, TInterface
		{
			assign(self, value);
			return self;
		}
	}
}
