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
using System.Text;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Internal.DoubleConversion;

namespace Elasticsearch.Net.Utf8Json
{
    // JSON RFC: https://www.ietf.org/rfc/rfc4627.txt

	internal struct JsonReader
    {
		private static readonly ArraySegment<byte> NullTokenSegment = new ArraySegment<byte>(new byte[] { 110, 117, 108, 108 }, 0, 4);
		private static readonly byte[] Bom = Encoding.UTF8.GetPreamble();

		private readonly byte[] _bytes;
		private int _offset;
		private readonly int _initialOffset;

        public JsonReader(byte[] bytes)
            : this(bytes, 0)
        {

        }

        public JsonReader(byte[] bytes, int offset)
        {
            _bytes = bytes;
            _offset = offset;

            // skip bom
            if (bytes.Length >= 3)
            {
                if (bytes[offset] == Bom[0] && bytes[offset + 1] == Bom[1] && bytes[offset + 2] == Bom[2])
					_offset = offset += 3;
			}

			_initialOffset = offset;
        }

		private JsonParsingException CreateParsingException(string expected)
        {
            var actual = ((char)_bytes[_offset]).ToString();
            var pos = _offset;

            try
            {
                var token = GetCurrentJsonToken();
                switch (token)
                {
                    case JsonToken.Number:
                        var ns = ReadNumberSegment();
                        actual = StringEncoding.UTF8.GetString(ns.Array, ns.Offset, ns.Count);
                        break;
                    case JsonToken.String:
                        actual = "\"" + ReadString() + "\"";
                        break;
                    case JsonToken.True:
                        actual = "true";
                        break;
                    case JsonToken.False:
                        actual = "false";
                        break;
                    case JsonToken.Null:
                        actual = "null";
                        break;
                    default:
                        break;
                }
            }
            catch { }

            return new JsonParsingException("expected:'" + expected + "', actual:'" + actual + "', at offset:" + pos, _bytes, pos, _offset, actual);
        }

		private JsonParsingException CreateParsingExceptionMessage(string message)
        {
            var actual = ((char)_bytes[_offset]).ToString();
            var pos = _offset;

            return new JsonParsingException(message, _bytes, pos, pos, actual);
        }

		private bool IsInRange => _offset < _bytes.Length;

		/// <summary>
		/// Advances the offset by a specified amount
		/// </summary>
		/// <param name="offset">The amount to offset by</param>
        public void AdvanceOffset(int offset) => _offset += offset;

		/// <summary>
		/// Resets the offset of the reader back to the original offset
		/// </summary>
		public void ResetOffset() => _offset = _initialOffset;

		public byte[] GetBufferUnsafe() => _bytes;

		public int GetCurrentOffsetUnsafe() => _offset;

