// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;

namespace Nest
{
	internal class UnionFormatter<TFirst, TSecond> : IJsonFormatter<Union<TFirst, TSecond>>
	{
		private readonly bool _attemptTSecondIfTFirstIsNull;

		public UnionFormatter() => _attemptTSecondIfTFirstIsNull = false;

		public UnionFormatter(bool attemptTSecondIfTFirstIsNull) => _attemptTSecondIfTFirstIsNull = attemptTSecondIfTFirstIsNull;

		public Union<TFirst, TSecond> Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var segment = reader.ReadNextBlockSegment();
			if (TryRead(ref segment, formatterResolver, out TFirst first))
			{
				if (first == null && _attemptTSecondIfTFirstIsNull)
				{
					if (TryRead(ref segment, formatterResolver, out TSecond second))
						return second;
				}
				else
					return first;
			}
			else if (TryRead(ref segment, formatterResolver, out TSecond second))
				return second;

			return null;
		}

		public void Serialize(ref JsonWriter writer, Union<TFirst, TSecond> value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Tag)
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
					throw new Exception($"Unrecognized tag value: {value.Tag}");
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
				v = default;
				return false;
			}
		}
	}
}
