using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	public interface IIndicesOptionalTypesOptionalFieldsPath<TParameters> : IRequest<TParameters>
		where TParameters : IRequestParameters, new()
	{
		IEnumerable<IndexNameMarker> Indices { get; set; }

		IEnumerable<TypeNameMarker> Types { get; set; }

		IEnumerable<PropertyPathMarker> Fields { get; set; }
	}

	public interface IIndicesOptionalTypesOptionalFieldsPath<TParameters, T> : IIndicesOptionalTypesOptionalFieldsPath<TParameters> 
		where TParameters : IRequestParameters, new()
		where T : class
	{
		bool AllIndices { get; set; }
	}

	internal static class IndicesOptionalTypesOptionalFieldsPathRouteParameters
	{
		public static void SetRouteParameters<TParameters>(
			IIndicesOptionalTypesOptionalFieldsPath<TParameters> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
		{	
			var inferrer = settings.Inferrer;
			pathInfo.Field = inferrer.PropertyPaths(path.Fields);
			if (pathInfo.Field.IsNullOrEmpty()) throw new DslException("Fields is required");

			pathInfo.Index = inferrer.IndexNames(path.Indices);
			pathInfo.Type = inferrer.TypeNames(path.Types);
		}

		public static void SetRouteParameters<TParameters, T>(
			IIndicesOptionalTypesOptionalFieldsPath<TParameters, T> path,
			IConnectionSettingsValues settings, 
			ElasticsearchPathInfo<TParameters> pathInfo)
			where TParameters : IRequestParameters, new()
			where T : class
		{	
			var inferrer = settings.Inferrer;
			pathInfo.Field = inferrer.PropertyPaths(path.Fields);
			if (pathInfo.Field.IsNullOrEmpty()) throw new DslException("Fields is required");

			pathInfo.Index = inferrer.IndexNames(path.Indices);
			if (pathInfo.Index.IsNullOrEmpty() && !path.AllIndices)
				pathInfo.Index = inferrer.IndexName<T>();
			pathInfo.Type = inferrer.TypeNames(path.Types) ?? inferrer.TypeName<T>();
			
		}
	
	}

	public abstract class IndicesOptionalTypesOptionalFieldsPathBase<TParameters> : BasePathRequest<TParameters>, IIndicesOptionalTypesOptionalFieldsPath<TParameters>
		where TParameters : IRequestParameters, new()
	{
		
		public IEnumerable<IndexNameMarker> Indices { get; set; }
		public IEnumerable<TypeNameMarker> Types { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
			
		protected IndicesOptionalTypesOptionalFieldsPathBase(params PropertyPathMarker[] fields)
		{
			this.Fields = fields.Select(f=>(PropertyPathMarker)f);
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesOptionalTypesOptionalFieldsPathRouteParameters.SetRouteParameters<TParameters>(this, settings, pathInfo);
		}
	}

	public abstract class IndicesOptionalTypesOptionalFieldsPathBase<TParameters, T> : BasePathRequest<TParameters>, IIndicesOptionalTypesOptionalFieldsPath<TParameters, T>
		where TParameters : IRequestParameters, new()
		where T : class
	{
		
		public IEnumerable<IndexNameMarker> Indices { get; set; }
		public IEnumerable<TypeNameMarker> Types { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public bool AllIndices { get; set; }

		protected IndicesOptionalTypesOptionalFieldsPathBase(params PropertyPathMarker[] fields)
		{
			this.Fields = fields.Select(f=>(PropertyPathMarker)f);
		}

		protected IndicesOptionalTypesOptionalFieldsPathBase(params Expression<Func<T, object>>[] fields)
		{
			this.Fields = fields.Select(f=>(PropertyPathMarker)f);
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{	
			IndicesOptionalTypesOptionalFieldsPathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}/{types/{Fields}
	/// </pre>
	/// {types} is optional, {indices} is too, {Fields} is mandatory
	/// </summary>
	public abstract class IndicesOptionalTypesOptionalFieldsPathDescriptor<TDescriptor, TParameters, T> 
		: BasePathDescriptor<TDescriptor, TParameters>, IIndicesOptionalTypesOptionalFieldsPath<TParameters, T>
		where TDescriptor : IndicesOptionalTypesOptionalFieldsPathDescriptor<TDescriptor, TParameters, T>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
		where T : class
	{
		private IIndicesOptionalTypesOptionalFieldsPath<TParameters, T> Self { get { return this; } }

		IEnumerable<IndexNameMarker> IIndicesOptionalTypesOptionalFieldsPath<TParameters>.Indices { get; set; }
		IEnumerable<TypeNameMarker> IIndicesOptionalTypesOptionalFieldsPath<TParameters>.Types { get; set; }
		IEnumerable<PropertyPathMarker> IIndicesOptionalTypesOptionalFieldsPath<TParameters>.Fields { get; set; }
		bool IIndicesOptionalTypesOptionalFieldsPath<TParameters, T>.AllIndices { get; set; }

		/// <summary>
		/// Force the operation to hit _all indices
		/// </summary>
		public TDescriptor AllIndices(bool allIndices = true)
		{
			Self.AllIndices = allIndices;
			return (TDescriptor) this;
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
		/// Specify the fields to operate on
		/// </summary>
		public TDescriptor Fields(params Expression<Func<T, object>>[] fields)
		{
			Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return (TDescriptor)this;
		}

		/// <summary>
		/// Specify the fields to operate on
		/// </summary>
		public TDescriptor Fields(params PropertyPathMarker[] fields)
		{
			Self.Fields = fields;
			return (TDescriptor)this;
		}

		protected override void SetRouteParameters(IConnectionSettingsValues settings, ElasticsearchPathInfo<TParameters> pathInfo)
		{
			IndicesOptionalTypesOptionalFieldsPathRouteParameters.SetRouteParameters<TParameters, T>(this, settings, pathInfo);
		}

	}
}
