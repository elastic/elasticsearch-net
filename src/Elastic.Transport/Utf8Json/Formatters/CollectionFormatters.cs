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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Utf8Json.Formatters
{
	internal class ArrayFormatter<T> : IJsonFormatter<T[]>
	{
		private static readonly ArrayPool<T> ArrayPool = new ArrayPool<T>(99);

		public void Serialize(ref JsonWriter writer, T[] value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }

			writer.WriteBeginArray();
			var formatter = formatterResolver.GetFormatterWithVerify<T>();
			if (value.Length != 0)
				formatter.Serialize(ref writer, value[0], formatterResolver);

			for (var i = 1; i < value.Length; i++)
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

			var workingArea = ArrayPool.Rent();
			try
			{
				var array = workingArea;
				reader.ReadIsBeginArrayWithVerify();
				while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
				{
					if (array.Length < count)
						Array.Resize(ref array, array.Length * 2);

					array[count - 1] = formatter.Deserialize(ref reader, formatterResolver);
				}

				var result = new T[count];
				Array.Copy(array, result, count);
				Array.Clear(workingArea, 0, Math.Min(count, workingArea.Length));
				return result;
			}
			finally
			{
				ArrayPool.Return(workingArea);
			}
		}
	}

	internal class ArraySegmentFormatter<T> : IJsonFormatter<ArraySegment<T>>
	{
		private static readonly ArrayPool<T> ArrayPool = new ArrayPool<T>(99);

		public void Serialize(ref JsonWriter writer, ArraySegment<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value.Array == null) { writer.WriteNull(); return; }

			var array = value.Array;
			var offset = value.Offset;
			var count = value.Count;

			writer.WriteBeginArray();
			var formatter = formatterResolver.GetFormatterWithVerify<T>();
			if (count != 0)
				formatter.Serialize(ref writer, value.Array[offset], formatterResolver);

			for (var i = 1; i < count; i++)
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

			var workingArea = ArrayPool.Rent();
			try
			{
				var array = workingArea;
				reader.ReadIsBeginArrayWithVerify();
				while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
				{
					if (array.Length < count)
						Array.Resize(ref array, array.Length * 2);

					array[count - 1] = formatter.Deserialize(ref reader, formatterResolver);
				}

				var result = new T[count];
				Array.Copy(array, result, count);
				Array.Clear(workingArea, 0, Math.Min(count, workingArea.Length));
				return new ArraySegment<T>(result, 0, result.Length);
			}
			finally
			{
				ArrayPool.Return(workingArea);
			}
		}
	}

	internal class ListFormatter<T> : IJsonFormatter<List<T>>
	{
		public void Serialize(ref JsonWriter writer, List<T> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null) { writer.WriteNull(); return; }

			writer.WriteBeginArray();
			var formatter = formatterResolver.GetFormatterWithVerify<T>();
			if (value.Count != 0)
				formatter.Serialize(ref writer, value[0], formatterResolver);

			for (var i = 1; i < value.Count; i++)
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
				list.Add(formatter.Deserialize(ref reader, formatterResolver));

			return list;
		}
	}

	internal abstract class CollectionFormatterBase<TElement, TIntermediate, TEnumerator, TCollection> : IJsonFormatter<TCollection>
		where TCollection : class, IEnumerable<TElement>
		where TEnumerator : IEnumerator<TElement>
	{
		public void Serialize(ref JsonWriter writer, TCollection value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
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
							isFirst = false;
						else
							writer.WriteValueSeparator();
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
				return null;

			var formatter = formatterResolver.GetFormatterWithVerify<TElement>();
			var builder = Create();

			var count = 0;
			reader.ReadIsBeginArrayWithVerify();
			while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count))
				Add(ref builder, count - 1, formatter.Deserialize(ref reader, formatterResolver));

			return Complete(ref builder);
		}

		// Some collections can use struct iterator, this is optimization path
		protected abstract TEnumerator GetSourceEnumerator(TCollection source);

		// abstraction for deserialize
		protected abstract TIntermediate Create();
		protected abstract void Add(ref TIntermediate collection, int index, TElement value);
		protected abstract TCollection Complete(ref TIntermediate intermediateCollection);
	}

	internal abstract class CollectionFormatterBase<TElement, TIntermediate, TCollection> : CollectionFormatterBase<TElement, TIntermediate, IEnumerator<TElement>, TCollection>
		where TCollection : class, IEnumerable<TElement>
	{
		protected override IEnumerator<TElement> GetSourceEnumerator(TCollection source) => source.GetEnumerator();
	}

	internal abstract class CollectionFormatterBase<TElement, TCollection> : CollectionFormatterBase<TElement, TCollection, TCollection>
		where TCollection : class, IEnumerable<TElement>
	{
		protected sealed override TCollection Complete(ref TCollection intermediateCollection) => intermediateCollection;
	}

	internal sealed class GenericCollectionFormatter<TElement, TCollection> : CollectionFormatterBase<TElement, TCollection>
		where TCollection : class, ICollection<TElement>, new()
	{
		protected override TCollection Create() => new TCollection();

		protected override void Add(ref TCollection collection, int index, TElement value) => collection.Add(value);
	}

	internal sealed class LinkedListFormatter<T> : CollectionFormatterBase<T, LinkedList<T>, LinkedList<T>.Enumerator, LinkedList<T>>
	{
		protected override void Add(ref LinkedList<T> collection, int index, T value) => collection.AddLast(value);

		protected override LinkedList<T> Complete(ref LinkedList<T> intermediateCollection) => intermediateCollection;

		protected override LinkedList<T> Create() => new LinkedList<T>();

		protected override LinkedList<T>.Enumerator GetSourceEnumerator(LinkedList<T> source) => source.GetEnumerator();
	}

	internal sealed class QueueFormatter<T> : CollectionFormatterBase<T, Queue<T>, Queue<T>.Enumerator, Queue<T>>
	{
		protected override void Add(ref Queue<T> collection, int index, T value) => collection.Enqueue(value);

		protected override Queue<T> Create() => new Queue<T>();

		protected override Queue<T>.Enumerator GetSourceEnumerator(Queue<T> source) => source.GetEnumerator();

		protected override Queue<T> Complete(ref Queue<T> intermediateCollection) => intermediateCollection;
	}


