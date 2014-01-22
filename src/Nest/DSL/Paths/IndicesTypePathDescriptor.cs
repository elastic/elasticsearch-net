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
	/// Provides a base for descriptor that need to form a url path
	/// in the form of /{index}/[anythinghere] where index is optional.
	/// </summary>
	public class IndicesTypePathDescriptor<P, K, T> 
		where P : IndicesTypePathDescriptor<P, K, T> 
		where K : FluentQueryString<K>, new()
		where T : class
	{
		internal bool? _AllIndices { get; set; }
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal TypeNameMarker _Type { get; set; }
		
		public P AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (P)this;
		}
		public P Index<T>()
		{
			return this.Indices(typeof (T));
		}
			
		public P Indices(params string[] indices)
		{
			this._Indices = indices.Cast<IndexNameMarker>();
			return (P)this;
		}

		public P Indices(params Type[] indices)
		{
			this._Indices = indices.Cast<IndexNameMarker>();
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
		
		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings)
			where K : FluentQueryString<K>, new()
		{
			if (!this._AllIndices.HasValue && this._Indices == null)
				throw new DslException("missing Index() or explicit OnAllIndices()");
			var inferrer = new ElasticInferrer(settings);
			if (this._Type == null)
				this._Type = inferrer.TypeName<T>();

			var index = !this._Indices.HasAny()
				? inferrer.IndexName<T>()
				: string.Join(",", this._Indices.Select(inferrer.IndexName));
			if (this._AllIndices.GetValueOrDefault(false))
				index = "_all";

			var type = new ElasticInferrer(settings).TypeName(this._Type); 
			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
				Type = type
			};
			pathInfo.QueryString = new K();
			return pathInfo;
		}

	}
}
