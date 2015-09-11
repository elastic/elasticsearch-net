using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndexNamePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexName Index { get; set; }
		string Name { get; set; }
	}

	internal static class IndexNamePathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndexNamePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			if (path.Name == null)
				throw new DslException("missing name route parameter");
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName(path.Index) ?? inferrer.DefaultIndex; 
			pathInfo.Index = index;
			pathInfo.Name = path.Name;
		}
		
		public static void SetRouteParameters<TParameters, T>(
			IIndexNamePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
			where T : class
		{	
			if (path.Name == null)
				throw new DslException("missing name route parameter");
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName(path.Index) ?? inferrer.IndexName(typeof(T)) ?? inferrer.DefaultIndex; 
			pathInfo.Index = index;
			pathInfo.Name = path.Name;
		}
	}

	public abstract class IndexNamePathBase<TParameters> : PathRequestBase<TParameters>, IIndexNamePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexName Index { get; set; }
		public string Name { get; set; }
		
		public IndexNamePathBase(IndexName index, string name)
		{
			this.Index = index;
			this.Name = name;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexNamePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
		
	public abstract class IndexNamePathBase<TParameters, T> : PathRequestBase<TParameters>, IIndexNamePath<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		public IndexName Index { get; set; }
		public string Name { get; set; }
		
		public IndexNamePathBase(string name)
		{
			this.Name = name;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexNamePathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{name}
	/// </pre>
	/// neither parameter is optional 
	/// </summary>
	public abstract class IndexNamePathDescriptor<TDescriptor, TParameters, T> : PathDescriptorBase<TDescriptor, TParameters>, IIndexNamePath<TParameters>
		where TDescriptor : IndexNamePathDescriptor<TDescriptor, TParameters, T>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{
		private IIndexNamePath<TParameters> Self => this;

		IndexName IIndexNamePath<TParameters>.Index { get; set; }
		string IIndexNamePath<TParameters>.Name { get; set; }
		
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
			IndexNamePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
