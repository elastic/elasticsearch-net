using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nest.Litterateur
{
	public static class EnumerableExtensions
	{
		//lazy programmer:
		// http://stackoverflow.com/questions/23222046/combine-entries-from-two-lists-by-position-using-linq
		public static IEnumerable<T> Intertwine<T>(this IEnumerable<T> one, IEnumerable<T> two, bool swap = false)
		{
			if (swap)
			{
				var tmp = one;
				one = two;
				two = tmp;
			}
			using (IEnumerator<T> enumeratorOne = one.GetEnumerator(),
								  enumeratorTwo = two.GetEnumerator())
			{
				bool twoFinished = false;

				while (enumeratorOne.MoveNext())
				{
					yield return enumeratorOne.Current;

					if (!twoFinished && enumeratorTwo.MoveNext())
						yield return enumeratorTwo.Current;
					else twoFinished = true;
				}

				if (twoFinished) yield break;

				while (enumeratorTwo.MoveNext())
					yield return enumeratorTwo.Current;
			}
		}

	}

	public static class StringExtensions
	{
		public static string PascalToUnderscore(this string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			return Regex.Replace(
				Regex.Replace(
					Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1_$2"), @"([a-z\d])([A-Z])", "$1_$2")
				, @"[-\s]", "_").ToLower();
		}

		public static string TrimDocExtension(this string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			return input.EndsWith(".doc", StringComparison.OrdinalIgnoreCase)
				? input.Substring(0, input.Length - 4)
				: input;
		}
	}
}
