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
		
		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }

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
		public IInnerHits InnerHits { get; set; }
	}

	public class NestedFilterDescriptor<T> : FilterBase, INestedFilter where T : class
	{
		private INestedFilter Self { get { return this; } }

		NestedScore? INestedFilter.Score { get; set; }

		IFilterContainer INestedFilter.Filter { get; set; }

		IQueryContainer INestedFilter.Query { get; set; }

		PropertyPathMarker INestedFilter.Path { get; set; }

		bool? INestedFilter.Join { get; set; }

		IInnerHits INestedFilter.InnerHits { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return (Self.Query == null || Self.Query.IsConditionless)
                    && (Self.Filter == null || Self.Filter.IsConditionless);
			}
		}

		public NestedFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var q = new FilterDescriptor<T>();
			Self.Filter = filterSelector(q);
			return this;
		}

		public NestedFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}

		public NestedFilterDescriptor<T> Score(NestedScore score)
		{
			Self.Score = score;
			return this;
		}
		
		public NestedFilterDescriptor<T> Path(string path)
		{
			Self.Path = path;
			return this;
		}
		
		public NestedFilterDescriptor<T> Join(bool join = true)
		{
			Self.Join = join;
			return this;
		}
		
		public NestedFilterDescriptor<T> Path(Expression<Func<T, object>> objectPath)
		{
			Self.Path = objectPath;
			return this;
		}

		public NestedFilterDescriptor<T> InnerHits()
		{
			Self.InnerHits = new InnerHits();
			return this;
		}

		public NestedFilterDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> innerHitsSelector)
		{
			if (innerHitsSelector == null) return this;
			Self.InnerHits = innerHitsSelector(new InnerHitsDescriptor<T>());
			return this;
		}

	}
}
