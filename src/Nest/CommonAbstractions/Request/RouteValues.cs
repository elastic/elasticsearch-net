// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	internal class ResolvedRouteValues : Dictionary<string, string>
	{

	}

	public class RouteValues : Dictionary<string, IUrlParameter>
	{
		// Not too happy with this, only exists because IndexRequest needs a resolved
		// id to know if it has to send a PUT or POST.
		internal ResolvedRouteValues Resolved { get; private set; }

		internal ResolvedRouteValues Resolve(IConnectionSettingsValues configurationValues)
		{
			var resolved = new ResolvedRouteValues();
			foreach (var kv in this)
			{
				var value = this[kv.Key].GetString(configurationValues);
				if (!value.IsNullOrEmpty()) resolved[kv.Key] = value;
			}
			Resolved = resolved;
			return resolved;
		}

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			switch (routeValue) {
				case null when !required: {
					if (!ContainsKey(name)) return this;

					Remove(name);
					Resolved = null; //invalidate cache
					return this;
				}
				case null: throw new ArgumentNullException(name, $"{name} is required to build a url to this API");
				default:
					this[name] = routeValue;
					Resolved = null; //invalidate cache
					return this;
			}
		}

		internal RouteValues Required(string route, IUrlParameter value) => Route(route, value);
		
		internal RouteValues Optional(string route, IUrlParameter value) => Route(route, value, false);

		internal RouteValues Optional(string route, Metrics value) => Route(route, value, false);

		internal RouteValues Optional(string route, IndexMetrics value) => Route(route, value, false);

		internal TActual Get<TActual>(string route) 
		{
			if (TryGetValue(route, out var actual) && actual != null)
				return (TActual)actual;

			return default;
		}
	}
}
