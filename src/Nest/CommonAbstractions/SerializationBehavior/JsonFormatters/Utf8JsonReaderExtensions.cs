using System;
using System.Runtime.CompilerServices;
using System.Text;
using Utf8Json;
using Utf8Json.Formatters;

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
		private const byte DateMathSeparator = (byte)'|';

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDouble(this ref ArraySegment<byte> arraySegment)
		{
			var i = 0;
			while (i < arraySegment.Count)
			{
				if (arraySegment.Array[arraySegment.Offset + i] == 46)
					return true;

				i++;
			}

			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualsBytes(this ref ArraySegment<byte> arraySegment, byte[] bytes)
		{
			if (arraySegment == default || bytes == null || bytes.Length == 0)
				return false;

			if (arraySegment.Count != bytes.Length)
				return false;

			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] != arraySegment.Array[arraySegment.Offset + i])
					return false;
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDateTime(this ref ArraySegment<byte> arraySegment, IJsonFormatterResolver formatterResolver, out DateTime dateTime)
		{
			dateTime = default;

			// TODO: Nicer way to do this
			var reader = new JsonReader(arraySegment.Array, arraySegment.Offset - 1); // include opening quote "
            try
            {
            	dateTime = ISO8601DateTimeFormatter.Default.Deserialize(ref reader, formatterResolver);
				return true;
			}
            catch
			{
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ContainsDateMathSeparator(this ref ArraySegment<byte> segment)
		{
			var i = 0;
			while (i < segment.Count)
			{
				if (segment.Array[segment.Offset + i] == DateMathSeparator &&
					i + 1 < segment.Count && segment.Array[segment.Offset + i + 1] == DateMathSeparator)
					return true;

				i++;
			}

			return false;
		}
	}
}
