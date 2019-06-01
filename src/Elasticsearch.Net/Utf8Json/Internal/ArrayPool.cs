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

namespace Elasticsearch.Net.Utf8Json.Internal
{
    internal sealed class BufferPool : ArrayPool<byte>
    {
        public static readonly BufferPool Default = new BufferPool(65535);

        public BufferPool(int bufferLength)
            : base(bufferLength)
        {
        }
    }

    internal class ArrayPool<T>
    {
        readonly int bufferLength;
        readonly object gate;
        int index;
        T[][] buffers;

        public ArrayPool(int bufferLength)
        {
            this.bufferLength = bufferLength;
            this.buffers = new T[4][];
            this.gate = new object();
        }

        public T[] Rent()
        {
            lock (gate)
            {
                if (index >= buffers.Length)
                {
                    Array.Resize(ref buffers, buffers.Length * 2);
                }

                if (buffers[index] == null)
                {
                    buffers[index] = new T[bufferLength];
                }

                var buffer = buffers[index];
                buffers[index] = null;
                index++;

                return buffer;
            }
        }

        public void Return(T[] array)
        {
            if (array.Length != bufferLength)
            {
                throw new InvalidOperationException("return buffer is not from pool");
            }

            lock (gate)
            {
                if (index != 0)
                {
                    buffers[--index] = array;
                }
            }
        }
    }
}
