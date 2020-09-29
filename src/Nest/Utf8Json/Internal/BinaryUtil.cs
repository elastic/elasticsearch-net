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
using System.Runtime.CompilerServices;

namespace Elasticsearch.Net.Utf8Json.Internal
{
	internal static class BinaryUtil
    {
		private const int ArrayMaxSize = 0x7FFFFFC7; // https://msdn.microsoft.com/en-us/library/system.array

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnsureCapacity(ref byte[] bytes, int offset, int appendLength)
        {
            var newLength = offset + appendLength;

            // If null(most case first time) fill byte.
            if (bytes == null)
            {
                bytes = new byte[newLength];
                return;
            }

            // like MemoryStream.EnsureCapacity
            var current = bytes.Length;
            if (newLength > current)
            {
                var num = newLength;
                if (num < 256)
                {
                    num = 256;
                    FastResize(ref bytes, num);
                    return;
                }

                if (current == ArrayMaxSize)
					throw new InvalidOperationException("byte[] size reached maximum size of array(0x7FFFFFC7), can not write to single byte[]. Details: https://msdn.microsoft.com/en-us/library/system.array");

				var newSize = unchecked((current * 2));
                if (newSize < 0) // overflow
					num = ArrayMaxSize;
				else
                {
                    if (num < newSize)
						num = newSize;
				}

                FastResize(ref bytes, num);
            }
        }

        // Buffer.BlockCopy version of Array.Resize
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FastResize(ref byte[] array, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException("newSize");

            var array2 = array;
            if (array2 == null)
			{
				array = JsonSerializer.MemoryPool.Rent(newSize);
                return;
            }

            if (array2.Length != newSize)
            {
                var array3 = JsonSerializer.MemoryPool.Rent(newSize);
                Buffer.BlockCopy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
                array = array3;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe byte[] FastCloneWithResize(byte[] src, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException("newSize");
            if (src.Length < newSize) throw new ArgumentException("length < newSize");

            if (src == null) return new byte[newSize];

            var dst = new byte[newSize];

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &dst[0])
            {
                Buffer.MemoryCopy(pSrc, pDst, dst.Length, newSize);
            }

            return dst;
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe byte[] ToArray(ref ArraySegment<byte> src)
		{
			if (src == null) throw new ArgumentNullException("src");

			var dst = new byte[src.Count];

			if (src.Count == 0)
				return dst;

			fixed (byte* pSrc = &src.Array[src.Offset])
			fixed (byte* pDst = &dst[0])
			{
				Buffer.MemoryCopy(pSrc, pDst, dst.Length, src.Count);
			}

			return dst;
		}
    }
}
