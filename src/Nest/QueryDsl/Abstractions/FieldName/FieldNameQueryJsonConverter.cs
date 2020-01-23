using System;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldNameQueryJsonConverter<TReadAs> : ReserializeJsonConverter<TReadAs, IFieldNameQuery>
		where TReadAs : class, IFieldNameQuery, new()
	{
		protected override object DeserializeJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			reader.Read(); //property name
			var fieldName = (string)reader.Value;
			reader.Read();
			TReadAs query = null;

			switch (reader.TokenType)
			{
				case JsonToken.StartObject:
					query = ReadAs(reader, objectType, existingValue, serializer);
					break;
				case JsonToken.Null:
					break;
				default:
					query = new TReadAs();
					switch (query)
					{
						case ITermQuery termQuery:
							termQuery.Value = reader.Value;
							break;
						case IMatchQuery matchQuery:
							matchQuery.Query = (string)reader.Value;
							break;
						case IMatchPhraseQuery matchPhraseQuery:
							matchPhraseQuery.Query = (string)reader.Value;
							break;
						case IMatchPhrasePrefixQuery matchPhrasePrefixQuery:
							matchPhrasePrefixQuery.Query = (string)reader.Value;
							break;
					}
					break;
			}

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
			writer.WritePropertyName(field);

			Reserialize(writer, value, serializer);

			writer.WriteEndObject();
		}
	}
}
