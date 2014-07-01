using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndicesOptionalTypesNamePath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		bool? AllIndices { get; set; }

		IEnumerable<IndexNameMarker> Indices { get; set; }

		IEnumerable<TypeNameMarker> Types { get; set; }

		string Name { get; set; }
	}

	internal static class IndicesOptionalTypesNamePathRouteParamaters
	{
		public static void SetRouteParameters<TParameters>(
			IIndicesOptionalTypesNamePath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			var inferrer = new ElasticInferrer(settings);
			if (!path.AllIndices.HasValue && path.Indices == null)
				path.Indices = new[] {(IndexNameMarker)inferrer.DefaultIndex};
			if (path.Name.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			var indices = path.Indices.HasAny()
				? inferrer.IndexNames(path.Indices)
				: path.AllIndices.GetValueOrDefault(false)
					? "_all"
					: inferrer.DefaultIndex;

			var types = path.Types.HasAny()
				? inferrer.TypeNames(path.Types)
				: null;

			pathInfo.Index = indices;
			pathInfo.Type = types;
			pathInfo.Name = path.Name;
		}
	
	}

	public abstract class IndicesOptionalTypesNamePathBase<TParameters> : BasePathRequest<TParameters>, IIndicesOptionalTypesNamePath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		
		public bool? AllIndices { get; set; }
		public IEnumerable<IndexNameMarker> Indices { get; set; }
		public IEnumerable<TypeNameMarker> Types { get; set; }
		public string Name { get; set; }

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesOptionalTypesNamePathRouteParamaters.SetRouteParameters(this, settings, pathInfo);
		}

	}
	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}/{types/{name}
	/// </pre>
	/// {types} is optional, {indices} is too but needs an explicit AllIndices().
	/// </summary>
	public abstract class IndicesOptionalTypesNamePathDescriptor<TDescriptor, TParameters> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndicesOptionalTypesNamePath<TParameters>
		where TDescriptor : IndicesOptionalTypesNamePathDescriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		private IIndicesOptionalTypesNamePath<TParameters> Self { get { return this; } }

		bool? IIndicesOptionalTypesNamePath<TParameters>.AllIndices { get; set; }
		IEnumerable<IndexNameMarker> IIndicesOptionalTypesNamePath<TParameters>.Indices { get; set; }
		IEnumerable<TypeNameMarker> IIndicesOptionalTypesNamePath<TParameters>.Types { get; set; }
		string IIndicesOptionalTypesNamePath<TParameters>.Name { get; set; }

		public TDescriptor AllIndices(bool allIndices = true)
		{
			Self.AllIndices = allIndices;
			return (TDescriptor)this;
		}
		/// <summary>
		/// Specify multiple indices by string 
		/// </summary>
		public TDescriptor Indices(params string[] indices)
		{
			indices = indices ?? new string[]{};
			Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor) this;
		}

		/// <summary>
		/// Specify multiple indices by stating the types you are searching on. 
		/// Each type will be asked for their default index and dedupped. 
		/// </summary>
		public TDescriptor Indices(params Type[] indices)
		{
			indices = indices ?? new Type[] {};
			Self.Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor) this;
		}
		
		/// <summary>
		/// Use the default index of T
		/// </summary>
		public TDescriptor Index<TAlternative>() where TAlternative : class
		{
			return this.Indices(new[] {typeof (TAlternative)});
		}
		/// <summary>
		/// Use the default index of T
		/// </summary>
		public TDescriptor Index(Type index) 
		{
			return this.Indices(new[] { index });
		}
		/// <summary>
		/// Use the default index of T
		/// </summary>
		public TDescriptor Index(string index) 
		{
			return this.Indices(new[] { index });
		}
		/// <summary>
		/// limit the types to operate on by specifiying them by string
		/// </summary>
		public TDescriptor Types(params string[] types)
		{
			types = types ?? new string[]{};
			Self.Types = types.Select(t=>(TypeNameMarker)t);
			return (TDescriptor)this;
		}
		
		/// <summary>
		/// limit the types to operate on by specifying the CLR types, the type names will be inferred.
		/// </summary>
		public TDescriptor Types(params Type[] types)
		{
			types = types ?? new Type[]{};
			Self.Types = types.Select(t=>(TypeNameMarker)t);
			return (TDescriptor)this;
		}

		/// <summary>
		/// Limit the operation on type T
		/// </summary>
		public TDescriptor Type<TAlternative>() where TAlternative : class
		{
			return this.Types(new Type[] {typeof (TAlternative)});
		}

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public TDescriptor Name(string name)
		{
			Self.Name = name;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndicesOptionalTypesNamePathRouteParamaters.SetRouteParameters(this, settings, pathInfo);
		}

	}
}
