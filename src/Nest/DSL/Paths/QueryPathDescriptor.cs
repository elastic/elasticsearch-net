using System;
using System.Collections.Generic;
using System.Linq;
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
	///	/{indices}/{types}
	/// </pre>
	/// all parameters are optional and will default to the defaults for <para>T</para>
	/// </summary>
	public class QueryPathDescriptorBase<P, T, K> : BasePathDescriptor<P>
		where P : QueryPathDescriptorBase<P, T, K>, new()
		where T : class
		where K : FluentRequestParameters<K>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public P Indices(params Type[] indices)
		{
			if (indices == null) return (P)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (P)this;
		}
		public P Indices(params string[] indices)
		{
			if (indices == null) return (P)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (P)this;
		}
		public P Indices(IEnumerable<Type> indices)
		{
			if (indices == null) return (P)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (P)this;
		}
		public P Indices(IEnumerable<string> indices)
		{
			if (indices == null) return (P)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (P)this;
		}

		public P Index<TAlternative>() where TAlternative : class
		{
			return this.Indices(typeof(TAlternative));
		}

		public P Index(Type index)
		{
			return this.Indices(index);
		}
		public P Index(string index)
		{
			return this.Indices(index);
		}
		public P Types(IEnumerable<string> types)
		{
			if (types == null) return (P)this;
			this._Types = types.Select(s => (TypeNameMarker)s); ;
			return (P)this;
		}
		public P Types(params string[] types)
		{
			if (types == null) return (P)this;
			return this.Types((IEnumerable<string>)types);
		}
		public P Types(IEnumerable<Type> types)
		{
			if (types == null) return (P)this;
			this._Types = types.Select(t => (TypeNameMarker)t);
			return (P)this;
		}
		public P Types(params Type[] types)
		{
			if (types == null) return (P)this;
			return this.Types((IEnumerable<Type>)types);
		}
		public P Type(string type)
		{
			return this.Types(type);
		}
		public P Type(Type type)
		{
			return this.Types(type);
		}
		public P Type<TAlternative>() where TAlternative : class
		{
			return this.Types(typeof(TAlternative));
		}
		public P AllIndices()
		{
			this._AllIndices = true;
			return (P)this;
		}
		public P AllTypes()
		{
			this._AllTypes = true;
			return (P)this;
		}

		internal virtual ElasticsearchPathInfo<K> ToPathInfo<K>(IConnectionSettingsValues settings, K queryString)
			where K : FluentRequestParameters<K>, new()
		{
			//start out with defaults
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName<T>();
			var type = inferrer.TypeName<T>();
			var pathInfo = new ElasticsearchPathInfo<K>()
			{
				Index = index,
				Type = type
			};

			if (this._Types.HasAny())
				pathInfo.Type = inferrer.TypeNames(this._Types);
			else if (this._AllTypes)
				pathInfo.Type = null;
			else pathInfo.Type = inferrer.TypeName<T>();

			if (this._Indices.HasAny())
				pathInfo.Index = inferrer.IndexNames(this._Indices);
			else if (this._AllIndices && !pathInfo.Type.IsNullOrEmpty())
				pathInfo.Index = "_all";
			else
				pathInfo.Index = this._AllIndices ? null : inferrer.IndexName<T>();



			pathInfo.RequestParameters = queryString ?? new K();
			pathInfo.RequestParameters.RequestConfiguration(this._RequestConfiguration);
			return pathInfo;
		}

	}
}
