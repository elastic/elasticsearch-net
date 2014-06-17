using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolFilterExtensions
	{
		internal static bool CanMergeMustAndMustNots(this IBoolFilter bq)
		{
			return bq == null || !((IBoolFilter)bq).Should.HasAny();
		}

		internal static bool CanJoinShould(this IBoolFilter bq)
		{
			var bf = (IBoolFilter)bq;
			return bq == null 
			       || (
				       (bf.Should.HasAny() && !bf.Must.HasAny() && !bf.MustNot.HasAny())
				       || !bf.Should.HasAny()
				       );
		}
		
		internal static IEnumerable<IFilterContainer> MergeShouldFilters(this IFilterContainer lbq, IFilterContainer rbq)
		{
			var lBoolDescriptor = lbq.Bool;
			var lHasShouldFilters = lBoolDescriptor != null &&
			                        ((IBoolFilter)lBoolDescriptor).Should.HasAny();

			var rBoolDescriptor = rbq.Bool;
			var rHasShouldFilters = rBoolDescriptor != null &&
			                        ((IBoolFilter)rBoolDescriptor).Should.HasAny();


			var lq = lHasShouldFilters ? ((IBoolFilter)lBoolDescriptor).Should : new[] { lbq };
			var rq = rHasShouldFilters ? ((IBoolFilter)rBoolDescriptor).Should : new[] { rbq };

			return lq.Concat(rq);
		}
	}
}