using System;
using System.Linq;
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
			if (!(request?.Documents.HasAny()).GetValueOrDefault(false))
			{
				writer.WriteEndObject();
				return;
			}
			var docs = request.Documents.Select(d =>
			{
				if (request.Index != null) d.Index = null;
				if (request.Type != null) d.Type = null;
				return d;
			}).ToList();

			var flatten = docs.All(p => p.CanBeFlattened);

			writer.WritePropertyName(flatten ? "ids" : "docs");
			writer.WriteStartArray();
			foreach (var id in docs)
			{
				if (flatten)
					serializer.Serialize(writer, id.Id);
				else
					serializer.Serialize(writer, id);
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
