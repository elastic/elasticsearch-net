using System;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndexOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexNameMarker Index { get; set; }
		bool? AllIndices { get; set; }
	}

	internal static class IndexOptionalPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndexOptionalPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			var inferrer = new ElasticInferrer(settings);
			if (!path.AllIndices.HasValue && path.Index == null)
				path.Index = inferrer.DefaultIndex;

			string index = null;
			if (!path.AllIndices.GetValueOrDefault(false))
				index = inferrer.IndexName(path.Index);

			pathInfo.Index = index;
		}
	
	}

	public abstract class IndexOptionalPathBase<TParameters> : BasePathRequest<TParameters>, IIndexOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexNameMarker Index { get; set; }
		public bool? AllIndices { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}


	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}
	/// </pre>
	/// index is optional but AllIndices() needs to be explicitly specified for it to be optional
	/// </summary>
	public abstract class IndexOptionalPathDescriptorBase<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndexOptionalPath<TParameters>
		where TDescriptor : IndexOptionalPathDescriptorBase<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndexOptionalPath<TParameters> Self { get { return this;  } }

		IndexNameMarker IIndexOptionalPath<TParameters>.Index { get; set; }
		
		bool? IIndexOptionalPath<TParameters>.AllIndices { get; set; }

		public TDescriptor AllIndices(bool allIndices = true)
		{
			Self.AllIndices = allIndices;
			return (TDescriptor)this;
		}
		
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
			IndexOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
