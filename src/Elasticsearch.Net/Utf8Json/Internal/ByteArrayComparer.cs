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
	internal static class ByteArrayComparer
    {
        private static readonly bool Is32Bit = IntPtr.Size == 4;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetHashCode(byte[] bytes, int offset, int count)
		{
			if (Is32Bit)
				return unchecked((int)FarmHash.Hash32(bytes, offset, count));

			return unchecked((int)FarmHash.Hash64(bytes, offset, count));
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Equals(byte[] xs, int xsOffset, int xsCount, byte[] ys) =>
			Equals(xs, xsOffset, xsCount, ys, 0, ys.Length);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool Equals(byte[] xs, int xsOffset, int xsCount, byte[] ys, int ysOffset, int ysCount)
        {
            if (xs == null || ys == null || xsCount != ysCount)
				return false;

			if (xsCount == 0 && ysCount == 0)
				return true;

            fixed (byte* p1 = &xs[xsOffset])
            fixed (byte* p2 = &ys[ysOffset])
            {
                switch (xsCount)
                {
                    case 0:
                        return true;
                    case 1:
                        return *p1 == *p2;
                    case 2:
                        return *(short*)p1 == *(short*)p2;
                    case 3:
                        if (*p1 != *p2) return false;
                        return *(short*)(p1 + 1) == *(short*)(p2 + 1);
                    case 4:
                        return *(int*)p1 == *(int*)p2;
                    case 5:
                        if (*p1 != *p2) return false;
                        return *(int*)(p1 + 1) == *(int*)(p2 + 1);
                    case 6:
                        if (*(short*)p1 != *(short*)p2) return false;
                        return *(int*)(p1 + 2) == *(int*)(p2 + 2);
                    case 7:
                        if (*p1 != *p2) return false;
                        if (*(short*)(p1 + 1) != *(short*)(p2 + 1)) return false;
                        return *(int*)(p1 + 3) == *(int*)(p2 + 3);
                    default:
                        {
                            var x1 = p1;
                            var x2 = p2;

                            var xEnd = p1 + xsCount - 8;
                            var yEnd = p2 + ysCount - 8;

                            while (x1 < xEnd)
                            {
                                if (*(long*)x1 != *(long*)x2)
                                {
                                    return false;
                                }

                                x1 += 8;
                                x2 += 8;
                            }

                            return *(long*)xEnd == *(long*)yEnd;
                        }
                }
            }
        }
    }
}
