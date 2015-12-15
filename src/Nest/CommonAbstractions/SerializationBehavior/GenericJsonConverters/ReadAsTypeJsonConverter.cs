using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class ReadAsTypeJsonConverter<T> : JsonConverter
		where T : class
	{
		public override bool CanRead => true;
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => 
			FromJson.ReadAs<T>(reader, objectType, existingValue, serializer);
	}
}