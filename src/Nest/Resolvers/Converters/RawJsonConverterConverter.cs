using System;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class RawJsonConverter : JsonConverter
	{
		public override bool CanRead
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				return;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}