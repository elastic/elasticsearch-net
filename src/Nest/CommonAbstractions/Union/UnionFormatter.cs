using System;
using Utf8Json;

namespace Nest
{
	internal class UnionFormatter<TFirst, TSecond> : IJsonFormatter<Union<TFirst, TSecond>>
	{
		public Union<TFirst, TSecond> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			Union<TFirst, TSecond> union = null;
			var segment = reader.ReadNextBlockSegment();
			if (TryRead(ref segment, formatterResolver, out TFirst first))
				union = first;
			else if (TryRead(ref segment, formatterResolver, out TSecond second))
				union = second;

			return union;
		}

		public void Serialize(ref JsonWriter writer, Union<TFirst, TSecond> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value._tag)
			{
				case 0:
				{
					var formatter = formatterResolver.GetFormatter<TFirst>();
					formatter.Serialize(ref writer, value.Item1, formatterResolver);
					break;
				}
				case 1:
				{
					var formatter = formatterResolver.GetFormatter<TSecond>();
					formatter.Serialize(ref writer, value.Item2, formatterResolver);
					break;
				}
				default:
					throw new Exception($"Unrecognized tag value: {value._tag}");
			}
		}

		public bool TryRead<T>(ref ArraySegment<byte> segment, IJsonFormatterResolver formatterResolver, out T v)
		{
			var segmentReader = new JsonReader(segment.Array, segment.Offset);
			try
			{
				var formatter = formatterResolver.GetFormatter<T>();
				v = formatter.Deserialize(ref segmentReader, formatterResolver);
				return true;
			}
			catch
			{
				v = default(T);
				return false;
			}
		}
	}
}
