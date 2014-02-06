using System;
using Newtonsoft.Json;

namespace Nest.Resolvers.Converters
{
	public class ActAsSearchDescriptorConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IActAsSearchDescriptor;
			if (v == null)
				return;

			serializer.Serialize(writer, v._SearchDescriptor);
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