using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class BulkRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var bulk = value as IBulkRequest;
			var settings = serializer?.GetConnectionSettings();
			var requestResponseSerializer = settings?.RequestResponseSerializer;
			var sourceSerializer = settings?.SourceSerializer;
			if (requestResponseSerializer == null|| bulk?.Operations == null) return ;

			foreach(var op in bulk.Operations)
			{
				op.Index = op.Index ?? bulk.Index ?? op.ClrType;
				if (op.Index.Equals(bulk.Index)) op.Index = null;
				op.Type = op.Type ?? bulk.Type ?? op.ClrType;
				if (op.Type.Equals(bulk.Type)) op.Type = null;
				op.Id = op.GetIdForOperation(settings.Inferrer);

				var opJson = requestResponseSerializer.SerializeToString(op, SerializationFormatting.None);
				writer.WriteRaw($"{{\"{op.Operation}\":" + opJson + "}\n");
				var body = op.GetBody();
				if (body == null) continue;
				var bodyJson = (op.Operation == "update" ? requestResponseSerializer : sourceSerializer)
					.SerializeToString(body, SerializationFormatting.None);

				writer.WriteRaw(bodyJson + "\n");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

	}
}
