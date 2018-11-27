using System;
using System.Runtime.Serialization;

namespace Nest
{
	internal class FieldJsonConverter : JsonConverter
	{
		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var field = (Field)value;
			if (field == null)
			{
				writer.WriteNull();
				return;
			}
			var settings = serializer.GetConnectionSettings();
			var fieldName = settings.Inferrer.Field(field);

			if (!string.IsNullOrEmpty(field.Format))
			{
				writer.WriteStartObject();
				writer.WritePropertyName("field");
				writer.WriteValue(fieldName);
				writer.WritePropertyName("format");
				writer.WriteValue(field.Format);
				writer.WriteEndObject();
			}
			else
			{
				writer.WriteValue(fieldName);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
					return new Field((string)reader.Value);
				case JsonToken.StartObject:
					string fieldName = null;
					double? boost = null;
					string format = null;
					reader.Read();

					while (reader.TokenType != JsonToken.EndObject)
					{
						if (reader.TokenType == JsonToken.PropertyName)
						{
							switch ((string)reader.Value)
							{
								case "field":
									fieldName = reader.ReadAsString();
									break;
								case "boost":
									boost = reader.ReadAsDouble();
									break;
								case "format":
									format = reader.ReadAsString();
									break;
								default:
									reader.Read();
									break;
							}

							reader.Read();
							continue;
						}

						break;
					}

					return new Field(fieldName, boost, format);
				default:
					return null;
			}
		}
	}
}
