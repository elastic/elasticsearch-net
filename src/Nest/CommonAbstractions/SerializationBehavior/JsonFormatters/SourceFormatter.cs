using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	internal class SourceFormatter<T> : IJsonFormatter<T>
	{
		public virtual SerializationFormatting? ForceFormatting { get; } = null;

		public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var settings = formatterResolver.GetConnectionSettings();

			// avoid deserialization through stream when not using custom source serializer
			if (ReferenceEquals(settings.SourceSerializer, settings.RequestResponseSerializer))
				return formatterResolver.GetFormatter<T>().Deserialize(ref reader, formatterResolver);

			var arraySegment = reader.ReadNextBlockSegment();
			using (var ms = settings.MemoryStreamFactory.Create(arraySegment.Array, arraySegment.Offset, arraySegment.Count))
				return settings.SourceSerializer.Deserialize<T>(ms);
		}

		public virtual void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
		{
			var settings = formatterResolver.GetConnectionSettings();

			// avoid serialization to bytes when not using custom source serializer
			if (ReferenceEquals(settings.SourceSerializer, settings.RequestResponseSerializer))
			{
				formatterResolver.GetFormatter<T>().Serialize(ref writer, value, formatterResolver);
				return;
			}

			var sourceSerializer = settings.SourceSerializer;
			var f = ForceFormatting ?? SerializationFormatting.None;
			byte[] bytes;
			using (var ms = settings.MemoryStreamFactory.Create())
			{
				sourceSerializer.Serialize(value, ms, f);
				// TODO: read each byte instead of creating and allocating an array
				bytes = ms.ToArray();
			}

			writer.WriteRaw(bytes);
		}
	}
}
