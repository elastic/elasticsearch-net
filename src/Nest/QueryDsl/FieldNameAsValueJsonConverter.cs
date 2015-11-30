using System;
using Newtonsoft.Json;
using Nest.Resolvers;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class FieldNameAsValueJsonConverter<TReadAs> : ReserializeJsonConverter<TReadAs,IFieldNameQuery>
		where TReadAs : class, IFieldNameQuery, new()
	{
		internal string MetaFieldName => "field";
		protected override object DeserializeJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var query = this.ReadAs(jo.CreateReader(), typeof(TReadAs), existingValue, serializer);
			query.Field = jo[this.MetaFieldName].ToString();

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

			var field = contract.Infer.Field(fieldName);
			if (field.IsNullOrEmpty())
				return;

			using (var sw = new StringWriter())
			using (var localWriter = new JsonTextWriter(sw))
			{
				this.Reserialize(localWriter, value, serializer);
				var jo = JObject.Parse(sw.ToString());
				jo.AddFirst(new JProperty(this.MetaFieldName, field));
				writer.WriteToken(jo.CreateReader());
            }
		}
	}
}
