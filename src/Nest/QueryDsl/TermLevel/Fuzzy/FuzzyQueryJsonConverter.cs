using System;
using System.Linq;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class FuzzyQueryJsonConverter: JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;

		public override bool CanRead => true;

		public override bool CanWrite => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (!j.HasValues) return null;

			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			JToken v;
			if (!jo.TryGetValue("value", out v)) return null;

			IFuzzyQuery fq = new FuzzyQueryDescriptor<object>();
			fq.Field = field;
			fq.Boost = GetPropValue<double?>(jo, "boost");
			fq.Fuzziness = GetPropValue<string>(jo, "fuzziness");
			fq.MaxExpansions = GetPropValue<int?>(jo, "max_expansions");
			fq.UnicodeAware = GetPropValue<bool?>(jo, "unicode_aware");
			fq.Transpositions = GetPropValue<bool?>(jo, "transpositions");
			var rewriteString = GetPropValue<string>(jo, "rewrite");
			if (!rewriteString.IsNullOrEmpty())
				fq.Rewrite = rewriteString.ToEnum<RewriteMultiTerm>();
			
			if (v.Type == JTokenType.String)
			{
				fq.PrefixLength = GetPropValue<int?>(jo, "prefix_length"); 
				fq.Value = GetPropValue<string>(jo, "value"); 
			}
			else if (v.Type == JTokenType.Date)
			{
				fq.Value = GetPropValue<DateTime?>(jo, "value"); 
			}
			else if (v.Type == JTokenType.Integer || v.Type == JTokenType.Float)
			{
				fq.Value = GetPropValue<double?>(jo, "value");  
			}
			else
			{
				return null;
			}

			return fq;
		}

		public TReturn GetPropValue<TReturn>(JObject jObject, string FieldName)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(FieldName, out jToken) 
				? default(TReturn) 
				: jToken.Value<TReturn>();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFieldNameQuery;
			if (v == null || v.Conditionless)
				return;

			var fieldName = v.Field;
			if (fieldName == null)
				return;

			var contract = serializer.ContractResolver as SettingsContractResolver;
			if (contract == null)
				return;

			var field = contract.Infer.FieldName(fieldName);
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