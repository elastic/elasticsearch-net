using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public interface IQueryPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IEnumerable<IndexNameMarker> Indices { get; set; }
		IEnumerable<TypeNameMarker> Types { get; set; }
		bool? AllIndices { get; set; }
		bool? AllTypes { get; set; }
	}
	
	public interface IQueryPath<TParameters, T> : IQueryPath<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class { }
	

	internal static class QueryPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IQueryPath<TParameters> path,
			IConnectionSettingsValues settings,
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{
			var inferrer = new ElasticInferrer(settings);

			if (path.Types.HasAny())
				pathInfo.Type = inferrer.TypeNames(path.Types);
			else if (path.AllTypes.GetValueOrDefault(false))
				pathInfo.Type = null;

			if (path.Indices.HasAny())
				pathInfo.Index = inferrer.IndexNames(path.Indices);
			else if (path.AllIndices.GetValueOrDefault(false) && !pathInfo.Type.IsNullOrEmpty())
				pathInfo.Index = "_all";
			else if (!path.AllIndices.GetValueOrDefault(false) && pathInfo.Index.IsNullOrEmpty())
				pathInfo.Index = inferrer.DefaultIndex;

		}
		public static void SetRouteParameters<TParameters, T>(
			IQueryPath<TParameters> path,
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
			else if (path.AllTypes.GetValueOrDefault(false))
				pathInfo.Type = null;
			else pathInfo.Type = inferrer.TypeName<T>();

			if (path.Indices.HasAny())
				pathInfo.Index = inferrer.IndexNames(path.Indices);
			else
				pathInfo.Index = path.AllIndices.GetValueOrDefault(false) ? null : inferrer.IndexName<T>();

			if (pathInfo.Index.IsNullOrEmpty() && !pathInfo.Type.IsNullOrEmpty())
				pathInfo.Index = "_all";
		}
	}

	public abstract class QueryPathBase<TParameters> : BasePathRequest<TParameters>, IQueryPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		protected QueryPathBase()
		{
			this.AllTypes = true;
		}

		protected QueryPathBase(IndexNameMarker index, TypeNameMarker type = null)
		{
			this.Indices = new [] { index };
			if (type != null)
				this.Types = new[] { type };
			else this.AllTypes = true;
		}

		protected QueryPathBase(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null)
		{
			this.Indices = indices;
			this.AllTypes = !types.HasAny();
			this.Types = types;
		}


		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			QueryPathRouteParameters.SetRouteParameters<TParameters>(this, settings, pathInfo);
		}

		public IEnumerable<IndexNameMarker> Indices { get; set; }
		public IEnumerable<TypeNameMarker> Types { get; set; }
		public bool? AllIndices { get; set; }
		public bool? AllTypes { get; set; }
	}


	public abstract class QueryPathBase<TParameters, T> : QueryPathBase<TParameters>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		protected QueryPathBase()
		{
			this.AllIndices = false;
			this.AllTypes = false;
		}
		
		protected QueryPathBase(IndexNameMarker index, TypeNameMarker type = null)
		{
			this.Indices = new [] { index };
			if (type != null)
				this.Types = new[] { type };
		}

		protected QueryPathBase(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null)
		{
			this.Indices = indices;
			this.Types = types;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			QueryPathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}
	}



	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}/{types}
	/// </pre>
	/// all parameters are optional and will default to the defaults for <para>T</para>
	/// </summary>
	public abstract class QueryPathDescriptorBase<TDescriptor, TParameters, T> 
        : BasePathDescriptor<TDescriptor, TParameters>, IQueryPath<TParameters>
		where TDescriptor : QueryPathDescriptorBase<TDescriptor, TParameters, T>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
        where T : class
	{
		private IQueryPath<TParameters> Self { get { return this; } }

		IEnumerable<IndexNameMarker> IQueryPath<TParameters>.Indices { get; set; }
		IEnumerable<TypeNameMarker> IQueryPath<TParameters>.Types { get; set; }
		bool? IQueryPath<TParameters>.AllIndices { get; set; }
		bool? IQueryPath<TParameters>.AllTypes { get; set; }

		/// <summary>
		/// The indices to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Indices(params Type[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}

		/// <summary>
		/// The indices to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Indices(params IndexNameMarker[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices;
			return (TDescriptor)this;
		}

		/// <summary>
		/// The indices to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Indices(params string[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}

		/// <summary>
		/// The indices to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Indices(IEnumerable<Type> indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}

		/// <summary>
		/// The indices to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Indices(IEnumerable<string> indices)
		{
			if (indices == null) return (TDescriptor)this;
			Self.Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}

		/// <summary>
		/// The index to execute the search on, using the default index for typeof TAlternative. Defaults to the default index
		/// </summary>
		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			return this.Indices(typeof(TAlternative));
		}

		/// <summary>
		/// The index to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Index(Type index)
		{
			if (index == null) return (TDescriptor)this;
			return this.Indices(index);
		}

		/// <summary>
		/// The index to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Index(IndexNameMarker index)
		{
			if (index == null) return (TDescriptor)this;
			return this.Indices(index);
		}

		/// <summary>
		/// The index to execute the search on. Defaults to the default index
		/// </summary>
		public TDescriptor Index(string index)
		{
			if (index == null) return (TDescriptor)this;
			return this.Indices(index);
		}

		/// <summary>
		/// The types to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Types(IEnumerable<string> types)
		{
			if (types == null) return (TDescriptor)this;
			Self.Types = types.Select(s => (TypeNameMarker)s); ;
			return (TDescriptor)this;
		}

		/// <summary>
		/// The types to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Types(params string[] types)
		{
			if (types == null) return (TDescriptor)this;
			return this.Types((IEnumerable<string>)types);
		}

		/// <summary>
		/// The types to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Types(IEnumerable<Type> types)
		{
			if (types == null) return (TDescriptor)this;
			Self.Types = types.Select(t => (TypeNameMarker)t);
			return (TDescriptor)this;
		}

		/// <summary>
		/// The types to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Types(params TypeNameMarker[] types)
		{
			if (types == null) return (TDescriptor)this;
			Self.Types = types;
			return (TDescriptor)this;
		}
		/// <summary>
		/// The types to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Types(params Type[] types)
		{
			if (types == null) return (TDescriptor)this;
			return this.Types((IEnumerable<Type>)types);
		}


		/// <summary>
		/// The type to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Type(TypeNameMarker type)
		{
			if (type == null) return (TDescriptor)this;
			return this.Types(type);
		}

		/// <summary>
		/// The type to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Type(string type)
		{
			if (type == null) return (TDescriptor)this;
			return this.Types(type);
		}

		/// <summary>
		/// The type to execute the search on. Defaults to the inferred typename of T 
		/// unless T is dynamic then a type (or AllTypes()) MUST be specified.
		/// </summary>
		public TDescriptor Type(Type type)
		{
			if (type == null) return (TDescriptor)this;
			return this.Types(type);
		}

		/// <summary>
		/// An alternative type to infer the typename from
		/// </summary>
		public TDescriptor Type<TAlternative>() where TAlternative : class
		{
			return this.Types(typeof(TAlternative));
		}

		/// <summary>
		/// Execute search over all indices
		/// </summary>
		public TDescriptor AllIndices()
		{
			Self.AllIndices = true;
			return (TDescriptor)this;
		}

		/// <summary>
		/// Execute search over all types
		/// </summary>
		public TDescriptor AllTypes()
		{
			Self.AllTypes = true;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			QueryPathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}
}
