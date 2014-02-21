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
	///	/{index}
	/// </pre>
	/// index is not optional 
	/// </summary>
	public class IndexPathDescriptorBase<P, K> 
		where P : IndexPathDescriptorBase<P, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		
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

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			if (this._Index == null)
				throw new DslException("missing call to Index()");

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
