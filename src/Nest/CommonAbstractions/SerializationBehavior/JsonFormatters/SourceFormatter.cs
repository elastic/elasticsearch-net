using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class CollapsedSourceFormatter<T> : SourceFormatter<T>
	{
		public override SerializationFormatting? ForceFormatting { get; } = SerializationFormatting.None;
	}

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
			// This used to check for reference of the source serializer and the request response serializer
			// However each now gets wrapped in a new `DiagnosticsSerializerProxy` so this check no longer works
			// therefor we now check if the SourceSerializer is internal with a formatter.
			// DiagnosticsSerializerProxy implements this interface, it simply proxies to whatever it wraps so
			// we need to assert the resolver is not actually null here since it can wrap something that is not
			// `IInternalSerializerWithFormatter`
			if (settings.SourceSerializer is IInternalSerializerWithFormatter s && s.FormatterResolver != null)
			{
				formatterResolver.GetFormatter<T>().Serialize(ref writer, value, formatterResolver);
				return;
			}

			var sourceSerializer = settings.SourceSerializer;
			var f = ForceFormatting ?? SerializationFormatting.None;

			writer.WriteSerialized(value, sourceSerializer, settings, f);
		}
	}
}
