using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{

	public interface IIndicesOptionalPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IEnumerable<IndexNameMarker> Indices { get; set; }
	}
	
	internal static class IndicesOptionalPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndicesOptionalPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo
			)
			where TParameters : IRequestParameters, new()
		{
			var index = path.Indices == null ? null : string.Join(",", path.Indices.Select(i => new ElasticInferrer(settings).IndexName(i)));
			pathInfo.Index = index;
		}
	}

	public abstract class IndicesOptionalPathBase<TParameters> : BasePathRequest<TParameters>, IIndicesOptionalPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IEnumerable<IndexNameMarker> Indices { get; set; }
		
		protected override void SetRouteParameters(
			IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}
	/// </pre>
	/// {indices} is optional 
	/// </summary>
	public abstract class IndicesOptionalPathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndicesOptionalPath<TParameters>
		where TDescriptor : IndicesOptionalPathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{

		private IIndicesOptionalPath<TParameters> Self { get { return this; } }

		IEnumerable<IndexNameMarker> IIndicesOptionalPath<TParameters>.Indices { get; set; }
		
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
			Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}

		public TDescriptor Indices(params Type[] indicesTypes)
		{
			Self.Indices = indicesTypes.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndicesOptionalPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
