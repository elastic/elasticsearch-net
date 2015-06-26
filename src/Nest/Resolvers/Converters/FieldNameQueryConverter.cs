using System;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{


	/// <summary>
	/// JSON converter for IDictionary that ignores the contract resolver (e.g. CamelCasePropertyNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	public class FieldNameQueryConverter<T>: JsonConverter
		where T : class, new()
	{
		private readonly ReadAsTypeConverter<T> _reader;

		public FieldNameQueryConverter()
		{
			this._reader = new ReadAsTypeConverter<T>();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(IFieldNameQuery).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return true; }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var depth = reader.Depth;
			if (reader.TokenType != JsonToken.StartObject)
				return null;
			reader.Read();
			var fieldName = reader.Value as string;
			reader.Read();
			var query = this._reader.ReadJson(reader, objectType, existingValue, serializer);
			var setter = query as IFieldNameQuery;
			if (setter != null)
				setter.SetFieldName(fieldName);
			do
			{
				reader.Read();
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);


			return query;


		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFieldNameQuery;
			if (v == null) return;

			var fieldName = v.GetFieldName();
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
				serializer.Serialize(writer, value);
			}
			writer.WriteEndObject();
		}
	}
}