using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{

	public interface IIndicesTypePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		bool? AllIndices { get; set; }
		IEnumerable<IndexNameMarker> Indices { get; set; }
		TypeNameMarker Type { get; set; }
		
	}
	
	internal static class IndicesTypePathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndicesTypePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{
			var inferrer = new ElasticInferrer(settings);

			var index = !path.Indices.HasAny()
				? null
				: string.Join(",", path.Indices.Select(inferrer.IndexName));

			if (path.AllIndices.GetValueOrDefault(false))
				index = "_all";

			var type = inferrer.TypeName(path.Type); 
			pathInfo.Index = index;
			pathInfo.Type = type;
		}

		public static void SetRouteParameters<TParameters, T>(
			IIndicesTypePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
			where T : class
		{
			var inferrer = new ElasticInferrer(settings);
			if (path.Type == null)
				path.Type = inferrer.TypeName<T>();

			var index = !path.Indices.HasAny()
				? inferrer.IndexName<T>()
				: string.Join(",", path.Indices.Select(inferrer.IndexName));
			if (path.AllIndices.GetValueOrDefault(false))
				index = "_all";

			var type = inferrer.TypeName(path.Type); 
			pathInfo.Index = index;
			pathInfo.Type = type;
		}
	}

	public abstract class IndicesTypePathBase<TParameters> : BasePathRequest<TParameters>, IIndicesTypePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public bool? AllIndices { get; set; }
		public IEnumerable<IndexNameMarker> Indices { get; set; }
		public TypeNameMarker Type { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesTypePathRouteParameters.SetRouteParameters<TParameters>(this, settings, pathInfo);
		}
	}
	
	public abstract class IndicesTypePathBase<TParameters, T> : IndicesTypePathBase<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesTypePathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}/{type}
	/// </pre>
	/// {indices} is optional and so is {type} and will fallback to default of <para>T</para>
	/// </summary>
	public abstract class IndicesTypePathDescriptor<TDescriptor, TParameters, T> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndicesTypePath<TParameters> 
		where TDescriptor : IndicesTypePathDescriptor<TDescriptor, TParameters, T> 
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{
		private IIndicesTypePath<TParameters> Self { get { return this; } }

		bool? IIndicesTypePath<TParameters>.AllIndices { get; set; }
		IEnumerable<IndexNameMarker> IIndicesTypePath<TParameters>.Indices { get; set; }
		TypeNameMarker IIndicesTypePath<TParameters>.Type { get; set; }
		
		public TDescriptor AllIndices(bool allIndices = true)
		{
			Self.AllIndices = allIndices;
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
			Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}

		public TDescriptor Indices(params Type[] indices)
		{
			Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor)this;
		}
		
		public TDescriptor Type<TAlternative>() 
		{
			Self.Type = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Type(string type)
		{
			Self.Type = type;
			return (TDescriptor)this;
		}

		public TDescriptor Type(Type type)
		{
			Self.Type = type;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndicesTypePathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}
}
