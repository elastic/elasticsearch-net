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

using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Formatters
{
    // multi dimensional array serialize to [[seq], [seq]]

	internal sealed class TwoDimensionalArrayFormatter<T> : IJsonFormatter<T[,]>
    {
        public void Serialize(ref JsonWriter writer, T[,] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<T>();

                var iLength = value.GetLength(0);
                var jLength = value.GetLength(1);

                writer.WriteBeginArray();
                for (var i = 0; i < iLength; i++)
                {
                    if (i != 0) writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    for (var j = 0; j < jLength; j++)
                    {
                        if (j != 0) writer.WriteValueSeparator();
                        formatter.Serialize(ref writer, value[i, j], formatterResolver);
                    }
                    writer.WriteEndArray();
                }
                writer.WriteEndArray();
            }
        }

        public T[,] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var buffer = new ArrayBuffer<ArrayBuffer<T>>(4);
            var formatter = formatterResolver.GetFormatterWithVerify<T>();

            var guessInnerLength = 0;
            var outerCount = 0;
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref outerCount))
            {
                var innerArray = new ArrayBuffer<T>(guessInnerLength == 0 ? 4 : guessInnerLength);
                var innerCount = 0;
                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref innerCount))
					innerArray.Add(formatter.Deserialize(ref reader, formatterResolver));

				guessInnerLength = innerArray.Size;
                buffer.Add(innerArray);
            }

            var t = new T[buffer.Size, guessInnerLength];
            for (var i = 0; i < buffer.Size; i++)
			for (var j = 0; j < guessInnerLength; j++)
				t[i, j] = buffer.Buffer[i].Buffer[j];

			return t;
        }
    }

	internal sealed class ThreeDimensionalArrayFormatter<T> : IJsonFormatter<T[,,]>
    {
        public void Serialize(ref JsonWriter writer, T[,,] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<T>();

                var iLength = value.GetLength(0);
                var jLength = value.GetLength(1);
                var kLength = value.GetLength(2);

                writer.WriteBeginArray();
                for (var i = 0; i < iLength; i++)
                {
                    if (i != 0) writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    for (var j = 0; j < jLength; j++)
                    {
                        if (j != 0) writer.WriteValueSeparator();
                        writer.WriteBeginArray();
                        for (var k = 0; k < kLength; k++)
                        {
                            if (k != 0) writer.WriteValueSeparator();
                            formatter.Serialize(ref writer, value[i, j, k], formatterResolver);
                        }
                        writer.WriteEndArray();
                    }
                    writer.WriteEndArray();
                }
                writer.WriteEndArray();
            }
        }

        public T[,,] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var buffer = new ArrayBuffer<ArrayBuffer<ArrayBuffer<T>>>(4);
            var formatter = formatterResolver.GetFormatterWithVerify<T>();

            var guessInnerLength2 = 0;
            var guessInnerLength = 0;
            var outerCount = 0;
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref outerCount))
            {
                var innerArray = new ArrayBuffer<ArrayBuffer<T>>(guessInnerLength == 0 ? 4 : guessInnerLength);
                var innerCount = 0;
                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref innerCount))
                {
                    var innerArray2 = new ArrayBuffer<T>(guessInnerLength2 == 0 ? 4 : guessInnerLength2);
                    var innerCount2 = 0;
                    reader.ReadIsBeginArrayWithVerify();
                    while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref innerCount2))
						innerArray2.Add(formatter.Deserialize(ref reader, formatterResolver));

					guessInnerLength2 = innerArray2.Size;
                    innerArray.Add(innerArray2);
                }

                guessInnerLength = innerArray.Size;
                buffer.Add(innerArray);
            }

            var t = new T[buffer.Size, guessInnerLength, guessInnerLength2];
            for (var i = 0; i < buffer.Size; i++)
			for (var j = 0; j < guessInnerLength; j++)
			for (var k = 0; k < guessInnerLength2; k++)
				t[i, j, k] = buffer.Buffer[i].Buffer[j].Buffer[k];

			return t;
        }
    }

	internal sealed class FourDimensionalArrayFormatter<T> : IJsonFormatter<T[,,,]>
    {
        public void Serialize(ref JsonWriter writer, T[,,,] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
				writer.WriteNull();
			else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<T>();

                var iLength = value.GetLength(0);
                var jLength = value.GetLength(1);
                var kLength = value.GetLength(2);
                var lLength = value.GetLength(3);

                writer.WriteBeginArray();
                for (var i = 0; i < iLength; i++)
                {
                    if (i != 0) writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    for (var j = 0; j < jLength; j++)
                    {
                        if (j != 0) writer.WriteValueSeparator();
                        writer.WriteBeginArray();
                        for (var k = 0; k < kLength; k++)
                        {
                            if (k != 0) writer.WriteValueSeparator();
                            writer.WriteBeginArray();
                            for (var l = 0; l < lLength; l++)
                            {
                                if (l != 0) writer.WriteValueSeparator();
                                formatter.Serialize(ref writer, value[i, j, k, l], formatterResolver);
                            }
                            writer.WriteEndArray();
                        }
                        writer.WriteEndArray();
                    }
                    writer.WriteEndArray();
                }
                writer.WriteEndArray();
            }
        }

        public T[,,,] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var buffer = new ArrayBuffer<ArrayBuffer<ArrayBuffer<ArrayBuffer<T>>>>(4);
            var formatter = formatterResolver.GetFormatterWithVerify<T>();

            var guessInnerLength3 = 0;
            var guessInnerLength2 = 0;
            var guessInnerLength = 0;
            var outerCount = 0;
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref outerCount))
            {
                var innerArray = new ArrayBuffer<ArrayBuffer<ArrayBuffer<T>>>(guessInnerLength == 0 ? 4 : guessInnerLength);
                var innerCount = 0;
                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref innerCount))
                {
                    var innerArray2 = new ArrayBuffer<ArrayBuffer<T>>(guessInnerLength2 == 0 ? 4 : guessInnerLength2);
                    var innerCount2 = 0;
                    reader.ReadIsBeginArrayWithVerify();
                    while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref innerCount2))
                    {
                        var innerArray3 = new ArrayBuffer<T>(guessInnerLength3 == 0 ? 4 : guessInnerLength3);
                        var innerCount3 = 0;
                        reader.ReadIsBeginArrayWithVerify();
                        while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref innerCount3))
							innerArray3.Add(formatter.Deserialize(ref reader, formatterResolver));

						guessInnerLength3 = innerArray3.Size;
                        innerArray2.Add(innerArray3);
                    }

                    guessInnerLength2 = innerArray2.Size;
                    innerArray.Add(innerArray2);
                }

                guessInnerLength = innerArray.Size;
                buffer.Add(innerArray);
            }

            var t = new T[buffer.Size, guessInnerLength, guessInnerLength2, guessInnerLength3];
            for (var i = 0; i < buffer.Size; i++)
			for (var j = 0; j < guessInnerLength; j++)
			for (var k = 0; k < guessInnerLength2; k++)
			for (var l = 0; l < guessInnerLength3; l++)
				t[i, j, k, l] = buffer.Buffer[i].Buffer[j].Buffer[k].Buffer[l];

			return t;
        }
    }
}
