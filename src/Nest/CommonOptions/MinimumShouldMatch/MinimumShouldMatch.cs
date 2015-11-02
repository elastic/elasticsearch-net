using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(MinimumShouldMatchJsonConverter))]
	public class MinimumShouldMatch : Union<int?, string>
	{
		public MinimumShouldMatch(int count) : base(count) { }

		public MinimumShouldMatch(string percentage) : base(percentage) { }

		public static MinimumShouldMatch Fixed(int count) => count;
		public static MinimumShouldMatch Percentage(float percentage) => $"{percentage}%";

		public static implicit operator MinimumShouldMatch(string first) => new MinimumShouldMatch(first);
		public static implicit operator MinimumShouldMatch(int second) => new MinimumShouldMatch(second);
	}

	internal class MinimumShouldMatchJsonConverter :JsonConverter 
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public static UnionJsonConverter<int?, string> Unionconverter = new UnionJsonConverter<int?, string>();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var union = Unionconverter.ReadJson(reader, objectType, existingValue, serializer) as Union<int?, string>;
			if (union.Item1.HasValue) return new MinimumShouldMatch(union.Item1.Value);
			return new MinimumShouldMatch(union.Item2);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Unionconverter.WriteJson(writer, value, serializer);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
