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
	///	/{index}
	/// </pre>
	/// index is not optional 
	/// </summary>
	public class IndexPathDescriptorBase<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor> 
		where TDescriptor : IndexPathDescriptorBase<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		
		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			this._Index = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Index(string indexType)
		{
			this._Index = indexType;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type indexType)
		{
			this._Index = indexType;
			return (TDescriptor)this;
		}

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			if (this._Index == null)
				throw new DslException("missing call to Index()");

			var index = new ElasticInferrer(settings).IndexName(this._Index); 
			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Index = index;
			return pathInfo;
		}

	}
}
