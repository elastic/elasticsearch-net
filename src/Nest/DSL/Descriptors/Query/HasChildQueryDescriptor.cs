using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class HasChildQueryDescriptor<T> : IQuery where T : class
	{
		internal bool IsConditionless
		{
			get
			{
				return this._QueryDescriptor == null || this._QueryDescriptor.IsConditionless;
			}
		}

		public HasChildQueryDescriptor()
		{
			this._Type = new TypeNameResolver().GetTypeNameFor<T>();
		}
		[JsonProperty("type")]
		internal string _Type { get; set; }

		[JsonProperty("_scope")]
		internal string _Scope { get; set; }

		[JsonProperty("query")]
		internal BaseQuery _QueryDescriptor { get; set; }

		public HasChildQueryDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			var q = new QueryDescriptor<T>();
			this._QueryDescriptor = querySelector(q);
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
		[JsonProperty(PropertyName = "_cache")]
		internal bool? _Cache { get; set; }

		[JsonProperty(PropertyName = "_name")]
		internal string _Name { get; set; }
	}
}
