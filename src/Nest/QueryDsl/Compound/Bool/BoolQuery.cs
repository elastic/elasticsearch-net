using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoolQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoolQuery : IQuery
	{
		[JsonProperty("must")]
		IEnumerable<QueryContainer> Must { get; set; }

		[JsonProperty("must_not")]
		IEnumerable<QueryContainer> MustNot { get; set; }

		[JsonProperty("should")]
		IEnumerable<QueryContainer> Should { get; set; }

		[JsonProperty("filter")]
		IEnumerable<QueryContainer> Filter { get; set; }

		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty("disable_coord")]
		bool? DisableCoord { get; set; }
	}

	public class BoolQuery : QueryBase, IBoolQuery
	{
		public IEnumerable<QueryContainer> Must { get; set; }
		public IEnumerable<QueryContainer> MustNot { get; set; }
		public IEnumerable<QueryContainer> Should { get; set; }
		public IEnumerable<QueryContainer> Filter { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }

		internal override void WrapInContainer(IQueryContainer c) => c.Bool = this;

		protected override bool Conditionless => IsConditionless(this);
		internal static bool IsConditionless(IBoolQuery q)
		{
			if (!q.Must.HasAny() && !q.Should.HasAny() && !q.MustNot.HasAny() && !q.Filter.HasAny())
				return true;

			return (q.MustNot.HasAny() && q.MustNot.All(qq => qq.IsConditionless))
				|| (q.Should.HasAny() && q.Should.All(qq => qq.IsConditionless))
				|| (q.Must.HasAny() && q.Must.All(qq => qq.IsConditionless))
				|| (q.Filter.HasAny() && q.Filter.All(qq => qq.IsConditionless));
		}
	}

	public class BoolQueryDescriptor<T>
		: QueryDescriptorBase<BoolQueryDescriptor<T>, IBoolQuery>
		, IBoolQuery where T : class
	{
		protected override bool Conditionless => BoolQuery.IsConditionless(this);
		IEnumerable<QueryContainer> IBoolQuery.Must { get; set; }
		IEnumerable<QueryContainer> IBoolQuery.MustNot { get; set; }
		IEnumerable<QueryContainer> IBoolQuery.Should { get; set; }
		IEnumerable<QueryContainer> IBoolQuery.Filter { get; set; }
		MinimumShouldMatch IBoolQuery.MinimumShouldMatch { get; set; }
		bool? IBoolQuery.DisableCoord { get; set; }

		public BoolQueryDescriptor<T> DisableCoord() => Assign(a => a.DisableCoord = true);

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatches) => Assign(a => a.MinimumShouldMatch = minimumShouldMatches);

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Must = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Must = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params QueryContainer[] queries) => Assign(a => a.Must = queries.Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. 
		/// The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.MustNot = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. 
		/// The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.MustNot = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		///  The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params QueryContainer[] queries) => Assign(a => a.MustNot = queries.Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Should = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Should = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params QueryContainer[] queries) => Assign(a => a.Should = queries.Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Filter = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Filter = queries.Select(q => q?.InvokeQuery(new QueryContainerDescriptor<T>())).Where(q => !q.IsConditionless).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params QueryContainer[] queries) => Assign(a => a.Filter = queries.Where(q => !q.IsConditionless).ToListOrNullIfEmpty());
	}
}
