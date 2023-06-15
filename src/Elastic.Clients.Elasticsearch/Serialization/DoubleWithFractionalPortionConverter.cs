// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

#pragma warning disable IDE0005
using System;
using System.Buffers.Text;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Elastic.Clients.Elasticsearch.Serialization.JsonConstants;
#pragma warning restore IDE0005

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class DoubleWithFractionalPortionConverter : JsonConverter<double>
{
	// We don't handle floating point literals (NaN, etc.) because for source serialization because Elasticsearch only support finite values for numeric fields.
	// We must handle the possibility of numbers as strings in the source however.

	public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String && options.NumberHandling.HasFlag(JsonNumberHandling.AllowReadingFromString))
		{
			var value = reader.GetString();

			if (!double.TryParse(value, out var parsedValue))
				ThrowHelper.ThrowJsonException($"Unable to parse '{value}' as a double.");

			return parsedValue;
		}

		return reader.GetDouble();
	}

	public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
	{
		Span<byte> utf8bytes = stackalloc byte[128]; // This is the size used in STJ for future proofing. https://github.com/dotnet/runtime/blob/dae6c2472b699b7cff2efeb5ce06b75c9551bc40/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L79

		// NOTE: This code is based on https://github.com/dotnet/runtime/blob/dae6c2472b699b7cff2efeb5ce06b75c9551bc40/src/libraries/System.Text.Json/src/System/Text/Json/JsonConstants.cs#L79

		// Frameworks that are not .NET Core 3.0 or higher do not produce round-trippable strings by
		// default. Further, the Utf8Formatter on older frameworks does not support taking a precision
		// specifier for 'G' nor does it represent other formats such as 'R'. As such, we duplicate
		// the .NET Core 3.0 logic of forwarding to the UTF16 formatter and transcoding it back to UTF8,
		// with some additional changes to remove dependencies on Span APIs which don't exist downlevel.

		// PERFORMANCE: This code could be benchmarked and tweaked to make it faster.

#if NETCOREAPP
		if (Utf8Formatter.TryFormat(value, utf8bytes, out var bytesWritten))
		{
			if (utf8bytes.IndexOfAny(NonIntegerBytes) == -1)
			{
				utf8bytes[bytesWritten++] = (byte)'.';
				utf8bytes[bytesWritten++] = (byte)'0';
			}

#pragma warning disable IDE0057 // Use range operator
			writer.WriteRawValue(utf8bytes.Slice(0, bytesWritten), skipInputValidation: true);
#pragma warning restore IDE0057 // Use range operator

			return;
		}
#else
		var utf16Text = value.ToString("G17", CultureInfo.InvariantCulture);

		if (utf16Text.Length < utf8bytes.Length)
        {
			try
			{
				var bytes = Encoding.UTF8.GetBytes(utf16Text);

				if (bytes.Length < utf8bytes.Length)
				{
					bytes.CopyTo(utf8bytes);
					return;
				}
			}
			catch
			{
				// Swallow this and fall through to our general exception.
			}
		}
#endif

		ThrowHelper.ThrowJsonException($"Unable to serialize double value.");
	}
}
