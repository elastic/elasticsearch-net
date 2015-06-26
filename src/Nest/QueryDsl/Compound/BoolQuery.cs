using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<BoolQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IBoolQuery : IQuery
	{
		[JsonProperty("must",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> Must { get; set; }

		[JsonProperty("must_not",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> MustNot { get; set; }

		[JsonProperty("should",
			ItemConverterType = typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IEnumerable<IQueryContainer> Should { get; set; }

		[JsonProperty("minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty("disable_coord")]
		bool? DisableCoord { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }
	}

	public class BoolQuery : PlainQuery, IBoolQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				if (!Must.HasAny() && !Should.HasAny() && !MustNot.HasAny())
					return true;
				return (MustNot.HasAny() && MustNot.All(q => q.IsConditionless))
					|| (Should.HasAny() && Should.All(q => q.IsConditionless))
					|| (Must.HasAny() && Must.All(q => q.IsConditionless));
			}
		}
		public IEnumerable<IQueryContainer> Must { get; set; }
		public IEnumerable<IQueryContainer> MustNot { get; set; }
		public IEnumerable<IQueryContainer> Should { get; set; }
		public string MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }
		public double? Boost { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Bool = this;
		}
	}

	public class BoolQueryDescriptor<T> : IBoolQuery where T : class
	{
		private IBoolQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless
		{
			get
			{
				if (!Self.Must.HasAny() && !Self.Should.HasAny() && !Self.MustNot.HasAny())
					return true;
				return (Self.MustNot.HasAny() && Self.MustNot.All(q => q.IsConditionless))
					|| (Self.Should.HasAny() && Self.Should.All(q => q.IsConditionless))
					|| (Self.Must.HasAny() && Self.Must.All(q => q.IsConditionless));
			}
		}
		IEnumerable<IQueryContainer> IBoolQuery.Must { get; set; }
		IEnumerable<IQueryContainer> IBoolQuery.MustNot { get; set; }
		IEnumerable<IQueryContainer> IBoolQuery.Should { get; set; }
		string IBoolQuery.MinimumShouldMatch { get; set; }
		bool? IBoolQuery.DisableCoord { get; set; }
		double? IBoolQuery.Boost { get; set; }

		public BoolQueryDescriptor<T> DisableCoord()
		{
			Self.DisableCoord = true;
			return this;
		}

		public BoolQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}
		
		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatches)
		{
			Self.MinimumShouldMatch = minimumShouldMatches.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		
		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied. String overload where you can specify percentages
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatches)
		{
			Self.MinimumShouldMatch = minimumShouldMatches;
			return this;
		}

		/// <summary>
		/// Boost this results matching this query.
		/// </summary>
		/// <param name="boost"></param>
		public BoolQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params Func<QueryDescriptor<T>, QueryContainer>[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			Self.Must = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause(s) that must appear in matching documents
		/// </summary>
		public BoolQueryDescriptor<T> Must(params QueryContainer[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			Self.Must = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. 
		/// The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params Func<QueryDescriptor<T>, QueryContainer>[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			Self.MustNot = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		
		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document.
		///  The minimum number of should clauses to match can be set using minimum_should_match parameter.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MustNot(params QueryContainer[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			Self.MustNot = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		
		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params Func<QueryDescriptor<T>, QueryContainer>[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var selector in queries)
			{
				var filter = new QueryDescriptor<T>();
				var q = selector(filter);
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			Self.Should = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
		/// </summary>
		/// <param name="queries"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Should(params QueryContainer[] queries)
		{
			var descriptors = new List<QueryContainer>();
			foreach (var q in queries)
			{
				if (q.IsConditionless)
					continue;
				descriptors.Add(q);
			}
			Self.Should = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
