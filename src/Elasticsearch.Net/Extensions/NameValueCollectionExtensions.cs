using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Elasticsearch.Net
{
	internal static class NameValueCollectionExtensions
	{
		internal static string ToQueryString(this NameValueCollection self, string prefix = "?")
		{
			if (self == null)
				return null;

			if (self.AllKeys.Length == 0) return string.Empty;

			return prefix + string.Join("&", Array.ConvertAll(self.AllKeys, key => $"{Encode(key)}={Encode(self[key])}"));
		}
		private static string Encode(string s)
		{
			return s == null ? null : Uri.EscapeDataString(s);
		}

		internal static NameValueCollection ToNameValueCollection(this IDictionary<string, object> dict, IFormatProvider provider)
		{
			if (dict == null || dict.Count < 0)
				return null;
			
			var nv = new NameValueCollection();
			foreach (var kv in dict.Where(kv => !kv.Key.IsNullOrEmpty()))
			{
				nv.Add(kv.Key, string.Format(provider, "{0}", kv.Value));
			}
			return nv;
		}
	}
}
