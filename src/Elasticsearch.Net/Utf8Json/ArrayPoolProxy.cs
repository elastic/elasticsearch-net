using System;

namespace Elasticsearch.Net.Utf8Json
{
	internal static class ArrayPoolProxy
	{
		/// <summary>
		/// Method to Resize an already rented array
		/// </summary>
		public static byte[] Resize(byte[] array, int newSize)
		{
			byte[] newRented = Rent(newSize);
			Buffer.BlockCopy(array, 0, newRented, 0, array.Length > newSize ? newSize : array.Length);
			Return(array);
			return newRented;
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
