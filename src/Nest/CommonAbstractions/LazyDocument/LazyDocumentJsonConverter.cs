using System;
using System.Linq;
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

			if (value is LazyDocument lazyDocument)
				writer.WriteRaw(lazyDocument.Bytes);
			else
				writer.WriteNull();
		}

		public ILazyDocument Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			// TODO: ensure this handles all types. May need switch () { reader.ReadNumberSegment(), etc. }
			var arraySegment = reader.ReadNextBlockSegment();

			// copy byte array
			return new LazyDocument(arraySegment.ToArray(), formatterResolver);
		}
	}
}
