using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest.Resolvers.Converters.Queries
{
	public class StopWordsToStringJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			string value = null;
			if (reader.TokenType == JsonToken.StartArray)
			{
				JArray o = JArray.Load(reader);
				value = string.Join(",", o.ToObject<List<string>>().Select(s=>s.Trim()));
			}
			else if (reader.TokenType == JsonToken.String)
			{
				value = reader.Value as string;
			}
			return value;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = value as string;
			if (string.IsNullOrWhiteSpace(s))
			{
				writer.WriteNull();
				return;
			}
			var tokens = s.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (tokens.Count == 0)
			{
				writer.WriteNull();
				return;
			}
			if (tokens.Count == 1  && tokens[0].StartsWith("_") && tokens[0].EndsWith("_"))
			{
				writer.WriteValue(tokens[0]);
				return;
			}
			writer.WriteStartArray();
			foreach(var token in tokens) writer.WriteValue(token);
			writer.WriteEndArray();
		}
	}

	public class StopWordsToListJsonConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			List<string> value = null;
			if (reader.TokenType == JsonToken.StartArray)
			{
				JArray o = JArray.Load(reader);
				value = o.ToObject<List<string>>().Select(s=>s.Trim()).ToList();
			}
			else if (reader.TokenType == JsonToken.String)
			{
				value = new List<string> { reader.Value as string };
			}
			return value;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = ((value as IEnumerable<string>) ?? Enumerable.Empty<string>()).ToList();
			if (s == null || !s.Any())
			{
				writer.WriteNull();
				return;
			}
			if (s.Count == 1  && s[0].StartsWith("_") && s[0].EndsWith("_"))
			{
				writer.WriteValue(s[0]);
				return;
			}
			writer.WriteStartArray();
			foreach(var token in s) writer.WriteValue(token);
			writer.WriteEndArray();
		}
	}
}
