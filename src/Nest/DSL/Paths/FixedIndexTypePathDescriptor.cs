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
	/// {index} is optional and so is {type} and will NOT fallback to the defaults of <para>T</para>
	/// type can only be specified in conjuction with index.
	/// </summary>
	public class FixedIndexTypePathDescriptor<P, K> 
		where P : FixedIndexTypePathDescriptor<P, K> 
		where K : FluentQueryString<K>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }

		public P FixedPath(string index, string type = null)
		{
			this._Index = index;
			this._Type = type;
			return (P)this;
		}

		public P FixedPath(Type index, Type type = null)
		{
			this._Index = index;
			this._Type = type;
			return (P)this;
		}
		public P FixedPath<T>(bool fixateType = false)
		{
			this._Index = typeof (T);
			if (fixateType)
				this._Type = typeof(T);
			return (P) this;
		}
		public P FixedPath<TIndex,TType>()
		{
			this._Index = typeof (TIndex);
			this._Type = typeof(TType);
			return (P) this;
		}
		
		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);

			var index = inferrer.IndexName(this._Index); 
			var type = inferrer.TypeName(this._Type); 
		
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
