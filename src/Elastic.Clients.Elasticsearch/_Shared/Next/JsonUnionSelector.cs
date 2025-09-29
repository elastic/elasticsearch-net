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
/// <returns>The selected <see cref="UnionTag"/> value.</returns>
/// <remarks>
/// IMPORTANT:
/// The <see cref="Utf8JsonReader"/> is passed by reference for best performance. If the selector function
/// implementation needs to advance the reader position, it must always operate on a copy of the original
/// <paramref name="reader"/> or restore the reader state before returning.
/// </remarks>
internal delegate UnionTag JsonUnionSelectorFunc(ref Utf8JsonReader reader, JsonSerializerOptions options);

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
	/// A selector function that always returns <see cref="UnionTag.None"/>.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <returns>A static value of <see cref="UnionTag.None"/>.</returns>
	public static UnionTag None(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		_ = reader;
		_ = options;
		return UnionTag.None;
	}

	/// <summary>
	/// A selector function that always returns <see cref="UnionTag.T1"/>.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <returns>A static value of <see cref="UnionTag.T1"/>.</returns>
	public static UnionTag T1(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		_ = reader;
		_ = options;
		return UnionTag.T1;
	}

	/// <summary>
	/// A selector function that always returns <see cref="UnionTag.T2"/>.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <returns>A static value of <see cref="UnionTag.T2"/>.</returns>
	public static UnionTag T2(ref Utf8JsonReader reader, JsonSerializerOptions options)
	{
		_ = reader;
		_ = options;
		return UnionTag.T2;
	}

	// We avoid using a `params` array for performance reasons. Create `Match()` overloads with additional parameters as needed.
	public static UnionTag Match(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonUnionSelectorFunc case1, JsonUnionSelectorFunc case2)
	{
		if (case1(ref reader, options) is var tag1 and not UnionTag.None)
		{
			return tag1;
		}

		if (case2(ref reader, options) is var tag2 and not UnionTag.None)
		{
			return tag2;
		}

		return UnionTag.None;
	}

	public static UnionTag MatchTokenTypes(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonTokenTypes types, JsonUnionSelectorFunc next)
	{
		if (((int)types & (1 << (int)reader.TokenType)) is not 0)
		{
			return next(ref reader, options);
		}

		return UnionTag.None;
	}

	public static UnionTag MatchProperty(ref Utf8JsonReader reader, JsonSerializerOptions options, string name, UnionTag result)
	{
		if (reader.TokenType is not JsonTokenType.StartObject)
		{
			return UnionTag.None;
		}

		var internalReader = reader;

		while (internalReader.Read() && (internalReader.TokenType is JsonTokenType.PropertyName))
		{
			if (internalReader.ValueTextEquals(name))
			{
				return result;
			}

			internalReader.Read();
			internalReader.SafeSkip();
		}

		return UnionTag.None;
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="first">The JSON token types that resolve to <see cref="UnionTag.T1"/>.</param>
	/// <param name="second">The JSON token types that resolve to <see cref="UnionTag.T2"/>.</param>
	/// <returns>
	/// Either <see cref="UnionTag.T1"/> or <see cref="UnionTag.T2"/> if the current token type of <paramref name="reader"/> matches one
	/// of the provided JSON token types or <see cref="UnionTag.None"/>, if not.
	/// </returns>
	public static UnionTag ByTokenType(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonTokenTypes first, JsonTokenTypes second)
	{
		return Match(ref reader, options,
			(ref r, o) => MatchTokenTypes(ref r, o, first, T1),
			(ref r, o) => MatchTokenTypes(ref r, o, second, T2)
		);
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="name">The property name to look for.</param>
	/// <returns>
	/// <see cref="UnionTag.T1"/> if the <paramref name="reader"/> points to an object that contains a property of the given
	/// <paramref name="name"/> or <see cref="UnionTag.T2"/>, if not.
	/// </returns>
	public static UnionTag ByPropertyOfT1(ref Utf8JsonReader reader, JsonSerializerOptions options, string name)
	{
		return Match(ref reader, options,
			(ref r, o) => MatchProperty(ref r, o, name, UnionTag.T1),
			T2
		 );
	}

	/// <summary>
	/// A selector function that selects a union variant based on the current JSON token type.
	/// </summary>
	/// <param name="reader">A reference to the <see cref="Utf8JsonReader"/> instance.</param>
	/// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
	/// <param name="name">The property name to look for.</param>
	/// <returns>
	/// <see cref="UnionTag.T2"/> if the <paramref name="reader"/> points to an object that contains a property of the given
	/// <paramref name="name"/> or <see cref="UnionTag.T1"/>, if not.
	/// </returns>
	public static UnionTag ByPropertyOfT2(ref Utf8JsonReader reader, JsonSerializerOptions options, string name)
	{
		return Match(ref reader, options,
			(ref r, o) => MatchProperty(ref r, o, name, UnionTag.T2),
			T1
		);
	}
}
