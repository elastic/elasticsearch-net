using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Extensions
{
	internal static class NameValueCollectionExtensions
	{
		internal static string ToQueryString(this NameValueCollection nv)
		{
			if (nv == null || nv.AllKeys.Length == 0) return string.Empty;

			// initialize with capacity for number of key/values with length 5 each
			var builder = new StringBuilder("?", nv.AllKeys.Length * 2 * 5);
			for (var i = 0; i < nv.AllKeys.Length; i++)
			{
				if (i != 0)
					builder.Append("&");

				var key = nv.AllKeys[i];
				builder.Append(Uri.EscapeDataString(key));
				var value = nv[key];
				if (!value.IsNullOrEmpty())
				{
					builder.Append("=");
					builder.Append(Uri.EscapeDataString(nv[key]));
				}
			}

			return builder.ToString();
		}

		internal static void UpdateFromDictionary(this NameValueCollection queryString, Dictionary<string, object> queryStringUpdates,
			ElasticsearchUrlFormatter provider
		)
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
				if (resolved != null)
					queryString[kv.Key] = resolved;
				else
					queryString.Remove(kv.Key);
			}
		}
	}
}
