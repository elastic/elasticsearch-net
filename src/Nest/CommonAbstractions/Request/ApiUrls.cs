// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// Each Request type holds a static instance of this class which creates cached builders for each
	/// of the defined urls in the json spec.
	/// </summary>
	internal class ApiUrls
	{
		private static readonly RouteValues EmptyRouteValues = new();
		private readonly string _errorMessageSuffix;

		/// <summary>
		/// If the spec only defines a single non parameterizable route this allows us to shortcircuit and avoid hitting
		/// the cached string builders.
		/// </summary>
		private readonly string _fixedUrl;

		/// <summary>
		/// Creates a lookup for number of parts <=> list of routes with that number of parts.
		/// <see cref="UrlLookup.Matches"/> allows us to quickly find the right url to use in the list.
		/// </summary>
		public Dictionary<int, List<UrlLookup>> Routes { get; }
		
		/// <summary> Only intended to be created once per request and stored in a static </summary>
		internal ApiUrls(string[] routes)
		{
			if (routes == null || routes.Length == 0) throw new ArgumentException("urls is null or empty", nameof(routes));
			if (routes.Length == 1 && !routes[0].Contains("{")) _fixedUrl = routes[0];
			else
			{
				foreach (var route in routes)
				{
					var bracketsCount = route.Count(c => c.Equals('{'));
					if (Routes == null) Routes = new Dictionary<int, List<UrlLookup>>();
					if (Routes.ContainsKey(bracketsCount))
						Routes[bracketsCount].Add(new UrlLookup(route));
					else
						Routes.Add(bracketsCount, new List<UrlLookup> { new UrlLookup(route) });
				}
			}

			_errorMessageSuffix = string.Join(",", routes);

			// received multiple urls without brackets we resolve to first
			if (Routes == null) _fixedUrl = routes[0];
		}

		public string Resolve(RouteValues routeValues, IConnectionSettingsValues settings)
		{
			if (_fixedUrl != null) return _fixedUrl;

			if (TryHandleAllIndexSearch(routeValues, settings, out var url)) return url;

			var resolved = routeValues.Resolve(settings);

			if (!Routes.TryGetValue(resolved.Count, out var routes))
				throw new Exception($"No route taking {resolved.Count} parameters{_errorMessageSuffix}");

			if (routes.Count == 1)
				return routes[0].ToUrl(resolved);

			//find the first url with N parts that has all provided named parts
			foreach (var u in routes)
			{
				if (u.Matches(resolved))
					return u.ToUrl(resolved);
			}
			throw new Exception($"No route taking {routeValues.Count} parameters{_errorMessageSuffix}");
		}

		private bool TryHandleAllIndexSearch(RouteValues routeValues, IConnectionSettingsValues settings, out string url)
		{
			// We special case search requests against all indices to return the rooted URL (/_search) rather than /_all/_search.
			// This was introduced to support point in time searches which do not allow indices (even _all) to be specified.

			url = null;

			// If not potentially an all index search, we can exit
			if (routeValues.Count != 1
				|| !routeValues.TryGetValue("index", out var value)
				|| value.GetString(settings) != Indices.All
				|| !Routes.TryGetValue(0, out var rootRoute)) return false;

			var resolved = EmptyRouteValues.Resolve(settings);

			if (rootRoute.Count != 1) return false;

			var resolvedUrl = rootRoute[0].ToUrl(resolved);

			if (resolvedUrl != "_search") return false;

			url = resolvedUrl;
			return true;
		}
	}
}
