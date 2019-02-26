using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#if NETSTANDARD
using System.Collections.Concurrent;
#endif

namespace Utf8Json.Formatters
{
    // unfortunately, can't use IDictionary<KVP> because supports IReadOnlyDictionary.
    public abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TEnumerator, TDictionary> : IJsonFormatter<TDictionary>
        where TDictionary : class, IEnumerable<KeyValuePair<TKey, TValue>>
        where TEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        public void Serialize(ref JsonWriter writer, TDictionary value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            else
            {
                var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>() as IObjectPropertyNameFormatter<TKey>;
                var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

                writer.WriteBeginObject();

                var e = GetSourceEnumerator(value);
                try
                {
                    if (keyFormatter != null)
                    {
                        if (e.MoveNext())
                        {
                            var item = e.Current;
                            keyFormatter.SerializeToPropertyName(ref writer, item.Key, formatterResolver);
                            writer.WriteNameSeparator();
                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                        }
                        else
                        {
                            goto END;
                        }

                        while (e.MoveNext())
                        {
                            writer.WriteValueSeparator();
                            var item = e.Current;
                            keyFormatter.SerializeToPropertyName(ref writer, item.Key, formatterResolver);
                            writer.WriteNameSeparator();
                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                        }
                    }
                    else
                    {
                        if (e.MoveNext())
                        {
                            var item = e.Current;
                            writer.WriteString(item.Key.ToString());
                            writer.WriteNameSeparator();
                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                        }
                        else
                        {
                            goto END;
                        }

                        while (e.MoveNext())
                        {
                            writer.WriteValueSeparator();
                            var item = e.Current;
                            writer.WriteString(item.Key.ToString());
                            writer.WriteNameSeparator();
                            valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                        }
                    }
                }
                finally
                {
                    e.Dispose();
                }

                END:
                writer.WriteEndObject();
            }
        }

        public TDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                var keyFormatter = formatterResolver.GetFormatterWithVerify<TKey>() as IObjectPropertyNameFormatter<TKey>;
                if (keyFormatter == null) throw new InvalidOperationException(typeof(TKey) + " does not support dictionary key deserialize.");
                var valueFormatter = formatterResolver.GetFormatterWithVerify<TValue>();

                reader.ReadIsBeginObjectWithVerify();

                var dict = Create();
                var i = 0;
                while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
                {
                    var key = keyFormatter.DeserializeFromPropertyName(ref reader, formatterResolver);
                    reader.ReadIsNameSeparatorWithVerify();
                    var value = valueFormatter.Deserialize(ref reader, formatterResolver);
                    Add(ref dict, i - 1, key, value);
                }

