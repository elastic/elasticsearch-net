using System;
using System.Text;
using Utf8Json.Internal;

#if NETSTANDARD
using System.Runtime.CompilerServices;
#endif

namespace Utf8Json
{
    // JSON RFC: https://www.ietf.org/rfc/rfc4627.txt

    public struct JsonWriter
    {
        static readonly byte[] emptyBytes = new byte[0];

        // write direct from UnsafeMemory
#if NETSTANDARD
        internal
#endif
        byte[] buffer;
#if NETSTANDARD
        internal
#endif
        int offset;

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

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void EnsureCapacity(int appendLength)
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, appendLength);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteRaw(byte rawValue)
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = rawValue;
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteRaw(byte[] rawValue)
        {
#if NETSTANDARD
            UnsafeMemory.WriteRaw(ref this, rawValue);
#else
            BinaryUtil.EnsureCapacity(ref buffer, offset, rawValue.Length);
            Buffer.BlockCopy(rawValue, 0, buffer, offset, rawValue.Length);
            offset += rawValue.Length;
#endif
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteRawUnsafe(byte rawValue)
        {
            buffer[offset++] = rawValue;
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteBeginArray()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'[';
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteEndArray()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)']';
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteBeginObject()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'{';
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteEndObject()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'}';
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteValueSeparator()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)',';
        }

        /// <summary>:</summary>
#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteNameSeparator()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)':';
        }

        /// <summary>WriteString + WriteNameSeparator</summary>
#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WritePropertyName(string propertyName)
        {
            WriteString(propertyName);
            WriteNameSeparator();
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteQuotation()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
            buffer[offset++] = (byte)'\"';
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteNull()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 4);
            buffer[offset + 0] = (byte)'n';
            buffer[offset + 1] = (byte)'u';
            buffer[offset + 2] = (byte)'l';
            buffer[offset + 3] = (byte)'l';
            offset += 4;
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteTrue()
        {
            BinaryUtil.EnsureCapacity(ref buffer, offset, 4);
            buffer[offset + 0] = (byte)'t';
            buffer[offset + 1] = (byte)'r';
            buffer[offset + 2] = (byte)'u';
            buffer[offset + 3] = (byte)'e';
            offset += 4;
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteSingle(Single value)
        {
            offset += Utf8Json.Internal.DoubleConversion.DoubleToStringConverter.GetBytes(ref buffer, offset, value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteDouble(double value)
        {
            offset += Utf8Json.Internal.DoubleConversion.DoubleToStringConverter.GetBytes(ref buffer, offset, value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteByte(byte value)
        {
            WriteUInt64((ulong)value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteUInt16(ushort value)
        {
            WriteUInt64((ulong)value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteUInt32(uint value)
        {
            WriteUInt64((ulong)value);
        }

        public void WriteUInt64(ulong value)
        {
            offset += NumberConverter.WriteUInt64(ref buffer, offset, value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteSByte(sbyte value)
        {
            WriteInt64((long)value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public void WriteInt16(short value)
        {
            WriteInt64((long)value);
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
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
                byte escapeChar = default(byte);
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
                    // use switch jumptable
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
                    case (char)32:
                    case (char)33:
                    case (char)35:
                    case (char)36:
                    case (char)37:
                    case (char)38:
                    case (char)39:
                    case (char)40:
                    case (char)41:
                    case (char)42:
                    case (char)43:
                    case (char)44:
                    case (char)45:
                    case (char)46:
                    case (char)47:
                    case (char)48:
                    case (char)49:
                    case (char)50:
                    case (char)51:
                    case (char)52:
                    case (char)53:
                    case (char)54:
                    case (char)55:
                    case (char)56:
                    case (char)57:
                    case (char)58:
                    case (char)59:
                    case (char)60:
                    case (char)61:
                    case (char)62:
                    case (char)63:
                    case (char)64:
                    case (char)65:
                    case (char)66:
                    case (char)67:
                    case (char)68:
                    case (char)69:
                    case (char)70:
                    case (char)71:
                    case (char)72:
                    case (char)73:
                    case (char)74:
                    case (char)75:
                    case (char)76:
                    case (char)77:
                    case (char)78:
                    case (char)79:
                    case (char)80:
                    case (char)81:
                    case (char)82:
                    case (char)83:
                    case (char)84:
                    case (char)85:
                    case (char)86:
                    case (char)87:
                    case (char)88:
                    case (char)89:
                    case (char)90:
                    case (char)91:
                    default:
                        continue;
                }

                max += 2;
                BinaryUtil.EnsureCapacity(ref buffer, startoffset, max); // check +escape capacity

                offset += StringEncoding.UTF8.GetBytes(value, from, i - from, buffer, offset);
                from = i + 1;
                buffer[offset++] = (byte)'\\';
                buffer[offset++] = escapeChar;
            }

            if (from != value.Length)
            {
                offset += StringEncoding.UTF8.GetBytes(value, from, value.Length - from, buffer, offset);
            }

            buffer[offset++] = (byte)'\"';
        }
    }
}