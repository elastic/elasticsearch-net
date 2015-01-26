using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Litterateur
{
	public static class EnumerableExtensions
	{
		//lazy programmer:
		// http://stackoverflow.com/questions/23222046/combine-entries-from-two-lists-by-position-using-linq
		public static IEnumerable<T> Intertwine<T>(this IEnumerable<T> one, IEnumerable<T> two)
		{
			using (IEnumerator<T> enumeratorOne = one.GetEnumerator(),
								  enumeratorTwo = two.GetEnumerator())
			{
				bool twoFinished = false;

				while (enumeratorOne.MoveNext())
				{
					yield return enumeratorOne.Current;

					if (!twoFinished && enumeratorTwo.MoveNext())
					{
						yield return enumeratorTwo.Current;
					}
				}

				if (!twoFinished)
				{
					while (enumeratorTwo.MoveNext())
					{
						yield return enumeratorTwo.Current;
					}
				}
			}
		}
	}
}
