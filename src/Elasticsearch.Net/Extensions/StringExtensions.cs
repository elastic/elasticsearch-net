using System;
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
		internal static TimeSpan? ToTimeSpan(this string s)
		{
			if (s.IsNullOrEmpty())
				return null;
			long wholeNumber;
			if (long.TryParse(s, out wholeNumber))
			{
				return TimeSpan.FromMilliseconds(wholeNumber);
			}
			double decimalNumber;
			var unit = s.Substring(s.Length - 1);
			if (s.Length <= 1 || !double.TryParse(s.Substring(0, s.Length - 1), out decimalNumber))
			{
				return null;
			}
			switch (unit)
			{
				case "s":
					return TimeSpan.FromSeconds(decimalNumber);
				case "m":
					return TimeSpan.FromMinutes(decimalNumber);
				case "h":
					return TimeSpan.FromHours(decimalNumber);
				case "d":
					return TimeSpan.FromDays(decimalNumber);
				case "w":
					return TimeSpan.FromDays(decimalNumber * 7);
				default:
					return null;
			}
		}
	}
}
