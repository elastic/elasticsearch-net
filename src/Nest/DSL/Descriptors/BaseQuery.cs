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

		public static BaseQuery operator &(BaseQuery lbq, BaseQuery rbq)
		{
			var q = new BaseQuery();
			var bq = new BoolBaseQueryDescriptor();

			bq._MustQueries = new[] { lbq, rbq };

			if (lbq.BoolQueryDescriptor.CanJoinMust()
				&& rbq.BoolQueryDescriptor.CanJoinMust())
			{
				bq._MustQueries = lbq.MergeMustQueries(rbq);
			}

			q.BoolQueryDescriptor = bq;
			return q;
		}

		public static BaseQuery operator |(BaseQuery lbq, BaseQuery rbq)
		{
			var q = new BaseQuery();
			var bq = new BoolBaseQueryDescriptor();
			bq._ShouldQueries = new[] { lbq, rbq };

			if (lbq.BoolQueryDescriptor.CanJoinShould()
				&& rbq.BoolQueryDescriptor.CanJoinShould())
			{
				bq._ShouldQueries = lbq.MergeShouldQueries(rbq);
			}

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

	}
}
