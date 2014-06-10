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

	public class RegisterPercolatorDescriptor<T> : IndexNamePathDescriptor<RegisterPercolatorDescriptor<T>, IndexRequestParameters>
 		, IPathInfo<IndexRequestParameters> 
		where T : class
	{
		internal FluentDictionary<string, object> _RequestBody
		{
			get
			{
				var body = new FluentDictionary<string, object>(this._Metadata);
				body.Add("query", this._Query);
				return body;
			}
		}
		internal QueryContainer _Query { get; set; }

		internal FluentDictionary<string, object> _Metadata { get; set; } 
		
		/// <summary>
		/// Add metadata associated with this percolator query document
		/// </summary>
		public RegisterPercolatorDescriptor<T> AddMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			if (selector == null)
				return this;

			this._Metadata = selector(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// The query to perform the percolation
		/// </summary>
		public RegisterPercolatorDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryDescriptor<T>());
			this._Query = d;
			return this;
		}

		ElasticsearchPathInfo<IndexRequestParameters> IPathInfo<IndexRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			//registering a percolator in elasticsearch < 1.0 is actually indexing a document in a 
			//special _percolator index where the passed index is actually a type
			//the name is actually the id, we rectify that here

			var pathInfo = base.ToPathInfo(settings, new IndexRequestParameters());
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.Index = pathInfo.Index;
			pathInfo.Id = pathInfo.Name;
			pathInfo.Type = ".percolator";

			return pathInfo;
		}
	}
}
