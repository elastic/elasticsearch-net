using System;
using Utf8Json;
using Utf8Json.Internal;
using Utf8Json.Resolvers;

namespace Nest
{
	internal class SuggestContextFormatter : IJsonFormatter<ISuggestContext>
	{
		private static readonly AutomataDictionary ContextTypes = new AutomataDictionary
		{
			{ "geo", 0 },
			{ "category", 1 }
		};

		public ISuggestContext Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var segment = reader.ReadNextBlockSegment();
			var segmentReader = new JsonReader(segment.Array, segment.Offset);

			var count = 0;
			ArraySegment<byte> contextType = default;
			while (segmentReader.ReadIsInObject(ref count))
			{
				if (segmentReader.ReadPropertyName() == "type")
				{
					contextType = segmentReader.ReadStringSegmentRaw();
					break;
				}

				segmentReader.ReadNextBlock();
			}

			segmentReader = new JsonReader(segment.Array, segment.Offset);

			if (ContextTypes.TryGetValue(contextType, out var value))
			{
				switch (value)
				{
					case 0:
						return Deserialize<GeoSuggestContext>(ref segmentReader, formatterResolver);
					case 1:
						return Deserialize<CategorySuggestContext>(ref segmentReader, formatterResolver);
				}
			}

			return Deserialize<CategorySuggestContext>(ref segmentReader, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, ISuggestContext value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			formatterResolver.GetFormatter<object>().Serialize(ref writer, value, formatterResolver);
		}

		private static TContext Deserialize<TContext>(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
			where TContext : ISuggestContext
		{
			var formatter = formatterResolver.GetFormatter<TContext>();
			return formatter.Deserialize(ref reader, formatterResolver);
		}
	}
}
