using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FilteredQueryDescriptor<object>>))]
	public interface IFilteredQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Filter { get; set; }
	}

	public class FilteredQuery : Query, IFilteredQuery
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

	public class FilteredQueryDescriptor<T> : IFilteredQuery where T : class
	{
		private IFilteredQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => FilteredQuery.IsConditionless(this);
		IQueryContainer IFilteredQuery.Query { get; set; }
		IQueryContainer IFilteredQuery.Filter { get; set; }

		public FilteredQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public FilteredQueryDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var query = new QueryDescriptor<T>();
			var q = querySelector(query);

			Self.Query = q;
			return this;
		}

		public FilteredQueryDescriptor<T> Filter(Func<QueryDescriptor<T>, QueryContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new QueryDescriptor<T>();
			var f = filterSelector(filter);

			Self.Filter = f;
			return this;
		}
	}
}
