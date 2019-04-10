using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LookupTuple = System.ValueTuple<System.Func<Nest.RouteValues, bool>, System.Func<Nest.RouteValues, Nest.IConnectionSettingsValues, string>>;

namespace Nest
{
	internal class ApiUrls
	{
		private readonly string _fixedUrl;
		public Dictionary<int, List<LookupTuple>> Routes { get; }

		private readonly string _errorMessageSuffix;

		/// <summary> Only intended to be created once per request and stored in a static </summary>
		internal ApiUrls(string[] routes)
		{
			if (routes == null || routes.Length == 0) throw new ArgumentException(nameof(routes), "urls is null or empty");
			if (routes.Length == 1 && !routes[0].Contains("{")) _fixedUrl = routes[0];
			else
			{
				foreach (var route in routes)
				{
					var bracketsCount = route.Count(c => c.Equals('{'));
					if (Routes == null) Routes = new Dictionary<int, List<LookupTuple>>();
					if (Routes.ContainsKey(bracketsCount))
						Routes[bracketsCount].Add(FromRoute(route));
					else
						Routes.Add(bracketsCount, new List<LookupTuple>() { FromRoute(route) });
				}
			}

			_errorMessageSuffix = string.Join(",", routes);

			// received multiple urls without brackets we resolve to first
			if (Routes == null) _fixedUrl = routes[0];
		}

		private static LookupTuple FromRoute(string route)
		{
			var tokenized = route.Replace("{", "{@")
				.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

			var parts = tokenized
				.Where(p => p.StartsWith("@"))
				.Select(p => p.Remove(0, 1))
				.ToArray();

			Func<RouteValues, bool> lookup;
			Func<RouteValues, IConnectionSettingsValues, string> toString;
			lookup = r => parts.All(p => r.ContainsKey(p));
			toString = (r ,s) =>
			{
				var sb = new StringBuilder();
				var i = 0;
				foreach (var t in tokenized)
				{
					if (t[0] == '@')
					{
						if (r.TryGetValue(parts[i], out var v))
						{
							var sv = v.GetString(s);
							if (string.IsNullOrEmpty(sv))
								throw new Exception($"{parts[i]} of type ({v.GetType().Name}) did not resolve to a value  on url {route}");
							sb.Append(Uri.EscapeDataString(sv));
						}
						else throw new Exception($"No value provided for {parts[i]} on url {route}");

						i++;
					}
					else sb.Append(t);
				}
				return sb.ToString();
			};
			return (lookup, toString);
		}

		public string Resolve(RouteValues routeValues, IConnectionSettingsValues settings)
		{
			if (_fixedUrl != null) return _fixedUrl;
			if (!Routes.TryGetValue(routeValues.Count, out var routes))
				throw new Exception($"No route taking {routeValues.Count} parameters" + _errorMessageSuffix);

			if (routes.Count == 1)
				return routes[0].Item2(routeValues, settings);

			//find the first url that has all provided parameters
			foreach (var u in routes)
			{
				if (u.Item1(routeValues))
					return u.Item2(routeValues, settings);
			}
			throw new Exception($"No route taking {routeValues.Count} parameters" + _errorMessageSuffix);
		}
	}
}
