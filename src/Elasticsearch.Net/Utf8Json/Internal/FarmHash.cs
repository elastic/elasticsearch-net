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

using System.Runtime.CompilerServices;

namespace Elasticsearch.Net.Utf8Json.Internal
{
    internal static class FarmHash
    {
        // entry point of 32bit

        #region Hash32

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe uint Hash32(byte[] bytes, int offset, int count)
        {
            if (count <= 4)
				return Hash32Len0to4(bytes, offset, (uint)count);

			fixed (byte* p = &bytes[offset])
				return Hash32(p, (uint)count);
		}

        // port of farmhash.cc, 32bit only

        // Magic numbers for 32-bit hashing.  Copied from Murmur3.
		private const uint C1 = 0xcc9e2d51;
		private const uint C2 = 0x1b873593;

		private static unsafe uint Fetch32(byte* p) => *(uint*)p;

		private static uint Rotate32(uint val, int shift) => shift == 0 ? val : ((val >> shift) | (val << (32 - shift)));

		// A 32-bit to 32-bit integer hash copied from Murmur3.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Fmix(uint h)
        {
            unchecked
            {
                h ^= h >> 16;
                h *= 0x85ebca6b;
                h ^= h >> 13;
                h *= 0xc2b2ae35;
                h ^= h >> 16;
                return h;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Mur(uint a, uint h)
        {
            unchecked
            {
                // Helper from Murmur3 for combining two 32-bit values.
                a *= C1;
                a = Rotate32(a, 17);
                a *= C2;
                h ^= a;
                h = Rotate32(h, 19);
                return h * 5 + 0xe6546b64;
            }
        }

        // 0-4
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Hash32Len0to4(byte[] s, int offset, uint len)
        {
            unchecked
            {
                uint b = 0;
                uint c = 9;
                var max = offset + len;
                for (var i = offset; i < max; i++)
                {
                    b = b * C1 + s[i];
                    c ^= b;
                }
                return Fmix(Mur(b, Mur(len, c)));
            }
        }

        // 5-12
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe uint Hash32Len5to12(byte* s, uint len)
        {
            unchecked
            {
                uint a = len, b = len * 5, c = 9, d = b;
                a += Fetch32(s);
                b += Fetch32(s + len - 4);
                c += Fetch32(s + ((len >> 1) & 4));
                return Fmix(Mur(c, Mur(b, Mur(a, d))));
            }
        }

        // 13-24
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe uint Hash32Len13to24(byte* s, uint len)
        {
            unchecked
            {
                var a = Fetch32(s - 4 + (len >> 1));
                var b = Fetch32(s + 4);
                var c = Fetch32(s + len - 8);
                var d = Fetch32(s + (len >> 1));
                var e = Fetch32(s);
                var f = Fetch32(s + len - 4);
                var h = d * C1 + len;
                a = Rotate32(a, 12) + f;
                h = Mur(c, h) + a;
                a = Rotate32(a, 3) + c;
                h = Mur(e, h) + a;
                a = Rotate32(a + f, 12) + d;
                h = Mur(b, h) + a;
                return Fmix(h);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe uint Hash32(byte* s, uint len)
        {
            if (len <= 24)
				return len <= 12 ? Hash32Len5to12(s, len) : Hash32Len13to24(s, len);

			unchecked
            {
                // len > 24
                uint h = len, g = C1 * len, f = g;
                var a0 = Rotate32(Fetch32(s + len - 4) * C1, 17) * C2;
                var a1 = Rotate32(Fetch32(s + len - 8) * C1, 17) * C2;
                var a2 = Rotate32(Fetch32(s + len - 16) * C1, 17) * C2;
                var a3 = Rotate32(Fetch32(s + len - 12) * C1, 17) * C2;
                var a4 = Rotate32(Fetch32(s + len - 20) * C1, 17) * C2;
                h ^= a0;
                h = Rotate32(h, 19);
                h = h * 5 + 0xe6546b64;
                h ^= a2;
                h = Rotate32(h, 19);
                h = h * 5 + 0xe6546b64;
                g ^= a1;
                g = Rotate32(g, 19);
                g = g * 5 + 0xe6546b64;
                g ^= a3;
                g = Rotate32(g, 19);
                g = g * 5 + 0xe6546b64;
                f += a4;
                f = Rotate32(f, 19) + 113;
                var iters = (len - 1) / 20;
                do
                {
                    var a = Fetch32(s);
                    var b = Fetch32(s + 4);
                    var c = Fetch32(s + 8);
                    var d = Fetch32(s + 12);
                    var e = Fetch32(s + 16);
                    h += a;
                    g += b;
                    f += c;
                    h = Mur(d, h) + e;
                    g = Mur(c, g) + a;
                    f = Mur(b + e * C1, f) + d;
                    f += g;
                    g += f;
                    s += 20;
                } while (--iters != 0);
                g = Rotate32(g, 11) * C1;
                g = Rotate32(g, 17) * C1;
                f = Rotate32(f, 11) * C1;
                f = Rotate32(f, 17) * C1;
                h = Rotate32(h + g, 19);
                h = h * 5 + 0xe6546b64;
                h = Rotate32(h, 17) * C1;
                h = Rotate32(h + f, 19);
                h = h * 5 + 0xe6546b64;
                h = Rotate32(h, 17) * C1;
                return h;
            }
        }

        #endregion

        #region hash64

        // entry point

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ulong Hash64(byte[] bytes, int offset, int count)
        {
            fixed (byte* p = &bytes[offset])
				return Hash64(p, (uint)count);
		}

        // port from farmhash.cc

		private struct Pair
        {
            public ulong First;
            public ulong Second;

            public Pair(ulong first, ulong second)
            {
                First = first;
                Second = second;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Pair MakePair(ulong first, ulong second) => new Pair(first, second);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Swap(ref ulong x, ref ulong z)
        {
            var temp = z;
            z = x;
            x = temp;
        }

        // Some primes between 2^63 and 2^64 for various uses.
		private const ulong K0 = 0xc3a5c85c97cb3127UL;
		private const ulong K1 = 0xb492b66fbe98f273UL;
		private const ulong K2 = 0x9ae16a3b2f90404fUL;

		private static unsafe ulong Fetch64(byte* p) => *(ulong*)p;

		private static ulong Rotate64(ulong val, int shift) => shift == 0 ? val : (val >> shift) | (val << (64 - shift));

		// farmhashna.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong ShiftMix(ulong val) => val ^ (val >> 47);

		// farmhashna.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong HashLen16(ulong u, ulong v, ulong mul)
        {
            unchecked
            {
                // Murmur-inspired hashing.
                var a = (u ^ v) * mul;
                a ^= a >> 47;
                var b = (v ^ a) * mul;
                b ^= b >> 47;
                b *= mul;
                return b;
            }
        }

        // farmhashxo.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong Hash64(byte* s, uint len)
        {
            if (len <= 16)
				// farmhashna::
				return HashLen0to16(s, len);

            if (len <= 32)
				// farmhashna::
				return HashLen17to32(s, len);

            if (len <= 64)
				return HashLen33to64(s, len);

			if (len <= 96) return HashLen65to96(s, len);

			if (len <= 256)
				// farmhashna::
				return Hash64NA(s, len);

            // farmhashuo::
            return Hash64UO(s, len);
        }

        // 0-16 farmhashna.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong HashLen0to16(byte* s, uint len)
        {
            unchecked
            {
                if (len >= 8)
                {
                    var mul = K2 + len * 2;
                    var a = Fetch64(s) + K2;
                    var b = Fetch64(s + len - 8);
                    var c = Rotate64(b, 37) * mul + a;
                    var d = (Rotate64(a, 25) + b) * mul;
                    return HashLen16(c, d, mul);
                }
                if (len >= 4)
                {
                    var mul = K2 + len * 2;
                    ulong a = Fetch32(s);
                    return HashLen16(len + (a << 3), Fetch32(s + len - 4), mul);
                }
                if (len > 0)
                {
                    ushort a = s[0];
                    ushort b = s[len >> 1];
                    ushort c = s[len - 1];
                    var y = a + ((uint)b << 8);
                    var z = len + ((uint)c << 2);
                    return ShiftMix(y * K2 ^ z * K0) * K2;
                }
                return K2;
            }
        }

        // 17-32 farmhashna.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong HashLen17to32(byte* s, uint len)
        {
            unchecked
            {
                var mul = K2 + len * 2;
                var a = Fetch64(s) * K1;
                var b = Fetch64(s + 8);
                var c = Fetch64(s + len - 8) * mul;
                var d = Fetch64(s + len - 16) * K2;
                return HashLen16(Rotate64(a + b, 43) + Rotate64(c, 30) + d,
                                 a + Rotate64(b + K2, 18) + c, mul);
            }
        }

        // farmhashxo.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong H32(byte* s, uint len, ulong mul, ulong seed0 = 0, ulong seed1 = 0)
        {
            unchecked
            {
                var a = Fetch64(s) * K1;
                var b = Fetch64(s + 8);
                var c = Fetch64(s + len - 8) * mul;
                var d = Fetch64(s + len - 16) * K2;
                var u = Rotate64(a + b, 43) + Rotate64(c, 30) + d + seed0;
                var v = a + Rotate64(b + K2, 18) + c + seed1;
                a = ShiftMix((u ^ v) * mul);
                b = ShiftMix((v ^ a) * mul);
                return b;
            }
        }

        // 33-64 farmhashxo.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong HashLen33to64(byte* s, uint len)
        {
            const ulong mul0 = K2 - 30;

            unchecked
            {
                var mul1 = K2 - 30 + 2 * len;
                var h0 = H32(s, 32, mul0);
                var h1 = H32(s + len - 32, 32, mul1);
                return (h1 * mul1 + h0) * mul1;
            }
        }

        // 65-96 farmhashxo.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong HashLen65to96(byte* s, uint len)
        {
            const ulong mul0 = K2 - 114;

            unchecked
            {
                var mul1 = K2 - 114 + 2 * len;
                var h0 = H32(s, 32, mul0);
                var h1 = H32(s + 32, 32, mul1);
                var h2 = H32(s + len - 32, 32, mul1, h0, h1);
                return (h2 * 9 + (h0 >> 17) + (h1 >> 21)) * mul1;
            }
        }

        // farmhashna.cc
        // Return a 16-byte hash for 48 bytes.  Quick and dirty.
        // Callers do best to use "random-looking" values for a and b.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Pair WeakHashLen32WithSeeds(ulong w, ulong x, ulong y, ulong z, ulong a, ulong b)
        {
            unchecked
            {
                a += w;
                b = Rotate64(b + a + z, 21);
                var c = a;
                a += x;
                a += y;
                b += Rotate64(a, 44);
                return MakePair(a + z, b + c);
            }
        }

        // farmhashna.cc
        // Return a 16-byte hash for s[0] ... s[31], a, and b.  Quick and dirty.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe Pair WeakHashLen32WithSeeds(byte* s, ulong a, ulong b) =>
			WeakHashLen32WithSeeds(Fetch64(s),
				Fetch64(s + 8),
				Fetch64(s + 16),
				Fetch64(s + 24),
				a,
				b);

		// na(97-256) farmhashna.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong Hash64NA(byte* s, uint len)
        {
            const ulong seed = 81;

            unchecked
            {
                // For strings over 64 bytes we loop.  Internal state consists of
                // 56 bytes: v, w, x, y, and z.
                var x = seed;
                var y = seed * K1 + 113;
                var z = ShiftMix(y * K2 + 113) * K2;
                var v = MakePair(0, 0);
                var w = MakePair(0, 0);
                x = x * K2 + Fetch64(s);

                // Set end so that after the loop we have 1 to 64 bytes left to process.
                var end = s + ((len - 1) / 64) * 64;
                var last64 = end + ((len - 1) & 63) - 63;

                do
                {
                    x = Rotate64(x + y + v.First + Fetch64(s + 8), 37) * K1;
                    y = Rotate64(y + v.Second + Fetch64(s + 48), 42) * K1;
                    x ^= w.Second;
                    y += v.First + Fetch64(s + 40);
                    z = Rotate64(z + w.First, 33) * K1;
                    v = WeakHashLen32WithSeeds(s, v.Second * K1, x + w.First);
                    w = WeakHashLen32WithSeeds(s + 32, z + w.Second, y + Fetch64(s + 16));
                    Swap(ref z, ref x);
                    s += 64;
                } while (s != end);
                var mul = K1 + ((z & 0xff) << 1);
                // Make s point to the last 64 bytes of input.
                s = last64;
                w.First += ((len - 1) & 63);
                v.First += w.First;
                w.First += v.First;
                x = Rotate64(x + y + v.First + Fetch64(s + 8), 37) * mul;
                y = Rotate64(y + v.Second + Fetch64(s + 48), 42) * mul;
                x ^= w.Second * 9;
                y += v.First * 9 + Fetch64(s + 40);
                z = Rotate64(z + w.First, 33) * mul;
                v = WeakHashLen32WithSeeds(s, v.Second * mul, x + w.First);
                w = WeakHashLen32WithSeeds(s + 32, z + w.Second, y + Fetch64(s + 16));
                Swap(ref z, ref x);
                return HashLen16(HashLen16(v.First, w.First, mul) + ShiftMix(y) * K0 + z,
                                 HashLen16(v.Second, w.Second, mul) + x,
                                 mul);
            }
        }

        // farmhashuo.cc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong H(ulong x, ulong y, ulong mul, int r)
        {
            unchecked
            {
                var a = (x ^ y) * mul;
                a ^= (a >> 47);
                var b = (y ^ a) * mul;
                return Rotate64(b, r) * mul;
            }
        }

        // uo(257-) farmhashuo.cc, Hash64WithSeeds
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe ulong Hash64UO(byte* s, uint len)
        {
            const ulong seed0 = 81;
            const ulong seed1 = 0;

            unchecked
            {
                // For strings over 64 bytes we loop.  Internal state consists of
                // 64 bytes: u, v, w, x, y, and z.
                var x = seed0;
                var y = seed1 * K2 + 113;
                var z = ShiftMix(y * K2) * K2;
                var v = MakePair(seed0, seed1);
                var w = MakePair(0, 0);
                var u = x - z;
                x *= K2;
                var mul = K2 + (u & 0x82);

                // Set end so that after the loop we have 1 to 64 bytes left to process.
                var end = s + ((len - 1) / 64) * 64;
                var last64 = end + ((len - 1) & 63) - 63;

                do
                {
                    var a0 = Fetch64(s);
                    var a1 = Fetch64(s + 8);
                    var a2 = Fetch64(s + 16);
                    var a3 = Fetch64(s + 24);
                    var a4 = Fetch64(s + 32);
                    var a5 = Fetch64(s + 40);
                    var a6 = Fetch64(s + 48);
                    var a7 = Fetch64(s + 56);
                    x += a0 + a1;
                    y += a2;
                    z += a3;
                    v.First += a4;
                    v.Second += a5 + a1;
                    w.First += a6;
                    w.Second += a7;

                    x = Rotate64(x, 26);
                    x *= 9;
                    y = Rotate64(y, 29);
                    z *= mul;
                    v.First = Rotate64(v.First, 33);
                    v.Second = Rotate64(v.Second, 30);
                    w.First ^= x;
                    w.First *= 9;
                    z = Rotate64(z, 32);
                    z += w.Second;
                    w.Second += z;
                    z *= 9;
                    Swap(ref u, ref y);

                    z += a0 + a6;
                    v.First += a2;
                    v.Second += a3;
                    w.First += a4;
                    w.Second += a5 + a6;
                    x += a1;
                    y += a7;

                    y += v.First;
                    v.First += x - y;
                    v.Second += w.First;
                    w.First += v.Second;
                    w.Second += x - y;
                    x += w.Second;
                    w.Second = Rotate64(w.Second, 34);
                    Swap(ref u, ref z);
                    s += 64;
                } while (s != end);
                // Make s point to the last 64 bytes of input.
                s = last64;
                u *= 9;
                v.Second = Rotate64(v.Second, 28);
                v.First = Rotate64(v.First, 20);
                w.First += ((len - 1) & 63);
                u += y;
                y += u;
                x = Rotate64(y - x + v.First + Fetch64(s + 8), 37) * mul;
                y = Rotate64(y ^ v.Second ^ Fetch64(s + 48), 42) * mul;
                x ^= w.Second * 9;
                y += v.First + Fetch64(s + 40);
                z = Rotate64(z + w.First, 33) * mul;
                v = WeakHashLen32WithSeeds(s, v.Second * mul, x + w.First);
                w = WeakHashLen32WithSeeds(s + 32, z + w.Second, y + Fetch64(s + 16));
                return H(HashLen16(v.First + x, w.First ^ y, mul) + z - u,
                         H(v.Second + y, w.Second + z, K2, 30) ^ x,
                         K2,
                         31);
            }
        }

        #endregion
    }
}
