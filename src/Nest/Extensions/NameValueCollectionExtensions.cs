using System;
using System.Collections.Specialized;

namespace Nest
{
    public static class NameValueCollectionExtensions
    {
        public static void CopyKeyValues(this NameValueCollection self, NameValueCollection source)
        {
            foreach (var key in source.AllKeys)
            {
                if (self[key] != null) throw new ApplicationException(string.Format("Attempted to add duplicate key '{0}'", key));

                self.Add(key, source[key]);
            }
        }

        public static string ToQueryString(this NameValueCollection self)
        {
            if (self.AllKeys.Length == 0) return string.Empty;

            return "?" + string.Join("&", Array.ConvertAll(self.AllKeys, key => string.Format("{0}={1}", Uri.EscapeUriString(key), Uri.EscapeUriString(self[key]))));
        }
    }
}
