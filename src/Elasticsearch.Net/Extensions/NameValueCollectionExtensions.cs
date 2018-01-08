using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Elasticsearch.Net
{
	internal static class NameValueCollectionExtensions
	{
		internal static string ToQueryString(this NameValueCollection nv, ElasticsearchUrlFormatter formatter, string prefix = "?")
		{
			if (nv == null) return null;

			if (nv.AllKeys.Length == 0) return string.Empty;
			string E(string v) => Uri.EscapeDataString(v);

			return prefix + string.Join("&", nv.AllKeys.Select(key => $"{E(key)}={nv[E(key)]}"));
		}

		internal static void SetLocalQueryString(this NameValueCollection queryString, Dictionary<string, object> queryStringUpdates, ElasticsearchUrlFormatter provider)
		{
			if (queryString == null || queryString.Count < 0) return;
			if (queryStringUpdates == null || queryStringUpdates.Count < 0) return;

			foreach (var kv in queryStringUpdates.Where(kv => !kv.Key.IsNullOrEmpty()))
			{
				if (kv.Value == null)
				{
					queryString.Remove(kv.Key);
					continue;
				}
				var resolved = provider.CreateString(kv.Value);
				if (!resolved.IsNullOrEmpty())
					queryString[kv.Key] = resolved;
				else
					queryString.Remove(kv.Key);
			}
		}
	}
}
