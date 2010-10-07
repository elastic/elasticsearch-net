using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Extensions
{
	public static class Extensions
	{
		public static void ThrowIfNull<T>(this T value, string name) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}
	}
}
