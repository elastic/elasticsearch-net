using System;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class ActAsQueryConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IActAsQuery;
			if (v == null)
				return;

			serializer.Serialize(writer, v._Query);
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