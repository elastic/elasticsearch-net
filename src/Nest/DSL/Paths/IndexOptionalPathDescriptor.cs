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
	/// index is optional but AllIndices() needs to be explicitly specified for it to be optional
	/// </summary>
	public abstract class IndexOptionalPathDescriptorBase<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : IndexOptionalPathDescriptorBase<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IndexNameMarker _Index { get; set; }
		
		internal bool? _AllIndices { get; set; }

		public TDescriptor AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (TDescriptor)this;
		}
		
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			var inferrer = new ElasticInferrer(settings);
			if (!this._AllIndices.HasValue && this._Index == null)
				this._Index = inferrer.DefaultIndex;

			string index = null;
			if (!this._AllIndices.GetValueOrDefault(false))
				index = inferrer.IndexName(this._Index);

			pathInfo.Index = index;
		}

	}
}
