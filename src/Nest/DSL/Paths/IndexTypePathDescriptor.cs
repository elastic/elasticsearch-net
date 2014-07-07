using System;
using Elasticsearch.Net;
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
	public abstract class IndexTypePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			var inferrer = new ElasticInferrer(settings);
			if (this._Index == null)
				throw new DslException("Index() not specified");
			if (this._Type == null)
				throw new DslException("Type() not specified");

			var index = inferrer.IndexName(this._Index); 
			var type = inferrer.TypeName(this._Type); 

			pathInfo.Index = index;
			pathInfo.Type = type;
		}

	}
}
