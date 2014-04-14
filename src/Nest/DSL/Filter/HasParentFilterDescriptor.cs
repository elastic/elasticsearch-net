using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class HasParentFilterDescriptor<T> : FilterBase where T : class
	{
		public HasParentFilterDescriptor()
		{
			this._Type = TypeNameMarker.Create<T>();
		}
		internal override bool IsConditionless
		{
			get
			{
				return this._QueryDescriptor == null || this._QueryDescriptor.IsConditionless || this._Type.IsNullOrEmpty();
			}
		}

		[JsonProperty("type")]
		internal TypeNameMarker _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set;}
		
		[JsonProperty("query")]
		internal IQueryDescriptor _QueryDescriptor { get; set; }

		public HasParentFilterDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			this._QueryDescriptor = querySelector(q);
			return this;
		}

		public HasParentFilterDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
		public HasParentFilterDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}
	}
}
