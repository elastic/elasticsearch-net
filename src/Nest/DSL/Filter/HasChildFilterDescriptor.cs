using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<HasChildFilterDescriptor<object>>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IHasChildFilter : IFilterBase
	{
		[JsonProperty("type")]
		TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		string _Scope { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor _QueryDescriptor { get; set; }
	}

	public class HasChildFilterDescriptor<T> : FilterBase, IHasChildFilter where T : class
	{
		bool IFilterBase.IsConditionless
		{
			get
			{
				var hf = ((IHasChildFilter)this);
				return hf._QueryDescriptor == null || hf._QueryDescriptor.IsConditionless || hf._Type.IsNullOrEmpty();
			}
		}

		TypeNameMarker IHasChildFilter._Type { get; set; }

		string IHasChildFilter._Scope { get; set;}
		
		IQueryDescriptor IHasChildFilter._QueryDescriptor { get; set; }

		public HasChildFilterDescriptor()
		{
			((IHasChildFilter)this)._Type = TypeNameMarker.Create<T>();
		}

		public HasChildFilterDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasChildFilter)this)._QueryDescriptor = querySelector(q);
			return this;
		}
		
		public HasChildFilterDescriptor<T> Scope(string scope)
		{
			((IHasChildFilter)this)._Scope = scope;
			return this;
		}

		public HasChildFilterDescriptor<T> Type(string type)
		{
			((IHasChildFilter)this)._Type = type;
			return this;
		}
	}
}
