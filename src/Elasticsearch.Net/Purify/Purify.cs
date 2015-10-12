using System;
using System.Reflection;

namespace PurifyNet
{
    internal static class Purifier
    {

        private static readonly bool hasBrokenDotNetUri;

        private static readonly bool isMono;

        static Purifier()
        {
            isMono = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic) == null;
            if (isMono)
                return;

            //ShouldUseLegacyV2Quirks was introduced in .net 4.5
            //Eventhough 4.5 is an inplace update of 4.0 this call will return 
            //a different value if an application specifically targets 4.0 or 4.5+
            var legacyV2Quirks = typeof(UriParser).GetProperty("ShouldUseLegacyV2Quirks", BindingFlags.Static | BindingFlags.NonPublic);
            if (legacyV2Quirks == null)
            {
                hasBrokenDotNetUri = true; //neither 4.0 or 4.5
                return;
            }
            var isBrokenUri = (bool)legacyV2Quirks.GetValue(null, null);
            if (!isBrokenUri)
                return; //application targets 4.5

            //4.0 uses legacyV2quirks on the UriParser but you can set
            //  <uri>
            //    <schemeSettings>
            //      <add name="http" genericUriParserOptions="DontUnescapePathDotsAndSlashes" />
            //          </schemeSettings>
            //  </uri>
            //
            //  this will fix AbsoluteUri but not ToString()
            //  i.e new Uri("http://google.com/%2F").AbsoluteUri
            //       will return the url untouched but:
            //  new Uri("http://google.com/%2F").ToString()
            //      will still return http://google.com//
            //
            //  so instead of using reflection perform a one off function test.

            hasBrokenDotNetUri = !new Uri("http://google.com/%2F")
                .ToString()
                .EndsWith("%2F", StringComparison.InvariantCulture);

        }

        public static Uri Purify(this Uri uri)
        {
            if (!uri.IsAbsoluteUri)
                return uri;

            IPurifier purifier;
            if (isMono)
                purifier = new PurifierMono();
            else if (hasBrokenDotNetUri)
                purifier = new PurifierDotNet();
            else return uri;
           
            purifier.Purify(uri);
            return uri;
        }
    }
}

