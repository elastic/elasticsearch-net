using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IQueryPath<TParameters, T> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		IEnumerable<IndexNameMarker> Indices { get; set; }
		IEnumerable<TypeNameMarker> Types { get; set; }
		bool AllIndices { get; set; }
		bool AllTypes { get; set; }
	}

	internal static class QueryPathRouteParameters
	{
		public static void SetRouteParameters<TParameters, T>(
			IQueryPath<TParameters, T> path,
			IConnectionSettingsValues settings,
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
			where T : class
		{
			//start out with defaults
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName<T>();
			var type = inferrer.TypeName<T>();
			pathInfo.Index = index;
			pathInfo.Type = type;

			if (path.Types.HasAny())
				pathInfo.Type = inferrer.TypeNames(path.Types);
			else if (path.AllTypes)
				pathInfo.Type = null;
			else pathInfo.Type = inferrer.TypeName<T>();

			if (path.Indices.HasAny())
				pathInfo.Index = inferrer.IndexNames(path.Indices);
			else if (path.AllIndices && !pathInfo.Type.IsNullOrEmpty())
				pathInfo.Index = "_all";
			else
				pathInfo.Index = path.AllIndices ? null : inferrer.IndexName<T>();

		}

	}

	public abstract class QueryPathBase<TParameters, T> : BasePathRequest<TParameters>, IQueryPath<TParameters, T>
		where TParameters : IRequestParameters, new()
		where T : class
	{

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			QueryPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

		public IEnumerable<IndexNameMarker> Indices { get; set; }
		public IEnumerable<TypeNameMarker> Types { get; set; }
		public bool AllIndices { get; set; }
		public bool AllTypes { get; set; }
	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}/{types}
	/// </pre>
	/// all parameters are optional and will default to the defaults for <para>T</para>
	/// </summary>
	public abstract class QueryPathDescriptorBase<TDescriptor, T, TParameters>
		: BasePathDescriptor<TDescriptor, TParameters>, IQueryPath<TParameters, T>
		where TDescriptor : QueryPathDescriptorBase<TDescriptor, T, TParameters>, new()
		where T : class
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IQueryPath<TParameters, T> Self { get { return this; } }

		IEnumerable<IndexNameMarker> IQueryPath<TParameters, T>.Indices { get; set; }
		IEnumerable<TypeNameMarker> IQueryPath<TParameters, T>.Types { get; set; }
		bool IQueryPath<TParameters, T>.AllIndices { get; set; }
		bool IQueryPath<TParameters, T>.AllTypes { get; set; }

		public TDescriptor Indices(params Type[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}
		public TDescriptor Indices(params string[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}
		public TDescriptor Indices(IEnumerable<Type> indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}
		public TDescriptor Indices(IEnumerable<string> indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}

		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			return this.Indices(typeof(TAlternative));
		}

		public TDescriptor Index(Type index)
		{
			return this.Indices(index);
		}
		public TDescriptor Index(string index)
		{
			return this.Indices(index);
		}
		public TDescriptor Types(IEnumerable<string> types)
		{
			if (types == null) return (TDescriptor)this;
			Self.Types = types.Select(s => (TypeNameMarker)s); ;
			return (TDescriptor)this;
		}
		public TDescriptor Types(params string[] types)
		{
			if (types == null) return (TDescriptor)this;
			return this.Types((IEnumerable<string>)types);
		}
		public TDescriptor Types(IEnumerable<Type> types)
		{
			if (types == null) return (TDescriptor)this;
			Self.Types = types.Select(t => (TypeNameMarker)t);
			return (TDescriptor)this;
		}
		public TDescriptor Types(params Type[] types)
		{
			if (types == null) return (TDescriptor)this;
			return this.Types((IEnumerable<Type>)types);
		}
		public TDescriptor Type(string type)
		{
			return this.Types(type);
		}
		public TDescriptor Type(Type type)
		{
			return this.Types(type);
		}
		public TDescriptor Type<TAlternative>() where TAlternative : class
		{
			return this.Types(typeof(TAlternative));
		}
		public TDescriptor AllIndices()
		{
			Self.AllIndices = true;
			return (TDescriptor)this;
		}
		public TDescriptor AllTypes()
		{
			Self.AllTypes = true;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			QueryPathRouteParameters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