// should deserialize reverse order.
	internal sealed class StackFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, Stack<T>.Enumerator, Stack<T>>
	{
		protected override void Add(ref ArrayBuffer<T> collection, int index, T value) => collection.Add(value);

		protected override ArrayBuffer<T> Create() => new ArrayBuffer<T>(4);

		protected override Stack<T>.Enumerator GetSourceEnumerator(Stack<T> source) => source.GetEnumerator();

		protected override Stack<T> Complete(ref ArrayBuffer<T> intermediateCollection)
		{
			var bufArray = intermediateCollection.Buffer;
			var stack = new Stack<T>(intermediateCollection.Size);
			for (var i = intermediateCollection.Size - 1; i >= 0; i--)
				stack.Push(bufArray[i]);

			return stack;
		}
	}

	internal sealed class HashSetFormatter<T> : CollectionFormatterBase<T, HashSet<T>, HashSet<T>.Enumerator, HashSet<T>>
	{
		protected override void Add(ref HashSet<T> collection, int index, T value) => collection.Add(value);

		protected override HashSet<T> Complete(ref HashSet<T> intermediateCollection) => intermediateCollection;

		protected override HashSet<T> Create() => new HashSet<T>();

		protected override HashSet<T>.Enumerator GetSourceEnumerator(HashSet<T> source) => source.GetEnumerator();
	}

	internal sealed class ReadOnlyCollectionFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, ReadOnlyCollection<T>>
	{
		protected override void Add(ref ArrayBuffer<T> collection, int index, T value) => collection.Add(value);

		protected override ReadOnlyCollection<T> Complete(ref ArrayBuffer<T> intermediateCollection) =>
			new ReadOnlyCollection<T>(intermediateCollection.ToArray());

		protected override ArrayBuffer<T> Create() => new ArrayBuffer<T>(4);
	}

	internal sealed class InterfaceListFormatter<T> : CollectionFormatterBase<T, List<T>, IList<T>>
	{
		protected override void Add(ref List<T> collection, int index, T value) => collection.Add(value);

		protected override List<T> Create() => new List<T>();

		protected override IList<T> Complete(ref List<T> intermediateCollection) => intermediateCollection;
	}

	internal sealed class InterfaceCollectionFormatter<T> : CollectionFormatterBase<T, List<T>, ICollection<T>>
	{
		protected override void Add(ref List<T> collection, int index, T value) => collection.Add(value);

		protected override List<T> Create() => new List<T>();

		protected override ICollection<T> Complete(ref List<T> intermediateCollection) => intermediateCollection;
	}

	internal sealed class InterfaceEnumerableFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IEnumerable<T>>
	{
		protected override void Add(ref ArrayBuffer<T> collection, int index, T value) => collection.Add(value);

		protected override ArrayBuffer<T> Create() => new ArrayBuffer<T>(4);

		protected override IEnumerable<T> Complete(ref ArrayBuffer<T> intermediateCollection) => intermediateCollection.ToArray();
	}

	// {Key:key, Elements:[Array]}  (not compatible with JSON.NET)
	internal sealed class InterfaceGroupingFormatter<TKey, TElement> : IJsonFormatter<IGrouping<TKey, TElement>>
	{
		public void Serialize(ref JsonWriter writer, IGrouping<TKey, TElement> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteRaw(CollectionFormatterHelper.groupingName[0]);
			formatterResolver.GetFormatterWithVerify<TKey>().Serialize(ref writer, value.Key, formatterResolver);
			writer.WriteRaw(CollectionFormatterHelper.groupingName[1]);
			formatterResolver.GetFormatterWithVerify<IEnumerable<TElement>>().Serialize(ref writer, value.AsEnumerable(), formatterResolver);

			writer.WriteEndObject();
		}

		public IGrouping<TKey, TElement> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			TKey resultKey = default;
			IEnumerable<TElement> resultValue = default;

			reader.ReadIsBeginObjectWithVerify();

			var count = 0;
			while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref count))
			{
				var keyString = reader.ReadPropertyNameSegmentRaw();
				CollectionFormatterHelper.groupingAutomata.TryGetValue(keyString, out var key);

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

	internal sealed class InterfaceLookupFormatter<TKey, TElement> : IJsonFormatter<ILookup<TKey, TElement>>
	{
		public void Serialize(ref JsonWriter writer, ILookup<TKey, TElement> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			formatterResolver.GetFormatterWithVerify<IEnumerable<IGrouping<TKey, TElement>>>().Serialize(ref writer, value.AsEnumerable(), formatterResolver);
		}

		public ILookup<TKey, TElement> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			// check if TElement is null
			if (reader.ReadIsNull())
				return null;

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

	internal class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
	{
		private readonly TKey _key;
		private readonly IEnumerable<TElement> _elements;

		public Grouping(TKey key, IEnumerable<TElement> elements)
		{
			_key = key;
			_elements = elements;
		}

		public TKey Key => _key;

		public IEnumerator<TElement> GetEnumerator() => _elements.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	internal class Lookup<TKey, TElement> : ILookup<TKey, TElement>
	{
		private readonly Dictionary<TKey, IGrouping<TKey, TElement>> _groupings;

		public Lookup(Dictionary<TKey, IGrouping<TKey, TElement>> groupings) => _groupings = groupings;

		public IEnumerable<TElement> this[TKey key] => _groupings[key];

		public int Count => _groupings.Count;

		public bool Contains(TKey key) => _groupings.ContainsKey(key);

		public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator() => _groupings.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	// NonGenerics

	internal sealed class NonGenericListFormatter<T> : IJsonFormatter<T>
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
				formatter.Serialize(ref writer, value[0], formatterResolver);

			for (var i = 1; i < value.Count; i++)
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
				list.Add(formatter.Deserialize(ref reader, formatterResolver));

			return list;
		}
	}

	internal sealed class NonGenericInterfaceEnumerableFormatter : IJsonFormatter<IEnumerable>
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
				++i;
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
				list.Add(formatter.Deserialize(ref reader, formatterResolver));

			return list;
		}
	}

	internal sealed class NonGenericInterfaceCollectionFormatter : IJsonFormatter<ICollection>
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
				if (e is IDisposable d) d.Dispose();
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
				list.Add(formatter.Deserialize(ref reader, formatterResolver));

			return list;
		}
	}

	internal sealed class NonGenericInterfaceListFormatter : IJsonFormatter<IList>
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
				formatter.Serialize(ref writer, value[0], formatterResolver);

			for (var i = 1; i < value.Count; i++)
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
				list.Add(formatter.Deserialize(ref reader, formatterResolver));

			return list;
		}
	}

	internal sealed class ObservableCollectionFormatter<T> : CollectionFormatterBase<T, ObservableCollection<T>>
	{
		protected override void Add(ref ObservableCollection<T> collection, int index, T value) => collection.Add(value);

		protected override ObservableCollection<T> Create() => new ObservableCollection<T>();
	}

	internal sealed class ReadOnlyObservableCollectionFormatter<T> : CollectionFormatterBase<T, ObservableCollection<T>, ReadOnlyObservableCollection<T>>
	{
		protected override void Add(ref ObservableCollection<T> collection, int index, T value) => collection.Add(value);

		protected override ObservableCollection<T> Create() => new ObservableCollection<T>();

		protected override ReadOnlyObservableCollection<T> Complete(ref ObservableCollection<T> intermediateCollection) =>
			new ReadOnlyObservableCollection<T>(intermediateCollection);
	}

	internal sealed class InterfaceReadOnlyListFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IReadOnlyList<T>>
	{
		protected override void Add(ref ArrayBuffer<T> collection, int index, T value) => collection.Add(value);

		protected override ArrayBuffer<T> Create() => new ArrayBuffer<T>(4);

		protected override IReadOnlyList<T> Complete(ref ArrayBuffer<T> intermediateCollection) => intermediateCollection.ToArray();
	}

	internal sealed class InterfaceReadOnlyCollectionFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, IReadOnlyCollection<T>>
	{
		protected override void Add(ref ArrayBuffer<T> collection, int index, T value) => collection.Add(value);

		protected override ArrayBuffer<T> Create() => new ArrayBuffer<T>(4);

		protected override IReadOnlyCollection<T> Complete(ref ArrayBuffer<T> intermediateCollection) => intermediateCollection.ToArray();
	}

	internal sealed class InterfaceSetFormatter<T> : CollectionFormatterBase<T, HashSet<T>, ISet<T>>
	{
		protected override void Add(ref HashSet<T> collection, int index, T value) => collection.Add(value);

		protected override ISet<T> Complete(ref HashSet<T> intermediateCollection) => intermediateCollection;

		protected override HashSet<T> Create() => new HashSet<T>();
	}

	internal sealed class ConcurrentBagFormatter<T> : CollectionFormatterBase<T, ConcurrentBag<T>>
	{
		protected override void Add(ref ConcurrentBag<T> collection, int index, T value) => collection.Add(value);

		protected override ConcurrentBag<T> Create() => new ConcurrentBag<T>();
	}

	internal sealed class ConcurrentQueueFormatter<T> : CollectionFormatterBase<T, ConcurrentQueue<T>>
	{
		protected override void Add(ref ConcurrentQueue<T> collection, int index, T value) => collection.Enqueue(value);

		protected override ConcurrentQueue<T> Create() => new ConcurrentQueue<T>();
	}

	internal sealed class ConcurrentStackFormatter<T> : CollectionFormatterBase<T, ArrayBuffer<T>, ConcurrentStack<T>>
	{
		protected override void Add(ref ArrayBuffer<T> collection, int index, T value) => collection.Add(value);

		protected override ArrayBuffer<T> Create() => new ArrayBuffer<T>(4);

		protected override ConcurrentStack<T> Complete(ref ArrayBuffer<T> intermediateCollection)
		{
			var bufArray = intermediateCollection.Buffer;
			var stack = new ConcurrentStack<T>();
			for (var i = intermediateCollection.Size - 1; i >= 0; i--)
				stack.Push(bufArray[i]);

			return stack;
		}
	}

	internal static class CollectionFormatterHelper
	{
		internal static readonly byte[][] groupingName;
		internal static readonly AutomataDictionary groupingAutomata;

		static CollectionFormatterHelper()
		{
			groupingName = new[]
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
