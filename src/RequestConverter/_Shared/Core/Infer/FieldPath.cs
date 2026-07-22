// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text;

namespace RequestConverter;

/// <summary>
/// Maps an Elasticsearch dotted field path (e.g. <c>user.address.city</c>, <c>title.keyword</c>) to the body of a
/// member-access lambda against the placeholder document type, e.g. <c>x =&gt; x.User.Address.City</c>. Each
/// dot-segment becomes a PascalCased property name. Used only by <see cref="FormattingOptions.UseStronglyTypedDocument"/>.
/// </summary>
internal static class FieldPath
{
	/// <summary>
	/// Builds the member-access lambda body for <paramref name="fieldPath"/>. Returns <c>false</c> (and leaves
	/// <paramref name="lambdaBody"/> null) when any segment cannot be expressed as a C# member - for example an empty
	/// segment, a segment starting with a digit, or one containing characters that are not part of an identifier - so the
	/// caller can fall back to emitting the raw string.
	/// </summary>
	public static bool TryToLambdaBody(string? fieldPath, out string? lambdaBody)
	{
		lambdaBody = null;

		if (string.IsNullOrEmpty(fieldPath))
			return false;

		var builder = new StringBuilder("x => x");
		foreach (var segment in fieldPath!.Split('.'))
		{
			if (!TryToProperty(segment, out var property))
				return false;

			builder.Append('.').Append(property);
		}

		lambdaBody = builder.ToString();
		return true;
	}

	/// <summary>
	/// PascalCases a single JSON object key into a C# property name (e.g. <c>post_date</c> -&gt; <c>PostDate</c>,
	/// <c>@timestamp</c> -&gt; <c>Timestamp</c>). Falls back to the raw key when it cannot form a valid identifier;
	/// the output is illustrative, so a non-compiling key is acceptable rather than dropping the property.
	/// </summary>
	public static string ToPropertyName(string key) =>
		TryToProperty(key, out var property) ? property! : key;

	// One dotted segment -> a PascalCased property name. Strips a single leading metadata marker (`_id` -> `Id`,
	// `@timestamp` -> `Timestamp`), splits on `_`/`-`, and capitalizes each sub-token's first letter while preserving
	// any inner capitals so an already-camelCase token survives intact.
	private static bool TryToProperty(string segment, out string? property)
	{
		property = null;

		if (segment.Length == 0)
			return false;

		var trimmed = segment[0] is '_' or '@' or '$' ? segment[1..] : segment;
		if (trimmed.Length == 0)
			return false;

		var builder = new StringBuilder(trimmed.Length);
		foreach (var token in trimmed.Split('_', '-'))
		{
			if (token.Length == 0)
				continue;

			builder.Append(char.ToUpperInvariant(token[0]));
			if (token.Length > 1)
				builder.Append(token, 1, token.Length - 1);
		}

		if (builder.Length == 0)
			return false;

		var candidate = builder.ToString();
		if (!IsValidIdentifier(candidate))
			return false;

		property = candidate;
		return true;
	}

	private static bool IsValidIdentifier(string value)
	{
		if (!(char.IsLetter(value[0]) || value[0] == '_'))
			return false;

		foreach (var c in value)
		{
			if (!(char.IsLetterOrDigit(c) || c == '_'))
				return false;
		}

		return true;
	}
}
