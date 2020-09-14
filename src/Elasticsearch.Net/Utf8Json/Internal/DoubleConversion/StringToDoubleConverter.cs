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
using System.Globalization;
using System.Text;

namespace Elasticsearch.Net.Utf8Json.Internal.DoubleConversion
{
#pragma warning disable 660
#pragma warning disable 661

    internal struct Iterator
    {
		private byte[] _buffer;
		private int _offset;

        public Iterator(byte[] buffer, int offset)
        {
            _buffer = buffer;
            _offset = offset;
        }

        public byte Value => _buffer[_offset];

		public static Iterator operator ++(Iterator self)
        {
            self._offset++;
            return self;
        }

        public static Iterator operator +(Iterator self, int length) => new Iterator { _buffer = self._buffer, _offset = self._offset + length };

		public static int operator -(Iterator lhs, Iterator rhs) => lhs._offset - rhs._offset;

		public static bool operator ==(Iterator lhs, Iterator rhs) => lhs._offset == rhs._offset;

		public static bool operator !=(Iterator lhs, Iterator rhs) => lhs._offset != rhs._offset;

		public static bool operator ==(Iterator lhs, char rhs) => lhs._buffer[lhs._offset] == (byte)rhs;

		public static bool operator !=(Iterator lhs, char rhs) => lhs._buffer[lhs._offset] != (byte)rhs;

		public static bool operator ==(Iterator lhs, byte rhs) => lhs._buffer[lhs._offset] == (byte)rhs;

		public static bool operator !=(Iterator lhs, byte rhs) => lhs._buffer[lhs._offset] != (byte)rhs;

		public static bool operator >=(Iterator lhs, char rhs) => lhs._buffer[lhs._offset] >= (byte)rhs;

		public static bool operator <=(Iterator lhs, char rhs) => lhs._buffer[lhs._offset] <= (byte)rhs;

		public static bool operator >(Iterator lhs, char rhs) => lhs._buffer[lhs._offset] > (byte)rhs;

		public static bool operator <(Iterator lhs, char rhs) => lhs._buffer[lhs._offset] < (byte)rhs;
	}

#pragma warning restore 661
#pragma warning restore 660

    // C# API
    internal static partial class StringToDoubleConverter
    {
        [ThreadStatic] private static byte[] _kBuffer;

		private static byte[] GetBuffer() => _kBuffer ??= new byte[kBufferSize];

		[ThreadStatic] private static byte[] _fallbackBuffer;

		private static byte[] GetFallbackBuffer() => _fallbackBuffer ??= new byte[99];

		public static double ToDouble(byte[] buffer, int offset, out int readCount) =>
			StringToIeee(new Iterator(buffer, offset), buffer.Length - offset, true, out readCount);

		public static float ToSingle(byte[] buffer, int offset, out int readCount) =>
			unchecked((float)StringToIeee(new Iterator(buffer, offset), buffer.Length - offset, false, out readCount));
	}

    // port
    internal static partial class StringToDoubleConverter
    {
		[Flags]
		private enum Flags
        {
            NO_FLAGS = 0,
            ALLOW_HEX = 1, // defined but always disallow
            ALLOW_OCTALS = 2,
            ALLOW_TRAILING_JUNK = 4,
            ALLOW_LEADING_SPACES = 8,
            ALLOW_TRAILING_SPACES = 16,
            ALLOW_SPACES_AFTER_SIGN = 32,
            ALLOW_CASE_INSENSIBILITY = 64, // not supported
        };

        const Flags flags_ = Flags.ALLOW_TRAILING_JUNK | Flags.ALLOW_TRAILING_SPACES | Flags.ALLOW_SPACES_AFTER_SIGN;
        const double empty_string_value_ = 0.0;
        const double junk_string_value_ = double.NaN;
        const int kMaxSignificantDigits = 772;
        const int kBufferSize = kMaxSignificantDigits + 10;
        static readonly byte[] infinity_symbol_ = StringEncoding.UTF8.GetBytes(double.PositiveInfinity.ToString());
        static readonly byte[] nan_symbol_ = StringEncoding.UTF8.GetBytes(double.NaN.ToString());

