using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Benchmarking
{
	public static class PartitionExtension
	{
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int size)
		{
			T[] array = null;
			int count = 0;

			foreach (var item in source)
			{
				if (array == null)
				{
					array = new T[size];
				}
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

		public static decimal GetMedian(this IEnumerable<int> source)
		{
			// Create a copy of the input, and sort the copy
			int[] temp = source.ToArray();
			Array.Sort(temp);

			int count = temp.Length;
			if (count == 0)
			{
				throw new InvalidOperationException("Empty collection");
			}

			if (count % 2 == 0)
			{
				// count is even, average two middle elements
				int a = temp[count / 2 - 1];
				int b = temp[count / 2];
				return (a + b) / 2m;
			}
			
			// count is odd, return the middle element
			return temp[count / 2];
		}
	}
}