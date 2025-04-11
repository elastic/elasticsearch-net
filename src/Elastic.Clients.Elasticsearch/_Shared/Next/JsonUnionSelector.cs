// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
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
	public static UnionTag ByTokenType(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonTokenTypes first, JsonTokenTypes second)
	{
		_ = reader;
		_ = options;

		if (((int)first & (1 << (int)reader.TokenType)) is not 0)
		{
			return UnionTag.T1;
		}

		if (((int)second & (1 << (int)reader.TokenType)) is not 0)
		{
			return UnionTag.T2;
		}

		return UnionTag.None;
	}

	public static UnionTag ByPropertyOfT1(ref Utf8JsonReader reader, JsonSerializerOptions options, string name)
	{
		reader.ValidateToken(JsonTokenType.StartObject);

		var internalReader = reader;

		while (internalReader.Read() && (internalReader.TokenType is JsonTokenType.PropertyName))
		{
			if (internalReader.ValueTextEquals(name))
			{
				return UnionTag.T1;
			}

			internalReader.Read();
			internalReader.Skip();
		}

		return UnionTag.T2;
	}

	public static UnionTag ByPropertyOfT2(ref Utf8JsonReader reader, JsonSerializerOptions options, string name)
	{
		reader.ValidateToken(JsonTokenType.StartObject);

		var internalReader = reader;

		while (internalReader.Read() && (internalReader.TokenType is JsonTokenType.PropertyName))
		{
			if (internalReader.ValueTextEquals(name))
			{
				return UnionTag.T2;
			}

			internalReader.Read();
			internalReader.Skip();
		}

		return UnionTag.T1;
	}
}
