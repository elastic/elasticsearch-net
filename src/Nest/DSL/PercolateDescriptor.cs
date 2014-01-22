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
	public partial class PercolateDescriptor<T> : IndexTypePathTypedDescriptor<PercolateDescriptor<T>, PercolateQueryString, T> 
		, IPathInfo<PercolateQueryString> 
		where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal BaseQuery _Query { get; set; }

		[JsonProperty(PropertyName = "doc")]
		internal T _Document { get; set; }

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

		ElasticSearchPathInfo<PercolateQueryString> IPathInfo<PercolateQueryString>.ToPathInfo(IConnectionSettings settings)
		{
			var pathInfo = base.ToPathInfo<PercolateQueryString>(settings);
			//.NET does not like sending data using get so we use POST
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			return pathInfo;
		}
	}
}
