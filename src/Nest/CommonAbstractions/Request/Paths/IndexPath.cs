using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndexPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexName Index { get; set; }
	}

	internal static class IndexPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndexPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			Console.WriteLine($"Set route params: {path.Index}");
			if (path.Index == null)
				throw new DslException("missing index route parameter");

			var index = new ElasticInferrer(settings).IndexName(path.Index); 
			Console.WriteLine($"resolved: {index}");
			pathInfo.Index = index;
		}
	
	}

	public abstract class IndexPathBase<TParameters> : BasePathRequest<TParameters>, IIndexPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexPathBase(IndexName index)
		{
			this.Index = index;
		}

		public IndexName Index { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}
	/// </pre>
	/// index is not optional 
	/// </summary>
	public abstract class IndexPathDescriptorBase<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndexPath<TParameters> 
		where TDescriptor : IndexPathDescriptorBase<TDescriptor, TParameters>
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndexPath<TParameters> Self => this;

		IndexName IIndexPath<TParameters>.Index { get; set; }
		
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			Console.WriteLine("Set route params");
			IndexPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
}
