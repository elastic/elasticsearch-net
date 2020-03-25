using System;

namespace Elasticsearch.Net.Utf8Json
{
	internal static class MemoryPool
	{
		// Method to Resize an already rented array
		public static void Resize(ref byte[] array, int newSize)
		{
			byte[] newRented = Rent(newSize);
			Buffer.BlockCopy(array, 0, newRented, 0, array.Length);
			Return(array);
			array = newRented;
		}

		public static byte[] Rent(int minLength = 65535)
		{
			return System.Buffers.ArrayPool<byte>.Shared.Rent(minLength);
		}

		public static void Return(byte[] bytes)
		{
			System.Buffers.ArrayPool<byte>.Shared.Return(bytes);
		}
	}
}
