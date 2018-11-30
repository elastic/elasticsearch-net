using System;
using System.Runtime.CompilerServices;
using Utf8Json;

namespace Nest
{
	internal static class Utf8JsonReaderExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double? ReadNullableDouble(this ref JsonReader reader)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Null)
				return reader.ReadDouble();

			reader.ReadNext();
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long? ReadNullableLong(this ref JsonReader reader)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Null)
				return reader.ReadInt64();

			reader.ReadNext();
			return null;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool? ReadNullableBoolean(this ref JsonReader reader)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Null)
				return reader.ReadBoolean();

			reader.ReadNext();
			return null;
		}
	}

	internal static class ArraySegmentBytesExtensions
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDouble(this ref ArraySegment<byte> arraySegment)
		{
			for (var i = arraySegment.Offset; i < arraySegment.Count; i++)
			{
				if (arraySegment.Array[i] == 48)
					return true;
			}

			return false;
		}
	}
}
