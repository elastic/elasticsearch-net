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
	public class IndicesOptionalPathDescriptorBase<TIndicesOptionalPathDescriptor, K> 
		where TIndicesOptionalPathDescriptor : IndicesOptionalPathDescriptorBase<TIndicesOptionalPathDescriptor, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		
		internal bool? _AllIndices { get; set; }

		public TIndicesOptionalPathDescriptor AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (TIndicesOptionalPathDescriptor)this;
		}
			
		public TIndicesOptionalPathDescriptor Index(string index)
		{
			return this.Indices(index);
		}
	
		public TIndicesOptionalPathDescriptor Index<T>() where T : class
		{
			return this.Indices(typeof(T));
		}
			
		public TIndicesOptionalPathDescriptor Indices(params string[] indices)
		{
			this._Indices = indices.Cast<IndexNameMarker>();
			return (TIndicesOptionalPathDescriptor)this;
		}

		public TIndicesOptionalPathDescriptor Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Cast<IndexNameMarker>();
			return (TIndicesOptionalPathDescriptor)this;
		}

		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings)
			where K : FluentQueryString<K>, new()
		{
			if (!this._AllIndices.HasValue && this._Indices == null)
				throw new DslException("UpdateSettings missing Index() or explicit OnAllIndices()");

			string index = null;
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
