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
	///	/{indices}
	/// </pre>
	/// {indices} is optional 
	/// </summary>
	public abstract class IndicesOptionalPathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : IndicesOptionalPathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		
		public TDescriptor Index(string index)
		{
			return this.Indices(index);
		}
	
		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			return this.Indices(typeof(TAlternative));
		}
			
		public TDescriptor Indices(params string[] indices)
		{
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}

		public TDescriptor Indices(params Type[] indicesTypes)
		{
			this._Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			var index = this._Indices == null ? null : string.Join(",", this._Indices.Select(i => new ElasticInferrer(settings).IndexName(i)));

			pathInfo.Index = index;
		}

	}
}
