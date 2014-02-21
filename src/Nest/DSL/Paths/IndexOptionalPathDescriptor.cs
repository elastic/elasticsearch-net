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
	///	/{index}
	/// </pre>
	/// index is optional but AllIndices() needs to be explicitly specified for it to be optional
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

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
			if (!this._AllIndices.HasValue && this._Index == null)
				this._Index = inferrer.DefaultIndex;

			string index = null;
			if (!this._AllIndices.GetValueOrDefault(false))
				index = inferrer.IndexName(this._Index);

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
