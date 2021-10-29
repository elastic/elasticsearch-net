using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl
{
	// TODO: FieldNameQueryConvertor (see FieldNameQueryFormatter)
	public interface IFieldNameQuery
	{
		Field Field { get; set; }
	}

	public abstract class FieldNameQueryBase : QueryBase, IFieldNameQuery
	{
		[JsonIgnore]
		public Field Field { get; set; }
	}

	//public abstract class FieldNameQueryDescriptorBase<TDescriptor, TInterface>
	//	: QueryDescriptorBase<TDescriptor, TInterface>, IFieldNameQuery
	//	where TDescriptor : FieldNameQueryDescriptorBase<TDescriptor, TInterface>, TInterface
	//	where TInterface : class, IFieldNameQuery
	//{
	//	Field IFieldNameQuery.Field { get; set; }

	//	public TDescriptor Field(Field field) => Assign(field, (a, v) => a.Field = v);

	//	//public TDescriptor Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
	//	//	Assign(objectPath, (a, v) => a.Field = v);
	//}

	[ConvertAs(typeof(QueryBase))]
	public interface IQuery
	{
		/// <summary>
		///     Provides a boost to this query to influence its relevance score.
		///     For example, a query with a boost of 2 is twice as important as a query with a boost of 1,
		///     although the actual boost value that is applied undergoes normalization and internal optimization.
		/// </summary>
		//float? Boost { get; set; } // Was defined as a double before the code gen work

		///// <summary>
		/////     Whether the query is conditionless. A conditionless query is not serialized as part of the request
		/////     sent to Elasticsearch.
		///// </summary>
		//[JsonIgnore]
		//bool Conditionless { get; }

		///// <summary>
		/////     Whether the query should be treated as strict. A strict query will throw an exception when serialized
		/////     if it is <see cref="Conditionless" />.
		///// </summary>
		//[JsonIgnore]
		//bool IsStrict { get; set; }

		///// <summary>
		/////     Whether the query should be treated as verbatim. A verbatim query will be serialized as part of the request,
		/////     irrespective
		/////     of whether it is <see cref="Conditionless" /> or not.
		///// </summary>
		//[JsonIgnore]
		//bool IsVerbatim { get; set; }

		/// <summary>
		///     Whether the query should be treated as writable. Used when determining how to combine queries.
		/// </summary>
		[JsonIgnore]
		bool IsWritable { get; }

		/// <summary>
		///     The name of the query. Allows you to retrieve for each document what part of the query it matched on.
		/// </summary>
		//string QueryName { get; set; }
	}

	//public abstract class QueryDescriptorBase<TDescriptor, TInterface>
	//	: DescriptorBase<TDescriptor, TInterface>, IQuery
	//	where TDescriptor : QueryDescriptorBase<TDescriptor, TInterface>, TInterface
	//	where TInterface : class, IQuery
	//{
	//	///// <inheritdoc cref="IQuery.Conditionless"/>
	//	//protected abstract bool Conditionless { get; }

	//	//float? IQuery.Boost { get; set; }

	//	//bool IQuery.Conditionless => Conditionless;

	//	//bool IQuery.IsStrict { get; set; }

	//	//bool IQuery.IsVerbatim { get; set; }

	//	//bool IQuery.IsWritable => true; // Self.IsVerbatim || !Self.Conditionless;

	//	//string IQuery.QueryName { get; set; }

	//	///// <inheritdoc cref="IQuery.QueryName"/>
	//	//public TDescriptor QueryName(string name) => Assign(name, (a, v) => a.QueryName = v);

	//	///// <inheritdoc cref="IQuery.Boost"/>
	//	//public TDescriptor Boost(float? boost) => Assign(boost, (a, v) => a.Boost = v);

	//	///// <inheritdoc cref="IQuery.IsVerbatim"/>
	//	//public TDescriptor Verbatim(bool verbatim = true) => Assign(verbatim, (a, v) => a.IsVerbatim = v);

	//	///// <inheritdoc cref="IQuery.IsStrict"/>
	//	//public TDescriptor Strict(bool strict = true) => Assign(strict, (a, v) => a.IsStrict = v);
	//}

	//public abstract class QueryContainerVariantBase 

	public abstract partial class QueryBase : IQuery
	{
		[JsonIgnore]
		public bool IsWritable => throw new NotImplementedException();

		////protected abstract bool Conditionless { get; }
		//[JsonIgnore]
		//public bool IsStrict { get; set; }

		//[JsonIgnore]
		//public bool IsVerbatim { get; set; }

		//[JsonIgnore]
		//public bool IsWritable => true; //IsVerbatim || !Conditionless;

		//bool IQuery.Conditionless => Conditionless;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator false(QueryBase a) => false;

		//always evaluate to false so that each side of && equation is evaluated
		public static bool operator true(QueryBase a) => false;

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
