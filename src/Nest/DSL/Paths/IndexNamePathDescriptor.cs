using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;

using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{name}
	/// </pre>
	/// neither parameter is optional 
	/// </summary>
	public class IndexNamePathDescriptor<P, K> : BasePathDescriptor<P>
		where P : IndexNamePathDescriptor<P, K>, new()
		where K : FluentRequestParameters<K>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		internal string _Name { get; set; }
		
		public P Index<T>() where T : class
		{
			this._Index = typeof(T);
			return (P)this;
		}
			
		public P Index(string indexType)
		{
			this._Index = indexType;
			return (P)this;
		}

		public P Index(Type indexType)
		{
			this._Index = indexType;
			return (P)this;
		}
		
		public P Name(string name)
		{
			this._Name = name;
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			if (this._Name == null)
				throw new DslException("missing Repository()");
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName(this._Index) ?? inferrer.DefaultIndex; 
			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
				Name = this._Name
			};
			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
