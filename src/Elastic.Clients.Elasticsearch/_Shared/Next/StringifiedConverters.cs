// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class StringifiedBoolConverter :
	JsonConverter<bool>
{
	public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.True => true,
			JsonTokenType.False => false,
			_ => throw new JsonException($"Expected JSON '{nameof(JsonTokenType.True)}' or '{JsonTokenType.False}' or '{JsonTokenType.String}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
	{
		writer.WriteBooleanValue(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool ParseValue(ref Utf8JsonReader reader)
	{
		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out bool result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(Boolean)}'.");
	}
}

internal sealed class StringifiedIntConverter :
	JsonConverter<int>
{
	public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.Number => reader.GetInt32(),
			_ => throw new JsonException($"Expected JSON '{nameof(JsonTokenType.Number)}' or '{JsonTokenType.String}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int ParseValue(ref Utf8JsonReader reader)
	{
		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out int result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(Int32)}'.");
	}
}

internal sealed class StringifiedLongConverter :
	JsonConverter<long>
{
	public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.Number => reader.GetInt64(),
			_ => throw new JsonException($"Expected JSON '{nameof(JsonTokenType.Number)}' or '{JsonTokenType.String}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static long ParseValue(ref Utf8JsonReader reader)
	{
		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out long result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(Int64)}'.");
	}
}

internal sealed class StringifiedSingleConverter :
	JsonConverter<float>
{
	public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.Number => reader.GetSingle(),
			_ => throw new JsonException($"Expected JSON '{nameof(JsonTokenType.Number)}' or '{JsonTokenType.String}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static float ParseValue(ref Utf8JsonReader reader)
	{
		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out float result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(Single)}'.");
	}
}

internal sealed class StringifiedDoubleConverter :
	JsonConverter<double>
{
	public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.String => ParseValue(ref reader),
			JsonTokenType.Number => reader.GetDouble(),
			_ => throw new JsonException($"Expected JSON '{nameof(JsonTokenType.Number)}' or '{JsonTokenType.String}' token, but got '{reader.TokenType}'.")
		};
	}

	public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static double ParseValue(ref Utf8JsonReader reader)
	{
		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out double result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException($"Unable to convert JSON string value '{reader.GetString()!}' to '{nameof(Double)}'.");
	}
}
