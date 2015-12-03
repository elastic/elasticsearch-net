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
		[JsonProperty("must",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> Must { get; set; }

		[JsonProperty("must_not",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> MustNot { get; set; }

		[JsonProperty("should",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> Should { get; set; }

		[JsonProperty("filter",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> Filter { get; set; }

 		[JsonProperty("minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty("disable_coord")]
		bool? DisableCoord { get; set; }
	}

	public class BoolQuery : QueryBase, IBoolQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IEnumerable<IQueryContainer> Must { get; set; }
		public IEnumerable<IQueryContainer> MustNot { get; set; }
		public IEnumerable<IQueryContainer> Should { get; set; }
		public IEnumerable<IQueryContainer> Filter { get; set; }
		public string MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Bool = this;

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
		bool IQuery.Conditionless => BoolQuery.IsConditionless(this);
		IEnumerable<IQueryContainer> IBoolQuery.Must { get; set; }
		IEnumerable<IQueryContainer> IBoolQuery.MustNot { get; set; }
		IEnumerable<IQueryContainer> IBoolQuery.Should { get; set; }
		IEnumerable<IQueryContainer> IBoolQuery.Filter { get; set; }
		string IBoolQuery.MinimumShouldMatch { get; set; }
		bool? IBoolQuery.DisableCoord { get; set; }

		public BoolQueryDescriptor<T> DisableCoord() => Assign(a => a.DisableCoord = true);

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatches) => 
			Assign(a => a.MinimumShouldMatch = minimumShouldMatches.ToString(CultureInfo.InvariantCulture));

		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied. String overload where you can specify percentages
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatches) => 
			Assign(a => a.MinimumShouldMatch = minimumShouldMatches);

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) => Assign(a => 
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryContainerDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.Must = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params QueryContainer[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.Must = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. 
		/// The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryContainerDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.MustNot = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		///  The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params QueryContainer[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.MustNot = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryContainerDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.Should = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params QueryContainer[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.Should = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params Func<QueryContainerDescriptor<T>, QueryContainer>[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryContainerDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.Filter = descriptors.HasAny() ? descriptors : null;
		});

		/// <summary>
		/// The clause (query) which is to be used as a filter (in filter context).
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Filter(params QueryContainer[] queries) => Assign(a =>
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			a.Filter = descriptors.HasAny() ? descriptors : null;
		});

	}
}
