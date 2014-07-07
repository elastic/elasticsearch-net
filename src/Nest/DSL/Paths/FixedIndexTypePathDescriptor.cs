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
	/// {index} is optional and so is {type} and will NOT fallback to the defaults of <para>T</para>
	/// type can only be specified in conjuction with index.
	/// </summary>
	public abstract class FixedIndexTypePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			var inferrer = new ElasticInferrer(settings);

			var index = inferrer.IndexName(this._Index); 
			var type = inferrer.TypeName(this._Type); 
		
			pathInfo.Index = index;
			pathInfo.Type = type;
		}

	}
}
