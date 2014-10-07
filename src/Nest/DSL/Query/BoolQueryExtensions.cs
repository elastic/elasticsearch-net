using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolQueryExtensions
	{
		internal static bool CanMergeMustAndMustNots(this IBoolQuery bq)
		{
			return bq == null || !bq.Should.HasAny();
		}

		internal static bool CanJoinShould(this IQueryContainer container)
		{
			return !container.IsStrict && container.Bool.CanJoinShould();
		}

		internal static bool CanJoinShould(this IBoolQuery bq)
		{
			return bq == null 
				   || (bq.MinimumShouldMatch == null
					&& (
				      (bq.Should.HasAny() && !bq.Must.HasAny() && !bq.MustNot.HasAny())
				       || !bq.Should.HasAny()
				       )
				   );
		}

		internal static IEnumerable<IQueryContainer> MergeShouldQueries(this IQueryContainer lbq, IQueryContainer rbq)
		{
			if (!lbq.CanJoinShould() || !lbq.CanJoinShould())
				return new[] { lbq, rbq }; 

			var lBoolQuery = lbq.Bool;
			var rBoolQuery = rbq.Bool;

			var lHasShouldQueries = lBoolQuery != null && lBoolQuery.Should.HasAny();
			var rHasShouldQueries = rBoolQuery != null && rBoolQuery.Should.HasAny();

			var lq = lHasShouldQueries ? lBoolQuery.Should : new[] { lbq };
			var rq = rHasShouldQueries ? rBoolQuery.Should : new[] { rbq };

			//originally: return lq.Concat(rq);
			//however performance of this degraded rapidly see #974
 			//forcing ToList() helped but manually newing a list doing addrange is 10x faster even still.
			var x = new List<IQueryContainer>(lq);
			x.AddRange(rq);
			return x;
		}
	}
}