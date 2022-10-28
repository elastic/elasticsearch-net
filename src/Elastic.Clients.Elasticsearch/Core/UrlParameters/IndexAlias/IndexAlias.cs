// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(IndexAliasConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public class IndexAlias : IEquatable<IndexAlias>, IUrlParameter
{
	internal IndexAlias(string index) => Alias = index;

	public string Alias { get; }

	internal string DebugDisplay => Alias;

	public static implicit operator IndexAlias(string dataStreamName) => new(dataStreamName);

	public static bool operator ==(IndexAlias left, IndexAlias right) => Equals(left, right);

	public static bool operator !=(IndexAlias left, IndexAlias right) => !Equals(left, right);

	bool IEquatable<IndexAlias>.Equals(IndexAlias other) => EqualsMarker(other);

	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Equals(Alias, StringComparison.Ordinal);

	public override bool Equals(object obj) => obj is string s ? EqualsString(s) : obj is IndexAlias i && EqualsMarker(i);

	string IUrlParameter.GetString(ITransportConfiguration? settings) => Alias;

	public override string ToString() => Alias ?? string.Empty;

	private bool EqualsMarker(IndexAlias other)
	{
		if (other == null)
			return false;

		return EqualsString(other.Alias);
	}

	private static int TypeHashCode { get; } = typeof(IndexAlias).GetHashCode();

	public override int GetHashCode()
	{
		unchecked
		{
			return (TypeHashCode * 397) ^ (Alias.GetHashCode());
		}
	}
}

internal sealed class IndexAliasConverter : JsonConverter<IndexAlias>
{
	public override IndexAlias? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException($"Unexpected token '{reader.TokenType}' for IndexAlias");

		return reader.GetString();
	}

	public override void Write(Utf8JsonWriter writer, IndexAlias value, JsonSerializerOptions options)
	{
		if (value is null || value.Alias is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value.Alias);
	}
}
