// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

[DebuggerDisplay("{DebugDisplay,nq}")]
[JsonConverter(typeof(NameConverter))]
public sealed class Name :
	IEquatable<Name>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<Name>
#endif
{
	public Name(string name)
	{
		if (name is null)
		{
			throw new ArgumentNullException(nameof(name));
		}

		Value = name.Trim();
	}

	internal string Value { get; }

	private string DebugDisplay => Value;

	private static int TypeHashCode { get; } = typeof(Name).GetHashCode();

	public bool Equals(Name other) => EqualsString(other?.Value);

	string IUrlParameter.GetString(ITransportConfiguration? settings) => Value;

	public override string ToString() => Value;

	public static implicit operator Name(string name) => name.IsNullOrEmpty() ? null : new Name(name);

	public static bool operator ==(Name left, Name right) => Equals(left, right);

	public static bool operator !=(Name left, Name right) => !Equals(left, right);

	public override bool Equals(object obj) =>
		obj is string s ? EqualsString(s) : obj is Name i && EqualsString(i.Value);

	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Trim() == Value;

	public override int GetHashCode()
	{
		unchecked
		{
			var result = TypeHashCode;
			result = (result * 397) ^ (Value?.GetHashCode() ?? 0);
			return result;
		}
	}

	#region IParsable

	public static Name Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out Name? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		result = new Name(s);
		return true;
	}

	#endregion IParsable
}

internal sealed class NameConverter :
	JsonConverter<Name>
{
	public override Name? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.String);

		return reader.GetString()!;
	}

	public override Name ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.PropertyName);

		return reader.GetString()!;
	}

	public override void Write(Utf8JsonWriter writer, Name value, JsonSerializerOptions options)
	{
		if (value?.Value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		writer.WriteStringValue(value.Value);
	}

	public override void WriteAsPropertyName(Utf8JsonWriter writer, Name value, JsonSerializerOptions options)
	{
		if (value?.Value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		writer.WritePropertyName(value.Value);
	}
}
