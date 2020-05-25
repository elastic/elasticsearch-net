// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
{
	internal static class StringExtensions
	{
		internal static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s)) return s;

			if (!char.IsUpper(s[0])) return s;

			var camelCase = char.ToLowerInvariant(s[0]).ToString();
			if (s.Length > 1)
				camelCase += s.Substring(1);

			return camelCase;
		}
	}
}
