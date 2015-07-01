using System;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public abstract class ReserializeJsonConverter<TReadAs, TInterface> : JsonConverter
		where TReadAs : class, TInterface, new()
		where TInterface : class
	{
		protected ReadAsTypeConverter<TReadAs> Reader { get; } = new ReadAsTypeConverter<TReadAs>();

		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => typeof(TInterface).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			var depth = reader.Depth;
			var deserialized = this.DeserializeJson(reader, objectType, existingValue, serializer);
			
			//TODO this might not be necessary
			do
			{
				reader.Read();
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);

			return deserialized;
		}

		protected TReadAs ReadAs(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			this.Reader.ReadJson(reader, objectType, existingValue, serializer) as TReadAs;

		protected abstract object DeserializeJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as TInterface;
			if (v != null)
			{
				this.SerializeJson(writer, value, v, serializer);
			}
		}
		protected abstract void SerializeJson(JsonWriter writer, object value, TInterface castValue, JsonSerializer serializer);

		protected void Reserialize(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var properties = value.GetCachedObjectProperties();
			foreach (var p in properties)
			{
				if (p.Ignored) continue;
				var vv = p.ValueProvider.GetValue(value);
				if (vv == null) continue;
				writer.WritePropertyName(p.PropertyName);
				serializer.Serialize(writer, vv);
			}
		}
	}
}