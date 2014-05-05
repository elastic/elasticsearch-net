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
	/// {index} is optional and so is {type} and will NOT fallback to the defaults of <para>T</para>
	/// type can only be specified in conjuction with index.
	/// </summary>
	public class FixedIndexTypePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : FixedIndexTypePathDescriptor<TDescriptor, TParameters> 
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		internal TypeNameMarker _Type { get; set; }

		public TDescriptor FixedPath(string index, string type = null)
		{
			this._Index = index;
			this._Type = type;
			return (TDescriptor)this;
		}

		public TDescriptor FixedPath(Type index, Type type = null)
		{
			this._Index = index;
			this._Type = type;
			return (TDescriptor)this;
		}
		public TDescriptor FixedPath<TAlternative>(bool fixateType = false)
		{
			this._Index = typeof (TAlternative);
			if (fixateType)
				this._Type = typeof(TAlternative);
			return (TDescriptor) this;
		}
		public TDescriptor FixedPath<TIndex,TType>()
		{
			this._Index = typeof (TIndex);
			this._Type = typeof(TType);
			return (TDescriptor) this;
		}
		
		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			var inferrer = new ElasticInferrer(settings);

			var index = inferrer.IndexName(this._Index); 
			var type = inferrer.TypeName(this._Type); 
		
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
