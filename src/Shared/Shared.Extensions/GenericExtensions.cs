using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Extensions
{
    public static class GenericExtensions
    {
		public static void ThrowIfNull<T>(this T value, string name)
		{
			if (value == null)
				throw new ArgumentNullException(name);
		}
    }
}
