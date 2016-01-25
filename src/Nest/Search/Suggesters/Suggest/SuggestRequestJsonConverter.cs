using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class SuggestRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var suggestRequest = value as ISuggestRequest;
			if (suggestRequest == null) return;

			writer.WriteStartObject();
			if (!suggestRequest.GlobalText.IsNullOrEmpty())
			{
				writer.WritePropertyName("text");
				writer.WriteValue(suggestRequest.GlobalText);
			}

			if (suggestRequest.Suggest != null)
			{
				foreach (var kv in (IEnumerable<KeyValuePair<string, ISuggestBucket>>)suggestRequest.Suggest)
				{
					writer.WritePropertyName(kv.Key);
					serializer.Serialize(writer, kv.Value);
				}
			}
			writer.WriteEndObject();
		}
	}
}