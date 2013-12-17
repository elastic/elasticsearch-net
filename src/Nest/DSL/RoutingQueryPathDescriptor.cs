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
	public class RoutingQueryPathDescriptor<T> : QueryPathDescriptorBase<RoutingQueryPathDescriptor<T>, T>
		where T : class
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
	}
}
