#if NETSTANDARD

using System;
using Utf8Json.Internal;
using Utf8Json.Formatters.Internal;

namespace Utf8Json.Formatters.Internal
{
    // reduce static constructor generate size on generics(especially IL2CPP on Unity)
    internal static class ValueTupleFormatterHelper
    {
        internal static readonly byte[][] nameCache1;
        internal static readonly AutomataDictionary dictionary1;
        internal static readonly byte[][] nameCache2;
        internal static readonly AutomataDictionary dictionary2;
        internal static readonly byte[][] nameCache3;
        internal static readonly AutomataDictionary dictionary3;
        internal static readonly byte[][] nameCache4;
        internal static readonly AutomataDictionary dictionary4;
        internal static readonly byte[][] nameCache5;
        internal static readonly AutomataDictionary dictionary5;
        internal static readonly byte[][] nameCache6;
        internal static readonly AutomataDictionary dictionary6;
        internal static readonly byte[][] nameCache7;
        internal static readonly AutomataDictionary dictionary7;
        internal static readonly byte[][] nameCache8;
        internal static readonly AutomataDictionary dictionary8;

        static ValueTupleFormatterHelper()
        {
            nameCache1 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
            };
            dictionary1 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
            };
            nameCache2 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
            };
            dictionary2 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
            };
            nameCache3 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item3"),
            };
            dictionary3 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item3"), 2 },
            };
            nameCache4 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item4"),
            };
            dictionary4 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item3"), 2 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item4"), 3 },
            };
            nameCache5 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item5"),
            };
            dictionary5 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item3"), 2 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item4"), 3 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item5"), 4 },
            };
            nameCache6 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item5"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item6"),
            };
            dictionary6 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item3"), 2 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item4"), 3 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item5"), 4 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item6"), 5 },
            };
            nameCache7 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item5"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item6"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item7"),
            };
            dictionary7 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item3"), 2 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item4"), 3 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item5"), 4 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item6"), 5 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item7"), 6 },
            };
            nameCache8 = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Item1"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item2"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item3"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item4"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item5"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item6"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Item7"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Rest"),
            };
            dictionary8 = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item1"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item2"), 1 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item3"), 2 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item4"), 3 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item5"), 4 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item6"), 5 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Item7"), 6 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Rest"), 7 },
            };
        }
    }
}

namespace Utf8Json.Formatters
{

