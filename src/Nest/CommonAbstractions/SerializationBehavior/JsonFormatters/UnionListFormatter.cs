using System.Collections.Generic;
using Utf8Json;

namespace Nest
{
	public class UnionListFormatter<TCollection, TFirst, TSecond> : IJsonFormatter<TCollection>
		where TCollection : List<Union<TFirst, TSecond>>, new()
	{
		private static readonly UnionFormatter<TFirst, TSecond> CharFilterFormatter =
			new UnionFormatter<TFirst, TSecond>();

		public TCollection Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;

			var count = 0;
			var list = new TCollection();
			reader.ReadIsBeginArrayWithVerify();
			while (!reader.ReadIsEndArrayWithSkipValueSeparator(ref count)) list.Add(CharFilterFormatter.Deserialize(ref reader, formatterResolver));

			return list;
		}

		public void Serialize(ref JsonWriter writer, TCollection value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginArray();
			if (value.Count != 0) CharFilterFormatter.Serialize(ref writer, value[0], formatterResolver);
			for (var i = 1; i < value.Count; i++)
			{
				writer.WriteValueSeparator();
				CharFilterFormatter.Serialize(ref writer, value[i], formatterResolver);
			}
			writer.WriteEndArray();
		}
	}
}
