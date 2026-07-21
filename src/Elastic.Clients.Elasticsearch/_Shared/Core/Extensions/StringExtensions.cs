// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch;

internal static class StringExtensions
{
	// Taken from:
	// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/Common/JsonCamelCaseNamingPolicy.cs

	internal static string ToCamelCase(this string name)
	{
		if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
		{
			return name;
		}

#if NET
		return string.Create(name.Length, name, (chars, name) =>
		{
			name.CopyTo(chars);
			FixCasing(chars);
		});
#else
		var chars = name.ToCharArray();
		FixCasing(chars);
		return new string(chars);
#endif
	}

	private static void FixCasing(Span<char> chars)
	{
		for (var i = 0; i < chars.Length; i++)
		{
			if (i == 1 && !char.IsUpper(chars[i]))
			{
				break;
			}

			var hasNext = (i + 1 < chars.Length);

			// Stop when next char is already lowercase.
			if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
			{
				// If the next char is a space, lowercase current char before exiting.
				if (chars[i + 1] == ' ')
				{
					chars[i] = char.ToLowerInvariant(chars[i]);
				}

				break;
			}

			chars[i] = char.ToLowerInvariant(chars[i]);
		}
	}
}
