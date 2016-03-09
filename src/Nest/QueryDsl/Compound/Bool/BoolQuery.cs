using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<BoolQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoolQuery : IQuery
	{
		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		[JsonProperty("must", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> Must { get; set; }

		/// <summary>
		/// The clause (query) must not appear in the matching documents.
		/// Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		[JsonProperty("must_not", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> MustNot { get; set; }

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch"/>.
		/// </summary>
		[JsonProperty("should", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> Should { get; set; }

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		[JsonProperty("filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
		IEnumerable<QueryContainer> Filter { get; set; }

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// Specifies if the coordination factor for the query should be disabled.
		/// The coordination factor is used to reward documents that contain a higher
		/// percentage of the query terms. The more query terms that appear in the document,
		/// the greater the chances that the document is a good match for the query.
		/// </summary>
		[JsonProperty("disable_coord")]
		bool? DisableCoord { get; set; }

		bool Locked { get; }
	}

	public class BoolQuery : QueryBase, IBoolQuery
	{
		internal static bool Locked(IBoolQuery q) => !q.Name.IsNullOrEmpty() || q.Boost.HasValue || q.DisableCoord.HasValue || q.MinimumShouldMatch != null;
		bool IBoolQuery.Locked => BoolQuery.Locked(this);

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public IEnumerable<QueryContainer> Must { get; set; }

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		public IEnumerable<QueryContainer> MustNot { get; set; }

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch"/>.
		/// </summary>
		public IEnumerable<QueryContainer> Should { get; set; }

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		public IEnumerable<QueryContainer> Filter { get; set; }

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		public MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// Specifies if the coordination factor for the query should be disabled.
		/// The coordination factor is used to reward documents that contain a higher
		/// percentage of the query terms. The more query terms that appear in the document,
		/// the greater the chances that the document is a good match for the query.
		/// </summary>
		public bool? DisableCoord { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Bool = this;

		protected override bool Conditionless => IsConditionless(this);
		internal static bool IsConditionless(IBoolQuery q)
		{
			var musts = q.Must == null || q.Must.All(qq => qq.IsConditionless());
			if (!musts) return false;

			var shoulds = q.Should == null || q.Should.All(qq => qq.IsConditionless());
			if (!shoulds) return false;

			var filters = q.Filter == null || q.Filter.All(qq => qq.IsConditionless());
			if (!filters) return false;

			var mustNots = q.MustNot == null || q.MustNot.All(qq => qq.IsConditionless());

			return mustNots;
		}
	}

	public class BoolQueryDescriptor<T>
		: QueryDescriptorBase<BoolQueryDescriptor<T>, IBoolQuery>
		, IBoolQuery where T : class
	{
		bool IBoolQuery.Locked => BoolQuery.Locked(this);

		protected override bool Conditionless => BoolQuery.IsConditionless(this);
		IEnumerable<QueryContainer> IBoolQuery.Must { get; set; }
		IEnumerable<QueryContainer> IBoolQuery.MustNot { get; set; }
		IEnumerable<QueryContainer> IBoolQuery.Should { get; set; }
		IEnumerable<QueryContainer> IBoolQuery.Filter { get; set; }
		MinimumShouldMatch IBoolQuery.MinimumShouldMatch { get; set; }
		bool? IBoolQuery.DisableCoord { get; set; }

		/// <summary>
		/// Specifies if the coordination factor for the query should be disabled.
		/// The coordination factor is used to reward documents that contain a higher
		/// percentage of the query terms. The more query terms that appear in the document,
		/// the greater the chances that the document is a good match for the query.
		/// </summary>
		/// <returns></returns>
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
			Assign(a => a.Must = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Must = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params QueryContainer[] queries) =>
			Assign(a => a.Must = queries.ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.MustNot = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.MustNot = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params QueryContainer[] queries) =>
			Assign(a => a.MustNot = queries.ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch"/>.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Should = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch"/>.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Should = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		/// The minimum number of should clauses to match can be set using <see cref="MinimumShouldMatch"/>.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params QueryContainer[] queries) =>
			Assign(a => a.Should = queries.ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) =>
			Assign(a => a.Filter = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(IEnumerable<Func<QueryContainerDescriptor<T>, QueryContainer>> queries) =>
			Assign(a => a.Filter = queries.Select(q => q?.Invoke(new QueryContainerDescriptor<T>())).ToListOrNullIfEmpty());

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params QueryContainer[] queries) =>
			Assign(a => a.Filter = queries.ToListOrNullIfEmpty());
	}
}