		public JsonToken GetCurrentJsonToken()
        {
            SkipWhiteSpace();
            if (_offset < _bytes.Length)
            {
                var c = _bytes[_offset];
                switch (c)
                {
                    case (byte)'{': return JsonToken.BeginObject;
                    case (byte)'}': return JsonToken.EndObject;
                    case (byte)'[': return JsonToken.BeginArray;
                    case (byte)']': return JsonToken.EndArray;
                    case (byte)'t': return JsonToken.True;
                    case (byte)'f': return JsonToken.False;
                    case (byte)'n': return JsonToken.Null;
                    case (byte)',': return JsonToken.ValueSeparator;
                    case (byte)':': return JsonToken.NameSeparator;
                    case (byte)'-': return JsonToken.Number;
                    case (byte)'0': return JsonToken.Number;
                    case (byte)'1': return JsonToken.Number;
                    case (byte)'2': return JsonToken.Number;
                    case (byte)'3': return JsonToken.Number;
                    case (byte)'4': return JsonToken.Number;
                    case (byte)'5': return JsonToken.Number;
                    case (byte)'6': return JsonToken.Number;
                    case (byte)'7': return JsonToken.Number;
                    case (byte)'8': return JsonToken.Number;
                    case (byte)'9': return JsonToken.Number;
                    case (byte)'\"': return JsonToken.String;
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 46:
                    case 47:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 67:
                    case 68:
                    case 69:
                    case 70:
                    case 71:
                    case 72:
                    case 73:
                    case 74:
                    case 75:
                    case 76:
                    case 77:
                    case 78:
                    case 79:
                    case 80:
                    case 81:
                    case 82:
                    case 83:
                    case 84:
                    case 85:
                    case 86:
                    case 87:
                    case 88:
                    case 89:
                    case 90:
                    case 92:
                    case 94:
                    case 95:
                    case 96:
                    case 97:
                    case 98:
                    case 99:
                    case 100:
                    case 101:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 107:
                    case 108:
                    case 109:
                    case 111:
                    case 112:
                    case 113:
                    case 114:
                    case 115:
                    case 117:
                    case 118:
                    case 119:
                    case 120:
                    case 121:
                    case 122:
                    default:
                        return JsonToken.None;
                }
            }

			return JsonToken.None;
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipWhiteSpace()
        {
            // eliminate array bound check
            for (var i = _offset; i < _bytes.Length; i++)
            {
                switch (_bytes[i])
                {
                    case 0x20: // Space
                    case 0x09: // Horizontal tab
                    case 0x0A: // Line feed or New line
                    case 0x0D: // Carriage return
                        continue;
                    case (byte)'/': // BeginComment
                        i = ReadComment(_bytes, i);
                        continue;
                    // optimize skip jumptable
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 11:
                    case 12:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    default:
                        _offset = i;
                        return; // end
                }
            }

            _offset = _bytes.Length;
        }

        public bool ReadIsNull()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == 'n')
            {
                if (_bytes[_offset + 1] != 'u') goto ERROR;
                if (_bytes[_offset + 2] != 'l') goto ERROR;
                if (_bytes[_offset + 3] != 'l') goto ERROR;
                _offset += 4;
                return true;
            }

			return false;

			ERROR:
            throw CreateParsingException("null");
        }

