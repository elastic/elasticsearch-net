using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<IndicesQueryDescriptor<object>>))]
	public interface IIndicesQuery : IQuery
	{
		[JsonProperty("indices")]
		IEnumerable<IndexName> Indices { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("no_match_query")]
		[JsonConverter(typeof(NoMatchQueryJsonConverter))]
		IQueryContainer NoMatchQuery { get; set; }
	}

	public class NoMatchQueryContainer : QueryContainer, ICustomJson
	{
		public NoMatchShortcut? Shortcut { get; set; }

		object ICustomJson.GetCustomJson()
		{
			if (this.Shortcut.HasValue) return this.Shortcut;
			var f = ((IQueryContainer)this);
			if (f.RawQuery.IsNullOrEmpty()) return f;
			return new RawJson(f.RawQuery);
		}
	}

	public class IndicesQuery : QueryBase, IIndicesQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IQueryContainer Query { get; set; }
		public IQueryContainer NoMatchQuery { get; set; }
		public IEnumerable<IndexName> Indices { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Indices = this;
		internal static bool IsConditionless(IIndicesQuery q) => q.NoMatchQuery == null && q.Query == null;
	}

	public class IndicesQueryDescriptor<T> 
		: QueryDescriptorBase<IndicesQueryDescriptor<T>, IIndicesQuery> 
		, IIndicesQuery where T : class
	{
		bool IQuery.Conditionless => IndicesQuery.IsConditionless(this);
		IQueryContainer IIndicesQuery.Query { get; set; }
		IQueryContainer IIndicesQuery.NoMatchQuery { get; set; }
		IEnumerable<IndexName> IIndicesQuery.Indices { get; set; }

		public IndicesQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => Assign(a =>
		{
			var query = selector(new QueryContainerDescriptor<T>());
			if (query.IsConditionless)
				a.Query = query;
		});

		public IndicesQueryDescriptor<T> Query<K>(Func<QueryContainerDescriptor<K>, QueryContainer> selector) where K : class => Assign(a =>
		{
			var query = selector(new QueryContainerDescriptor<K>());
			if (query.IsConditionless)
				a.Query = query;
		});

		public IndicesQueryDescriptor<T> NoMatchQuery(NoMatchShortcut shortcut) =>
			Assign(a => a.NoMatchQuery = new NoMatchQueryContainer { Shortcut = shortcut });

		public IndicesQueryDescriptor<T> NoMatchQuery(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => Assign(a =>
		{
			var query = selector(new QueryContainerDescriptor<T>());
			if (query.IsConditionless)
				a.NoMatchQuery = query;
		});

		public IndicesQueryDescriptor<T> NoMatchQuery<K>(Func<QueryContainerDescriptor<K>, IQueryContainer> selector) where K : class => Assign(a =>
		{
			var query = selector(new QueryContainerDescriptor<K>());
			if (query.IsConditionless)
				a.NoMatchQuery = query;
		});

		public IndicesQueryDescriptor<T> Indices(IEnumerable<IndexName> indices) => Assign(a => a.Indices = indices);
	}
}
