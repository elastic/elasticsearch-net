using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<BoolBaseQueryDescriptor>))]
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
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Bool = this;
		}

		public string Name { get; set; }

		bool IQuery.IsConditionless { get { return false; } }

		public IEnumerable<IQueryContainer> Must { get; set; }
		public IEnumerable<IQueryContainer> MustNot { get; set; }
		public IEnumerable<IQueryContainer> Should { get; set; }
		public string MinimumShouldMatch { get; set; }
		public bool? DisableCoord { get; set; }
		public double? Boost { get; set; }
	}

	//TODO 2.0 why is this separate from the descriptor
	public class BoolBaseQueryDescriptor : IBoolQuery
	{
		[JsonProperty("must")]
		IEnumerable<IQueryContainer> IBoolQuery.Must { get; set; }

		[JsonProperty("must_not")]
		IEnumerable<IQueryContainer> IBoolQuery.MustNot { get; set; }

		[JsonProperty("should")]
		IEnumerable<IQueryContainer> IBoolQuery.Should { get; set; }

		[JsonProperty("minimum_should_match")]
		string IBoolQuery.MinimumShouldMatch { get; set; }
		
		[JsonProperty("disable_coord")]
		bool? IBoolQuery.DisableCoord { get; set; }
		
		[JsonProperty("boost")]
		double? IBoolQuery.Boost { get; set; }

		[JsonProperty("_name")]
		string IQuery.Name { get; set; }

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

	public class BoolQueryDescriptor<T> : BoolBaseQueryDescriptor where T : class
	{
		public BoolQueryDescriptor<T> DisableCoord()
		{
			((IBoolQuery)this).DisableCoord = true;
			return this;
		}

		/// <summary>
		/// Specifies the name of the query.
		/// </summary>
		/// <returns></returns>
		public BoolQueryDescriptor<T> Name(string name)
		{
			((IBoolQuery) this).Name = name;
			return this;
		}
		
		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied.
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatches)
		{
			((IBoolQuery)this).MinimumShouldMatch = minimumShouldMatches.ToString(CultureInfo.InvariantCulture);
			return this;
		}
		
		/// <summary>
		/// Specifies a minimum number of the optional BooleanClauses which must be satisfied. String overload where you can specify percentages
		/// </summary>
		/// <param name="minimumShouldMatches"></param>
		/// <returns></returns>
		public BoolQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatches)
		{
			((IBoolQuery)this).MinimumShouldMatch = minimumShouldMatches;
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
			((IBoolQuery)this).Must = descriptors.HasAny() ? descriptors : null;
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
			((IBoolQuery)this).Must = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
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
			((IBoolQuery)this).MustNot = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		
		/// <summary>
		/// The clause (query) must not appear in the matching documents. Note that it is not possible to search on documents that only consists of a must_not clauses.
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
			((IBoolQuery)this).MustNot = descriptors.HasAny() ? descriptors : null;
			return this;
		}
		
		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. 
		/// The minimum number of should clauses to match can be set using minimum_should_match parameter.
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
			((IBoolQuery)this).Should = descriptors.HasAny() ? descriptors : null;
			return this;
		}

		/// <summary>
		/// The clause (query) should appear in the matching document. A boolean query with no must clauses, one or more should clauses must match a document. 
		/// The minimum number of should clauses to match can be set using minimum_should_match parameter.
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
			((IBoolQuery)this).Should = descriptors.HasAny() ? descriptors : null;
			return this;
		}
	}
}
