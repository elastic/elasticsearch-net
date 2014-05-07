using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{

	/// <summary>
	/// Provides a base for descriptors that need to describe a path in the form of 
	/// <pre>
	///	/{indices}/{types/{name}
	/// </pre>
	/// {types} is optional, {indices} is too but needs an explicit AllIndices().
	/// </summary>
	public class IndicesOptionalTypesNamePathDecriptor<TDescriptor, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : IndicesOptionalTypesNamePathDecriptor<TDescriptor, TParameters>, new()
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal bool? _AllIndices { get; set; }

		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal string _Name { get; set; }

		public TDescriptor AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (TDescriptor)this;
		}
		/// <summary>
		/// Specify multiple indices by string 
		/// </summary>
		public TDescriptor Indices(params string[] indices)
		{
			indices = indices ?? new string[]{};
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (TDescriptor) this;
		}

		/// <summary>
		/// Specify multiple indices by stating the types you are searching on. 
		/// Each type will be asked for their default index and dedupped. 
		/// </summary>
		public TDescriptor Indices(params Type[] indices)
		{
			indices = indices ?? new Type[] {};
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
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
			this._Types = types.Select(t=>(TypeNameMarker)t);
			return (TDescriptor)this;
		}
		
		/// <summary>
		/// limit the types to operate on by specifying the CLR types, the type names will be inferred.
		/// </summary>
		public TDescriptor Types(params Type[] types)
		{
			types = types ?? new Type[]{};
			this._Types = types.Select(t=>(TypeNameMarker)t);
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
			this._Name = name;
			return (TDescriptor)this;
		}

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			var inferrer = new ElasticInferrer(settings);
			if (!this._AllIndices.HasValue && this._Indices == null)
				this._Indices = new[] {(IndexNameMarker)inferrer.DefaultIndex};
			if (this._Name.IsNullOrEmpty())
				throw new DslException("missing Repository()");

			var indices = this._Indices.HasAny()
				? inferrer.IndexNames(this._Indices)
				: this._AllIndices.GetValueOrDefault(false)
					? "_all"
					: inferrer.DefaultIndex;

			var types = this._Types.HasAny()
				? inferrer.TypeNames(this._Types)
				: null;

			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Index = indices;
			pathInfo.Type = types;
			pathInfo.Name = this._Name;
			return pathInfo;
		}

	}
}
