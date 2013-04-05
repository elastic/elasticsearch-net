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
	public class RoutingQueryPathDescriptor : RoutingQueryPathDescriptor<dynamic> { }
	public class RoutingQueryPathDescriptor<T> : QueryPathDescriptor<T> , IQueryPathDescriptor where T : class
	{
		internal string _Routing { get; set; }

		public RoutingQueryPathDescriptor<T> Routing(string routing)
		{
			routing.ThrowIfNullOrEmpty("routing");
			this._Routing = routing;
			return this;
		}
		public override IDictionary<string, string> GetUrlParams()
		{
			var dict = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(this._Routing))
				dict.Add("routing", this._Routing);
			return dict;
		}

		#region newing querypath funcs for fluent interface sake
		public new RoutingQueryPathDescriptor<T> Indices(IEnumerable<string> indices)
		{
			base.Indices(indices);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Index(string index)
		{
			base.Index(index);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Types(IEnumerable<string> types)
		{
			base.Types(types);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Types(params string[] types)
		{
			base.Types(types);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Types(IEnumerable<Type> types)
		{
			base.Types(types);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Types(params Type[] types)
		{
			base.Types(types);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Type(string type)
		{
			base.Type(type);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> Type(Type type)
		{
			base.Type(type);
			return this;
		}
		public new RoutingQueryPathDescriptor<T> AllIndices()
		{
			base.AllIndices();
			return this;
		}
		public new RoutingQueryPathDescriptor<T> AllTypes()
		{
			base.AllTypes();
			return this;
		}
		#endregion
	}
}
