// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// The encoding to use when serializing vector data using the <see cref="FloatVectorDataConverter"/> converter.
/// </summary>
public enum FloatVectorDataEncoding
{
	/// <summary>
	/// Legacy (JSON array) vector encoding for backwards compatibility.
	/// </summary>
	Legacy,

	/// <summary>
	/// <c>Base64</c> vector encoding.
	/// </summary>
	/// <remarks>
	///	<c>Base64</c> encoding is available starting from Elasticsearch 9.3.0.
	/// </remarks>
	Base64
}

public sealed class FloatVectorDataConverter :
	JsonConverter<ReadOnlyMemory<float>>
{
	private FloatVectorDataEncoding? _encoding;

	public override ReadOnlyMemory<float> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.StartArray => new(reader.ReadCollectionValue<float>(options, null)!.ToArray()),
			JsonTokenType.String => ReadBase64VectorData(ref reader),
			_ => throw reader.UnexpectedTokenException(JsonTokenType.StartArray, JsonTokenType.String)
		};
	}

	public override void Write(Utf8JsonWriter writer, ReadOnlyMemory<float> value, JsonSerializerOptions options)
	{
		var encoding = _encoding;
		if (encoding is null)
		{
			var settings = ContextProvider<IElasticsearchClientSettings>.GetContext(options);
			_encoding = settings.FloatVectorDataEncoding;
		}

		switch (_encoding)
		{
			case FloatVectorDataEncoding.Legacy:
				writer.WriteSpanValue(options, value.Span, null);
				break;

			case FloatVectorDataEncoding.Base64:
				WriteBase64VectorData(writer, value);
				break;

			default:
				throw new NotSupportedException();
		}
	}

	private static ReadOnlyMemory<float> ReadBase64VectorData(ref Utf8JsonReader reader)
	{
		var bytes = reader.GetBytesFromBase64();

		if ((bytes.Length & 3) != 0)
		{
			throw new ArgumentException("Decoded vector data length is not a multiple of 4 (not valid 32-bit floats).");
		}

		var span = bytes.AsSpan();

		if (BitConverter.IsLittleEndian)
		{
			// Host is little-endian. We must swap the byte order.

			var intSourceDest = MemoryMarshal.Cast<byte, int>(span);

			for (var i = 0; i < intSourceDest.Length; i++)
			{
				intSourceDest[i] = BinaryPrimitives.ReverseEndianness(intSourceDest[i]);
			}
		}

		var result = new float[bytes.Length / 4];
		Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);

		return new(result);
	}

	private static void WriteBase64VectorData(Utf8JsonWriter writer, ReadOnlyMemory<float> value)
	{
		if (value.IsEmpty)
		{
			writer.WriteStringValue(string.Empty);
			return;
		}

		// If the host is big-endian we can reinterpret the memory as bytes without copying.
		if (!BitConverter.IsLittleEndian)
		{
			writer.WriteBase64StringValue(MemoryMarshal.AsBytes(value.Span));
		}

		// Host is little-endian. We must swap the byte order.

		var pool = MemoryPool<byte>.Shared;
		var required = checked(value.Length * sizeof(float));
		var owner = pool.Rent(required);

		try
		{
			var dest = owner.Memory.Span[..required];

			var intSource = MemoryMarshal.Cast<float, int>(value.Span);
			var intDest = MemoryMarshal.Cast<byte, int>(dest);

			for (var i = 0; i < intSource.Length; i++)
			{
				intDest[i] = BinaryPrimitives.ReverseEndianness(intSource[i]);
			}

			writer.WriteBase64StringValue(dest);
		}
		finally
		{
			owner.Dispose();
		}
	}
}

/// <summary>
/// The encoding to use when serializing vector data using the <see cref="ByteVectorDataConverter"/> converter.
/// </summary>
public enum ByteVectorDataEncoding
{
	/// <summary>
	/// Legacy (JSON array) vector encoding for backwards compatibility.
	/// </summary>
	Legacy,

	/// <summary>
	/// Hexadecimal string vector encoding.
	/// </summary>
	/// <remarks>
	///	Hexadecimal encoding is available starting from Elasticsearch 8.14.0.
	/// </remarks>
	Hex,

