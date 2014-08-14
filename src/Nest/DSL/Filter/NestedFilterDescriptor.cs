using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<NestedFilterDescriptor<object>>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface INestedFilter : IFilter
	{
		[JsonProperty("score_mode"), JsonConverter(typeof (StringEnumConverter))]
		NestedScore? Score { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterDescriptor<object>>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("path")]
		PropertyPathMarker Path { get; set; }

		[JsonProperty("join")]
		bool? Join { get; set; }
	}

	public class NestedFilter : PlainFilter, INestedFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.Nested = this;
		}

		public NestedScore? Score { get; set; }
		public IFilterContainer Filter { get; set; }
		public IQueryContainer Query { get; set; }
		public PropertyPathMarker Path { get; set; }
		public bool? Join { get; set; }
	}

	public class NestedFilterDescriptor<T> : FilterBase, INestedFilter where T : class
	{
		NestedScore? INestedFilter.Score { get; set; }

		IFilterContainer INestedFilter.Filter { get; set; }

		IQueryContainer INestedFilter.Query { get; set; }

		PropertyPathMarker INestedFilter.Path { get; set; }

		bool? INestedFilter.Join { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return (((INestedFilter)this).Query == null 
					|| ((INestedFilter)this).Query.IsConditionless)
                    && (((INestedFilter)this).Filter == null
                    || ((INestedFilter)this).Filter.IsConditionless)
                    ;
			}
		}

		public NestedFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var q = new FilterDescriptor<T>();
			((INestedFilter)this).Filter = filterSelector(q);
			return this;
		}

		public NestedFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((INestedFilter)this).Query = querySelector(q);
			return this;
		}

		public NestedFilterDescriptor<T> Score(NestedScore score)
		{
			((INestedFilter)this).Score = score;
			return this;
		}
		
		public NestedFilterDescriptor<T> Path(string path)
		{
			((INestedFilter)this).Path = path;
			return this;
		}
		
		public NestedFilterDescriptor<T> Join(bool join = true)
		{
			((INestedFilter)this).Join = join;
			return this;
		}
		
		public NestedFilterDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			((INestedFilter)this).Path = objectPath;
			return this;
		}
	}
}
