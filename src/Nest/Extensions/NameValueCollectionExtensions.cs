using System;
using System.Collections.Specialized;

namespace Nest
{
	public static class NameValueCollectionExtensions
	{
		public static void CopyKeyValues(this NameValueCollection source, NameValueCollection dest)
		{
			foreach (var key in source.AllKeys)
			{
				if (dest[key] != null) throw new ApplicationException(string.Format("Attempted to add duplicate key '{0}'", key));

				dest.Add(key, source[key]);
			}
		}

		public static string ToQueryString(this NameValueCollection self, string prefix = "?")
		{
			if (self.AllKeys.Length == 0) return string.Empty;

			return prefix + string.Join("&", Array.ConvertAll(self.AllKeys, key => string.Format("{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(self[key]))));
		}
	}
}
