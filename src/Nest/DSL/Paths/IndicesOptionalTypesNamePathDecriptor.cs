using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
	public class IndicesOptionalTypesNamePathDecriptor<P, K>
		where P : IndicesOptionalTypesNamePathDecriptor<P, K>, new()
		where K : FluentQueryString<K>, new()
	{
		internal bool? _AllIndices { get; set; }

		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal string _Name { get; set; }

		public P AllIndices(bool allIndices = true)
		{
			this._AllIndices = allIndices;
			return (P)this;
		}
		/// <summary>
		/// Specify multiple indices by string 
		/// </summary>
		public P Indices(params string[] indices)
		{
			indices = indices ?? new string[]{};
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (P) this;
		}

		/// <summary>
		/// Specify multiple indices by stating the types you are searching on. 
		/// Each type will be asked for their default index and dedupped. 
		/// </summary>
		public P Indices(params Type[] indices)
		{
			indices = indices ?? new Type[] {};
			this._Indices = indices.Select(s=>(IndexNameMarker)s);
			return (P) this;
		}
		
		/// <summary>
		/// Use the default index of T
		/// </summary>
		public P Index<T>() where T : class
		{
			return this.Indices(new[] {typeof (T)});
		}
		/// <summary>
		/// Use the default index of T
		/// </summary>
		public P Index(Type index) 
		{
			return this.Indices(new[] { index });
		}
		/// <summary>
		/// Use the default index of T
		/// </summary>
		public P Index(string index) 
		{
			return this.Indices(new[] { index });
		}
		/// <summary>
		/// limit the types to operate on by specifiying them by string
		/// </summary>
		public P Types(params string[] types)
		{
			types = types ?? new string[]{};
			this._Types = types.Select(t=>(TypeNameMarker)t);
			return (P)this;
		}
		
		/// <summary>
		/// limit the types to operate on by specifying the CLR types, the type names will be inferred.
		/// </summary>
		public P Types(params Type[] types)
		{
			types = types ?? new Type[]{};
			this._Types = types.Select(t=>(TypeNameMarker)t);
			return (P)this;
		}

		/// <summary>
		/// Limit the operation on type T
		/// </summary>
		public P Type<T>() where T : class
		{
			return this.Types(new Type[] {typeof (T)});
		}

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public P Name(string name)
		{
			this._Name = name;
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentQueryString<K>, new()
		{
			var inferrer = new ElasticInferrer(settings);
			if (!this._AllIndices.HasValue && this._Indices == null)
				this._Indices = new[] {(IndexNameMarker)inferrer.DefaultIndex};
			if (this._Name.IsNullOrEmpty())
				throw new DslException("missing Name()");

			var indices = this._Indices.HasAny()
				? inferrer.IndexNames(this._Indices)
				: this._AllIndices.GetValueOrDefault(false)
					? "_all"
					: inferrer.DefaultIndex;

			var types = this._Types.HasAny()
				? inferrer.TypeNames(this._Types)
				: null;

			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = indices,
				Type = types,
				Name = this._Name
			};
			pathInfo.QueryString = queryString ?? new K();
			return pathInfo;
		}

	}
}
