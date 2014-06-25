using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
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
	public abstract class IndexTypePathTypedDescriptor<TDescriptor, TParameter, T> : BasePathDescriptor<TDescriptor, TParameter>
		where TDescriptor : IndexTypePathTypedDescriptor<TDescriptor, TParameter, T>, new()
		where TParameter : FluentRequestParameters<TParameter>, new()
		where T : class
	{
		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		
		public TDescriptor Index<TAlternative>() 
		{
			this._Index = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Index(string index)
		{
			this._Index = index;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type indexType)
		{
			this._Index = indexType;
			return (TDescriptor)this;
		}
		
		public TDescriptor Type<TAlternative>() 
		{
			this._Type = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Type(string type)
		{
			this._Type = type;
			return (TDescriptor)this;
		}

		public TDescriptor Type(Type type)
		{
			this._Type = type;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameter> pathInfo)
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Index == null)
				this._Index = inferrer.IndexName<T>();
			if (this._Type == null)
				this._Type = inferrer.TypeName<T>();

			var index = inferrer.IndexName(this._Index); 
			var type = inferrer.TypeName(this._Type); 

			pathInfo.Index = index;
			pathInfo.Type = type;
		}

	}
}
