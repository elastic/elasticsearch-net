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

		internal static bool CanJoinShould(this IBoolQuery bq)
		{
			return bq == null
			       || (
				       (bq.Should.HasAny() && !bq.Must.HasAny() && !bq.MustNot.HasAny())
				       || !bq.Should.HasAny()
				       );
		}

		internal static IEnumerable<IQueryContainer> MergeShouldQueries(this IQueryContainer lbq, IQueryContainer rbq)
		{
			var lBoolDescriptor = lbq.Bool;
			var lHasShouldQueries = lBoolDescriptor != null &&
			                        lBoolDescriptor.Should.HasAny();

			var rBoolDescriptor = rbq.Bool;
			var rHasShouldQueries = rBoolDescriptor != null &&
			                        rBoolDescriptor.Should.HasAny();

			var lq = lHasShouldQueries ? lBoolDescriptor.Should : new[] { lbq };
			var rq = rHasShouldQueries ? rBoolDescriptor.Should : new[] { rbq };

			return lq.Concat(rq);
		}
	}
}