        public bool ReadIsBeginArray()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == '[')
            {
                _offset += 1;
                return true;
            }

			return false;
		}

        public void ReadIsBeginArrayWithVerify()
        {
            if (!ReadIsBeginArray()) throw CreateParsingException("[");
        }

        public bool ReadIsEndArray()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == ']')
            {
                _offset += 1;
                return true;
            }

			return false;
		}

        public void ReadIsEndArrayWithVerify()
        {
            if (!ReadIsEndArray()) throw CreateParsingException("]");
        }

        public bool ReadIsEndArrayWithSkipValueSeparator(ref int count)
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == ']')
            {
                _offset += 1;
                return true;
            }

			if (count++ != 0)
				ReadIsValueSeparatorWithVerify();

			return false;
		}

        /// <summary>
        /// Convinient pattern of ReadIsBeginArrayWithVerify + while(!ReadIsEndArrayWithSkipValueSeparator)
        /// </summary>
        public bool ReadIsInArray(ref int count)
        {
            if (count == 0)
            {
                ReadIsBeginArrayWithVerify();
                if (ReadIsEndArray())
					return false;
			}
            else
			{
				if (ReadIsEndArray())
					return false;

				ReadIsValueSeparatorWithVerify();
			}

            count++;
            return true;
        }

        public bool ReadIsBeginObject()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == '{')
            {
                _offset += 1;
                return true;
            }

			return false;
		}

        public void ReadIsBeginObjectWithVerify()
        {
            if (!ReadIsBeginObject()) throw CreateParsingException("{");
        }

        public bool ReadIsEndObject()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == '}')
            {
                _offset += 1;
                return true;
            }

			return false;
		}
        public void ReadIsEndObjectWithVerify()
        {
            if (!ReadIsEndObject()) throw CreateParsingException("}");
        }

        public bool ReadIsEndObjectWithSkipValueSeparator(ref int count)
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == '}')
            {
                _offset += 1;
                return true;
            }

			if (count++ != 0)
				ReadIsValueSeparatorWithVerify();

			return false;
		}

        /// <summary>
        /// Convinient pattern of ReadIsBeginObjectWithVerify + while(!ReadIsEndObjectWithSkipValueSeparator)
        /// </summary>
        public bool ReadIsInObject(ref int count)
        {
            if (count == 0)
            {
                ReadIsBeginObjectWithVerify();
                if (ReadIsEndObject())
					return false;
			}
            else
			{
				if (ReadIsEndObject())
					return false;

				ReadIsValueSeparatorWithVerify();
			}

            count++;
            return true;
        }

        public bool ReadIsValueSeparator()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == ',')
            {
                _offset += 1;
                return true;
            }

			return false;
		}

        public void ReadIsValueSeparatorWithVerify()
        {
            if (!ReadIsValueSeparator()) throw CreateParsingException(",");
        }

        public bool ReadIsNameSeparator()
        {
            SkipWhiteSpace();
            if (IsInRange && _bytes[_offset] == ':')
            {
                _offset += 1;
                return true;
            }

			return false;
		}

        public void ReadIsNameSeparatorWithVerify()
        {
            if (!ReadIsNameSeparator()) throw CreateParsingException(":");
        }

		private void ReadStringSegmentCore(out byte[] resultBytes, out int resultOffset, out int resultLength)
        {
            // SkipWhiteSpace is already called from IsNull

            byte[] builder = null;
            var builderOffset = 0;
            char[] codePointStringBuffer = null;
            var codePointStringOffet = 0;

            if (_bytes[_offset] != '\"') throw CreateParsingException("String Begin Token");
            _offset++;

            var from = _offset;

            // eliminate array-bound check
            for (var i = _offset; i < _bytes.Length; i++)
            {
                byte escapeCharacter = 0;
                switch (_bytes[i])
                {
                    case (byte)'\\': // escape character
                        switch ((char)_bytes[i + 1])
                        {
                            case '"':
                            case '\\':
                            case '/':
                                escapeCharacter = _bytes[i + 1];
                                goto COPY;
                            case 'b':
                                escapeCharacter = (byte)'\b';
                                goto COPY;
                            case 'f':
                                escapeCharacter = (byte)'\f';
                                goto COPY;
                            case 'n':
                                escapeCharacter = (byte)'\n';
                                goto COPY;
                            case 'r':
                                escapeCharacter = (byte)'\r';
                                goto COPY;
                            case 't':
                                escapeCharacter = (byte)'\t';
                                goto COPY;
                            case 'u':
                                codePointStringBuffer ??= StringBuilderCache.GetCodePointStringBuffer();

                                if (codePointStringOffet == 0)
                                {
                                    builder ??= StringBuilderCache.GetBuffer();

                                    var copyCount = i - from;
                                    BinaryUtil.EnsureCapacity(ref builder, builderOffset, copyCount + 1); // require + 1
                                    Buffer.BlockCopy(_bytes, from, builder, builderOffset, copyCount);
                                    builderOffset += copyCount;
                                }

                                if (codePointStringBuffer.Length == codePointStringOffet)
									Array.Resize(ref codePointStringBuffer, codePointStringBuffer.Length * 2);

								var a = (char)_bytes[i + 2];
                                var b = (char)_bytes[i + 3];
                                var c = (char)_bytes[i + 4];
                                var d = (char)_bytes[i + 5];
                                var codepoint = GetCodePoint(a, b, c, d);
                                codePointStringBuffer[codePointStringOffet++] = (char)codepoint;
                                i += 5;
                                _offset += 6;
                                from = _offset;
                                continue;
                            default:
                                throw CreateParsingExceptionMessage("Bad JSON escape.");
                        }
                    case (byte)'"': // endtoken
                        _offset++;
                        goto END;
                    default: // string
                        if (codePointStringOffet != 0)
                        {
                            if (builder == null) builder = StringBuilderCache.GetBuffer();
                            BinaryUtil.EnsureCapacity(ref builder, builderOffset, StringEncoding.UTF8.GetMaxByteCount(codePointStringOffet));
                            builderOffset += StringEncoding.UTF8.GetBytes(codePointStringBuffer, 0, codePointStringOffet, builder, builderOffset);
                            codePointStringOffet = 0;
                        }
                        _offset++;
                        continue;
                }

                COPY:
                {
                    if (builder == null) builder = StringBuilderCache.GetBuffer();
                    if (codePointStringOffet != 0)
                    {
                        BinaryUtil.EnsureCapacity(ref builder, builderOffset, StringEncoding.UTF8.GetMaxByteCount(codePointStringOffet));
                        builderOffset += StringEncoding.UTF8.GetBytes(codePointStringBuffer, 0, codePointStringOffet, builder, builderOffset);
                        codePointStringOffet = 0;
                    }

                    var copyCount = i - from;
                    BinaryUtil.EnsureCapacity(ref builder, builderOffset, copyCount + 1); // require + 1!
                    Buffer.BlockCopy(_bytes, from, builder, builderOffset, copyCount);
                    builderOffset += copyCount;
                    builder[builderOffset++] = escapeCharacter;
                    i += 1;
                    _offset += 2;
                    from = _offset;
                }
            }

            resultLength = 0;
            resultBytes = null;
            resultOffset = 0;
            throw CreateParsingException("String End Token");

            END:
            if (builderOffset == 0 && codePointStringOffet == 0) // no escape
            {
                resultBytes = _bytes;
                resultOffset = from;
                resultLength = _offset - 1 - from; // skip last quote
            }
            else
            {
                if (builder == null) builder = StringBuilderCache.GetBuffer();
                if (codePointStringOffet != 0)
                {
                    BinaryUtil.EnsureCapacity(ref builder, builderOffset, StringEncoding.UTF8.GetMaxByteCount(codePointStringOffet));
                    builderOffset += StringEncoding.UTF8.GetBytes(codePointStringBuffer, 0, codePointStringOffet, builder, builderOffset);
                    codePointStringOffet = 0;
                }

                var copyCount = _offset - from - 1;
                BinaryUtil.EnsureCapacity(ref builder, builderOffset, copyCount);
                Buffer.BlockCopy(_bytes, from, builder, builderOffset, copyCount);
                builderOffset += copyCount;

                resultBytes = builder;
                resultOffset = 0;
                resultLength = builderOffset;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetCodePoint(char a, char b, char c, char d) =>
			(((((ToNumber(a) * 16) + ToNumber(b)) * 16) + ToNumber(c)) * 16) + ToNumber(d);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int ToNumber(char x)
        {
            if ('0' <= x && x <= '9')
				return x - '0';

			if ('a' <= x && x <= 'f')
				return x - 'a' + 10;

			if ('A' <= x && x <= 'F')
				return x - 'A' + 10;

			throw new JsonParsingException("Invalid Character" + x);
        }

        public ArraySegment<byte> ReadStringSegmentUnsafe()
        {
            if (ReadIsNull()) return NullTokenSegment;

			ReadStringSegmentCore(out var bytes, out var offset, out var length);
            return new ArraySegment<byte>(bytes, offset, length);
        }

        public string ReadString()
        {
            if (ReadIsNull()) return null;

			ReadStringSegmentCore(out var bytes, out var offset, out var length);
			return Encoding.UTF8.GetString(bytes, offset, length);
        }

        /// <summary>ReadString + ReadIsNameSeparatorWithVerify</summary>
        public string ReadPropertyName()
        {
            var key = ReadString();
            ReadIsNameSeparatorWithVerify();
            return key;
        }

        /// <summary>Get raw string-span(do not unescape)</summary>
        public ArraySegment<byte> ReadStringSegmentRaw()
        {
            ArraySegment<byte> key;
            if (ReadIsNull())
				key = NullTokenSegment;
			else
            {
                // SkipWhiteSpace is already called from IsNull
                if (_bytes[_offset++] != '\"') throw CreateParsingException("\"");

                var from = _offset;

                for (var i = _offset; i < _bytes.Length; i++)
                {
                    if (_bytes[i] == '\"')
					{
						// is escape?
                        if (_bytes[i - 1] == '\\')
							continue;

						_offset = i + 1;
						goto OK;
					}
                }
                throw CreateParsingExceptionMessage("not found end string.");

                OK:
                key = new ArraySegment<byte>(_bytes, from, _offset - from - 1); // remove \"
            }

            return key;
        }

        /// <summary>Get raw string-span(do not unescape) + ReadIsNameSeparatorWithVerify</summary>
        public ArraySegment<byte> ReadPropertyNameSegmentRaw()
        {
            var key = ReadStringSegmentRaw();
            ReadIsNameSeparatorWithVerify();
            return key;
        }

        public bool ReadBoolean()
        {
            SkipWhiteSpace();
            if (_bytes[_offset] == 't')
            {
                if (_bytes[_offset + 1] != 'r') goto ERROR_TRUE;
                if (_bytes[_offset + 2] != 'u') goto ERROR_TRUE;
                if (_bytes[_offset + 3] != 'e') goto ERROR_TRUE;
                _offset += 4;
                return true;
            }

			if (_bytes[_offset] == 'f')
			{
				if (_bytes[_offset + 1] != 'a') goto ERROR_FALSE;
				if (_bytes[_offset + 2] != 'l') goto ERROR_FALSE;
				if (_bytes[_offset + 3] != 's') goto ERROR_FALSE;
				if (_bytes[_offset + 4] != 'e') goto ERROR_FALSE;
				_offset += 5;
				return false;
			}

			throw CreateParsingException("true | false");

			ERROR_TRUE:
            throw CreateParsingException("true");
            ERROR_FALSE:
            throw CreateParsingException("false");
        }

		private static bool IsWordBreak(byte c)
        {
            switch (c)
            {
                case (byte)' ':
                case (byte)'{':
                case (byte)'}':
                case (byte)'[':
                case (byte)']':
                case (byte)',':
                case (byte)':':
                case (byte)'\"':
                    return true;
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 33:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                case 92:
                case 94:
                case 95:
                case 96:
                case 97:
                case 98:
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                case 106:
                case 107:
                case 108:
                case 109:
                case 110:
                case 111:
                case 112:
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                case 120:
                case 121:
                case 122:
                default:
                    return false;
            }
        }

        public void ReadNext()
        {
            var token = GetCurrentJsonToken();
            ReadNextCore(token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ReadNextCore(JsonToken token)
        {
            switch (token)
            {
                case JsonToken.BeginObject:
                case JsonToken.BeginArray:
                case JsonToken.ValueSeparator:
                case JsonToken.NameSeparator:
                case JsonToken.EndObject:
                case JsonToken.EndArray:
                    _offset += 1;
                    break;
                case JsonToken.True:
                case JsonToken.Null:
                    _offset += 4;
                    break;
                case JsonToken.False:
                    _offset += 5;
                    break;
                case JsonToken.String:
                    _offset += 1; // position is "\"";
                    for (var i = _offset; i < _bytes.Length; i++)
                    {
                        if (_bytes[i] == '\"')
                        {
							// backtrack and count escape characters
							var count = 0;
							for (var j = i - 1; j >= _offset; j--)
							{
								if (_bytes[j] != '\\')
									break;

								count++;
							}

							// even number of escape characters means this " is not escaped.
							if (count % 2 == 0)
							{
								_offset = i + 1;
								return; // end
							}
                        }
                    }
                    throw CreateParsingExceptionMessage("not found end string.");
                case JsonToken.Number:
                    for (var i = _offset; i < _bytes.Length; i++)
                    {
                        if (IsWordBreak(_bytes[i]))
                        {
                            _offset = i;
                            return;
                        }
                    }
                    _offset = _bytes.Length;
                    break;
                case JsonToken.None:
                default:
                    break;
            }
        }

        public void ReadNextBlock()
        {
            var stack = 0;

            AGAIN:
            var token = GetCurrentJsonToken();
            switch (token)
            {
                case JsonToken.BeginObject:
                case JsonToken.BeginArray:
                    _offset++;
                    stack++;
                    goto AGAIN;
                case JsonToken.EndObject:
                case JsonToken.EndArray:
                    _offset++;
                    stack--;
                    if (stack != 0)
						goto AGAIN;

					break;
                case JsonToken.True:
                case JsonToken.False:
                case JsonToken.Null:
                case JsonToken.String:
                case JsonToken.Number:
                case JsonToken.NameSeparator:
                case JsonToken.ValueSeparator:
                    do
                    {
                        ReadNextCore(token);
                        token = GetCurrentJsonToken();
                    } while (stack != 0 && !((int)token < 5)); // !(None, Begin/EndObject, Begin/EndArray)

                    if (stack != 0)
						goto AGAIN;

					break;
                case JsonToken.None:
                default:
                    break;
            }
        }

        public ArraySegment<byte> ReadNextBlockSegment()
        {
            var startOffset = _offset;
            ReadNextBlock();
            return new ArraySegment<byte>(_bytes, startOffset, _offset - startOffset);
        }

        public sbyte ReadSByte() => checked((sbyte)ReadInt64());

		public short ReadInt16() => checked((short)ReadInt64());

		public int ReadInt32() => checked((int)ReadInt64());

		public long ReadInt64()
        {
            SkipWhiteSpace();

			var v = NumberConverter.ReadInt64(_bytes, _offset, out var readCount);
            if (readCount == 0)
				throw CreateParsingException("Number Token");

			_offset += readCount;
            return v;
        }

        public byte ReadByte() => checked((byte)ReadUInt64());

		public ushort ReadUInt16() => checked((ushort)ReadUInt64());

		public uint ReadUInt32() => checked((uint)ReadUInt64());

		public ulong ReadUInt64()
        {
            SkipWhiteSpace();

			var v = NumberConverter.ReadUInt64(_bytes, _offset, out var readCount);
            if (readCount == 0)
				throw CreateParsingException("Number Token");

			_offset += readCount;
            return v;
        }

        public float ReadSingle()
        {
            SkipWhiteSpace();
			var v = StringToDoubleConverter.ToSingle(_bytes, _offset, out var readCount);
            if (readCount == 0)
				throw CreateParsingException("Number Token");

			_offset += readCount;
            return v;
        }

        public double ReadDouble()
        {
            SkipWhiteSpace();
			var v = StringToDoubleConverter.ToDouble(_bytes, _offset, out var readCount);
            if (readCount == 0)
				throw CreateParsingException("Number Token");

			_offset += readCount;
            return v;
        }

        public ArraySegment<byte> ReadNumberSegment()
        {
			SkipWhiteSpace();
			var initialOffset = _offset;
			for (var i = _offset; i < _bytes.Length; i++)
			{
				if (!NumberConverter.IsNumberRepresentation(_bytes[i]))
				{
					if (NumberConverter.IsENotation(_bytes[i]) && (i + 1) < _bytes.Length && NumberConverter.IsNumberRepresentation(_bytes[i + 1]))
					{
						i++;
						continue;
					}

					_offset = i;
					goto END;
				}
			}
			_offset = _bytes.Length;

			END:
			return new ArraySegment<byte>(_bytes, initialOffset, _offset - initialOffset);
        }

        // return last offset.
		private static int ReadComment(byte[] bytes, int offset)
        {
            // current token is '/'
            if (bytes[offset + 1] == '/')
            {
                // single line
                offset += 2;
                for (var i = offset; i < bytes.Length; i++)
                {
                    if (bytes[i] == '\r' || bytes[i] == '\n')
						return i;
				}

                throw new JsonParsingException("Can not find end token of single line comment(\r or \n).");
            }

			if (bytes[offset + 1] == '*')
			{

				offset += 2; // '/' + '*';
				for (var i = offset; i < bytes.Length; i++)
				{
					if (bytes[i] == '*' && bytes[i + 1] == '/')
						return i + 1;
				}
				throw new JsonParsingException("Can not find end token of multi line comment(*/).");
			}

			return offset;
        }

		private static class StringBuilderCache
        {
            [ThreadStatic] private static byte[] _buffer;

            [ThreadStatic] private static char[] _codePointStringBuffer;

            public static byte[] GetBuffer() => _buffer ??= new byte[65535];

			public static char[] GetCodePointStringBuffer() => _codePointStringBuffer ??= new char[65535];
		}
    }

    public class JsonParsingException : Exception
    {
		private readonly WeakReference _underlyingBytes;
		private readonly int _limit;
        public int Offset { get; }
        public string ActualChar { get; }

        public JsonParsingException(string message)
            : base(message)
        {
		}

        public JsonParsingException(string message, byte[] underlyingBytes, int offset, int limit, string actualChar)
            : base(message)
        {
            _underlyingBytes = new WeakReference(underlyingBytes);
            Offset = offset;
            ActualChar = actualChar;
            _limit = limit;
        }

        /// <summary>
        /// Underlying bytes is may be a pooling buffer, be careful to use it. If lost reference or can not handled byte[], return null.
        /// </summary>
        public byte[] GetUnderlyingByteArrayUnsafe() => _underlyingBytes.Target as byte[];

		/// <summary>
        /// Underlying bytes is may be a pooling buffer, be careful to use it. If lost reference or can not handled byte[], return null.
        /// </summary>
        public string GetUnderlyingStringUnsafe()
        {
			if (_underlyingBytes.Target is byte[] bytes)
				return StringEncoding.UTF8.GetString(bytes, 0, _limit) + "...";

			return null;
        }
    }
}
