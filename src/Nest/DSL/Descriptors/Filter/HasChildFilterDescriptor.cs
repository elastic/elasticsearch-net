using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class HasChildFilterDescriptor<T> : FilterBase where T : class
	{
		public HasChildFilterDescriptor()
		{
			this._Type = ElasticClient.GetTypeNameFor<T>();
		}

		[JsonProperty("type")]
		internal string _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set;}
		
		[JsonProperty("query")]
		internal QueryDescriptor<T> _QueryDescriptor { get; set;}

		public HasChildFilterDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
			this._QueryDescriptor = new QueryDescriptor<T>();
			querySelector(this._QueryDescriptor);
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
