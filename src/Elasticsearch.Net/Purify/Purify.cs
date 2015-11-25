using System;
using System.Reflection;

namespace Purify
{

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
		private static readonly bool HasBrokenDotNetUri;

		private static readonly bool IsMono;

		static Purifier()
		{
			IsMono = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic) == null;
			if (IsMono)
				return;

			//ShouldUseLegacyV2Quirks was introduced in .net 4.5
			//Eventhough 4.5 is an inplace update of 4.0 this call will return 
			//a different value if an application specifically targets 4.0 or 4.5+
			var legacyV2Quirks = typeof(UriParser).GetProperty("ShouldUseLegacyV2Quirks", BindingFlags.Static | BindingFlags.NonPublic);
			if (legacyV2Quirks == null)
			{
				HasBrokenDotNetUri = true; //neither 4.0 or 4.5
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

			HasBrokenDotNetUri = !new Uri("http://google.com/%2F")
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
			if (IsMono)
				purifier = new PurifierMono();
			else if (HasBrokenDotNetUri)
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
			private static readonly FieldInfo FlagsField;
			private static readonly FieldInfo InfoField;
			private static readonly FieldInfo StringField;
			private static readonly FieldInfo InfoStringField;
			private static readonly FieldInfo MoreInfoField;
			private static readonly FieldInfo MoreInfoAbsoluteUri;
			private static readonly FieldInfo MoreInfoPath;
			private static readonly FieldInfo MoreInfoQuery;

			static PurifierDotNet()
			{
				var uriType = typeof(Uri);
				FlagsField = uriType.GetField("m_Flags", BindingFlags.NonPublic | BindingFlags.Instance);
				StringField = uriType.GetField("m_String", BindingFlags.NonPublic | BindingFlags.Instance);
				InfoField = uriType.GetField("m_Info", BindingFlags.NonPublic | BindingFlags.Instance);
				var infoFieldType = InfoField.FieldType;
				InfoStringField = infoFieldType.GetField("String", BindingFlags.Public | BindingFlags.Instance);
				MoreInfoField = infoFieldType.GetField("MoreInfo", BindingFlags.Public | BindingFlags.Instance);
				var moreInfoType = MoreInfoField.FieldType;
				MoreInfoAbsoluteUri = moreInfoType.GetField("AbsoluteUri", BindingFlags.Public | BindingFlags.Instance);
				MoreInfoQuery = moreInfoType.GetField("Query", BindingFlags.Public | BindingFlags.Instance);
				MoreInfoPath = moreInfoType.GetField("Path", BindingFlags.Public | BindingFlags.Instance);
			}

			//Code inspired by Rasmus Faber's solution in this post: http://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash
			public Uri Purify(Uri uri)
			{
				// ReSharper disable once UnusedVariable
				string paq = uri.PathAndQuery; // need to access PathAndQuery
				// ReSharper disable once UnusedVariable
				var abs = uri.AbsoluteUri; //need to access this as well the MoreInfo prop is initialized.
				ulong flags = (ulong)FlagsField.GetValue(uri);
				flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
				FlagsField.SetValue(uri, flags);
				object info = InfoField.GetValue(uri);
				var source = (string)StringField.GetValue(uri);
				InfoStringField.SetValue(info, source);
				object moreInfo = MoreInfoField.GetValue(info);
				MoreInfoAbsoluteUri.SetValue(moreInfo, source);
				var uriInfo = new UriInfo(uri, source);
				MoreInfoPath.SetValue(moreInfo, uriInfo.Path);
				MoreInfoQuery.SetValue(moreInfo, uriInfo.Query);
				return uri;
			}
		}

		private class PurifierMono : IPurifier
		{
			private static readonly Type UriType = typeof(Uri);
			private static readonly FieldInfo MonoSourceField;
			private static readonly FieldInfo MonoQueryField;
			private static readonly FieldInfo MonoPathField;
			private static readonly FieldInfo MonoCachedToStringField;
			private static readonly FieldInfo MonoCachedAbsoluteUriField;

			static PurifierMono()
			{
				MonoSourceField = UriType.GetField("source", BindingFlags.NonPublic | BindingFlags.Instance);
				MonoQueryField = UriType.GetField("query", BindingFlags.NonPublic | BindingFlags.Instance);
				MonoPathField = UriType.GetField("path", BindingFlags.NonPublic | BindingFlags.Instance);
				MonoCachedToStringField = UriType.GetField("cachedToString", BindingFlags.NonPublic | BindingFlags.Instance);
				MonoCachedAbsoluteUriField = UriType.GetField("cachedAbsoluteUri",
					BindingFlags.NonPublic | BindingFlags.Instance);
			}

			public Uri Purify(Uri uri)
			{
				var source = (string)MonoSourceField.GetValue(uri);
				MonoCachedToStringField.SetValue(uri, source);
				MonoCachedAbsoluteUriField.SetValue(uri, source);
				var uriInfo = new UriInfo(uri, source);
				MonoPathField.SetValue(uri, uriInfo.Path);
				MonoQueryField.SetValue(uri, uriInfo.Query);
				return uri;
			}
		}

		/// <summary>
		/// Class that breaks a Uri into path and query components given its orignal source
		/// </summary>
		private class UriInfo
		{
			public string Path { get; }
			public string Query { get; }

			public UriInfo(Uri uri, string source)
			{
				var fragPos = source.IndexOf("#", StringComparison.Ordinal);
				var queryPos = source.IndexOf("?", StringComparison.Ordinal);
				var start = source.IndexOf(uri.Host, StringComparison.Ordinal) + uri.Host.Length;
				var pathEnd = queryPos == -1 ? fragPos : queryPos;
				if (pathEnd == -1)
					pathEnd = source.Length + 1;
				Path = queryPos > -1 ? source.Substring(start, pathEnd - start) : source.Substring(start);
				Query = fragPos > -1 ? source.Substring(queryPos, fragPos - queryPos) : source.Substring(queryPos);
			}
		}
	}
}
