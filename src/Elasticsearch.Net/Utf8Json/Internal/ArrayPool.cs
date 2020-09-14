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
    internal class ArrayPool<T>
    {
        private readonly int _bufferLength;
        private readonly object _gate;
        private int _index;
        private T[][] _buffers;

        public ArrayPool(int bufferLength)
        {
            _bufferLength = bufferLength;
            _buffers = new T[4][];
            _gate = new object();
        }

        public T[] Rent()
        {
            lock (_gate)
            {
                if (_index >= _buffers.Length)
					Array.Resize(ref _buffers, _buffers.Length * 2);

                if (_buffers[_index] == null)
					_buffers[_index] = new T[_bufferLength];

                var buffer = _buffers[_index];
                _buffers[_index] = null;
                _index++;

                return buffer;
            }
        }

        public void Return(T[] array)
        {
            if (array.Length != _bufferLength)
				throw new InvalidOperationException("return buffer is not from pool");

            lock (_gate)
            {
                if (_index != 0)
					_buffers[--_index] = array;
			}
        }
    }
}
