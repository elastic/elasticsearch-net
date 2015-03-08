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

		[JsonProperty("query")]
		IQueryContainer Query { get; set; }

		[JsonProperty("filter")]
		IFilterContainer Filter { get; set; }
	}
	
	public class HasChildFilter : PlainFilter, IHasChildFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.HasChild = this;
		}

		public TypeNameMarker Type { get; set; }
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
	}

	public class HasChildFilterDescriptor<T> : FilterBase, IHasChildFilter where T : class
	{
		bool IFilter.IsConditionless
		{
			get
			{
				var hf = ((IHasChildFilter)this);
				if (hf.Type.IsNullOrEmpty())
					return true;

				if (hf.Query == null && hf.Filter == null)
					return true;
				if (hf.Filter == null && hf.Query != null)
					return hf.Query.IsConditionless;
				if (hf.Filter != null && hf.Query == null)
					return hf.Filter.IsConditionless;
				return hf.Query.IsConditionless && hf.Filter.IsConditionless;
			}
		}

		TypeNameMarker IHasChildFilter.Type { get; set; }

		IQueryContainer IHasChildFilter.Query { get; set; }

		IFilterContainer IHasChildFilter.Filter { get; set; }

		public HasChildFilterDescriptor()
		{
			((IHasChildFilter)this).Type = TypeNameMarker.Create<T>();
		}

		public HasChildFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasChildFilter)this).Query = querySelector(q);
			return this;
		}

		public HasChildFilterDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			var f = new FilterDescriptor<T>();
			((IHasChildFilter) this).Filter = filterSelector(f);
			return this;
		}

		public HasChildFilterDescriptor<T> Type(string type)
		{
			((IHasChildFilter)this).Type = type;
			return this;
		}
	}
}
