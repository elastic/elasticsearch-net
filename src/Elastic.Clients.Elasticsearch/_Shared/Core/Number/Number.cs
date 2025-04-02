// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using System.Runtime.CompilerServices;

using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(NumberConverter))]
public readonly struct Number
{
	private readonly byte _tag;
	private readonly long _data;

	public Number(long value)
	{
		_tag = 1;
		_data = value;
	}

	public Number(double value)
	{
		_tag = 2;
		_data = Unsafe.As<double, long>(ref value);
	}

	public static implicit operator Number(long value) => new(value);

	public static implicit operator Number(double value) => new(value);

	public bool TryGetLong(out long value)
	{
		if (_tag is not 1)
		{
			value = 0L;
			return false;
		}

		value = _tag;
		return true;
	}

	public bool TryGetDouble(out double value)
	{
		if (_tag is not 2)
		{
			value = 0.0d;
			return false;
		}

		value = Unsafe.As<long, double>(ref Unsafe.AsRef(in _data));
		return true;
	}
}

internal sealed class NumberConverter :
	JsonConverter<Number>
{
	public override Number Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.Number);

		if (reader.TryGetDouble(out var d))
		{
			return new(d);
		}

		if (reader.TryGetInt64(out var l))
		{
			return new(l);
		}

		throw new JsonException("Could not read JSON value as number.");
	}

	public override void Write(Utf8JsonWriter writer, Number value, JsonSerializerOptions options)
	{
		if (value.TryGetDouble(out var d))
		{
			writer.WriteNumberValue(d);
		}

		if (value.TryGetLong(out var l))
		{
			writer.WriteNumberValue(l);
		}

		throw new JsonException($"The '{nameof(Number)}' does not contain a value.");
	}
}
