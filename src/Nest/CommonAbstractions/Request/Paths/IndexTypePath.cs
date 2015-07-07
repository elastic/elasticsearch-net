using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{

	public interface IIndexTypePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IndexName Index { get; set; }
		TypeName Type { get; set; }
	}

	internal static class IndexTypePathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndexTypePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			var inferrer = new ElasticInferrer(settings);

			if (path.Index == null)
			{
				if (path.Type != null && path.Type.Type != null)
					path.Index = path.Type.Type;
				else 
					throw new DslException("Index() not specified");
			}
			
			if (path.Type == null)
				throw new DslException("Type() not specified");

			var index = inferrer.IndexName(path.Index); 
			var type = inferrer.TypeName(path.Type);

			pathInfo.Index = index;
			pathInfo.Type = type;
		}

		public static void SetRouteParameters<TParameters, T>(
			IIndexTypePath<TParameters> path,
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

	public abstract class IndexTypePathBase<TParameters> : BasePathRequest<TParameters>, IIndexTypePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }
		
		public IndexTypePathBase(IndexName index, TypeName typeNameMarker)
		{
			this.Index = index;
			this.Type = typeNameMarker;
		}
		
		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndexTypePathRouteParameters.SetRouteParameters<TParameters>(this, settings, pathInfo);
		}
	}

	public abstract class IndexTypePathBase<TParameters, T> : BasePathRequest<TParameters>, IIndexTypePath<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		public IndexName Index { get; set; }
		public TypeName Type { get; set; }

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndexTypePathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}
	/// </pre>
	/// Where neither parameter is optional
	/// </summary>
	public abstract class IndexTypePathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndexTypePath<TParameters>
		where TDescriptor : IndexTypePathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndexTypePath<TParameters> Self { get { return this;  } }

		IndexName IIndexTypePath<TParameters>.Index { get; set; }
		TypeName IIndexTypePath<TParameters>.Type { get; set; }
		
		public TDescriptor Index<TAlternative>() where TAlternative : class
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
		
		public TDescriptor Type<TAlternative>() where TAlternative : class
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
			IndexTypePathRouteParameters.SetRouteParameters<TParameters>(this, settings, pathInfo);
		}

	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{index}/{type}
	/// </pre>
	/// Where neither parameter is optional
	/// </summary>
	public abstract class IndexTypePathDescriptor<TDescriptor, TParameters, T> 
		: IndexTypePathDescriptor<TDescriptor, TParameters>
		where TDescriptor : IndexTypePathDescriptor<TDescriptor, TParameters, T>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{
		private IIndexTypePath<TParameters> Self { get { return this;  } }

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndexTypePathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}

}