        static readonly byte[] kWhitespaceTable7 = { 32, 13, 10, 9, 11, 12 };
        static readonly int kWhitespaceTable7Length = kWhitespaceTable7.Length;

        static readonly ushort[] kWhitespaceTable16 = {
              160, 8232, 8233, 5760, 6158, 8192, 8193, 8194, 8195,
              8196, 8197, 8198, 8199, 8200, 8201, 8202, 8239, 8287, 12288, 65279
        };
        static readonly int kWhitespaceTable16Length = kWhitespaceTable16.Length;

		private static bool IsWhitespace(int x)
        {
            if (x < 128)
            {
                for (var i = 0; i < kWhitespaceTable7Length; i++)
                {
                    if (kWhitespaceTable7[i] == x) return true;
                }
            }
            else
            {
                for (var i = 0; i < kWhitespaceTable16Length; i++)
                {
                    if (kWhitespaceTable16[i] == x) return true;
                }
            }
            return false;
        }

		private static bool AdvanceToNonspace(ref Iterator current, Iterator end)
        {
            while (current != end)
            {
                if (!IsWhitespace(current.Value)) return true;
                current++;
            }
            return false;
        }

		private static bool ConsumeSubString(ref Iterator current,
                                        Iterator end,
                                        byte[] substring)
        {
            for (var i = 1; i < substring.Length; i++)
            {
                ++current;
                if (current == end || current != substring[i])
                {
                    return false;
                }
            }
            ++current;
            return true;
        }


        // Consumes first character of the str is equal to ch
		private static bool ConsumeFirstCharacter(ref Iterator iter, byte[] str, int offset) => iter.Value == str[offset];

		private static double SignedZero(bool sign) => sign ? -0.0 : 0.0;

