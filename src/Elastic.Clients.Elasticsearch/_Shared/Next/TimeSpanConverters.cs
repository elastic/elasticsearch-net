// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Transport.Extensions;

namespace Elastic.Clients.Elasticsearch.Serialization;

#region TimeSpanSeconds

internal sealed class TimeSpanSecondsMarker;

internal sealed class TimeSpanSecondsMarkerConverter :
	JsonConverter<TimeSpanSecondsMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public TimeSpanSecondsMarkerConverter()
	{
		WrappedConverter = new TimeSpanSecondsConverter();
	}

	public override TimeSpanSecondsMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, TimeSpanSecondsMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class TimeSpanSecondsConverter :
	JsonConverter<TimeSpan>
{
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
		{
			// Leniency for stringified numbers (sequence-safe). Int64 max is 19 digits + sign = 20; 32 with headroom.
			Span<byte> tmp = stackalloc byte[32];
			var bytes = tmp[..reader.GetValueBytes(tmp)];
			if (Utf8Parser.TryParse(bytes, out long value, out var consumed) && consumed == bytes.Length)
				return TimeSpan.FromSeconds(value);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return TimeSpan.FromSeconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value.Seconds);
	}
}

#endregion TimeSpanSeconds

#region TimeSpanMillis

internal sealed class TimeSpanMillisMarker;

internal sealed class TimeSpanMillisMarkerConverter :
	JsonConverter<TimeSpanMillisMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public TimeSpanMillisMarkerConverter()
	{
		WrappedConverter = new TimeSpanMillisConverter();
	}

	public override TimeSpanMillisMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, TimeSpanMillisMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class TimeSpanMillisConverter :
	JsonConverter<TimeSpan>
{
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
		{
			// Leniency for stringified numbers (sequence-safe). Int64 max is 19 digits + sign = 20; 32 with headroom.
			Span<byte> tmp = stackalloc byte[32];
			var bytes = tmp[..reader.GetValueBytes(tmp)];
			if (Utf8Parser.TryParse(bytes, out long value, out var consumed) && consumed == bytes.Length)
				return TimeSpan.FromMilliseconds(value);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return TimeSpan.FromMilliseconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value.Milliseconds);
	}
}

#endregion TimeSpanMillis

#region TimeSpanNanos

internal sealed class TimeSpanNanosMarker;

internal sealed class TimeSpanNanosMarkerConverter :
	JsonConverter<TimeSpanNanosMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public TimeSpanNanosMarkerConverter()
	{
		WrappedConverter = new TimeSpanNanosConverter();
	}

	public override TimeSpanNanosMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, TimeSpanNanosMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class TimeSpanNanosConverter :
	JsonConverter<TimeSpan>
{
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
		{
			// Leniency for stringified numbers (sequence-safe). Int64 max is 19 digits + sign = 20; 32 with headroom.
			Span<byte> tmp = stackalloc byte[32];
			var bytes = tmp[..reader.GetValueBytes(tmp)];
			if (Utf8Parser.TryParse(bytes, out long value, out var consumed) && consumed == bytes.Length)
				return TimeSpanHelper.FromNanoseconds(value);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return TimeSpanHelper.FromNanoseconds(reader.GetInt64());
	}

	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(TimeSpanHelper.ToNanoseconds(value));
	}
}

#endregion TimeSpanNanos

#region TimeSpanSecondsFloat

internal sealed class TimeSpanSecondsFloatMarker;

internal sealed class TimeSpanSecondsFloatMarkerConverter :
	JsonConverter<TimeSpanSecondsFloatMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public TimeSpanSecondsFloatMarkerConverter()
	{
		WrappedConverter = new TimeSpanSecondsFloatConverter();
	}

	public override TimeSpanSecondsFloatMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, TimeSpanSecondsFloatMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class TimeSpanSecondsFloatConverter :
	JsonConverter<TimeSpan>
{
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
		{
			// Leniency for stringified numbers (sequence-safe). Double round-trip is ~25 bytes; 64 with headroom.
			Span<byte> tmp = stackalloc byte[64];
			var bytes = tmp[..reader.GetValueBytes(tmp)];
			if (Utf8Parser.TryParse(bytes, out double value, out var consumed) && consumed == bytes.Length)
				return TimeSpan.FromSeconds(value);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return TimeSpan.FromSeconds(reader.GetDouble());
	}

	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value.TotalSeconds);
	}
}

#endregion TimeSpanSecondsFloat

#region TimeSpanMillisFloat

internal sealed class TimeSpanMillisFloatMarker;

internal sealed class TimeSpanMillisFloatMarkerConverter :
	JsonConverter<TimeSpanMillisFloatMarker>,
	IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }

	public TimeSpanMillisFloatMarkerConverter()
	{
		WrappedConverter = new TimeSpanMillisFloatConverter();
	}

	public override TimeSpanMillisFloatMarker Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, TimeSpanMillisFloatMarker value, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}
}

internal sealed class TimeSpanMillisFloatConverter :
	JsonConverter<TimeSpan>
{
	public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
		{
			// Leniency for stringified numbers (sequence-safe). Double round-trip is ~25 bytes; 64 with headroom.
			Span<byte> tmp = stackalloc byte[64];
			var bytes = tmp[..reader.GetValueBytes(tmp)];
			if (Utf8Parser.TryParse(bytes, out double value, out var consumed) && consumed == bytes.Length)
				return TimeSpan.FromMilliseconds(value);
		}

		reader.ValidateToken(JsonTokenType.Number);

		return TimeSpan.FromMilliseconds(reader.GetDouble());
	}

	public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
	{
		writer.WriteNumberValue(value.TotalMilliseconds);
	}
}

#endregion TimeSpanMillisFloat
