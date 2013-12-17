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

	public class QueryPathDescriptor : QueryPathDescriptor<dynamic>
	{
		internal override bool AllowInfer
		{
			get { return false; }
		}
	}

	public class QueryPathDescriptor<T> : QueryPathDescriptorBase<QueryPathDescriptor<T>, T>
		where T : class
	{
		
	}

	public class QueryPathDescriptorBase<TQueryPathDescriptor, T> : 
		QueryDescriptor<T>
		where TQueryPathDescriptor : QueryPathDescriptorBase<TQueryPathDescriptor, T>, new() where T : class
	{
		protected readonly TypeNameResolver typeNameResolver;

		internal virtual bool AllowInfer { get { return true; } }

		public QueryPathDescriptorBase()
		{
			this.typeNameResolver = new TypeNameResolver();
		}

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
	
		protected override QueryDescriptor<T> Clone()
		{
			return new TQueryPathDescriptor
			{
				IsStrict = this.IsStrict,
				IsVerbatim = this.IsVerbatim,
				_AllIndices = this._AllIndices,
				_AllTypes = this._AllTypes,
				_Indices = this._Indices,
				_Types = this._Types
			};
		}
		public virtual IDictionary<string, string> GetUrlParams()
		{
			return null;
		}
	}
}
