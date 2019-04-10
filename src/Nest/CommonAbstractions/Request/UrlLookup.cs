using System;
using System.Linq;
using System.Text;

namespace Nest {
	internal class UrlLookup
	{
		private UrlLookup(Func<ResolvedRouteValues, bool> lookup, Func<ResolvedRouteValues, IConnectionSettingsValues, string> toString) =>
			(Predicate, ToUrl) = (lookup, toString);

		public Func<ResolvedRouteValues, bool> Predicate { get; set; }
		public Func<ResolvedRouteValues, IConnectionSettingsValues, string> ToUrl { get; set; }
		
		public static UrlLookup FromRoute(string route)
		{
			var tokenized = route.Replace("{", "{@")
				.Split(new[] { '{', '}' }, StringSplitOptions.RemoveEmptyEntries);

			var parts = tokenized
				.Where(p => p.StartsWith("@"))
				.Select(p => p.Remove(0, 1))
				.ToArray();

			Func<ResolvedRouteValues, bool> lookup;
			Func<ResolvedRouteValues, IConnectionSettingsValues, string> toString;
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
							if (string.IsNullOrEmpty(v))
								throw new Exception($"'{parts[i]}' defined but is empty on url: {route}");
							sb.Append(Uri.EscapeDataString(v));
						}
						else throw new Exception($"No value provided for '{parts[i]}' on url: {route}");

						i++;
					}
					else sb.Append(t);
				}
				return sb.ToString();
			};
			return new UrlLookup(lookup, toString);
		}
	}
}