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
	public class IndexOptionalPathDescriptorBase<P, K> 
		where P : IndexOptionalPathDescriptorBase<P, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		
		internal bool? _AllIndices { get; set; }

		public P AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (P)this;
		}
		
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

		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings)
			where K : FluentQueryString<K>, new()
		{
			if (!this._AllIndices.HasValue && this._Index == null)
				throw new DslException("UpdateSettings missing Index() or explicit OnAllIndices()");

			string index = null;
			if (!this._AllIndices.GetValueOrDefault(false))
				index = new ElasticInferrer(settings).IndexName(this._Index);

			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.QueryString = new K();
			return pathInfo;
		}

	}
}
