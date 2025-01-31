// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#nullable enable

using Elastic.Clients.Elasticsearch.Core;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

/// <summary>
/// <para>Buckets path can be expressed in different ways, and an aggregation may accept some or all of these<br/>forms depending on its type. Please refer to each aggregation's documentation to know what buckets<br/>path forms they accept.</para>
/// </summary>
[JsonConverter(typeof(BucketsPathConverter))]
public sealed partial class BucketsPath : IComplexUnion<BucketsPath.Kind>
{
	public enum Kind
	{
		Single,
		Array,
		Dictionary
	}

	internal readonly Kind _kind;
	internal readonly object _value;

	Kind IComplexUnion<Kind>.ValueKind => _kind;

	object IComplexUnion<Kind>.Value => _value;

	private BucketsPath(Kind kind, object value)
	{
		_kind = kind;
		_value = value;
	}

	public static BucketsPath Single(string single) => new(Kind.Single, single);

	public bool IsSingle => _kind == Kind.Single;

	public bool TryGetSingle([NotNullWhen(true)] out string? single)
	{
		single = null;
		if (_kind == Kind.Single)
		{
			single = (string)_value;
			return true;
		}

		return false;
	}

	public static implicit operator BucketsPath(string single) => BucketsPath.Single(single);

	public static BucketsPath Array(string[] array) => new(Kind.Array, array);

	public bool IsArray => _kind == Kind.Array;

	public bool TryGetArray([NotNullWhen(true)] out string[]? array)
	{
		array = null;
		if (_kind == Kind.Array)
		{
			array = (string[])_value;
			return true;
		}

		return false;
	}

	public static implicit operator BucketsPath(string[] array) => BucketsPath.Array(array);

	public static BucketsPath Dictionary(Dictionary<string, string> dictionary) => new(Kind.Dictionary, dictionary);

	public bool IsDictionary => _kind == Kind.Dictionary;

	public bool TryGetDictionary([NotNullWhen(true)] out Dictionary<string, string>? dictionary)
	{
		dictionary = null;
		if (_kind == Kind.Dictionary)
		{
			dictionary = (Dictionary<string, string>)_value;
			return true;
		}

		return false;
	}

	public static implicit operator BucketsPath(Dictionary<string, string> dictionary) => BucketsPath.Dictionary(dictionary);
}

internal sealed class BucketsPathConverter : JsonConverter<BucketsPath>
{
	public override BucketsPath? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		(reader.TokenType) switch
		{
			JsonTokenType.Null => null,
			JsonTokenType.String => BucketsPath.Single(JsonSerializer.Deserialize<string>(ref reader, options)!),
			JsonTokenType.StartArray => BucketsPath.Array(JsonSerializer.Deserialize<string[]>(ref reader, options)!),
			JsonTokenType.StartObject => BucketsPath.Dictionary(JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options)!),
			_ => throw new JsonException($"Unexpected token '{reader.TokenType}'.")
		};

	public override void Write(Utf8JsonWriter writer, BucketsPath value, JsonSerializerOptions options)
	{
		switch (value._kind)
		{
			case BucketsPath.Kind.Single:
				writer.WriteStringValue((string)value._value);
				break;

			case BucketsPath.Kind.Array:
				JsonSerializer.Serialize(writer, (string[])value._value, options);
				break;

			case BucketsPath.Kind.Dictionary:
				JsonSerializer.Serialize(writer, (Dictionary<string, string>)value._value, options);
				break;

			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
