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
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Formatters
{
    internal sealed class SByteFormatter : IJsonFormatter<sbyte>, IObjectPropertyNameFormatter<sbyte>
    {
        public static readonly SByteFormatter Default = new SByteFormatter();

        public void Serialize(ref JsonWriter writer, sbyte value, IJsonFormatterResolver formatterResolver) => writer.WriteSByte(value);

        public sbyte Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadSByte();

        public void SerializeToPropertyName(ref JsonWriter writer, sbyte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteSByte(value);
            writer.WriteQuotation();
        }

        public sbyte DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadSByte(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableSByteFormatter : IJsonFormatter<sbyte?>, IObjectPropertyNameFormatter<sbyte?>
    {
        public static readonly NullableSByteFormatter Default = new NullableSByteFormatter();

        public void Serialize(ref JsonWriter writer, sbyte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteSByte(value.Value);
        }

        public sbyte? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadSByte();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, sbyte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteSByte(value.Value);
            writer.WriteQuotation();
        }

        public sbyte? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadSByte(key.Array, key.Offset, out _);
        }
    }

    internal sealed class SByteArrayFormatter : IJsonFormatter<sbyte[]>
    {
        public static readonly SByteArrayFormatter Default = new SByteArrayFormatter();

        public void Serialize(ref JsonWriter writer, sbyte[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteSByte(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteSByte(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public sbyte[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new sbyte[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadSByte();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class Int16Formatter : IJsonFormatter<short>, IObjectPropertyNameFormatter<short>
    {
        public static readonly Int16Formatter Default = new Int16Formatter();

        public void Serialize(ref JsonWriter writer, short value, IJsonFormatterResolver formatterResolver) => writer.WriteInt16(value);

        public short Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadInt16();

        public void SerializeToPropertyName(ref JsonWriter writer, short value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteInt16(value);
            writer.WriteQuotation();
        }

        public short DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadInt16(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableInt16Formatter : IJsonFormatter<short?>, IObjectPropertyNameFormatter<short?>
    {
        public static readonly NullableInt16Formatter Default = new NullableInt16Formatter();

        public void Serialize(ref JsonWriter writer, short? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteInt16(value.Value);
        }

        public short? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadInt16();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, short? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteInt16(value.Value);
            writer.WriteQuotation();
        }

        public short? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadInt16(key.Array, key.Offset, out _);
        }
    }

    internal sealed class Int16ArrayFormatter : IJsonFormatter<short[]>
    {
        public static readonly Int16ArrayFormatter Default = new Int16ArrayFormatter();

        public void Serialize(ref JsonWriter writer, short[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteInt16(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteInt16(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public short[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new short[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadInt16();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class Int32Formatter : IJsonFormatter<int>, IObjectPropertyNameFormatter<int>
    {
        public static readonly Int32Formatter Default = new Int32Formatter();

        public void Serialize(ref JsonWriter writer, int value, IJsonFormatterResolver formatterResolver) => writer.WriteInt32(value);

        public int Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadInt32();

        public void SerializeToPropertyName(ref JsonWriter writer, int value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteInt32(value);
            writer.WriteQuotation();
        }

        public int DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadInt32(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableInt32Formatter : IJsonFormatter<int?>, IObjectPropertyNameFormatter<int?>
    {
        public static readonly NullableInt32Formatter Default = new NullableInt32Formatter();

        public void Serialize(ref JsonWriter writer, int? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteInt32(value.Value);
        }

        public int? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadInt32();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, int? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteInt32(value.Value);
            writer.WriteQuotation();
        }

        public int? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadInt32(key.Array, key.Offset, out _);
        }
    }

    internal sealed class Int32ArrayFormatter : IJsonFormatter<int[]>
    {
        public static readonly Int32ArrayFormatter Default = new Int32ArrayFormatter();

        public void Serialize(ref JsonWriter writer, int[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteInt32(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteInt32(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public int[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new int[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadInt32();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class Int64Formatter : IJsonFormatter<long>, IObjectPropertyNameFormatter<long>
    {
        public static readonly Int64Formatter Default = new Int64Formatter();

        public void Serialize(ref JsonWriter writer, long value, IJsonFormatterResolver formatterResolver) => writer.WriteInt64(value);

        public long Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadInt64();

        public void SerializeToPropertyName(ref JsonWriter writer, long value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteInt64(value);
            writer.WriteQuotation();
        }

        public long DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadInt64(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableInt64Formatter : IJsonFormatter<long?>, IObjectPropertyNameFormatter<long?>
    {
        public static readonly NullableInt64Formatter Default = new NullableInt64Formatter();

        public void Serialize(ref JsonWriter writer, long? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteInt64(value.Value);
        }

        public long? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadInt64();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, long? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteInt64(value.Value);
            writer.WriteQuotation();
        }

        public long? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadInt64(key.Array, key.Offset, out _);
        }
    }

    internal sealed class Int64ArrayFormatter : IJsonFormatter<long[]>
    {
        public static readonly Int64ArrayFormatter Default = new Int64ArrayFormatter();

        public void Serialize(ref JsonWriter writer, long[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteInt64(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteInt64(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public long[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new long[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadInt64();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class ByteFormatter : IJsonFormatter<byte>, IObjectPropertyNameFormatter<byte>
    {
        public static readonly ByteFormatter Default = new ByteFormatter();

        public void Serialize(ref JsonWriter writer, byte value, IJsonFormatterResolver formatterResolver) => writer.WriteByte(value);

        public byte Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadByte();

        public void SerializeToPropertyName(ref JsonWriter writer, byte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteByte(value);
            writer.WriteQuotation();
        }

        public byte DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadByte(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableByteFormatter : IJsonFormatter<byte?>, IObjectPropertyNameFormatter<byte?>
    {
        public static readonly NullableByteFormatter Default = new NullableByteFormatter();

        public void Serialize(ref JsonWriter writer, byte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteByte(value.Value);
        }

        public byte? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadByte();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, byte? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteByte(value.Value);
            writer.WriteQuotation();
        }

        public byte? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadByte(key.Array, key.Offset, out _);
        }
    }


    internal sealed class UInt16Formatter : IJsonFormatter<ushort>, IObjectPropertyNameFormatter<ushort>
    {
        public static readonly UInt16Formatter Default = new UInt16Formatter();

        public void Serialize(ref JsonWriter writer, ushort value, IJsonFormatterResolver formatterResolver) => writer.WriteUInt16(value);

        public ushort Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadUInt16();

        public void SerializeToPropertyName(ref JsonWriter writer, ushort value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteUInt16(value);
            writer.WriteQuotation();
        }

        public ushort DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadUInt16(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableUInt16Formatter : IJsonFormatter<ushort?>, IObjectPropertyNameFormatter<ushort?>
    {
        public static readonly NullableUInt16Formatter Default = new NullableUInt16Formatter();

        public void Serialize(ref JsonWriter writer, ushort? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteUInt16(value.Value);
        }

        public ushort? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadUInt16();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, ushort? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteUInt16(value.Value);
            writer.WriteQuotation();
        }

        public ushort? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadUInt16(key.Array, key.Offset, out _);
        }
    }

    internal sealed class UInt16ArrayFormatter : IJsonFormatter<ushort[]>
    {
        public static readonly UInt16ArrayFormatter Default = new UInt16ArrayFormatter();

        public void Serialize(ref JsonWriter writer, ushort[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteUInt16(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteUInt16(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public ushort[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new ushort[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadUInt16();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class UInt32Formatter : IJsonFormatter<uint>, IObjectPropertyNameFormatter<uint>
    {
        public static readonly UInt32Formatter Default = new UInt32Formatter();

        public void Serialize(ref JsonWriter writer, uint value, IJsonFormatterResolver formatterResolver) => writer.WriteUInt32(value);

        public uint Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadUInt32();

        public void SerializeToPropertyName(ref JsonWriter writer, uint value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteUInt32(value);
            writer.WriteQuotation();
        }

        public uint DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadUInt32(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableUInt32Formatter : IJsonFormatter<uint?>, IObjectPropertyNameFormatter<uint?>
    {
        public static readonly NullableUInt32Formatter Default = new NullableUInt32Formatter();

        public void Serialize(ref JsonWriter writer, uint? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteUInt32(value.Value);
        }

        public uint? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadUInt32();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, uint? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteUInt32(value.Value);
            writer.WriteQuotation();
        }

        public uint? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadUInt32(key.Array, key.Offset, out _);
        }
    }

    internal sealed class UInt32ArrayFormatter : IJsonFormatter<uint[]>
    {
        public static readonly UInt32ArrayFormatter Default = new UInt32ArrayFormatter();

        public void Serialize(ref JsonWriter writer, uint[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteUInt32(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteUInt32(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public uint[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new uint[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadUInt32();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class UInt64Formatter : IJsonFormatter<ulong>, IObjectPropertyNameFormatter<ulong>
    {
        public static readonly UInt64Formatter Default = new UInt64Formatter();

        public void Serialize(ref JsonWriter writer, ulong value, IJsonFormatterResolver formatterResolver) => writer.WriteUInt64(value);

        public ulong Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadUInt64();

        public void SerializeToPropertyName(ref JsonWriter writer, ulong value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteUInt64(value);
            writer.WriteQuotation();
        }

        public ulong DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadUInt64(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableUInt64Formatter : IJsonFormatter<ulong?>, IObjectPropertyNameFormatter<ulong?>
    {
        public static readonly NullableUInt64Formatter Default = new NullableUInt64Formatter();

        public void Serialize(ref JsonWriter writer, ulong? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteUInt64(value.Value);
        }

        public ulong? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadUInt64();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, ulong? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteUInt64(value.Value);
            writer.WriteQuotation();
        }

        public ulong? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadUInt64(key.Array, key.Offset, out _);
        }
    }

    internal sealed class UInt64ArrayFormatter : IJsonFormatter<ulong[]>
    {
        public static readonly UInt64ArrayFormatter Default = new UInt64ArrayFormatter();

        public void Serialize(ref JsonWriter writer, ulong[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteUInt64(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteUInt64(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public ulong[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new ulong[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadUInt64();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class SingleFormatter : IJsonFormatter<float>, IObjectPropertyNameFormatter<float>
    {
        public static readonly SingleFormatter Default = new SingleFormatter();

        public void Serialize(ref JsonWriter writer, float value, IJsonFormatterResolver formatterResolver) => writer.WriteSingle(value);

        public float Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadSingle();

        public void SerializeToPropertyName(ref JsonWriter writer, float value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteSingle(value);
            writer.WriteQuotation();
        }

        public float DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadSingle(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableSingleFormatter : IJsonFormatter<float?>, IObjectPropertyNameFormatter<float?>
    {
        public static readonly NullableSingleFormatter Default = new NullableSingleFormatter();

        public void Serialize(ref JsonWriter writer, float? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteSingle(value.Value);
        }

        public float? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadSingle();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, float? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteSingle(value.Value);
            writer.WriteQuotation();
        }

        public float? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadSingle(key.Array, key.Offset, out _);
        }
    }

    internal sealed class SingleArrayFormatter : IJsonFormatter<float[]>
    {
        public static readonly SingleArrayFormatter Default = new SingleArrayFormatter();

        public void Serialize(ref JsonWriter writer, float[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteSingle(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteSingle(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public float[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new float[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadSingle();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class DoubleFormatter : IJsonFormatter<double>, IObjectPropertyNameFormatter<double>
    {
        public static readonly DoubleFormatter Default = new DoubleFormatter();

        public void Serialize(ref JsonWriter writer, double value, IJsonFormatterResolver formatterResolver) => writer.WriteDouble(value);

        public double Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadDouble();

        public void SerializeToPropertyName(ref JsonWriter writer, double value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteDouble(value);
            writer.WriteQuotation();
        }

        public double DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadDouble(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableDoubleFormatter : IJsonFormatter<double?>, IObjectPropertyNameFormatter<double?>
    {
        public static readonly NullableDoubleFormatter Default = new NullableDoubleFormatter();

        public void Serialize(ref JsonWriter writer, double? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteDouble(value.Value);
        }

        public double? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadDouble();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, double? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteDouble(value.Value);
            writer.WriteQuotation();
        }

        public double? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadDouble(key.Array, key.Offset, out _);
        }
    }

    internal sealed class DoubleArrayFormatter : IJsonFormatter<double[]>
    {
        public static readonly DoubleArrayFormatter Default = new DoubleArrayFormatter();

        public void Serialize(ref JsonWriter writer, double[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteDouble(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteDouble(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public double[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new double[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadDouble();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

    internal sealed class BooleanFormatter : IJsonFormatter<bool>, IObjectPropertyNameFormatter<bool>
    {
        public static readonly BooleanFormatter Default = new BooleanFormatter();

        public void Serialize(ref JsonWriter writer, bool value, IJsonFormatterResolver formatterResolver) => writer.WriteBoolean(value);

        public bool Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) => reader.ReadBoolean();

        public void SerializeToPropertyName(ref JsonWriter writer, bool value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteQuotation();
            writer.WriteBoolean(value);
            writer.WriteQuotation();
        }

        public bool DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadBoolean(key.Array, key.Offset, out _);
        }
    }

    internal sealed class NullableBooleanFormatter : IJsonFormatter<bool?>, IObjectPropertyNameFormatter<bool?>
    {
        public static readonly NullableBooleanFormatter Default = new NullableBooleanFormatter();

        public void Serialize(ref JsonWriter writer, bool? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
                writer.WriteBoolean(value.Value);
        }

        public bool? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            return reader.ReadBoolean();
        }

        public void SerializeToPropertyName(ref JsonWriter writer, bool? value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteQuotation();
            writer.WriteBoolean(value.Value);
            writer.WriteQuotation();
        }

        public bool? DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var key = reader.ReadStringSegmentRaw();
            return NumberConverter.ReadBoolean(key.Array, key.Offset, out _);
        }
    }

    internal sealed class BooleanArrayFormatter : IJsonFormatter<bool[]>
    {
        public static readonly BooleanArrayFormatter Default = new BooleanArrayFormatter();

        public void Serialize(ref JsonWriter writer, bool[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
                writer.WriteNull();
            else
            {
                writer.WriteBeginArray();

                if (value.Length != 0)
                    writer.WriteBoolean(value[0]);
                for (var i = 1; i < value.Length; i++)
                {
                    writer.WriteValueSeparator();
                    writer.WriteBoolean(value[i]);
                }

                writer.WriteEndArray();
            }
        }

        public bool[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
                return null;

            reader.ReadIsBeginArrayWithVerify();
            var array = new bool[4];
            var count = 0;
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                if (array.Length < count)
                    Array.Resize(ref array, count * 2);

                array[count - 1] = reader.ReadBoolean();
            }

            Array.Resize(ref array, count);
            return array;
        }
    }

}
