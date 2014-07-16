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
	}

	public class HasParentFilter : PlainFilter, IHasParentFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.HasParent = this;
		}

		public TypeNameMarker Type { get; set; }
		public IQueryContainer Query { get; set; }
	}

	public class HasParentFilterDescriptor<T> : FilterBase, IHasParentFilter where T : class
	{
		TypeNameMarker IHasParentFilter.Type { get; set; }

		IQueryContainer IHasParentFilter.Query { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				var pf = ((IHasParentFilter)this);
				return pf.Query == null 
					|| pf.Query.IsConditionless 
					|| pf.Type.IsNullOrEmpty();
			}
		}

		public HasParentFilterDescriptor()
		{
			((IHasParentFilter)this).Type = TypeNameMarker.Create<T>();
		}

		public HasParentFilterDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasParentFilter)this).Query = querySelector(q);
			return this;
		}

		public HasParentFilterDescriptor<T> Type(string type)
		{
			((IHasParentFilter)this).Type = type;
			return this;
		}
	}
}
