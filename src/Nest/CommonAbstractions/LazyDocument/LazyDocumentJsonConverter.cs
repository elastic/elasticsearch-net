using System;
using Utf8Json;

namespace Nest
{
	internal class LazyDocumentJsonFormatter : IJsonFormatter<ILazyDocument>
	{
		public void Serialize(ref JsonWriter writer, ILazyDocument value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var lazyDocument = (LazyDocument)value;

			for (var i = lazyDocument.ArraySegment.Offset; i < lazyDocument.ArraySegment.Count; i++)
				writer.WriteByte(lazyDocument.ArraySegment.Array[i]);
		}

		public ILazyDocument Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			// TODO: ensure this handles all types. May need switch () { reader.ReadNumberSegment(), etc. }
			var arraySegment = reader.ReadNextBlockSegment();

			return new LazyDocument(arraySegment, formatterResolver);
		}
	}
}
