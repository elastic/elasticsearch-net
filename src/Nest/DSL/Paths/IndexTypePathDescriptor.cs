using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
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
	/// Where neither parameter is optional
	/// </summary>
	public class IndexTypePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : IndexTypePathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }
		
		public TDescriptor Index<TAlternative>() where TAlternative : class
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
		
		public TDescriptor Type<TAlternative>() where TAlternative : class
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
		
		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Index == null)
				throw new DslException("Index() not specified");
			if (this._Type == null)
				throw new DslException("Type() not specified");

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var type = new ElasticInferrer(settings).TypeName(this._Type); 
			var pathInfo = new ElasticsearchPathInfo<TParameters>()
			{
				Index = index,
				Type = type
			};
			pathInfo.RequestParameters = queryString ?? new TParameters();
			pathInfo.RequestParameters.RequestConfiguration(r=>this._RequestConfiguration);
			return pathInfo;
		}

	}
}
