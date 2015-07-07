using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndicesOptionalExplicitAllPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IEnumerable<IndexName> Indices { get; set; }
		bool? AllIndices { get; set; }
	}

	internal static class IndicesOptionalExplicitAllPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndicesOptionalExplicitAllPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			var inferrer = new ElasticInferrer(settings);
			if (!path.AllIndices.HasValue && path.Indices == null)
				path.Indices = new[] {(IndexName)inferrer.DefaultIndex};

			string index = "_all";
			if (!path.AllIndices.GetValueOrDefault(false))
				index = string.Join(",", path.Indices.Select(inferrer.IndexName));

			pathInfo.Index = index;
		
		}
	
	}

	public abstract class IndicesOptionalExplicitAllPathBase<TParameters> : BasePathRequest<TParameters>, IIndicesOptionalExplicitAllPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IEnumerable<IndexName> Indices { get; set; }
		public bool? AllIndices { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesOptionalExplicitAllPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}
	/// </pre>
	/// {indices} is optional but AllIndices() needs to be explicitly called.
	/// </summary>
	public abstract class IndicesOptionalExplicitAllPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndicesOptionalExplicitAllPath<TParameters>
		where TDescriptor : IndicesOptionalExplicitAllPathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndicesOptionalExplicitAllPath<TParameters> Self => this;

		IEnumerable<IndexName> IIndicesOptionalExplicitAllPath<TParameters>.Indices { get; set; }
		
		bool? IIndicesOptionalExplicitAllPath<TParameters>.AllIndices { get; set; }

		public TDescriptor AllIndices(bool allIndices = true)
		{
			Self.AllIndices = allIndices;
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
			Self.Indices = indices.Select(s=>(IndexName)s);
			return (TDescriptor)this;
		}

		public TDescriptor Indices(params Type[] indicesTypes)
		{
			Self.Indices = indicesTypes.Select(s=>(IndexName)s);
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndicesOptionalExplicitAllPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
