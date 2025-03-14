// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class SingleWithFractionalPortionConverter :
	JsonConverter<float>
{
	// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L79
	public const int MaximumFormatLength = 128 + 2;

	private static readonly StandardFormat DefaultFormat = StandardFormat.Parse("G9");
	private static readonly JsonEncodedText NaN = JsonEncodedText.Encode("NaN"u8);
	private static readonly JsonEncodedText PositiveInfinity = JsonEncodedText.Encode("Infinity"u8);
	private static readonly JsonEncodedText NegativeInfinity = JsonEncodedText.Encode("-Infinity"u8);

	public static ReadOnlySpan<byte> NonIntegerChars => "E."u8;

	public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is not JsonTokenType.String)
		{
			return reader.GetSingle();
		}

		if (options.NumberHandling.HasFlag(JsonNumberHandling.AllowNamedFloatingPointLiterals))
		{
			if (reader.ValueTextEquals(NaN))
			{
				return float.NaN;
			}

			if (reader.ValueTextEquals(PositiveInfinity))
			{
				return float.PositiveInfinity;
			}

			if (reader.ValueTextEquals(NegativeInfinity))
			{
				return float.NegativeInfinity;
			}
		}

		if (!options.NumberHandling.HasFlag(JsonNumberHandling.AllowReadingFromString))
		{
			return reader.GetSingle();
		}

		Debug.Assert(!reader.HasValueSequence);

		return Utf8Parser.TryParse(reader.ValueSpan, out float result, out var consumed) && (consumed == reader.ValueSpan.Length)
			? result
			: throw new JsonException("Unable to convert JSON string value to 'float'.");
	}

	public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
	{
		if (options.NumberHandling.HasFlag(JsonNumberHandling.AllowNamedFloatingPointLiterals))
		{
			if (float.IsNaN(value))
			{
				writer.WriteStringValue(NaN);
				return;
			}

			if (float.IsPositiveInfinity(value))
			{
				writer.WriteStringValue(PositiveInfinity);
				return;
			}

			if (float.IsNegativeInfinity(value))
			{
				writer.WriteStringValue(NegativeInfinity);
				return;
			}
		}

		if (options.NumberHandling.HasFlag(JsonNumberHandling.WriteAsString))
		{
			throw new NotImplementedException("'JsonNumberHandling.WriteAsString' is currently not supported.");
		}

		Span<byte> buffer = stackalloc byte[MaximumFormatLength];

		if (!Utf8Formatter.TryFormat(value, buffer, out var bytesWritten, DefaultFormat))
		{
			throw new JsonException($"Could not convert value '{value}' to JSON number.");
		}

		if (buffer.IndexOfAny(NonIntegerChars) == -1)
		{
			buffer[bytesWritten++] = (byte)'.';
			buffer[bytesWritten++] = (byte)'0';
		}

		writer.WriteRawValue(buffer[..bytesWritten], true);
	}
}
