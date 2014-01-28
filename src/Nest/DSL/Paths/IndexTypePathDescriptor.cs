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
	///	/{index}/{type}
	/// </pre>
	/// Where neither parameter is optional
	/// </summary>
	public class IndexTypePathDescriptor<P, K> 
		where P : IndexTypePathDescriptor<P, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		
		public P Index<T>() where T : class
		{
			this._Index = typeof(T);
			return (P)this;
		}
			
		public P Index(string index)
		{
			this._Index = index;
			return (P)this;
		}

		public P Index(Type indexType)
		{
			this._Index = indexType;
			return (P)this;
		}
		
		public P Type<T>() where T : class
		{
			this._Type = typeof(T);
			return (P)this;
		}
			
		public P Type(string type)
		{
			this._Type = type;
			return (P)this;
		}

		public P Type(Type type)
		{
			this._Type = type;
			return (P)this;
		}
		
		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Index == null)
				throw new DslException("Index() not specified");
			if (this._Type == null)
				throw new DslException("Type() not specified");

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var type = new ElasticInferrer(settings).TypeName(this._Type); 
			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
				Type = type
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
