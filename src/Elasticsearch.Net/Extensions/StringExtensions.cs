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
			try
			{
				long millis;
				if (s.EndsWith("S"))
				{
					millis = long.Parse(s.Substring(0, s.Length - 1));
				}
				else if (s.EndsWith("ms"))
				{
					millis = (long)(double.Parse(s.Substring(0, s.Length - 2)));
				}
				else if (s.EndsWith("s"))
				{
					millis = (long)(double.Parse(s.Substring(0, s.Length - 1)) * 1000);
				}
				else if (s.EndsWith("m"))
				{
					millis = (long)(double.Parse(s.Substring(0, s.Length - 1)) * 60 * 1000);
				}
				else if (s.EndsWith("H") || s.EndsWith("h"))
				{
					millis = (long)(double.Parse(s.Substring(0, s.Length - 1)) * 60 * 60 * 1000);
				}
				else if (s.EndsWith("d"))
				{
					millis = (long)(double.Parse(s.Substring(0, s.Length - 1)) * 24 * 60 * 60 * 1000);
				}
				else if (s.EndsWith("w"))
				{
					millis = (long)(double.Parse(s.Substring(0, s.Length - 1)) * 7 * 24 * 60 * 60 * 1000);
				}
				else
				{
					millis = long.Parse(s);
				}
				return TimeSpan.FromMilliseconds(millis);
			}
			catch (FormatException)
			{
				return null;
			}
			catch (OverflowException)
			{
				return null;
			}
		}
	}
}
