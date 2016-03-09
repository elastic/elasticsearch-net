using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndicesQueryDescriptor<object>>))]
	public interface IIndicesQuery : IQuery
	{
		[JsonProperty("indices")]
		[JsonConverter(typeof(IndicesJsonConverter))]
		Indices Indices { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("no_match_query")]
		[JsonConverter(typeof(NoMatchQueryJsonConverter))]
		QueryContainer NoMatchQuery { get; set; }
	}

	public class NoMatchQueryContainer : QueryContainer
	{
		public NoMatchShortcut? Shortcut { get; set; }

		public static implicit operator NoMatchQueryContainer(NoMatchShortcut shortcut) => new NoMatchQueryContainer { Shortcut = shortcut };
	}

	public class IndicesQuery : QueryBase, IIndicesQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public QueryContainer Query { get; set; }
		public QueryContainer NoMatchQuery { get; set; }
		public Indices Indices { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Indices = this;
		internal static bool IsConditionless(IIndicesQuery q) => 
			q.Indices == null || (q.NoMatchQuery.IsConditionless() && q.Query.IsConditionless());
	}

	public class IndicesQueryDescriptor<T> 
		: QueryDescriptorBase<IndicesQueryDescriptor<T>, IIndicesQuery> 
		, IIndicesQuery where T : class
	{
		protected override bool Conditionless => IndicesQuery.IsConditionless(this);
		QueryContainer IIndicesQuery.Query { get; set; }
		QueryContainer IIndicesQuery.NoMatchQuery { get; set; }
		Indices IIndicesQuery.Indices { get; set; }

		public IndicesQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Query = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public IndicesQueryDescriptor<T> Query<TOther>(Func<QueryContainerDescriptor<TOther>, QueryContainer> selector) where TOther : class => 
			Assign(a => a.Query = selector?.Invoke(new QueryContainerDescriptor<TOther>()));

		public IndicesQueryDescriptor<T> NoMatchQuery(NoMatchShortcut shortcut) =>
			Assign(a => a.NoMatchQuery = new NoMatchQueryContainer { Shortcut = shortcut });

		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.NoMatchQuery = selector?.Invoke(new QueryContainerDescriptor<T>()));

		public IndicesQueryDescriptor<T> NoMatchQuery<TOther>(Func<QueryContainerDescriptor<TOther>, QueryContainer> selector) where TOther : class => 
			Assign(a => a.NoMatchQuery = selector?.Invoke(new QueryContainerDescriptor<TOther>()));

		public IndicesQueryDescriptor<T> Indices(Indices indices) => Assign(a => a.Indices = indices);
		public IndicesQueryDescriptor<T> Indices(params IndexName[] indices) => Assign(a => a.Indices = indices);
		public IndicesQueryDescriptor<T> Indices(IEnumerable<IndexName> indices) => Assign(a => a.Indices = indices.ToArray());
	}
}
