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
using System.Threading;

namespace Elasticsearch.Net.Utf8Json.Internal
{
    // Safe for multiple-read, single-write.
    internal class ThreadsafeTypeKeyHashTable<TValue>
    {
		private Entry[] _buckets;
		private int _size; // only use in writer lock

		private readonly object _writerLock = new object();
		private readonly float _loadFactor;

        // IEqualityComparer.Equals is overhead if key only Type, don't use it.
        // readonly IEqualityComparer<TKey> comparer;

        public ThreadsafeTypeKeyHashTable(int capacity = 4, float loadFactor = 0.75f)
        {
            var tableSize = CalculateCapacity(capacity, loadFactor);
            _buckets = new Entry[tableSize];
            _loadFactor = loadFactor;
        }

        public bool TryAdd(Type key, TValue value) => TryAdd(key, _ => value);

		public bool TryAdd(Type key, Func<Type, TValue> valueFactory) => TryAddInternal(key, valueFactory, out _);

		private bool TryAddInternal(Type key, Func<Type, TValue> valueFactory, out TValue resultingValue)
        {
            lock (_writerLock)
            {
                var nextCapacity = CalculateCapacity(_size + 1, _loadFactor);

                if (_buckets.Length < nextCapacity)
                {
                    // rehash
                    var nextBucket = new Entry[nextCapacity];
                    for (var i = 0; i < _buckets.Length; i++)
                    {
                        var e = _buckets[i];
                        while (e != null)
                        {
                            var newEntry = new Entry { Key = e.Key, Value = e.Value, Hash = e.Hash };
                            AddToBuckets(nextBucket, key, newEntry, null, out resultingValue);
                            e = e.Next;
                        }
                    }

                    // add entry(if failed to add, only do resize)
                    var successAdd = AddToBuckets(nextBucket, key, null, valueFactory, out resultingValue);

                    // replace field(threadsafe for read)
                    VolatileWrite(ref _buckets, nextBucket);

                    if (successAdd) _size++;
                    return successAdd;
                }
                else
                {
                    // add entry(insert last is thread safe for read)
                    var successAdd = AddToBuckets(_buckets, key, null, valueFactory, out resultingValue);
                    if (successAdd) _size++;
                    return successAdd;
                }
            }
        }

        bool AddToBuckets(Entry[] buckets, Type newKey, Entry newEntryOrNull, Func<Type, TValue> valueFactory, out TValue resultingValue)
        {
            var h = newEntryOrNull?.Hash ?? newKey.GetHashCode();
            if (buckets[h & (buckets.Length - 1)] == null)
            {
                if (newEntryOrNull != null)
                {
                    resultingValue = newEntryOrNull.Value;
                    VolatileWrite(ref buckets[h & (buckets.Length - 1)], newEntryOrNull);
                }
                else
                {
                    resultingValue = valueFactory(newKey);
                    VolatileWrite(ref buckets[h & (buckets.Length - 1)], new Entry { Key = newKey, Value = resultingValue, Hash = h });
                }
            }
            else
            {
                var searchLastEntry = buckets[h & (buckets.Length - 1)];
                while (true)
                {
                    if (searchLastEntry.Key == newKey)
                    {
                        resultingValue = searchLastEntry.Value;
                        return false;
                    }

                    if (searchLastEntry.Next == null)
                    {
                        if (newEntryOrNull != null)
                        {
                            resultingValue = newEntryOrNull.Value;
                            VolatileWrite(ref searchLastEntry.Next, newEntryOrNull);
                        }
                        else
                        {
                            resultingValue = valueFactory(newKey);
                            VolatileWrite(ref searchLastEntry.Next, new Entry { Key = newKey, Value = resultingValue, Hash = h });
                        }
                        break;
                    }
                    searchLastEntry = searchLastEntry.Next;
                }
            }

            return true;
        }

        public bool TryGetValue(Type key, out TValue value)
        {
            var table = _buckets;
            var hash = key.GetHashCode();
            var entry = table[hash & table.Length - 1];

            if (entry == null) goto NOT_FOUND;

            if (entry.Key == key)
            {
                value = entry.Value;
                return true;
            }

            var next = entry.Next;
            while (next != null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }
                next = next.Next;
            }

            NOT_FOUND:
            value = default;
            return false;
        }

        public TValue GetOrAdd(Type key, Func<Type, TValue> valueFactory)
        {
            TValue v;
            if (TryGetValue(key, out v))
            {
                return v;
            }

            TryAddInternal(key, valueFactory, out v);
            return v;
        }

		private static int CalculateCapacity(int collectionSize, float loadFactor)
        {
            var initialCapacity = (int)(collectionSize / loadFactor);
            var capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity < 8 ? 8 : capacity;
		}

		private static void VolatileWrite(ref Entry location, Entry value) => Volatile.Write(ref location, value);

		private static void VolatileWrite(ref Entry[] location, Entry[] value) => Volatile.Write(ref location, value);

		private class Entry
        {
            public Type Key;
            public TValue Value;
            public int Hash;
            public Entry Next;

            // debug only
            public override string ToString() => Key + "(" + Count() + ")";

			private int Count()
            {
                var count = 1;
                var n = this;
                while (n.Next != null)
                {
                    count++;
                    n = n.Next;
                }
                return count;
            }
        }
    }
}
