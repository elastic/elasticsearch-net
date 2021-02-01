// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Elasticsearch.Net;

namespace Nest
{
	internal class ResolvedRouteValues : Dictionary<string, string>
	{
		internal static ResolvedRouteValues Empty = new(0);

		public ResolvedRouteValues(int size) : base(size) { }
	}

	public class RouteValues : Dictionary<string, IUrlParameter>
	{
		/// <summary>
		/// Used specifically by index requests to determine whether to use PUT or POST.
		/// </summary>
		internal bool ContainsId { get; private set;}

		internal ResolvedRouteValues Resolve(IConnectionSettingsValues configurationValues)
		{
			if (Count == 0) return ResolvedRouteValues.Empty;
			
			var resolved = new ResolvedRouteValues(Count);
			foreach (var kv in this)
			{
				var value = this[kv.Key].GetString(configurationValues);
				if (value.IsNullOrEmpty()) continue;
				resolved[kv.Key] = value;
				if (IsId(kv.Key)) ContainsId = true;
			}
			return resolved;
		}

		private RouteValues Route(string name, IUrlParameter routeValue, bool required = true)
		{
			switch (routeValue) {
				case null when !required: {
					if (!ContainsKey(name)) return this;
					Remove(name);
					if (IsId(name)) ContainsId = false; // invalidate cache
					return this;
				}
				case null: throw new ArgumentNullException(name, $"{name} is required to build a url to this API");
				default:
					this[name] = routeValue;
					if (IsId(name)) ContainsId = false; // invalidate cache
					return this;
			}
		}

		private static bool IsId(string key) => key.Equals("id", StringComparison.OrdinalIgnoreCase);

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
