using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<HasChildFilterDescriptor<object>>))]
	public interface IHasChildFilter : IFilter
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }

		[JsonProperty("min_children")]
		int? MinChildren { get; set; }

		[JsonProperty("max_children")]
		int? MaxChildren { get; set; }

		[JsonProperty("query")]
		IQueryContainer Query { get; set; }

		[JsonProperty("filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(ReadAsTypeConverter<InnerHits>))]
		IInnerHits InnerHits { get; set; }

	}

	public class HasChildFilter : PlainFilter, IHasChildFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.HasChild = this;
		}

		public TypeNameMarker Type { get; set; }
		public int? MinChildren { get; set; }
		public int? MaxChildren { get; set; }
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
		public IInnerHits InnerHits { get; set; }
	}

	public class HasChildFilterDescriptor<T> : FilterBase, IHasChildFilter where T : class
	{
		private IHasChildFilter Self { get { return this; } }

		bool IFilter.IsConditionless
		{
			get
			{
				if (Self.Type.IsNullOrEmpty()) return true;
				if (Self.Query == null && Self.Filter == null) return true;
				if (Self.Filter == null && Self.Query != null) return Self.Query.IsConditionless;
				if (Self.Filter != null && Self.Query == null) return Self.Filter.IsConditionless;
				return Self.Query.IsConditionless && Self.Filter.IsConditionless;
			}
		}

		TypeNameMarker IHasChildFilter.Type { get; set; }

		int? IHasChildFilter.MinChildren { get; set; }

		int? IHasChildFilter.MaxChildren { get; set; }

		IQueryContainer IHasChildFilter.Query { get; set; }

		IFilterContainer IHasChildFilter.Filter { get; set; }

		IInnerHits IHasChildFilter.InnerHits { get; set; }

		public HasChildFilterDescriptor()
		{
			Self.Type = TypeNameMarker.Create<T>();
		}

		public HasChildFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			Self.Query = querySelector(q);
			return this;
		}

		public HasChildFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var f = new FilterDescriptor<T>();
			Self.Filter = filterSelector(f);
			return this;
		}

		public HasChildFilterDescriptor<T> Type(string type)
		{
			Self.Type = type;
			return this;
		}

		public HasChildFilterDescriptor<T> MinChildren(int minChildren)
		{
			Self.MinChildren = minChildren;
			return this;
		}

		public HasChildFilterDescriptor<T> MaxChildren(int maxChildren)
		{
			Self.MaxChildren = maxChildren;
			return this;
		}

		public HasChildFilterDescriptor<T> InnerHits()
		{
			Self.InnerHits = new InnerHits();
			return this;
		}

		public HasChildFilterDescriptor<T> InnerHits(Func<InnerHitsDescriptor<T>, IInnerHits> innerHitsSelector)
		{
			if (innerHitsSelector == null) return this;
			Self.InnerHits = innerHitsSelector(new InnerHitsDescriptor<T>());
			return this;
		}
	}
}
