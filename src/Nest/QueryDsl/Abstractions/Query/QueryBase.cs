// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IQuery
	{
		/// <summary>
		/// Provides a boost to this query to influence its relevance score.
		/// For example, a query with a boost of 2 is twice as important as a query with a boost of 1,
		/// although the actual boost value that is applied undergoes normalization and internal optimization.
		/// </summary>
		/// <remarks>
		/// Setting a boost for an <see cref="ISpanOrQuery"/> query will throw a parsing exception on the server.
		/// </remarks>
		[DataMember(Name = "boost")]
		double? Boost { get; set; }

		/// <summary>
		/// Whether the query is conditionless. A conditionless query is not serialized as part of the request
		/// sent to Elasticsearch.
		/// </summary>
		[IgnoreDataMember]
		bool Conditionless { get; }

		/// <summary>
		/// Whether the query should be treated as strict. A strict query will throw an exception when serialized
		/// if it is <see cref="Conditionless" />.
		/// </summary>
		[IgnoreDataMember]
		bool IsStrict { get; set; }

		/// <summary>
		/// Whether the query should be treated as verbatim. A verbatim query will be serialized as part of the request,
		/// irrespective
		/// of whether it is <see cref="Conditionless" /> or not.
		/// </summary>
		[IgnoreDataMember]
		bool IsVerbatim { get; set; }

		/// <summary>
		/// Whether the query should be treated as writable. Used when determining how to combine queries.
		/// </summary>
		[IgnoreDataMember]
		bool IsWritable { get; }

		/// <summary>
		/// The name of the query. Allows you to retrieve for each document what part of the query it matched on.
		/// </summary>
		[DataMember(Name = "_name")]
		string Name { get; set; }
	}

	public abstract class QueryBase : IQuery
	{
		public double? Boost { get; set; }
		public bool IsStrict { get; set; }
		public bool IsVerbatim { get; set; }
		public bool IsWritable => IsVerbatim || !Conditionless;

		/// <inheritdoc />
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
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftQuery, rightQuery, out var q))
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
			query = null;
			if (leftQuery == null && rightQuery == null) return true;

			var leftWritable = leftQuery?.IsWritable ?? false;
			var rightWritable = rightQuery?.IsWritable ?? false;
			if (leftWritable && rightWritable) return false;
			if (!leftWritable && !rightWritable) return true;

			query = leftWritable ? leftQuery : rightQuery;
			return true;
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
