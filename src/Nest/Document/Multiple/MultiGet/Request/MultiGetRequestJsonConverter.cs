using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class MultiGetRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = value as IMultiGetRequest;
			writer.WriteStartObject();
			if (!(request?.GetOperations.HasAny()).GetValueOrDefault(false))
			{
				writer.WriteEndObject();
				return;
			}
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			foreach (var id in request.GetOperations)
			{
				if (request.Index != null && request.Type != null)
					writer.WriteValue(id.Id);
				else
				{
					if (request.Index != null) id.Index = null;

					serializer.Serialize(writer, id);
					//writer.WriteValue(id);
				}
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

	}
}
