using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public class PercolatorDescriptor<T> : FluentDictionary<string, object> where T : class
	{
		internal string _Index { get; set; }
		internal string _Name { get; set; }
	
		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		/// <summary>
		/// Explicitly specify an index, otherwise the default index for T is used.
		/// </summary>
		public PercolatorDescriptor<T> Index(string index)
		{
			this._Index = index;
			return this;
		}
		
		/// <summary>
		/// Explicitly specify an type, otherwise the default type for T is used.
		/// </summary>
		public PercolatorDescriptor<T> Name(string name)
		{
			this._Name = name;
			return this;
		}
		/// <summary>
		/// Add metadata associated with this percolator query document
		/// </summary>
		public new PercolatorDescriptor<T> Add(string key, object value)
		{
			base.Add(key, value);
			return this;
		}

		//Remove metadata associated with this percolator query document
		public PercolatorDescriptor<T> Remove(string key)
		{
			base.Remove(key);
			return this;
		} 

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public PercolatorDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryDescriptor<T>());
			this._Query = d;
			base.Add("query", d);
			return this;
		}
	}
}
