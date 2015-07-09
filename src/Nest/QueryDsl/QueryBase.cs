using Newtonsoft.Json;
using System;

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
		bool IQuery.Conditionless { get; }

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(QueryBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(QueryBase a) => false;

		public static QueryBase operator &(QueryBase leftQuery, QueryBase rightQuery)
		{
			var lc = new QueryContainer();
			leftQuery.WrapInContainer(lc);
			var rc = new QueryContainer();
			rightQuery.WrapInContainer(rc);
			var query = ((lc && rc) as IQueryContainer).Bool;
			return new BoolQuery()
			{
				Must = query.Must,
				MustNot = query.MustNot,
				Should = query.Should
			};
		}

		public QueryContainer ToContainer()
		{
			return ToContainer(this);
		}

		public static QueryContainer ToContainer(QueryBase query, QueryContainer queryContainer = null)
		{
			if (query == null) return null;
			var c = queryContainer ?? new QueryContainer();
			IQueryContainer fc = c;
			query.WrapInContainer(c);
			return c;
		}

		public static implicit operator QueryContainer(QueryBase query)
		{
			if (query == null) return null;
			var c = new QueryContainer();
			query.WrapInContainer(c);
			return c;
		}

		protected abstract void WrapInContainer(IQueryContainer container);
	}
}