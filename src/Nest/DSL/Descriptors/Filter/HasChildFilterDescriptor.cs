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
	public class HasChildFilterDescriptor<T> : FilterBase where T : class
	{
		public HasChildFilterDescriptor()
		{
			this._Type = new TypeNameResolver().GetTypeNameFor<T>();
		}
		internal override bool IsConditionless
		{
			get
			{
				return this._QueryDescriptor == null || this._QueryDescriptor.IsConditionless || this._Type.IsNullOrEmpty();
			}
		}

		[JsonProperty("type")]
		internal string _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set;}
		
		[JsonProperty("query")]
		internal BaseQuery _QueryDescriptor { get; set; }

		public HasChildFilterDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			this._QueryDescriptor = querySelector(q);
			return this;
		}
		public HasChildFilterDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
		public HasChildFilterDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}
	}
}
