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
using System.Runtime.InteropServices;

namespace Elasticsearch.Net.Utf8Json.Internal.DoubleConversion
{
    using uint32_t = UInt32;

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct UnionDoubleULong
    {
        [FieldOffset(0)]
        public double d;
        [FieldOffset(0)]
        public ulong u64;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    internal struct UnionFloatUInt
    {
        [FieldOffset(0)]
        public float f;
        [FieldOffset(0)]
        public uint u32;
    }

    // https://github.com/google/double-conversion/blob/master/double-conversion/ieee.h

    internal struct Double
    {
		private const ulong KSignMask = (0x8000000000000000);
		private const ulong KExponentMask = (0x7FF0000000000000);
		private const ulong KSignificandMask = (0x000FFFFFFFFFFFFF);
		private const ulong KHiddenBit = (0x0010000000000000);
		private const int KPhysicalSignificandSize = 52;  // Excludes the hidden bit.
		private const int KSignificandSize = 53;

		private const int KExponentBias = 0x3FF + KPhysicalSignificandSize;
		private const int KDenormalExponent = -KExponentBias + 1;
		private const int KMaxExponent = 0x7FF - KExponentBias;
		private const ulong KInfinity = (0x7FF0000000000000);
		private const ulong KNaN = (0x7FF8000000000000);

		private readonly ulong _d64;

        public Double(double d) => _d64 = new UnionDoubleULong { d = d }.u64;

		public Double(DiyFp d) => _d64 = DiyFpToUint64(d);

		// The value encoded by this Double must be greater or equal to +0.0.
        // It must not be special (infinity, or NaN).
        public DiyFp AsDiyFp() => new DiyFp(Significand(), Exponent());

		// The value encoded by this Double must be strictly greater than 0.
        public DiyFp AsNormalizedDiyFp()
        {
            var f = Significand();
            var e = Exponent();

            // The current double could be a denormal.
            while ((f & KHiddenBit) == 0)
            {
                f <<= 1;
                e--;
            }
            // Do the final shifts in one go.
            f <<= DiyFp.KSignificandSize - KSignificandSize;
            e -= DiyFp.KSignificandSize - KSignificandSize;
            return new DiyFp(f, e);
        }

        // Returns the double's bit as uint64.
        public ulong AsUint64() => _d64;

		// Returns the next greater double. Returns +infinity on input +infinity.
        public double NextDouble()
        {
            if (_d64 == KInfinity) return new Double(KInfinity).value();
            if (Sign() < 0 && Significand() == 0)
            {
                // -0.0
                return 0.0;
            }
            if (Sign() < 0)
				return new Double(_d64 - 1).value();

			return new Double(_d64 + 1).value();
		}

        public double PreviousDouble()
        {
            if (_d64 == (KInfinity | KSignMask)) return -Infinity();
            if (Sign() < 0)
				return new Double(_d64 + 1).value();

			if (Significand() == 0) return -0.0;
			return new Double(_d64 - 1).value();
		}

        public int Exponent()
        {
            if (IsDenormal()) return KDenormalExponent;

            var d64 = AsUint64();
            var biased_e =
                (int)((d64 & KExponentMask) >> KPhysicalSignificandSize);
            return biased_e - KExponentBias;
        }

        public ulong Significand()
        {
            var d64 = AsUint64();
            var significand = d64 & KSignificandMask;
            if (!IsDenormal())
				return significand + KHiddenBit;

			return significand;
		}

        // Returns true if the double is a denormal.
        public bool IsDenormal()
        {
            var d64 = AsUint64();
            return (d64 & KExponentMask) == 0;
        }

        // We consider denormals not to be special.
        // Hence only Infinity and NaN are special.
        public bool IsSpecial()
        {
            var d64 = AsUint64();
            return (d64 & KExponentMask) == KExponentMask;
        }

        public bool IsNan()
        {
            var d64 = AsUint64();
            return ((d64 & KExponentMask) == KExponentMask) &&
                ((d64 & KSignificandMask) != 0);
        }

        public bool IsInfinite()
        {
            var d64 = AsUint64();
            return ((d64 & KExponentMask) == KExponentMask) &&
                ((d64 & KSignificandMask) == 0);
        }

        public int Sign()
        {
            var d64 = AsUint64();
            return (d64 & KSignMask) == 0 ? 1 : -1;
        }

        // Precondition: the value encoded by this Double must be greater or equal
        // than +0.0.
        public DiyFp UpperBoundary() => new DiyFp(Significand() * 2 + 1, Exponent() - 1);

		// Computes the two boundaries of this.
        // The bigger boundary (m_plus) is normalized. The lower boundary has the same
        // exponent as m_plus.
        // Precondition: the value encoded by this Double must be greater than 0.
        public void NormalizedBoundaries(out DiyFp out_m_minus, out DiyFp out_m_plus)
        {
            var v = AsDiyFp();
            var __ = new DiyFp((v.F << 1) + 1, v.E - 1);
            var m_plus = DiyFp.Normalize(ref __);

            DiyFp m_minus;
            if (LowerBoundaryIsCloser())
				m_minus = new DiyFp((v.F << 2) - 1, v.E - 2);
			else
				m_minus = new DiyFp((v.F << 1) - 1, v.E - 1);
			m_minus.F = m_minus.F << (m_minus.E - m_plus.E);
            m_minus.E = (m_plus.E);
            out_m_plus = m_plus;
            out_m_minus = m_minus;
        }

        public bool LowerBoundaryIsCloser()
        {
            // The boundary is closer if the significand is of the form f == 2^p-1 then
            // the lower boundary is closer.
            // Think of v = 1000e10 and v- = 9999e9.
            // Then the boundary (== (v - v-)/2) is not just at a distance of 1e9 but
            // at a distance of 1e8.
            // The only exception is for the smallest normal: the largest denormal is
            // at the same distance as its successor.
            // Note: denormals have the same exponent as the smallest normals.
            var physical_significand_is_zero = ((AsUint64() & KSignificandMask) == 0);
            return physical_significand_is_zero && (Exponent() != KDenormalExponent);
        }

        public double value() => new UnionDoubleULong { u64 = _d64 }.d;

		// Returns the significand size for a given order of magnitude.
        // If v = f*2^e with 2^p-1 <= f <= 2^p then p+e is v's order of magnitude.
        // This function returns the number of significant binary digits v will have
        // once it's encoded into a double. In almost all cases this is equal to
        // kSignificandSize. The only exceptions are denormals. They start with
        // leading zeroes and their effective significand-size is hence smaller.
        public static int SignificandSizeForOrderOfMagnitude(int order)
        {
            if (order >= (KDenormalExponent + KSignificandSize))
				return KSignificandSize;
			if (order <= KDenormalExponent)
				return 0;

			return order - KDenormalExponent;
        }

        public static double Infinity() => new Double(KInfinity).value();

		public static double NaN() => new Double(KNaN).value();

		public static ulong DiyFpToUint64(DiyFp diy_fp)
        {
            var significand = diy_fp.F;
            var exponent = diy_fp.E;
            while (significand > KHiddenBit + KSignificandMask)
            {
                significand >>= 1;
                exponent++;
            }
            if (exponent >= KMaxExponent)
				return KInfinity;

			if (exponent < KDenormalExponent)
				return 0;

			while (exponent > KDenormalExponent && (significand & KHiddenBit) == 0)
            {
                significand <<= 1;
                exponent--;
            }
            ulong biased_exponent;
            if (exponent == KDenormalExponent && (significand & KHiddenBit) == 0)
				biased_exponent = 0;
			else
				biased_exponent = (ulong)(exponent + KExponentBias);

			return (significand & KSignificandMask) |
                (biased_exponent << KPhysicalSignificandSize);
        }
    }

    internal struct Single
    {
		private const int KExponentBias = 0x7F + KPhysicalSignificandSize;
		private const int KDenormalExponent = -KExponentBias + 1;
		private const int KMaxExponent = 0xFF - KExponentBias;
		private const uint KInfinity = 0x7F800000;
		private const uint KNaN = 0x7FC00000;

		private const uint KSignMask = 0x80000000;
		private const uint KExponentMask = 0x7F800000;
		private const uint KSignificandMask = 0x007FFFFF;
		private const uint KHiddenBit = 0x00800000;
		private const int KPhysicalSignificandSize = 23;  // Excludes the hidden bit.
        public const int KSignificandSize = 24;

		private readonly uint _d32;

        public Single(float f) => _d32 = new UnionFloatUInt { f = f }.u32;

		// The value encoded by this Single must be greater or equal to +0.0.
        // It must not be special (infinity, or NaN).
        public DiyFp AsDiyFp() => new DiyFp(Significand(), Exponent());

		// Returns the single's bit as uint64.
        public uint AsUint32() => _d32;

		public int Exponent()
        {
            if (IsDenormal()) return KDenormalExponent;

            var d32 = AsUint32();
            var biased_e = (int)((d32 & KExponentMask) >> KPhysicalSignificandSize);
            return biased_e - KExponentBias;
        }

        public uint Significand()
        {
            var d32 = AsUint32();
            var significand = d32 & KSignificandMask;
            if (!IsDenormal())
				return significand + KHiddenBit;

			return significand;
		}

        // Returns true if the single is a denormal.
        public bool IsDenormal()
        {
            var d32 = AsUint32();
            return (d32 & KExponentMask) == 0;
        }

        // We consider denormals not to be special.
        // Hence only Infinity and NaN are special.
        public bool IsSpecial()
        {
            var d32 = AsUint32();
            return (d32 & KExponentMask) == KExponentMask;
        }

        public bool IsNan()
        {
            var d32 = AsUint32();
            return ((d32 & KExponentMask) == KExponentMask) &&
                ((d32 & KSignificandMask) != 0);
        }

        public bool IsInfinite()
        {
            var d32 = AsUint32();
            return ((d32 & KExponentMask) == KExponentMask) &&
                ((d32 & KSignificandMask) == 0);
        }

        public int Sign()
        {
            var d32 = AsUint32();
            return (d32 & KSignMask) == 0 ? 1 : -1;
        }

        // Computes the two boundaries of this.
        // The bigger boundary (m_plus) is normalized. The lower boundary has the same
        // exponent as m_plus.
        // Precondition: the value encoded by this Single must be greater than 0.
        public void NormalizedBoundaries(out DiyFp out_m_minus, out DiyFp out_m_plus)
        {
            var v = AsDiyFp();
            var __ = new DiyFp((v.F << 1) + 1, v.E - 1);
            var m_plus = DiyFp.Normalize(ref __);
            DiyFp m_minus;
            if (LowerBoundaryIsCloser())
				m_minus = new DiyFp((v.F << 2) - 1, v.E - 2);
			else
				m_minus = new DiyFp((v.F << 1) - 1, v.E - 1);

			m_minus.F = (m_minus.F << (m_minus.E - m_plus.E));
            m_minus.E = (m_plus.E);
            out_m_plus = m_plus;
            out_m_minus = m_minus;
        }

        // Precondition: the value encoded by this Single must be greater or equal
        // than +0.0.
        public DiyFp UpperBoundary() => new DiyFp(Significand() * 2 + 1, Exponent() - 1);

		public bool LowerBoundaryIsCloser()
        {
            // The boundary is closer if the significand is of the form f == 2^p-1 then
            // the lower boundary is closer.
            // Think of v = 1000e10 and v- = 9999e9.
            // Then the boundary (== (v - v-)/2) is not just at a distance of 1e9 but
            // at a distance of 1e8.
            // The only exception is for the smallest normal: the largest denormal is
            // at the same distance as its successor.
            // Note: denormals have the same exponent as the smallest normals.
            var physical_significand_is_zero = ((AsUint32() & KSignificandMask) == 0);
            return physical_significand_is_zero && (Exponent() != KDenormalExponent);
        }

        public float value() => new UnionFloatUInt { u32 = _d32 }.f;

		public static float Infinity() => new Single(KInfinity).value();

		public static float NaN() => new Single(KNaN).value();
	}
}
