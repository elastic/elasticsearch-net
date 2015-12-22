using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class SuggestRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var searchRequest = serializer.Deserialize<SearchRequest>(reader);
			return new PutWarmerRequest("unknown name because its not on the body when deserializing") { Search = searchRequest };
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var suggestRequest = value as ISuggestRequest;
			if (suggestRequest == null) return;

			var dict = new Dictionary<string, object>();
			if (!suggestRequest.GlobalText.IsNullOrEmpty())
			{
				writer.WritePropertyName("text");
				writer.WriteValue(suggestRequest.GlobalText);
			}

			if (suggestRequest.Suggest != null)
			{
				foreach (var kv in suggestRequest.Suggest)
				{
					var item = kv.Value;
					var bucket = new SuggestBucket() { Text = item.Text };

					var completion = item as ICompletionSuggester;
					if (completion != null) bucket.Completion = completion;

					var phrase = item as IPhraseSuggester;
					if (phrase != null) bucket.Phrase = phrase;

					var term = item as ITermSuggester;
					if (term != null) bucket.Term = term;

					writer.WritePropertyName(kv.Key);
					serializer.Serialize(writer, bucket);
					dict.Add(kv.Key, bucket);
				}
			}
		}
	}
}