using System.Collections.Generic;
using System.Linq;

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
}
