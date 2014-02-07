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
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type <para>T</para>
	/// </summary>
	public class IndexTypePathTypedDescriptor<P, K, T> 
		where P : IndexTypePathTypedDescriptor<P, K, T>, new()
		where K : FluentQueryString<K>, new()
		where T : class
	{
		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		
		public P Index<T>() 
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
		
		public P Type<T>() 
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
		
		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Index == null)
				this._Index = inferrer.IndexName<T>();
			if (this._Type == null)
				this._Type = inferrer.TypeName<T>();

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var type = new ElasticInferrer(settings).TypeName(this._Type); 
			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
				Type = type
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
