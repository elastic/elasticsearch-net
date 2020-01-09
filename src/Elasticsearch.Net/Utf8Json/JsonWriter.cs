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
        static readonly byte[] emptyBytes = new byte[0];

        // write direct from UnsafeMemory
        internal byte[] buffer;
        internal int offset;

        public int CurrentOffset
        {
            get
            {
                return offset;
            }
        }

        public void AdvanceOffset(int offset)
        {
            this.offset += offset;
        }

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
            Buffer.BlockCopy(buf.Array, buf.Offset + 1, result, 0, result.Length); // without quotation
            return result;
        }

        public JsonWriter(byte[] initialBuffer)
        {
            this.buffer = initialBuffer;
            this.offset = 0;
        }

        public ArraySegment<byte> GetBuffer()
        {
            if (buffer == null) return new ArraySegment<byte>(emptyBytes, 0, 0);
            return new ArraySegment<byte>(buffer, 0, offset);
        }

        public byte[] ToUtf8ByteArray()
        {
            if (buffer == null) return emptyBytes;
            return BinaryUtil.FastCloneWithResize(buffer, offset);
        }

        public override string ToString()
        {
            if (buffer == null) return null;
            return Encoding.UTF8.GetString(buffer, 0, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int appendLength)
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, appendLength);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRaw(byte rawValue)
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = rawValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRaw(byte[] rawValue)
        {
            UnsafeMemory.WriteRaw(ref this, rawValue);
        }
        public void WriteRaw(byte[] rawValue, int length)
        {
            UnsafeMemory.WriteRaw(ref this, rawValue, length);
        }

		public void WriteRaw(MemoryStream ms)
		{
			UnsafeMemory.WriteRaw(ref this, ms);
		}

		public void WriteSerialized<T>(T value, IElasticsearchSerializer serializer, IConnectionConfigurationValues settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using var ms = settings.MemoryStreamFactory.Create();
			serializer.Serialize(value, ms, formatting);
			WriteRaw(ms);
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteRawUnsafe(byte rawValue)
        {
            buffer[offset++] = rawValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginArray()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'[';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndArray()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)']';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginObject()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'{';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndObject()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'}';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteValueSeparator()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)',';
        }

        /// <summary>:</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNameSeparator()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)':';
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
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'\"';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 4);
            buffer[offset + 0] = (byte)'n';
            buffer[offset + 1] = (byte)'u';
            buffer[offset + 2] = (byte)'l';
            buffer[offset + 3] = (byte)'l';
            offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBoolean(bool value)
        {
            if (value)
            {
                BinaryUtil.EnsureCapacity(ref buffer, offset, 4);
                buffer[offset + 0] = (byte)'t';
                buffer[offset + 1] = (byte)'r';
                buffer[offset + 2] = (byte)'u';
                buffer[offset + 3] = (byte)'e';
                offset += 4;
            }
            else
            {
                BinaryUtil.EnsureCapacity(ref buffer, offset, 5);
                buffer[offset + 0] = (byte)'f';
                buffer[offset + 1] = (byte)'a';
                buffer[offset + 2] = (byte)'l';
                buffer[offset + 3] = (byte)'s';
                buffer[offset + 4] = (byte)'e';
                offset += 5;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteTrue()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 4);
            buffer[offset + 0] = (byte)'t';
            buffer[offset + 1] = (byte)'r';
            buffer[offset + 2] = (byte)'u';
            buffer[offset + 3] = (byte)'e';
            offset += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteFalse()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 5);
            buffer[offset + 0] = (byte)'f';
            buffer[offset + 1] = (byte)'a';
            buffer[offset + 2] = (byte)'l';
            buffer[offset + 3] = (byte)'s';
            buffer[offset + 4] = (byte)'e';
            offset += 5;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSingle(float value)
        {
            offset += DoubleToStringConverter.GetBytes(ref buffer, offset, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteDouble(double value)
        {
            offset += DoubleToStringConverter.GetBytes(ref buffer, offset, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteByte(byte value)
        {
            WriteUInt64((ulong)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUInt16(ushort value)
        {
            WriteUInt64((ulong)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUInt32(uint value)
        {
            WriteUInt64((ulong)value);
        }

        public void WriteUInt64(ulong value)
        {
            offset += NumberConverter.WriteUInt64(ref buffer, offset, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSByte(sbyte value)
        {
            WriteInt64((long)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteInt16(short value)
        {
            WriteInt64((long)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteInt32(int value)
        {
            WriteInt64((long)value);
        }

        public void WriteInt64(long value)
        {
            offset += NumberConverter.WriteInt64(ref buffer, offset, value);
        }

        public void WriteString(string value)
        {
            if (value == null)
            {
                WriteNull();
                return;
            }

            // single-path escape

            // nonescaped-ensure
            var startoffset = offset;
            var max = StringEncoding.UTF8.GetMaxByteCount(value.Length) + 2;
            BinaryUtil.EnsureCapacity(ref buffer, startoffset, max);

            var from = 0;
            var to = value.Length;

            buffer[offset++] = (byte)'\"';

            // for JIT Optimization, for-loop i < str.Length
            for (int i = 0; i < value.Length; i++)
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
                BinaryUtil.EnsureCapacity(ref buffer, startoffset, max); // check +escape capacity
				offset += StringEncoding.UTF8.GetBytes(value, from, i - from, buffer, offset);
                from = i + 1;

				if (escapeChar == default)
					ToUnicode(value[i], ref offset, buffer);
				else
				{
					buffer[offset++] = (byte)'\\';
					buffer[offset++] = escapeChar;
				}
			}

            if (from != value.Length)
            {
                offset += StringEncoding.UTF8.GetBytes(value, from, value.Length - from, buffer, offset);
            }

            buffer[offset++] = (byte)'\"';
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
