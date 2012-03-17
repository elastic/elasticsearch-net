using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization=MemberSerialization.OptIn)]
	public class HasChildQueryDescriptor<T> : FilterBase where T : class
	{
		public HasChildQueryDescriptor()
		{
			this._Type = ElasticClient.GetTypeNameFor<T>();
		}
		[JsonProperty("type")]
		internal string _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set;}
		
		[JsonProperty("query")]
		internal QueryDescriptor<T> _QueryDescriptor { get; set;}

		public HasChildQueryDescriptor<T> Query(Action<QueryDescriptor<T>> querySelector)
		{
			this._QueryDescriptor = new QueryDescriptor<T>();
			querySelector(this._QueryDescriptor);
			return this;
		}
		public HasChildQueryDescriptor<T> Scope(string scope)
		{
			this._Scope = scope;
			return this;
		}
		public HasChildQueryDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}
	}
}
