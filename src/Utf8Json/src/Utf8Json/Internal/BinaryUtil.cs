using System;
using System.Collections.Generic;
using System.Text;

#if NETSTANDARD
using System.Runtime.CompilerServices;
#endif

namespace Utf8Json.Internal
{
    public static class BinaryUtil
    {
        const int ArrayMaxSize = 0x7FFFFFC7; // https://msdn.microsoft.com/en-us/library/system.array

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void EnsureCapacity(ref byte[] bytes, int offset, int appendLength)
        {
            var newLength = offset + appendLength;

            // If null(most case fisrt time) fill byte.
            if (bytes == null)
            {
                bytes = new byte[newLength];
                return;
            }

            // like MemoryStream.EnsureCapacity
            var current = bytes.Length;
            if (newLength > current)
            {
                int num = newLength;
                if (num < 256)
                {
                    num = 256;
                    FastResize(ref bytes, num);
                    return;
                }

                if (current == ArrayMaxSize)
                {
                    throw new InvalidOperationException("byte[] size reached maximum size of array(0x7FFFFFC7), can not write to single byte[]. Details: https://msdn.microsoft.com/en-us/library/system.array");
                }

                var newSize = unchecked((current * 2));
                if (newSize < 0) // overflow
                {
                    num = ArrayMaxSize;
                }
                else
                {
                    if (num < newSize)
                    {
                        num = newSize;
                    }
                }

                FastResize(ref bytes, num);
            }
        }

        // Buffer.BlockCopy version of Array.Resize
#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static void FastResize(ref byte[] array, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException("newSize");

            byte[] array2 = array;
            if (array2 == null)
			{
				array = JsonSerializer.MemoryPool.Rent(newSize);
                return;
            }

            if (array2.Length != newSize)
            {
                byte[] array3 = JsonSerializer.MemoryPool.Rent(newSize);
                Buffer.BlockCopy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
                array = array3;
            }
        }

#if NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        public static
#if NETSTANDARD
            unsafe
#endif
            byte[] FastCloneWithResize(byte[] src, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException("newSize");
            if (src.Length < newSize) throw new ArgumentException("length < newSize");

            if (src == null) return new byte[newSize];

            byte[] dst = new byte[newSize];

#if NETSTANDARD && !NET45
            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &dst[0])
            {
                Buffer.MemoryCopy(pSrc, pDst, dst.Length, newSize);
            }
#else
            Buffer.BlockCopy(src, 0, dst, 0, newSize);
#endif

            return dst;
        }

#if NETSTANDARD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
		public static
#if NETSTANDARD
			unsafe
#endif
			byte[] ToArray(ref ArraySegment<byte> src)
		{
			if (src == null) throw new ArgumentNullException("src");

			byte[] dst = new byte[src.Count];

			if (src.Count == 0)
				return dst;

#if NETSTANDARD && !NET45
			fixed (byte* pSrc = &src.Array[src.Offset])
			fixed (byte* pDst = &dst[0])
			{
				Buffer.MemoryCopy(pSrc, pDst, dst.Length, src.Count);
			}
#else
            Buffer.BlockCopy(src.Array, src.Offset, dst, 0, src.Count);
#endif

			return dst;
		}
    }
}
