using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest.Resolvers.Converters
{
	public class ShardsSegmentConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{

		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartArray)
			{
				var list = new List<ShardsSegment>();
				serializer.Populate(reader, list);
				return list.First();
			}

			var o = new ShardsSegment();
			serializer.Populate(reader, o);
			return o;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ShardsSegment);
		}
	}
}