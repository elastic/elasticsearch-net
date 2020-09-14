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
using System.Runtime.CompilerServices;
using Elasticsearch.Net.Utf8Json.Internal.DoubleConversion;

namespace Elasticsearch.Net.Utf8Json.Internal
{
    /// <summary>
    /// zero-allocate itoa, dtoa, atoi, atod converters.
    /// </summary>
	internal static class NumberConverter
    {
		/// <summary>
		/// e or E
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsENotation(byte c) => c == (byte)'E' || c == (byte)'e';

		/// <summary>
        /// 0 ~ 9
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumber(byte c) => (byte)'0' <= c && c <= (byte)'9';

		/// <summary>
        /// Is 0 ~ 9, '.', '+', '-'?
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumberRepresentation(byte c)
        {
            switch (c)
            {
                case 43: // +
                case 45: // -
                case 46: // .
                case 48: // 0
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57: // 9
                    return true;
                case 44:
                case 47:
                default:
                    return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ReadSByte(byte[] bytes, int offset, out int readCount) => checked((sbyte)ReadInt64(bytes, offset, out readCount));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReadInt16(byte[] bytes, int offset, out int readCount) => checked((short)ReadInt64(bytes, offset, out readCount));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReadInt32(byte[] bytes, int offset, out int readCount) => checked((int)ReadInt64(bytes, offset, out readCount));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReadInt64(byte[] bytes, int offset, out int readCount)
        {
            var value = 0L;
            var sign = 1;

            if (bytes[offset] == '-')
				sign = -1;

			for (var i = ((sign == -1) ? offset + 1 : offset); i < bytes.Length; i++)
            {
                if (!IsNumber(bytes[i]))
                {
                    readCount = i - offset;
                    goto END;
                }

                // long.MinValue causes overflow so use unchecked.
                value = unchecked(value * 10 + (bytes[i] - '0'));
            }
            readCount = bytes.Length - offset;

            END:
            return unchecked(value * sign);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReadByte(byte[] bytes, int offset, out int readCount) => checked((byte)ReadUInt64(bytes, offset, out readCount));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReadUInt16(byte[] bytes, int offset, out int readCount) => checked((ushort)ReadUInt64(bytes, offset, out readCount));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReadUInt32(byte[] bytes, int offset, out int readCount) => checked((uint)ReadUInt64(bytes, offset, out readCount));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReadUInt64(byte[] bytes, int offset, out int readCount)
        {
            var value = 0UL;

            for (var i = offset; i < bytes.Length; i++)
            {
                if (!IsNumber(bytes[i]))
                {
                    readCount = i - offset;
                    goto END;
                }

                value = checked(value * 10 + (ulong)(bytes[i] - '0'));
            }
            readCount = bytes.Length - offset;

            END:
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ReadSingle(byte[] bytes, int offset, out int readCount) => StringToDoubleConverter.ToSingle(bytes, offset, out readCount);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ReadDouble(byte[] bytes, int offset, out int readCount) => StringToDoubleConverter.ToDouble(bytes, offset, out readCount);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteByte(ref byte[] buffer, int offset, byte value) => WriteUInt64(ref buffer, offset, (ulong)value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteUInt16(ref byte[] buffer, int offset, ushort value) => WriteUInt64(ref buffer, offset, (ulong)value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteUInt32(ref byte[] buffer, int offset, uint value) => WriteUInt64(ref buffer, offset, (ulong)value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteUInt64(ref byte[] buffer, int offset, ulong value)
        {
            var startOffset = offset;

            ulong num1 = value, num2, num3, num4, num5, div;

            if (num1 < 10000)
            {
                if (num1 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 1); goto L1; }
                if (num1 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 2); goto L2; }
                if (num1 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 3); goto L3; }
                BinaryUtil.EnsureCapacity(ref buffer, offset, 4); goto L4;
            }
            else
            {
                num2 = num1 / 10000;
                num1 -= num2 * 10000;
                if (num2 < 10000)
                {
                    if (num2 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 5); goto L5; }
                    if (num2 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 6); goto L6; }
                    if (num2 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 7); goto L7; }
                    BinaryUtil.EnsureCapacity(ref buffer, offset, 8); goto L8;
                }
                else
                {
                    num3 = num2 / 10000;
                    num2 -= num3 * 10000;
                    if (num3 < 10000)
                    {
                        if (num3 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 9); goto L9; }
                        if (num3 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 10); goto L10; }
                        if (num3 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 11); goto L11; }
                        BinaryUtil.EnsureCapacity(ref buffer, offset, 12); goto L12;
                    }
                    else
                    {
                        num4 = num3 / 10000;
                        num3 -= num4 * 10000;
                        if (num4 < 10000)
                        {
                            if (num4 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 13); goto L13; }
                            if (num4 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 14); goto L14; }
                            if (num4 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 15); goto L15; }
                            BinaryUtil.EnsureCapacity(ref buffer, offset, 16); goto L16;
                        }
                        else
                        {
                            num5 = num4 / 10000;
                            num4 -= num5 * 10000;
                            if (num5 < 10000)
                            {
                                if (num5 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 17); goto L17; }
                                if (num5 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 18); goto L18; }
                                if (num5 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 19); goto L19; }
                                BinaryUtil.EnsureCapacity(ref buffer, offset, 20); goto L20;
                            }
                            L20:
                            buffer[offset++] = (byte)('0' + (div = (num5 * 8389UL) >> 23));
                            num5 -= div * 1000;
                            L19:
                            buffer[offset++] = (byte)('0' + (div = (num5 * 5243UL) >> 19));
                            num5 -= div * 100;
                            L18:
                            buffer[offset++] = (byte)('0' + (div = (num5 * 6554UL) >> 16));
                            num5 -= div * 10;
                            L17:
                            buffer[offset++] = (byte)('0' + (num5));
                        }
                        L16:
                        buffer[offset++] = (byte)('0' + (div = (num4 * 8389UL) >> 23));
                        num4 -= div * 1000;
                        L15:
                        buffer[offset++] = (byte)('0' + (div = (num4 * 5243UL) >> 19));
                        num4 -= div * 100;
                        L14:
                        buffer[offset++] = (byte)('0' + (div = (num4 * 6554UL) >> 16));
                        num4 -= div * 10;
                        L13:
                        buffer[offset++] = (byte)('0' + (num4));
                    }
                    L12:
                    buffer[offset++] = (byte)('0' + (div = (num3 * 8389UL) >> 23));
                    num3 -= div * 1000;
                    L11:
                    buffer[offset++] = (byte)('0' + (div = (num3 * 5243UL) >> 19));
                    num3 -= div * 100;
                    L10:
                    buffer[offset++] = (byte)('0' + (div = (num3 * 6554UL) >> 16));
                    num3 -= div * 10;
                    L9:
                    buffer[offset++] = (byte)('0' + (num3));
                }
                L8:
                buffer[offset++] = (byte)('0' + (div = (num2 * 8389UL) >> 23));
                num2 -= div * 1000;
                L7:
                buffer[offset++] = (byte)('0' + (div = (num2 * 5243UL) >> 19));
                num2 -= div * 100;
                L6:
                buffer[offset++] = (byte)('0' + (div = (num2 * 6554UL) >> 16));
                num2 -= div * 10;
                L5:
                buffer[offset++] = (byte)('0' + (num2));
            }
            L4:
            buffer[offset++] = (byte)('0' + (div = (num1 * 8389UL) >> 23));
            num1 -= div * 1000;
            L3:
            buffer[offset++] = (byte)('0' + (div = (num1 * 5243UL) >> 19));
            num1 -= div * 100;
            L2:
            buffer[offset++] = (byte)('0' + (div = (num1 * 6554UL) >> 16));
            num1 -= div * 10;
            L1:
            buffer[offset++] = (byte)('0' + (num1));

            return offset - startOffset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteSByte(ref byte[] buffer, int offset, sbyte value) => WriteInt64(ref buffer, offset, (long)value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteInt16(ref byte[] buffer, int offset, short value) => WriteInt64(ref buffer, offset, (long)value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteInt32(ref byte[] buffer, int offset, int value) => WriteInt64(ref buffer, offset, (long)value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteInt64(ref byte[] buffer, int offset, long value)
        {
            var startOffset = offset;

            long num1 = value, num2, num3, num4, num5, div;

            if (value < 0)
            {
                if (value == long.MinValue) // -9223372036854775808
                {
                    BinaryUtil.EnsureCapacity(ref buffer, offset, 20);
                    buffer[offset++] = (byte)'-';
                    buffer[offset++] = (byte)'9';
                    buffer[offset++] = (byte)'2';
                    buffer[offset++] = (byte)'2';
                    buffer[offset++] = (byte)'3';
                    buffer[offset++] = (byte)'3';
                    buffer[offset++] = (byte)'7';
                    buffer[offset++] = (byte)'2';
                    buffer[offset++] = (byte)'0';
                    buffer[offset++] = (byte)'3';
                    buffer[offset++] = (byte)'6';
                    buffer[offset++] = (byte)'8';
                    buffer[offset++] = (byte)'5';
                    buffer[offset++] = (byte)'4';
                    buffer[offset++] = (byte)'7';
                    buffer[offset++] = (byte)'7';
                    buffer[offset++] = (byte)'5';
                    buffer[offset++] = (byte)'8';
                    buffer[offset++] = (byte)'0';
                    buffer[offset++] = (byte)'8';
                    return offset - startOffset;
                }

                BinaryUtil.EnsureCapacity(ref buffer, offset, 1);
                buffer[offset++] = (byte)'-';
                num1 = unchecked(-value);
            }

            // WriteUInt64(inlined)

            if (num1 < 10000)
            {
                if (num1 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 1); goto L1; }
                if (num1 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 2); goto L2; }
                if (num1 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 3); goto L3; }
                BinaryUtil.EnsureCapacity(ref buffer, offset, 4); goto L4;
            }
            else
            {
                num2 = num1 / 10000;
                num1 -= num2 * 10000;
                if (num2 < 10000)
                {
                    if (num2 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 5); goto L5; }
                    if (num2 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 6); goto L6; }
                    if (num2 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 7); goto L7; }
                    BinaryUtil.EnsureCapacity(ref buffer, offset, 8); goto L8;
                }
                else
                {
                    num3 = num2 / 10000;
                    num2 -= num3 * 10000;
                    if (num3 < 10000)
                    {
                        if (num3 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 9); goto L9; }
                        if (num3 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 10); goto L10; }
                        if (num3 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 11); goto L11; }
                        BinaryUtil.EnsureCapacity(ref buffer, offset, 12); goto L12;
                    }
                    else
                    {
                        num4 = num3 / 10000;
                        num3 -= num4 * 10000;
                        if (num4 < 10000)
                        {
                            if (num4 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 13); goto L13; }
                            if (num4 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 14); goto L14; }
                            if (num4 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 15); goto L15; }
                            BinaryUtil.EnsureCapacity(ref buffer, offset, 16); goto L16;
                        }
                        else
                        {
                            num5 = num4 / 10000;
                            num4 -= num5 * 10000;
                            if (num5 < 10000)
                            {
                                if (num5 < 10) { BinaryUtil.EnsureCapacity(ref buffer, offset, 17); goto L17; }
                                if (num5 < 100) { BinaryUtil.EnsureCapacity(ref buffer, offset, 18); goto L18; }
                                if (num5 < 1000) { BinaryUtil.EnsureCapacity(ref buffer, offset, 19); goto L19; }
                                BinaryUtil.EnsureCapacity(ref buffer, offset, 20); goto L20;
                            }
                            L20:
                            buffer[offset++] = (byte)('0' + (div = (num5 * 8389L) >> 23));
                            num5 -= div * 1000;
                            L19:
                            buffer[offset++] = (byte)('0' + (div = (num5 * 5243L) >> 19));
                            num5 -= div * 100;
                            L18:
                            buffer[offset++] = (byte)('0' + (div = (num5 * 6554L) >> 16));
                            num5 -= div * 10;
                            L17:
                            buffer[offset++] = (byte)('0' + (num5));
                        }
                        L16:
                        buffer[offset++] = (byte)('0' + (div = (num4 * 8389L) >> 23));
                        num4 -= div * 1000;
                        L15:
                        buffer[offset++] = (byte)('0' + (div = (num4 * 5243L) >> 19));
                        num4 -= div * 100;
                        L14:
                        buffer[offset++] = (byte)('0' + (div = (num4 * 6554L) >> 16));
                        num4 -= div * 10;
                        L13:
                        buffer[offset++] = (byte)('0' + (num4));
                    }
                    L12:
                    buffer[offset++] = (byte)('0' + (div = (num3 * 8389L) >> 23));
                    num3 -= div * 1000;
                    L11:
                    buffer[offset++] = (byte)('0' + (div = (num3 * 5243L) >> 19));
                    num3 -= div * 100;
                    L10:
                    buffer[offset++] = (byte)('0' + (div = (num3 * 6554L) >> 16));
                    num3 -= div * 10;
                    L9:
                    buffer[offset++] = (byte)('0' + (num3));
                }
                L8:
                buffer[offset++] = (byte)('0' + (div = (num2 * 8389L) >> 23));
                num2 -= div * 1000;
                L7:
                buffer[offset++] = (byte)('0' + (div = (num2 * 5243L) >> 19));
                num2 -= div * 100;
                L6:
                buffer[offset++] = (byte)('0' + (div = (num2 * 6554L) >> 16));
                num2 -= div * 10;
                L5:
                buffer[offset++] = (byte)('0' + (num2));
            }
            L4:
            buffer[offset++] = (byte)('0' + (div = (num1 * 8389L) >> 23));
            num1 -= div * 1000;
            L3:
            buffer[offset++] = (byte)('0' + (div = (num1 * 5243L) >> 19));
            num1 -= div * 100;
            L2:
            buffer[offset++] = (byte)('0' + (div = (num1 * 6554L) >> 16));
            num1 -= div * 10;
            L1:
            buffer[offset++] = (byte)('0' + (num1));

            return offset - startOffset;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteSingle(ref byte[] bytes, int offset, float value) => DoubleToStringConverter.GetBytes(ref bytes, offset, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int WriteDouble(ref byte[] bytes, int offset, double value) => DoubleToStringConverter.GetBytes(ref bytes, offset, value);

		// boolean is not number:)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ReadBoolean(byte[] bytes, int offset, out int readCount)
        {
            if (bytes[offset] == 't')
            {
                if (bytes[offset + 1] != 'r') goto ERROR_TRUE;
                if (bytes[offset + 2] != 'u') goto ERROR_TRUE;
                if (bytes[offset + 3] != 'e') goto ERROR_TRUE;
                readCount = 4;
                return true;
            }
            else if (bytes[offset] == 'f')
            {
                if (bytes[offset + 1] != 'a') goto ERROR_FALSE;
                if (bytes[offset + 2] != 'l') goto ERROR_FALSE;
                if (bytes[offset + 3] != 's') goto ERROR_FALSE;
                if (bytes[offset + 4] != 'e') goto ERROR_FALSE;
                readCount = 5;
                return false;
            }
            else
            {
                throw new InvalidOperationException("value is not boolean.");
            }

            ERROR_TRUE:
            throw new InvalidOperationException("value is not boolean(true).");
            ERROR_FALSE:
            throw new InvalidOperationException("value is not boolean(false).");
        }
    }
}
