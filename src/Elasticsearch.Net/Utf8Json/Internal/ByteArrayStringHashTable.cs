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

namespace Elasticsearch.Net.Utf8Json.Internal
{
    // like ArraySegment<byte> hashtable.
    // Add is safe for construction phase only and requires capacity(does not do rehash)

    internal class ByteArrayStringHashTable<T> : IEnumerable<KeyValuePair<string, T>>
    {
        readonly Entry[][] buckets; // immutable array(faster than linkedlist)
        readonly ulong indexFor;

        public ByteArrayStringHashTable(int capacity)
            : this(capacity, 0.42f) // default: 0.75f -> 0.42f
        {
        }

        public ByteArrayStringHashTable(int capacity, float loadFactor)
        {
            var tableSize = CalculateCapacity(capacity, loadFactor);
            this.buckets = new Entry[tableSize][];
            this.indexFor = (ulong)buckets.Length - 1;
        }

        public void Add(string key, T value)
        {
            if (!TryAddInternal(Encoding.UTF8.GetBytes(key), value))
            {
                throw new ArgumentException("Key already exists. Key:" + key);
            }
        }

        public void Add(byte[] key, T value)
        {
            if (!TryAddInternal(key, value))
            {
                throw new ArgumentException("Key already exists. Key:" + Encoding.UTF8.GetString(key));
            }
        }

        bool TryAddInternal(byte[] key, T value)
        {
            var h = ByteArrayGetHashCode(key, 0, key.Length);
            var entry = new Entry { Key = key, Value = value };

            var array = buckets[h & (indexFor)];
            if (array == null)
            {
                buckets[h & (indexFor)] = new[] { entry };
            }
            else
            {
                // check duplicate
                for (int i = 0; i < array.Length; i++)
                {
                    var e = array[i].Key;
                    if (ByteArrayComparer.Equals(key, 0, key.Length, e))
                    {
                        return false;
                    }
                }

                var newArray = new Entry[array.Length + 1];
                Array.Copy(array, newArray, array.Length);
                array = newArray;
                array[array.Length - 1] = entry;
                buckets[h & (indexFor)] = array;
            }

            return true;
        }

        public bool TryGetValue(ArraySegment<byte> key, out T value)
        {
            var table = buckets;
            var hash = ByteArrayGetHashCode(key.Array, key.Offset, key.Count);
            var entry = table[hash & indexFor];

            if (entry == null) goto NOT_FOUND;

            {
                var v = entry[0];
                if (ByteArrayComparer.Equals(key.Array, key.Offset, key.Count, v.Key))
                {
                    value = v.Value;
                    return true;
                }
            }

            for (int i = 1; i < entry.Length; i++)
            {
                var v = entry[i];
                if (ByteArrayComparer.Equals(key.Array, key.Offset, key.Count, v.Key))
                {
                    value = v.Value;
                    return true;
                }
            }

            NOT_FOUND:
            value = default(T);
            return false;
        }

        static readonly bool Is32Bit = (IntPtr.Size == 4);

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        static ulong ByteArrayGetHashCode(byte[] x, int offset, int count)
        {
            // FarmHash https://github.com/google/farmhash
            if (x == null || x.Length == 0 || count == 0) return 0;

            if (Is32Bit)
            {
                return (ulong)FarmHash.Hash32(x, offset, count);
            }
            else
            {
                return FarmHash.Hash64(x, offset, count);
            }

        }

        static int CalculateCapacity(int collectionSize, float loadFactor)
        {
            var initialCapacity = (int)(((float)collectionSize) / loadFactor);
            var capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            if (capacity < 8)
            {
                return 8;
            }

            return capacity;
        }

        // only for Debug use
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            var b = this.buckets;

            foreach (var item in b)
            {
                if (item == null) continue;
                foreach (var item2 in item)
                {
                    yield return new KeyValuePair<string, T>(Encoding.UTF8.GetString(item2.Key), item2.Value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        struct Entry
        {
            public byte[] Key;
            public T Value;

            // for debugging
            public override string ToString()
            {
                return "(" + Encoding.UTF8.GetString(Key) + ", " + Value + ")";
            }
        }
    }
}
