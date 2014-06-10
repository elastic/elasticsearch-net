using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	public partial class PercolateDescriptor<T,K> : IndexTypePathTypedDescriptor<PercolateDescriptor<T, K>, PercolateRequestParameters, T> 
		, IPathInfo<PercolateRequestParameters> 
		where T : class
		where K : class
	{
		[JsonProperty(PropertyName = "query")]
		internal QueryContainer _Query { get; set; }

		[JsonProperty(PropertyName = "doc")]
		internal K _Document { get; set; }

		/// <summary>
		/// The object to perculate
		/// </summary>
		public PercolateDescriptor<T, K> Object(K @object)
		{
			this._Document = @object;
			return this;
		}

		/// <summary>
		/// Optionally specify more search options such as facets, from/to etcetera.
		/// </summary>
		public PercolateDescriptor<T, K> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryDescriptor<T>());
			this._Query = d;
			return this;
		}

		ElasticsearchPathInfo<PercolateRequestParameters> IPathInfo<PercolateRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			//.NET does not like sending data using get so we use POST
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			return pathInfo;
		}
	}
}
