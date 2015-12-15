using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(StopWordsJsonConverter))]
	public class StopWords : Union<string, IEnumerable<string>>
	{
		public StopWords(string item) : base(item) { }
		public StopWords(IEnumerable<string> item) : base(item) { }

		public static implicit operator StopWords(string first) => new StopWords(first);
		public static implicit operator StopWords(List<string> second) => new StopWords(second);
		public static implicit operator StopWords(string[] second) => new StopWords(second);
	}

	internal class StopWordsJsonConverter :JsonConverter 
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public static UnionJsonConverter<string, IEnumerable<string>> Unionconverter = new UnionJsonConverter<string, IEnumerable<string>>();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var union = Unionconverter.ReadJson(reader, objectType, existingValue, serializer) as Union<string, IEnumerable<string>>;
			if (union.Item1 != null) return new StopWords(union.Item1);
			return new StopWords(union.Item2);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Unionconverter.WriteJson(writer, value, serializer);
		}

		public override bool CanConvert(Type objectType) => true;
	}

}
