using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace Shared.Extensions
{
	public static class StringExtensions
	{
		public static NameValueCollection ToNameValueCollection(this string queryString)
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

		public static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s))
				return s;

			if (!char.IsUpper(s[0]))
				return s;

			string camelCase = char.ToLower(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
			if (s.Length > 1)
				camelCase += s.Substring(1);

			return camelCase;
		}

		public static byte[] Utf8Bytes(this string s)
		{
			return s.IsNullOrEmpty() ? null : Encoding.UTF8.GetBytes(s);
		}

		public static string Utf8String(this byte[] bytes)
		{
			return bytes == null ? null : Encoding.UTF8.GetString(bytes);
		}

		public static void ThrowIfNullOrEmpty(this string @object, string parameterName)
		{
			@object.ThrowIfNull(parameterName);
			if (string.IsNullOrWhiteSpace(@object))
				throw new ArgumentException("Argument can't be null or empty", parameterName);
		}

		public static string F(this string format, params object[] args)
		{
			format.ThrowIfNull("format");
			return string.Format(format, args);
		}

		public static string EscapedFormat(this string format, params object[] args)
		{
			format.ThrowIfNull("format");
			var arguments = new List<object>();
			foreach (var a in args)
			{
				var s = a as string;
				arguments.Add(s != null ? Uri.EscapeDataString(s) : a);
			}
			return string.Format(format, arguments.ToArray());
		}

		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}
	}
}
