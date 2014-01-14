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
	public class IndexPathDescriptorBase<TIndexPathDescriptor, K> 
		where TIndexPathDescriptor : IndexPathDescriptorBase<TIndexPathDescriptor, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		
		public TIndexPathDescriptor Index<T>() where T : class
		{
			this._Index = typeof(T);
			return (TIndexPathDescriptor)this;
		}
			
		public TIndexPathDescriptor Index(string indexType)
		{
			this._Index = indexType;
			return (TIndexPathDescriptor)this;
		}

		public TIndexPathDescriptor Index(Type indexType)
		{
			this._Index = indexType;
			return (TIndexPathDescriptor)this;
		}

		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings)
			where K : FluentQueryString<K>, new()
		{
			if (this._Index == null)
				throw new DslException("IndexPathDescriptor missing Index()");

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.QueryString = new K();
			return pathInfo;
		}

	}
}
