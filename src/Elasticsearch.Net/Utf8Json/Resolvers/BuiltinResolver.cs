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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Elasticsearch.Net.Utf8Json.Formatters;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	internal sealed class BuiltinResolver : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new BuiltinResolver();

        BuiltinResolver()
        {

        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                // Reduce IL2CPP code generate size(don't write long code in <T>)
                formatter = (IJsonFormatter<T>)BuiltinResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }

        // used from PrimitiveObjectFormatter
        internal static class BuiltinResolverGetFormatterHelper
        {
            static readonly Dictionary<Type, object> formatterMap = new Dictionary<Type, object>()
            {
                // Primitive
                {typeof(Int16), Int16Formatter.Default},
                {typeof(Int32), Int32Formatter.Default},
                {typeof(Int64), Int64Formatter.Default},
                {typeof(UInt16), UInt16Formatter.Default},
                {typeof(UInt32), UInt32Formatter.Default},
                {typeof(UInt64), UInt64Formatter.Default},
                {typeof(float), SingleFormatter.Default},
                {typeof(double), DoubleFormatter.Default},
                {typeof(bool), BooleanFormatter.Default},
                {typeof(byte), ByteFormatter.Default},
                {typeof(sbyte), SByteFormatter.Default},

                // Nulllable Primitive
                {typeof(Nullable<Int16>), NullableInt16Formatter.Default},
                {typeof(Nullable<Int32>), NullableInt32Formatter.Default},
                {typeof(Nullable<Int64>), NullableInt64Formatter.Default},
                {typeof(Nullable<UInt16>), NullableUInt16Formatter.Default},
                {typeof(Nullable<UInt32>), NullableUInt32Formatter.Default},
                {typeof(Nullable<UInt64>), NullableUInt64Formatter.Default},
                {typeof(Nullable<float>), NullableSingleFormatter.Default},
                {typeof(Nullable<double>), NullableDoubleFormatter.Default},
                {typeof(Nullable<bool>), NullableBooleanFormatter.Default},
                {typeof(Nullable<byte>), NullableByteFormatter.Default},
                {typeof(Nullable<sbyte>), NullableSByteFormatter.Default},

                // StandardClassLibraryFormatter

                // DateTime
                {typeof(DateTime), ISO8601DateTimeFormatter.Default}, // ISO8601
                {typeof(TimeSpan), ISO8601TimeSpanFormatter.Default},
                {typeof(DateTimeOffset), ISO8601DateTimeOffsetFormatter.Default},
                {typeof(DateTime?), new StaticNullableFormatter<DateTime>(ISO8601DateTimeFormatter.Default)}, // ISO8601
                {typeof(TimeSpan?), new StaticNullableFormatter<TimeSpan>(ISO8601TimeSpanFormatter.Default)},
                {typeof(DateTimeOffset?),new StaticNullableFormatter<DateTimeOffset>(ISO8601DateTimeOffsetFormatter.Default)},

                {typeof(string), NullableStringFormatter.Default},
                {typeof(char), CharFormatter.Default},
                {typeof(char?), NullableCharFormatter.Default},
                {typeof(decimal), DecimalFormatter.Default},
                {typeof(decimal?), new StaticNullableFormatter<decimal>(DecimalFormatter.Default)},
                {typeof(Guid), GuidFormatter.Default},
                {typeof(Guid?), new StaticNullableFormatter<Guid>(GuidFormatter.Default)},
                {typeof(Uri), UriFormatter.Default},
                {typeof(Version), VersionFormatter.Default},
                {typeof(StringBuilder), StringBuilderFormatter.Default},
                {typeof(BitArray), BitArrayFormatter.Default},
                {typeof(Type), TypeFormatter.Default},

                // special primitive
                {typeof(byte[]), ByteArrayFormatter.Default},

                // optimized primitive array formatter
                {typeof(Int16[]), Int16ArrayFormatter.Default},
                {typeof(Int32[]), Int32ArrayFormatter.Default},
                {typeof(Int64[]), Int64ArrayFormatter.Default},
                {typeof(UInt16[]), UInt16ArrayFormatter.Default},
                {typeof(UInt32[]), UInt32ArrayFormatter.Default},
                {typeof(UInt64[]), UInt64ArrayFormatter.Default},
                {typeof(float[]), SingleArrayFormatter.Default},
                {typeof(double[]), DoubleArrayFormatter.Default},
                {typeof(bool[]), BooleanArrayFormatter.Default},
                {typeof(sbyte[]), SByteArrayFormatter.Default},

                {typeof(char[]), CharArrayFormatter.Default},
                {typeof(string[]), NullableStringArrayFormatter.Default},

                // well known collections
                {typeof(List<Int16>), new ListFormatter<Int16>()},
                {typeof(List<Int32>), new ListFormatter<Int32>()},
                {typeof(List<Int64>), new ListFormatter<Int64>()},
                {typeof(List<UInt16>), new ListFormatter<UInt16>()},
                {typeof(List<UInt32>), new ListFormatter<UInt32>()},
                {typeof(List<UInt64>), new ListFormatter<UInt64>()},
                {typeof(List<float>), new ListFormatter<float>()},
                {typeof(List<double>), new ListFormatter<double>()},
                {typeof(List<bool>), new ListFormatter<bool>()},
                {typeof(List<byte>), new ListFormatter<byte>()},
                {typeof(List<sbyte>), new ListFormatter<sbyte>()},
                {typeof(List<DateTime>), new ListFormatter<DateTime>()},
                {typeof(List<char>), new ListFormatter<char>()},
                {typeof(List<string>), new ListFormatter<string>()},

                { typeof(ArraySegment<byte>), ByteArraySegmentFormatter.Default },
                { typeof(ArraySegment<byte>?),new StaticNullableFormatter<ArraySegment<byte>>(ByteArraySegmentFormatter.Default) },

                {typeof(System.Numerics.BigInteger), BigIntegerFormatter.Default},
                {typeof(System.Numerics.BigInteger?), new StaticNullableFormatter<System.Numerics.BigInteger>(BigIntegerFormatter.Default)},
                {typeof(System.Numerics.Complex), ComplexFormatter.Default},
                {typeof(System.Numerics.Complex?), new StaticNullableFormatter<System.Numerics.Complex>(ComplexFormatter.Default)},
                {typeof(System.Dynamic.ExpandoObject), ExpandoObjectFormatter.Default },
                {typeof(System.Threading.Tasks.Task), TaskUnitFormatter.Default},
            };

            internal static object GetFormatter(Type t)
            {
                object formatter;
                if (formatterMap.TryGetValue(t, out formatter))
                {
                    return formatter;
                }

                return null;
            }
        }
    }
}
