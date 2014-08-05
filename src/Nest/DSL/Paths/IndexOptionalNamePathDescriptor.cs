using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndexOptionalNamePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexNameMarker Index { get; set; }
		string Name { get; set; }
	}

	internal static class IndexOptionalNamePathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndexOptionalNamePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			if (path.Name == null)
				throw new DslException("missing name route parameter");
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName(path.Index); 
			pathInfo.Index = index;
			pathInfo.Name = path.Name;
		}
		
	}

	public abstract class IndexOptionalNamePathBase<TParameters> : BasePathRequest<TParameters>, IIndexOptionalNamePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexNameMarker Index { get; set; }
		public string Name { get; set; }
		
		public IndexOptionalNamePathBase(string name)
		{
			this.Name = name;
		}
		public IndexOptionalNamePathBase(IndexNameMarker index, string name)
		{
			this.Index = index;
			this.Name = name;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexOptionalNamePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
		
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{name}
	/// </pre>
	/// neither parameter is optional 
	/// </summary>
	public abstract class IndexOptionalNamePathDescriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor, TParameters>, IIndexOptionalNamePath<TParameters>
		where TDescriptor : IndexOptionalNamePathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndexOptionalNamePath<TParameters> Self { get { return this; } }

		IndexNameMarker IIndexOptionalNamePath<TParameters>.Index { get; set; }
		string IIndexOptionalNamePath<TParameters>.Name { get; set; }
		
		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			Self.Index = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Index(string indexType)
		{
			Self.Index = indexType;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type indexType)
		{
			Self.Index = indexType;
			return (TDescriptor)this;
		}
		
		public TDescriptor Name(string name)
		{
			Self.Name = name;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndexOptionalNamePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
