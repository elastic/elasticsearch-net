using System.Collections.Specialized;

namespace Elasticsearch.Net
{
	internal static class StringExtensions
	{
		internal static NameValueCollection ToNameValueCollection(this string queryString)
		{
			if (string.IsNullOrWhiteSpace(queryString)) return new NameValueCollection();

			var queryParameters = new NameValueCollection();
			var querySegments = queryString.Split('&');

			foreach (var segment in querySegments)
			{
				var parts = segment.Split('=');
				if (parts.Length > 0)
				{
					var key = parts[0].Trim(new char[] { '?', ' ' });
					var val = parts[1].Trim();

					queryParameters.Add(key, val);
				}
			}

			return queryParameters;
		}
		
	}
}
