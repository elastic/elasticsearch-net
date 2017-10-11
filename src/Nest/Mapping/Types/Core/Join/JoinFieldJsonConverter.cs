using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class JoinFieldJsonConverter :JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;

		public static UnionJsonConverter<ParentJoinField, ChildJoinField> Unionconverter =
			new UnionJsonConverter<ParentJoinField, ChildJoinField>();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.String)
			{
				var parent = reader.Value.ToString();
				return new ParentJoinField(parent);
			}
			var jObject = JObject.Load(reader);
			if (jObject.Properties().Any(p=>p.Name == "parent"))
				using(var childReader =  jObject.CreateReader())
					return serializer.Deserialize<ChildJoinField>(childReader);

			using(var parentReader = jObject.CreateReader())
				return serializer.Deserialize<ParentJoinField>(parentReader);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Unionconverter.WriteJson(writer, value, serializer);
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
