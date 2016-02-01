using System;
using Newtonsoft.Json;

namespace Nest
{
	internal abstract class JsonConverterBase<T> : JsonConverter where T : class
	{
		public override bool CanConvert(Type objectType) => true;
		public override bool CanWrite => true;
		public override bool CanRead => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as T;
			if (v == null) return;
			this.WriteJson(writer, v, serializer);
		}

		public abstract void WriteJson(JsonWriter writer, T value, JsonSerializer serializer);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

	}
}