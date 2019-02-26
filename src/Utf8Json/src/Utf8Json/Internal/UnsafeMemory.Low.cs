#if NETSTANDARD

using System;
using System.Runtime.CompilerServices;

namespace Utf8Json.Internal
{
    // for string key property name write optimization.

    public static class UnsafeMemory
    {
        public static readonly bool Is32Bit = (IntPtr.Size == 4);

        public static void WriteRaw(ref JsonWriter writer, byte[] src)
        {
            switch (src.Length)
            {
                case 0: break;
                case 1: if (Is32Bit) { UnsafeMemory32.WriteRaw1(ref writer, src); } else { UnsafeMemory64.WriteRaw1(ref writer, src); } break;
                case 2: if (Is32Bit) { UnsafeMemory32.WriteRaw2(ref writer, src); } else { UnsafeMemory64.WriteRaw2(ref writer, src); } break;
                case 3: if (Is32Bit) { UnsafeMemory32.WriteRaw3(ref writer, src); } else { UnsafeMemory64.WriteRaw3(ref writer, src); } break;
                case 4: if (Is32Bit) { UnsafeMemory32.WriteRaw4(ref writer, src); } else { UnsafeMemory64.WriteRaw4(ref writer, src); } break;
                case 5: if (Is32Bit) { UnsafeMemory32.WriteRaw5(ref writer, src); } else { UnsafeMemory64.WriteRaw5(ref writer, src); } break;
                case 6: if (Is32Bit) { UnsafeMemory32.WriteRaw6(ref writer, src); } else { UnsafeMemory64.WriteRaw6(ref writer, src); } break;
                case 7: if (Is32Bit) { UnsafeMemory32.WriteRaw7(ref writer, src); } else { UnsafeMemory64.WriteRaw7(ref writer, src); } break;
                case 8: if (Is32Bit) { UnsafeMemory32.WriteRaw8(ref writer, src); } else { UnsafeMemory64.WriteRaw8(ref writer, src); } break;
                case 9: if (Is32Bit) { UnsafeMemory32.WriteRaw9(ref writer, src); } else { UnsafeMemory64.WriteRaw9(ref writer, src); } break;
                case 10: if (Is32Bit) { UnsafeMemory32.WriteRaw10(ref writer, src); } else { UnsafeMemory64.WriteRaw10(ref writer, src); } break;
                case 11: if (Is32Bit) { UnsafeMemory32.WriteRaw11(ref writer, src); } else { UnsafeMemory64.WriteRaw11(ref writer, src); } break;
                case 12: if (Is32Bit) { UnsafeMemory32.WriteRaw12(ref writer, src); } else { UnsafeMemory64.WriteRaw12(ref writer, src); } break;
                case 13: if (Is32Bit) { UnsafeMemory32.WriteRaw13(ref writer, src); } else { UnsafeMemory64.WriteRaw13(ref writer, src); } break;
                case 14: if (Is32Bit) { UnsafeMemory32.WriteRaw14(ref writer, src); } else { UnsafeMemory64.WriteRaw14(ref writer, src); } break;
                case 15: if (Is32Bit) { UnsafeMemory32.WriteRaw15(ref writer, src); } else { UnsafeMemory64.WriteRaw15(ref writer, src); } break;
                case 16: if (Is32Bit) { UnsafeMemory32.WriteRaw16(ref writer, src); } else { UnsafeMemory64.WriteRaw16(ref writer, src); } break;
                case 17: if (Is32Bit) { UnsafeMemory32.WriteRaw17(ref writer, src); } else { UnsafeMemory64.WriteRaw17(ref writer, src); } break;
                case 18: if (Is32Bit) { UnsafeMemory32.WriteRaw18(ref writer, src); } else { UnsafeMemory64.WriteRaw18(ref writer, src); } break;
                case 19: if (Is32Bit) { UnsafeMemory32.WriteRaw19(ref writer, src); } else { UnsafeMemory64.WriteRaw19(ref writer, src); } break;
                case 20: if (Is32Bit) { UnsafeMemory32.WriteRaw20(ref writer, src); } else { UnsafeMemory64.WriteRaw20(ref writer, src); } break;
                case 21: if (Is32Bit) { UnsafeMemory32.WriteRaw21(ref writer, src); } else { UnsafeMemory64.WriteRaw21(ref writer, src); } break;
                case 22: if (Is32Bit) { UnsafeMemory32.WriteRaw22(ref writer, src); } else { UnsafeMemory64.WriteRaw22(ref writer, src); } break;
                case 23: if (Is32Bit) { UnsafeMemory32.WriteRaw23(ref writer, src); } else { UnsafeMemory64.WriteRaw23(ref writer, src); } break;
                case 24: if (Is32Bit) { UnsafeMemory32.WriteRaw24(ref writer, src); } else { UnsafeMemory64.WriteRaw24(ref writer, src); } break;
                case 25: if (Is32Bit) { UnsafeMemory32.WriteRaw25(ref writer, src); } else { UnsafeMemory64.WriteRaw25(ref writer, src); } break;
                case 26: if (Is32Bit) { UnsafeMemory32.WriteRaw26(ref writer, src); } else { UnsafeMemory64.WriteRaw26(ref writer, src); } break;
                case 27: if (Is32Bit) { UnsafeMemory32.WriteRaw27(ref writer, src); } else { UnsafeMemory64.WriteRaw27(ref writer, src); } break;
                case 28: if (Is32Bit) { UnsafeMemory32.WriteRaw28(ref writer, src); } else { UnsafeMemory64.WriteRaw28(ref writer, src); } break;
                case 29: if (Is32Bit) { UnsafeMemory32.WriteRaw29(ref writer, src); } else { UnsafeMemory64.WriteRaw29(ref writer, src); } break;
                case 30: if (Is32Bit) { UnsafeMemory32.WriteRaw30(ref writer, src); } else { UnsafeMemory64.WriteRaw30(ref writer, src); } break;
                case 31: if (Is32Bit) { UnsafeMemory32.WriteRaw31(ref writer, src); } else { UnsafeMemory64.WriteRaw31(ref writer, src); } break;
                default:
                    MemoryCopy(ref writer, src);
                    break;
            }
        }

