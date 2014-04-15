using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	internal static class BoolBaseQueryDescriptorExtensions
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

		internal static IEnumerable<IQueryDescriptor> MergeShouldQueries(this IQueryDescriptor lbq, IQueryDescriptor rbq)
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


	public interface IBoolQuery
	{
		[JsonProperty("must")]
		IEnumerable<IQueryDescriptor> Must { get; set; }

		[JsonProperty("must_not")]
		IEnumerable<IQueryDescriptor> MustNot { get; set; }

		[JsonProperty("should")]
		IEnumerable<IQueryDescriptor> Should { get; set; }

		[JsonProperty("minimum_number_should_match")]
		object MinimumNumberShouldMatches { get; set; }

		[JsonProperty("disable_coord")]
		bool? DisableCoord { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolBaseQueryDescriptor : IBoolQuery, IQuery
	{
		[JsonProperty("must")]
		IEnumerable<IQueryDescriptor> IBoolQuery.Must { get; set; }

		[JsonProperty("must_not")]
		IEnumerable<IQueryDescriptor> IBoolQuery.MustNot { get; set; }

		[JsonProperty("should")]
		IEnumerable<IQueryDescriptor> IBoolQuery.Should { get; set; }

		[JsonProperty("minimum_number_should_match")]
		object IBoolQuery.MinimumNumberShouldMatches { get; set; }
		
		[JsonProperty("disable_coord")]
		bool? IBoolQuery.DisableCoord { get; set; }
		
		[JsonProperty("boost")]
		double? IBoolQuery.Boost { get; set; }
		
		bool IQuery.IsConditionless
		{
			get
			{
				var bq = ((IBoolQuery)this);

				if (!bq.Must.HasAny() && !bq.Should.HasAny() && !bq.MustNot.HasAny())
					return true;
				return (bq.MustNot.HasAny() && bq.MustNot.All(q => q.IsConditionless))
					|| (bq.Should.HasAny() && bq.Should.All(q => q.IsConditionless))
					|| (bq.Must.HasAny() && bq.Must.All(q => q.IsConditionless));
			}
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolQueryDescriptor<T> : BoolBaseQueryDescriptor where T : class
	{

		public BoolQueryDescriptor<T> DisableCoord()
		{
			((IBoolQuery)this).DisableCoord = true;
			return this;
		}

		

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumNumberShouldMatch(int minimumShouldMatches)
		{
			((IBoolQuery)this).MinimumNumberShouldMatches = minimumShouldMatches;
			return this;
		}
		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied. String overload where you can specify percentages
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumNumberShouldMatch(string minimumShouldMatches)
		{
			((IBoolQuery)this).MinimumNumberShouldMatches = minimumShouldMatches;
			return this;
		}

		/// <summary>
		/// Boost this results matching this query.
		/// </summary>
		/// <param name="boost"></param>
		public BoolQueryDescriptor<T> Boost(double boost)
		{
			((IBoolQuery)this).Boost = boost;
			return this;
		}

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params Func<QueryDescriptor<T>, BaseQuery>[] queries)
		{
			var descriptors = new List<BaseQuery>();
			foreach (var selector in queries)
			{
				var filter = new QueryDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IBoolQuery)this).Must = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params BaseQuery[] queries)
		{
			var descriptors = new List<BaseQuery>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IBoolQuery)this).Must = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. The minimum number of should clauses to match can be set using minimum_number_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Func<QueryDescriptor<T>, BaseQuery>[] queries)
		{
			var descriptors = new List<BaseQuery>();
			foreach (var selector in queries)
			{
				var filter = new QueryDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IBoolQuery)this).MustNot = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. The minimum number of should clauses to match can be set using minimum_number_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params BaseQuery[] queries)
		{
			var descriptors = new List<BaseQuery>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IBoolQuery)this).MustNot = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Func<QueryDescriptor<T>, BaseQuery>[] queries)
		{
			var descriptors = new List<BaseQuery>();
			foreach (var selector in queries)
			{
				var filter = new QueryDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IBoolQuery)this).Should = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params BaseQuery[] queries)
		{
			var descriptors = new List<BaseQuery>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			((IBoolQuery)this).Should = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
