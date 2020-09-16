#region Utf8Json License https://github.com/neuecc/Utf8Json/blob/master/LICENSE
// MIT License
//
// Copyright (c) 2017 Yoshifumi Kawai
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Internal.DoubleConversion;

namespace Elasticsearch.Net.Utf8Json
{
    // JSON RFC: https://www.ietf.org/rfc/rfc4627.txt

    internal struct JsonWriter
    {
		// write direct from UnsafeMemory
        internal byte[] Buffer;
        internal int Offset;

        public int CurrentOffset  => Offset;

        public void AdvanceOffset(int offset) => Offset += offset;

		public static byte[] GetEncodedPropertyName(string propertyName)
        {
            var writer = new JsonWriter();
            writer.WritePropertyName(propertyName);
            return writer.ToUtf8ByteArray();
        }

        public static byte[] GetEncodedPropertyNameWithPrefixValueSeparator(string propertyName)
        {
            var writer = new JsonWriter();
            writer.WriteValueSeparator();
            writer.WritePropertyName(propertyName);
            return writer.ToUtf8ByteArray();
        }

        public static byte[] GetEncodedPropertyNameWithBeginObject(string propertyName)
        {
            var writer = new JsonWriter();
            writer.WriteBeginObject();
            writer.WritePropertyName(propertyName);
            return writer.ToUtf8ByteArray();
        }

        public static byte[] GetEncodedPropertyNameWithoutQuotation(string propertyName)
        {
            var writer = new JsonWriter();
            writer.WriteString(propertyName); // "propname"
            var buf = writer.GetBuffer();
            var result = new byte[buf.Count - 2];
            System.Buffer.BlockCopy(buf.Array, buf.Offset + 1, result, 0, result.Length); // without quotation
            return result;
        }

        public JsonWriter(byte[] initialBuffer)
        {
            Buffer = initialBuffer;
            Offset = 0;
        }

        public ArraySegment<byte> GetBuffer() =>
			Buffer == null
				? new ArraySegment<byte>(Array.Empty<byte>(), 0, 0)
				: new ArraySegment<byte>(Buffer, 0, Offset);

		public byte[] ToUtf8ByteArray() =>
			Buffer == null
				? Array.Empty<byte>()
				: BinaryUtil.FastCloneWithResize(Buffer, Offset);

		public override string ToString() =>
			Buffer == null
				? string.Empty
				: Encoding.UTF8.GetString(Buffer, 0, Offset);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int appendLength) => BinaryUtil.EnsureCapacity(ref Buffer, Offset, appendLength);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRaw(byte rawValue)
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = rawValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRaw(byte[] rawValue) => UnsafeMemory.WriteRaw(ref this, rawValue);

		public void WriteRaw(byte[] rawValue, int length) => UnsafeMemory.WriteRaw(ref this, rawValue, length);

		public void WriteRaw(MemoryStream ms) => UnsafeMemory.WriteRaw(ref this, ms);

