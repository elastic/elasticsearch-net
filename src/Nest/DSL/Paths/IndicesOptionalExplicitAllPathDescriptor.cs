using System;
using System.Collections.Generic;
using System.Configuration;
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
	/// {indices} is optional but AllIndices() needs to be explicitly called.
	/// </summary>
	public abstract class IndicesOptionalExplicitAllPathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : IndicesOptionalExplicitAllPathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		
		internal bool? _AllIndices { get; set; }

		public TDescriptor AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (TDescriptor)this;
		}
			
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
			var inferrer = new ElasticInferrer(settings);
			if (!this._AllIndices.HasValue && this._Indices == null)
				this._Indices = new[] {(IndexNameMarker)inferrer.DefaultIndex};

			string index = "_all";
			if (!this._AllIndices.GetValueOrDefault(false))
				index = string.Join(",", this._Indices.Select(inferrer.IndexName));

			pathInfo.Index = index;
		}

	}
}
