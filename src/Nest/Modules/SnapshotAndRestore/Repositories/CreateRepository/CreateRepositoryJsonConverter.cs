using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class CreateRepositoryJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as ICreateRepositoryRequest;
			serializer.Serialize(writer, v.Repository);
		}
	}
}