		public void WriteSerialized<T>(T value, IElasticsearchSerializer serializer, IConnectionConfigurationValues settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using var ms = settings.MemoryStreamFactory.Create();
			serializer.Serialize(value, ms, formatting);
			WriteRaw(ms);
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRawUnsafe(byte rawValue) => Buffer[Offset++] = rawValue;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginArray()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)'[';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndArray()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)']';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginObject()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)'{';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndObject()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)'}';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteValueSeparator()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)',';
        }

        /// <summary>:</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNameSeparator()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)':';
        }

        /// <summary>WriteString + WriteNameSeparator</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WritePropertyName(string propertyName)
        {
            WriteString(propertyName);
            WriteNameSeparator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteQuotation()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 1);
            Buffer[Offset++] = (byte)'\"';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 4);
            Buffer[Offset + 0] = (byte)'n';
            Buffer[Offset + 1] = (byte)'u';
            Buffer[Offset + 2] = (byte)'l';
            Buffer[Offset + 3] = (byte)'l';
            Offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBoolean(bool value)
        {
            if (value)
            {
                BinaryUtil.EnsureCapacity(ref Buffer, Offset, 4);
                Buffer[Offset + 0] = (byte)'t';
                Buffer[Offset + 1] = (byte)'r';
                Buffer[Offset + 2] = (byte)'u';
                Buffer[Offset + 3] = (byte)'e';
                Offset += 4;
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref Buffer, Offset, 5);
                Buffer[Offset + 0] = (byte)'f';
                Buffer[Offset + 1] = (byte)'a';
                Buffer[Offset + 2] = (byte)'l';
                Buffer[Offset + 3] = (byte)'s';
                Buffer[Offset + 4] = (byte)'e';
                Offset += 5;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteTrue()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 4);
            Buffer[Offset + 0] = (byte)'t';
            Buffer[Offset + 1] = (byte)'r';
            Buffer[Offset + 2] = (byte)'u';
            Buffer[Offset + 3] = (byte)'e';
            Offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteFalse()
        {
            BinaryUtil.EnsureCapacity(ref Buffer, Offset, 5);
            Buffer[Offset + 0] = (byte)'f';
            Buffer[Offset + 1] = (byte)'a';
            Buffer[Offset + 2] = (byte)'l';
            Buffer[Offset + 3] = (byte)'s';
            Buffer[Offset + 4] = (byte)'e';
            Offset += 5;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSingle(float value) => Offset += DoubleToStringConverter.GetBytes(ref Buffer, Offset, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteDouble(double value) => Offset += DoubleToStringConverter.GetBytes(ref Buffer, Offset, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteByte(byte value) => WriteUInt64(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUInt16(ushort value) => WriteUInt64(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUInt32(uint value) => WriteUInt64(value);

		public void WriteUInt64(ulong value) => Offset += NumberConverter.WriteUInt64(ref Buffer, Offset, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSByte(sbyte value) => WriteInt64(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteInt16(short value) => WriteInt64(value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteInt32(int value) => WriteInt64(value);

		public void WriteInt64(long value) => Offset += NumberConverter.WriteInt64(ref Buffer, Offset, value);

		public void WriteString(string value)
        {
            if (value == null)
            {
                WriteNull();
                return;
            }

            // single-path escape

            // nonescaped-ensure
            var startOffset = Offset;
            var max = StringEncoding.UTF8.GetMaxByteCount(value.Length) + 2;
            BinaryUtil.EnsureCapacity(ref Buffer, startOffset, max);

            var from = 0;
            var to = value.Length;

            Buffer[Offset++] = (byte)'\"';

            // for JIT Optimization, for-loop i < str.Length
            for (var i = 0; i < value.Length; i++)
            {
				byte escapeChar = default;
                switch (value[i])
                {
                    case '"':
                        escapeChar = (byte)'"';
                        break;
                    case '\\':
                        escapeChar = (byte)'\\';
                        break;
                    case '\b':
                        escapeChar = (byte)'b';
                        break;
                    case '\f':
                        escapeChar = (byte)'f';
                        break;
                    case '\n':
                        escapeChar = (byte)'n';
                        break;
                    case '\r':
                        escapeChar = (byte)'r';
                        break;
                    case '\t':
                        escapeChar = (byte)'t';
                        break;
					case (char)0:
					case (char)1:
					case (char)2:
					case (char)3:
					case (char)4:
					case (char)5:
					case (char)6:
					case (char)7:
					case (char)11:
					case (char)14:
					case (char)15:
					case (char)16:
					case (char)17:
					case (char)18:
					case (char)19:
					case (char)20:
					case (char)21:
					case (char)22:
					case (char)23:
					case (char)24:
					case (char)25:
					case (char)26:
					case (char)27:
					case (char)28:
					case (char)29:
					case (char)30:
					case (char)31:
					case '\u0085':
					case '\u2028':
					case '\u2029':
						break;
					default:
						continue;
				}

                max += escapeChar == default ? 6 : 2;
                BinaryUtil.EnsureCapacity(ref Buffer, startOffset, max); // check +escape capacity
				Offset += StringEncoding.UTF8.GetBytes(value, from, i - from, Buffer, Offset);
                from = i + 1;

				if (escapeChar == default)
					ToUnicode(value[i], ref Offset, Buffer);
				else
				{
					Buffer[Offset++] = (byte)'\\';
					Buffer[Offset++] = escapeChar;
				}
			}

            if (from != value.Length)
				Offset += StringEncoding.UTF8.GetBytes(value, @from, value.Length - @from, Buffer, Offset);

			Buffer[Offset++] = (byte)'\"';
        }

		private static void ToUnicode(char c, ref int offset, byte[] buffer)
		{
			buffer[offset++] = (byte)'\\';
			buffer[offset++] = (byte)'u';
			buffer[offset++] = (byte)CharUtils.HexDigit((c >> 12) & '\x000f');
			buffer[offset++] = (byte)CharUtils.HexDigit((c >> 8) & '\x000f');
			buffer[offset++] = (byte)CharUtils.HexDigit((c >> 4) & '\x000f');
			buffer[offset++] = (byte)CharUtils.HexDigit(c & '\x000f');
		}
	}
}
