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
	public class IndexTypePathTypedDescriptor<TDescriptor, TParameter, T> : BasePathDescriptor<TDescriptor>
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
		
		internal virtual ElasticsearchPathInfo<TParameter> ToPathInfo(IConnectionSettingsValues settings, TParameter queryString)
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Index == null)
				this._Index = inferrer.IndexName<T>();
			if (this._Type == null)
				this._Type = inferrer.TypeName<T>();

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var type = new ElasticInferrer(settings).TypeName(this._Type); 
			var pathInfo = new ElasticsearchPathInfo<TParameter>()
			{
				Index = index,
				Type = type
			};
			pathInfo.RequestParameters = queryString ?? new TParameter();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
