using System;
using System.Collections;
using System.Globalization;
using Elasticsearch.Net;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// JSON converter for IDictionary that ignores the contract resolver (e.g. CamelCasePropertyNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	public class FieldNameQueryConverter: JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(IFieldNameQuery).IsAssignableFrom(objectType);
		}

		public override bool CanRead
		{
			get { return false; }
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new InvalidOperationException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFieldNameQuery;
			if (v == null || v.IsConditionless)
				return;

			var fieldName = v.GetFieldName();
			if (fieldName == null)
				return;

			var contract = serializer.ContractResolver as ElasticContractResolver;
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