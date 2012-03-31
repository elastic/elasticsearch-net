using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class PathDescriptor
	{
		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<string> _Types { get; set; }
		internal string _Routing { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }
		public PathDescriptor Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfEmpty("indices");
			this._Indices = indices;
			return this;
		}
		public PathDescriptor Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Indices = new[] { index };
			return this;
		}
		public PathDescriptor Types(IEnumerable<string> types)
		{
			types.ThrowIfEmpty("types");
			this._Types = types;
			return this;
		}
		public PathDescriptor Types(params string[] types)
		{
			return this.Types((IEnumerable<string>)types);
		}
		public PathDescriptor Types(IEnumerable<Type> types)
		{
			types.ThrowIfEmpty("types");
			return this.Types((IEnumerable<string>)types.Select(t => ElasticClient.GetTypeNameFor(t)).ToArray());
		}
		public PathDescriptor Types(params Type[] types)
		{
			return this.Types((IEnumerable<Type>)types);
		}
		public PathDescriptor Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Types = new[] { type };
			return this;
		}
		public PathDescriptor Type(Type type)
		{
			type.ThrowIfNull("type");
			return this.Type(ElasticClient.GetTypeNameFor(type));
		}
		public PathDescriptor AllIndices()
		{
			this._AllIndices = true;
			return this;
		}
		public PathDescriptor AllTypes()
		{
			this._AllTypes = true;
			return this;
		}
	}
}
