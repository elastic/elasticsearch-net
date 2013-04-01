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
	public abstract class QueryPathDescriptor : QueryPathDescriptor<dynamic>
	{

	}
	public abstract class QueryPathDescriptor<T> : QueryDescriptor<T>, IQueryPathDescriptor where T : class
	{
		protected readonly TypeNameResolver typeNameResolver;

		public QueryPathDescriptor()
		{
			this.typeNameResolver = new TypeNameResolver();
		}

		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<TypeNameMarker> _Types { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public IQueryPathDescriptor Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfEmpty("indices");
			this._Indices = indices;
			return this;
		}
		public IQueryPathDescriptor Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Indices = new[] { index };
			return this;
		}
		public IQueryPathDescriptor Types(IEnumerable<string> types)
		{
			types.ThrowIfEmpty("types");
			this._Types = types.Cast<TypeNameMarker>();
			return this;
		}
		public IQueryPathDescriptor Types(params string[] types)
		{
			return this.Types((IEnumerable<string>) types);
		}
		public IQueryPathDescriptor Types(IEnumerable<Type> types)
		{
			types.ThrowIfEmpty("types");
			this._Types = types.Cast<TypeNameMarker>();
			return this;
		}
		public IQueryPathDescriptor Types(params Type[] types)
		{
			return this.Types((IEnumerable<Type>)types);
		}
		public IQueryPathDescriptor Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Types = new[] { (TypeNameMarker)type };
			return this;
		}
		public IQueryPathDescriptor Type(Type type)
		{
			type.ThrowIfNull("type");
			this._Types = new[] { (TypeNameMarker)type };
			return this;
		}
		public IQueryPathDescriptor AllIndices()
		{
			this._AllIndices = true;
			return this;
		}
		public IQueryPathDescriptor AllTypes()
		{
			this._AllTypes = true;
			return this;
		}
	
		public virtual IDictionary<string, string> GetUrlParams()
		{
			return null;
		}
	}

	public interface IQueryPathDescriptor
	{
		IQueryPathDescriptor Indices(IEnumerable<string> indices);
		IQueryPathDescriptor Index(string index);
		IQueryPathDescriptor Types(IEnumerable<string> types);
		IQueryPathDescriptor Types(params string[] types);
		IQueryPathDescriptor Types(IEnumerable<Type> types);
		IQueryPathDescriptor Types(params Type[] types);
		IQueryPathDescriptor Type(string type);
		IQueryPathDescriptor Type(Type type);
		IQueryPathDescriptor AllIndices();
		IQueryPathDescriptor AllTypes();
	}
}
