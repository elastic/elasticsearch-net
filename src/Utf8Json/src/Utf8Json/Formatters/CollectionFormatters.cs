using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Utf8Json.Formatters.Internal;
using Utf8Json.Internal;

#if NETSTANDARD
using System.Collections.Concurrent;
#endif

namespace Utf8Json.Formatters
{
    public class ArrayFormatter<T> : IJsonFormatter<T[]>
    {
        static readonly ArrayPool<T> arrayPool = new ArrayPool<T>(99);

        public void Serialize(ref JsonWriter writer, T[] value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteBeginArray();
            var formatter = formatterResolver.GetFormatterWithVerify<T>();
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

        public T[] Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<T>();

            var workingArea = arrayPool.Rent();
            try
            {
                var array = workingArea;
                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize<T>(ref array, array.Length * 2);
                    }

                    array[count - 1] = formatter.Deserialize(ref reader, formatterResolver);
                }

                var result = new T[count];
                Array.Copy(array, result, count);
                Array.Clear(workingArea, 0, Math.Min(count, workingArea.Length));
                return result;
            }
            finally
            {
                arrayPool.Return(workingArea);
            }
        }
    }

    public class ArraySegmentFormatter<T> : IJsonFormatter<ArraySegment<T>>
    {
        static readonly ArrayPool<T> arrayPool = new ArrayPool<T>(99);

        public void Serialize(ref JsonWriter writer, ArraySegment<T> value, IJsonFormatterResolver formatterResolver)
        {
            if (value.Array == null) { writer.WriteNull(); return; }

            var array = value.Array;
            var offset = value.Offset;
            var count = value.Count;

            writer.WriteBeginArray();
            var formatter = formatterResolver.GetFormatterWithVerify<T>();
            if (count != 0)
            {
                formatter.Serialize(ref writer, value.Array[offset], formatterResolver);
            }

            for (int i = 1; i < count; i++)
            {
                writer.WriteValueSeparator();
                formatter.Serialize(ref writer, array[offset + i], formatterResolver);
            }
            writer.WriteEndArray();
        }

        public ArraySegment<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return default(ArraySegment<T>);

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<T>();

            var workingArea = arrayPool.Rent();
            try
            {
                var array = workingArea;
                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    if (array.Length < count)
                    {
                        Array.Resize<T>(ref array, array.Length * 2);
                    }

                    array[count - 1] = formatter.Deserialize(ref reader, formatterResolver);
                }

                var result = new T[count];
                Array.Copy(array, result, count);
                Array.Clear(workingArea, 0, Math.Min(count, workingArea.Length));
                return new ArraySegment<T>(result, 0, result.Length);
            }
            finally
            {
                arrayPool.Return(workingArea);
            }
        }
    }

    public class ListFormatter<T> : IJsonFormatter<List<T>>
    {
        public void Serialize(ref JsonWriter writer, List<T> value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null) { writer.WriteNull(); return; }

            writer.WriteBeginArray();
            var formatter = formatterResolver.GetFormatterWithVerify<T>();
            if (value.Count != 0)
            {
                formatter.Serialize(ref writer, value[0], formatterResolver);
            }
            for (int i = 1; i < value.Count; i++)
            {
                writer.WriteValueSeparator();
                formatter.Serialize(ref writer, value[i], formatterResolver);
            }
            writer.WriteEndArray();
        }

        public List<T> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<T>();

            var list = new List<T>();
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }
    }

    public abstract class CollectionFormatterBase<TElement, TIntermediate, TEnumerator, TCollection> : IJsonFormatter<TCollection>
        where TCollection : class, IEnumerable<TElement>
        where TEnumerator : IEnumerator<TElement>
    {
        public void Serialize(ref JsonWriter writer, TCollection value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteBeginArray();
                var formatter = formatterResolver.GetFormatterWithVerify<TElement>();

                // Unity's foreach struct enumerator causes boxing so iterate manually.
                var e = GetSourceEnumerator(value);
                try
                {
                    var isFirst = true;
                    while (e.MoveNext())
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            writer.WriteValueSeparator();
                        }
                        formatter.Serialize(ref writer, e.Current, formatterResolver);
                    }
                }
                finally
                {
                    e.Dispose();
                }

                writer.WriteEndArray();
            }
        }

        public TCollection Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                var formatter = formatterResolver.GetFormatterWithVerify<TElement>();
                var builder = Create();

                var count = 0;
                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    Add(ref builder, count - 1, formatter.Deserialize(ref reader, formatterResolver));
                }

                return Complete(ref builder);
            }
        }

        // Some collections can use struct iterator, this is optimization path
        protected abstract TEnumerator GetSourceEnumerator(TCollection source);

        // abstraction for deserialize
        protected abstract TIntermediate Create();
        protected abstract void Add(ref TIntermediate collection, int index, TElement value);
        protected abstract TCollection Complete(ref TIntermediate intermediateCollection);
    }

    public abstract class CollectionFormatterBase<TElement, TIntermediate, TCollection> : CollectionFormatterBase<TElement, TIntermediate, IEnumerator<TElement>, TCollection>
        where TCollection : class, IEnumerable<TElement>
    {
        protected override IEnumerator<TElement> GetSourceEnumerator(TCollection source)
        {
            return source.GetEnumerator();
        }
    }

    public abstract class CollectionFormatterBase<TElement, TCollection> : CollectionFormatterBase<TElement, TCollection, TCollection>
        where TCollection : class, IEnumerable<TElement>
    {
        protected sealed override TCollection Complete(ref TCollection intermediateCollection)
        {
            return intermediateCollection;
        }
    }

    public sealed class GenericCollectionFormatter<TElement, TCollection> : CollectionFormatterBase<TElement, TCollection>
         where TCollection : class, ICollection<TElement>, new()
    {
        protected override TCollection Create()
        {
            return new TCollection();
        }

        protected override void Add(ref TCollection collection, int index, TElement value)
        {
            collection.Add(value);
        }
    }

    public sealed class LinkedListFormatter<T> : CollectionFormatterBase<T, LinkedList<T>, LinkedList<T>.Enumerator, LinkedList<T>>
    {
        protected override void Add(ref LinkedList<T> collection, int index, T value)
        {
            collection.AddLast(value);
        }

        protected override LinkedList<T> Complete(ref LinkedList<T> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override LinkedList<T> Create()
        {
            return new LinkedList<T>();
        }

        protected override LinkedList<T>.Enumerator GetSourceEnumerator(LinkedList<T> source)
        {
            return source.GetEnumerator();
        }
    }

    public sealed class QeueueFormatter<T> : CollectionFormatterBase<T, Queue<T>, Queue<T>.Enumerator, Queue<T>>
    {
        protected override void Add(ref Queue<T> collection, int index, T value)
        {
            collection.Enqueue(value);
        }

        protected override Queue<T> Create()
        {
            return new Queue<T>();
        }

        protected override Queue<T>.Enumerator GetSourceEnumerator(Queue<T> source)
        {
            return source.GetEnumerator();
        }

        protected override Queue<T> Complete(ref Queue<T> intermediateCollection)
        {
            return intermediateCollection;
        }
    }


    // should deserialize reverse order.
    public sealed class StackFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, Stack<T>.Enumerator, Stack<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }

        protected override Stack<T>.Enumerator GetSourceEnumerator(Stack<T> source)
        {
            return source.GetEnumerator();
        }

        protected override Stack<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            var bufArray = intermediateCollection.Buffer;
            var stack = new Stack<T>(intermediateCollection.Size);
            for (int i = intermediateCollection.Size - 1; i >= 0; i--)
            {
                stack.Push(bufArray[i]);
            }
            return stack;
        }
    }

    public sealed class HashSetFormatter<T> : CollectionFormatterBase<T, HashSet<T>, HashSet<T>.Enumerator, HashSet<T>>
    {
        protected override void Add(ref HashSet<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override HashSet<T> Complete(ref HashSet<T> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override HashSet<T> Create()
        {
            return new HashSet<T>();
        }

        protected override HashSet<T>.Enumerator GetSourceEnumerator(HashSet<T> source)
        {
            return source.GetEnumerator();
        }
    }

    public sealed class ReadOnlyCollectionFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, ReadOnlyCollection<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ReadOnlyCollection<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            return new ReadOnlyCollection<T>(intermediateCollection.ToArray());
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }
    }

    public sealed class InterfaceListFormatter<T> : CollectionFormatterBase<T, List<T>, IList<T>>
    {
        protected override void Add(ref List<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override List<T> Create()
        {
            return new List<T>();
        }

        protected override IList<T> Complete(ref List<T> intermediateCollection)
        {
            return intermediateCollection;
        }
    }

    public sealed class InterfaceCollectionFormatter<T> : CollectionFormatterBase<T, List<T>, ICollection<T>>
    {
        protected override void Add(ref List<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override List<T> Create()
        {
            return new List<T>();
        }

        protected override ICollection<T> Complete(ref List<T> intermediateCollection)
        {
            return intermediateCollection;
        }
    }


    public sealed class InterfaceEnumerableFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IEnumerable<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }

        protected override IEnumerable<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            return intermediateCollection.ToArray();
        }
    }

    // {Key:key, Elements:[Array]}  (not compatible with JSON.NET)
    public sealed class InterfaceGroupingFormatter<TKey, TElement> : IJsonFormatter<IGrouping<TKey, TElement>>
    {
        public void Serialize(ref JsonWriter writer, IGrouping<TKey, TElement> value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            else
            {
                writer.WriteRaw(CollectionFormatterHelper.groupingName[0]);
                formatterResolver.GetFormatterWithVerify<TKey>().Serialize(ref writer, value.Key, formatterResolver);
                writer.WriteRaw(CollectionFormatterHelper.groupingName[1]);
                formatterResolver.GetFormatterWithVerify<IEnumerable<TElement>>().Serialize(ref writer, value.AsEnumerable(), formatterResolver);

                writer.WriteEndObject();
            }
        }

        public IGrouping<TKey, TElement> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                TKey resultKey = default(TKey);
                IEnumerable<TElement> resultValue = default(IEnumerable<TElement>);

                reader.ReadIsBeginObjectWithVerify();

                var count = 0;
                while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
                {
                    var keyString = reader.ReadPropertyNameSegmentRaw();
                    int key;
#if NETSTANDARD
                    CollectionFormatterHelper.groupingAutomata.TryGetValue(keyString, out key);
#else
                    CollectionFormatterHelper.groupingAutomata.TryGetValueSafe(keyString, out key);
#endif

                    switch (key)
                    {
                        case 0:
                            resultKey = formatterResolver.GetFormatterWithVerify<TKey>().Deserialize(ref reader, formatterResolver);
                            break;
                        case 1:
                            resultValue = formatterResolver.GetFormatterWithVerify<IEnumerable<TElement>>().Deserialize(ref reader, formatterResolver);
                            break;
                        default:
                            reader.ReadNextBlock();
                            break;
                    }
                }

                return new Grouping<TKey, TElement>(resultKey, resultValue);
            }
        }
    }

    public sealed class InterfaceLookupFormatter<TKey, TElement> : IJsonFormatter<ILookup<TKey, TElement>>
    {
        public void Serialize(ref JsonWriter writer, ILookup<TKey, TElement> value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            else
            {
                formatterResolver.GetFormatterWithVerify<IEnumerable<IGrouping<TKey, TElement>>>().Serialize(ref writer, value.AsEnumerable(), formatterResolver);
            }
        }

        public ILookup<TKey, TElement> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            else
            {
                if (reader.ReadIsNull()) return null;

                var count = 0;

                var formatter = formatterResolver.GetFormatterWithVerify<IGrouping<TKey, TElement>>();
                var intermediateCollection = new Dictionary<TKey, IGrouping<TKey, TElement>>();

                reader.ReadIsBeginArrayWithVerify();
                while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
                {
                    var g = formatter.Deserialize(ref reader, formatterResolver);
                    intermediateCollection.Add(g.Key, g);
                }

                return new Lookup<TKey, TElement>(intermediateCollection);
            }
        }
    }

    class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        readonly TKey key;
        readonly IEnumerable<TElement> elements;

        public Grouping(TKey key, IEnumerable<TElement> elements)
        {
            this.key = key;
            this.elements = elements;
        }

        public TKey Key
        {
            get
            {
                return key;
            }
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Lookup<TKey, TElement> : ILookup<TKey, TElement>
    {
        readonly Dictionary<TKey, IGrouping<TKey, TElement>> groupings;

        public Lookup(Dictionary<TKey, IGrouping<TKey, TElement>> groupings)
        {
            this.groupings = groupings;
        }

        public IEnumerable<TElement> this[TKey key]
        {
            get
            {
                return groupings[key];
            }
        }

        public int Count
        {
            get
            {
                return groupings.Count;
            }
        }

        public bool Contains(TKey key)
        {
            return groupings.ContainsKey(key);
        }

        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            return groupings.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return groupings.Values.GetEnumerator();
        }
    }

    // NonGenerics

    public sealed class NonGenericListFormatter<T> : IJsonFormatter<T>
        where T : class, IList, new()
    {
        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            writer.WriteBeginArray();
            if (value.Count != 0)
            {
                formatter.Serialize(ref writer, value[0], formatterResolver);
            }
            for (int i = 1; i < value.Count; i++)
            {
                writer.WriteValueSeparator();
                formatter.Serialize(ref writer, value[i], formatterResolver);
            }
            writer.WriteEndArray();
        }

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            var list = new T();
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }
    }

    public sealed class NonGenericInterfaceEnumerableFormatter : IJsonFormatter<IEnumerable>
    {
        public static readonly IJsonFormatter<IEnumerable> Default = new NonGenericInterfaceEnumerableFormatter();

        public void Serialize(ref JsonWriter writer, IEnumerable value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            writer.WriteBeginArray();

            var i = 0;
            foreach (var item in value)
            {
                if (i != 0) writer.WriteValueSeparator();
                formatter.Serialize(ref writer, item, formatterResolver);
            }

            writer.WriteEndArray();
        }

        public IEnumerable Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            var list = new List<object>();
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }
    }

    public sealed class NonGenericInterfaceCollectionFormatter : IJsonFormatter<ICollection>
    {
        public static readonly IJsonFormatter<ICollection> Default = new NonGenericInterfaceCollectionFormatter();

        public void Serialize(ref JsonWriter writer, ICollection value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            writer.WriteBeginArray();
            var e = value.GetEnumerator();
            try
            {
                if (e.MoveNext())
                {
                    formatter.Serialize(ref writer, e.Current, formatterResolver);
                    while (e.MoveNext())
                    {
                        writer.WriteValueSeparator();
                        formatter.Serialize(ref writer, e.Current, formatterResolver);
                    }
                }
            }
            finally
            {
                var d = e as IDisposable;
                if (d != null) d.Dispose();
            }
            writer.WriteEndArray();
        }

        public ICollection Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            var list = new List<object>();
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }
    }

    public sealed class NonGenericInterfaceListFormatter : IJsonFormatter<IList>
    {
        public static readonly IJsonFormatter<IList> Default = new NonGenericInterfaceListFormatter();

        public void Serialize(ref JsonWriter writer, IList value, IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            writer.WriteBeginArray();
            if (value.Count != 0)
            {
                formatter.Serialize(ref writer, value[0], formatterResolver);
            }
            for (int i = 1; i < value.Count; i++)
            {
                writer.WriteValueSeparator();
                formatter.Serialize(ref writer, value[i], formatterResolver);
            }
            writer.WriteEndArray();
        }

        public IList Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull()) return null;

            var count = 0;
            var formatter = formatterResolver.GetFormatterWithVerify<object>();

            var list = new List<object>();
            reader.ReadIsBeginArrayWithVerify();
            while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
            {
                list.Add(formatter.Deserialize(ref reader, formatterResolver));
            }

            return list;
        }
    }


