using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IQuery
	{
		/// <summary>
		/// The _name of the query. this allows you to retrieve for each document what part of the query it matched on
		/// </summary>
		[JsonProperty(PropertyName = "_name")]
		string Name { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonIgnore]
		bool Conditionless { get; }
	}
	
	public abstract class QueryBase : IQuery
	{
		public string Name { get; set; }
		public double? Boost { get; set; }
		bool IQuery.Conditionless => this.Conditionless;
		protected abstract bool Conditionless { get; }

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(QueryBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(QueryBase a) => false;

		public static QueryBase operator &(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l,r) => l && r);

		public static QueryBase operator |(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l,r) => l || r);

		public static QueryBase operator !(QueryBase query) => query == null || ((IQuery)query).Conditionless
			? null
			: new BoolQuery { MustNot = new QueryContainer[] {query}};

		public static QueryBase operator +(QueryBase query) => query == null || ((IQuery)query).Conditionless
			? null
			: new BoolQuery { Filter = new QueryContainer[] {query}};

		private static QueryBase Combine(QueryBase leftQuery, QueryBase rightQuery, Func<QueryContainer, QueryContainer, QueryContainer> combine)
		{
			QueryBase q;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftQuery, rightQuery, out q))
				return q;

			IQueryContainer container = combine(leftQuery, rightQuery);
			var query = container.Bool;
			return new BoolQuery
			{
				Must = query.Must,
				MustNot = query.MustNot,
				Should = query.Should,
				Filter = query.Filter,
			};
		}

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryBase leftQuery, QueryBase rightQuery, out QueryBase query)
		{
			var combined = new [] {leftQuery, rightQuery};
			var any = combined.Any(bf => bf == null || ((IQuery) bf).Conditionless); 
			query = any ?  combined.FirstOrDefault(bf => bf != null && !((IQuery)bf).Conditionless) : null;
			return any;
		}

		public static implicit operator QueryContainer(QueryBase query) => 
			query == null || query.Conditionless ? null : new QueryContainer(query);

		internal abstract void WrapInContainer(IQueryContainer container);
	}
}