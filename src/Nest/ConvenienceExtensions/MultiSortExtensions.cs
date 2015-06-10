namespace Nest
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Provides MultiSort extensions that allow easy sorting across multiple fields
	/// </summary>
	public static class MultiSortExtensions
	{
		public static SearchDescriptor<T> MultiSort<T>(
			this SearchDescriptor<T> instance,
			params Func<SortFieldDescriptor<T>, IFieldSort>[] sorts) where T : class
		{
			foreach (var sort in sorts)
			{
				instance.Sort(sort);
			}

			return instance;
		}

		public static SearchDescriptor<T> MultiSort<T>(
			this SearchDescriptor<T> instance,
			IEnumerable<Func<SortFieldDescriptor<T>, IFieldSort>> sorts) where T : class
		{
			foreach (var sort in sorts)
			{
				instance.Sort(sort);
			}

			return instance;
		}

		public static SearchDescriptor<T> MultiSort<T>(
			this SearchDescriptor<T> instance,
			IEnumerable<SortFieldDescriptor<T>> sorts) where T : class
		{
			foreach (var sort in sorts)
			{
				var copy = sort;
				instance.Sort(s => copy);
			}

			return instance;
		}
	}
}
