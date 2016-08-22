using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Xunit
{

	public static class ForEachAsyncExtensions
	{

		public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
		{
			return Task.WhenAll(
				from partition in Partitioner.Create(source).GetPartitions(dop)
				select Task.Run(async delegate
				{
					using (partition)
						while (partition.MoveNext())
							await body(partition.Current);
				}));
		}

	}
}