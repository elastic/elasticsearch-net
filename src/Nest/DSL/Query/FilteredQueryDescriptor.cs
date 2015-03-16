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
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }
	}

	public class FilteredQuery : PlainQuery, IFilteredQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Filtered = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get {return false;}}
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
	}

	public class FilteredQueryDescriptor<T> : IFilteredQuery where T : class
	{
		private IFilteredQuery Self { get { return this; } }

		IQueryContainer IFilteredQuery.Query { get; set; }

		IFilterContainer IFilteredQuery.Filter { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				if (Self.Query == null && Self.Filter == null)
					return true;
				if (Self.Filter == null && Self.Query != null)
					return Self.Query.IsConditionless;
				if (Self.Filter != null && Self.Query == null)
					return Self.Filter.IsConditionless;
				return Self.Query.IsConditionless && Self.Filter.IsConditionless;
			}
		}

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

		public FilteredQueryDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			filterSelector.ThrowIfNull("filterSelector");
			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);

			Self.Filter = f;
			return this;
		}
	}
}
