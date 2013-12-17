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

	public class QueryPathDescriptorBase<TQueryPathDescriptor, T, K>
		where TQueryPathDescriptor : QueryPathDescriptorBase<TQueryPathDescriptor, T, K>, new() where T : class
		where K : FluentQueryString<K>, new()
	{
		internal virtual bool AllowInfer { get { return true; } }

		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public TQueryPathDescriptor Indices(IEnumerable<string> indices)
		{
			this._Indices = indices;
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor Index(string index)
		{
			this._Indices = new[] { index };
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor Types(IEnumerable<string> types)
		{
			this._Types = types.Select(s => (TypeNameMarker)s); ;
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor Types(params string[] types)
		{
			return (TQueryPathDescriptor)this.Types((IEnumerable<string>) types);
		}
		public TQueryPathDescriptor Types(IEnumerable<Type> types)
		{
			this._Types = types.Cast<TypeNameMarker>();
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor Types(params Type[] types)
		{
			return (TQueryPathDescriptor)this.Types((IEnumerable<Type>)types);
		}
		public TQueryPathDescriptor Type(string type)
		{
			this._Types = new[] { (TypeNameMarker)type };
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor Type(Type type)
		{
			this._Types = new[] { (TypeNameMarker)type };
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor AllIndices()
		{
			this._AllIndices = true;
			return (TQueryPathDescriptor)this;
		}
		public TQueryPathDescriptor AllTypes()
		{
			this._AllTypes = true;
			return (TQueryPathDescriptor)this;
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
