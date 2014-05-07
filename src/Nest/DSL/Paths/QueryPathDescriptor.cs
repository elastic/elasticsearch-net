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
	public class QueryPathDescriptorBase<TDescriptor, T, TParameters> : BasePathDescriptor<TDescriptor>
		where TDescriptor : QueryPathDescriptorBase<TDescriptor, T, TParameters>, new()
		where T : class
		where TParameters : FluentRequestParameters<TParameters>, new()
	{
		internal IEnumerable<IndexNameMarker> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public TDescriptor Indices(params Type[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}
		public TDescriptor Indices(params string[] indices)
		{
			if (indices == null) return (TDescriptor)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}
		public TDescriptor Indices(IEnumerable<Type> indices)
		{
			if (indices == null) return (TDescriptor)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
			return (TDescriptor)this;
		}
		public TDescriptor Indices(IEnumerable<string> indices)
		{
			if (indices == null) return (TDescriptor)this;
			this._Indices = indices.Select(s => (IndexNameMarker)s);
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
			this._Types = types.Select(s => (TypeNameMarker)s); ;
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
			this._Types = types.Select(t => (TypeNameMarker)t);
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
			this._AllIndices = true;
			return (TDescriptor)this;
		}
		public TDescriptor AllTypes()
		{
			this._AllTypes = true;
			return (TDescriptor)this;
		}

		internal virtual ElasticsearchPathInfo<TParameters> ToPathInfo(IConnectionSettingsValues settings, TParameters queryString)
		{
			//start out with defaults
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName<T>();
			var type = inferrer.TypeName<T>();
			var pathInfo = base.ToPathInfo(queryString);
			pathInfo.Index = index;
			pathInfo.Type = type;

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

			return pathInfo;
		}

	}
}
