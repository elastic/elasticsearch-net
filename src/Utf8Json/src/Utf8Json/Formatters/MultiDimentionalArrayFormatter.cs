using Utf8Json.Internal;

namespace Utf8Json.Formatters
{
    // multi dimentional array serialize to [[seq], [seq]]

    public sealed class TwoDimentionalArrayFormatter<T> : IJsonFormatter<T[,]>
    {
        public void Serialize(ref JsonWriter writer, T[,] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<T>();

                var iLength = value.GetLength(0);
                var jLength = value.GetLength(1);

                writer.WriteBeginArray();
                for (int i = 0; i < iLength; i++)
                {
                    if (i != 0) writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    for (int j = 0; j < jLength; j++)
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
                {
                    innerArray.Add(formatter.Deserialize(ref reader, formatterResolver));
                }

                guessInnerLength = innerArray.Size;
                buffer.Add(innerArray);
            }

            var t = new T[buffer.Size, guessInnerLength];
            for (int i = 0; i < buffer.Size; i++)
            {
                for (int j = 0; j < guessInnerLength; j++)
                {
                    t[i, j] = buffer.Buffer[i].Buffer[j];
                }
            }

            return t;
        }
    }

    public sealed class ThreeDimentionalArrayFormatter<T> : IJsonFormatter<T[,,]>
    {
        public void Serialize(ref JsonWriter writer, T[,,] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<T>();

                var iLength = value.GetLength(0);
                var jLength = value.GetLength(1);
                var kLength = value.GetLength(2);

                writer.WriteBeginArray();
                for (int i = 0; i < iLength; i++)
                {
                    if (i != 0) writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    for (int j = 0; j < jLength; j++)
                    {
                        if (j != 0) writer.WriteValueSeparator();
                        writer.WriteBeginArray();
                        for (int k = 0; k < kLength; k++)
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
                    {
                        innerArray2.Add(formatter.Deserialize(ref reader, formatterResolver));
                    }

                    guessInnerLength2 = innerArray2.Size;
                    innerArray.Add(innerArray2);
                }

                guessInnerLength = innerArray.Size;
                buffer.Add(innerArray);
            }

            var t = new T[buffer.Size, guessInnerLength, guessInnerLength2];
            for (int i = 0; i < buffer.Size; i++)
            {
                for (int j = 0; j < guessInnerLength; j++)
                {
                    for (int k = 0; k < guessInnerLength2; k++)
                    {
                        t[i, j, k] = buffer.Buffer[i].Buffer[j].Buffer[k];
                    }
                }
            }

            return t;
        }
    }

    public sealed class FourDimentionalArrayFormatter<T> : IJsonFormatter<T[,,,]>
    {
        public void Serialize(ref JsonWriter writer, T[,,,] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<T>();

                var iLength = value.GetLength(0);
                var jLength = value.GetLength(1);
                var kLength = value.GetLength(2);
                var lLength = value.GetLength(3);

                writer.WriteBeginArray();
                for (int i = 0; i < iLength; i++)
                {
                    if (i != 0) writer.WriteValueSeparator();
                    writer.WriteBeginArray();
                    for (int j = 0; j < jLength; j++)
                    {
                        if (j != 0) writer.WriteValueSeparator();
                        writer.WriteBeginArray();
                        for (int k = 0; k < kLength; k++)
                        {
                            if (k != 0) writer.WriteValueSeparator();
                            writer.WriteBeginArray();
                            for (int l = 0; l < lLength; l++)
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
                        {
                            innerArray3.Add(formatter.Deserialize(ref reader, formatterResolver));
                        }
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
            for (int i = 0; i < buffer.Size; i++)
            {
                for (int j = 0; j < guessInnerLength; j++)
                {
                    for (int k = 0; k < guessInnerLength2; k++)
                    {
                        for (int l = 0; l < guessInnerLength3; l++)
                        {
                            t[i, j, k, l] = buffer.Buffer[i].Buffer[j].Buffer[k].Buffer[l];
                        }
                    }
                }
            }

            return t;
        }
    }
}