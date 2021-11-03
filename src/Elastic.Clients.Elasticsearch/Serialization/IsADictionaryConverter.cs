using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	internal sealed class IsADictionaryConverter<T, TValue> : JsonConverter<T> where T : IsADictionaryBase<string, TValue>
	{
		public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException();
		public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();

			foreach (var item in value)
			{
				writer.WritePropertyName(item.Key);
				JsonSerializer.Serialize(writer, item.Value, item.Value.GetType(), options);
			}

			writer.WriteEndObject();
		}
	}
}
