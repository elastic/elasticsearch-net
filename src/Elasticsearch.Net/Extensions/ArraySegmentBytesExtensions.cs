using System;
using System.Runtime.CompilerServices;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Elasticsearch.Net.Extensions
{
	internal static class ArraySegmentBytesExtensions
	{
		private const byte DateMathSeparator = (byte)'|';
		private const byte DecimalPoint = (byte)'.';

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsDouble(this ref ArraySegment<byte> arraySegment)
		{
			var i = 0;
			while (i < arraySegment.Count)
			{
				if (arraySegment.Array != null && arraySegment.Array[arraySegment.Offset + i] == DecimalPoint)
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

			for (var i = 0; i < bytes.Length; i++)
			{
				if (arraySegment.Array != null && bytes[i] != arraySegment.Array[arraySegment.Offset + i])
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
				if (segment.Array != null && (segment.Array[segment.Offset + i] == DateMathSeparator &&
					i + 1 < segment.Count && segment.Array[segment.Offset + i + 1] == DateMathSeparator))
					return true;

				i++;
			}

			return false;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static string Utf8String(this ref ArraySegment<byte> segment) =>
			// segment.Array is never null
			// ReSharper disable once AssignNullToNotNullAttribute
			StringEncoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
	}
}
