// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

internal static class StringExtensions
{
	internal static string ToCamelCase(this string s)
	{
		if (string.IsNullOrEmpty(s))
			return s;

		if (!char.IsUpper(s[0]))
			return s;

		var chars = s.ToCharArray();

		for (var i = 0; i < chars.Length; i++)
		{
			var current = chars[i];

			if (char.IsSeparator(current))
				break;

			if (0 < i && i + 1 < chars.Length)
			{
				var next = chars[i + 1];
				if (!char.IsUpper(next) && !char.IsSeparator(next))
					break;
			}

			chars[i] = char.ToLowerInvariant(current);
		}

		return new string(chars);
	}
}
