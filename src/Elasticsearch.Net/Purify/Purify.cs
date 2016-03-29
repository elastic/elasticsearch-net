using System;
using System.Globalization;
using System.Reflection;

namespace Purify
{

#if DOTNETCORE
	public static class Purifier
	{
		public static Uri Purify(this Uri uri) => uri;
	}
#else
	/// <summary>
	/// The Uri classes in .NET prior to 4.5 and Mono scrub through your Uris and modify them in order to prevent vulnerabilities, for 
	/// example escaped slashes are unescaped. This scrubbing however prevents Uris that are inline with RFC 3986. Beyond that it prevents 
	/// using .NET's HTTP clients (HttpClient and WebClient) to talk to APIs that require accessing resources using escaped 
	/// slashes unless you are using .NET 4.5.
	/// <pre>
	/// This static class allows you to purify a Uri instance so that it remains untouched across all .NET runtime versions
	/// </pre>
	/// </summary>
	public static class Purifier
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
			//
			//  i.e new Uri("http://google.com/%2F").AbsoluteUri
			//       will return the url untouched but:
			//  new Uri("http://google.com/%2F").ToString()
			//      will still return http://google.com//
			//
			//  so instead of using reflection perform a one off functional test.

			hasBrokenDotNetUri = !new Uri("http://google.com/%2F")
				.ToString()
				.EndsWith("%2F", StringComparison.InvariantCulture);
		}

		/// <summary>
		/// Will purify the <param name="uri"></param> to the unscrubed version.
		/// <pre>Calling this will be a NOOP on .NET 4.5 and up.</pre>
		/// </summary>
		/// <param name="uri">The uri to be purified</param>
		/// <returns>The purified uri</returns>
		public static Uri Purify(this Uri uri)
		{
			IPurifier purifier = null;
			if (isMono)
				purifier = new PurifierMono();
			else if (hasBrokenDotNetUri)
				purifier = new PurifierDotNet();
			else return uri;

			return purifier.Purify(uri);
		}

		private interface IPurifier
		{
			/// <summary>
			/// purifies and returns the passed <param name="uri"></param>
			/// </summary>
			Uri Purify(Uri uri);
		}

		private class PurifierDotNet : IPurifier
		{
			private static FieldInfo flagsField;
			private static FieldInfo infoField;
			private static FieldInfo stringField;
			private static FieldInfo infoStringField;
			private static FieldInfo moreInfoField;
			private static FieldInfo moreInfoAbsoluteUri;
			private static FieldInfo moreInfoPath;
			private static FieldInfo moreInfoQuery;

			static PurifierDotNet()
			{
				var uriType = typeof(Uri);
				flagsField = uriType.GetField("m_Flags", BindingFlags.NonPublic | BindingFlags.Instance);
				stringField = uriType.GetField("m_String", BindingFlags.NonPublic | BindingFlags.Instance);
				infoField = uriType.GetField("m_Info", BindingFlags.NonPublic | BindingFlags.Instance);
				var infoFieldType = infoField.FieldType;
				infoStringField = infoFieldType.GetField("String", BindingFlags.Public | BindingFlags.Instance);
				moreInfoField = infoFieldType.GetField("MoreInfo", BindingFlags.Public | BindingFlags.Instance);
				var moreInfoType = moreInfoField.FieldType;
				moreInfoAbsoluteUri = moreInfoType.GetField("AbsoluteUri", BindingFlags.Public | BindingFlags.Instance);
				moreInfoQuery = moreInfoType.GetField("Query", BindingFlags.Public | BindingFlags.Instance);
				moreInfoPath = moreInfoType.GetField("Path", BindingFlags.Public | BindingFlags.Instance);
			}

			//Code inspired by Rasmus Faber's solution in this post: http://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash
			public Uri Purify(Uri uri)
			{
				string paq = uri.PathAndQuery; // need to access PathAndQuery
				var abs = uri.AbsoluteUri; //need to access this as well the MoreInfo prop is initialized.
				ulong flags = (ulong)flagsField.GetValue(uri);
				flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
				flagsField.SetValue(uri, flags);
				object info = infoField.GetValue(uri);
				var source = (string)stringField.GetValue(uri);
				infoStringField.SetValue(info, source);
				object moreInfo = moreInfoField.GetValue(info);
				moreInfoAbsoluteUri.SetValue(moreInfo, source);
				var uriInfo = new UriInfo(uri, source);
				moreInfoPath.SetValue(moreInfo, uriInfo.Path);
				moreInfoQuery.SetValue(moreInfo, uriInfo.Query);
				return uri;
			}
		}

		private class PurifierMono : IPurifier
		{
			private static Type uriType = typeof(Uri);
			private static FieldInfo mono_sourceField;
			private static FieldInfo mono_queryField;
			private static FieldInfo mono_pathField;
			private static FieldInfo mono_cachedToStringField;
			private static FieldInfo mono_cachedAbsoluteUriField;

			static PurifierMono()
			{
				mono_sourceField = uriType.GetField("source", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_queryField = uriType.GetField("query", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_pathField = uriType.GetField("path", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_cachedToStringField = uriType.GetField("cachedToString", BindingFlags.NonPublic | BindingFlags.Instance);
				mono_cachedAbsoluteUriField = uriType.GetField("cachedAbsoluteUri",
					BindingFlags.NonPublic | BindingFlags.Instance);
			}

			public Uri Purify(Uri uri)
			{
				var source = (string)mono_sourceField.GetValue(uri);
				mono_cachedToStringField.SetValue(uri, source ?? string.Empty);
				mono_cachedAbsoluteUriField.SetValue(uri, source ?? string.Empty);
				var uriInfo = new UriInfo(uri, source);
				mono_pathField.SetValue(uri, uriInfo.Path ?? string.Empty);
				mono_queryField.SetValue(uri, uriInfo.Query ?? string.Empty);
				return uri;
			}
		}

		/// <summary>
		/// Class that breaks a Uri into path and query components given its orignal source
		/// </summary>
		private class UriInfo
		{
			public string Path { get; private set; }
			public string Query { get; private set; }

			public UriInfo(Uri uri, string source)
			{
				var fragPos = source.IndexOf("#", StringComparison.Ordinal);
				var queryPos = source.IndexOf("?", StringComparison.Ordinal);
				var start = source.IndexOf(uri.Host, StringComparison.Ordinal) + uri.Host.Length;
				var pathEnd = queryPos == -1 ? fragPos : queryPos;
				if (pathEnd == -1)
					pathEnd = source.Length + 1;

				if (start < pathEnd - 1 && source[start] == ':')
				{
					var portLength = uri.Port.ToString().Length;
					start += portLength + 1;
				}

				Path = queryPos > -1 ? source.Substring(start, pathEnd - start) : source.Substring(start);
				Query = fragPos > -1
					? source.Substring(queryPos, fragPos - queryPos)
					: queryPos > -1
						? source.Substring(queryPos, (source.Length - queryPos))
						: null;
			}
		}
	}

#endif
}
