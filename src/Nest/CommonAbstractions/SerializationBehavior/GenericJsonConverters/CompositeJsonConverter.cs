using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class CompositeJsonConverter<TRead, TWrite> : JsonConverter
		where TRead : JsonConverter, new()
		where TWrite : JsonConverter, new()
	{
		private TRead Read { get; set; }
		private TWrite Write { get; set; }

		public override bool CanRead => true;
		public override bool CanWrite => true;

		public CompositeJsonConverter()
		{
			this.Read = new TRead();
			this.Write = new TWrite();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			this.Write.WriteJson(writer, value, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return this.Read.ReadJson(reader, objectType, existingValue, serializer);
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}