using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FilteredQueryDescriptor<object>>))]
	public interface IFilteredQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainerDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeJsonConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Filter { get; set; }
	}

	public class FilteredQuery : QueryBase, IFilteredQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public IQueryContainer Query { get; set; }
		public IQueryContainer Filter { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Filtered = this;

		internal static bool IsConditionless(IFilteredQuery q)
		{
			if (q.Query == null && q.Filter == null)
				return true;
			if (q.Filter == null && q.Query != null)
				return q.Query.IsConditionless;
			if (q.Filter != null && q.Query == null)
				return q.Filter.IsConditionless;
			return q.Query.IsConditionless && q.Filter.IsConditionless;
		}
	}

	public class FilteredQueryDescriptor<T> 
		: QueryDescriptorBase<FilteredQueryDescriptor<T>, IFilteredQuery>  
		, IFilteredQuery where T : class
	{
		bool IQuery.Conditionless => FilteredQuery.IsConditionless(this);
		IQueryContainer IFilteredQuery.Query { get; set; }
		IQueryContainer IFilteredQuery.Filter { get; set; }

		public FilteredQueryDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Query = selector(new QueryContainerDescriptor<T>()));

		public FilteredQueryDescriptor<T> Filter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) => 
			Assign(a => a.Filter = selector(new QueryContainerDescriptor<T>()));
	}
}
