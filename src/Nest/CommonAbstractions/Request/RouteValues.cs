using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	public class RouteValues : Dictionary<string, IUrlParameter>
	{
		// TODO Remove
		public string Id => throw new Exception("need to be refactored out");

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			switch (routeValue) {
				case null when !required: {
					if (ContainsKey(name)) Remove(name);
					return this;
				}
				case null: throw new ArgumentNullException(name, $"{name} is required to build a url to this API");
				default:
					this[name] = routeValue;
					return this;
			}
		}

		internal RouteValues Required(string route, IUrlParameter value) => Route(route, value);

		internal RouteValues Optional(string route, IUrlParameter value) => Route(route, value, false);

		internal RouteValues Optional(string route, Metrics value) => Route(route, value, false);

		internal RouteValues Optional(string route, IndexMetrics value) => Route(route, value, false);

		internal TActual Get<TActual>(string route) where TActual : class, IUrlParameter
		{
			IUrlParameter actual;
			if (TryGetValue(route, out actual) && actual != null)
				return (TActual)actual;

			return null;
		}
	}
}
