using System;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IQuery
	{
		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonIgnore]
		bool Conditionless { get; }

		[JsonIgnore]
		bool IsStrict { get; set; }

		[JsonIgnore]
		bool IsVerbatim { get; set; }

		[JsonIgnore]
		bool IsWritable { get; }

		/// <summary>
		/// The _name of the query. this allows you to retrieve for each document what part of the query it matched on
		/// </summary>
		[JsonProperty(PropertyName = "_name")]
		string Name { get; set; }
	}

	public abstract class QueryBase : IQuery
	{
		public double? Boost { get; set; }
		public bool IsStrict { get; set; }
		public bool IsVerbatim { get; set; }
		public bool IsWritable => IsVerbatim || !Conditionless;
		public string Name { get; set; }
		protected abstract bool Conditionless { get; }

		bool IQuery.Conditionless => Conditionless;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(QueryBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(QueryBase a) => false;

		public static QueryBase operator &(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l && r);

		public static QueryBase operator |(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l || r);

		public static QueryBase operator !(QueryBase query) => query == null || !query.IsWritable
			? null
			: new BoolQuery { MustNot = new QueryContainer[] { query } };

		public static QueryBase operator +(QueryBase query) => query == null || !query.IsWritable
			? null
			: new BoolQuery { Filter = new QueryContainer[] { query } };

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
			var combined = new[] { leftQuery, rightQuery };
			var anyEmpty = combined.Any(q => q == null || !q.IsWritable);
			query = anyEmpty ? combined.FirstOrDefault(q => q != null && q.IsWritable) : null;
			return anyEmpty;
		}

		public static implicit operator QueryContainer(QueryBase query) =>
			query == null ? null : new QueryContainer(query);

		internal void WrapInContainer(IQueryContainer container)
		{
			container.IsVerbatim = IsVerbatim;
			container.IsStrict = IsStrict;
			InternalWrapInContainer(container);
		}

		internal abstract void InternalWrapInContainer(IQueryContainer container);
	}
}