#if NETSTANDARD

    public sealed class ObservableCollectionFormatter<T> : CollectionFormatterBase<T, ObservableCollection<T>>
    {
        protected override void Add(ref ObservableCollection<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ObservableCollection<T> Create()
        {
            return new ObservableCollection<T>();
        }
    }

    public sealed class ReadOnlyObservableCollectionFormatter<T> : CollectionFormatterBase<T, ObservableCollection<T>, ReadOnlyObservableCollection<T>>
    {
        protected override void Add(ref ObservableCollection<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ObservableCollection<T> Create()
        {
            return new ObservableCollection<T>();
        }

        protected override ReadOnlyObservableCollection<T> Complete(ref ObservableCollection<T> intermediateCollection)
        {
            return new ReadOnlyObservableCollection<T>(intermediateCollection);
        }
    }

    public sealed class InterfaceReadOnlyListFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IReadOnlyList<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }

        protected override IReadOnlyList<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            return intermediateCollection.ToArray();
        }
    }

    public sealed class InterfaceReadOnlyCollectionFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IReadOnlyCollection<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }

        protected override IReadOnlyCollection<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            return intermediateCollection.ToArray();
        }
    }

    public sealed class InterfaceSetFormatter<T> : CollectionFormatterBase<T, HashSet<T>, ISet<T>>
    {
        protected override void Add(ref HashSet<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ISet<T> Complete(ref HashSet<T> intermediateCollection)
        {
            return intermediateCollection;
        }

        protected override HashSet<T> Create()
        {
            return new HashSet<T>();
        }
    }

    public sealed class ConcurrentBagFormatter<T> : CollectionFormatterBase<T, System.Collections.Concurrent.ConcurrentBag<T>>
    {
        protected override void Add(ref ConcurrentBag<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ConcurrentBag<T> Create()
        {
            return new ConcurrentBag<T>();
        }
    }

    public sealed class ConcurrentQueueFormatter<T> : CollectionFormatterBase<T, System.Collections.Concurrent.ConcurrentQueue<T>>
    {
        protected override void Add(ref ConcurrentQueue<T> collection, int index, T value)
        {
            collection.Enqueue(value);
        }

        protected override ConcurrentQueue<T> Create()
        {
            return new ConcurrentQueue<T>();
        }
    }

    public sealed class ConcurrentStackFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, ConcurrentStack<T>>
    {
        protected override void Add(ref ArrayBuffer<T> collection, int index, T value)
        {
            collection.Add(value);
        }

        protected override ArrayBuffer<T> Create()
        {
            return new ArrayBuffer<T>(4);
        }

        protected override ConcurrentStack<T> Complete(ref ArrayBuffer<T> intermediateCollection)
        {
            var bufArray = intermediateCollection.Buffer;
            var stack = new ConcurrentStack<T>();
            for (int i = intermediateCollection.Size - 1; i >= 0; i--)
            {
                stack.Push(bufArray[i]);
            }
            return stack;
        }
    }

#endif
}


namespace Utf8Json.Formatters.Internal
{
    internal static class CollectionFormatterHelper
    {
        internal static readonly byte[][] groupingName;
        internal static readonly AutomataDictionary groupingAutomata;

        static CollectionFormatterHelper()
        {
            groupingName = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("Key"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("Elements"),
            };
            groupingAutomata = new AutomataDictionary
            {
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Key"), 0 },
                {JsonWriter.GetEncodedPropertyNameWithoutQuotation("Elements"), 1 },
            };
        }
    }
}