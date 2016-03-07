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
				if (dest[key] != null) throw new Exception($"Attempted to add duplicate key '{key}'");

				dest.Add(key, source[key]);
			}
		}

		internal static string ToQueryString(this NameValueCollection self, string prefix = "?")
		{
			if (self == null)
				return null;

			return self.AllKeys.Length == 0
				? string.Empty
				: $"{prefix}{string.Join("&", self.AllKeys.Select(key => $"{Encode(key)}={Encode(self[key])}"))}";
		}

		//TODO: It's not placeholder function ;)
		private static string Encode(string s) => s;
		//private static string Encode(string s) => s == null ? null : Uri.EscapeDataString(s);

		internal static NameValueCollection ToNameValueCollection(this IDictionary<string, object> originalDictionary,
			IFormatProvider provider)
		{
			if (originalDictionary == null || originalDictionary.Count < 0)
				return null;

			var nameValueCollection = new NameValueCollection();

			foreach (var keyValuePair in originalDictionary.Where(kv => !kv.Key.IsNullOrEmpty()))
				nameValueCollection.Add(keyValuePair.Key, string.Format(provider, "{0}", keyValuePair.Value));

			return nameValueCollection;
		}
	}
}