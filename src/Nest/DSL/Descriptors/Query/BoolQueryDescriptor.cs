using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	internal static class BoolBaseQueryDescriptorExtensions
	{
		internal static bool CanJoinMust(this BoolBaseQueryDescriptor bq)
		{
			return bq == null || (bq != null && bq._CanJoinMust());
		}
		internal static bool CanJoinMustNot(this BoolBaseQueryDescriptor bq)
		{
			return bq == null || (bq != null && bq._CanJoinMustNot());
		}
		internal static bool CanJoinShould(this BoolBaseQueryDescriptor bq)
		{
			return bq == null || (bq != null && bq._CanJoinShould());
		}
		internal static IEnumerable<BaseQuery> MergeMustQueries(this BaseQuery lbq, BaseQuery rbq)
		{
			var lBoolDescriptor = lbq.BoolQueryDescriptor;
			var lHasMustQueries = lBoolDescriptor != null &&
				lBoolDescriptor._MustQueries.HasAny();

			var rBoolDescriptor = rbq.BoolQueryDescriptor;
			var rHasMustQueries = rBoolDescriptor != null &&
				rBoolDescriptor._MustQueries.HasAny();

			var lq = lHasMustQueries
			? lBoolDescriptor._MustQueries
			: new[] { lbq };
			var rq = rHasMustQueries ? rBoolDescriptor._MustQueries : new[] { rbq };

			return lq.Concat(rq);
		}
		internal static IEnumerable<BaseQuery> MergeShouldQueries(this BaseQuery lbq, BaseQuery rbq)
		{
			var lBoolDescriptor = lbq.BoolQueryDescriptor;
			var lHasShouldQueries = lBoolDescriptor != null &&
				lBoolDescriptor._ShouldQueries.HasAny();

			var rBoolDescriptor = rbq.BoolQueryDescriptor;
			var rHasShouldQueries = rBoolDescriptor != null &&
				rBoolDescriptor._ShouldQueries.HasAny();


			var lq = lHasShouldQueries ? lBoolDescriptor._ShouldQueries : new[] { lbq };
			var rq = rHasShouldQueries ? rBoolDescriptor._ShouldQueries : new[] { rbq };

			return lq.Concat(rq);
		}
		internal static IEnumerable<BaseQuery> MergeMustNotQueries(this BaseQuery lbq, BaseQuery rbq)
		{
			var lBoolDescriptor = lbq.BoolQueryDescriptor;
			var lHasMustNotQueries = lBoolDescriptor != null &&
				lBoolDescriptor._MustNotQueries.HasAny();

			var rBoolDescriptor = rbq.BoolQueryDescriptor;
			var rHasMustNotQueries = rBoolDescriptor != null &&
				rBoolDescriptor._MustNotQueries.HasAny();


			var lq = lHasMustNotQueries ? lBoolDescriptor._MustNotQueries : Enumerable.Empty<BaseQuery>();
			var rq = rHasMustNotQueries ? rBoolDescriptor._MustNotQueries : Enumerable.Empty<BaseQuery>();
			if (!lq.HasAny() && !rq.HasAny())
				return null;

			return lq.Concat(rq);
		}
	}


	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolBaseQueryDescriptor
	{
		[JsonProperty("must")]
		internal IEnumerable<BaseQuery> _MustQueries { get; set; }

		[JsonProperty("must_not")]
		internal IEnumerable<BaseQuery> _MustNotQueries { get; set; }

		[JsonProperty("should")]
		internal IEnumerable<BaseQuery> _ShouldQueries { get; set; }

		internal bool _HasOnlyMustNot()
		{
			return _MustNotQueries.HasAny() && !_ShouldQueries.HasAny() && !_MustQueries.HasAny();
		}

		internal bool _CanJoinMust()
		{
			return !_ShouldQueries.HasAny();
		}
		internal bool _CanJoinShould()
		{
			return (_ShouldQueries.HasAny() && !_MustQueries.HasAny() && !_MustNotQueries.HasAny())
				|| !_ShouldQueries.HasAny();
		}
		internal bool _CanJoinMustNot()
		{
			return !_ShouldQueries.HasAny();
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class BoolQueryDescriptor<T> : BoolBaseQueryDescriptor, IQuery where T : class
	{
		[JsonProperty("disable_coord")]
		internal bool _DisableCoord { get; set; }

		public BoolQueryDescriptor<T> DisableCoord()
		{
			this._DisableCoord = true;
			return this;
		}

		[JsonProperty("minimum_number_should_match")]
		internal int? _MinimumNumberShouldMatches { get; set; }

		[JsonProperty("boost")]
		internal double? _Boost { get; set; }

		internal bool IsConditionless
		{
			get
			{
				if (!this._MustQueries.HasAny() && !this._ShouldQueries.HasAny() && !this._MustNotQueries.HasAny())
					return true;
				return (this._MustNotQueries.HasAny() && this._MustNotQueries.All(q => q.IsConditionless))
					|| (this._ShouldQueries.HasAny() && this._ShouldQueries.All(q => q.IsConditionless))
					|| (this._MustQueries.HasAny() && this._MustQueries.All(q => q.IsConditionless));
			}

		}

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumNumberShouldMatch(int minimumShouldMatches)
		{
			this._MinimumNumberShouldMatches = minimumShouldMatches;
			return this;
		}
		public BoolQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
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
			this._MustQueries = descriptors.HasAny() ? descriptors : null;
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
			this._MustQueries = descriptors.HasAny() ? descriptors : null;
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
			this._MustNotQueries = descriptors.HasAny() ? descriptors : null;
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
			this._MustNotQueries = descriptors.HasAny() ? descriptors : null;
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
			this._ShouldQueries = descriptors.HasAny() ? descriptors : null;
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
			this._ShouldQueries = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