		private static double StringToIeee(
                    Iterator input,
                    int length,
                    bool read_as_double,
                    out int processed_characters_count)
        {
            var current = input;
            var end = input + length;

            processed_characters_count = 0;

            var allow_trailing_junk = (flags_ & Flags.ALLOW_TRAILING_JUNK) != 0;
            var allow_leading_spaces = (flags_ & Flags.ALLOW_LEADING_SPACES) != 0;
            var allow_trailing_spaces = (flags_ & Flags.ALLOW_TRAILING_SPACES) != 0;
            var allow_spaces_after_sign = (flags_ & Flags.ALLOW_SPACES_AFTER_SIGN) != 0;
            // bool allow_case_insensibility = (flags_ & Flags.ALLOW_CASE_INSENSIBILITY) != 0;

            // To make sure that iterator dereferencing is valid the following
            // convention is used:
            // 1. Each '++current' statement is followed by check for equality to 'end'.
            // 2. If AdvanceToNonspace returned false then current == end.
            // 3. If 'current' becomes equal to 'end' the function returns or goes to
            // 'parsing_done'.
            // 4. 'current' is not dereferenced after the 'parsing_done' label.
            // 5. Code before 'parsing_done' may rely on 'current != end'.
            if (length == 0) return empty_string_value_;

            if (allow_leading_spaces || allow_trailing_spaces)
            {
                if (!AdvanceToNonspace(ref current, end))
                {
                    processed_characters_count = (int)(current - input);
                    return empty_string_value_;
                }
                if (!allow_leading_spaces && (input != current))
                {
                    // No leading spaces allowed, but AdvanceToNonspace moved forward.
                    return junk_string_value_;
                }
            }

            // The longest form of simplified number is: "-<significant digits>.1eXXX\0".
            var buffer = GetBuffer();  // NOLINT: size is known at compile time.
            var buffer_pos = 0;

            // Exponent will be adjusted if insignificant digits of the integer part
            // or insignificant leading zeros of the fractional part are dropped.
            var exponent = 0;
            var significant_digits = 0;
            var insignificant_digits = 0;
            var nonzero_digit_dropped = false;

            var sign = false;

            if (current == '+' || current == '-')
            {
                sign = (current == '-');
                current++;
                var next_non_space = current;
                // Skip following spaces (if allowed).
                if (!AdvanceToNonspace(ref next_non_space, end)) return junk_string_value_;
                if (!allow_spaces_after_sign && (current != next_non_space))
                {
                    return junk_string_value_;
                }
                current = next_non_space;
            }

            if (infinity_symbol_ != null)
            {
                if (ConsumeFirstCharacter(ref current, infinity_symbol_, 0))
                {
                    if (!ConsumeSubString(ref current, end, infinity_symbol_))
                    {
                        return junk_string_value_;
                    }

                    if (!(allow_trailing_spaces || allow_trailing_junk) && (current != end))
                    {
                        return junk_string_value_;
                    }
                    if (!allow_trailing_junk && AdvanceToNonspace(ref current, end))
                    {
                        return junk_string_value_;
                    }

                    processed_characters_count = (current - input);
                    return sign ? double.NegativeInfinity : double.PositiveInfinity;
                }
            }

            if (nan_symbol_ != null)
            {
                if (ConsumeFirstCharacter(ref current, nan_symbol_, 0))
                {
                    if (!ConsumeSubString(ref current, end, nan_symbol_))
                    {
                        return junk_string_value_;
                    }

                    if (!(allow_trailing_spaces || allow_trailing_junk) && (current != end))
                    {
                        return junk_string_value_;
                    }
                    if (!allow_trailing_junk && AdvanceToNonspace(ref current, end))
                    {
                        return junk_string_value_;
                    }

                    processed_characters_count = (current - input);
                    return sign ? -double.NaN : double.NaN;
                }
            }

            var leading_zero = false;
            if (current == '0')
            {
                current++;
                if (current == end)
                {
                    processed_characters_count = (current - input);
                    return SignedZero(sign);
                }

                leading_zero = true;

                // It could be hexadecimal value.
                //if ((flags_ & ALLOW_HEX) && (*current == 'x' || *current == 'X'))
                //{
                //    ++current;
                //    if (current == end || !isDigit(*current, 16))
                //    {
                //        return junk_string_value_;  // "0x".
                //    }

                //    bool result_is_junk;
                //    double result = RadixStringToIeee < 4 > (&current,
                //                                         end,
                //                                         sign,
                //                                         allow_trailing_junk,
                //                                         junk_string_value_,
                //                                         read_as_double,
                //                                         &result_is_junk);
                //    if (!result_is_junk)
                //    {
                //        if (allow_trailing_spaces) AdvanceToNonspace(&current, end);
                //        *processed_characters_count = static_cast<int>(current - input);
                //    }
                //    return result;
                //}

                // Ignore leading zeros in the integer part.
                while (current == '0')
                {
                    current++;
                    if (current == end)
                    {
                        processed_characters_count = (current - input);
                        return SignedZero(sign);
                    }
                }
            }

            var octal = leading_zero && (flags_ & Flags.ALLOW_OCTALS) != 0;

            // Copy significant digits of the integer part (if any) to the buffer.
            while (current >= '0' && current <= '9')
            {
                if (significant_digits < kMaxSignificantDigits)
                {
                    buffer[buffer_pos++] = (current.Value);
                    significant_digits++;
                    // Will later check if it's an octal in the buffer.
                }
                else
                {
                    insignificant_digits++;  // Move the digit into the exponential part.
                    nonzero_digit_dropped = nonzero_digit_dropped || current != '0';
                }
                // octal = octal && *current < '8';
                current++;
                if (current == end) goto parsing_done;
            }

            if (significant_digits == 0)
            {
                octal = false;
            }

            if (current == '.')
            {
                if (octal && !allow_trailing_junk) return junk_string_value_;
                if (octal) goto parsing_done;

                current++;
                if (current == end)
                {
                    if (significant_digits == 0 && !leading_zero)
                    {
                        return junk_string_value_;
                    }
                    else
                    {
                        goto parsing_done;
                    }
                }

                if (significant_digits == 0)
                {
                    // octal = false;
                    // Integer part consists of 0 or is absent. Significant digits start after
                    // leading zeros (if any).
                    while (current == '0')
                    {
                        ++current;
                        if (current == end)
                        {
                            processed_characters_count = (current - input);
                            return SignedZero(sign);
                        }
                        exponent--;  // Move this 0 into the exponent.
                    }
                }

                // There is a fractional part.
                // We don't emit a '.', but adjust the exponent instead.
                while (current >= '0' && current <= '9')
                {
                    if (significant_digits < kMaxSignificantDigits)
                    {
                        buffer[buffer_pos++] = current.Value;
                        significant_digits++;
                        exponent--;
                    }
                    else
                    {
                        // Ignore insignificant digits in the fractional part.
                        nonzero_digit_dropped = nonzero_digit_dropped || current != '0';
                    }
                    ++current;
                    if (current == end) goto parsing_done;
                }
            }

            if (!leading_zero && exponent == 0 && significant_digits == 0)
            {
                // If leading_zeros is true then the string contains zeros.
                // If exponent < 0 then string was [+-]\.0*...
                // If significant_digits != 0 the string is not equal to 0.
                // Otherwise there are no digits in the string.
                return junk_string_value_;
            }

            // Parse exponential part.
            if (current == 'e' || current == 'E')
            {
                if (octal && !allow_trailing_junk) return junk_string_value_;
                if (octal) goto parsing_done;
                ++current;
                if (current == end)
                {
                    if (allow_trailing_junk)
                    {
                        goto parsing_done;
                    }
                    else
                    {
                        return junk_string_value_;
                    }
                }
                var exponen_sign = (byte)'+';
                if (current == '+' || current == '-')
                {
                    exponen_sign = current.Value;
                    ++current;
                    if (current == end)
                    {
                        if (allow_trailing_junk)
                        {
                            goto parsing_done;
                        }
                        else
                        {
                            return junk_string_value_;
                        }
                    }
                }

                if (current == end || current < '0' || current > '9')
                {
                    if (allow_trailing_junk)
                    {
                        goto parsing_done;
                    }
                    else
                    {
                        return junk_string_value_;
                    }
                }

                const int max_exponent = int.MaxValue / 2;

                var num = 0;
                do
                {
                    // Check overflow.
                    var digit = current.Value - (byte)'0';
                    if (num >= max_exponent / 10
                        && !(num == max_exponent / 10 && digit <= max_exponent % 10))
						num = max_exponent;
					else
						num = num * 10 + digit;

					++current;
                } while (current != end && current >= '0' && current <= '9');

                exponent += (exponen_sign == '-' ? -num : num);
            }

            if (!(allow_trailing_spaces || allow_trailing_junk) && (current != end))
            {
                return junk_string_value_;
            }
            if (!allow_trailing_junk && AdvanceToNonspace(ref current, end))
            {
                return junk_string_value_;
            }
            if (allow_trailing_spaces)
            {
                AdvanceToNonspace(ref current, end);
            }

            parsing_done:
            exponent += insignificant_digits;

            //if (octal)
            //{
            //    double result;
            //    bool result_is_junk;
            //    char* start = buffer;
            //    result = RadixStringToIeee < 3 > (&start,
            //                                      buffer + buffer_pos,
            //                                      sign,
            //                                      allow_trailing_junk,
            //                                      junk_string_value_,
            //                                      read_as_double,
            //                                      &result_is_junk);
            //    ASSERT(!result_is_junk);
            //    *processed_characters_count = static_cast<int>(current - input);
            //    return result;
            //}

            if (nonzero_digit_dropped)
            {
                buffer[buffer_pos++] = (byte)'1';
                exponent--;
            }

            buffer[buffer_pos] = (byte)'\0';

            double? converted;
            if (read_as_double)
            {
                converted = StringToDouble.Strtod(new Vector(buffer, 0, buffer_pos), exponent);
            }
            else
            {
                converted = StringToDouble.Strtof(new Vector(buffer, 0, buffer_pos), exponent);
            }

            if (converted == null)
            {
                // read-again
                processed_characters_count = (current - input);

                var fallbackbuffer = GetFallbackBuffer();
                BinaryUtil.EnsureCapacity(ref _fallbackBuffer, 0, processed_characters_count);
                var fallbackI = 0;
                while (input != current)
                {
                    fallbackbuffer[fallbackI++] = input.Value;
                    input++;
                }
                var laststr = Encoding.UTF8.GetString(fallbackbuffer, 0, fallbackI);
                return double.Parse(laststr, CultureInfo.InvariantCulture);
            }

            processed_characters_count = (current - input);
            return sign ? -converted.Value : converted.Value;
        }
    }
}
