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
	public class PercolateDescriptor<T> where T : class
	{
		protected readonly TypeNameResolver typeNameResolver;

		public PercolateDescriptor()
		{
			this.typeNameResolver = new TypeNameResolver();
		}

		internal string _Index { get; set; }
		internal string _Type { get; set; }
		internal string _Id { get; set; }
		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "doc")]
		internal T _Document { get; set; }

		/// <summary>
		/// Explicitly specify an index, otherwise the default index for T is used.
		/// </summary>
		public PercolateDescriptor<T> Index(string index)
		{
			this._Index = index;
			return this;
		}
		
		/// <summary>
		/// Explicitly specify an type, otherwise the default type for T is used.
		/// </summary>
		public PercolateDescriptor<T> Type(string type)
		{
			this._Type = type;
			return this;
		}

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateDescriptor<T> Object(T @object)
		{
			this._Document = @object;
			return this;
		}

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public PercolateDescriptor<T> Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryDescriptor<T>());
			this._Query = d;
			return this;
		}
	}
}
