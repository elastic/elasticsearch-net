using System;
using Nest.Resolvers;
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

	public class FieldNameQueryConverter<TReadAs> : ReserializeJsonConverter<TReadAs, IFieldNameQuery>
		where TReadAs : class, IFieldNameQuery, new()
	{
		protected override object DeserializeJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			//{
			reader.Read(); //property name
			var fieldName = reader.Value as string;
			reader.Read(); //{
			var query = this.ReadAs(reader, objectType, existingValue, serializer);
			if (query == null) return null;
			query.Field = fieldName;

			return query;
		}

		protected override void SerializeJson(JsonWriter writer, object value, IFieldNameQuery castValue, JsonSerializer serializer)
		{
			var fieldName = castValue.Field;
			if (fieldName == null)
				return;

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;

			var field = contract.Infer.PropertyPath(fieldName);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				writer.WriteStartObject();
				{
					this.Reserialize(writer, value, serializer);
				}
				writer.WriteEndObject();
			}
			writer.WriteEndObject();
		}
	}
}