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
using System.Dynamic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Utf8Json.Formatters;

namespace Elasticsearch.Net.Utf8Json.Resolvers
{
	internal sealed class BuiltinResolver : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new BuiltinResolver();

		private BuiltinResolver()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>() => FormatterCache<T>.formatter;

		private static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache() =>
				// Reduce IL2CPP code generate size(don't write long code in <T>)
				formatter = (IJsonFormatter<T>)BuiltinResolverGetFormatterHelper.GetFormatter(typeof(T));
		}

        // used from PrimitiveObjectFormatter
        internal static class BuiltinResolverGetFormatterHelper
        {
			private static readonly Dictionary<Type, object> FormatterMap = new Dictionary<Type, object>()
            {
                // Primitive
                {typeof(short), Int16Formatter.Default},
                {typeof(int), Int32Formatter.Default},
                {typeof(long), Int64Formatter.Default},
                {typeof(ushort), UInt16Formatter.Default},
                {typeof(uint), UInt32Formatter.Default},
                {typeof(ulong), UInt64Formatter.Default},
                {typeof(float), SingleFormatter.Default},
                {typeof(double), DoubleFormatter.Default},
                {typeof(bool), BooleanFormatter.Default},
                {typeof(byte), ByteFormatter.Default},
                {typeof(sbyte), SByteFormatter.Default},

                // Nullable Primitive
                {typeof(short?), NullableInt16Formatter.Default},
                {typeof(int?), NullableInt32Formatter.Default},
                {typeof(long?), NullableInt64Formatter.Default},
                {typeof(ushort?), NullableUInt16Formatter.Default},
                {typeof(uint?), NullableUInt32Formatter.Default},
                {typeof(ulong?), NullableUInt64Formatter.Default},
                {typeof(float?), NullableSingleFormatter.Default},
                {typeof(double?), NullableDoubleFormatter.Default},
                {typeof(bool?), NullableBooleanFormatter.Default},
                {typeof(byte?), NullableByteFormatter.Default},
                {typeof(sbyte?), NullableSByteFormatter.Default},

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
                {typeof(short[]), Int16ArrayFormatter.Default},
                {typeof(int[]), Int32ArrayFormatter.Default},
                {typeof(long[]), Int64ArrayFormatter.Default},
                {typeof(ushort[]), UInt16ArrayFormatter.Default},
                {typeof(uint[]), UInt32ArrayFormatter.Default},
                {typeof(ulong[]), UInt64ArrayFormatter.Default},
                {typeof(float[]), SingleArrayFormatter.Default},
                {typeof(double[]), DoubleArrayFormatter.Default},
                {typeof(bool[]), BooleanArrayFormatter.Default},
                {typeof(sbyte[]), SByteArrayFormatter.Default},

                {typeof(char[]), CharArrayFormatter.Default},
                {typeof(string[]), NullableStringArrayFormatter.Default},

                // well known collections
                {typeof(List<short>), new ListFormatter<short>()},
                {typeof(List<int>), new ListFormatter<int>()},
                {typeof(List<long>), new ListFormatter<long>()},
                {typeof(List<ushort>), new ListFormatter<ushort>()},
                {typeof(List<uint>), new ListFormatter<uint>()},
                {typeof(List<ulong>), new ListFormatter<ulong>()},
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

                {typeof(BigInteger), BigIntegerFormatter.Default},
                {typeof(BigInteger?), new StaticNullableFormatter<BigInteger>(BigIntegerFormatter.Default)},
                {typeof(Complex), ComplexFormatter.Default},
                {typeof(Complex?), new StaticNullableFormatter<Complex>(ComplexFormatter.Default)},
                {typeof(ExpandoObject), ExpandoObjectFormatter.Default },
                {typeof(Task), TaskUnitFormatter.Default},
            };

            internal static object GetFormatter(Type t) => FormatterMap.TryGetValue(t, out var formatter) ? formatter : null;
		}
    }
}
