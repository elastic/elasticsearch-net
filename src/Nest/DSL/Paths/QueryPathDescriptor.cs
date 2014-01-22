using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{

	public class QueryPathDescriptor<T> : QueryPathDescriptor<T, BulkQueryString>
		where T : class
	{
		internal override bool AllowInfer
		{
			get { return false; }
		}
	}
	public class TempQueryPathDescriptor<K> : QueryPathDescriptor<dynamic, K>
		where K : FluentQueryString<K>, new()
	{
		internal override bool AllowInfer
		{
			get { return false; }
		}
	}

	public class QueryPathDescriptor<T, K> : QueryPathDescriptorBase<QueryPathDescriptor<T, K>, T, K>
		where T : class
		where K : FluentQueryString<K>, new()
	{
		
	}

	public class QueryPathDescriptorBase<P, T, K>
		where P : QueryPathDescriptorBase<P, T, K>, new() where T : class
		where K : FluentQueryString<K>, new()
	{
		internal virtual bool AllowInfer { get { return true; } }

		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public P Indices(IEnumerable<string> indices)
		{
			this._Indices = indices;
			return (P)this;
		}
		public P Index(string index)
		{
			this._Indices = new[] { index };
			return (P)this;
		}
		public P Types(IEnumerable<string> types)
		{
			this._Types = types.Select(s => (TypeNameMarker)s); ;
			return (P)this;
		}
		public P Types(params string[] types)
		{
			return (P)this.Types((IEnumerable<string>) types);
		}
		public P Types(IEnumerable<Type> types)
		{
			this._Types = types.Cast<TypeNameMarker>();
			return (P)this;
		}
		public P Types(params Type[] types)
		{
			return (P)this.Types((IEnumerable<Type>)types);
		}
		public P Type(string type)
		{
			this._Types = new[] { (TypeNameMarker)type };
			return (P)this;
		}
		public P Type(Type type)
		{
			this._Types = new[] { (TypeNameMarker)type };
			return (P)this;
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
	
		public virtual IDictionary<string, string> GetUrlParams()
		{
			return null;
		}	
		
		internal virtual ElasticSearchPathInfo<K> ToPathInfo<K>(IConnectionSettings settings) 
			where K : FluentQueryString<K>, new()
		{
			//start out with defaults
			var inferrer = new ElasticInferrer(settings);
			var index = inferrer.IndexName<T>();
			var type = inferrer.TypeName<T>();
			var pathInfo = new ElasticSearchPathInfo<K>()
			{
				Index = index,
				Type = type
			};

			if (this._AllTypes)
				pathInfo.Type = null;
			else if (this._Types.HasAny())
				pathInfo.Type = string.Join(",", this._Types.Select(s=>s.Resolve(settings)));

			if (this._AllIndices && pathInfo.Type == inferrer.TypeName<T>())
				pathInfo.Type = null;

			if (this._AllIndices && !pathInfo.Type.IsNullOrEmpty())
				pathInfo.Index = "_all";
			else if (this._AllIndices)
				pathInfo.Index = null;
			else if (this._Indices.HasAny())
				pathInfo.Index = string.Join(",", this._Indices);

			pathInfo.QueryString = new K();
			return pathInfo;
		}

	}
}
