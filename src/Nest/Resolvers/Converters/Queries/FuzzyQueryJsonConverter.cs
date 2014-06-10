using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{


	/// <summary>
	/// JSON converter for IDictionary that ignores the contract resolver (e.g. CamelCasePropertyNamesContractResolver)
	/// when converting dictionary keys to property names.
	/// </summary>
	public class FuzzyQueryJsonConverter: JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return true;
		}

		public override bool CanRead { get { return true; } }

		public override bool CanWrite { get { return true; } }

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			if (!j.HasValues) return null;

			var firstProp = j.Properties().FirstOrDefault();
			if (firstProp == null) return null;

			var field = firstProp.Name;
			var jo = firstProp.Value.Value<JObject>();
			if (jo == null) return null;

			JToken v = null;
			if (!jo.TryGetValue("value", out v)) return null;
			
			IFuzzyQuery fq = null;
			if (v.Type == JTokenType.Date) fq = new FuzzyDateQueryDescriptor<object>();
			else if (v.Type == JTokenType.String) fq = new FuzzyQueryDescriptor<object>();
			else if (v.Type == JTokenType.Integer || v.Type == JTokenType.Float) fq = new FuzzyNumericQueryDescriptor<object>();
			else return null;

			fq.Field = field;
			fq.Boost = GetPropValue<double?>(jo, "boost");
			fq.Fuzziness = GetPropValue<string>(jo, "fuzziness");
			fq.MaxExpansions = GetPropValue<int?>(jo, "max_expansions");
			fq.UnicodeAware = GetPropValue<bool?>(jo, "unicode_aware");
			fq.Transpositions = GetPropValue<bool?>(jo, "transpositions");
			var rewriteString = GetPropValue<string>(jo, "rewrite");
			if (!rewriteString.IsNullOrEmpty())
				fq.Rewrite = Enum.Parse(typeof(RewriteMultiTerm), rewriteString) as RewriteMultiTerm?;
			
			if (fq is IStringFuzzyQuery)
			{
				var fqs = fq as IStringFuzzyQuery;
				fqs.PrefixLength = GetPropValue<int?>(jo, "prefix_length"); 
				fqs.Value = GetPropValue<string>(jo, "value"); 
			}
			if (fq is IFuzzyDateQuery)
			{
				var fdq = fq as IFuzzyDateQuery;
				fdq.Value = GetPropValue<DateTime?>(jo, "value"); 
			}
			if (fq is IFuzzyNumericQuery)
			{
				var fnq = fq as IFuzzyNumericQuery;
				fnq.Value = GetPropValue<double?>(jo, "value");  
			}

			return fq;
		}

		public TReturn GetPropValue<TReturn>(JObject jObject, string propertyName)
		{
			JToken jToken = null;
			return !jObject.TryGetValue(propertyName, out jToken) 
				? default(TReturn) 
				: jToken.Value<TReturn>();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = value as IFieldNameQuery;
			if (v == null || v.IsConditionless)
				return;

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