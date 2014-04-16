using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public interface IHasParentFilter : IFilterBase
	{
		[JsonProperty("type")]
		TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		string _Scope { get; set; }

		[JsonProperty("query")]
		IQueryDescriptor _QueryDescriptor { get; set; }
	}

	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class HasParentFilterDescriptor<T> : FilterBase, IHasParentFilter where T : class
	{
		TypeNameMarker IHasParentFilter._Type { get; set; }

		string IHasParentFilter._Scope { get; set;}
		
		IQueryDescriptor IHasParentFilter._QueryDescriptor { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				var pf = ((IHasParentFilter)this);
				return pf._QueryDescriptor == null 
					|| pf._QueryDescriptor.IsConditionless 
					|| pf._Type.IsNullOrEmpty();
			}
		}

		public HasParentFilterDescriptor()
		{
			((IHasParentFilter)this)._Type = TypeNameMarker.Create<T>();
		}

		public HasParentFilterDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			((IHasParentFilter)this)._QueryDescriptor = querySelector(q);
			return this;
		}

		public HasParentFilterDescriptor<T> Scope(string scope)
		{
			((IHasParentFilter)this)._Scope = scope;
			return this;
		}

		public HasParentFilterDescriptor<T> Type(string type)
		{
			((IHasParentFilter)this)._Type = type;
			return this;
		}
	}
}
