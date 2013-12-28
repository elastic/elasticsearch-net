using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// http://www.elasticsearch.org/blog/terms-filter-lookup/
	/// </summary>
	public class TermsLookupFilterDescriptor : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return this._Type == null || this._Index == null || this._Id.IsNullOrEmpty() 
					|| this._Path.IsNullOrEmpty();
			}
		}

		[JsonProperty("id")]
		internal string _Id { get; set; }

		[JsonProperty("type")]
		internal TypeNameMarker _Type { get; set; }

		[JsonProperty("index")]
		internal IndexNameMarker _Index { get; set; }

		[JsonProperty("path")]
		internal string _Path { get; set; }

		[JsonProperty("routing")]
		internal string _Routing { get; set; }


		public TermsLookupFilterDescriptor Lookup<T>(string field, string id, string index = null, string type = null)
		{
			this._Path = field;
			this._Id = id;
			this._Type = type ?? new TypeNameMarker {Type = typeof (T)};
			this._Index = index ?? new IndexNameMarker {Type = typeof (T)};
			return this;
		}

		public TermsLookupFilterDescriptor Lookup<T>(Expression<Func<T, object>> field, string id, string index = null, string type = null)
		{
			var fieldName = new PropertyNameResolver().Resolve(field);
			return this.Lookup<T>(fieldName, id, index, type);
		}

		/// <summary>
		/// If not specified will use the default typename for the type specified on Lookup&lt;T&gt;
		/// </summary>
		public TermsLookupFilterDescriptor Type(string type)
		{
			this._Type = type;
			return this;
		}

		/// <summary>
		/// If not specified will use the default index for the type specified on Lookup&lt;T&gt;
		/// </summary>
		public TermsLookupFilterDescriptor Index(string index)
		{
			this._Index = index;
			return this;
		}

		/// <summary>
		/// A custom routing value to be used when retrieving the external terms doc.
		/// </summary>
		public TermsLookupFilterDescriptor Routing(string routing)
		{
			this._Routing = routing;
			return this;
		}
	}

}
