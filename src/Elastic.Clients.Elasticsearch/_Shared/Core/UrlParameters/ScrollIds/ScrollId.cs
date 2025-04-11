// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(ScrollIdConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public sealed class ScrollId :
	IEquatable<ScrollId>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<ScrollId>
#endif
{
	internal ScrollId(string scrollId) => Id = scrollId;

	public string Id { get; }

	internal string DebugDisplay => Id;

	public static implicit operator ScrollId(string scrollId) => new(scrollId);

	public static bool operator ==(ScrollId left, ScrollId right) => Equals(left, right);

	public static bool operator !=(ScrollId left, ScrollId right) => !Equals(left, right);

	bool IEquatable<ScrollId>.Equals(ScrollId other) => EqualsMarker(other);

	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Equals(Id, StringComparison.Ordinal);

	public override bool Equals(object obj) => obj is string s ? EqualsString(s) : obj is ScrollId i && EqualsMarker(i);

	string IUrlParameter.GetString(ITransportConfiguration? settings) => Id;

	public override string ToString() => Id ?? string.Empty;

	private bool EqualsMarker(ScrollId other)
	{
		if (other == null)
			return false;

		return EqualsString(other.Id);
	}

	private static int TypeHashCode { get; } = typeof(ScrollId).GetHashCode();

	public override int GetHashCode()
	{
		unchecked
		{
			return (TypeHashCode * 23) ^ (Id.GetHashCode());
		}
	}

	#region IParsable

	public static ScrollId Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out ScrollId? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		result = new ScrollId(s);
		return true;
	}

	#endregion IParsable
}

internal sealed class ScrollIdConverter : JsonConverter<ScrollId>
{
	public override ScrollId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException($"Unexpected token '{reader.TokenType}' for DataStreamName");

		return reader.GetString();
	}

	public override void Write(Utf8JsonWriter writer, ScrollId value, JsonSerializerOptions options)
	{
		if (value is null || value.Id is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value.Id);
	}
}