    public sealed class ValueTupleFormatter<T1> : IJsonFormatter<ValueTuple<T1>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache1;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary1;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1>(item1);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2> : IJsonFormatter<ValueTuple<T1, T2>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache2;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary2;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2>(item1, item2);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2, T3> : IJsonFormatter<ValueTuple<T1, T2, T3>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache3;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary3;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2, T3> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteRaw(cache[2]);
            formatterResolver.GetFormatterWithVerify<T3>().Serialize(ref writer, value.Item3, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2, T3> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            T3 item3 = default(T3);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        item3 = formatterResolver.GetFormatterWithVerify<T3>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2, T3>(item1, item2, item3);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2, T3, T4> : IJsonFormatter<ValueTuple<T1, T2, T3, T4>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache4;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary4;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2, T3, T4> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteRaw(cache[2]);
            formatterResolver.GetFormatterWithVerify<T3>().Serialize(ref writer, value.Item3, formatterResolver);
            writer.WriteRaw(cache[3]);
            formatterResolver.GetFormatterWithVerify<T4>().Serialize(ref writer, value.Item4, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2, T3, T4> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            T3 item3 = default(T3);
            T4 item4 = default(T4);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        item3 = formatterResolver.GetFormatterWithVerify<T3>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 3:
                        item4 = formatterResolver.GetFormatterWithVerify<T4>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2, T3, T4, T5> : IJsonFormatter<ValueTuple<T1, T2, T3, T4, T5>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache5;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary5;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteRaw(cache[2]);
            formatterResolver.GetFormatterWithVerify<T3>().Serialize(ref writer, value.Item3, formatterResolver);
            writer.WriteRaw(cache[3]);
            formatterResolver.GetFormatterWithVerify<T4>().Serialize(ref writer, value.Item4, formatterResolver);
            writer.WriteRaw(cache[4]);
            formatterResolver.GetFormatterWithVerify<T5>().Serialize(ref writer, value.Item5, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2, T3, T4, T5> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            T3 item3 = default(T3);
            T4 item4 = default(T4);
            T5 item5 = default(T5);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        item3 = formatterResolver.GetFormatterWithVerify<T3>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 3:
                        item4 = formatterResolver.GetFormatterWithVerify<T4>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 4:
                        item5 = formatterResolver.GetFormatterWithVerify<T5>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2, T3, T4, T5, T6> : IJsonFormatter<ValueTuple<T1, T2, T3, T4, T5, T6>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache6;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary6;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteRaw(cache[2]);
            formatterResolver.GetFormatterWithVerify<T3>().Serialize(ref writer, value.Item3, formatterResolver);
            writer.WriteRaw(cache[3]);
            formatterResolver.GetFormatterWithVerify<T4>().Serialize(ref writer, value.Item4, formatterResolver);
            writer.WriteRaw(cache[4]);
            formatterResolver.GetFormatterWithVerify<T5>().Serialize(ref writer, value.Item5, formatterResolver);
            writer.WriteRaw(cache[5]);
            formatterResolver.GetFormatterWithVerify<T6>().Serialize(ref writer, value.Item6, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2, T3, T4, T5, T6> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            T3 item3 = default(T3);
            T4 item4 = default(T4);
            T5 item5 = default(T5);
            T6 item6 = default(T6);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        item3 = formatterResolver.GetFormatterWithVerify<T3>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 3:
                        item4 = formatterResolver.GetFormatterWithVerify<T4>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 4:
                        item5 = formatterResolver.GetFormatterWithVerify<T5>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 5:
                        item6 = formatterResolver.GetFormatterWithVerify<T6>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2, T3, T4, T5, T6, T7> : IJsonFormatter<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache7;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary7;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteRaw(cache[2]);
            formatterResolver.GetFormatterWithVerify<T3>().Serialize(ref writer, value.Item3, formatterResolver);
            writer.WriteRaw(cache[3]);
            formatterResolver.GetFormatterWithVerify<T4>().Serialize(ref writer, value.Item4, formatterResolver);
            writer.WriteRaw(cache[4]);
            formatterResolver.GetFormatterWithVerify<T5>().Serialize(ref writer, value.Item5, formatterResolver);
            writer.WriteRaw(cache[5]);
            formatterResolver.GetFormatterWithVerify<T6>().Serialize(ref writer, value.Item6, formatterResolver);
            writer.WriteRaw(cache[6]);
            formatterResolver.GetFormatterWithVerify<T7>().Serialize(ref writer, value.Item7, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2, T3, T4, T5, T6, T7> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            T3 item3 = default(T3);
            T4 item4 = default(T4);
            T5 item5 = default(T5);
            T6 item6 = default(T6);
            T7 item7 = default(T7);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        item3 = formatterResolver.GetFormatterWithVerify<T3>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 3:
                        item4 = formatterResolver.GetFormatterWithVerify<T4>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 4:
                        item5 = formatterResolver.GetFormatterWithVerify<T5>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 5:
                        item6 = formatterResolver.GetFormatterWithVerify<T6>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 6:
                        item7 = formatterResolver.GetFormatterWithVerify<T7>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
        }
    }


    public sealed class ValueTupleFormatter<T1, T2, T3, T4, T5, T6, T7, TRest> : IJsonFormatter<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>> where TRest : struct
    {
        static readonly byte[][] cache = TupleFormatterHelper.nameCache8;
        static readonly AutomataDictionary dictionary = TupleFormatterHelper.dictionary8;

        public void Serialize(ref JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteRaw(cache[0]);
            formatterResolver.GetFormatterWithVerify<T1>().Serialize(ref writer, value.Item1, formatterResolver);
            writer.WriteRaw(cache[1]);
            formatterResolver.GetFormatterWithVerify<T2>().Serialize(ref writer, value.Item2, formatterResolver);
            writer.WriteRaw(cache[2]);
            formatterResolver.GetFormatterWithVerify<T3>().Serialize(ref writer, value.Item3, formatterResolver);
            writer.WriteRaw(cache[3]);
            formatterResolver.GetFormatterWithVerify<T4>().Serialize(ref writer, value.Item4, formatterResolver);
            writer.WriteRaw(cache[4]);
            formatterResolver.GetFormatterWithVerify<T5>().Serialize(ref writer, value.Item5, formatterResolver);
            writer.WriteRaw(cache[5]);
            formatterResolver.GetFormatterWithVerify<T6>().Serialize(ref writer, value.Item6, formatterResolver);
            writer.WriteRaw(cache[6]);
            formatterResolver.GetFormatterWithVerify<T7>().Serialize(ref writer, value.Item7, formatterResolver);
            writer.WriteRaw(cache[7]);
            formatterResolver.GetFormatterWithVerify<TRest>().Serialize(ref writer, value.Rest, formatterResolver);
            writer.WriteEndObject();
        }

        public ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) throw new InvalidOperationException("Data is Nil, ValueTuple can not be null.");

            T1 item1 = default(T1);
            T2 item2 = default(T2);
            T3 item3 = default(T3);
            T4 item4 = default(T4);
            T5 item5 = default(T5);
            T6 item6 = default(T6);
            T7 item7 = default(T7);
            TRest item8 = default(TRest);
            
            var count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
            {
                var keyString = reader.ReadPropertyNameSegmentRaw();
                int key;
#if NETSTANDARD
                dictionary.TryGetValue(keyString, out key);
#else
                dictionary.TryGetValueSafe(keyString, out key);
#endif

                switch (key)
                {
                    case 0:
                        item1 = formatterResolver.GetFormatterWithVerify<T1>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 1:
                        item2 = formatterResolver.GetFormatterWithVerify<T2>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 2:
                        item3 = formatterResolver.GetFormatterWithVerify<T3>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 3:
                        item4 = formatterResolver.GetFormatterWithVerify<T4>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 4:
                        item5 = formatterResolver.GetFormatterWithVerify<T5>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 5:
                        item6 = formatterResolver.GetFormatterWithVerify<T6>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 6:
                        item7 = formatterResolver.GetFormatterWithVerify<T7>().Deserialize(ref reader, formatterResolver);
                        break;
                    case 7:
                        item8 = formatterResolver.GetFormatterWithVerify<TRest>().Deserialize(ref reader, formatterResolver);
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }
            }
            
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, item8);
        }
    }

}

#endif