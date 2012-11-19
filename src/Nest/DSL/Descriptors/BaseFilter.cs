using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class BaseFilter
	{
		[JsonProperty(PropertyName = "bool")]
		internal BoolBaseFilterDescriptor BoolFilterDescriptor { get; set; }

		internal bool IsConditionless { get; set; }

		public static BaseFilter operator &(BaseFilter lbq, BaseFilter rbq)
		{
			if (lbq.IsConditionless && rbq.IsConditionless)
				return new BaseFilter() { IsConditionless = true };
			else if (lbq.IsConditionless)
				return rbq;
			else if (rbq.IsConditionless)
				return lbq;

			var f = new BaseFilter();
			var fq = new BoolBaseFilterDescriptor();
			f.BoolFilterDescriptor = fq;

			if (lbq.BoolFilterDescriptor == null && rbq.BoolFilterDescriptor == null)
			{
				fq._MustFilters = new[] { lbq, rbq };
				return f;
			}
			if (lbq.BoolFilterDescriptor != null && rbq.BoolFilterDescriptor != null)
			{
				if (lbq.BoolFilterDescriptor._HasOnlyMustNot() && rbq.BoolFilterDescriptor._HasOnlyMustNot())
				{
					fq._MustFilters = null;
					fq._MustNotFilters = lbq.MergeMustNotFilters(rbq);
				}
				else if (lbq.BoolFilterDescriptor.CanJoinMust() && rbq.BoolFilterDescriptor.CanJoinMust())
					fq._MustFilters = lbq.MergeMustFilters(rbq);
				else
					fq._MustFilters = new[] { lbq, rbq };

				if (lbq.BoolFilterDescriptor.CanJoinMustNot() && rbq.BoolFilterDescriptor.CanJoinMustNot())
					fq._MustNotFilters = lbq.MergeMustNotFilters(rbq);
				return f;
			}
			if (lbq.BoolFilterDescriptor != null)
			{
				if (lbq.BoolFilterDescriptor._HasOnlyMustNot())
				{
					fq._MustNotFilters = lbq.BoolFilterDescriptor._MustNotFilters;
					fq._MustFilters = new[] { rbq };
					return f;
				}

				else if (lbq.BoolFilterDescriptor.CanJoinMust())
					fq._MustFilters = lbq.MergeMustFilters(rbq);
				else
					fq._MustFilters = new[] { lbq, rbq };

				if (lbq.BoolFilterDescriptor.CanJoinMustNot())
					fq._MustNotFilters = lbq.MergeMustNotFilters(rbq);
				return f;
			}

			if (rbq.BoolFilterDescriptor._HasOnlyMustNot())
			{
				fq._MustNotFilters = rbq.BoolFilterDescriptor._MustNotFilters;
				fq._MustFilters = new[] { lbq };
				return f;
			}

			if (rbq.BoolFilterDescriptor.CanJoinMust())
				fq._MustFilters = rbq.MergeMustFilters(lbq);
			else
				fq._MustFilters = new[] { rbq, lbq };

			if (rbq.BoolFilterDescriptor.CanJoinMustNot())
				fq._MustNotFilters = rbq.MergeMustNotFilters(lbq);
			return f;

		}

		public static BaseFilter operator |(BaseFilter lbq, BaseFilter rbq)
		{
			if (lbq.IsConditionless && rbq.IsConditionless)
				return new BaseFilter() { IsConditionless = true };
			else if (lbq.IsConditionless)
				return rbq;
			else if (rbq.IsConditionless)
				return lbq;

			var f = new BaseFilter();
			var fq = new BoolBaseFilterDescriptor();
			fq._ShouldFilters = new[] { lbq, rbq };
			f.BoolFilterDescriptor = fq;

			if (lbq.BoolFilterDescriptor == null && rbq.BoolFilterDescriptor == null)
			{
				fq._ShouldFilters = lbq.MergeShouldFilters(rbq);
				return f;
			}
			else if (lbq.BoolFilterDescriptor != null && rbq.BoolFilterDescriptor == null)
			{
				JoinShouldOnSide(lbq, rbq, fq);
			}
			else if (rbq.BoolFilterDescriptor != null && lbq.BoolFilterDescriptor == null)
			{
				JoinShouldOnSide(rbq, lbq, fq);
			}
			else
			{
				if (lbq.BoolFilterDescriptor.CanJoinShould() && rbq.BoolFilterDescriptor.CanJoinShould())
					fq._ShouldFilters = lbq.MergeShouldFilters(rbq);
				else
					fq._ShouldFilters = new[] { lbq, rbq };
			}



			return f;
		}

		public static BaseFilter operator !(BaseFilter lbq)
		{
			if (lbq.IsConditionless)
				return new BaseFilter { IsConditionless = true };

			var f = new BaseFilter();
			var fq = new BoolBaseFilterDescriptor();
			fq._MustNotFilters = new[] { lbq };

			f.BoolFilterDescriptor = fq;
			return f;
		}

		public static bool operator false(BaseFilter a)
		{
			return false;
		}

		public static bool operator true(BaseFilter a)
		{
			return false;
		}

		private static void JoinShouldOnSide(BaseFilter lbq, BaseFilter rbq, BoolBaseFilterDescriptor bq)
		{
			bq._ShouldFilters = lbq.MergeShouldFilters(rbq);
		}
	}
}
