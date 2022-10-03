// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	public abstract partial class Query
	{
		//[JsonIgnore]
		//public bool IsWritable => throw new NotImplementedException();

		////protected abstract bool Conditionless { get; }
		//[JsonIgnore]
		//public bool IsStrict { get; set; }

		//[JsonIgnore]
		//public bool IsVerbatim { get; set; }

		//[JsonIgnore]
		//public bool IsWritable => true; //IsVerbatim || !Conditionless;

		//bool IQuery.Conditionless => Conditionless;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(Query a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(Query a) => false;

		//public static QueryBase operator &(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l && r);

		//public static QueryBase operator |(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l || r);

		//public static QueryBase operator !(QueryBase query) => query == null || !query.IsWritable
		//	? null
		//	: new BoolQuery { MustNot = new QueryContainer[] { query } };

		//public static QueryBase operator +(QueryBase query) => query == null || !query.IsWritable
		//	? null
		//	: new BoolQuery { Filter = new QueryContainer[] { query } };

		//private static QueryBase Combine(QueryBase leftQuery, QueryBase rightQuery, Func<QueryContainer, QueryContainer, QueryContainer> combine)
		//{
		//	if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftQuery, rightQuery, out var q))
		//		return q;

		//	IQueryContainer container = combine(leftQuery, rightQuery);
		//	var query = container.Bool;
		//	return new BoolQuery
		//	{
		//		Must = query.Must,
		//		MustNot = query.MustNot,
		//		Should = query.Should,
		//		Filter = query.Filter,
		//	};
		//}

		//private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryBase leftQuery, QueryBase rightQuery,
		//	out QueryBase query)
		//{
		//	query = null;
		//	if (leftQuery == null && rightQuery == null)
		//		return true;

		//	var leftWritable = leftQuery?.IsWritable ?? false;
		//	var rightWritable = rightQuery?.IsWritable ?? false;
		//	if (leftWritable && rightWritable)
		//		return false;
		//	if (!leftWritable && !rightWritable)
		//		return true;

		//	query = leftWritable ? leftQuery : rightQuery;
		//	return true;
		//}

		//public static implicit operator QueryContainer(QueryBase query) =>
		//	query == null ? null : new QueryContainer(query);

		//internal void WrapInContainer(IQueryContainer container) => InternalWrapInContainer(container);

		////container.IsVerbatim = IsVerbatim;
		////container.IsStrict = IsStrict;

		//internal abstract void InternalWrapInContainer(IQueryContainer container);
	}
}
