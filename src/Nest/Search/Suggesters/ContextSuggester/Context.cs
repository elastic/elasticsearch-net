using System;
using System.Runtime.Serialization;

namespace Nest
{
	[ContractJsonConverter(typeof(ContextJsonConverter))]
	public class Context : Union<string, GeoLocation>
	{
		public Context(string category) : base(category) { }

		public Context(GeoLocation geo) : base(geo) { }

		public string Category => Item1;
		public GeoLocation Geo => Item2;

		public static implicit operator Context(string context) => new Context(context);

		public static implicit operator Context(GeoLocation context) => new Context(context);
	}

	internal class ContextJsonConverter : JsonConverter
	{
		public static UnionJsonConverter<string, GeoLocation> UnionConverter = new UnionJsonConverter<string, GeoLocation>();
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var union = UnionConverter.ReadJson(reader, objectType, existingValue, serializer) as Union<string, GeoLocation>;
			if (!union.Item1.IsNullOrEmpty()) return new Context(union.Item1);

			return new Context(union.Item2);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			UnionConverter.WriteJson(writer, value, serializer);
	}
}