        public static unsafe void MemoryCopy(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);
#if !NET45
            fixed (void* dstP = &writer.buffer[writer.offset])
            fixed (void* srcP = &src[0])
            {
                Buffer.MemoryCopy(srcP, dstP, writer.buffer.Length - writer.offset, src.Length);
            }
#else
            Buffer.BlockCopy(src, 0, writer.buffer, writer.offset, src.Length);
#endif
            writer.offset += src.Length;
        }
    }

    public static partial class UnsafeMemory32
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw1(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(byte*)pDst = *(byte*)pSrc;
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw2(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(short*)pDst = *(short*)pSrc;
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw3(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(byte*)pDst = *(byte*)pSrc;
                *(short*)(pDst + 1) = *(short*)(pSrc + 1);
            }

            writer.offset += src.Length;
        }
    }

    public static partial class UnsafeMemory64
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw1(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(byte*)pDst = *(byte*)pSrc;
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw2(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(short*)pDst = *(short*)pSrc;
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw3(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(byte*)pDst = *(byte*)pSrc;
                *(short*)(pDst + 1) = *(short*)(pSrc + 1);
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw4(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(int*)(pDst + 0) = *(int*)(pSrc + 0);
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw5(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(int*)(pDst + 0) = *(int*)(pSrc + 0);
                *(int*)(pDst + 1) = *(int*)(pSrc + 1);
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw6(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(int*)(pDst + 0) = *(int*)(pSrc + 0);
                *(int*)(pDst + 2) = *(int*)(pSrc + 2);
            }

            writer.offset += src.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void WriteRaw7(ref JsonWriter writer, byte[] src)
        {
            BinaryUtil.EnsureCapacity(ref writer.buffer, writer.offset, src.Length);

            fixed (byte* pSrc = &src[0])
            fixed (byte* pDst = &writer.buffer[writer.offset])
            {
                *(int*)(pDst + 0) = *(int*)(pSrc + 0);
                *(int*)(pDst + 3) = *(int*)(pSrc + 3);
            }

            writer.offset += src.Length;
        }
    }
}

#endif