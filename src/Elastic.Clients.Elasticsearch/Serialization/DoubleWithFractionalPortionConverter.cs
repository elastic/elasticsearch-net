// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if NETCOREAPP

using System.Buffers.Text;

#endif

using System.Globalization;

#if !NETCOREAPP

using System.Text;

#endif

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class DoubleWithFractionalPortionConverter : JsonConverter<double>
{
	public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			return reader.GetDouble();

		if (options.NumberHandling.HasFlag(JsonNumberHandling.AllowNamedFloatingPointLiterals))
		{
			// TODO: Handle 'reader.HasValueSequence'
			if (reader.ValueSpan.SequenceEqual(JsonConstants.LiteralNaN))
				return float.NaN;
			if (reader.ValueSpan.SequenceEqual(JsonConstants.LiteralPositiveInfinity))
				return float.PositiveInfinity;
			if (reader.ValueSpan.SequenceEqual(JsonConstants.LiteralNegativeInfinity))
				return float.NegativeInfinity;
		}

		if (options.NumberHandling.HasFlag(JsonNumberHandling.AllowReadingFromString))
		{
			var value = reader.GetString();

			if (!double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
				ThrowHelper.ThrowJsonException($"Unable to parse '{value}' as a double.");

			return result;
		}

		return reader.GetDouble();
	}

	public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
	{
		if (options.NumberHandling.HasFlag(JsonNumberHandling.AllowNamedFloatingPointLiterals))
		{
			switch (value)
			{
				case double.NaN:
					writer.WriteStringValue(JsonConstants.EncodedNaN);
					return;

				case double.PositiveInfinity:
					writer.WriteStringValue(JsonConstants.EncodedPositiveInfinity);
					return;

				case double.NegativeInfinity:
					writer.WriteStringValue(JsonConstants.EncodedNegativeInfinity);
					return;
			}
		}

		if (options.NumberHandling.HasFlag(JsonNumberHandling.WriteAsString))
		{
			// TODO: Implement as needed
			throw new NotImplementedException("The 'JsonNumberHandling.WriteAsString' is currently not supported.");
		}

		// This code is based on:
		// https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.Json/src/System/Text/Json/Writer/Utf8JsonWriter.WriteValues.Double.cs#L101

#if NETCOREAPP
		Span<byte> utf8Text = stackalloc byte[JsonConstants.MaximumFormatDoubleLength];

		if (Utf8Formatter.TryFormat(value, utf8Text, out var bytesWritten, JsonConstants.DoubleStandardFormat))
		{
			if (utf8Text.IndexOfAny(JsonConstants.NonIntegerChars) == -1)
			{
				utf8Text[bytesWritten++] = (byte)'.';
				utf8Text[bytesWritten++] = (byte)'0';
			}

			writer.WriteRawValue(utf8Text[..bytesWritten], true);
			return;
		}
#else
		var utf16Text = value.ToString(JsonConstants.DoubleFormatString, CultureInfo.InvariantCulture);
		if (utf16Text.IndexOfAny(JsonConstants.NonIntegerChars) == -1)
		{
			utf16Text += ".0";
		}

		try
		{
			var utf8Text = Encoding.UTF8.GetBytes(utf16Text);

			writer.WriteRawValue(utf8Text, true);
			return;
		}
		catch
		{
			// Swallow this and fall through to our general exception.
		}
#endif

		ThrowHelper.ThrowJsonException("Unable to serialize double value.");
	}
}
