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

[JsonConverter(typeof(DataStreamNameConverter))]
[DebuggerDisplay("{DebugDisplay,nq}")]
public sealed class DataStreamName :
	IEquatable<DataStreamName>,
	IUrlParameter
#if NET7_0_OR_GREATER
	, IParsable<DataStreamName>
#endif
{
	internal DataStreamName(string dataStreamName) => Name = dataStreamName;

	public string Name { get; }

	internal string DebugDisplay => Name;

	public static implicit operator DataStreamName(string dataStreamName) => new(dataStreamName);

	public static bool operator ==(DataStreamName left, DataStreamName right) => Equals(left, right);

	public static bool operator !=(DataStreamName left, DataStreamName right) => !Equals(left, right);

	bool IEquatable<DataStreamName>.Equals(DataStreamName other) => EqualsMarker(other);

	private bool EqualsString(string other) => !other.IsNullOrEmpty() && other.Equals(Name, StringComparison.Ordinal);

	public override bool Equals(object obj) => obj is string s ? EqualsString(s) : obj is DataStreamName i && EqualsMarker(i);

	string IUrlParameter.GetString(ITransportConfiguration? settings) => Name;

	public override string ToString() => Name ?? string.Empty;

	private bool EqualsMarker(DataStreamName other)
	{
		if (other == null)
			return false;

		return EqualsString(other.Name);
	}

	private static int TypeHashCode { get; } = typeof(DataStreamName).GetHashCode();

	public override int GetHashCode()
	{
		unchecked
		{
			return (TypeHashCode * 23) ^ (Name.GetHashCode());
		}
	}

	#region IParsable

	public static DataStreamName Parse(string s, IFormatProvider? provider) =>
		TryParse(s, provider, out var result) ? result : throw new FormatException();

	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider,
		[NotNullWhen(true)] out DataStreamName? result)
	{
		if (s is null)
		{
			result = null;
			return false;
		}

		result = new DataStreamName(s);
		return true;
	}

	#endregion IParsable
}

internal sealed class DataStreamNameConverter : JsonConverter<DataStreamName>
{
	public override DataStreamName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException($"Unexpected token '{reader.TokenType}' for DataStreamName");

		return reader.GetString();
	}

	public override void Write(Utf8JsonWriter writer, DataStreamName value, JsonSerializerOptions options)
	{
		if (value is null || value.Name is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value.Name);
	}
}
