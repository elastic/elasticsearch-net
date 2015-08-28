using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<HasParentFilterDescriptor<object>>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IHasParentFilter : IFilter
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryDescriptor<object>>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterDescriptor<object>>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }

	}

	public class HasParentFilter : PlainFilter, IHasParentFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.HasParent = this;
		}

		public TypeNameMarker Type { get; set; }
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
		public IInnerHits InnerHits { get; set; }
	}

	public class HasParentFilterDescriptor<T> : FilterBase, IHasParentFilter where T : class
	{
		private IHasParentFilter Self { get { return this; } }

		TypeNameMarker IHasParentFilter.Type { get; set; }

		IQueryContainer IHasParentFilter.Query { get; set; }

		IFilterContainer IHasParentFilter.Filter { get; set; }

		IInnerHits IHasParentFilter.InnerHits { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return Self.Type.IsNullOrEmpty()
					|| ((Self.Query == null || Self.Query.IsConditionless)
					&& (Self.Filter == null || Self.Filter.IsConditionless));
			}
		}

		public HasParentFilterDescriptor()
		{
			Self.Type = TypeNameMarker.Create<T>();
		}

		public HasParentFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}

		public HasParentFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var f = new FilterDescriptor<T>();
			Self.Filter = filterSelector(f);
			return this;
		}

		public HasParentFilterDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		public HasParentFilterDescriptor<T> InnerHits()
		{
			Self.InnerHits = new InnerHits();
			return this;
		}

		public HasParentFilterDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> innerHitsSelector)
		{
			if (innerHitsSelector == null) return this;
			Self.InnerHits = innerHitsSelector(new InnerHitsDescriptor<T>());
			return this;
		}
	}
}
