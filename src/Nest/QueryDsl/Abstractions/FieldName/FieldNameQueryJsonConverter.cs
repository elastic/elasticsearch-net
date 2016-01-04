using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldNameQueryJsonConverter<TReadAs> : ReserializeJsonConverter<TReadAs, IFieldNameQuery>
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

			var settings = serializer.GetConnectionSettings();

			var field = settings?.Inferrer.Field(fieldName);
			if (field.IsNullOrEmpty()) return;

			writer.WriteStartObject();
			{
				writer.WritePropertyName(field);
				{
					this.Reserialize(writer, value, serializer);
				}
				writer.WriteEndObject();
			}
		}
	}
}