	/// <summary>
	/// <c>Base64</c> vector encoding.
	/// </summary>
	/// <remarks>
	///	<c>Base64</c> encoding is available starting from Elasticsearch 9.3.0.
	/// </remarks>
	Base64
}

public sealed class ByteVectorDataConverter :
	JsonConverter<ReadOnlyMemory<byte>>
{
	private ByteVectorDataEncoding? _encoding;

	public override ReadOnlyMemory<byte> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.StartArray => new(reader.ReadCollectionValue(options, (ref r, _) => unchecked((byte)r.GetSByte()))!.ToArray()),
			JsonTokenType.String => ReadStringVectorData(ref reader),
			_ => throw reader.UnexpectedTokenException(JsonTokenType.StartArray, JsonTokenType.String)
		};
	}

	public override void Write(Utf8JsonWriter writer, ReadOnlyMemory<byte> value, JsonSerializerOptions options)
	{
		if (_encoding is null)
		{
			var settings = ContextProvider<IElasticsearchClientSettings>.GetContext(options);
			_encoding = settings.ByteVectorDataEncoding;
		}

		switch (_encoding)
		{
			case ByteVectorDataEncoding.Legacy:
				writer.WriteSpanValue(options, value.Span, (w, _, b) => w.WriteNumberValue(unchecked((sbyte)b)));
				break;

			case ByteVectorDataEncoding.Hex:
				WriteHexVectorData(writer, value);
				break;

			case ByteVectorDataEncoding.Base64:
				writer.WriteBase64StringValue(value.Span);
				break;

			default:
				throw new NotSupportedException();
		}
	}

	private static ReadOnlyMemory<byte> ReadStringVectorData(ref Utf8JsonReader reader)
	{
		if (reader.TryGetBytesFromBase64(out var result))
		{
			return result;
		}

		return ReadHexVectorData(ref reader);
	}

	private static ReadOnlyMemory<byte> ReadHexVectorData(ref Utf8JsonReader reader)
	{
#if NET5_0_OR_GREATER
		var data = Convert.FromHexString(reader.GetString()!);
#else
		var data = FromHex(reader.GetString()!);
#endif

		return new(data);
	}

	private static void WriteHexVectorData(Utf8JsonWriter writer, ReadOnlyMemory<byte> value)
	{
		if (value.IsEmpty)
		{
			writer.WriteStringValue(string.Empty);
			return;
		}

		// We don't use Convert.ToHexString even for .NET 5.0+ to be able to use pooled memory.

		var pool = MemoryPool<char>.Shared;
		var required = checked(value.Length * 2);
		var owner = pool.Rent(required);

		try
		{
			var source = value.Span;
			var dest = owner.Memory.Span[..required];

			byte b;

			for(int bx = 0, cx = 0; bx < source.Length; ++bx, ++cx)
			{
				b = ((byte)(source[bx] >> 4));
				dest[cx] = (char)(b > 9 ? b + 0x37 : b + 0x30);
				b = ((byte)(source[bx] & 0x0F));
				dest[++cx]=(char)(b > 9 ? b + 0x37 : b + 0x30);
			}

			writer.WriteStringValue(dest);
		}
		finally
		{
			owner.Dispose();
		}
	}

#if !NET5_0_OR_GREATER
	public static byte[] FromHex(string data)
	{
		if (data.Length is 0)
		{
			return [];
		}

		if (data.Length % 2 != 0)
		{
			throw new ArgumentException("Decoded vector data length is not a multiple of 2 (not valid 8-bit hex niblets).");
		}

		var buffer = new byte[data.Length / 2];
		char c;

		for (int bx = 0, sx = 0; bx < buffer.Length; ++bx, ++sx)
		{
			c = data[sx];
			buffer[bx] = (byte)((c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0')) << 4);
			c = data[++sx];
			buffer[bx] |= (byte)(c > '9' ? (c > 'Z' ? (c - 'a' + 10) : (c - 'A' + 10)) : (c - '0'));
		}

		return buffer;
	}
#endif
}
