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

		internal bool _Strict { get; set; }

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
			
			//if neither side is already a bool query lets join them
			if (lbq.BoolQueryDescriptor == null && rbq.BoolQueryDescriptor == null)
			{
				bq._MustQueries = new[] { lbq, rbq };
				return q;
			}
			//if both sides of the operation are bool queries
			if (lbq.BoolQueryDescriptor != null && rbq.BoolQueryDescriptor != null)
			{
				if (lbq._Strict || rbq._Strict)
				{
					bq._MustQueries = new[] { lbq, rbq };
				return q;
				}

				//if both sides only define must_not's join them if they are not marked with strict()
				if (lbq.BoolQueryDescriptor._HasOnlyMustNot()
					&& rbq.BoolQueryDescriptor._HasOnlyMustNot()
				)
				{
					bq._MustQueries = null;
					bq._MustNotQueries = lbq.MergeMustNotQueries(rbq);
					return q;
				}

				//if both sides indicate its safe to merge must_not's with musts and neither is strict
				if (lbq.BoolQueryDescriptor.CanJoinMustNot()
					&& rbq.BoolQueryDescriptor.CanJoinMustNot()
					)
					bq._MustNotQueries = lbq.MergeMustNotQueries(rbq);

				//if left side has only must queries take only the musts from the right
				if (lbq.BoolQueryDescriptor._HasOnlyMustNot())
				{
					bq._MustNotQueries = lbq.BoolQueryDescriptor._MustNotQueries;
					bq._MustQueries = rbq.BoolQueryDescriptor._MustQueries;
				}
				//and visa versa
				else if (rbq.BoolQueryDescriptor._HasOnlyMustNot())
				{
					bq._MustQueries = lbq.BoolQueryDescriptor._MustQueries;
					bq._MustNotQueries = rbq.BoolQueryDescriptor._MustNotQueries;
				}
				//if both sides can join must queries and neither is strict: merge
				else if (lbq.BoolQueryDescriptor.CanJoinMust()
						 && rbq.BoolQueryDescriptor.CanJoinMust()
					)
					bq._MustQueries = lbq.MergeMustQueries(rbq);
				//otherwise create a new nested scope for both
				else
					bq._MustQueries = new[] {lbq, rbq};


			}
			//if the left side is already a bool query 
			else if (lbq.BoolQueryDescriptor != null)
			{
				//if the left  side has only must not queries assume its safe to join the right side
				if (lbq.BoolQueryDescriptor._HasOnlyMustNot())
				{
					bq._MustNotQueries = lbq.BoolQueryDescriptor._MustNotQueries;
					bq._MustQueries = new[] { rbq };
					return q;
				}

				//if left side indicates we can join must clause and lbq is not marked strict 
				//merge right side onto left side must clauses
				else if (lbq.BoolQueryDescriptor.CanJoinMust() && !lbq._Strict)
					bq._MustQueries = lbq.MergeMustQueries(rbq);
				else
					bq._MustQueries = new[] { lbq, rbq };


				if (lbq.BoolQueryDescriptor.CanJoinMustNot())
					bq._MustNotQueries = lbq.MergeMustNotQueries(rbq);
			}
			//if the right side is already a bool query
			else if (rbq.BoolQueryDescriptor != null)
			{
				if (rbq.BoolQueryDescriptor._HasOnlyMustNot())
				{
					bq._MustNotQueries = rbq.BoolQueryDescriptor._MustNotQueries;
					bq._MustQueries = new[] {lbq};
					return q;
				}

				if (rbq.BoolQueryDescriptor.CanJoinMust())
					bq._MustQueries = rbq.MergeMustQueries(lbq);
				else
					bq._MustQueries = new[] {rbq, lbq};

				if (rbq.BoolQueryDescriptor.CanJoinMustNot())
					bq._MustNotQueries = rbq.MergeMustNotQueries(lbq);
			}
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

			// if neither side is already a bool query we can merge the should
			if (lbq.BoolQueryDescriptor == null && rbq.BoolQueryDescriptor == null)
			{
				bq._ShouldQueries = lbq.MergeShouldQueries(rbq);
				return q;
			}
			//if left side is a bool query and the right side is not
			//merge the right side but only if the left bool is not strict
			//or defines a minimum number should match.
			else if (lbq.BoolQueryDescriptor != null && rbq.BoolQueryDescriptor == null 
				&& !lbq._Strict && lbq.BoolQueryDescriptor._MinimumNumberShouldMatches == null)
			{
				JoinShouldOnSide(lbq, rbq, bq);
			}
			//if right side is a bool query and the left side is not
			//merge the left side but only if the right bool is not strict
			//or defines a minimum number should match.
			else if (rbq.BoolQueryDescriptor != null && lbq.BoolQueryDescriptor == null
				&& !rbq._Strict && rbq.BoolQueryDescriptor._MinimumNumberShouldMatches == null)
			{
				JoinShouldOnSide(rbq, lbq, bq);
			}
			else
			{
				//if both are bool queries carrying only should clauses
				//and neither is marked with strict and neither has minimum_must_match set.
				if (!lbq._Strict && !rbq._Strict && 
					lbq.BoolQueryDescriptor.CanJoinShould() 
					&& rbq.BoolQueryDescriptor.CanJoinShould())
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
