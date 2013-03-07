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
	public class BaseQuery
	{
		[JsonProperty(PropertyName = "bool")]
		internal BoolBaseQueryDescriptor BoolQueryDescriptor { get; set; }

		internal bool IsConditionless { get; set; }

		public static BaseQuery operator &(BaseQuery lbq, BaseQuery rbq)
		{
			if (lbq == null)
				return rbq;
			if (rbq == null)
				return lbq;

			if (lbq.IsConditionless && rbq.IsConditionless)
				return new BaseQuery() { IsConditionless = true };
			else if (lbq.IsConditionless)
				return rbq;
			else if (rbq.IsConditionless)
				return lbq;

			var q = new BaseQuery();
			var bq = new BoolBaseQueryDescriptor();
			q.BoolQueryDescriptor = bq;

			
			if (lbq.BoolQueryDescriptor == null && rbq.BoolQueryDescriptor == null)
			{
				bq._MustQueries = new[] { lbq, rbq };
				return q;
			}
			if (lbq.BoolQueryDescriptor != null && rbq.BoolQueryDescriptor != null)
			{
				if (lbq.BoolQueryDescriptor._HasOnlyMustNot() && rbq.BoolQueryDescriptor._HasOnlyMustNot())
				{
					bq._MustQueries = null;
					bq._MustNotQueries = lbq.MergeMustNotQueries(rbq);
				}
				else if (lbq.BoolQueryDescriptor.CanJoinMust() && rbq.BoolQueryDescriptor.CanJoinMust())
					bq._MustQueries = lbq.MergeMustQueries(rbq);
				else
					bq._MustQueries = new[] { lbq, rbq };

				if (lbq.BoolQueryDescriptor.CanJoinMustNot() && rbq.BoolQueryDescriptor.CanJoinMustNot())
					bq._MustNotQueries = lbq.MergeMustNotQueries(rbq);
				return q;
			}
			if (lbq.BoolQueryDescriptor != null)
			{
				if (lbq.BoolQueryDescriptor._HasOnlyMustNot())
				{
					bq._MustNotQueries = lbq.BoolQueryDescriptor._MustNotQueries;
					bq._MustQueries = new[] { rbq };
					return q;
				}

				else if (lbq.BoolQueryDescriptor.CanJoinMust())
					bq._MustQueries = lbq.MergeMustQueries(rbq);
				else
					bq._MustQueries = new[] { lbq, rbq };

				if (lbq.BoolQueryDescriptor.CanJoinMustNot())
					bq._MustNotQueries = lbq.MergeMustNotQueries(rbq);
				return q;
			}

			if (rbq.BoolQueryDescriptor._HasOnlyMustNot())
			{
				bq._MustNotQueries = rbq.BoolQueryDescriptor._MustNotQueries;
				bq._MustQueries = new[] { lbq };
				return q;
			}

			if (rbq.BoolQueryDescriptor.CanJoinMust())
				bq._MustQueries = rbq.MergeMustQueries(lbq);
			else
				bq._MustQueries = new[] { rbq, lbq };

			if (rbq.BoolQueryDescriptor.CanJoinMustNot())
				bq._MustNotQueries = rbq.MergeMustNotQueries(lbq);
			return q;

		}

		public static BaseQuery operator |(BaseQuery lbq, BaseQuery rbq)
		{
			if (lbq == null)
				return rbq;
			if (rbq == null)
				return lbq;

			if (lbq.IsConditionless && rbq.IsConditionless)
				return new BaseQuery() { IsConditionless = true };
			else if (lbq.IsConditionless)
				return rbq;
			else if (rbq.IsConditionless)
				return lbq;

			var q = new BaseQuery();
			var bq = new BoolBaseQueryDescriptor();
			bq._ShouldQueries = new[] { lbq, rbq };
			q.BoolQueryDescriptor = bq;

			if (lbq.BoolQueryDescriptor == null && rbq.BoolQueryDescriptor == null)
			{
				bq._ShouldQueries = lbq.MergeShouldQueries(rbq);
				return q;
			}
			else if (lbq.BoolQueryDescriptor != null && rbq.BoolQueryDescriptor == null)
			{
				JoinShouldOnSide(lbq, rbq, bq);
			}
			else if (rbq.BoolQueryDescriptor != null && lbq.BoolQueryDescriptor == null)
			{
				JoinShouldOnSide(rbq, lbq, bq);
			}
			else
			{
				if (lbq.BoolQueryDescriptor.CanJoinShould() && rbq.BoolQueryDescriptor.CanJoinShould())
					bq._ShouldQueries = lbq.MergeShouldQueries(rbq);
				else
					bq._ShouldQueries = new[] { lbq, rbq };
			}



			return q;
		}

		public static BaseQuery operator !(BaseQuery lbq)
		{
			if (lbq == null || lbq.IsConditionless)
				return new BaseQuery { IsConditionless = true };

			var q = new BaseQuery();
			var bq = new BoolBaseQueryDescriptor();
			bq._MustNotQueries = new[] { lbq };

			q.BoolQueryDescriptor = bq;
			return q;
		}

		public static bool operator false(BaseQuery a)
		{
			return false;
		}

		public static bool operator true(BaseQuery a)
		{
			return false;
		}

		private static void JoinShouldOnSide(BaseQuery lbq, BaseQuery rbq, BoolBaseQueryDescriptor bq)
		{
			bq._ShouldQueries = lbq.MergeShouldQueries(rbq);
		}
	}
}
