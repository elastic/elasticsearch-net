using Elasticsearch.Net;

namespace Nest
{
	internal class FieldNameQueryFormatter<T, TInterface> : ReadAsFormatter<T, TInterface>
		where T : class, TInterface, IFieldNameQuery, new()
		where TInterface : class, IFieldNameQuery
	{
		public override void Serialize(ref JsonWriter writer, TInterface value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var fieldName = value.Field;
			if (fieldName == null)
				return;

			var settings = formatterResolver.GetConnectionSettings();
			var field = settings.Inferrer.Field(fieldName);
			if (field.IsNullOrEmpty())
				return;

			writer.WriteBeginObject();
			writer.WritePropertyName(field);

			base.Serialize(ref writer, value, formatterResolver);

			writer.WriteEndObject();
		}

		public override TInterface Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			reader.ReadIsBeginObjectWithVerify();
			var token = reader.GetCurrentJsonToken();

			TInterface query = null;
			string fieldName = null;
			if (token != JsonToken.EndObject)
			{
				fieldName = reader.ReadPropertyName();
				query = base.Deserialize(ref reader, formatterResolver);
			}

			reader.ReadIsEndObjectWithVerify();

			if (query == null)
				return null;

			query.Field = fieldName;
			return query;
		}
	}
}
