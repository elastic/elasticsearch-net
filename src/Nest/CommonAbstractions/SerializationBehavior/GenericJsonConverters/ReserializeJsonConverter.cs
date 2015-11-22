using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class ReserializeJsonConverter<TReadAs, TInterface> : JsonConverter
		where TReadAs : class, TInterface, new()
		where TInterface : class
	{
		protected ReadAsTypeJsonConverter<TReadAs> Reader { get; } = new ReadAsTypeJsonConverter<TReadAs>();

		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => typeof(TInterface).IsAssignableFrom(objectType);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			var depth = reader.Depth;
			var deserialized = this.DeserializeJson(reader, objectType, existingValue, serializer);
			return reader.ReadToEnd(depth, deserialized);
		}

		protected TReadAs ReadAs(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			this.Reader.ReadJson(reader, objectType, existingValue, serializer) as TReadAs;

		protected virtual object DeserializeJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			this.ReadAs(reader, objectType, existingValue, serializer);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var custom = value as ICustomJson;
			if (custom != null)
			{
				var json = custom.GetCustomJson();
				var rawJson = json as RawJson;
				if (rawJson != null)
				{
					writer.WriteRawValue(rawJson.Data);
					return;
				}
			}

			var v = value as TInterface;
			if (v != null)
			{
				this.SerializeJson(writer, value, v, serializer);
			}
		}

		protected virtual void SerializeJson(JsonWriter writer, object value, TInterface castValue, JsonSerializer serializer)
		{
			this.Reserialize(writer, value, serializer);
		}

		protected void Reserialize(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var properties = value.GetType().GetCachedObjectProperties();
			if (properties.Count == 0) return;
			writer.WriteStartObject();
			foreach (var p in properties)
			{
				if (p.Ignored) continue;
				var vv = p.ValueProvider.GetValue(value);
				if (vv == null) continue;
				writer.WritePropertyName(p.PropertyName);
				serializer.Serialize(writer, vv);
			}
			writer.WriteEndObject();
		}
	}
}