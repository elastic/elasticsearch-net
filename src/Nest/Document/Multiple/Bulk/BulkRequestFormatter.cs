using System;
using Elasticsearch.Net;

namespace Nest
{
	internal class BulkRequestFormatter : IJsonFormatter<IBulkRequest>
	{
		private const byte Newline = (byte)'\n';

		public IBulkRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver) =>
			throw new NotSupportedException();

		public void Serialize(ref JsonWriter writer, IBulkRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value?.Operations == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
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

				var bodySerializer = op.Operation == "update" || body is ILazyDocument
					? requestResponseSerializer
					: sourceSerializer;

				var bodyBytes = bodySerializer.SerializeToBytes(body, SerializationFormatting.None);
				writer.WriteRaw(bodyBytes);
				writer.WriteRaw(Newline);
			}
		}
	}
}
