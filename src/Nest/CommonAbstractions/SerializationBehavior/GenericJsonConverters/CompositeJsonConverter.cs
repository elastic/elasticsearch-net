using System;
using System.Runtime.Serialization;

namespace Nest
{
	internal class CompositeJsonConverter<TRead, TWrite> : JsonConverter
		where TRead : JsonConverter, new()
		where TWrite : JsonConverter, new()
	{
		public CompositeJsonConverter()
		{
			Read = new TRead();
			Write = new TWrite();
		}

		public override bool CanRead => true;
		public override bool CanWrite => true;
		private TRead Read { get; set; }
		private TWrite Write { get; set; }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => Write.WriteJson(writer, value, serializer);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			Read.ReadJson(reader, objectType, existingValue, serializer);

		public override bool CanConvert(Type objectType) => true;
	}
}
