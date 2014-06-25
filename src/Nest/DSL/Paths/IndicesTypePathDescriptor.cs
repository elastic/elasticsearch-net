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
	///	/{indices}/{type}
	/// </pre>
	/// {indices} is optional and so is {type} and will fallback to default of <para>T</para>
	/// </summary>
	public abstract class IndicesTypePathDescriptor<TDescriptor, TParameters, T> : BasePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : IndicesTypePathDescriptor<TDescriptor, TParameters, T> 
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{
		internal bool? _AllIndices { get; set; }
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal TypeNameMarker _Type { get; set; }
		
		public TDescriptor AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (TDescriptor)this;
		}
		public TDescriptor Index<TAlternative>()
		{
			return this.Indices(typeof (TAlternative));
		}
		public TDescriptor Index(string index)
		{
			return this.Indices(index);
		}
		public TDescriptor Indices(params string[] indices)
		{
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}

		public TDescriptor Indices(params Type[] indices)
		{
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Type == null)
				this._Type = inferrer.TypeName<T>();

			var index = !this._Indices.HasAny()
				? inferrer.IndexName<T>()
				: string.Join(",", this._Indices.Select(inferrer.IndexName));
			if (this._AllIndices.GetValueOrDefault(false))
				index = "_all";

			var type = new ElasticInferrer(settings).TypeName(this._Type); 
			pathInfo.Index = index;
			pathInfo.Type = type;
		}

	}
}
