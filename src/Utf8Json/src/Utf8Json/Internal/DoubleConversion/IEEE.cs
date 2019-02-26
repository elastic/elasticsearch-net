using System;
using System.Runtime.InteropServices;

namespace Utf8Json.Internal.DoubleConversion
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
        public const ulong kSignMask = (0x8000000000000000);
        public const ulong kExponentMask = (0x7FF0000000000000);
        public const ulong kSignificandMask = (0x000FFFFFFFFFFFFF);
        public const ulong kHiddenBit = (0x0010000000000000);
        public const int kPhysicalSignificandSize = 52;  // Excludes the hidden bit.
        public const int kSignificandSize = 53;

        const int kExponentBias = 0x3FF + kPhysicalSignificandSize;
        const int kDenormalExponent = -kExponentBias + 1;
        const int kMaxExponent = 0x7FF - kExponentBias;
        const ulong kInfinity = (0x7FF0000000000000);
        const ulong kNaN = (0x7FF8000000000000);

        ulong d64_;

        public Double(double d)
        {
            d64_ = new UnionDoubleULong { d = d }.u64;
        }

        public Double(DiyFp d)
        {
            d64_ = DiyFpToUint64(d);
        }

        // The value encoded by this Double must be greater or equal to +0.0.
        // It must not be special (infinity, or NaN).
        public DiyFp AsDiyFp()
        {
            return new DiyFp(Significand(), Exponent());
        }

        // The value encoded by this Double must be strictly greater than 0.
        public DiyFp AsNormalizedDiyFp()
        {
            ulong f = Significand();
            int e = Exponent();

            // The current double could be a denormal.
            while ((f & kHiddenBit) == 0)
            {
                f <<= 1;
                e--;
            }
            // Do the final shifts in one go.
            f <<= DiyFp.kSignificandSize - kSignificandSize;
            e -= DiyFp.kSignificandSize - kSignificandSize;
            return new DiyFp(f, e);
        }

        // Returns the double's bit as uint64.
        public ulong AsUint64()
        {
            return d64_;
        }

        // Returns the next greater double. Returns +infinity on input +infinity.
        public double NextDouble()
        {
            if (d64_ == kInfinity) return new Double(kInfinity).value();
            if (Sign() < 0 && Significand() == 0)
            {
                // -0.0
                return 0.0;
            }
            if (Sign() < 0)
            {
                return new Double(d64_ - 1).value();
            }
            else
            {
                return new Double(d64_ + 1).value();
            }
        }

        public double PreviousDouble()
        {
            if (d64_ == (kInfinity | kSignMask)) return -Infinity();
            if (Sign() < 0)
            {
                return new Double(d64_ + 1).value();
            }
            else
            {
                if (Significand() == 0) return -0.0;
                return new Double(d64_ - 1).value();
            }
        }

        public int Exponent()
        {
            if (IsDenormal()) return kDenormalExponent;

            ulong d64 = AsUint64();
            int biased_e =
                (int)((d64 & kExponentMask) >> kPhysicalSignificandSize);
            return biased_e - kExponentBias;
        }

        public ulong Significand()
        {
            ulong d64 = AsUint64();
            ulong significand = d64 & kSignificandMask;
            if (!IsDenormal())
            {
                return significand + kHiddenBit;
            }
            else
            {
                return significand;
            }
        }

        // Returns true if the double is a denormal.
        public bool IsDenormal()
        {
            ulong d64 = AsUint64();
            return (d64 & kExponentMask) == 0;
        }

        // We consider denormals not to be special.
        // Hence only Infinity and NaN are special.
        public bool IsSpecial()
        {
            ulong d64 = AsUint64();
            return (d64 & kExponentMask) == kExponentMask;
        }

        public bool IsNan()
        {
            ulong d64 = AsUint64();
            return ((d64 & kExponentMask) == kExponentMask) &&
                ((d64 & kSignificandMask) != 0);
        }

        public bool IsInfinite()
        {
            ulong d64 = AsUint64();
            return ((d64 & kExponentMask) == kExponentMask) &&
                ((d64 & kSignificandMask) == 0);
        }

        public int Sign()
        {
            ulong d64 = AsUint64();
            return (d64 & kSignMask) == 0 ? 1 : -1;
        }

        // Precondition: the value encoded by this Double must be greater or equal
        // than +0.0.
        public DiyFp UpperBoundary()
        {
            return new DiyFp(Significand() * 2 + 1, Exponent() - 1);
        }

        // Computes the two boundaries of this.
        // The bigger boundary (m_plus) is normalized. The lower boundary has the same
        // exponent as m_plus.
        // Precondition: the value encoded by this Double must be greater than 0.
        public void NormalizedBoundaries(out DiyFp out_m_minus, out DiyFp out_m_plus)
        {
            DiyFp v = this.AsDiyFp();
            var __ = new DiyFp((v.f << 1) + 1, v.e - 1);
            var m_plus = DiyFp.Normalize(ref __);

            DiyFp m_minus;
            if (LowerBoundaryIsCloser())
            {
                m_minus = new DiyFp((v.f << 2) - 1, v.e - 2);
            }
            else
            {
                m_minus = new DiyFp((v.f << 1) - 1, v.e - 1);
            }
            m_minus.f = m_minus.f << (m_minus.e - m_plus.e);
            m_minus.e = (m_plus.e);
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
            bool physical_significand_is_zero = ((AsUint64() & kSignificandMask) == 0);
            return physical_significand_is_zero && (Exponent() != kDenormalExponent);
        }

        public double value()
        {
            return new UnionDoubleULong { u64 = d64_ }.d;
        }

        // Returns the significand size for a given order of magnitude.
        // If v = f*2^e with 2^p-1 <= f <= 2^p then p+e is v's order of magnitude.
        // This function returns the number of significant binary digits v will have
        // once it's encoded into a double. In almost all cases this is equal to
        // kSignificandSize. The only exceptions are denormals. They start with
        // leading zeroes and their effective significand-size is hence smaller.
        public static int SignificandSizeForOrderOfMagnitude(int order)
        {
            if (order >= (kDenormalExponent + kSignificandSize))
            {
                return kSignificandSize;
            }
            if (order <= kDenormalExponent) return 0;
            return order - kDenormalExponent;
        }

        public static double Infinity()
        {
            return new Double(kInfinity).value();
        }

        public static double NaN()
        {
            return new Double(kNaN).value();
        }

        public static ulong DiyFpToUint64(DiyFp diy_fp)
        {
            ulong significand = diy_fp.f;
            int exponent = diy_fp.e;
            while (significand > kHiddenBit + kSignificandMask)
            {
                significand >>= 1;
                exponent++;
            }
            if (exponent >= kMaxExponent)
            {
                return kInfinity;
            }
            if (exponent < kDenormalExponent)
            {
                return 0;
            }
            while (exponent > kDenormalExponent && (significand & kHiddenBit) == 0)
            {
                significand <<= 1;
                exponent--;
            }
            ulong biased_exponent;
            if (exponent == kDenormalExponent && (significand & kHiddenBit) == 0)
            {
                biased_exponent = 0;
            }
            else
            {
                biased_exponent = (ulong)(exponent + kExponentBias);
            }
            return (significand & kSignificandMask) |
                (biased_exponent << kPhysicalSignificandSize);
        }
    }

    internal struct Single
    {
        const int kExponentBias = 0x7F + kPhysicalSignificandSize;
        const int kDenormalExponent = -kExponentBias + 1;
        const int kMaxExponent = 0xFF - kExponentBias;
        const uint32_t kInfinity = 0x7F800000;
        const uint32_t kNaN = 0x7FC00000;

        public const uint32_t kSignMask = 0x80000000;
        public const uint32_t kExponentMask = 0x7F800000;
        public const uint32_t kSignificandMask = 0x007FFFFF;
        public const uint32_t kHiddenBit = 0x00800000;
        public const int kPhysicalSignificandSize = 23;  // Excludes the hidden bit.
        public const int kSignificandSize = 24;

        uint32_t d32_;

        public Single(float f)
        {
            this.d32_ = new UnionFloatUInt { f = f }.u32;
        }

        // The value encoded by this Single must be greater or equal to +0.0.
        // It must not be special (infinity, or NaN).
        public DiyFp AsDiyFp()
        {
            return new DiyFp(Significand(), Exponent());
        }

        // Returns the single's bit as uint64.
        public uint32_t AsUint32()
        {
            return d32_;
        }

        public int Exponent()
        {
            if (IsDenormal()) return kDenormalExponent;

            uint32_t d32 = AsUint32();
            int biased_e = (int)((d32 & kExponentMask) >> kPhysicalSignificandSize);
            return biased_e - kExponentBias;
        }

        public uint32_t Significand()
        {
            uint32_t d32 = AsUint32();
            uint32_t significand = d32 & kSignificandMask;
            if (!IsDenormal())
            {
                return significand + kHiddenBit;
            }
            else
            {
                return significand;
            }
        }

        // Returns true if the single is a denormal.
        public bool IsDenormal()
        {
            uint32_t d32 = AsUint32();
            return (d32 & kExponentMask) == 0;
        }

        // We consider denormals not to be special.
        // Hence only Infinity and NaN are special.
        public bool IsSpecial()
        {
            uint32_t d32 = AsUint32();
            return (d32 & kExponentMask) == kExponentMask;
        }

        public bool IsNan()
        {
            uint32_t d32 = AsUint32();
            return ((d32 & kExponentMask) == kExponentMask) &&
                ((d32 & kSignificandMask) != 0);
        }

        public bool IsInfinite()
        {
            uint32_t d32 = AsUint32();
            return ((d32 & kExponentMask) == kExponentMask) &&
                ((d32 & kSignificandMask) == 0);
        }

        public int Sign()
        {
            uint32_t d32 = AsUint32();
            return (d32 & kSignMask) == 0 ? 1 : -1;
        }

        // Computes the two boundaries of this.
        // The bigger boundary (m_plus) is normalized. The lower boundary has the same
        // exponent as m_plus.
        // Precondition: the value encoded by this Single must be greater than 0.
        public void NormalizedBoundaries(out DiyFp out_m_minus, out DiyFp out_m_plus)
        {
            DiyFp v = this.AsDiyFp();
            var __ = new DiyFp((v.f << 1) + 1, v.e - 1);
            DiyFp m_plus = DiyFp.Normalize(ref __);
            DiyFp m_minus;
            if (LowerBoundaryIsCloser())
            {
                m_minus = new DiyFp((v.f << 2) - 1, v.e - 2);
            }
            else
            {
                m_minus = new DiyFp((v.f << 1) - 1, v.e - 1);
            }
            m_minus.f = (m_minus.f << (m_minus.e - m_plus.e));
            m_minus.e = (m_plus.e);
            out_m_plus = m_plus;
            out_m_minus = m_minus;
        }

        // Precondition: the value encoded by this Single must be greater or equal
        // than +0.0.
        public DiyFp UpperBoundary()
        {
            return new DiyFp(Significand() * 2 + 1, Exponent() - 1);
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
            bool physical_significand_is_zero = ((AsUint32() & kSignificandMask) == 0);
            return physical_significand_is_zero && (Exponent() != kDenormalExponent);
        }

        public float value() { return new UnionFloatUInt { u32 = d32_ }.f; }

        public static float Infinity()
        {
            return new Single(kInfinity).value();
        }

        public static float NaN()
        {
            return new Single(kNaN).value();
        }
    }
}
