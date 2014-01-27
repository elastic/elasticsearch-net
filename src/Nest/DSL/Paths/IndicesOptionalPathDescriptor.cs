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
	/// {indices} is optional but AllIndices() needs to be explicitly called.
	/// </summary>
	public class IndicesOptionalPathDescriptorBase<P, K> 
		where P : IndicesOptionalPathDescriptorBase<P, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		
		internal bool? _AllIndices { get; set; }

		public P AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (P)this;
		}
			
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
			this._Indices = indices.Cast<IndexNameMarker>();
			return (P)this;
		}

		public P Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Cast<IndexNameMarker>();
			return (P)this;
		}

		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings)
			where K : FluentQueryString<K>, new()
		{
			if (!this._AllIndices.HasValue && this._Indices == null)
				throw new DslException("missing Index() or explicit OnAllIndices()");

			string index = "_all";
			if (!this._AllIndices.GetValueOrDefault(false))
				index = string.Join(",", this._Indices.Select(i => new ElasticInferrer(settings).IndexName(i)));

			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.QueryString = new K();
			return pathInfo;
		}

	}
}
