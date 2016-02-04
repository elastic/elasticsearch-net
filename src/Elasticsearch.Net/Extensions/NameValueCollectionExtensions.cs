using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Elasticsearch.Net
{
	internal static class NameValueCollectionExtensions
	{
		internal static void CopyKeyValues(this NameValueCollection source, NameValueCollection dest)
		{
			foreach (var key in source.AllKeys)
			{
				if (dest[key] != null) throw new Exception(string.Format("Attempted to add duplicate key '{0}'", key));

				dest.Add(key, source[key]);
			}
		}
		internal static string ToQueryString(this NameValueCollection self, string prefix = "?")
		{
			if (self == null)
				return null;

			if (self.AllKeys.Length == 0) return string.Empty;
			
			return prefix + string.Join("&", self.AllKeys.Select(key => $"{Encode(key)}={Encode(self[key])}"));
		}

		private static string Encode(string s) => s;
		//private static string Encode(string s) => s == null ? null : Uri.EscapeDataString(s);

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
