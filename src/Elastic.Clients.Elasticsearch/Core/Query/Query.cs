// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public abstract partial class SearchQuery
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
	public static bool operator false(SearchQuery a) => false;

	//always evaluate to false so that each side of && equation is evaluated
	public static bool operator true(SearchQuery a) => false;

	//public static QueryBase operator &(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l && r);

	//public static QueryBase operator |(QueryBase leftQuery, QueryBase rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l || r);

	//public static QueryBase operator !(QueryBase query) => query == null || !query.IsWritable
	//	? null
	//	: new BoolQuery { MustNot = new Query[] { query } };

	//public static QueryBase operator +(QueryBase query) => query == null || !query.IsWritable
	//	? null
	//	: new BoolQuery { Filter = new Query[] { query } };

	//private static QueryBase Combine(QueryBase leftQuery, QueryBase rightQuery, Func<Query, Query, Query> combine)
	//{
	//	if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftQuery, rightQuery, out var q))
	//		return q;

	//	IQuery container = combine(leftQuery, rightQuery);
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

	//public static implicit operator Query(QueryBase query) =>
	//	query == null ? null : new Query(query);

	//internal void WrapInContainer(IQuery container) => InternalWrapInContainer(container);

	////container.IsVerbatim = IsVerbatim;
	////container.IsStrict = IsStrict;

	//internal abstract void InternalWrapInContainer(IQuery container);
}
