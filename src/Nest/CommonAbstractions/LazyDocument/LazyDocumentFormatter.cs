// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;
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
					var reader = new JsonReader(lazyDocument.Bytes);
					LazyDocumentFormatter.WriteUnindented(ref reader, ref writer);
					break;
				default: writer.WriteNull();
					break;
			}
		}

		public ILazyDocument Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
			{
				reader.ReadNextBlock();
				return null;
			}

			var arraySegment = reader.ReadNextBlockSegment();

			// copy byte array
			return new LazyDocument(BinaryUtil.ToArray(ref arraySegment), formatterResolver);
		}
	}

	internal class LazyDocumentFormatter : IJsonFormatter<LazyDocument>
	{
		/// <summary>
		/// Removes indentation in JSON byte representation
		/// </summary>
		internal static void WriteUnindented(ref JsonReader reader, ref JsonWriter writer)
        {
            var token = reader.GetCurrentJsonToken();
            switch (token)
            {
                case JsonToken.BeginObject:
                    {
                        writer.WriteBeginObject();
                        var c = 0;
                        while (reader.ReadIsInObject(ref c))
                        {
                            if (c != 1)
								writer.WriteRaw((byte)',');
							writer.WritePropertyName(reader.ReadPropertyName());
							WriteUnindented(ref reader, ref writer);
                        }
                        writer.WriteEndObject();
                    }
                    break;
                case JsonToken.BeginArray:
                    {
                        writer.WriteBeginArray();
                        var c = 0;
                        while (reader.ReadIsInArray(ref c))
                        {
                            if (c != 1)
								writer.WriteRaw((byte)',');
							WriteUnindented(ref reader, ref writer);
                        }
                        writer.WriteEndArray();
                    }
                    break;
                case JsonToken.Number:
					var segment = reader.ReadNumberSegment();
					for (var i = 0; i < segment.Count; i++)
						// segment.Array never null
						// ReSharper disable once PossibleNullReferenceException
						writer.WriteRawUnsafe(segment.Array[i + segment.Offset]);
					break;
                case JsonToken.String:
					var s = reader.ReadString();
					writer.WriteString(s);
					break;
                case JsonToken.True:
                case JsonToken.False:
					var b = reader.ReadBoolean();
					writer.WriteBoolean(b);
					break;
                case JsonToken.Null:
					reader.ReadIsNull();
					writer.WriteNull();
					break;
            }
        }

		public void Serialize(ref JsonWriter writer, LazyDocument value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var reader = new JsonReader(value.Bytes);
			WriteUnindented(ref reader, ref writer);
		}

		public LazyDocument Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null) return null;

			var arraySegment = reader.ReadNextBlockSegment();

			// copy byte array
			return new LazyDocument(BinaryUtil.ToArray(ref arraySegment), formatterResolver);
		}
	}
}
