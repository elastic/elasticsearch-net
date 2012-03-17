using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class QueryPathDescriptor : QueryPathDescriptor<dynamic>
	{
		
	}
	public class QueryPathDescriptor<T> : QueryDescriptor<T> where T : class
	{
		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<string> _Types { get; set; }
		internal string _Routing { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public QueryPathDescriptor<T> Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfEmpty("indices");
			this._Indices = indices;
			return this;
		}
		public QueryPathDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Indices = new[] { index };
			return this;
		}
		public QueryPathDescriptor<T> Types(IEnumerable<string> types)
		{
			types.ThrowIfEmpty("types");
			this._Types = types;
			return this;
		}
		public QueryPathDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Types = new[] { type };
			return this;
		}
		public QueryPathDescriptor<T> AllIndices()
		{
			this._AllIndices = true;
			return this;
		}
		public QueryPathDescriptor<T> AllTypes()
		{
			this._AllTypes = true;
			return this;
		}
		public QueryPathDescriptor<T> Routing(string routing)
		{
			routing.ThrowIfNullOrEmpty("routing");
			this._Routing = routing;
			return this;
		}

	}
}
