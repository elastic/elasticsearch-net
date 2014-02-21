using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;

using Nest.Resolvers;

namespace Nest
{
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}
	/// </pre>
	/// {indices} is optional 
	/// </summary>
	public class IndicesOptionalPathDescriptor<P, K> 
		where P : IndicesOptionalPathDescriptor<P, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		
		public P Index(string index)
		{
			return this.Indices(index);
		}
	
		public P Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}
			
		public P Indices(params string[] indices)
		{
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (P)this;
		}

		public P Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			var index = this._Indices == null ? null : string.Join(",", this._Indices.Select(i => new ElasticInferrer(settings).IndexName(i)));

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
