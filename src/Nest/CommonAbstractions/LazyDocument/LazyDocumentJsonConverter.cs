using System;
using System.Linq;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	internal class LazyDocumentInterfaceFormatter : IJsonFormatter<ILazyDocument>
	{
		public void Serialize(ref JsonWriter writer, ILazyDocument value, IJsonFormatterResolver formatterResolver)
		{
			switch (value)
			{
				case null:
					writer.WriteNull();
					return;
				case LazyDocument lazyDocument:
					writer.WriteRaw(lazyDocument.Bytes);
					break;
				default: writer.WriteNull();
					break;
			}
		}

		public ILazyDocument Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			// TODO: ensure this handles all types. May need switch () { reader.ReadNumberSegment(), etc. }
			var arraySegment = reader.ReadNextBlockSegment();

			// copy byte array
			return new LazyDocument(BinaryUtil.ToArray(ref arraySegment), formatterResolver);
		}
	}

	internal class LazyDocumentFormatter : IJsonFormatter<LazyDocument>
	{
		public void Serialize(ref JsonWriter writer, LazyDocument value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteRaw(value.Bytes);
		}

		public LazyDocument Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var arraySegment = reader.ReadNextBlockSegment();

			// copy byte array
			return new LazyDocument(BinaryUtil.ToArray(ref arraySegment), formatterResolver);
		}
	}
}
