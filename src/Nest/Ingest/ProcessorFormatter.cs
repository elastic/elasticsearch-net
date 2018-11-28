using Utf8Json;

namespace Nest
{
	internal class ProcessorFormatter<TProcessor> : IJsonFormatter<IProcessor>
		where TProcessor : IProcessor
	{
		public IProcessor Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			IProcessor processor = null;
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				reader.ReadPropertyName();
				var processFormatter = formatterResolver.GetFormatter<TProcessor>();
				processor = processFormatter.Deserialize(ref reader, formatterResolver);
			}

			return processor;
		}

		public void Serialize(ref JsonWriter writer, IProcessor value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Name == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			writer.WritePropertyName(value.Name);
			var processorFormatter = formatterResolver.GetFormatter<IProcessor>();
			processorFormatter.Serialize(ref writer, value, formatterResolver);
			writer.WriteEndObject();
		}
	}
}