                return Complete(ref dict);
            }
        }

        // abstraction for serialize

        // Some collections can use struct iterator, this is optimization path
        protected abstract TEnumerator GetSourceEnumerator(TDictionary source);

        // abstraction for deserialize
        protected abstract TIntermediate Create();
        protected abstract void Add(ref TIntermediate collection, int index, TKey key, TValue value);
        protected abstract TDictionary Complete(ref TIntermediate intermediateCollection);
    }

    public abstract class DictionaryFormatterBase<TKey, TValue, TIntermediate, TDictionary> : DictionaryFormatterBase<TKey, TValue, TIntermediate, IEnumerator<KeyValuePair<TKey, TValue>>, TDictionary>
        where TDictionary : class, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        protected override IEnumerator<KeyValuePair<TKey, TValue>> GetSourceEnumerator(TDictionary source)
        {
            return source.GetEnumerator();
        }
    }

    public abstract class DictionaryFormatterBase<TKey, TValue, TDictionary> : DictionaryFormatterBase<TKey, TValue, TDictionary, TDictionary>
        where TDictionary : class, IDictionary<TKey, TValue>
    {
        protected override TDictionary Complete(ref TDictionary intermediateCollection)
        {
            return intermediateCollection;
        }
    }


    public sealed class DictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, Dictionary<TKey, TValue>.Enumerator, Dictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override Dictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override Dictionary<TKey, TValue> Create()
        {
            return new Dictionary<TKey, TValue>();
        }

        protected override Dictionary<TKey, TValue>.Enumerator GetSourceEnumerator(Dictionary<TKey, TValue> source)
        {
            return source.GetEnumerator();
        }
    }

    public sealed class GenericDictionaryFormatter<TKey, TValue, TDictionary> : DictionaryFormatterBase<TKey, TValue, TDictionary>
        where TDictionary : class, IDictionary<TKey, TValue>, new()
    {
        protected override void Add(ref TDictionary collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override TDictionary Create()
        {
            return new TDictionary();
        }
    }

    public sealed class InterfaceDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, IDictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override Dictionary<TKey, TValue> Create()
        {
            return new Dictionary<TKey, TValue>();
        }

        protected override IDictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }
    }

    public sealed class SortedListFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, SortedList<TKey, TValue>>
    {
        protected override void Add(ref SortedList<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override SortedList<TKey, TValue> Create()
        {
            return new SortedList<TKey, TValue>();
        }
    }

    public sealed class SortedDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, SortedDictionary<TKey, TValue>, SortedDictionary<TKey, TValue>.Enumerator, SortedDictionary<TKey, TValue>>
    {
        protected override void Add(ref SortedDictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override SortedDictionary<TKey, TValue> Complete(ref SortedDictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override SortedDictionary<TKey, TValue> Create()
        {
            return new SortedDictionary<TKey, TValue>();
        }

        protected override SortedDictionary<TKey, TValue>.Enumerator GetSourceEnumerator(SortedDictionary<TKey, TValue> source)
        {
            return source.GetEnumerator();
        }
    }

#if NETSTANDARD

    public sealed class ReadOnlyDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, ReadOnlyDictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override ReadOnlyDictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection)
        {
            return new ReadOnlyDictionary<TKey, TValue>(intermediateCollection);
        }

        protected override Dictionary<TKey, TValue> Create()
        {
            return new Dictionary<TKey, TValue>();
        }
    }

    public sealed class InterfaceReadOnlyDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, Dictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>>
    {
        protected override void Add(ref Dictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override IReadOnlyDictionary<TKey, TValue> Complete(ref Dictionary<TKey, TValue> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override Dictionary<TKey, TValue> Create()
        {
            return new Dictionary<TKey, TValue>();
        }
    }

    public sealed class ConcurrentDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, System.Collections.Concurrent.ConcurrentDictionary<TKey, TValue>>
    {
        protected override void Add(ref ConcurrentDictionary<TKey, TValue> collection, int index, TKey key, TValue value)
        {
            collection.TryAdd(key, value);
        }

        protected override ConcurrentDictionary<TKey, TValue> Create()
        {
            // concurrent dictionary can't access defaultConcurrecyLevel so does not use count overload.
            return new ConcurrentDictionary<TKey, TValue>();
        }
    }

#endif

    public sealed class NonGenericDictionaryFormatter<T> : IJsonFormatter<T>
        where T : class, System.Collections.IDictionary, new()
    {
        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            else
            {
                var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

                writer.WriteBeginObject();

                var e = value.GetEnumerator();
                try
                {
                    if (e.MoveNext())
                    {
                        System.Collections.DictionaryEntry item = (System.Collections.DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                    else
                    {
                        goto END;
                    }

                    while (e.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        System.Collections.DictionaryEntry item = (System.Collections.DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                }
                finally
                {
                    var disp = e as IDisposable;
                    if (disp != null)
                    {
                        disp.Dispose();
                    }
                }

                END:
                writer.WriteEndObject();
            }
        }

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return default(T);
            }
            else
            {
                var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

                reader.ReadIsBeginObjectWithVerify();

                var dict = new T();
                var i = 0;
                while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
                {
                    var key = reader.ReadPropertyName();
                    var value = valueFormatter.Deserialize(ref reader, formatterResolver);
                    dict.Add(key, value);
                }

                return dict;
            }
        }
    }

    public sealed class NonGenericInterfaceDictionaryFormatter : IJsonFormatter<System.Collections.IDictionary>
    {
        public static readonly IJsonFormatter<System.Collections.IDictionary> Default = new NonGenericInterfaceDictionaryFormatter();


        public void Serialize(ref JsonWriter writer, System.Collections.IDictionary value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            else
            {
                var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

                writer.WriteBeginObject();

                var e = value.GetEnumerator();
                try
                {
                    if (e.MoveNext())
                    {
                        System.Collections.DictionaryEntry item = (System.Collections.DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                    else
                    {
                        goto END;
                    }

                    while (e.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        System.Collections.DictionaryEntry item = (System.Collections.DictionaryEntry)e.Current;
                        writer.WritePropertyName(item.Key.ToString());
                        valueFormatter.Serialize(ref writer, item.Value, formatterResolver);
                    }
                }
                finally
                {
                    var disp = e as IDisposable;
                    if (disp != null)
                    {
                        disp.Dispose();
                    }
                }

                END:
                writer.WriteEndObject();
            }
        }

        public System.Collections.IDictionary Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                var valueFormatter = formatterResolver.GetFormatterWithVerify<object>();

                reader.ReadIsBeginObjectWithVerify();

                var dict = new Dictionary<object, object>();
                var i = 0;
                while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref i))
                {
                    var key = reader.ReadPropertyName();
                    var value = valueFormatter.Deserialize(ref reader, formatterResolver);
                    dict.Add(key, value);
                }

                return dict;
            }
        }
    }
}
