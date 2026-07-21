// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// A delegate that selects a union variant (e.g. based on the current JSON token type).
/// </summary>
/// <param name="reader">A reference to a <see cref="Utf8JsonReader"/> instance.</param>
/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
/// <returns>The selected variant index (0 = no match, 1+ = variant index).</returns>
/// <remarks>
/// IMPORTANT:
/// The <see cref="Utf8JsonReader"/> is passed by reference for best performance. If the selector function
/// implementation needs to advance the reader position, it must always operate on a copy of the original
/// <paramref name="reader"/> or restore the reader state before returning.
/// </remarks>
internal delegate int JsonUnionSelectorFunc(ref Utf8JsonReader reader, JsonSerializerOptions options);

[Flags]
internal enum JsonTokenTypes
{
	None = 0,
	StartObject = 1 << JsonTokenType.StartObject,
	EndObject = 1 << JsonTokenType.EndObject,
	StartArray = 1 << JsonTokenType.StartArray,
	EndArray = 1 << JsonTokenType.EndArray,
	String = 1 << JsonTokenType.String | 1 << JsonTokenType.PropertyName,
	Number = 1 << JsonTokenType.Number,
	True = 1 << JsonTokenType.True,
	False = 1 << JsonTokenType.False
}

internal static class JsonUnionSelector
{
	/// <summary>
	/// A selector function that always returns <c>0</c> (no match).
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <returns>A static value of <c>0</c>.</returns>
	public static int None(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		_ = reader;
		_ = options;
		return 0;
	}

	/// <summary>
	/// A selector function that always returns <paramref name="variantIndex"/>.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="variantIndex">The variant index to return (1-based).</param>
	/// <returns>The specified <paramref name="variantIndex"/>.</returns>
	public static int Variant(ref Utf8JsonReader reader, JsonSerializerOptions options, int variantIndex)
	{
		_ = reader;
		_ = options;
		return variantIndex;
	}

	/// <summary>
	/// Matches a selector function against an array of provided cases and returns the first non-zero result,
	/// or <c>0</c>, if no case matched.
	/// </summary>
	public static int Match(ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonUnionSelectorFunc case1, JsonUnionSelectorFunc case2)
	{
		var result = case1(ref reader, options);
		if (result != 0)
		{
			return result;
		}

		return case2(ref reader, options);
	}

	/// <summary>
	/// Matches a selector function against an array of provided cases and returns the first non-zero result,
	/// or <c>0</c>, if no case matched.
	/// </summary>
	public static int Match(ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonUnionSelectorFunc case1, JsonUnionSelectorFunc case2,
		JsonUnionSelectorFunc case3)
	{
		var result = case1(ref reader, options);
		if (result != 0)
		{
			return result;
		}

		result = case2(ref reader, options);
		if (result != 0)
		{
			return result;
		}

		return case3(ref reader, options);
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="types">The JSON token types to match.</param>
	/// <param name="next">The next selector function to call if the token type matches.</param>
	/// <returns>
	/// The result of <paramref name="next"/>, if the current token type of <paramref name="reader"/> matches one
	/// of the provided JSON token types or <c>0</c>, if not.
	/// </returns>
	public static int MatchTokenTypes(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonTokenTypes types,
		JsonUnionSelectorFunc next)
	{
		if (((int)types & (1 << (int)reader.TokenType)) is not 0)
		{
			return next(ref reader, options);
		}

		return 0;
	}

	/// <summary>
	/// A selector function that selects a union variant based on a property present in the current JSON object.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="name">The property name to look for.</param>
	/// <param name="next">The next selector function to call, if the property was found.</param>
	/// <returns>
	/// The result of <paramref name="next"/>, if the <paramref name="reader"/> points to an object that contains a
	/// property of the given <paramref name="name"/> or <c>0</c>, if not.
	/// </returns>
	public static int MatchProperty(ref Utf8JsonReader reader, JsonSerializerOptions options, string name,
		JsonUnionSelectorFunc next)
	{
		if (reader.TokenType is not JsonTokenType.StartObject)
		{
			return 0;
		}

		var internalReader = reader;

		while (internalReader.Read() && (internalReader.TokenType is JsonTokenType.PropertyName))
		{
			if (internalReader.ValueTextEquals(name))
			{
				return next(ref reader, options);
			}

			internalReader.Read();
			internalReader.SafeSkip();
		}

		return 0;
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="first">The JSON token types that resolve to <c>1</c>.</param>
	/// <param name="second">The JSON token types that resolve to <c>2</c>.</param>
	/// <returns>
	/// Either <c>1</c> or <c>2</c> if the current token type of <paramref name="reader"/> matches one
	/// of the provided JSON token types or <c>0</c>, if not.
	/// </returns>
	public static int ByTokenType(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonTokenTypes first,
		JsonTokenTypes second)
	{
		return Match(ref reader, options,
			(ref r, o) => MatchTokenTypes(ref r, o, first, (ref _, _) => 1),
			(ref r, o) => MatchTokenTypes(ref r, o, second, (ref _, _) => 2)
		);
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="name">The property name to look for.</param>
	/// <returns>
	/// <c>1</c> if the <paramref name="reader"/> points to an object that contains a property of the given
	/// <paramref name="name"/> or <c>2</c>, if not.
	/// </returns>
	public static int ByPropertyOfT1(ref Utf8JsonReader reader, JsonSerializerOptions options, string name)
	{
		return Match(ref reader, options,
			(ref r, o) => MatchProperty(ref r, o, name, (ref _, _) => 1),
			(ref _, _) => 2
		);
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="name">The property name to look for.</param>
	/// <returns>
	/// <c>2</c> if the <paramref name="reader"/> points to an object that contains a property of the given
	/// <paramref name="name"/> or <c>1</c>, if not.
	/// </returns>
	public static int ByPropertyOfT2(ref Utf8JsonReader reader, JsonSerializerOptions options, string name)
	{
		return Match(ref reader, options,
			(ref r, o) => MatchProperty(ref r, o, name, (ref _, _) => 2),
			(ref _, _) => 1
		);
	}
}
