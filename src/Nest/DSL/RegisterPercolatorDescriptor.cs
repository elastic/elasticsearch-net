using System;
using Elasticsearch.Net;

namespace Nest
{

	public class RegisterPercolatorDescriptor<T> : IndexNamePathDescriptor<RegisterPercolatorDescriptor<T>, IndexRequestParameters>
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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			//registering a percolator in elasticsearch < 1.0 is actually indexing a document in a 
			//special _percolator index where the passed index is actually a type
			//the name is actually the id, we rectify that here

			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.Index = pathInfo.Index;
			pathInfo.Id = pathInfo.Name;
			pathInfo.Type = ".percolator";
		}
	}
}
