using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndexTypeTypedPath<TParameters, T> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		IndexNameMarker Index { get; set; }
		TypeNameMarker Type { get; set; }
	}

	internal static class IndexTypeTypePathRouteParameters
	{
		public static void SetRouteParameters<TParameters, T>(
			IIndexTypeTypedPath<TParameters, T> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
			where T : class
		{	
			var inferrer = new ElasticInferrer(settings);
			if (path.Index == null)
				path.Index = inferrer.IndexName<T>();
			if (path.Type == null)
				path.Type = inferrer.TypeName<T>();

			var index = inferrer.IndexName(path.Index); 
			var type = inferrer.TypeName(path.Type); 

			pathInfo.Index = index;
			pathInfo.Type = type;
		}
	}

	public abstract class IndexTypeTypePathBase<TParameters, T> : BasePathRequest<TParameters>, IIndexTypeTypedPath<TParameters, T>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		public IndexNameMarker Index { get; set; }
		public TypeNameMarker Type { get; set; }
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexTypeTypePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}
	}
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}
	/// </pre>
	/// if one of the parameters is not explicitly specified this will fall back to the defaults for type <para>T</para>
	/// </summary>
	public abstract class IndexTypePathTypedDescriptor<TDescriptor, TParameter, T> 
		: BasePathDescriptor<TDescriptor, TParameter>, IIndexTypeTypedPath<TParameter, T>
		where TDescriptor : IndexTypePathTypedDescriptor<TDescriptor, TParameter, T>, new()
		where TParameter : FluentRequestParameters<TParameter>, new()
		where T : class
	{
		private IIndexTypeTypedPath<TParameter, T> Self { get { return this; } }
		IndexNameMarker IIndexTypeTypedPath<TParameter, T>.Index { get; set; }
		TypeNameMarker IIndexTypeTypedPath<TParameter, T>.Type { get; set; }
		
		public TDescriptor Index<TAlternative>() 
		{
			Self.Index = typeof(TAlternative);
			return (TDescriptor)this;
		}
			
		public TDescriptor Index(string index)
		{
			Self.Index = index;
			return (TDescriptor)this;
		}

		public TDescriptor Index(Type indexType)
		{
			Self.Index = indexType;
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

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameter> pathInfo)
		{
			IndexTypeTypePathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}	
	}
}
