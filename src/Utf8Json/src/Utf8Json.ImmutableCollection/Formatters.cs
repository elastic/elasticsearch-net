using Utf8Json.Formatters;
using System.Collections.Immutable;
using System;
using Utf8Json.Internal;

namespace Utf8Json.ImmutableCollection
{
    // Immutablearray<T>.Enumerator is 'not' IEnumerator<T>, can't use abstraction layer.
    public class ImmutableArrayFormatter<T> : IJsonFormatter<ImmutableArray<T>>
    {
        public void Serialize(ref JsonWriter writer, ImmutableArray<T> value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBeginArray();
            var formatter = formatterResolver.GetFormatter<T>();
            if (value.Length != 0)
            {
                formatter.Serialize(ref writer, value[0], formatterResolver);
            }
            for (int i = 1; i < value.Length; i++)
            {
                writer.WriteValueSeparator();
                formatter.Serialize(ref writer, value[i], formatterResolver);
            }
            writer.WriteEndArray();
        }

        public ImmutableArray<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return ImmutableArray<T>.Empty;

            var count = 0;
            var formatter = formatterResolver.GetFormatter<T>();

            var builder = ImmutableArray.CreateBuilder<T>();
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                builder.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return builder.ToImmutable();
        }
    }

    public class ImmutableListFormatter<T> : CollectionFormatterBase<T, ImmutableList<T>.Builder, ImmutableList<T>.Enumerator, ImmutableList<T>>
    {
        protected override void Add(ref ImmutableList<T>.Builder collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ImmutableList<T> Complete(ref ImmutableList<T>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableList<T>.Builder Create()
        {
            return ImmutableList.CreateBuilder<T>();
        }

        protected override ImmutableList<T>.Enumerator GetSourceEnumerator(ImmutableList<T> source)
        {
            return source.GetEnumerator();
        }
    }

    public class ImmutableDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, ImmutableDictionary<TKey, TValue>.Builder, ImmutableDictionary<TKey, TValue>.Enumerator, ImmutableDictionary<TKey, TValue>>
    {
        protected override void Add(ref ImmutableDictionary<TKey, TValue>.Builder collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override ImmutableDictionary<TKey, TValue> Complete(ref ImmutableDictionary<TKey, TValue>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableDictionary<TKey, TValue>.Builder Create()
        {
            return ImmutableDictionary.CreateBuilder<TKey, TValue>();
        }

        protected override ImmutableDictionary<TKey, TValue>.Enumerator GetSourceEnumerator(ImmutableDictionary<TKey, TValue> source)
        {
            return source.GetEnumerator();
        }
    }

    public class ImmutableHashSetFormatter<T> : CollectionFormatterBase<T, ImmutableHashSet<T>.Builder, ImmutableHashSet<T>.Enumerator, ImmutableHashSet<T>>
    {
        protected override void Add(ref ImmutableHashSet<T>.Builder collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ImmutableHashSet<T> Complete(ref ImmutableHashSet<T>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableHashSet<T>.Builder Create()
        {
            return ImmutableHashSet.CreateBuilder<T>();
        }

        protected override ImmutableHashSet<T>.Enumerator GetSourceEnumerator(ImmutableHashSet<T> source)
        {
            return source.GetEnumerator();
        }
    }

    public class ImmutableSortedDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, ImmutableSortedDictionary<TKey, TValue>.Builder, ImmutableSortedDictionary<TKey, TValue>.Enumerator, ImmutableSortedDictionary<TKey, TValue>>
    {
        protected override void Add(ref ImmutableSortedDictionary<TKey, TValue>.Builder collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override ImmutableSortedDictionary<TKey, TValue> Complete(ref ImmutableSortedDictionary<TKey, TValue>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableSortedDictionary<TKey, TValue>.Builder Create()
        {
            return ImmutableSortedDictionary.CreateBuilder<TKey, TValue>();
        }

        protected override ImmutableSortedDictionary<TKey, TValue>.Enumerator GetSourceEnumerator(ImmutableSortedDictionary<TKey, TValue> source)
        {
            return source.GetEnumerator();
        }
    }

    public class ImmutableSortedSetFormatter<T> : CollectionFormatterBase<T, ImmutableSortedSet<T>.Builder, ImmutableSortedSet<T>.Enumerator, ImmutableSortedSet<T>>
    {
        protected override void Add(ref ImmutableSortedSet<T>.Builder collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ImmutableSortedSet<T> Complete(ref ImmutableSortedSet<T>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableSortedSet<T>.Builder Create()
        {
            return ImmutableSortedSet.CreateBuilder<T>();
        }

        protected override ImmutableSortedSet<T>.Enumerator GetSourceEnumerator(ImmutableSortedSet<T> source)
        {
            return source.GetEnumerator();
        }
    }

    // not best for performance(does not use ImmutableQueue<T>.Enumerator)
    public class ImmutableQueueFormatter<T> : CollectionFormatterBase<T, ImmutableQueueBuilder<T>, ImmutableQueue<T>>
    {
        protected override void Add(ref ImmutableQueueBuilder<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ImmutableQueue<T> Complete(ref ImmutableQueueBuilder<T> intermediateCollection)
        {
            return intermediateCollection.q;
        }

        protected override ImmutableQueueBuilder<T> Create()
        {
            return new ImmutableQueueBuilder<T>(ImmutableQueue<T>.Empty);
        }
    }

    // not best for performance(does not use ImmutableStack<T>.Enumerator)
    public class ImmutableStackFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, ImmutableStack<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ImmutableStack<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            var bufArray = intermediateCollection.Buffer;
            var stack = ImmutableStack<T>.Empty;
            for (int i = intermediateCollection.Size - 1; i >= 0; i--)
            {
                stack = stack.Push(bufArray[i]);
            }
            return stack;

        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }
    }

    public class InterfaceImmutableListFormatter<T> : CollectionFormatterBase<T, ImmutableList<T>.Builder, IImmutableList<T>>
    {
        protected override void Add(ref ImmutableList<T>.Builder collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override IImmutableList<T> Complete(ref ImmutableList<T>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableList<T>.Builder Create()
        {
            return ImmutableList.CreateBuilder<T>();
        }
    }

    public class InterfaceImmutableDictionaryFormatter<TKey, TValue> : DictionaryFormatterBase<TKey, TValue, ImmutableDictionary<TKey, TValue>.Builder, IImmutableDictionary<TKey, TValue>>
    {
        protected override void Add(ref ImmutableDictionary<TKey, TValue>.Builder collection, int index, TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        protected override IImmutableDictionary<TKey, TValue> Complete(ref ImmutableDictionary<TKey, TValue>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableDictionary<TKey, TValue>.Builder Create()
        {
            return ImmutableDictionary.CreateBuilder<TKey, TValue>();
        }
    }

    public class InterfaceImmutableSetFormatter<T> : CollectionFormatterBase<T, ImmutableHashSet<T>.Builder, IImmutableSet<T>>
    {
        protected override void Add(ref ImmutableHashSet<T>.Builder collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override IImmutableSet<T> Complete(ref ImmutableHashSet<T>.Builder intermediateCollection)
        {
            return intermediateCollection.ToImmutable();
        }

        protected override ImmutableHashSet<T>.Builder Create()
        {
            return ImmutableHashSet.CreateBuilder<T>();
        }
    }

    public class InterfaceImmutableQueueFormatter<T> : CollectionFormatterBase<T, ImmutableQueueBuilder<T>, IImmutableQueue<T>>
    {
        protected override void Add(ref ImmutableQueueBuilder<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override IImmutableQueue<T> Complete(ref ImmutableQueueBuilder<T> intermediateCollection)
        {
            return intermediateCollection.q;
        }

        protected override ImmutableQueueBuilder<T> Create()
        {
            return new ImmutableQueueBuilder<T>(ImmutableQueue<T>.Empty);
        }
    }

    public class InterfaceImmutableStackFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IImmutableStack<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override IImmutableStack<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            var bufArray = intermediateCollection.Buffer;
            var stack = ImmutableStack<T>.Empty;
            for (int i = intermediateCollection.Size - 1; i >= 0; i--)
            {
                stack = stack.Push(bufArray[i]);
            }
            return stack;
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }
    }


    // pseudo builders

    public struct ImmutableQueueBuilder<T>
    {
        public ImmutableQueue<T> q;

        public ImmutableQueueBuilder(ImmutableQueue<T> initial)
        {
            this.q = initial;
        }

        public void Add(T value)
        {
            q = q.Enqueue(value);
        }
    }
}