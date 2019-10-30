using System;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class BulkRequestFormatter : IJsonFormatter<IBulkRequest>
	{
		private const byte Newline = (byte)'\n';

		private static SourceWriteFormatter<object> SourceWriter { get; } = new SourceWriteFormatter<object>();

		public IBulkRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IBulkRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Operations == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
			var memoryStreamFactory = settings.MemoryStreamFactory;
			var requestResponseSerializer = settings.RequestResponseSerializer;
			var sourceSerializer = settings.SourceSerializer;
			var inferrer = settings.Inferrer;
			var formatter = formatterResolver.GetFormatter<object>();

			for (var index = 0; index < value.Operations.Count; index++)
			{
				var op = value.Operations[index];
				op.Index = op.Index ?? value.Index ?? op.ClrType;
				if (op.Index.Equals(value.Index)) op.Index = null;
				op.Id = op.GetIdForOperation(inferrer);
				op.Routing = op.GetRoutingForOperation(inferrer);

				writer.WriteBeginObject();
				writer.WritePropertyName(op.Operation);

				formatter.Serialize(ref writer, op, formatterResolver);
				writer.WriteEndObject();
				writer.WriteRaw(Newline);

				var body = op.GetBody();
				if (body == null)
					continue;

				SourceWriter.Serialize(ref writer, body, formatterResolver);
				writer.WriteRaw(Newline);
			}
		}
	}
}
