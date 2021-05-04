// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests.Core.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
		{
			T[] array = null;
			var count = 0;

			foreach (var item in source)
			{
				if (array == null) array = new T[size];
				array[count] = item;
				count++;
				if (count == size)
				{
					yield return new ReadOnlyCollection<T>(array);

					array = null;
					count = 0;
				}
			}

			if (array != null)
			{
				Array.Resize(ref array, count);
				yield return new ReadOnlyCollection<T>(array);
			}
		}
	}
}
